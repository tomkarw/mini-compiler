using System.Text;

namespace MiniCompiler
{
    public class DoubleTypeNode : SyntaxNode
    {
        public override string Type => "double";
        
        public DoubleTypeNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }
}