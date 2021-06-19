using System;

namespace MiniCompiler
{
    public class HexValueNode : ValueNode
    {
        public override string Type => "i32";
        
        public HexValueNode(SyntaxInfo si) : base(si)
        {
            // translate hex to string
            Text = Convert.ToInt32(Text, 16).ToString();
        }
    }
}