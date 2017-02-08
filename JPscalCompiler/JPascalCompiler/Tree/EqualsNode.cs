using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class EqualsNode: BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (!leftOperand.IsComparable(rightOperand))
            {
                throw new SemanticException("The operands are not comparables");
            }
            return leftOperand;
        }
    }
}