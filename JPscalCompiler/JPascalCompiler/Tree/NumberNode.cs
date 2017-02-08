using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    internal class NumberNode :ExpressionNode
    {
        public int Value { get; set; }

        public NumberNode(int value)
        {
            Value = value;
        }

        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("integer");
        }
    }
}