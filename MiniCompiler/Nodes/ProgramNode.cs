using System.Text;

namespace MiniCompiler
{
    public class ProgramNode : SyntaxNode
    {
        public SyntaxNode Declarations;
        public SyntaxNode Instructions;

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
            // constants
            // BUG: all strings after the one used get printed out in lli output...
            sb.AppendLine("@int_format = constant [2 x i8] c\"%d\"");
            sb.AppendLine("@double_format = constant [3 x i8] c\"%lf\"");
            sb.AppendLine("@hex_format = constant [4 x i8] c\"0X%X\"");
            sb.AppendLine("@true = constant [4 x i8] c\"True\"");
            sb.AppendLine("@false = constant [5 x i8] c\"False\"");
            // outside functions
            sb.AppendLine("declare i32 @printf(i8*, ...)");
            sb.AppendLine("declare i32 @scanf(i8*, ...)");
            // main function beginning
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