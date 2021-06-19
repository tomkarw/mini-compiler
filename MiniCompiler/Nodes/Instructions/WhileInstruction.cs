using System.Text;

namespace MiniCompiler
{
    public class WhileInstruction : SyntaxNode

    {
        private readonly SyntaxNode _condition;
        private readonly SyntaxNode _instruction;

        public WhileInstruction(SyntaxNode condition, SyntaxNode instruction) : base(condition)
        {
            _condition = condition;
            _instruction = instruction;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var startLab = Context.GetNewId();
            var innerLab = Context.GetNewId();
            var endLab = Context.GetNewId();
            sb.AppendLine($"br label %{startLab}");
            sb.AppendLine($"{startLab}:");
            var conditionId = _condition.GenCode(ref sb);

            if (_condition.Type != "i1")
            {
                Context.AddError(_condition.Line, "While statement condition must return boolean");
            }

            sb.AppendLine($"br i1 %{conditionId}, label %{innerLab}, label %{endLab}");
            sb.AppendLine($"{innerLab}:");
            _instruction.GenCode(ref sb);
            sb.AppendLine($"br label %{startLab}");
            sb.AppendLine($"{endLab}:");
            return null;
        }
    }
}