using System.Text;

namespace MiniCompiler
{
    public class BlockInstruction : SyntaxNode
    {
        public SyntaxNode Declarations;
        public SyntaxNode Instructions;

        public BlockInstruction() : base(-1, -1, null)
        {
        }

        public BlockInstruction(
            SyntaxNode declarations,
            SyntaxNode instructions
        ) : base(-1, -1, null)
        {
            Declarations = declarations;
            Instructions = instructions;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            Declarations.GenCode(ref sb);
            Instructions.GenCode(ref sb);

            return null;
        }
    }
}