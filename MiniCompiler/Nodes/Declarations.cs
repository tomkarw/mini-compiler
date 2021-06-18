using System.Text;

namespace MiniCompiler
{
    public abstract class DeclarationsOrEmptyNode : SyntaxNode
    {
        public DeclarationsOrEmptyNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }

    public class EmptyDeclarationsNode : DeclarationsOrEmptyNode
    {
        public EmptyDeclarationsNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }

    public class DeclarationsNode : DeclarationsOrEmptyNode
    {
        public DeclarationsOrEmptyNode Declarations { get; set; }
        public DeclarationNode Declaration { get; set; }

        public DeclarationsNode(
            DeclarationsOrEmptyNode declarations,
            DeclarationNode declaration
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
        public BaseTypeNode BaseTypeNode { get; set; }
        public IdsOrEmptyNode IdsOrEmptyNode { get; set; }
        public IdNode IdNode { get; set; }

        public DeclarationNode(
            BaseTypeNode baseTypeNode,
            IdsOrEmptyNode idsOrEmptyNode,
            IdNode idNode
        ) : base(-1, -1, "")
        {
            BaseTypeNode = baseTypeNode;
            IdsOrEmptyNode = idsOrEmptyNode;
            IdNode = idNode;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: add variable to context
            
            // declare variable in llvm
            var id = Context.GetNewId();
            var type = BaseTypeNode.Type;
            sb.AppendLine($"%{id} = alloca {type}");
            return null;
        }
    }

    public abstract class IdsOrEmptyNode : SyntaxNode
    {
        public IdsOrEmptyNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }

    public class EmptyIdsNode : DeclarationsOrEmptyNode
    {
        public EmptyIdsNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }

    public class IdsNode : IdsOrEmptyNode
    {
        public IdsOrEmptyNode IdsOrEmptyNode { get; set; }
        public IdNode IdNode { get; set; }

        public IdsNode(
            IdsOrEmptyNode idsOrEmptyNode,
            IdNode idNode
        ) : base(idNode.Line, idNode.Column, idNode.Text)
        {
            IdsOrEmptyNode = idsOrEmptyNode;
            IdNode = idNode;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IdNode : IdsOrEmptyNode
    {
        public IdNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}