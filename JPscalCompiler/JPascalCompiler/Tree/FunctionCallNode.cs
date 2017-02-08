using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

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

        protected override void ValidateNodeSemantic()
        {
            FunctionName.ValidateSemantic();
            if (TypesTable.Instance.Contains(FunctionName.Label) == false)
            {
                throw  new SemanticException(string.Format("Function {0} does not exists",FunctionName.Label));
            }
            
            foreach (var parameter in FunctionParams)
            {
                parameter.ValidateSemantic();
                if (parameter is IdNode)
                {
                    var param = (IdNode)parameter;
                    if (TypesTable.Instance.Contains(param.Label) == false)
                    {
                        throw new SemanticException(string.Format("Variable {0} does not exists", param.Label));
                    }
                }
            }


        }
    }
}
