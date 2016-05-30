using System.Collections;

namespace JPascalCompiler.LexerFolder
{
    public class Lexer
    {
        public SourceCode CodeContent;
        private Symbol _currentSymbol;
        public bool IsHtml;
        private Hashtable _reservedWords;
        private Hashtable _operators;
        private Hashtable _punctuationSymbols;

        public Lexer(SourceCode sourceCodeContent)
        {
            CodeContent = sourceCodeContent;
            InitializeHashTableofReservedWords();
            InitializeHashTableofOperators();
            InitializeHashTableofPunctuationSymbols();
            _currentSymbol = CodeContent.NextCharacter();
            IsHtml = true;
        }

        private void InitializeHashTableofPunctuationSymbols()
        {
            _punctuationSymbols = new Hashtable
            {
                //key = token TYPE, value = lexeme
                { "[",TokenTypes.PsOpenBracket},
                { "]",TokenTypes.PsCloseBracket},
                { ".." ,TokenTypes.PsArrayRange},
                { ",",TokenTypes.PsComa},
                { ";",TokenTypes.PsSentenseEnd},
                {":", TokenTypes.PsColon},
                {":=",TokenTypes.PsAssignment},
                {"(",TokenTypes.PsOpenParentesis},
                {")",TokenTypes.PsCloseParentesis},
                { ".", TokenTypes.PsPointAccesor}
            };
        }

        private void InitializeHashTableofOperators()
        {
            _operators = new Hashtable
            {
                //key = token TYPE, value = lexeme
                {"=",TokenTypes.OpEquals},
                {"+", TokenTypes.OpSum},
                {"-",TokenTypes.OpSub},
                {"*",TokenTypes.OpMult},
                {"/", TokenTypes.OpDivr},
                {"div", TokenTypes.OpDiv},
                {"mod",TokenTypes.OpMod},
                {"and", TokenTypes.OpAnd},
                {"not",TokenTypes.OpNot},
                {"or", TokenTypes.OpOr },
                {">", TokenTypes.OpGreaterThan },
                {">=", TokenTypes.OpGreaterThanOrEquals },
                {"<", TokenTypes.OpLessThan },
                {"<=", TokenTypes.OpLessThanOrEquals },
                {"<%", TokenTypes.BeginPascalCode},
                {"<>", TokenTypes.OpNotEquals}
            };
        }

        private void InitializeHashTableofReservedWords()
        {
            //key = token TYPE, value = lexeme
            _reservedWords = new Hashtable
            {
                //{"int", TokenTypes.Integer},
                //{"float",TokenTypes.Float },
                //{"char",TokenTypes.Char }, pendiente
                //{"string",TokenTypes.String }, pendiente
                //{"boolean",TokenTypes.Boolean}, pendiente
                {"type",TokenTypes.Type},
                {"array",TokenTypes.Array},
                {"of",TokenTypes.Of},
                {"var",TokenTypes.Var },
                //{"true",TokenTypes.True},
                //{"false",TokenTypes.False },
                {"record",TokenTypes.Record },
                {"end",TokenTypes.End},
                {"if",TokenTypes.If},
                {"then",TokenTypes.Then},
                {"begin",TokenTypes.Begin},
                {"else",TokenTypes.Else},
                {"for",TokenTypes.For},
                {"to",TokenTypes.To},
                {"do",TokenTypes.Do},
                {"in",TokenTypes.In},
                {"while",TokenTypes.While},
                {"repeat",TokenTypes.Repeat},
                {"until",TokenTypes.Until},
                {"const" ,TokenTypes.Const},
                {"case",TokenTypes.Case}
            };
        }


