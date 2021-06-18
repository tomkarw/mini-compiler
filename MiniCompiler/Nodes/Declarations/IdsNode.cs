using System.Text;

namespace MiniCompiler
{
    public class IdsNode : SyntaxNode
    {
        public SyntaxNode IdsOrEmptyNode { get; set; }
        public SyntaxNode IdNode { get; set; }

        public IdsNode(
            SyntaxNode idsOrEmptyNode,
            SyntaxNode idNode
        ) : base(idNode.Line, idNode.Column, idNode.Text)
        {
            IdsOrEmptyNode = idsOrEmptyNode;
            IdNode = idNode;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: bug in productions, needs fixing!!!
            if (IdsOrEmptyNode != null) 
            {
                IdsOrEmptyNode.Type = Type;
                IdsOrEmptyNode.GenCode(ref sb);
            }
            
            IdNode.Type = Type;
            IdNode.GenCode(ref sb);
            return null;
        }
    }
}