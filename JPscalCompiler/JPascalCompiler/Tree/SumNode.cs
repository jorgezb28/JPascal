using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    internal class SumNode: BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (leftOperand is IntType || leftOperand is FloatType || leftOperand is StringType)
            {
                if (rightOperand is IntType || rightOperand is FloatType || leftOperand is StringType)
                {

                }
                else
                {
                    throw new SemanticException("Right operand type is not valid for sum expression");
                }
            }
            else
            {
                throw new SemanticException("Left operand type is not valid for sum expression");
            }

            return leftOperand;

        }
    }
}