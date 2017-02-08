using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using NUnit.Framework.Constraints;

namespace JPascalCompiler.Tree
{
    public class ConstantNode: SentenceNode
    {
        public ConstantNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
            ConstantType = String.Empty;
        }

        public IdNode ConstantId { get; set; }
        public ExpressionNode ConstantValue { get; set; }
        public string ConstantType { get; set; }
        protected override void ValidateNodeSemantic()
        {
            if (!string.IsNullOrWhiteSpace(ConstantType))
            {
                SymbolTable.Instance.DeclareVariable(ConstantId.Label, ConstantType);
                var consType = TypesTable.Instance.GetType(ConstantType);
                var exprType = ConstantValue.Expressions[0].ValidateSemantic();
                if (!consType.IsAssignable(exprType))
                {
                    throw new SemanticException("Constant Id is not assignable with the expression provided");
                }
            }
            var type = ConstantValue.Expressions[0].ValidateSemantic();
            var typestr = TypesTable.Instance.GetStringType(type);
            SymbolTable.Instance.DeclareVariable(ConstantId.Label, typestr);
            
        }
    }
}
