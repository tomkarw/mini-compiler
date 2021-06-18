using System.Text;

namespace MiniCompiler
{
    public class IdExpressionNode : SyntaxNode
    {
        public SyntaxInfo Variable;
        
        public IdExpressionNode(SyntaxInfo variable) : base(variable)
        {
            Variable = variable;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var variable = Context.GetVariable(Variable);
            
            Type = variable.Type;
            
            var idLoadedValue = Context.GetNewId();
            sb.AppendLine($"%{idLoadedValue} = load {Type}, {Type}* %{variable.Id}");

            return idLoadedValue;
        }
    }
}