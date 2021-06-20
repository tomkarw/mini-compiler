using System;
using System.Text;

namespace MiniCompiler
{
    public class TabDimensionsNode : SyntaxNode
    {
        public TabDimensionsNode OtherDimensions;
        public SyntaxInfo Dimension;

        public TabDimensionsNode(
            TabDimensionsNode otherDimensions,
            SyntaxInfo dimension
        ) : base(dimension)
        {
            OtherDimensions = otherDimensions;
            Dimension = dimension;
        }

        public override string GenCode(ref StringBuilder sb)
        {
            throw new Exception();
        }
    }
}