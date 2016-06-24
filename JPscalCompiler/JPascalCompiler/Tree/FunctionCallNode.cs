using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class FunctionCallNode : SentenceNode
    {
        public IdNode FunctionName { get; set; }
        public List<ExpressionNode> FunctionParams { get; set; }
        public FunctionCallNode(IdNode functionName, List<ExpressionNode> parameters)
        {
            FunctionName = functionName;
            FunctionParams = parameters;
        }
    }
}
