using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Semantic.Types
{
    class RangeArrayType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is RangeArrayType;
        }

        public override bool IsComparable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}
