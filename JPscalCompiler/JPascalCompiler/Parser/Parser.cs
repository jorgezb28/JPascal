using System;
using System.Collections;
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
            return true;// ojo validar esto
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

            if (IF())
            {
                return true;
            }

            if (PREFOR())
            {
                return true;
            }

            if (PREID())
            {
                return true;
            }

            if (WHILE())
            {
                return true;
            }

            if (REPEAT())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CONST())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CASE())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;

            }
            if (DECLARETYPE())
            {
                return true;
            }

            if (FUNCTIONDECL())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (PROCEDUREDECL())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            return false;
        }

        private bool PROCEDUREDECL()
        {
            if (_currentToken.Type == TokenTypes.Procedure)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (PARAMS())
                    {
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (FUNCTIONBLOCK())
                            {
                                return true;
                            }
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Parameters Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool FUNCTIONDECL()
        {
            if (_currentToken.Type == TokenTypes.Function)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (PARAMS())
                    {
                        if (_currentToken.Type == TokenTypes.PsColon)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (_currentToken.Type == TokenTypes.Id)
                            {
                                _currentToken = _lexer.GetNextToken();
                                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                                {
                                    _currentToken = _lexer.GetNextToken();
                                    if (FUNCTIONBLOCK())
                                    {
                                        return true;
                                    }
                                   _parserSyntaxErrors.Add("Syntax Error.Function body expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                                    return false;
                                }
                                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                                return false;
                            }
                            _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                _currentToken.Row);
                            return false;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Function params expected at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool FUNCTIONBLOCK()
        {
            if (_currentToken.Type == TokenTypes.Begin)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool PARAMS()
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTADECL())
                {
                    if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool LISTADECL()
        {
            if (DECLPARAM())
            {
                if (EXTRADECL())
                {
                    return true;
                }
            }
            return true;

        }

        private bool EXTRADECL()
        {
            if (_currentToken.Type == TokenTypes.PsSentenseEnd)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTADECL())
                {
                    return true;
                }
            }
            return true;
        }

        private bool DECLPARAM()
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                _currentToken = _lexer.GetNextToken();
                if (ListaId())
                {
                    if (_currentToken.Type == TokenTypes.PsColon)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.Id)
                        {
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                    }
                }
            }
           
            if (ListaId())
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Id)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                            _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool DECLARETYPE()
        {
            if (_currentToken.Type == TokenTypes.Type)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.OpEquals)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (TYPE())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        private bool TYPE()
        {
            if (ENUMERATEDTYPE())
            {
                return true;
            }
            if (TYPEDEF())
            {
                return true;
            }
            if (RECORD())
            {
                return true;
            }
            if (ARRAY())
            {
                return true;
            }
            return false;
        }

        private bool ARRAY()
        {
            if (_currentToken.Type == TokenTypes.Array)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsOpenBracket)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (LISTARANGOS())
                    {
                        if (_currentToken.Type == TokenTypes.PsCloseBracket)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (_currentToken.Type == TokenTypes.Of)
                            {
                                _currentToken = _lexer.GetNextToken();
                                if (ARRAYTYPES())
                                {
                                    if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                                    {
                                        _currentToken = _lexer.GetNextToken();
                                        return true;
                                    }
                                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                    return false;

                                }
                                _parserSyntaxErrors.Add("Syntax Error.Array type expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                return false;
                            }
                            _parserSyntaxErrors.Add("Syntax Error.Expected word 'of' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                            return false;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ']' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Array dimention expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol '[' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                return false;
            }
            return false;

        }

        private bool ARRAYTYPES()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (RANGO())
            {
                return true;
            }
            if (ARRAY())
            {
                return true;
            }
            return false;
        }

        private bool RECORD()
        {
            if (_currentToken.Type == TokenTypes.Record)
            {
                _currentToken = _lexer.GetNextToken();
                if (BLOCKRECORD())
                {
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word 'end' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;

                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool LISTAPROPIEDADES()
        {
            if (ListaId())
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (TYPE())
                    {
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Type Expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool BLOCKRECORD()
        {
           if (LISTAPROPIEDADES())
           {
               //if (_currentToken.Type == TokenTypes.PsSentenseEnd)
               //{
               //    _currentToken = _lexer.GetNextToken();
                   if (_currentToken.Type != TokenTypes.End)
                   {
                        if (BLOCKRECORD())
                        {
                            return true;
                        }
                    }
                   else
                   {
                       return true;
                   }
                //}
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                   _currentToken.Row);
                return false;

            }
           return false;
        }

        private bool TYPEDEF()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool ENUMERATEDTYPE()
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                if (ListaId())
                {
                    if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;

            }
            return false;
        }

        private bool CASE()
        {
            if (_currentToken.Type == TokenTypes.Case)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (INDEX_ACCESS())
                    {
                        if (_currentToken.Type == TokenTypes.Of)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (CASELIST())
                            {
                                if (_currentToken.Type == TokenTypes.End)
                                {
                                    _currentToken = _lexer.GetNextToken();
                                    return true;
                                }
                                _parserSyntaxErrors.Add("Syntax Error.Expected word 'end' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                return false;
                            }
                            _parserSyntaxErrors.Add("Syntax Error.Missing case list or else(default) expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                            return false;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected word 'of' at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                        return false;

                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol '[' or '.' at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;

        }

        private bool CASELIST()
        {
            if (CASELITERAL() && _currentToken.Type != TokenTypes.Else)
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (BLOCK())
                    {
                        //if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        //{
                            //_currentToken = _lexer.GetNextToken();
                            if (CASELIST())
                            {
                                return true;
                            }
                        //}
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Sentence expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            if (ELSE())
            {
                if (BLOCK())
                {
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Sentence expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool CASELITERAL()
        {
            if (LISTAEXPR())
            {
                return true;
            }
            if (LISTARANGOS())
            {
                return true;
            }
            return false;
        }

        private bool LISTARANGOS()
        {
            if (RANGO())
            {
                if (LISTARANGOS_OP())
                {
                    return true;
                }
            }
            return false;
        }

        private bool RANGO()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsArrayRange)
                {
                    if (SUBRANGE())
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }


        private bool SUBRANGE()
        {
            //if (Expression())
            //{
                if (_currentToken.Type == TokenTypes.PsArrayRange)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (Expression())
                    {
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol '..' at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                return false;
            //}
            //return false;
        }

        private bool LISTARANGOS_OP()
        {
            if (_currentToken.Type== TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTARANGOS())
                {
                    return true;
                }
            }
            return true;

        }

        private bool INDEX_ACCESS()
        {
            if (_currentToken.Type == TokenTypes.PsOpenBracket)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    if (_currentToken.Type == TokenTypes.PsCloseBracket)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (INDEX_ACCESS())
                        {
                            return true;
                        }
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ']' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                return false;
            }
            if (_currentToken.Type == TokenTypes.PsPointAccesor)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (INDEX_ACCESS())
                    {
                        return true;

                    }
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool CONST()
        {
            if (_currentToken.Type == TokenTypes.Const)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (CONSTDECL())
                    {
                        return true;
                    }
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool CONSTDECL()
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                return false;
            }
            if (_currentToken.Type == TokenTypes.PsColon)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.OpEquals)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (Expression())
                        {
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol: '=' at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool REPEAT()
        {
            if (_currentToken.Type == TokenTypes.Repeat)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS_LOOP())
                {
                    if (_currentToken.Type ==TokenTypes.Until)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (Expression())
                        {
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                               _currentToken.Row);

                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word 'until' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool WHILE()
        {
            if (_currentToken.Type == TokenTypes.While)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    if (_currentToken.Type == TokenTypes.Do)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (LOOPBLOCK())
                        {
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word 'do' at: " + _currentToken.Column +
                                                   " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                               _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool PREFOR()
        {
            if (_currentToken.Type == TokenTypes.For)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (FORBODY())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool FORBODY()
        {
            if (FOR())
            {
                return true;
            }
            if (FORIN())
            {
                return true;
            }
            return false;
        }

        private bool FORIN()
        {
            if (_currentToken.Type == TokenTypes.In)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Do)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (LOOPBLOCK())
                        {
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word: 'do' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool FOR()
        {
            if (_currentToken.Type == TokenTypes.PsAssignment)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    if (_currentToken.Type == TokenTypes.To)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (Expression())
                        {
                            if (_currentToken.Type == TokenTypes.Do)
                            {
                                _currentToken = _lexer.GetNextToken();
                                if (LOOPBLOCK())
                                {
                                    return true;
                                }
                                _parserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                        _currentToken.Column + " , " + _currentToken.Row);
                                return false;
                            }
                            _parserSyntaxErrors.Add("Syntax Error.Expected word 'do' at: " + _currentToken.Column +
                                                    " , " + _currentToken.Row);
                            return false;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                                _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word 'to' at: " + _currentToken.Column + " , " +
                                            _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool LOOPBLOCK()
        {
            if (_currentToken.Type == TokenTypes.Begin)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS_LOOP())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            if (LOOP_S())
            {
                return true;
            }
            return true;

        }

        private bool LS_LOOP()
        {
            if (LOOP_S())
            {
                return LS_LOOP();
            }
            return true;
        }

        private bool LOOP_S()
        {
            if (_currentToken.Type == TokenTypes.Continue)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (_currentToken.Type == TokenTypes.Break)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (S())
            {
                return true;
            }
            return false;
        }

        private bool PREID()
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
                if (IDBODY())
                {
                    return true;
                }
            }
            return false;
        }

        private bool IDBODY()
        {
            if (_currentToken.Type== TokenTypes.PsAssignment)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            if (LLAMARFUNCIONSENTENCIA())
            {
                //_currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool LLAMARFUNCIONSENTENCIA()
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTAEXPR())
                {
                    if (_currentToken.Type==TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected  at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;

        }

        private bool LISTAEXPR()
        {
            if (Expression())
            {
                if (LISTAEXPR_OP())
                {
                    //_currentToken = _lexer.GetNextToken();
                    return true;
                }
            }
            return true;
        }

        private bool LISTAEXPR_OP()
        {
            if (_currentToken.Type == TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTAEXPR())
                {
                    return true;
                }
            }
            return true;

        }

        private bool IF()
        {
            if (_currentToken.Type == TokenTypes.If)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Then)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (BLOCK())
                        {
                            return ELSE();
                        }
                        _parserSyntaxErrors.Add("Syntax Error.'Begin' word or sentence expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word: 'then' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expresion Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool ELSE()
        {
            if (_currentToken.Type == TokenTypes.Else)
            {
                _currentToken = _lexer.GetNextToken();
                if (BLOCK())
                {
                    //_currentToken = _lexer.GetNextToken();
                    return true;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool BLOCK()
        {
            if (_currentToken.Type == TokenTypes.Begin)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                        _parserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            if (S())
            {
                return true;
            }
            return true;
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
                return  ea || rep ;
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
                return em || eap ;
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
                if (X())
                {
                    return true;
                }
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
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression())
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    _parserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                _parserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool X()
        {
            if (LLAMARFUNCIONSENTENCIA())
            {
                return true;
            }
            if (INDEX_ACCESS())
            {
                return true;
            }
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
