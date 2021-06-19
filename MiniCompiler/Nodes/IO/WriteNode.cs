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
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([3 x i8]* @int_format to i8*), i32 %{id})");
                    break;
                }
                case "double":
                {
                    sb.AppendLine($"call i32 (i8*, ...) @printf(i8* bitcast ([4 x i8]* @double_format to i8*), double %{id})");
                    break;
                }
                case "i1":
                {
                    var trueLab = Context.GetNewId();
                    var falseLab = Context.GetNewId();
                    var endLab = Context.GetNewId();
                    sb.AppendLine($"br i1 %{id}, label %{trueLab}, label %{falseLab}");
                    sb.AppendLine($"{trueLab}:");
                    sb.AppendLine("call i32 (i8*, ...) @printf(i8* bitcast ([5 x i8]* @true to i8*))");
                    sb.AppendLine($"br label %{endLab}");
                    sb.AppendLine($"{falseLab}:");
                    sb.AppendLine("call i32 (i8*, ...) @printf(i8* bitcast ([6 x i8]* @false to i8*))");
                    sb.AppendLine($"br label %{endLab}");
                    sb.AppendLine($"{endLab}:");
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