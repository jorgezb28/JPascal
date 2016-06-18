using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class BinaryOperationNode:ExpressionNode
    {
        public Type TypeNode { get; set; }

        public ExpressionNode LeftOperand { get; set; }
        public ExpressionNode RigthOperand { get; set; }


    }
}
