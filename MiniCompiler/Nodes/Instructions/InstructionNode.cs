namespace MiniCompiler
{
    public abstract class InstructionNode : SyntaxNode
    {
        public InstructionNode(
        ) : base(-1, -1, null)
        {
        }
    }
}