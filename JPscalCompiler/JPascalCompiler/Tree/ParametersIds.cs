using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class ParametersIds:SentenceNode
    {
        public List<ExpressionNode> ParametersIdsExpressions { get; set; }

        public ParametersIds()
        {
            ParametersIdsExpressions = new List<ExpressionNode>();
        }

        protected override void ValidateNodeSemantic()
        {
            foreach (var paramsIds in ParametersIdsExpressions)
            {
                paramsIds.ValidateSemantic();

                if (paramsIds is IdNode)
                {
                    var idNode = (IdNode)paramsIds;
                    var paramType = TypesTable.Instance.GetType(paramsIds);

                    SymbolTable.Instance.DeclareVariable(idNode.Label, paramType.ToString());
                }
            }}
    }
}
