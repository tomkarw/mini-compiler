using System.Text;

namespace MiniCompiler
{
    public class DeclarationsNode : SyntaxNode
    {
        public SyntaxNode Declarations { get; set; }
        public SyntaxNode Declaration { get; set; }

        public DeclarationsNode(
            SyntaxNode declarations,
            SyntaxNode declaration
        ) : base(declarations.Line, declarations.Column, declarations.Text)
        {
            Declarations = declarations;
            Declaration = declaration;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            Declarations.GenCode(ref sb);
            Declaration.GenCode(ref sb);
            return null;
        }
    }

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

    public class IdNode : SyntaxNode
    {
        public IdNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.AddVariable(Text, Type, Line, Column);
            sb.AppendLine($"%{id} = alloca {Type}");
            return null;
        }
    }
}