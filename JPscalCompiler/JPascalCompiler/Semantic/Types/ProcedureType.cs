using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Semantic.Types
{
    class ProcedureType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is ProcedureType;
        }

        public override bool IsComparable(BaseType otherType)
        {
            return otherType is ProcedureType;
        }
    }
}
