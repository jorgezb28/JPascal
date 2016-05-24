using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using JPascalCompiler.LexerFolder;

namespace JPascalCompiler.Parser
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        public Parser(Lexer lexer)
        {
            this._lexer = lexer;
        }

        public void Parse()
        {
            _currentToken = _lexer.GetNextToken();
            LS();

            if (_currentToken.Type != TokenTypes.EOF)
             {
                throw new SyntaxException("Se esperaba EOF");
            }
        }

        private void LS()
        {
            S();
            LS();
        }

        private void S()
        {
            Declaracion();
        }

        private void Declaracion()
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                _currentToken = _lexer.GetNextToken();
                FactorComunId();
            }
        }

        private void FactorComunId()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                Y();
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
            }

            IdOpcional();
        }

        private void AsignarValor()
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                Expression();
            }
        }

        private void Expression()
        {
            RelationalExpresion();
        }

        private void RelationalExpresion()
        {
            ExpresionAdicion();
            //RelationalExpresionP();
        }

        private void ExpresionAdicion()
        {
            ExpresionMul();
            //ExpresionAdicionP();
        }

        private void ExpresionMul()
        {
            ExpresionUnary();
            //ExpresionMulP();
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
            return true;
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
