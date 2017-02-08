using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class ParameterExpressionByReference:ExpressionNode
    {
        public List<ExpressionNode> ParametersIds { get; set; }
        public ExpressionNode ParameterType { get; set; }
        public ParameterExpressionByReference(List<ExpressionNode> parametersIds, ExpressionNode parameterType)
        {
            ParametersIds = parametersIds;
            ParameterType = parameterType;
        }

        public override BaseType ValidateSemantic()
        {
            var typeExpr =ParameterType.ValidateSemantic();

            foreach (var paramsIds in ParametersIds)
            {
                if (paramsIds is IdNode)
                {
                    var idNode = (IdNode) paramsIds;
                    var paramType = TypesTable.Instance.GetStringType(typeExpr);
                    SymbolTable.Instance.DeclareVariable(idNode.Label,paramType);
                }
                paramsIds.ValidateSemantic();
            }
            return typeExpr;
        }
    }

    
}
