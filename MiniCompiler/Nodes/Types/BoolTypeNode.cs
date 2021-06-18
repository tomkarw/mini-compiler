using System.Text;

namespace MiniCompiler
{
    public class BoolTypeNode : SyntaxNode
    {
        public override string Type => "i1";
        
        public BoolTypeNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }
}