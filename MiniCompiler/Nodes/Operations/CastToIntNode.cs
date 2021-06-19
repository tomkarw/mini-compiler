using System;
using System.Text;

namespace MiniCompiler
{
    public class CastToIntNode : SyntaxNode
    {
        public CastToIntNode(SyntaxInfo si) : base(si)
        {
            Text = "(int)";
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new Exception();
        }
    }
}