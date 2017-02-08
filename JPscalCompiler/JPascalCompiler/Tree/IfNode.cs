using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class IfNode:SentenceNode
    {
        public ExpressionNode IfConditionExpression { get; set; }
        public SentenceNode TrueBlockSentences { get; set; }
        public SentenceNode ElseBlockSentences { get; set; }
        public bool IsFirstSentece { get; set; }

        public IfNode(int column,int row, ExpressionNode ifConditionExpression, SentenceNode trueBlockSentences, SentenceNode elseBlockSentences)
        {
            IfConditionExpression = ifConditionExpression;
            TrueBlockSentences = trueBlockSentences;
            ElseBlockSentences = elseBlockSentences;
            RowSentence = row;
            ColumnSentence = column;
            IsFirstSentece = false;
        }

        public IfNode(int column, int row)
        {
            RowSentence = row;
            ColumnSentence = column;
            IsFirstSentece = false;
        }

        protected override void ValidateNodeSemantic()
        {
            IfConditionExpression.Expressions[0].ValidateSemantic();

            foreach (var sentence in TrueBlockSentences.Sentence)
            {
                sentence.ValidateSemantic();
            }

            foreach (var sentence in ElseBlockSentences.Sentence )
            {
                sentence.ValidateSemantic();
            }
        }
    }
}
