using System.Text;

namespace MiniCompiler
{
    public class DeclarationNode : SyntaxNode
    {
        public SyntaxNode BaseTypeNode { get; set; }
        public SyntaxNode IdsOrEmptyNode { get; set; }
        public SyntaxNode IdNode { get; set; }

        public DeclarationNode(
            SyntaxNode baseTypeNode,
            SyntaxNode idsOrEmptyNode,
            SyntaxNode idNode
        ) : base(-1, -1, null)
        {
            BaseTypeNode = baseTypeNode;
            IdsOrEmptyNode = idsOrEmptyNode;
            IdNode = idNode;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            IdsOrEmptyNode.Type = BaseTypeNode.Type;
            IdsOrEmptyNode.GenCode(ref sb);
            
            IdNode.Type = BaseTypeNode.Type;
            IdNode.GenCode(ref sb);

            return null;
        }
    }
}