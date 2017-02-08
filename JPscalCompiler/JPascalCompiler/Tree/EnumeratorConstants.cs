using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class EnumeratorConstants:SentenceNode
    {
        public List<ExpressionNode> EnumeratorConstantsExpressions { get; set; }

        public EnumeratorConstants()
        {
            EnumeratorConstantsExpressions = new List<ExpressionNode>();
        }

        protected override void ValidateNodeSemantic()
        {
            foreach (var constantsExpression in EnumeratorConstantsExpressions)
            {
                constantsExpression.ValidateSemantic();
                if (constantsExpression is IdNode)
                {
                    var id = (IdNode) constantsExpression;
                    if (SymbolTable.Instance.Contains(id.Label))
                    {
                        throw new SemanticException(string.Format("Variable {0} have been declared.Please change the name", id.Label));
                    }
                }
            }
        }
    }
}
