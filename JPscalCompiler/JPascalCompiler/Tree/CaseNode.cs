using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class CaseNode : SentenceNode
    {
        public CaseNode(int row, int column)
        {
            RowSentence = row;
            ColumnSentence = column;
            IsFirstSentence = false;
            CaseExpression = new ExpressionNode();
            CaseLiterals = new List<ExpressionNode>();
            ElseLiteralSentences = new List<SentenceNode>();
        }

        public List<ExpressionNode> CaseLiterals;
        public List<SentenceNode> ElseLiteralSentences { get; set; }
        public ExpressionNode CaseExpression { get; set; }
        public bool IsFirstSentence { get; set; }
    }
}
