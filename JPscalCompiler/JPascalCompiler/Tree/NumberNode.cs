namespace JPascalCompiler.Tree
{
    internal class NumberNode :ExpressionNode
    {
        private int _value;

        public NumberNode(int value)
        {
            _value = value;
        }
    }
}