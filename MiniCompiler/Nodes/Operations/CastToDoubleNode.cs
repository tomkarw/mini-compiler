using System;
using System.Text;

namespace MiniCompiler
{
    public class CastToDoubleNode : SyntaxNode
    {
        public CastToDoubleNode(SyntaxInfo si) : base(si)
        {
            Text = "(double)";
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new Exception();
        }
    }
}