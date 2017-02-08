using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Parser;
using JPascalCompiler.Semantic;

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

        protected override void ValidateNodeSemantic()
        {
            foreach (var idNode in IdsList)
            {
                if (!SymbolTable.Instance.Contains(IdType.Label))
                {
                    var idType = TypesTable.Instance.GetType(IdType);
                    var stringtype = TypesTable.Instance.GetStringType(idType);
                    SymbolTable.Instance.DeclareVariable(idNode.Label, stringtype);
                }
                else
                {
                    SymbolTable.Instance.DeclareVariable(idNode.Label, IdType.Label);
                 }
                //var t = SymbolTable.Instance.Contains(IdType.Label);
                //var str = TypesTable.Instance.GetStringType(t);
               
            }

            if (!TypesTable.Instance.Contains(IdType.Label))
            {
                throw new SemanticException(String.Format("The type {0} have not been declared",IdType.Label));
            }

            if (AssignedValue!= null)
            {
                AssignedValue.ValidateSemantic();
            }
            
        }
    }
}
