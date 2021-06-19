using System;
using System.Text;

namespace MiniCompiler
{
    public class ReadHexNode : SyntaxNode
    {
        public SyntaxInfo Id;

        public ReadHexNode(SyntaxInfo id) : base(id)
        {
            Id = id;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}