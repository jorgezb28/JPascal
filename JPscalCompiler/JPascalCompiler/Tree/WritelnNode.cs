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
        public WritelnNode(int row, int column, ExpressionNode writelnExpr)
        {
            ColumnSentence = column;
            RowSentence = row;
            WritelnExpr = writelnExpr;
        }

        
    }
}
