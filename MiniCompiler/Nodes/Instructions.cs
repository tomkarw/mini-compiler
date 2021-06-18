using System.Text;

namespace MiniCompiler
{
    public class InstructionsNode : SyntaxNode
    {
        public SyntaxNode Instructions { get; set; }
        public SyntaxNode Instruction { get; set; }

        public InstructionsNode(
            SyntaxNode declarations,
            SyntaxNode declaration
        ) : base(declarations.Line, declarations.Column, declarations.Text)
        {
            Instructions = declarations;
            Instruction = declaration;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            Instructions.GenCode(ref sb);
            Instruction.GenCode(ref sb);
            return null;
        }
    }

    public abstract class InstructionNode : SyntaxNode
    {
        public InstructionNode(
        ) : base(-1, -1, null)
        {
        }
    }

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