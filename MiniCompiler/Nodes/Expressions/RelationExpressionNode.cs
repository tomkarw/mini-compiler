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

        public SyntaxNode LhsExpression;
        public SyntaxInfo Op;
        public SyntaxNode RhsExpression;

        public RelationExpressionNode(SyntaxNode lhsExpression, SyntaxInfo relationOp,
            SyntaxNode rhsExpression)
            : base(lhsExpression)
        {
            LhsExpression = lhsExpression;
            Op = relationOp;
            RhsExpression = rhsExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.GetNewId();
            var lhs = LhsExpression.GenCode(ref sb);
            var rhs = RhsExpression.GenCode(ref sb);
            string cmp;
            string cmpType;
            switch (LhsExpression.Type, RhsExpression.Type)
            {
                case ("i32", "i32"):
                {
                    cmp = (Op.Text == "==" || Op.Text == "!=") ? "icmp " : "icmp s";
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
                    if (!(Op.Text == "==" || Op.Text == "!="))
                    {
                        Context.AddError(
                            Op.Line, Op.Column,
                            $"Cannot compare boolean values with '{Op.Text}', only '==' and '!=' allowed"
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
                    Context.AddError(Op.Line, Op.Column,
                        $"Cannot compare {LhsExpression.Type} and {RhsExpression.Type} values");
                    break;
                }
            }

            Type = "i1";

            sb.AppendLine($"%{id} = {cmp}{_operationMappings[Op.Text]} {cmpType} %{lhs}, %{rhs}");

            return id;
        }
    }
}