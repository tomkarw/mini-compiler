namespace MiniCompiler
{
    public class DoubleValueNode : ValueNode
    {
        public override string Type => "double";
        
        public DoubleValueNode(SyntaxInfo si) : base(si)
        {
        }
    }
}