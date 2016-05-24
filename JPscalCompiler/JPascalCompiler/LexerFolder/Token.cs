namespace JPascalCompiler.LexerFolder
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
        PsSentenseEnd,
        Record,
        PsColon,
        PsAssignment,
        End,
        OpGreaterThan,
        OpGreaterThanOrEquals,
        OpLessThan,
        OpLessThanOrEquals,
        BeginPascalCode,
        OpNotEquals,
        EndPascalCode,
        PsOpenParentesis,
        PsCloseParentesis
    }

    public class Token
    {
        public string Lexeme;
        public TokenTypes Type;
        public int Row;
        public int Column;
    }
}
