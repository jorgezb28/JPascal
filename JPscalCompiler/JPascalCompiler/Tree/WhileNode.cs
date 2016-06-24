using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class WhileNode : SentenceNode
    {
        public ExpressionNode ConditionExpression { get; set; }
        public bool IsFirstSentece { get; set; }

        public WhileNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
            IsFirstSentece = false;
        }
    }
}
