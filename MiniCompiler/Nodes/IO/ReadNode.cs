using System;
using System.Text;

namespace MiniCompiler
{
    public class ReadNode : SyntaxNode
    {
        private readonly SyntaxInfo _id;

        public ReadNode(SyntaxInfo id) : base(id)
        {
            _id = id;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var variable = Context.GetVariable(_id);
            switch (variable.Type)
            {
                case "i32":
                {
                    sb.AppendLine($"call i32 (i8*, ...) @scanf(i8* bitcast ([3 x i8]* @int_format to i8*), i32* %{variable.Id})");
                    break;
                }
                case "double":
                { 
                    sb.AppendLine( $"call i32 (i8*, ...) @scanf(i8* bitcast ([4 x i8]* @double_format to i8*), double* %{variable.Id})");
                    break;
                }
                default:
                {
                    Context.AddError(_id.Line, _id.Line, $"Cannot read into type '{variable.Type}'");
                    break;
                }
            }

            return null;
        }
    }
}