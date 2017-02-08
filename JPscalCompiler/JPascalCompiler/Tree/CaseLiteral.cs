using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class CaseLiteral:ExpressionNode
    {
        public CaseLiteral()
        {
            LiteralSentences = new List<SentenceNode>();
        }
        public List<SentenceNode> LiteralSentences { get; set; }

        public override BaseType ValidateSemantic()
        {
            foreach (var literalSentence in LiteralSentences)
            {
                literalSentence.ValidateSemantic();
            }

            return null;
        }
    }
}
