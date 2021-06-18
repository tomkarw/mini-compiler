using System.Text;

namespace MiniCompiler
{
    public abstract class BaseDeclarationsNode : SyntaxNode
    {
        public BaseDeclarationsNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }

    public class EmptyDeclarationsNode : BaseDeclarationsNode
    {
        public EmptyDeclarationsNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }

    public class DeclarationsNode : BaseDeclarationsNode
    {
        public BaseDeclarationsNode Declarations { get; set; }
        public DeclarationNode Declaration { get; set; }

        public DeclarationsNode(
            BaseDeclarationsNode declarations,
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
        public BaseIdsNode BaseIdsNode { get; set; }
        public IdNode IdNode { get; set; }

        public DeclarationNode(
            BaseTypeNode baseTypeNode,
            BaseIdsNode baseIdsNode,
            IdNode idNode
        ) : base(-1, -1, "")
        {
            BaseTypeNode = baseTypeNode;
            BaseIdsNode = baseIdsNode;
            IdNode = idNode;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: add variable to context
            
            // declare variable in llvm
            var id = Context.GetNewId();
            var type = BaseTypeNode.Type;
            sb.Append($"%{id} = alloca {type}\n");
            return null;
        }
    }

    public abstract class BaseIdsNode : SyntaxNode
    {
        public BaseIdsNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }

    public class EmptyIdsNode : BaseDeclarationsNode
    {
        public EmptyIdsNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }

    public class IdsNode : BaseIdsNode
    {
        public BaseIdsNode BaseIdsNode { get; set; }
        public IdNode IdNode { get; set; }

        public IdsNode(
            BaseIdsNode baseIdsNode,
            IdNode idNode
        ) : base(idNode.Line, idNode.Column, idNode.Text)
        {
            BaseIdsNode = baseIdsNode;
            IdNode = idNode;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IdNode : BaseIdsNode
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