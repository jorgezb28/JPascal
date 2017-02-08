using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

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
        protected override void ValidateNodeSemantic()
        {
            var caseExpr = CaseExpression.Expressions[0].ValidateSemantic();
            if (!(caseExpr is BooleanType))
            {
                throw  new SemanticException("Case expression is not a boolean expression");
            }

            foreach (var caseLiteral in CaseLiterals)
            {
                var caseliteraltype = TypesTable.Instance.GetType(caseLiteral);
                if (!(caseliteraltype is BooleanType) || !(caseliteraltype is CharType) || !(caseliteraltype is IntType)
                    || !(caseliteraltype is RangeArrayType) || !(caseliteraltype is StringType))
                {
                    throw new SemanticException("Case literal is not a sentence");
                }
            }

            foreach (var sentence in ElseLiteralSentences)
            {
                sentence.ValidateSemantic();
            }



        }
    }
}
