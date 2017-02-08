using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Semantic.Types
{
    class CharType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is CharType;
        }

        public override bool IsComparable(BaseType otherType)
        {
            return otherType is CharType;
        }
    }
}
