using System.Collections.Generic;

namespace JPascalCompiler.Tree
{
    public class SentenceNode
    {
        public List<SentenceNode> Sentence = new List<SentenceNode>();
        public int RowSentence { get; set; }
        public int ColumnSentence { get; set; }
    }
}