using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Semantic.Types
{
    class StringType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is StringType;
        }

        public override bool IsComparable(BaseType otherType)
        {
            return otherType is StringType;
        }
    }
}
