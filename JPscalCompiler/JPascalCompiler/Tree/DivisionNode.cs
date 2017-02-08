using System;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class DivisionNode: BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (leftOperand is IntType )
            {
                if (rightOperand is IntType )
                {
                    var intenger = (NumberNode) RigthOperand;
                    if (intenger.Value == 0)
                    {
                        throw  new SemanticException("Divide by zero is not valid");
                    }
                }
                else
                {
                    throw new SemanticException("Right operand is not a integer number");
                }
            }
            else
            {
                throw new SemanticException("Left operand is not a integer number");
            }

            return leftOperand;
        }
    }
}