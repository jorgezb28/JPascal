using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class ContinueNode: SentenceNode
    {
        public ContinueNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
        }

        protected override void ValidateNodeSemantic()
        {
            throw new NotImplementedException();
        }
    }
}
