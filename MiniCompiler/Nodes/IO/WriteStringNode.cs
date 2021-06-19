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
            s = s.Substring(1, s.Length - 2);
            s = s.Replace("\\n", "\\0A"); // TODO: more replaces?
            var cLength = s.Replace("\\0A", "x").Length + 1;
            sb.Insert(0, $"@{cId} = constant [{cLength} x i8] c\"{s}\\00\"\n");
            sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([{cLength} x i8]* @{cId} to i8*))");
            return null;
        }
    }
}