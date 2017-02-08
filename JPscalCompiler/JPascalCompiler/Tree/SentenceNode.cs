using System.Collections.Generic;

namespace JPascalCompiler.Tree
{
    public class SentenceNode
    {
        public List<SentenceNode> Sentence = new List<SentenceNode>();
        public int RowSentence { get; set; }
        public int ColumnSentence { get; set; }
        protected virtual void ValidateNodeSemantic()
        {
            throw new System.NotImplementedException();
        }

        public void ValidateSemantic()
        {
            ValidateNodeSemantic();
        }
    }
}