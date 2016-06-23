using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
