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
            throw new System.NotImplementedException();
        }
    }
}