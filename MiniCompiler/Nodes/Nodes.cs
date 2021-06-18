﻿using System.IO;
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
        public virtual string Type { get; set; }
        
        public SyntaxNode(int line, int column, string text) : base(line, column, text)
        {
        }

        public abstract string GenCode(ref StringBuilder sb);
        
        // public abstract char CheckType();
    }

    public class ProgramNode : SyntaxNode
    {

        public DeclarationsNode Declarations { get; set; }
        public InstructionsNode Instructions { get; set; }

        public ProgramNode() : base(-1, -1, null)
        {

        }

        public ProgramNode(
            DeclarationsNode declarations,
            InstructionsNode instructions
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

            // TODO: same production error
            Declarations?.GenCode(ref sb);
            Instructions?.GenCode(ref sb);

            // epilog
            sb.AppendLine("ret i32 0");
            sb.AppendLine("}");
            return null;
        }
    }
}