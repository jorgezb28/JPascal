using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    internal class StringNode: ExpressionNode
    {
        private string _str;

        public StringNode(string str)
        {
            this._str = str;
        }

        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("string");
        }
    }
}