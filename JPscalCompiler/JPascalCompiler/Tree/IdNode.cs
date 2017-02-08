using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class IdNode:ExpressionNode
    {
        public string Label { get; set; }
        public IdNode(string label)
        {
            Label = label;
        }

        public override BaseType ValidateSemantic()
        {
            if (!TypesTable.Instance.Contains(Label))
            {
                return SymbolTable.Instance.GetVariable(Label);
            }
            return TypesTable.Instance.GetType(Label);

            //var varType = SymbolTable.Instance.GetVariable(Label);
            //if ( !(varType is StringType))
            //{
            //    throw new SemanticException("Id values is not a string");
            //}
            //return varType;
        }
    }
}