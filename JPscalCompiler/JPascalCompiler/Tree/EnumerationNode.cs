using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class EnumerationNode:SentenceNode
    {
        public IdNode EnumerationId { get; set; }
        public List<ExpressionNode> EnumerationConstants { get; set; }
        public EnumerationNode(IdNode enumId, List<ExpressionNode> enumeratorConstantsExpressions)
        {
            EnumerationId = enumId;
            EnumerationConstants = enumeratorConstantsExpressions;
        }

        protected override void ValidateNodeSemantic()
        {
            SymbolTable.Instance.DeclareVariable(EnumerationId.Label, "enumeration");
            foreach (var constant in EnumerationConstants)
            {
                if (constant is IdNode)
                {
                    var id = (IdNode)constant;
                    SymbolTable.Instance.DeclareVariable(id.Label, "enumerationConstant");
                }
            }
        }
    }
}
