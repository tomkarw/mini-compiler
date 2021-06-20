using System.Text;

namespace MiniCompiler
{
    public class WhileInstructionNode : SyntaxNode

    {
        private readonly SyntaxNode _condition;
        private readonly SyntaxNode _instruction;

        public WhileInstructionNode(SyntaxNode condition, SyntaxNode instruction) : base(condition)
        {
            _condition = condition;
            _instruction = instruction;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var startLab = Context.GetNewId();
            var innerLab = Context.GetNewId();
            var endLab = Context.GetNewId();
            
            Context.PushNestedLoop(startLab, endLab);
            
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
            
            Context.PopNestedLoop();
            
            return null;
        }
    }
}