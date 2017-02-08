using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

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
        protected override void ValidateNodeSemantic()
        {
            var typeId =VariableId.ValidateSemantic();
            if (VariableId is IdNode)
            {
                var id = (IdNode) VariableId;
                if (!SymbolTable.Instance.Contains(id.Label))
                {
                    throw new SemanticException(string.Format("Variable {0} have not been declared", id.Label));
                }
            }

            var sentenctype = ValueExpression.Expressions[0].ValidateSemantic();
            if (!typeId.IsAssignable(sentenctype))
            {
                throw new SemanticException("Variable ant expresion provided are not assignable");
            }
        }
    }
}
