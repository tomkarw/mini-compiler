using System.Text;

namespace MiniCompiler
{
       public abstract class InstructionsOrEmptyNode : SyntaxNode
    {
        public InstructionsOrEmptyNode(int line, int column, string text) : base(line, column, text)
        {
        }
    }

    public class EmptyInstructionsNode : InstructionsOrEmptyNode
    {
        public EmptyInstructionsNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            return null;
        }
    }

    public class InstructionsNode : InstructionsOrEmptyNode
    {
        public InstructionsOrEmptyNode Instructions { get; set; }
        public InstructionNode Instruction { get; set; }

        public InstructionsNode(
            InstructionsOrEmptyNode declarations,
            InstructionNode declaration
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

    public class InstructionNode : SyntaxNode
    {

        public InstructionNode(
        ) : base(-1, -1, null)
        {
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // // TODO: bug in productions, needs fixing!!!
            // if (IdsOrEmptyNode != null) {
            //     IdsOrEmptyNode.Type = BaseTypeNode.Type;
            //     IdsOrEmptyNode.GenCode(ref sb);
            // }
            //
            // IdNode.Type = BaseTypeNode.Type;
            // IdNode.GenCode(ref sb);

            return null;
        }
    }
}