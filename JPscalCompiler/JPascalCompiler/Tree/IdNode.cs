namespace JPascalCompiler.Tree
{
    public class IdNode
    {
        public string Label { get; set; }
        public IdNode(string label)
        {
            Label = label;
        }
    }
}