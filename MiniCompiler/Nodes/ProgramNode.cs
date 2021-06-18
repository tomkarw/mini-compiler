using System.Text;

namespace MiniCompiler
{
    public class ProgramNode : SyntaxNode
    {

        public SyntaxNode Declarations ;
        public SyntaxNode Instructions ;

        public ProgramNode() : base(-1, -1, null)
        {

        }

        public ProgramNode(
            SyntaxNode declarations,
            SyntaxNode instructions
        ) : base(-1, -1, null)
        {
            Declarations = declarations;
            Instructions = instructions;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // prolog
            sb.AppendLine("declare i32 @printf(i8*, ...)");
            sb.AppendLine("declare i32 @scanf(i8*, ...)");
            sb.AppendLine("define i32 @main ()");
            sb.AppendLine("{");

            Declarations.GenCode(ref sb);
            Instructions.GenCode(ref sb);

            // epilog
            sb.AppendLine("ret i32 0");
            sb.AppendLine("}");
            return null;
        }
    }
}