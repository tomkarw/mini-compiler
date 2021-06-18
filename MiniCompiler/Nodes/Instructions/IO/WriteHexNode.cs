using System;
using System.Text;

namespace MiniCompiler
{
    public class WriteHexNode : SyntaxNode
    {
        public SyntaxNode Expression;
        
        public WriteHexNode(SyntaxNode expression) : base(expression)
        {
            Expression = expression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Expression.GenCode(ref sb);
            
            switch (Expression.Type)
            {
                case "i32":
                {
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([4 x i8]* @hex_format to i8*), i32 %{id})");
                    break;
                }
                case "double":
                case "i1":
                {
                    Context.Errors.Add($"[{Expression.Line}, {Expression.Column}] ERROR: Cannot write value of type {Expression.Type} as hex, try removing ',hex'.");
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return null;
        }
    }
}