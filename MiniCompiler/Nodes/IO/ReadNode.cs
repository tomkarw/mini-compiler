using System;
using System.Text;

namespace MiniCompiler
{
    public class ReadNode : SyntaxNode
    {
        public SyntaxInfo Id;
        
        public ReadNode(SyntaxInfo id) : base(id)
        {
            Id = id;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}