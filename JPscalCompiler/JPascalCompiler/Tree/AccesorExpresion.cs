using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class AccesorExpresion : ExpressionNode
    {
        public AccesorExpresion()
        {
            AccesorSentences = new List<ExpressionNode>();
        }

        public IdNode AccessorId { get; set; }
        public List<ExpressionNode> AccesorSentences { get; set; }
        public override BaseType ValidateSemantic()
        {
            var t =AccessorId.ValidateSemantic();
            foreach (var accesorSentence in AccesorSentences)
            {
                accesorSentence.ValidateSemantic();
            }
            return t;
        }
    }
}
