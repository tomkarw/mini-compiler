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
            throw new System.NotImplementedException();
        }
    }

    public class DeclarationNode : SyntaxNode
    {
        public BaseTypeNode BaseTypeNode { get; set; }
        public IdNode IdNode { get; set; }

        public DeclarationNode(
            BaseTypeNode baseTypeNode,
            IdNode idNode
        ) : base(-1, -1, "")
        {
            BaseTypeNode = baseTypeNode;
            IdNode = IdNode;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IdNode : SyntaxNode
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