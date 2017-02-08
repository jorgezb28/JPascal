using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class ForInNode:SentenceNode
    {
        public IdNode CurrentItem { get; set; }

        public IdNode SourceList { get; set; }

        public List<SentenceNode> ForInSentences { get; set; }
        public ForInNode(IdNode currentItem)
        {
            CurrentItem = currentItem;
            ForInSentences= new List<SentenceNode>();
        }

        protected override void ValidateNodeSemantic()
        {
            CurrentItem.ValidateSemantic();
            if (SymbolTable.Instance.Contains(CurrentItem.Label))
            {
                throw new SemanticException(string.Format("Variable {0} have been declared.Please change the name", CurrentItem.Label));
            }

            SourceList.ValidateSemantic();
            if (SymbolTable.Instance.Contains(CurrentItem.Label) == false)
            {
                throw new SemanticException(string.Format("Variable {0} have not been declared", CurrentItem.Label));
            }

            var colletionType = SymbolTable.Instance.GetVariable(SourceList.Label);
            if (!(colletionType is ArrayType) || !(colletionType is EnumeratorType))
            {
                throw    new SemanticException(string.Format("Varible {0} is not a collection to iterate .",SourceList.Label));
            }

        }
    }
}
