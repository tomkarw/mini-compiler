using System;
using System.Text;

namespace MiniCompiler
{
    public class ReadNode : SyntaxNode
    {
        private readonly SyntaxInfo _id;
        
        public ReadNode(SyntaxInfo id) : base(id)
        {
            _id = id;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var variable = Context.GetVariable(_id);

            throw new NotImplementedException();
        }
    }
}