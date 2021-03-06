using System.Text;

namespace MiniCompiler
{
    public class InstructionsNode : SyntaxNode
    {
        public SyntaxNode Instructions;
        public SyntaxNode Instruction;

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
}