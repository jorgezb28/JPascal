using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class WritelnNode : SentenceNode
    {
        public ExpressionNode WritelnExpr { get; set; }
        public ExpressionNode OptionalId { get; set; }
        public WritelnNode(int row, int column, ExpressionNode writelnExpr)
        {
            ColumnSentence = column;
            RowSentence = row;
            WritelnExpr = writelnExpr;
            OptionalId = new ExpressionNode();
        }

        public WritelnNode(int row, int column, ExpressionNode writelnExpr, ExpressionNode optionalId)
        {
            ColumnSentence = column;
            RowSentence = row;
            WritelnExpr = writelnExpr;
            OptionalId = optionalId;
        }

        
    }
}
