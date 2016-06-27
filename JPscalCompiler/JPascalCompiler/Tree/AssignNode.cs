using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class AssignNode :SentenceNode
    {
        public AssignNode(ExpressionNode idVal, ExpressionNode assignExpr)
        {
            VariableId = idVal;
            ValueExpression = assignExpr;
        }

        public ExpressionNode VariableId { get; set; }
        public ExpressionNode ValueExpression { get; set; }
    }
}
