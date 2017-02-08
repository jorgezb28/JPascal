using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class FunctionDeclarationNode:SentenceNode
    {
        public FunctionDeclarationNode(ExpressionNode functionName, List<ExpressionNode> functionParameters, 
            ExpressionNode returnValue, List<SentenceNode> functionBlockSentences)
        {
            FunctionName = functionName;
            FunctionParameters= functionParameters;
            ReturnValue = returnValue;
            FunctionSentences = functionBlockSentences;
        }

        public ExpressionNode FunctionName { get; set; }
        public List<ExpressionNode> FunctionParameters { get; set; }
        public ExpressionNode ReturnValue { get; set; }
        public List<SentenceNode> FunctionSentences { get; set; }

        protected override void ValidateNodeSemantic()
        {
            FunctionName.ValidateSemantic();

            if (FunctionName is IdNode)
            {
                var functionName = (IdNode)FunctionName;
                TypesTable.Instance.RegisterType(functionName.Label, new FunctionType());
            }

            foreach (var parameter in FunctionParameters)
            {
                parameter.ValidateSemantic();
            }

            ReturnValue.ValidateSemantic();

            foreach (var sentenceNode in FunctionSentences)
            {
                sentenceNode.ValidateSemantic();
            }
        }
    }
}
