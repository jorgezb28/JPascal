﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Tree
{
    public class RangeExpression : ExpressionNode
    {
        public ExpressionNode Start { get; set; }
        public ExpressionNode End { get; set; }
    }
}
