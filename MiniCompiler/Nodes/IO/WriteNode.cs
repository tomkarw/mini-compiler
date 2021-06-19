using System;
using System.Text;

namespace MiniCompiler
{
    public class WriteNode : SyntaxNode
    {
        public SyntaxNode Expression;
        
        public WriteNode(SyntaxNode expression) : base(expression)
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
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([2 x i8]* @int_format to i8*), i32 %{id})");
                    break;
                }
                case "double":
                {
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([3 x i8]* @double_format to i8*), double %{id})");
                    break;
                }
                case "i1":
                {
                    // TODO: Print 'True' or 'False'. I think llvm if statement is needed?
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([2 x i8]* @int_format to i8*), i1 %{id})");
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