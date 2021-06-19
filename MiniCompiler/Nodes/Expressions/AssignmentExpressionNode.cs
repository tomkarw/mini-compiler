using System.Text;

namespace MiniCompiler
{
    public class AssignmentExpressionNode : SyntaxNode
    {
        public SyntaxNode Variable;
        public SyntaxNode Expression;

        public AssignmentExpressionNode(SyntaxNode variable, SyntaxNode expression) : base(variable.Line,
            variable.Column, variable.Text)
        {
            Variable = variable;
            Expression = expression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var expressionId = Expression.GenCode(ref sb);

            var variable = Context.GetVariable(Variable);

            // check if types match or implicit conversion is allowed
            if (variable.Type != Expression.Type)
            {
                if (variable.Type == "double" && Expression.Type == "i32")
                {
                    // implicit conversion
                    var oldExpressionId = expressionId;
                    expressionId = Context.GetNewId();
                    sb.AppendLine($"%{expressionId} = sitofp i32 %{oldExpressionId} to double");
                }
                else
                {
                    Context.AddError(
                        Expression.Line, Expression.Column,
                        $"Cannot implicitly cast from {Expression.Type} to {variable.Type}, try using casting unary operator"
                    );
                }
            }

            sb.AppendLine($"store {variable.Type} %{expressionId}, {variable.Type}* %{variable.Id}");

            Type = variable.Type;

            // return id of loaded value for chained assignments
            var idLoadedValue = Context.GetNewId();
            sb.AppendLine($"%{idLoadedValue} = load {Type}, {Type}* %{variable.Id}");

            return idLoadedValue;
        }
    }
}