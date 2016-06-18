namespace JPascalCompiler.Tree
{
    internal class StringNode: ExpressionNode
    {
        private string _str;

        public StringNode(string str)
        {
            this._str = str;
        }
    }
}