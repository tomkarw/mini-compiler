using System.IO;
using System.Text;

namespace MiniCompiler
{
    public class SyntaxInfo
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public string Text { get; set; }

        public SyntaxInfo(int line, int column, string text)
        {
            Line = line;
            Column = column;
            Text = text;
        }
    }
    public abstract class SyntaxNode : SyntaxInfo
    {
        public int Type { get; set; }
        
        public SyntaxNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public abstract string GenCode(ref StringBuilder sb);
        
        // public abstract char CheckType();
    }

    public class ProgramNode : SyntaxNode
    {

        public DeclarationsNode Declarations { get; set; }
        // public InstructionsNode Instructions { get; set; }

        public ProgramNode() : base(-1, -1, "")
        {

        }

        public ProgramNode(
            DeclarationsNode declarations
            // InstructionsNode instructions
        ) : base(declarations.Line, declarations.Column, declarations.Text)
        {
            Declarations = declarations;
            // Instructions = instructions;
        }


        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: prolog
            Declarations.GenCode(ref sb);
            // Instructions.GenCode(ref sb);
            return null;

            // TODO: epilog
        }
    }

    public class InstructionsNode : SyntaxNode
    {
        public InstructionsNode(
            // InstructionsNode instructions,
            // InstructionNode instruction
        ) : base(-1, -1, "")
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}