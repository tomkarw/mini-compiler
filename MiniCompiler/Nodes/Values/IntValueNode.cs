using System.Text;

namespace MiniCompiler
{
    public class IntValueNode : ValueNode
    {
        public override string Type => "i32";
        
        public IntValueNode(SyntaxInfo si) : base(si)
        {
        }
    }
}