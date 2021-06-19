using System.Text;

namespace MiniCompiler
{
    public class WhileInstruction : SyntaxNode

    {
        private readonly SyntaxNode _condition;
        private readonly SyntaxNode _instruction;

        public WhileInstruction(SyntaxNode condition, SyntaxNode instruction) : base(condition)
        {
            _condition = condition;
            _instruction = instruction;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new System.NotImplementedException();
        }
    }
}