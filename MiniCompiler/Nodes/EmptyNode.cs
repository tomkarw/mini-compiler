using System.Text;

namespace MiniCompiler
{
    public class EmptyNode : SyntaxNode
    {
        public EmptyNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }
}