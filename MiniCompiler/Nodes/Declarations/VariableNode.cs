using System.Text;

namespace MiniCompiler
{
    public class VariableNode : SyntaxNode
    {
        public VariableNode(SyntaxInfo si) : base(si.Line, si.Column, si.Text)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var id = Context.AddVariable(Text, Type, Line, Column);
            sb.AppendLine($"%{id} = alloca {Type}");
            return null;
        }
    }
}