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
        public List<string> _parserSyntaxErrors;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            _parserSyntaxErrors = new List<string>();
        }

        public bool Parse()
        {
            _currentToken = _lexer.GetNextToken();
            //LS();
            if (LS()) //el codigo no tuvo errores de compilacion
            {
                return true;
            }

            if (_currentToken.Type != TokenTypes.EOF)
             {
                 _parserSyntaxErrors.Add("Se esperaba EOF");
                 return false;
             }
            return true; //no hubo error de compilacion
        }

        private bool LS()
        {
            if(S())
            {
                LS();
            }
            return false;// ojo validar esto
        }

        private bool S()
        {
            if (Declaracion())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: "+_currentToken.Column+" , "+_currentToken.Row);
                return false;
            }
            return false;
        }

        private bool Declaracion()
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                _currentToken = _lexer.GetNextToken();
                return FactorComunId();    
            }

            //_parserSyntaxErrors.Add("Syntax error. Expected word: 'var' at colum:"+_currentToken.Column+" , row: "+_currentToken.Row);
            return false;
        }

        private bool FactorComunId()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                return Y();
            }

            _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:"+_currentToken.Column+" , "+_currentToken.Row);
            return false;
        }

        private bool Y()
        {
            if (_currentToken.Type ==TokenTypes.PsColon)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    return AsignarValor();
                }
                
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            
            if (IdOpcional())
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Id)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ':' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ':' or ',' at: " + _currentToken.Column + " , " + _currentToken.Row);
            return false;
        }

        private bool AsignarValor()
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                return Expression();
            }
            //_parserSyntaxErrors.Add("Syntax Error.Expected symbol: '=' at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
            return true; //epsilon
        }

        private bool Expression()
        {
            return RelationalExpresion();
        }

        private bool RelationalExpresion()
        {
            var ea = ExpresionAdicion();
            var rep = RelationalExpresionP();
            return ea || rep ;
        }

        private bool RelationalExpresionP()
        {
            if (OpRelational())
            {
                var ea = ExpresionAdicion();
                var rep = RelationalExpresionP();
                return  ea||rep ;
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

        private bool ExpresionAdicion()
        {
            var em = ExpresionMul();
            var eap = ExpresionAdicionP();
            return em || eap;
        }

        private bool ExpresionAdicionP()
        {
            if (OpAdicion())
            {
                var em = ExpresionMul();
                var eap = ExpresionAdicionP();
                return em ||eap ;
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

        private bool ExpresionMul()
        {
            var eu = ExpresionUnary();
            var emp= ExpresionMulP();
            return eu || emp;
        }

        private bool ExpresionMulP()
        {
            if (OpMul())
            {
                var eu = ExpresionUnary();
                var emp =ExpresionMulP();
                return eu || emp;
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

        private bool ExpresionUnary()
        {
            if (_currentToken.Type == TokenTypes.OpNot)
            {
                _currentToken = _lexer.GetNextToken();
                return Factor();
            }
            return Factor();
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

        private bool IdOpcional()
        {
            if (_currentToken.Type == TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                return ListaId();
            }
            return true; //epsilon
        }

        private bool ListaId()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                return IdOpcional();
            }
            _parserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
            return false;
        }
    }

    public class SyntaxException : Exception
    {
        public SyntaxException(string msg):base (msg)
        {
        }
    }
}
