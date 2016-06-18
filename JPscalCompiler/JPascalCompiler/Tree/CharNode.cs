namespace JPascalCompiler.Tree
{
    public class CharNode: ExpressionNode
    {
        private char _ch;

        public CharNode(char ch)
        {
            _ch = ch;
        }
    }
}