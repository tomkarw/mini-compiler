using System.Text;

namespace MiniCompiler
{
    public abstract class SyntaxNode : SyntaxInfo
    {
        public virtual string Type { get; set; }
        
        public SyntaxNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public SyntaxNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
            
        }

        public abstract string GenCode(ref StringBuilder sb);
        
        // public abstract char CheckType();
    }
}