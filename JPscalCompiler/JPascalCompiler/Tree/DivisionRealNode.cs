using System;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class DivisionRealNode: BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (leftOperand is IntType || leftOperand is FloatType)
            {
                if (rightOperand is IntType || rightOperand is FloatType)
                {
                    if (rightOperand is IntType)
                    {
                        var intenger = (NumberNode)RigthOperand;
                        if (intenger.Value == 0)
                        {
                            throw new SemanticException("Divide by zero is not valid");
                        }
                    }
                    if (rightOperand is FloatType)
                    {
                        var floatNum = (FloatNode)RigthOperand;
                        if (!(Math.Abs(floatNum.FloatValue) > 0))
                        {
                            throw new SemanticException("Divide by zero is not valid");
                        }
                    }
                }
                else
                {
                    throw new SemanticException("Right operand is not a divisble number");
                }
            }
            else
            {
                throw new SemanticException("Left operand is not a divisible number");
            }

            return leftOperand;
        }
    }
}