using System.Text;

namespace MiniCompiler
{
    public class IntValueNode : SyntaxNode
    {
        public override string Type => "i32";
        
        public IntValueNode(SyntaxInfo si) : base(si)
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