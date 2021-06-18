using System.Text;

namespace MiniCompiler
{
    public class AssignmentExpressionNode : SyntaxNode
    {
        public SyntaxNode Variable ;
        public SyntaxNode Expression ;
        
        public AssignmentExpressionNode(SyntaxNode variable, SyntaxNode expression) : base(variable.Line, variable.Column, variable.Text)
        {
            Variable = variable;
            Expression = expression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var expressionId = Expression.GenCode(ref sb);
            
            var variable = Context.GetVariable(Variable);
            
            // TODO: check if types match or implicit conversion allowed

            var type = "i32";
            
            // generate llvm code
            sb.AppendLine($"store {type} %{expressionId}, {type}* %{variable.Id}");

            Type = type;
            
            // TODO: should I store variable value and return id to that?
            var idLoadedValue = Context.GetNewId();
            sb.AppendLine($"%{idLoadedValue} = load {Type}, {Type}* %{variable.Id}");

            return idLoadedValue;
        }
    }
}