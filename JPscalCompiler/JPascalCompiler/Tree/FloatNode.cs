using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class FloatNode:ExpressionNode
    {
        public float FloatValue { get; set; }

        public FloatNode(float intval)
        {
            FloatValue = intval;
        }

        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("float");

        }
    }
}