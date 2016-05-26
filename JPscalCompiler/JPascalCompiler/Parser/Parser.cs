using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using JPascalCompiler.LexerFolder;

namespace JPascalCompiler.Parser
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;
        private List<string> _parserSyntaxErrors;

        public Parser(Lexer lexer)
        {
            this._lexer = lexer;
            _parserSyntaxErrors = new List<string>();
        }

        public void Parse()
        {
            _currentToken = _lexer.GetNextToken();
            LS();

            if (_currentToken.Type != TokenTypes.EOF)
             {
                 _parserSyntaxErrors.Add("Se esperaba EOF");
            }
        }

        private void LS()
        {
            if(S())
            {
                LS();
            }
            
        }

        private bool S()
        {
            if (Declaracion())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {

                }
                else
                {
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ; at column, row: "+_currentToken.Column+" , "+_currentToken.Row);
                }
            }

        }

        private bool Declaracion()
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                _currentToken = _lexer.GetNextToken();
                return FactorComunId();    
            }
            else
            {
                throw new SyntaxException("Syntax error. Expected word: '"+_currentToken.Lexeme+"' at colum:"+_currentToken.Column+" , row: "+_currentToken.Row);
            }
        }

        private bool FactorComunId()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                Y();
            }
            else
            {
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, column:"+_currentToken.Column+" , "+_currentToken.Row);
                return false;
            }
        }

        private void Y()
        {
            if (_currentToken.Type ==TokenTypes.PsColon)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    AsignarValor();
                }
                else
                {
                    _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, column:" + _currentToken.Column + " , " + _currentToken.Row);
                }
            }
            else
            {
                IdOpcional();
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Id)
                    {
                        return;
                    }//error

                }//exception

            }

            
        }

        private void AsignarValor()
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                Expression();
            }
            else
            {
                retur
                //_parserSyntaxErrors.Add("Syntax Error.Expected symbol: = at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
            }
        }

        private void Expression()
        {
            RelationalExpresion();
        }

        private void RelationalExpresion()
        {
            ExpresionAdicion();
            RelationalExpresionP();
        }

        private bool RelationalExpresionP()
        {
            if (OpRelational())
            {
                ExpresionAdicion();
                RelationalExpresionP();
            }
            return false;
        }

        private bool OpRelational()
        {
            if (_currentToken.Type == TokenTypes.OpLessThan)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpGreaterThan)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpLessThanOrEquals)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if (_currentToken.Type == TokenTypes.OpGreaterThanOrEquals)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if (_currentToken.Type == TokenTypes.OpNotEquals)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if (_currentToken.Type == TokenTypes.OpEquals)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            return false;
            
        }

        private void ExpresionAdicion()
        {
            ExpresionMul();
            ExpresionAdicionP();
        }

        private bool ExpresionAdicionP()
        {
            if (OpAdicion())
            {
                ExpresionMul();
                ExpresionAdicionP();
            }
            return false;
        }

        private bool OpAdicion()
        {
            if (_currentToken.Type == TokenTypes.OpSum)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpSub)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpOr)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            return false;
        }

        private void ExpresionMul()
        {
            ExpresionUnary();
            ExpresionMulP();
        }

        private bool ExpresionMulP()
        {
            if (OpMul())
            {
                ExpresionUnary();
                ExpresionMulP();
            }
            return false;
        }

        private bool OpMul()
        {

            if (_currentToken.Type == TokenTypes.OpMult)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpDivr)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if(_currentToken.Type == TokenTypes.OpDiv)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type == TokenTypes.OpMod)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if(_currentToken.Type == TokenTypes.OpAnd)
            {
                var op = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            return false;
        }

        private void ExpresionUnary()
        {
            if (_currentToken.Type == TokenTypes.OpNot)
            {
                _currentToken = _lexer.GetNextToken();
                Factor();
            }
            else
            {
                Factor();
            }
        }

        private bool Factor()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var name= _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            else if (_currentToken.Type ==TokenTypes.Integer)
            {
                var value = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            }else if(_currentToken.Type == TokenTypes.String)
            {
                var str = _currentToken.Lexeme;
                _currentToken = _lexer.GetNextToken();
                return true;
            } else if (_currentToken.Type == TokenTypes.Boolean)
            {
                return true;
            }
            // pendiente implementar LLAMARFUNCION
            return false;
        }

        private void IdOpcional()
        {
            if (_currentToken.Type == TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                ListaId();
            }
        }

        private void ListaId()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                IdOpcional();
            }
        }
    }

    public class SyntaxException : Exception
    {
        public SyntaxException(string msg):base (msg)
        {
        }
    }
}
