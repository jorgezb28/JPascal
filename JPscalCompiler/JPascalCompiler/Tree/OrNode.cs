using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class OrNode:BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (!(leftOperand is BooleanType))
            {
                throw new SemanticException("The left operand is not a boolean value");
            }

            if (!(rightOperand is BooleanType))
            {
                throw new SemanticException("The left operand is not a boolean value");
            }

            return leftOperand;
        }
    }
}