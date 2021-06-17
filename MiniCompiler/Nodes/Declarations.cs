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
        public DeclarationNode(
        ) : base(-1, -1, "")
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}