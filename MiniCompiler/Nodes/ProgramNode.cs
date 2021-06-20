using System.Text;

namespace MiniCompiler
{
    public class ProgramNode : SyntaxNode
    {
        private readonly SyntaxNode _blockInstruction;

        public ProgramNode() : base(-1, -1, null)
        {
        }

        public ProgramNode(SyntaxNode blockInstruction) : base(-1, -1, null)
        {
            _blockInstruction = blockInstruction;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // prolog
            // constants
            sb.AppendLine("@int_format = constant [3 x i8] c\"%d\\00\"");
            sb.AppendLine("@double_format = constant [4 x i8] c\"%lf\\00\"");
            sb.AppendLine("@hex_format = constant [5 x i8] c\"0X%X\\00\"");
            sb.AppendLine("@hex_read_format = constant [3 x i8] c\"%X\\00\"");
            sb.AppendLine("@true = constant [5 x i8] c\"True\\00\"");
            sb.AppendLine("@false = constant [6 x i8] c\"False\\00\"");
            // outside functions
            sb.AppendLine("declare i32 @printf(i8*, ...)");
            sb.AppendLine("declare i32 @scanf(i8*, ...)");
            // main function beginning
            sb.AppendLine("define i32 @main ()");
            sb.AppendLine("{");

            _blockInstruction.GenCode(ref sb);

            // epilog
            sb.AppendLine("ret i32 0");
            sb.AppendLine("}");
            return null;
        }
    }
}