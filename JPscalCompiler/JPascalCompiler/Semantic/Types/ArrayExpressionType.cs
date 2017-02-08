using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Semantic.Types
{
    class ArrayExpressionType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is ArrayExpressionType;
        }

        public override bool IsComparable(BaseType otherType)
        {
            return otherType is ArrayExpressionType;
        }
    }
}
