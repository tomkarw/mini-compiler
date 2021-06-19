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
            throw new System.NotImplementedException();
        }
    }
}