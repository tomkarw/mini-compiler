using System.Text;

namespace MiniCompiler
{
    public class WriteStringNode : SyntaxNode
    {
        public SyntaxInfo String;

        public WriteStringNode(SyntaxInfo s) : base(s)
        {
            String = s;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var cId = Context.GetNewId();
            var s = String.Text;
            s = s
                // trim 
                .Substring(1, s.Length - 2)
                .Replace(@"\", @"#\")
                // \n -> \0A
                .Replace(@"#\n", @"\0A")
                // \" -> \22
                .Replace("#\\\"", @"\22")
                // \\ -> \5C
                .Replace(@"#\#\", @"\5C")
                .Replace(@"#\", "");
            var cLength = s
                .Replace(@"\0A", "x")
                .Replace(@"\22", "x")
                .Replace(@"\5C", "x").Length + 1;
            sb.Insert(0, $"@{cId} = constant [{cLength} x i8] c\"{s}\\00\"\n");
            sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([{cLength} x i8]* @{cId} to i8*))");
            return null;
        }
    }
}