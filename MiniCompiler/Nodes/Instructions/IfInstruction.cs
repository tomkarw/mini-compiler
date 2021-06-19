using System.Text;

namespace MiniCompiler
{
    public class IfInstruction : SyntaxNode

    {
        private readonly SyntaxNode _condition;
        private readonly SyntaxNode _ifInstruction;
        private readonly SyntaxNode _elseInstruction;

        public IfInstruction(SyntaxNode condition, SyntaxNode ifInstruction, SyntaxNode elseInstruction = null) : base(condition)
        {
            _condition = condition;
            _ifInstruction = ifInstruction;
            _elseInstruction = elseInstruction;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}