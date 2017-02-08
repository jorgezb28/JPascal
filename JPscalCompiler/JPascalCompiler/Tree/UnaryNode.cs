using JPascalCompiler.Semantic.Types;
using JPascalCompiler.Tree;

namespace JPascalCompiler.Parser
{
    internal class UnaryNode: ExpressionNode
    {
        public ExpressionNode UnaryOperand { get; set; }

        public UnaryNode()
        {
            UnaryOperand = new ExpressionNode();
        }

        public override BaseType ValidateSemantic()
        {
           return UnaryOperand.Expressions[0].ValidateSemantic();
        }
    }
}