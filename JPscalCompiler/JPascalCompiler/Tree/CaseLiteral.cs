using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class CaseLiteral:ExpressionNode
    {
        public CaseLiteral()
        {
            LiteralSentences = new List<SentenceNode>();
        }
        public List<SentenceNode> LiteralSentences { get; set; }
        
    }
}
