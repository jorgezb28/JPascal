using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Parser;

namespace JPascalCompiler.Tree
{
    public class DeclarationNode :SentenceNode
    {
        public IdNode IdType { get; set; }
        public List<IdNode> IdsList;
        public ExpressionNode AssignedValue { get; set; }

        public DeclarationNode(int row, int column)
        {
            RowSentence = row;
            ColumnSentence = column;
            IdsList = new List<IdNode>();
        }
    }
}
