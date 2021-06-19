using System;
using System.Text;

namespace MiniCompiler
{
    public class LogicalExpressionNode : SyntaxNode
    {
        public SyntaxNode LogicalExpression;
        public SyntaxInfo LogicalOperator;
        public SyntaxNode RelationExpression;

        public LogicalExpressionNode(SyntaxNode logicalExpression, SyntaxInfo logicalOp, SyntaxNode relationExpression)
            : base(logicalExpression)
        {
            LogicalExpression = logicalExpression;
            LogicalOperator = logicalOp;
            RelationExpression = relationExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: implement as shortened operations
            var id = Context.GetNewId();
            var lhs = LogicalExpression.GenCode(ref sb);
            var rhs = RelationExpression.GenCode(ref sb);
            switch (LogicalOperator.Text)
            {
                case "||":
                {
                    
                    break;
                }
                case "&&":
                {
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return id;
        }
    }
}