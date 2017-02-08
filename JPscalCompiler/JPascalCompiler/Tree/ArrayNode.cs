using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class ArrayNode:SentenceNode
    {
        public ArrayNode(List<IdNode> arrayIds, List<RangeExpression> arrayDimensions, TokenTypes arrayType)
        {
            ArrayIds = arrayIds;
            ArraySize = arrayDimensions;
            ArrayType = arrayType;
            IsACollection = true;
        }

        public List<IdNode> ArrayIds { get; set; }
        public List<RangeExpression> ArraySize { get; set; }
        public TokenTypes ArrayType { get; set; }
        public bool IsACollection { get; set; }
        protected override void ValidateNodeSemantic()
        {
            foreach (var idNode in ArrayIds)
            {
                SymbolTable.Instance.DeclareVariable(idNode.Label, "arraytype");
                var t = TypesTable.Instance.GetType("arraytype");
                TypesTable.Instance.RegisterType(idNode.Label, t);
            }

            foreach (var range in ArraySize)
            {
                range.ValidateSemantic();
            }
            TypesTable.Instance.GetType(ArrayType.ToString().ToLower());

        }
    }
}
