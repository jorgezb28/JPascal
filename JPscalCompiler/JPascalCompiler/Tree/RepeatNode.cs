using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class RepeatNode:SentenceNode
    {
        public RepeatNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
            IsFirstSentence = false;
        }

        public ExpressionNode UntilCondition { get; set; }
        public bool IsFirstSentence { get; set; }
        protected override void ValidateNodeSemantic()
        {
            UntilCondition.ValidateSemantic();

            foreach (var sentenceNode in Sentence)
            {
                sentenceNode.ValidateSemantic();
            }
        }
    }
}
