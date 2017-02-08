using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class FunctionCallExpression:ExpressionNode
    {
        public IdNode FunctionIdNode { get; set; }
        public List<ExpressionNode> FunctionParams { get; set; }
        public FunctionCallExpression(IdNode functionIdNode, List<ExpressionNode> functionParams)
        {
            FunctionIdNode = functionIdNode;
            FunctionParams = new List<ExpressionNode>(functionParams);
        }

        public override BaseType ValidateSemantic()
        {
            var idfun =FunctionIdNode.ValidateSemantic();

            foreach (var functionParam in FunctionParams)
            {
                functionParam.ValidateSemantic();
            }

            return idfun;
        }
    }
}
