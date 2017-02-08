using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class BinaryOperationNode:ExpressionNode
    {
        public Type TypeNode { get; set; }

        public ExpressionNode LeftOperand { get; set; }
        public ExpressionNode RigthOperand { get; set; }

        public override BaseType ValidateSemantic()
        {
            var l =LeftOperand.Expressions[0].ValidateSemantic();
            var r =RigthOperand.Expressions[0].ValidateSemantic();

            if (!l.IsComparable(r))
            {
                throw new SemanticException("The operands are not comparables");
            }

            return l;
        }
    }
}
