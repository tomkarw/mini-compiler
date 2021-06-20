using System.Text;

namespace MiniCompiler
{
    public class BreakInstruction : SyntaxNode
    {
        private readonly int _value;
        
        public BreakInstruction(SyntaxInfo si, string value) : base(si)
        {
            _value = int.Parse(value);
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}