using System.Text;

namespace MiniCompiler
{
    public class ValueNode : SyntaxNode
    {
        public ValueNode(SyntaxInfo si) : base(si)
        {
        }

        public override string GenCode(ref StringBuilder sb)
        {
            var idStoredValue = Context.GetNewId();
            sb.AppendLine($"%{idStoredValue} = alloca {Type}");
            sb.AppendLine($"store {Type} {Text}, {Type}* %{idStoredValue}");
            var idLoadedValue = Context.GetNewId();
            sb.AppendLine($"%{idLoadedValue} = load {Type}, {Type}* %{idStoredValue}");

            return idLoadedValue;
        }
    }
}