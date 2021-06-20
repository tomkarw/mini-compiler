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
            // push new context
            Context.PushVariableStack();
        
            Declarations.GenCode(ref sb);
            Instructions.GenCode(ref sb);

            // pop context
            Context.PopVariableStack();
            
            return null;
        }
    }
}