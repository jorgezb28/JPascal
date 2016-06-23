using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class ForNode : SentenceNode
    {
        public IdNode IndexVarible { get; set; }
        public ExpressionNode InitialValue { get; set; }
        public ExpressionNode FinalValue { get; set; }

        public List<SentenceNode> ForBodySentenses { get; set; }

        public ForNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
            InitialValue = new ExpressionNode();
            FinalValue = new ExpressionNode();
            ForBodySentenses = new List<SentenceNode>();
        }
    }
}
