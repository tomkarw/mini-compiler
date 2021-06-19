using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class MultiplicativeExpressionNode : SyntaxNode
    {
        private static Dictionary<string, string> _operationMappings = new Dictionary<string, string>
        {
            {"*", "mul"},
            {"/", "div"}
        };

        public SyntaxNode LhsExpression;
        public SyntaxInfo Op;
        public SyntaxNode RhsExpression;

        public MultiplicativeExpressionNode(SyntaxNode lhsExpression, SyntaxInfo op, SyntaxNode rhsExpression)
            : base(lhsExpression)
        {
            LhsExpression = lhsExpression;
            Op = op;
            RhsExpression = rhsExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.GetNewId();
            var lhs = LhsExpression.GenCode(ref sb);
            var rhs = RhsExpression.GenCode(ref sb);
            string op;
            string opType;
            switch (LhsExpression.Type, RhsExpression.Type)
            {
                case ("i32", "i32"):
                {
                    op = (Op.Text == "/") ? "s" : "";
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
                    op = "s";
                    opType = "i32";
                    Context.AddError(LhsExpression.Line, LhsExpression.Column,
                        $"Cannot use '{Op.Text}' with {LhsExpression.Type} and {RhsExpression.Type} values");
                    break;
                }
            }

            Type = opType;

            sb.AppendLine($"%{id} = {op}{_operationMappings[Op.Text]} {opType} %{lhs}, %{rhs}");

            return id;
        }
    }
}