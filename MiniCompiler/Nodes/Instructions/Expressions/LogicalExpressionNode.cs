using System.Text;

namespace MiniCompiler
{
    public class LogicalExpressionNode : SyntaxNode
    {
        public SyntaxNode Value;
        
        public LogicalExpressionNode(SyntaxNode value) : base(value)
        {
            Value = value;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}