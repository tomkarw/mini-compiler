using System.Text;

namespace MiniCompiler
{
    public class IfInstructionNode : SyntaxNode

    {
        private readonly SyntaxNode _condition;
        private readonly SyntaxNode _thenInstruction;
        private readonly SyntaxNode _elseInstruction;

        public IfInstructionNode(SyntaxNode condition, SyntaxNode thenInstruction, SyntaxNode elseInstruction = null) :
            base(condition)
        {
            _condition = condition;
            _thenInstruction = thenInstruction;
            _elseInstruction = elseInstruction;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var t = _condition.GenCode(ref sb);

            if (_condition.Type != "i1")
            {
                Context.AddError(_condition.Line, "If statement condition must return boolean");
            }

            var trueLab = Context.GetNewId();
            var falseLab = Context.GetNewId();
            sb.AppendLine($"br i1 %{t}, label %{trueLab}, label %{falseLab}");
            sb.AppendLine($"{trueLab}:");
            _thenInstruction.GenCode(ref sb);
            var endLab = Context.GetNewId();
            sb.AppendLine($"br label %{endLab}");
            sb.AppendLine($"{falseLab}:");
            // else only if present
            _elseInstruction?.GenCode(ref sb);
            sb.AppendLine($"br label %{endLab}");
            sb.AppendLine($"{endLab}:");
            return null;
        }
    }
}