using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Tree
{
    public class ProcedureDeclarationNode:SentenceNode
    {
        public ProcedureDeclarationNode(ExpressionNode procedureName, List<ExpressionNode> procedureParameters, List<SentenceNode> procedureBlockSentence)
        {
            ProcedureName = procedureName;
            ProcedureBlockSentence = procedureBlockSentence;
            ProcedureParameters = procedureParameters;
        }

        public ExpressionNode ProcedureName { get; set; }
        public List<ExpressionNode> ProcedureParameters { get; set; }
        public List<SentenceNode> ProcedureBlockSentence { get; set; }
        protected override void ValidateNodeSemantic()
        {
            if (ProcedureName is IdNode)
            {
                var procedureName = (IdNode)ProcedureName;
                TypesTable.Instance.RegisterType(procedureName.Label, new ProcedureType());
            }
            ProcedureName.ValidateSemantic();
            
            foreach (var parameter in ProcedureParameters)
            {
                parameter.ValidateSemantic();
            }

            foreach (var sentenceNode in ProcedureBlockSentence)
            {
                sentenceNode.ValidateSemantic();
            }
        }
    }
}
