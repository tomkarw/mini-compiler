using System.Text;

namespace MiniCompiler
{
    public class DeclarationsNode : SyntaxNode
    {
        public SyntaxNode Declarations ;
        public SyntaxNode Declaration ;

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
}