﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JPascalCompiler.Lexer
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
                {":=",TokenTypes.PsAssignment}
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
                {"div", TokenTypes.Opdiv},
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
                {"int", TokenTypes.Integer},
                {"float",TokenTypes.Float },
                {"char",TokenTypes.Char },
                {"string",TokenTypes.String },
                {"boolean",TokenTypes.Boolean},
                {"type",TokenTypes.Type},
                {"array",TokenTypes.Array},
                {"of",TokenTypes.Of},
                {"var",TokenTypes.Var },
                {"true",TokenTypes.True},
                {"false",TokenTypes.False },
                {"record",TokenTypes.Record },
                {"end",TokenTypes.End}
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
                            && _currentSymbol.Character != '>' && _currentSymbol.Character != '<')
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
                                Type = TokenTypes.Integer,
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
                                Type = TokenTypes.Float,
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
                    default:
                        break;
                }
            }
        }
    }
}
