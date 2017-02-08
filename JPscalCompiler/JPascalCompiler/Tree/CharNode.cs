using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class CharNode: ExpressionNode
    {
        public char Ch { get; set; } 

        public CharNode(char ch)
        {
            Ch = ch;
        }

        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("char");
        }
    }
}