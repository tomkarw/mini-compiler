using System.Linq;
using System.Text;

namespace MiniCompiler
{
    public class ContinueInstruction : SyntaxNode
    {
        public ContinueInstruction(SyntaxInfo si) : base(si)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            if (Context.NestedLoops.Count == 0)
            {
                Context.AddError(Line, $"continue used outside a loop");
            }
            else
            {
                sb.AppendLine($"br label %{Context.NestedLoops.Last().StartLabel}");
            }

            return null;
        }
    }
}