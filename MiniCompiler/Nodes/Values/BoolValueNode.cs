namespace MiniCompiler
{
    public class BoolValueNode : ValueNode
    {
        public override string Type => "i1";
        
        public BoolValueNode(SyntaxInfo si) : base(si)
        {
        }
    }
}