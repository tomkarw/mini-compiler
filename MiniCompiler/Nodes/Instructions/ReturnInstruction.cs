using System.Text;

namespace MiniCompiler
{
    public class ReturnInstruction : SyntaxNode

    {
        public ReturnInstruction(SyntaxInfo si) : base(si)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            sb.AppendLine("ret i32 0");
            return null;
        }
    }
}