using System;
using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class AdditiveExpressionNode : SyntaxNode
    {
        private static Dictionary<string, string> _operationMappings = new Dictionary<string, string>
        {
            {"+", "add"},
            {"-", "sub"}
        };

        public SyntaxNode AdditiveExpression;
        public SyntaxInfo AdditiveOperator;
        public SyntaxNode MultiplicativeExpression;

        public AdditiveExpressionNode(SyntaxNode additiveExpression, SyntaxInfo additiveOperator,
            SyntaxNode multiplicativeExpression)
            : base(additiveExpression)
        {
            AdditiveExpression = additiveExpression;
            AdditiveOperator = additiveOperator;
            MultiplicativeExpression = multiplicativeExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.GetNewId();
            var lhs = AdditiveExpression.GenCode(ref sb);
            var rhs = MultiplicativeExpression.GenCode(ref sb);
            string op;
            string opType;
            switch (AdditiveExpression.Type, MultiplicativeExpression.Type)
            {
                case ("i32", "i32"):
                {
                    op = "";
                    opType = "i32";
                    break;
                }
                case ("double", "double"):
                {
                    op = "f";
                    opType = "double";
                    break;
                }
                case ("i32", "double"):
                {
                    // implicit conversion
                    var oldId = lhs;
                    lhs = Context.GetNewId();
                    sb.AppendLine($"%{lhs} = sitofp i32 %{oldId} to double");

                    op = "f";
                    opType = "double";
                    break;
                }
                case ("double", "i32"):
                {
                    // implicit conversion
                    var oldId = rhs;
                    rhs = Context.GetNewId();
                    sb.AppendLine($"%{rhs} = sitofp i32 %{oldId} to double");

                    op = "f";
                    opType = "double";
                    break;
                }
                default:
                {
                    op = "";
                    opType = "i32";
                    Context.AddError(AdditiveExpression.Line, AdditiveExpression.Column,
                        $"Cannot use '{AdditiveOperator.Text}' with {AdditiveExpression.Type} and {MultiplicativeExpression.Type} values");
                    break;
                }
            }

            Type = opType;
            
            sb.AppendLine($"%{id} = {op}{_operationMappings[AdditiveOperator.Text]} {opType} %{lhs}, %{rhs}");

            return id;
        }
    }
}