using System;
using System.Runtime.Serialization;

namespace JPascalCompiler.Lexer
{
    [Serializable]
    internal class LexicalExcpetion : Exception
    {
        public LexicalExcpetion()
        {
        }

        public LexicalExcpetion(string message) : base(message)
        {
        }

        public LexicalExcpetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LexicalExcpetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}