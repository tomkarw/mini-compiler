using System.Text;

namespace MiniCompiler
{
    public class BlockInstructionNode : SyntaxNode
    {
        public SyntaxNode Instructions;
        
        public BlockInstructionNode(SyntaxNode instructions) : base(instructions.Line, instructions.Column, instructions.Text)
        {
            Instructions = instructions;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            Instructions.GenCode(ref sb);
            return null;
        }
    }
}