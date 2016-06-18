using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Tree;

namespace JPascalCompiler.Parser
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
        public List<string> ParserSyntaxErrors;
        public List<SentenceNode> SentenceList;
          

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            ParserSyntaxErrors = new List<string>();
            SentenceList = new List<SentenceNode>();
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
                 ParserSyntaxErrors.Add("Se esperaba EOF");
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
                
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: "+_currentToken.Column+" , "+_currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CONST())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CASE())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (PROCEDUREDECL())
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Parameters Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                                   ParserSyntaxErrors.Add("Syntax Error.Function body expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                                    return false;
                                }
                                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                                return false;
                            }
                            ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                _currentToken.Row);
                            return false;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Function params expected at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                    ParserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                    ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                            _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
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
                                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                    return false;

                                }
                                ParserSyntaxErrors.Add("Syntax Error.Array type expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                return false;
                            }
                            ParserSyntaxErrors.Add("Syntax Error.Expected word 'of' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                            return false;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ']' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Array dimention expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol '[' at: " + _currentToken.Column + " , " +
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word 'end' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;

                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                    ParserSyntaxErrors.Add("Syntax Error.Type Expected at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                                ParserSyntaxErrors.Add("Syntax Error.Expected word 'end' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                                return false;
                            }
                            ParserSyntaxErrors.Add("Syntax Error.Missing case list or else(default) expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                            return false;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expected word 'of' at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                        return false;

                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol '[' or '.' at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                  _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Sentence expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ':' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            if (ELSE())
            {
                if (BLOCK())
                {
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Sentence expected at: " + _currentToken.Column + " , " +
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
            var expr = new ExpressionNode();
            //if (Expression())
            //{
                if (_currentToken.Type == TokenTypes.PsArrayRange)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (Expression(expr))
                    {
                        return true;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                        _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol '..' at: " + _currentToken.Column + " , " +
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
            var expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.PsOpenBracket)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    if (_currentToken.Type == TokenTypes.PsCloseBracket)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (INDEX_ACCESS())
                        {
                            return true;
                        }
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ']' at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
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
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
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
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool CONSTDECL()
        {
            var expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
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
                        if (Expression(expr))
                        {
                            return true;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol: '=' at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool REPEAT()
        {
            var expr =new ExpressionNode();
            if (_currentToken.Type == TokenTypes.Repeat)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS_LOOP())
                {
                    if (_currentToken.Type ==TokenTypes.Until)
                    {
                        _currentToken = _lexer.GetNextToken();
                        
                        if (Expression(expr))
                        {
                            return true;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                               _currentToken.Row);

                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word 'until' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool WHILE()
        {
            var expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.While)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    if (_currentToken.Type == TokenTypes.Do)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (LOOPBLOCK())
                        {
                            return true;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word 'do' at: " + _currentToken.Column +
                                                   " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word: 'do' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool FOR()
        {
            var  expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.PsAssignment)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    if (_currentToken.Type == TokenTypes.To)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (Expression(expr))
                        {
                            if (_currentToken.Type == TokenTypes.Do)
                            {
                                _currentToken = _lexer.GetNextToken();
                                if (LOOPBLOCK())
                                {
                                    return true;
                                }
                                ParserSyntaxErrors.Add("Syntax Error.Expected sentence or 'begin' word at: " +
                                                        _currentToken.Column + " , " + _currentToken.Row);
                                return false;
                            }
                            ParserSyntaxErrors.Add("Syntax Error.Expected word 'do' at: " + _currentToken.Column +
                                                    " , " + _currentToken.Row);
                            return false;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
                                                _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word 'to' at: " + _currentToken.Column + " , " +
                                            _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
            var expr = new ExpressionNode();
            if (_currentToken.Type== TokenTypes.PsAssignment)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected  at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return false;

        }

        private bool LISTAEXPR()
        {
            var expr = new ExpressionNode();
            if (Expression(expr))
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
            var expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.If)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Then)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (BLOCK())
                        {
                            return ELSE();
                        }
                        ParserSyntaxErrors.Add("Syntax Error.'Begin' word or sentence expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word: 'then' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expresion Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected word: 'end' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                var declarationNode = new DeclarationNode(_currentToken.Row,_currentToken.Column);
                _currentToken = _lexer.GetNextToken();
                return FactorComunId(declarationNode);    
            }

            //_parserSyntaxErrors.Add("Syntax error. Expected word: 'var' at colum:"+_currentToken.Column+" , row: "+_currentToken.Row);
            return false;
        }

        private bool FactorComunId( DeclarationNode declarationNode)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var idNode = new IdNode(_currentToken.Lexeme);

                //var dec = declarationNode;
                declarationNode.IdsList.Add(idNode);

                //declarationNode = dec;

                _currentToken = _lexer.GetNextToken();
                return Y(declarationNode);
            }
            ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:"+_currentToken.Column+" , "+_currentToken.Row);
            return false;
        }

        private bool Y(DeclarationNode declarationNode)
        {
            if (_currentToken.Type ==TokenTypes.PsColon)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var typeDecl = _lexer.getTokenType(_currentToken.Lexeme);
                    declarationNode.IdType = typeDecl;

                    _currentToken = _lexer.GetNextToken();
                    return AsignarValor(declarationNode);
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
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
                    ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ':' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ':' or ',' at: " + _currentToken.Column + " , " + _currentToken.Row);
            return false;
        }

        private bool AsignarValor(DeclarationNode declarationNode)
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                var expresionDecl = new ExpressionNode();
                declarationNode.AssignedValue = expresionDecl;
                
                //mandar decl to expresion
                return Expression(expresionDecl);
            }
            //_parserSyntaxErrors.Add("Syntax Error.Expected symbol: '=' at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
            return true; //epsilon
        }

        private bool Expression(ExpressionNode expresionDecl)
        {
            var expr = new ExpressionNode();
            return RelationalExpresion(expr);
        }

        private bool RelationalExpresion(ExpressionNode expr)
        {
            var leftOperand = new ExpressionNode();
            var ea = ExpresionAdicion(leftOperand);


            var rep = RelationalExpresionP(leftOperand,expr);
            return ea || rep ;
        }

        private bool RelationalExpresionP(ExpressionNode leftOperand, ExpressionNode expr)
        {
            var binaryOperation = new BinaryOperationNode();

            if (OpRelational(binaryOperation))
            {
                var rigthOperand = new ExpressionNode();
                var ea = ExpresionAdicion(rigthOperand);

                binaryOperation.LeftOperand = leftOperand;
                binaryOperation.RigthOperand = rigthOperand;

                expr = binaryOperation;

                var rep = RelationalExpresionP(binaryOperation,expr);
                return  ea || rep ;
            }
            return false;
        }

        private bool OpRelational(ExpressionNode binaryOperation)
        {
            if (_currentToken.Type == TokenTypes.OpLessThan)
            {
                var lesThanNode = new LessThanNode();
                binaryOperation = lesThanNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.OpGreaterThan)
            {
                var greaterThanNode = new GreaterThanNode();
                binaryOperation = greaterThanNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpLessThanOrEquals)
            {
                var lessThanEqualsNode = new LessThanOrEqualsNode();
                binaryOperation = lessThanEqualsNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpGreaterThanOrEquals)
            {
                var greaterThanEqual = new GreaterThanOrEqualsNode();
                binaryOperation = greaterThanEqual;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpNotEquals)
            {
                var notEqualsNode = new NotEqualsNode();
                binaryOperation = notEqualsNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                var equalsNode = new EqualsNode();
                binaryOperation = equalsNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            binaryOperation = null;
            return false;
            
        }

        private bool ExpresionAdicion(ExpressionNode expr)
        {
            var leftOperand = new ExpressionNode();
            var em = ExpresionMul(leftOperand);


            var eap = ExpresionAdicionP(leftOperand,expr);
            return em || eap;
        }

        private bool ExpresionAdicionP(ExpressionNode leftOperand,ExpressionNode expr)
        {
            var binaryOperationNode = new BinaryOperationNode();
            if (OpAdicion(binaryOperationNode))
            {
                var rigthOperand = new ExpressionNode();
                var em = ExpresionMul(rigthOperand);

                binaryOperationNode.LeftOperand = leftOperand;
                binaryOperationNode.RigthOperand = rigthOperand;

                expr = binaryOperationNode;

                var eap = ExpresionAdicionP(binaryOperationNode,expr);
                return em || eap ;
            }
            return false;
        }

        private bool OpAdicion(ExpressionNode binaryOperationNode)
        {
            if (_currentToken.Type == TokenTypes.OpSum)
            {
                var sumNode = new SumNode();
                binaryOperationNode = sumNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpSub)
            {
                var subNode = new SubstractionNode();
                binaryOperationNode = subNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpOr)
            {
                var orNode = new OrNode();
                binaryOperationNode = orNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            binaryOperationNode = null;
            return false;
        }

        private bool ExpresionMul(ExpressionNode expr)
        {
            var leftOperand = new UnaryNode();
            var eu = ExpresionUnary(leftOperand);
            
            var emp= ExpresionMulP(leftOperand,expr);
            return eu || emp;
        }

        private bool ExpresionMulP(ExpressionNode leftOperand, ExpressionNode expr)
        {
            var binaryOperation = new BinaryOperationNode();
            if (OpMul( binaryOperation))
            {
                var rigthOperand = new UnaryNode();
                var eu = ExpresionUnary(rigthOperand);


                binaryOperation.LeftOperand = leftOperand;
                binaryOperation.RigthOperand = rigthOperand;

                expr = binaryOperation;

                var emp =ExpresionMulP(binaryOperation, expr);
                return eu || emp;
            }
            return false;
        }

        private bool OpMul( ExpressionNode binaryOperation)
        {

            if (_currentToken.Type == TokenTypes.OpMult)
            {
                var multNode = new MultiplicationNode();
                binaryOperation = multNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.OpDivr)
            {
                var divRealNode = new DivisionRealNode();
                binaryOperation = divRealNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if(_currentToken.Type == TokenTypes.OpDiv)
            {
                var divNode = new DivisionNode();
                binaryOperation = divNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.OpMod)
            {
                var modNode = new ModuleNode();
                binaryOperation = modNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if(_currentToken.Type == TokenTypes.OpAnd)
            {
                var andNode = new AndNode();
                binaryOperation = andNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            binaryOperation = null;
            return false;
        }

        private bool ExpresionUnary(ExpressionNode expr)
        {
            if (_currentToken.Type == TokenTypes.OpNot)
            {
                var unaryNode = new UnaryNode();
                var unaryOperand = new ExpressionNode();
                unaryNode.UnaryOperand = unaryOperand;

                _currentToken = _lexer.GetNextToken();
                return Factor(unaryOperand);
            }
            return Factor(expr);
        }

        private bool Factor(ExpressionNode expr)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var name= _currentToken.Lexeme;
                var idNode = new IdNode(name);
                expr = idNode;
                _currentToken = _lexer.GetNextToken();
                if (X())
                {
                    return true;
                }
                return true;
            }

            if (_currentToken.Type ==TokenTypes.Integer)
            {
                var value = int.Parse(_currentToken.Lexeme);
                var intNode = new NumberNode(value);
                expr = intNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.String)
            {
                var str = _currentToken.Lexeme;
                var strNode = new StringNode(str);
                expr = strNode;

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.Boolean)
            {
                var boolvalueType = _lexer.getTokenType(_currentToken.Lexeme);
                var boolNode = new BooleanNode {Value = boolvalueType == TokenTypes.True};
                expr = boolNode;

                return true;
            }
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();

                //pendiente nodo expresion en este punto
                if (Expression(expr))
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        return true;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expected symbol ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            expr = null;
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
            ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
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
