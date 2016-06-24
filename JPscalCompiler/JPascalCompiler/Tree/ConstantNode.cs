using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace JPascalCompiler.Tree
{
    public class ConstantNode: SentenceNode
    {
        public ConstantNode(int row, int column)
        {
            ColumnSentence = column;
            RowSentence = row;
        }

        public IdNode ConstantId { get; set; }
        public ExpressionNode ConstantValue { get; set; }
        public string ConstantType { get; set; }
    }
}
