﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Tree;
using NUnit.Framework.Constraints;

namespace JPascalCompiler.Parser
{
   public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
        public List<string> ParserSyntaxErrors;
        public List<SentenceNode> SentenceList;
       internal bool IsBlockSection;
       internal bool IsNestedSentence;
          

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            IsBlockSection = false;
            IsNestedSentence = false;
            ParserSyntaxErrors = new List<string>();
            SentenceList = new List<SentenceNode>();
        }

        public bool Parse()
        {
            _currentToken = _lexer.GetNextToken();
            //LS();
            var sentence = new SentenceNode();
            if (LS(sentence)) //el codigo no tuvo errores de compilacion
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

        private bool LS(SentenceNode sentence)
        {
            //var svalue = S();
            if(S(sentence))
            {
                LS(sentence);
            }
            return true;// ojo validar esto
        }

        private bool S(SentenceNode sentenceExpresion)
        {
            
            
            if ( Declaracion(sentenceExpresion))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    //var declSentenceNode = (DeclarationNode) sentence;
                    if (IsBlockSection || IsNestedSentence)
                    {
                        sentenceExpresion.Sentence.Add(sentenceExpresion.Sentence[0]);
                    }
                    else
                    {
                         SentenceList.Add(sentenceExpresion.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: "+_currentToken.Column+" , "+_currentToken.Row);
                return false;
            }

            if (IF(sentenceExpresion))
            {
                if (IsBlockSection)
                    {
                        sentenceExpresion.Sentence.Add(sentenceExpresion.Sentence[0]);
                    }
                    else
                    {
                         SentenceList.Add(sentenceExpresion.Sentence[0]);
                    }
                return true;
            }

            if (WRITELN(sentenceExpresion))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (IsBlockSection)
                    {
                        sentenceExpresion.Sentence.Add(sentenceExpresion.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(sentenceExpresion.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
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

       private bool WRITELN(SentenceNode sentence)
       {
           if (_currentToken.Type == TokenTypes.Writeln)
           {
               var column = _currentToken.Column;
               var row = _currentToken.Row;
               _currentToken = _lexer.GetNextToken();
               if (_currentToken.Type == TokenTypes.PsOpenParentesis)
               {
                   _currentToken = _lexer.GetNextToken();
                    var writelnExpr = new ExpressionNode();
                   if (Expression(writelnExpr))
                   {
                       if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                       {
                           _currentToken = _lexer.GetNextToken();
                            sentence.Sentence.Add(new WritelnNode(row,column,writelnExpr) );
                           return true;
                       }
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +_currentToken.Row);
                   return false;
               }
               ParserSyntaxErrors.Add("Syntax Error.Expected symbol: '(' at: " + _currentToken.Column + " , " + _currentToken.Row);
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
                if (LS(new SentenceNode()))
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
                if (ListaId(new SentenceNode()))
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
           
            if (ListaId(new SentenceNode()))
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
            if (ListaId(new SentenceNode()))
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
                if (ListaId(new SentenceNode()))
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
            var expr = new ExpressionNode();
            if (_currentToken.Type == TokenTypes.Case)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (INDEX_ACCESS(expr))
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
                    if (BLOCK(new SentenceNode()))
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
            if (ELSE(new SentenceNode()))
            {
                if (BLOCK(new SentenceNode()))
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

        private bool INDEX_ACCESS(ExpressionNode expr)
        {
           
            if (_currentToken.Type == TokenTypes.PsOpenBracket)
            {
                _currentToken = _lexer.GetNextToken();
                if (Expression(expr))
                {
                    if (_currentToken.Type == TokenTypes.PsCloseBracket)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (INDEX_ACCESS(expr))
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
                    if (INDEX_ACCESS(expr))
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
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (S(new SentenceNode()))
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

        private bool IF(SentenceNode sentence)
        {
            if (_currentToken.Type == TokenTypes.If)
            {
                var ifColumn = _currentToken.Column;
                var ifRow = _currentToken.Row;
                _currentToken = _lexer.GetNextToken();
                var ifConditionExpression = new ExpressionNode();
                if (Expression(ifConditionExpression))
                {
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Then)
                    {
                        _currentToken = _lexer.GetNextToken();
                        var ifBlockSentences = new SentenceNode();
                        if (BLOCK(ifBlockSentences))
                        {
                            var elseBlockSentences= new SentenceNode();
                            if (ELSE(elseBlockSentences))
                            {
                                sentence.Sentence.Add(new IfNode(ifRow, ifColumn, ifConditionExpression, ifBlockSentences, elseBlockSentences));
                                return true;
                            }
                            return true;
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

        private bool ELSE(SentenceNode elseBlockSentences)
        {
            if (_currentToken.Type == TokenTypes.Else)
            {
                IsNestedSentence = true;
                _currentToken = _lexer.GetNextToken();
                if (BLOCK(elseBlockSentences))
                {
                    //_currentToken = _lexer.GetNextToken();
                    //IsBlockSection = false;
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected sentence at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool BLOCK(SentenceNode blockSentences)
        {
            if (_currentToken.Type == TokenTypes.Begin)
            {
                IsBlockSection = true;
                _currentToken = _lexer.GetNextToken();
                if (LS(blockSentences))
                {
                    IsBlockSection = false;
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
            if (S(blockSentences))
            {
                return true;
            }
            return true;
        }

        private bool Declaracion(SentenceNode sentence)
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                var declarationNode = new DeclarationNode(_currentToken.Row, _currentToken.Column)
                {
                    RowSentence = _currentToken.Row,
                    ColumnSentence = _currentToken.Column
                };
                sentence.Sentence.Add(declarationNode);

                _currentToken = _lexer.GetNextToken();

                return FactorComunId(sentence);    
            }

            //_parserSyntaxErrors.Add("Syntax error. Expected word: 'var' at colum:"+_currentToken.Column+" , row: "+_currentToken.Row);
            return false;
            //return  new Tuple<bool, SentenceNode>(false,null);
        }

        private bool FactorComunId( SentenceNode declarationNode)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var idNode = new IdNode(_currentToken.Lexeme);

                var dec = (DeclarationNode)declarationNode.Sentence[0];
                dec.IdsList.Add(idNode);

                //declarationNode.Sentence[0] = dec;

                _currentToken = _lexer.GetNextToken();
                return Y(declarationNode);
            }
            ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:"+_currentToken.Column+" , "+_currentToken.Row);
            return false;
            //return new Tuple<bool, SentenceNode>(false,null);
        }

        private bool Y(SentenceNode declarationNode)
        {
            if (_currentToken.Type ==TokenTypes.PsColon)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var typeDecl = _lexer.getTokenType(_currentToken.Lexeme);
                    var dec = (DeclarationNode)declarationNode.Sentence[0];
                    dec.IdType = typeDecl;

                    _currentToken = _lexer.GetNextToken();
                    return AsignarValor(declarationNode);
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            
            if (IdOpcional(declarationNode))
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Id)
                    {
                        var typeDecl = _lexer.getTokenType(_currentToken.Lexeme);
                        var dec = (DeclarationNode)declarationNode.Sentence[0];
                        dec.IdType = typeDecl;

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

        private bool AsignarValor(SentenceNode declarationNode)
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                _currentToken = _lexer.GetNextToken();
                var expresionDecl = new ExpressionNode();
                var dec = (DeclarationNode)declarationNode.Sentence[0];
                dec.AssignedValue = expresionDecl;
                
                //mandar decl to expresion
                return Expression(expresionDecl);
            }
            //_parserSyntaxErrors.Add("Syntax Error.Expected symbol: '=' at column, row: " + _currentToken.Column + " , " + _currentToken.Row);
            return true; //epsilon
            //return new Tuple<bool, SentenceNode>(true,null);
        }

        private bool Expression(ExpressionNode expr)
        {
            //var expr = new ExpressionNode();
            return RelationalExpresion(expr);
        }

        private bool RelationalExpresion(ExpressionNode expr)
        {
            var leftReldOperand = new ExpressionNode();
            var ea = ExpresionAdicion(leftReldOperand);


            var rep = RelationalExpresionP(leftReldOperand,expr);
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

                expr.Expressions.Add(binaryOperation);

                var rep = RelationalExpresionP(binaryOperation,expr);
                return  ea || rep ;
            }
            if (leftOperand.Expressions.Any())
            {
                expr.Expressions.Add(leftOperand.Expressions[0]);
            }
            
            return false;
        }

        private bool OpRelational(BinaryOperationNode binaryOperation)
        {
            if (_currentToken.Type == TokenTypes.OpLessThan)
            {
               binaryOperation.TypeNode = typeof(LessThanNode);
               
                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.OpGreaterThan)
            {
                binaryOperation.TypeNode = typeof(GreaterThanNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpLessThanOrEquals)
            {
                binaryOperation.TypeNode = typeof(LessThanOrEqualsNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpGreaterThanOrEquals)
            {
                binaryOperation.TypeNode = typeof(GreaterThanOrEqualsNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpNotEquals)
            {
                binaryOperation.TypeNode = typeof(NotEqualsNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                binaryOperation.TypeNode = typeof(EqualsNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            return false;
            
        }

        private bool ExpresionAdicion(ExpressionNode addExpr)
        {
            var leftMultOperand = new ExpressionNode();
            var em = ExpresionMul(leftMultOperand);


            var eap = ExpresionAdicionP(leftMultOperand,addExpr);
            return em || eap;
        }

        private bool ExpresionAdicionP(ExpressionNode leftMultOperand, ExpressionNode leftAddExpr)
        {
            var binaryOperationNode = new BinaryOperationNode();
            if (OpAdicion(binaryOperationNode))
            {
                var rigthAddOperand = new ExpressionNode();
                var em = ExpresionMul(rigthAddOperand);

                binaryOperationNode.LeftOperand = leftMultOperand.Expressions[0];
                binaryOperationNode.RigthOperand = rigthAddOperand.Expressions[0];

                leftAddExpr.Expressions.Add(binaryOperationNode);

                var eap = ExpresionAdicionP(binaryOperationNode,leftAddExpr);
                return em || eap ;
            }
            if (leftMultOperand.Expressions.Any())
            {
                leftAddExpr.Expressions.Add(leftMultOperand.Expressions[0]);
            }
            return false;
        }

       private void Swap<T>(ref T source, ref T destiny)
       {
            T temp = source;
            source = destiny;
            destiny = temp;
        }

       private bool OpAdicion(BinaryOperationNode binaryOperationNode)
        {
            if (_currentToken.Type == TokenTypes.OpSum)
            {
                //var sumNode = new SumNode {TypeNode = GetType()};
                binaryOperationNode.TypeNode = typeof(SumNode);

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

        private bool ExpresionMul(ExpressionNode multExpr)
        {
            var leftUnaryOperand = new ExpressionNode();
            var eu = ExpresionUnary(leftUnaryOperand);
            
            var emp= ExpresionMulP(leftUnaryOperand,multExpr);
            return eu || emp;
        }

        private bool ExpresionMulP(ExpressionNode leftUnaryOperand, ExpressionNode multExpr)
        {
            var binaryOperation = new BinaryOperationNode();
            if (OpMul( binaryOperation))
            {
                var rigthMultOperand = new ExpressionNode();
                var eu = ExpresionUnary(rigthMultOperand);


                binaryOperation.LeftOperand = leftUnaryOperand.Expressions[0];
                binaryOperation.RigthOperand = rigthMultOperand.Expressions[0];

                multExpr.Expressions.Add( binaryOperation);

                var emp =ExpresionMulP(binaryOperation, multExpr);
                return eu || emp;
            }
            if (leftUnaryOperand.Expressions.Any())
            {
                multExpr.Expressions.Add(leftUnaryOperand.Expressions[0]);
            }
            
            return false;
        }

        private bool OpMul( BinaryOperationNode binaryOperation)
        {

            if (_currentToken.Type == TokenTypes.OpMult)
            {
                //var multNode = new MultiplicationNode();
                binaryOperation.TypeNode = typeof(MultiplicationNode);

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
            return false;
        }

        private bool ExpresionUnary(ExpressionNode unaryexpr)
        {
            if (_currentToken.Type == TokenTypes.OpNot)
            {
                var unaryNode = new UnaryNode();
                var unaryOperand = new ExpressionNode();
                unaryNode.UnaryOperand = unaryOperand;

                _currentToken = _lexer.GetNextToken();
                return Factor(unaryOperand);
            }
            return Factor(unaryexpr);
        }

        private bool Factor(ExpressionNode expr)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                int intval;
                if (int.TryParse(_currentToken.Lexeme,out intval))
                {
                    var intNode = new NumberNode(intval);
                    expr.Expressions.Add(intNode);

                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                if (_currentToken.Lexeme.StartsWith("\'") && _currentToken.Lexeme.EndsWith("\'") 
                    && _currentToken.Lexeme.Length>1)
                {
                    var str = _currentToken.Lexeme;
                    var strNode = new StringNode(str);
                    expr.Expressions.Add(strNode);

                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                if (_currentToken.Lexeme.StartsWith("\'") && _currentToken.Lexeme.EndsWith("\'"))
                {
                    var ch = char.Parse(_currentToken.Lexeme);
                    var charNode = new CharNode(ch);
                    expr.Expressions.Add(charNode);

                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                if (_currentToken.Lexeme.ToLower() == "true" || _currentToken.Lexeme.ToLower()=="false" )
                {
                    var boolvalueType = _lexer.getTokenType(_currentToken.Lexeme);
                    var boolNode = new BooleanNode { Value = boolvalueType == TokenTypes.True };
                    expr.Expressions.Add(boolNode);

                    return true;
                }

                float floatValue;
                if (float.TryParse(_currentToken.Lexeme, out floatValue))
                {
                    var floatNode = new FloatNode(floatValue);
                    expr.Expressions.Add(floatNode);

                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                var name= _currentToken.Lexeme;
                var idNode = new IdNode(name);
                expr.Expressions.Add(idNode);
                _currentToken = _lexer.GetNextToken();
                if (X(expr))
                {
                    return true;
                }
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

        private bool X(ExpressionNode expr)
        {
            if (LLAMARFUNCIONSENTENCIA())
            {
                return true;
            }
            if (INDEX_ACCESS(expr))
            {
                return true;
            }
            return false;
        }

        private bool IdOpcional(SentenceNode declarationNode)
        {
            if (_currentToken.Type == TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                return ListaId(declarationNode);
            }
            return true; //epsilon
            //return new Tuple<bool, SentenceNode>(true,declarationNode);
        }

        private bool ListaId(SentenceNode sentence)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var idnode = new IdNode(_currentToken.Lexeme);
                if (sentence.Sentence.Any())
                {
                    var dec = (DeclarationNode)sentence.Sentence[0];
                    dec.IdsList.Add(idnode);
                }

                _currentToken = _lexer.GetNextToken();
                return IdOpcional(sentence);
            }
            ParserSyntaxErrors.Add("Syntax Error.Identifier Expected, at:" + _currentToken.Column + " , " + _currentToken.Row);
            return false;
            //return  new Tuple<bool, SentenceNode>(false,null);
        }
    }

        public class SyntaxException : Exception
    {
        public SyntaxException(string msg):base (msg)
        {
        }
    }
}
