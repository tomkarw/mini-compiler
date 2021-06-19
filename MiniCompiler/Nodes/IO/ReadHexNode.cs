using System;
using System.Text;

namespace MiniCompiler
{
    public class ReadHexNode : SyntaxNode
    {
        private readonly SyntaxInfo _id;

        public ReadHexNode(SyntaxInfo id) : base(id)
        {
            _id = id;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var variable = Context.GetVariable(_id);
            if (variable.Type == "i32")
            {
                sb.AppendLine(
                    $"call i32 (i8*, ...) @scanf(i8* bitcast ([3 x i8]* @hex_read_format to i8*), i32* %{variable.Id})");
            }
            else
            {
                Context.AddError(_id.Line, _id.Line, $"Cannot read into type '{variable.Type}'");
            }

            return null;
        }
    }
}