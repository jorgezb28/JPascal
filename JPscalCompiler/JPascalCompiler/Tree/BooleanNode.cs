using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    internal class BooleanNode: ExpressionNode
    {
        public bool Value { get; set; }


        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("bool");
        }
    }
}