        public Token GetNextToken()
        {
            var state = 0;
            var currentLexeme = "";
            var tokenRow = 0;
            var tokenColumn = 0;

            while (true)
            {
                switch (state)
                {
                    case 0:
                        if (_currentSymbol.Character == '\0')
                        {
                            state = 6;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme = "$";
                        }
                        else if (char.IsWhiteSpace(_currentSymbol.Character))
                        {
                            state = 0;
                            currentLexeme = "";
                            if (_currentSymbol.Character == '\n')
                            {
                                tokenColumn = 0;
                                tokenRow++;
                            }
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (char.IsLetter(_currentSymbol.Character))
                        {
                            state = 1;
                            tokenRow = _currentSymbol.Row;
                            tokenColumn = _currentSymbol.Column;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (char.IsDigit(_currentSymbol.Character) )
                        {
                            state = 2;
                            tokenRow = _currentSymbol.Row;
                            tokenColumn = _currentSymbol.Column;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if ((_operators.ContainsKey(_currentSymbol.Character.ToString()) ||
                                 _punctuationSymbols.ContainsKey(_currentSymbol.Character.ToString())) 
                            && _currentSymbol.Character != '>' && _currentSymbol.Character != '<' && _currentSymbol.Character != '('
                            && _currentSymbol.Character != ':' & _currentSymbol.Character != '.')
                        {
                            state = 5;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '.')
                        {
                            state = 7;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (_currentSymbol.Character == ':')
                        {
                            state = 9;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (_currentSymbol.Character == '>')
                        {
                            state = 11;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '<')
                        {
                            state = 13;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '%')
                        {
                            state = 17;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '(')
                        {
                            state = 19;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '\'')
                        {
                            state = 23;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }



                        break;
                    case 1:
                        if (char.IsLetterOrDigit(_currentSymbol.Character) )
                        {
                            state = 1;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            if (_reservedWords.ContainsKey(currentLexeme.ToLower()))
                            {
                                var newTokenType = (TokenTypes)_reservedWords[currentLexeme.ToLower()];
                                return new Token
                                {
                                    Type = newTokenType,
                                    Lexeme = currentLexeme,
                                    Column = tokenColumn,
                                    Row = tokenRow
                                };
                            }
                            if (_operators.ContainsKey(currentLexeme.ToLower()))
                            {
                                var newTokenType = (TokenTypes)_operators[currentLexeme.ToLower()];
                                return new Token
                                {
                                    Type = newTokenType,
                                    Lexeme = currentLexeme,
                                    Column = tokenColumn,
                                    Row = tokenRow
                                };
                            }
                            return new Token
                            {
                                Type = TokenTypes.Id,
                                Lexeme = currentLexeme,
                                Column = tokenColumn,
                                Row = tokenRow
                            };
                        }
                        break;
                    case 2:
                        if (char.IsDigit(_currentSymbol.Character) )
                        {
                            state = 2;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (_currentSymbol.Character == '.')
                        {
                            state = 3;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else{
                            return new Token
                            {
                                //Type = TokenTypes.Integer,
                                Type = TokenTypes.Id,
                                Lexeme = currentLexeme,
                                Row = tokenRow,
                                Column = tokenColumn
                            };
                        }
                        break;
                    case 3:
                        if (char.IsDigit(_currentSymbol.Character))
                        {
                            state = 4;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            throw new LexicalExcpetion("Unsupported token:" + currentLexeme +" at col:"+ tokenColumn +" and row:"+ tokenRow );
                        }
                        break;

                    case 4:
                        if (char.IsDigit(_currentSymbol.Character))
                        {
                            state = 4;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            return new Token
                            {
                                //Type = TokenTypes.Float,
                                Type = TokenTypes.Id,
                                Lexeme = currentLexeme,
                                Row = tokenRow,
                                Column = tokenColumn
                            };
                        }
                        break;
                    case 5:
                        if (_punctuationSymbols.ContainsKey(currentLexeme.ToLower()))
                        {
                            var newTokenType = (TokenTypes)_punctuationSymbols[currentLexeme.ToLower()];
                            return new Token
                            {
                                Type = newTokenType,
                                Lexeme = currentLexeme,
                                Column = tokenColumn,
                                Row = tokenRow
                            };
                        }
                        if (_operators.ContainsKey(currentLexeme.ToLower()))
                        {
                            var newTokenType = (TokenTypes)_operators[currentLexeme.ToLower()];
                            return new Token
                            {
                                Type = newTokenType,
                                Lexeme = currentLexeme,
                                Column = tokenColumn,
                                Row = tokenRow
                            };
                        }
                        break;
                    case 6:
                        return  new Token {Type = TokenTypes.EOF, Column = tokenColumn,Lexeme = currentLexeme,Row = tokenRow};
                    case 7:
                        if (_currentSymbol.Character == '.')
                        {
                            state = 8;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            throw new LexicalExcpetion("Unsupported token:" + currentLexeme +_currentSymbol.Character +" at col:" + tokenColumn + " and row:" + tokenRow);
                        }
                        break;

                    case 8:
                        return new Token
                        {
                            Type = TokenTypes.PsArrayRange,
                            Lexeme = currentLexeme,
                            Column = tokenColumn,
                            Row = tokenRow
                        };
                    case 9:
                        if (_currentSymbol.Character =='=')
                        {
                            state = 10;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            return new Token
                            {
                                Type = TokenTypes.PsColon,
                                Lexeme = currentLexeme,
                                Column = tokenColumn,
                                Row = tokenRow
                            };
                        }
                        break;
                    case 10:
                            return new Token
                            {
                                Type = TokenTypes.PsAssignment,
                                Lexeme = currentLexeme,
                                Column = tokenColumn,
                                Row = tokenRow
                            };
                    case 11:
                        if (_currentSymbol.Character == '=')
                        {
                            state = 12;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            return new Token
                            {
                                Column = tokenColumn,
                                Row = tokenRow,
                                Lexeme = currentLexeme,
                                Type = TokenTypes.OpGreaterThan
                            };
                        }
                        break;
                    case 12:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.OpGreaterThanOrEquals
                        };
                    case 13:
                        if (_currentSymbol.Character == '=')
                        {
                            state = 14;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '%')
                        {
                            state = 15;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_currentSymbol.Character == '>')
                        {
                            state = 16;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else{
                            return new Token
                            {
                                Column = tokenColumn,
                                Row = tokenRow,
                                Lexeme = currentLexeme,
                                Type = TokenTypes.OpLessThan
                            };
                        }
                        break;
                    case 14:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.OpLessThanOrEquals
                        };
                    case 15:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.BeginPascalCode
                        };
                    case 16:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.OpNotEquals
                        };
                    case  17:
                        if (_currentSymbol.Character == '>')
                        {
                            state = 18;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            throw new LexicalExcpetion("Unsupported token:" + currentLexeme + _currentSymbol.Character + " at col:" + tokenColumn + " and row:" + tokenRow);   
                        }
                        break;
                    case 18:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.EndPascalCode
                        };
                    case 19:
                        if (_currentSymbol.Character == '*')
                        {
                            state = 20;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            return new Token
                            {
                                Column = tokenColumn,
                                Row = tokenRow,
                                Lexeme = currentLexeme,
                                Type = TokenTypes.PsOpenParentesis
                            };
                        }
                        break;

                    case 20:
                        if (_currentSymbol.Character =='*')
                        {
                            state = 21;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (_currentSymbol.Character != '*' || _currentSymbol.Character!='\n' || _currentSymbol.Character!='$')
                        {
                            state = 20;
                            currentLexeme = "";
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        break;
                    case 21:
                        if (_currentSymbol.Character == ')')
                        {
                            state = 22;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else
                        {
                            throw new LexicalExcpetion("Unsupported token:" + currentLexeme + _currentSymbol.Character + " at col:" + tokenColumn + " and row:" + tokenRow);   
                        }
                        break;
                    case 22:
                        state = 0;
                        _currentSymbol = CodeContent.NextCharacter();
                        break;
                    case 23:
                        if (_currentSymbol.Character == '\'')
                        {
                            state = 24;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (_currentSymbol.Character != '\'' || _currentSymbol.Character!='\n' || _currentSymbol.Character!='$')
                        {
                            state = 23;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        break;

                    case 24:
                        return new Token
                        {
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme,
                            Type = TokenTypes.Id
                        };
                       
                    default:
                        break;
                }
            }
        }
    }
}
