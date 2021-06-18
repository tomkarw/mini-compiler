using System.IO;

namespace MiniCompiler
{
    public class SyntaxInfo
    {
        public int Line ;
        public int Column ;
        public string Text ;

        public SyntaxInfo(int line, int column, string text)
        {
            Line = line;
            Column = column;
            Text = text;
        }
    }
}