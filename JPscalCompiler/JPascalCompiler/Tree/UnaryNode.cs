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
    }
}