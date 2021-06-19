using System.Text;

namespace MiniCompiler
{
    public class IntTypeNode : SyntaxNode
    {
        public override string Type => "i32";

        public IntTypeNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }
}