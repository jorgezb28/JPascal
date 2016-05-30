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
        OpDiv,
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
        PsCloseParentesis,
        If,
        Then,
        Begin,
        Else,
        Writeln,
        For,
        To,
        Do,
        Continue,
        Break,
        In,
        While,
        Repeat,
        Until,
        Const,
        Case,
        PsPointAccesor
    }

    public class Token
    {
        public string Lexeme;
        public TokenTypes Type;
        public int Row;
        public int Column;
    }
}
