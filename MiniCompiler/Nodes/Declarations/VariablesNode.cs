using System.Text;

namespace MiniCompiler
{
    public class VariablesNode : SyntaxNode
    {
        public SyntaxNode IdsOrEmptyNode ;
        public SyntaxNode VariableNode ;

        public VariablesNode(
            SyntaxNode idsOrEmptyNode,
            SyntaxNode idNode
        ) : base(idNode.Line, idNode.Column, idNode.Text)
        {
            IdsOrEmptyNode = idsOrEmptyNode;
            VariableNode = idNode;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: bug in productions, needs fixing!!!
            if (IdsOrEmptyNode != null) 
            {
                IdsOrEmptyNode.Type = Type;
                IdsOrEmptyNode.GenCode(ref sb);
            }
            
            VariableNode.Type = Type;
            VariableNode.GenCode(ref sb);
            return null;
        }
    }
}