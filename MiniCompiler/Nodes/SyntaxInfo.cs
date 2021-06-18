using System.IO;

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
}