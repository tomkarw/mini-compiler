using System;
using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class RelationExpressionNode : SyntaxNode
    {
        private static Dictionary<string, string> _operationMappings = new Dictionary<string, string>
        {
            {"==", "eq"},
            {"!=", "ne"},
            {">", "gt"},
            {">=", "ge"},
            {"<", "lt"},
            {"<=", "le"}
        };

        public SyntaxNode RelationExpression;
        public SyntaxInfo RelationOperator;
        public SyntaxNode AdditiveExpression;

        public RelationExpressionNode(SyntaxNode relationExpression, SyntaxInfo relationOp,
            SyntaxNode additiveExpression)
            : base(relationExpression)
        {
            RelationExpression = relationExpression;
            RelationOperator = relationOp;
            AdditiveExpression = additiveExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.GetNewId();
            var lhs = RelationExpression.GenCode(ref sb);
            var rhs = AdditiveExpression.GenCode(ref sb);
            string cmp;
            string cmpType;
            switch (RelationExpression.Type, AdditiveExpression.Type)
            {
                case ("i32", "i32"):
                {
                    cmp = (RelationOperator.Text == "==" || RelationOperator.Text == "!=") ? "icmp " : "icmp s";
                    cmpType = "i32";
                    break;
                }
                case ("double", "double"):
                {
                    cmp = "fcmp o";
                    cmpType = "double";
                    break;
                }
                case ("i1", "i1"):
                {
                    cmp = "icmp ";
                    cmpType = "i1";
                    if (!(RelationOperator.Text == "==" || RelationOperator.Text == "!="))
                    {
                        Context.AddError(
                            RelationOperator.Line, RelationOperator.Column,
                            $"Cannot compare boolean values with '{RelationOperator.Text}', only '==' and '!=' allowed"
                        );
                    }

                    break;
                }
                case ("i32", "double"):
                {
                    // implicit conversion
                    var oldId = lhs;
                    lhs = Context.GetNewId();
                    sb.AppendLine($"%{lhs} = sitofp i32 %{oldId} to double");

                    cmp = "fcmp o";
                    cmpType = "double";
                    break;
                }
                case ("double", "i32"):
                {
                    // implicit conversion
                    var oldId = rhs;
                    rhs = Context.GetNewId();
                    sb.AppendLine($"%{rhs} = sitofp i32 %{oldId} to double");

                    cmp = "fcmp o";
                    cmpType = "double";
                    break;
                }
                default:
                {
                    cmp = "icmp ";
                    cmpType = "i32";
                    Context.AddError(RelationOperator.Line, RelationOperator.Column,
                        $"Cannot compare {RelationExpression.Type} and {AdditiveExpression.Type} values");
                    break;
                }
            }

            Type = "i1";

            sb.AppendLine($"%{id} = {cmp}{_operationMappings[RelationOperator.Text]} {cmpType} %{lhs}, %{rhs}");

            return id;
        }
    }
}