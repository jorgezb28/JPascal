using System.Collections.Generic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class ExpressionNode
    {
        public List<ExpressionNode> Expressions = new List<ExpressionNode>();
        public virtual BaseType ValidateSemantic()
        {
            throw new System.NotImplementedException();
        }
    }
}