using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniCompiler
{
    public class TabVariableNode : SyntaxNode
    {
        private SyntaxInfo _id;
        private TabDimensionsNode _otherDimensions;
        private SyntaxInfo _dimension;

        public TabVariableNode(SyntaxInfo id, TabDimensionsNode otherDimensions, SyntaxInfo dimension) : base(id)
        {
            _id = id;
            _otherDimensions = otherDimensions;
            _dimension = dimension;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            // get list of dimensions
            var dims = new List<int> {int.Parse(_dimension.Text)};
            var nextDimension = _otherDimensions;
            while (nextDimension != null)
            {
                dims.Add(int.Parse(nextDimension.Dimension.Text));
                nextDimension = nextDimension?.OtherDimensions;
            }

            var id = Context.AddVariable(Text, Type, Line, Column, dims);

            var size = dims.Aggregate(1, (acc, dim) => acc * dim);
            sb.AppendLine($"%{id} = alloca {Type}, i32 {size}");

            return null;
        }
    }
}