namespace JPascalCompiler.Tree
{
    public class IdNode:ExpressionNode
    {
        public string Label { get; set; }
        public IdNode(string label)
        {
            Label = label;
        }
    }
}