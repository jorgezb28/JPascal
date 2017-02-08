using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;

namespace JPascalCompiler.Tree
{
    public class RecordNode:SentenceNode
    {
        
        public RecordNode(IdNode recordId, List<RecordPropertyNode> recordProperties)
        {
            RecordId = recordId;
            RecordProperties = recordProperties;
        }

        public List<RecordPropertyNode> RecordProperties { get; set; }
        public IdNode RecordId { get; set; }
        protected override void ValidateNodeSemantic()
        {
            SymbolTable.Instance.DeclareVariable(RecordId.Label,"record");

            foreach (var recordPropertyNode in RecordProperties)
            {
                recordPropertyNode.ValidateSemantic();
            }
        }
    }
}
