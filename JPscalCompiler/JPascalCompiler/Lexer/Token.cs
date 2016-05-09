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
        PunctuationSymbol,
        Char,
        String,
        Boolean,
        Type,
        Array,
        Of,
        Var,
        True,
        False,
        OpEquals,
        OpSum,
        OpSub,
        OpMult,
        OpDivr,
        Opdiv,
        OpMod,
        OpAnd,
        OpNot,
        OpOr,
        PsOpenBracket,
        PsCloseBracket,
        PsArrayRange,
        PsComa,
        PsSentenseEnd
    }

    public class Token
    {
        public string Lexeme;
        public TokenTypes Type;
        public int Row;
        public int Column;
    }
}
