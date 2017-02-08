using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class RecordPropertyNode:SentenceNode
    {
        public RecordPropertyNode()
        {
            RecordPropertyIds = new List<IdNode>();
            RecordPropertyType = new ExpressionNode();
        }
        public List<IdNode> RecordPropertyIds { get; set; }
        public ExpressionNode RecordPropertyType { get; set; }


        protected override void ValidateNodeSemantic()
        {
            foreach (var id in RecordPropertyIds)
            {
                var type = TypesTable.Instance.GetType(RecordPropertyType);
                TypesTable.Instance.RegisterType(id.Label,type);
            }
        }
    }
}
