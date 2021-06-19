using System.Text;

namespace MiniCompiler
{
    public class WriteStringNode : SyntaxNode
    {
        public SyntaxInfo String;

        public WriteStringNode(SyntaxInfo s) : base(s)
        {
            String = s;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: how to print strings?
            sb.AppendLine("; write string not yet implemented");
            return null;
        }
    }
}