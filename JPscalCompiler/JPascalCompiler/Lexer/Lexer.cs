using System;
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

        public Lexer(SourceCode sourceCodeContent)
        {
            CodeContent = sourceCodeContent;
            _currentSymbol = CodeContent.nextChar();
        }


        public Token GetNextToken()
        {
            var state = 0;
            var lexeme = "";
            var tokenRow = 0;
            var tokenColumn = 0;

            while (true)
            {
                switch (state)
                {
                    case 0:
                        if (_currentSymbol.Char == '\0')
                        {
                            state = 6;
                            tokenColumn = _currentSymbol.Column;
                            tokenRow = _currentSymbol.Row;
                            lexeme = "$";
                        }
                        break;

                    case 6:
                        return  new Token {Type = TokenTypes.EOF, Column = tokenColumn,Lexeme = lexeme,Row = tokenRow};

                    default:
                        break;
                }
            }
        }
    }
}
