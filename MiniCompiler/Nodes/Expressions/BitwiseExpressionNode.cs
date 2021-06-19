using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class BitwiseExpressionNode : SyntaxNode
    {
        private static Dictionary<string, string> _operationMappings = new Dictionary<string, string>
        {
            {"|", "or"},
            {"&", "and"}
        };

        public SyntaxNode LhsExpression;
        public SyntaxInfo Op;
        public SyntaxNode RhsExpression;

        public BitwiseExpressionNode(SyntaxNode lhsExpression, SyntaxInfo op,
            SyntaxNode rhsExpression)
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
            
            if (LhsExpression.Type != "i32" || RhsExpression.Type != "i32")
            {
                Context.AddError(LhsExpression.Line, LhsExpression.Column,
                        $"Cannot use '{Op.Text}' with {LhsExpression.Type} and {RhsExpression.Type} values");
            }
            else
            {
                sb.AppendLine($"%{id} = {_operationMappings[Op.Text]} i32 %{lhs}, %{rhs}");
            }
            
            Type = "i32";

            return id;
        }
    }
}