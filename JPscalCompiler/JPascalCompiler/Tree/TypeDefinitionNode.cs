using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class TypeDefinitionNode:SentenceNode
    {
        public List<IdNode> TypeId { get; set; }
        public ExpressionNode TypeDefinitionType { get; set; }

        public TypeDefinitionNode(List<IdNode> typeId, ExpressionNode typeDefinitionType)
        {
            TypeId = typeId;
            TypeDefinitionType = typeDefinitionType;
        }

        protected override void ValidateNodeSemantic()
        {
            var idType = TypesTable.Instance.GetType(TypeDefinitionType.Expressions[0]);
            var stringDecl = TypesTable.Instance.GetStringType(idType);

            foreach (var idNode in TypeId)
            {
                SymbolTable.Instance.DeclareVariable(idNode.Label,stringDecl);
            }

            TypeDefinitionType.Expressions[0].ValidateSemantic();
        }
    }
}
