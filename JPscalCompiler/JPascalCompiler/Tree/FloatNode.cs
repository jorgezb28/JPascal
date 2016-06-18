namespace JPascalCompiler.Tree
{
    public class FloatNode:ExpressionNode
    {
        private float _intval;

        public FloatNode(float intval)
        {
            _intval = intval;
        }
    }
}