using System;
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
                //key = token type, value = lexeme
                { "ps_openBracket","["},
                { "ps_closedBracket","]"},
                { "ps_arrayRange",".." },
                { "ps_coma" ,","}
            };
        }

        private void InitializeHashTableofOperators()
        {
            _operators = new Hashtable
            {
                //key = token type, value = lexeme
                {"op_equals","="},
                {"op_sum","+"},
                {"op_sub","-"},
                {"op_mult","*"},
                {"op_divr","/"},
                {"op_div","div"},
                {"op_mod","mod"},
                {"op_and","and"},
                {"op_not","not"},
                {"op_or","or" }

            };
        }

        private void InitializeHashTableofReservedWords()
        {
            //key = token type, value = lexeme
            _reservedWords = new Hashtable
            {
                {"rw_int", "int"},
                {"rw_float","float" },
                {"rw_char","char" },
                {"rw_string","string" },
                {"rw_boolean","boolean"},
                {"rw_type","type"},
                {"rw_array","array"},
                {"rw_of","of"},
                {"rw_var","var" },
                {"rw_true","true"},
                {"rw_false","false" }
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
                        else if (char.IsDigit(_currentSymbol.Character))
                        {
                            state = 2;
                            tokenRow = _currentSymbol.Row;
                            tokenColumn = _currentSymbol.Column;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }
                        else if (_operators.ContainsValue(_currentSymbol.Character.ToString()))
                        {
                            state = 5;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            currentLexeme += _currentSymbol.Character;
                            _currentSymbol = CodeContent.NextCharacter();
                        }else if (  _currentSymbol.Character =='.')
                        {
                            state = 7;
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
                            if (_reservedWords.ContainsValue(currentLexeme.ToLower()))
                            {
                                return new Token
                                {
                                    Type = TokenTypes.ReservedWord,
                                    Lexeme = currentLexeme,
                                    Column = tokenColumn,
                                    Row = tokenRow
                                };
                            }
                            if (_operators.ContainsValue(currentLexeme.ToLower()))
                            {
                                return new Token
                                {
                                    Type = TokenTypes.Operator,
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
                        return new Token
                        {
                            Type = TokenTypes.Operator,
                            Column = tokenColumn,
                            Row = tokenRow,
                            Lexeme = currentLexeme
                        };
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
                            Type = TokenTypes.PunctuationSymbol,
                            Lexeme = currentLexeme,
                            Column = tokenColumn,
                            Row = tokenRow
                        };
                        
                    default:
                        break;
                }
            }
        }
    }
}
