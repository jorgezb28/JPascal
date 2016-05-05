using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Lexer
{

    public enum TokenTypes
    {
        EOF,
        Id,
        Integer,
        Float,
        ReservedWord,
        Operator,
        PunctuationSymbol
    }

    public class Token
    {
        public string Lexeme;
        public TokenTypes Type;
        public int Row;
        public int Column;
    }
}
