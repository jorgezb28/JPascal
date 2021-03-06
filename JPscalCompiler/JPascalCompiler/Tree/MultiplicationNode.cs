﻿using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;
using JPascalCompiler.Tree;

namespace JPascalCompiler.Parser
{
    internal class MultiplicationNode : BinaryOperationNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftOperand = LeftOperand.ValidateSemantic();
            var rightOperand = RigthOperand.ValidateSemantic();

            if (leftOperand is IntType || leftOperand is FloatType)
            {
                if (rightOperand is IntType || rightOperand is FloatType)
                {

                }
                else
                {
                    throw new SemanticException("Right operand is not a number");
                }
            }
            else
            {
                throw new SemanticException("Left operand is not a number");
            }

            return leftOperand;
        }
    }
}