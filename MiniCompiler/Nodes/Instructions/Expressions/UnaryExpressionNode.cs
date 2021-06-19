using System;
using System.Collections.Generic;
using System.Text;

namespace MiniCompiler
{
    public class UnaryExpressionNode : SyntaxNode
    {
        private static Dictionary<string, string> _operationMappings = new Dictionary<string, string>
        {
            {"+", "add"},
            {"-", "sub"}
        };

        public SyntaxInfo Op;
        public SyntaxNode Expression;

        public UnaryExpressionNode(SyntaxInfo op,
            SyntaxNode expression)
            : base(expression)
        {
            Op = op;
            Expression = expression;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}