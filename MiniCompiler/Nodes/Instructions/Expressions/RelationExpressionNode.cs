using System;
using System.Text;

namespace MiniCompiler
{
    public class RelationExpressionNode : SyntaxNode
    {
        public SyntaxNode RelationExpression;
        public SyntaxInfo RelationOperator;
        public SyntaxNode AdditiveExpression;

        public RelationExpressionNode(SyntaxNode relationExpression, SyntaxInfo relationOp, SyntaxNode additiveExpression)
            : base(relationExpression)
        {
            RelationExpression = relationExpression;
            RelationOperator = relationOp;
            AdditiveExpression = additiveExpression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // TODO: implement as shortened operations
            var id = Context.GetNewId();
            var lhs = RelationExpression.GenCode(ref sb);
            var rhs = AdditiveExpression.GenCode(ref sb);
            switch (RelationOperator.Text)
            {
                case "==":
                {
                    
                    break;
                }
                case "!=":
                {
                    
                    break;
                }
                case ">":
                {
                    
                    break;
                }
                case ">=":
                {
                    
                    break;
                }
                case "<":
                {
                    
                    break;
                }
                case "<=":
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