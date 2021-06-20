using System.Text;

namespace MiniCompiler
{
    public class BreakInstruction : SyntaxNode
    {
        private readonly int _value;
        
        public BreakInstruction(SyntaxInfo si, string value) : base(si)
        {
            _value = int.Parse(value);
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var loopNumber = Context.NestedLoops.Count - _value;
            if (_value < 0 || _value > Context.NestedLoops.Count)
            {
                Context.AddError(Line, $"cannot break out of {_value} nested loops, only {Context.NestedLoops.Count} present");
            }
            else
            {
                sb.AppendLine($"br label %{Context.NestedLoops[loopNumber].EndLabel}");
            }

            return null;
        }
    }
}