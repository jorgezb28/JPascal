using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class RangeExpression : ExpressionNode
    {
        public RangeExpression()
        {
            Start = new ExpressionNode();
            End = new ExpressionNode();
        }
        public ExpressionNode Start { get; set; }
        public ExpressionNode End { get; set; }
        public override BaseType ValidateSemantic()
        {
            var startValidation = Start.ValidateSemantic();
            var endValitation = End.ValidateSemantic();

            if (!(startValidation is IntType))
            {
                throw  new SemanticException("Start range index is not a number");
            }
            if (!(endValitation is IntType))
            {
                throw new SemanticException("End range index is not a number");
            }

            return new RangeArrayType();
        }
    }
}
