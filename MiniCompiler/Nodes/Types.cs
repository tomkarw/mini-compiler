using System.Text;

namespace MiniCompiler
{
    public abstract class BaseTypeNode : SyntaxNode
    {
        protected BaseTypeNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }
    
    public class IntTypeNode : BaseTypeNode
    {
        public IntTypeNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}