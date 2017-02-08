using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class ArrayExpression : ExpressionNode
    {
        public ArrayExpression(List<RangeExpression> arraySize, TokenTypes arrayType)
        {
            //ArrayIds = arrayIds;
            ArraySize = arraySize;
            ArrayType = arrayType;
        }
        //public List<IdNode> ArrayIds { get; set; }
        public List<RangeExpression> ArraySize { get; set; }
        public TokenTypes ArrayType { get; set; }
        public override BaseType ValidateSemantic()
        {
            foreach (var rangeExpression in ArraySize)
            {
                rangeExpression.ValidateSemantic();
            }

            var type = ArrayType.ToString().ToLower();
            return TypesTable.Instance.GetType(type);
        }
    }

}
