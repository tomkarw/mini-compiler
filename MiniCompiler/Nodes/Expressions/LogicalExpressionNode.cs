using System;
using System.Text;

namespace MiniCompiler
{
    public class LogicalExpressionNode : SyntaxNode
    {
        private readonly SyntaxNode _lhsExpression;
        private readonly SyntaxInfo _op;
        private readonly SyntaxNode _rhsExpression;

        public LogicalExpressionNode(SyntaxNode lhsExpression, SyntaxInfo logicalOp, SyntaxNode rhsExpression)
            : base(lhsExpression)
        {
            _lhsExpression = lhsExpression;
            _op = logicalOp;
            _rhsExpression = rhsExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: implement as shortened operations
            var lhs = _lhsExpression.GenCode(ref sb);
            var rhs = _rhsExpression.GenCode(ref sb);

            if (_lhsExpression.Type != "i1" || _rhsExpression.Type != "i1")
            {
                Context.AddError(_lhsExpression.Line, _lhsExpression.Column,
                    $"Cannot use '{_op.Text}' with {_lhsExpression.Type} and {_rhsExpression.Type} values");
            }


            var trueLab = Context.GetNewId();
            var falseLab = Context.GetNewId();
            var returnPointer = Context.GetNewId();
            var endLab = Context.GetNewId();
            var returnValue = Context.GetNewId();

            sb.AppendLine($"%{returnPointer} = alloca i1");

            switch (_op.Text)
            {
                case "||":
                {
                    sb.AppendLine($"br i1 %{lhs}, label %{trueLab}, label %{falseLab}");
                    sb.AppendLine($"{trueLab}:");
                    sb.AppendLine($"store i1 true, i1* %{returnPointer}");
                    sb.AppendLine($"br label %{endLab}");
                    sb.AppendLine($"{falseLab}:");
                    sb.AppendLine($"store i1 %{rhs}, i1* %{returnPointer}");
                    break;
                }
                case "&&":
                {
                    sb.AppendLine($"br i1 %{lhs}, label %{trueLab}, label %{falseLab}");
                    sb.AppendLine($"{trueLab}:");
                    sb.AppendLine($"store i1 %{rhs}, i1* %{returnPointer}");
                    sb.AppendLine($"br label %{endLab}");
                    sb.AppendLine($"{falseLab}:");
                    sb.AppendLine($"store i1 false, i1* %{returnPointer}");
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendLine($"br label %{endLab}");
            sb.AppendLine($"{endLab}:");
            sb.AppendLine($"%{returnValue} = load i1, i1* %{returnPointer}");

            Type = "i1";

            return returnValue;
        }
    }
}