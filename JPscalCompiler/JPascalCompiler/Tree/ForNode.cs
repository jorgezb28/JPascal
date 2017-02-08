using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

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

        protected override void ValidateNodeSemantic()
        {
            var typeIndex = IndexVarible.ValidateSemantic();
            if (!(typeIndex is IntType))
            {
                throw  new SemanticException("For index variable is not a integer");
            }
            

            var init = InitialValue.Expressions[0].ValidateSemantic();
            if (!(init is IntType))
            {
                throw new SemanticException("For start loop  is not a valid expression");
            }
           

            var final = FinalValue.Expressions[0].ValidateSemantic();
            if (!(final is IntType))
            {
                throw new SemanticException("For end loop is not a valid expression");
            }
           

            foreach (var sentense in ForBodySentenses)
            {
                sentense.ValidateSemantic();
            }
        }
    }
}
