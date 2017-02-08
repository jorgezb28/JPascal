using System;
using System.CodeDom;
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
       internal bool IsFunctionCallExpression;
       public bool IsfunOrProc;
       private string _rootForIdex = string.Empty;
          

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
            IsBlockSection = false;
            IsNestedSentence = false;
            IsfunOrProc = false;
            IsFunctionCallExpression = false;
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
            if (_currentToken.Type == TokenTypes.BeginPascalCode)
            {
                _currentToken = _lexer.GetNextToken();
                LS(sentence);
            }
            if(S(sentence))
            {
                LS(sentence);
            }
            return true;// ojo validar esto
        }

        private bool S(SentenceNode sentenceExpresion)
        {
            var newSentece = new SentenceNode();

            if (Declaracion(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    //var declSentenceNode = (DeclarationNode) sentence;
                    if (IsBlockSection || IsNestedSentence)
                    {
                        //IsNestedSentence = false;
                        sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(newSentece.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: "+_currentToken.Column+" , "+_currentToken.Row);
                return false;
            }

            if (IF(newSentece))
            {
                if (IsBlockSection || IsNestedSentence)
                {
                    sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                }
                else
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                }
                //IsNestedSentence = false;
                return true;
            }

            if (WRITELN(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (IsBlockSection || IsNestedSentence)
                    {
                        //IsNestedSentence = false;
                        sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(newSentece.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }

                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (PREFOR(newSentece))
            {
                if (IsBlockSection || IsNestedSentence)
                {
                    //IsNestedSentence = false;
                    sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                }
                else
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                    //IsNestedSentence = false;
                    _rootForIdex = string.Empty;
                }

                //_currentToken = _lexer.GetNextToken();
                return true;
            }

            if (PREID(newSentece))
            {
                if (IsBlockSection || IsNestedSentence)
                {
                    sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                }
                else
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                    _rootForIdex = string.Empty;
                }
                //IsNestedSentence = false;
                return true;
            }

            if (WHILE(newSentece))
            {
                if (IsBlockSection || IsNestedSentence)
                {
                    //IsNestedSentence = false;
                    sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                }
                else
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                    IsNestedSentence = false;
                }
                return true;
            }

            if (REPEAT(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (IsBlockSection || IsNestedSentence)
                    {
                        //IsNestedSentence = false;
                        sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(newSentece.Sentence[0]);
                        IsNestedSentence = false;
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CONST(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (IsBlockSection || IsNestedSentence)
                    {
                        //IsNestedSentence = false;
                        sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(newSentece.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (CASE(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (IsBlockSection || IsNestedSentence)
                    {
                        //IsNestedSentence = false;
                        sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                    }
                    else
                    {
                        SentenceList.Add(newSentece.Sentence[0]);
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;

            }
            if (DECLARETYPE(newSentece))
            {
                if (IsBlockSection || IsNestedSentence)
                {
                    //IsNestedSentence = false;
                    sentenceExpresion.Sentence.Add(newSentece.Sentence[0]);
                }
                else
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                }
                return true;
            }

            if (FUNCTIONDECL(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                    _currentToken = _lexer.GetNextToken();
                    IsfunOrProc = false;
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (PROCEDUREDECL(newSentece))
            {
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    SentenceList.Add(newSentece.Sentence[0]);
                    _currentToken = _lexer.GetNextToken();
                    IsfunOrProc = false;
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            //IsNestedSentence = false;
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
                   Expression(writelnExpr);
                   //if (Expression(writelnExpr))
                   //{
                       if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                       {
                           _currentToken = _lexer.GetNextToken();
                           if (writelnExpr.Expressions.Any())
                           {
                               sentence.Sentence.Add(new WritelnNode(row, column, writelnExpr.Expressions[0]));
                               return true;
                           }
                           sentence.Sentence.Add(new WritelnNode(row, column, new ExpressionNode())); 
                           return true;
                       }
                       if (_currentToken.Type == TokenTypes.PsComa)
                       {
                           _currentToken = _lexer.GetNextToken();
                           var optionalId = new ExpressionNode();
                           if (Expression(optionalId))
                           {
                               if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                               {
                                   _currentToken = _lexer.GetNextToken();
                                   sentence.Sentence.Add(new WritelnNode(row, column, writelnExpr.Expressions[0],optionalId.Expressions[0]));
                                   return true;
                               }
                               ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ')' at: " + _currentToken.Column + " , " + _currentToken.Row);
                               return false;
                           }
                       }

                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol: ')' or ',' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                   // }
                   // ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " +_currentToken.Row);
                   //return false;
               }
               ParserSyntaxErrors.Add("Syntax Error.Expected symbol: '(' at: " + _currentToken.Column + " , " + _currentToken.Row);
               return false;
           }
           return false;
       }

       private bool PROCEDUREDECL(SentenceNode newProcedureSentece)
        {
           
            if (_currentToken.Type == TokenTypes.Procedure)
            {
                IsfunOrProc = true;
                newProcedureSentece.ColumnSentence = _currentToken.Column;
                newProcedureSentece.RowSentence = _currentToken.Row;

                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var procedurenName = new IdNode(_currentToken.Lexeme);
                    //Expression(procedurenName);
                    _currentToken = _lexer.GetNextToken();
                    var procedureParameters = new List<ExpressionNode>();
                    if (PARAMS(procedureParameters))
                    {
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            var procedureBlockSentence = new SentenceNode();
                            if (FUNCTIONBLOCK(procedureBlockSentence))
                            {
                                IsNestedSentence = false;
                                var procedurefunctionDeclaration =
                                    new ProcedureDeclarationNode(procedurenName, procedureParameters,
                                        procedureBlockSentence.Sentence);

                                newProcedureSentece.Sentence.Add(procedurefunctionDeclaration);
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

        private bool FUNCTIONDECL(SentenceNode newFunctionSentece)
        {
            if (_currentToken.Type == TokenTypes.Function)
            {
                IsfunOrProc = true;
                newFunctionSentece.ColumnSentence = _currentToken.Column;
                newFunctionSentece.RowSentence = _currentToken.Row;
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var functionName = new IdNode(_currentToken.Lexeme);
                    //Expression(functionName);
                    _currentToken = _lexer.GetNextToken();
                    var functionParameters = new List<ExpressionNode>();
                    if (PARAMS(functionParameters))
                    {
                        if (_currentToken.Type == TokenTypes.PsColon)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (_currentToken.Type == TokenTypes.Id)
                            {
                                var returnValue = new ExpressionNode();
                                Expression(returnValue);
                                //_currentToken = _lexer.GetNextToken();
                                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                                {
                                    _currentToken = _lexer.GetNextToken();
                                    var functionBlockSentence = new SentenceNode();
                                    if (FUNCTIONBLOCK(functionBlockSentence))
                                    {
                                        IsNestedSentence = false;
                                        var functionDeclaration =
                                            new FunctionDeclarationNode(functionName, functionParameters,
                                                returnValue.Expressions[0], functionBlockSentence.Sentence);

                                        newFunctionSentece.Sentence.Add(functionDeclaration);
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

        private bool FUNCTIONBLOCK(SentenceNode functionBlockSentence)
        {
            IsNestedSentence = true;
            if (_currentToken.Type == TokenTypes.Begin)
            {
                _currentToken = _lexer.GetNextToken();
                if (LS(functionBlockSentence))
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

        private bool PARAMS(List<ExpressionNode> functionParameters)
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTADECL(functionParameters))
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

        private bool LISTADECL(List<ExpressionNode> functionParameters)
        {
            if (DECLPARAM(functionParameters))
            {
                if (EXTRADECL(functionParameters))
                {
                    return true;
                }
            }
            return true;

        }

        private bool EXTRADECL(List<ExpressionNode> functionParameters)
        {
            if (_currentToken.Type == TokenTypes.PsSentenseEnd)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTADECL(functionParameters))
                {
                    return true;
                }
            }
            return true;
        }

        private bool DECLPARAM(List<ExpressionNode> functionParameters)
        {
            if (_currentToken.Type == TokenTypes.Var)
            {
                //var referenceParam = new ReferenceParameterExpression();
                _currentToken = _lexer.GetNextToken();

                var paramsIds = new ParametersIds();
                if (ListaId(paramsIds))
                {
                    if (_currentToken.Type == TokenTypes.PsColon)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.Id)
                        {
                            var parameterType = new ExpressionNode();
                            Expression(parameterType);

                            functionParameters.Add(new ParameterExpressionByReference(paramsIds.ParametersIdsExpressions,parameterType.Expressions[0]) );
                            //_currentToken = _lexer.GetNextToken();
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

        private bool DECLARETYPE(SentenceNode newSentece)
        {
            if (_currentToken.Type == TokenTypes.Type)
            {
                newSentece.RowSentence = _currentToken.Row;
                newSentece.ColumnSentence = _currentToken.Column;
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var typeId = new IdNode(_currentToken.Lexeme);
                    var typeIds = new List<IdNode>();
                    typeIds.Add(typeId);

                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.OpEquals)
                    {

                        _currentToken = _lexer.GetNextToken();
                        if (TYPE(typeIds, newSentece))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        private bool TYPE(List<IdNode> typesIds, SentenceNode newSentece)
        {
            if (ENUMERATEDTYPE(typesIds,newSentece))
            {
                return true;
            }
            if (TYPEDEF(typesIds,newSentece))
            {
                return true;
            }
            if (RECORD(typesIds,newSentece))
            {
                return true;
            }
            if (ARRAY(typesIds,newSentece))
            {
                return true;
            }
            return false;
        }

        private bool ARRAY(List<IdNode> typesIds, SentenceNode newArraySentece)
        {
            if (_currentToken.Type == TokenTypes.Array)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsOpenBracket)
                {
                    _currentToken = _lexer.GetNextToken();
                    var rangeArray = new List<RangeExpression>();
                    if (LISTARANGOS(rangeArray))
                    {
                        if (_currentToken.Type == TokenTypes.PsCloseBracket)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (_currentToken.Type == TokenTypes.Of)
                            {
                                _currentToken = _lexer.GetNextToken();
                                var arrayType = _lexer.getTokenType(_currentToken.Lexeme);
                                if (ARRAYTYPES(arrayType))
                                {
                                    if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                                    {
                                        
                                        if (newArraySentece is RecordPropertyNode)
                                        {
                                            var recordPropertyArray = (RecordPropertyNode)newArraySentece;
                                            var arrayExpression = new ArrayExpression(rangeArray,arrayType);
                                            recordPropertyArray.RecordPropertyType = arrayExpression;
                                        }
                                        else
                                        {
                                            var arrayNode = new ArrayNode(typesIds, rangeArray, arrayType);
                                            newArraySentece.Sentence.Add(arrayNode);
                                        }

                                       
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

        private bool ARRAYTYPES(TokenTypes arrayType)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                arrayType = _lexer.getTokenType(_currentToken.Lexeme);
                _currentToken = _lexer.GetNextToken();
                return true;
            }
            //if (RANGO(new RangeExpression()))
            //{
            //    return true;
            //}
            //if (ARRAY())
            //{
            //    return true;
            //}
            return false;
        }

        private bool RECORD(List<IdNode> typeId, SentenceNode recordSentence)
        {
            if (_currentToken.Type == TokenTypes.Record)
            {
                _currentToken = _lexer.GetNextToken();
                var recordProperties = new List<RecordPropertyNode>();
                if (BLOCKRECORD(recordProperties))
                {
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            var recordNode = new RecordNode(typeId[0], recordProperties);
                            recordSentence.Sentence.Add(recordNode);
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

        private bool LISTAPROPIEDADES(RecordPropertyNode recordProperty)
        {
            if (ListaId(recordProperty))
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                {
                    _currentToken = _lexer.GetNextToken();
                    if (TYPE(recordProperty.RecordPropertyIds, recordProperty))
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

        private bool BLOCKRECORD(List<RecordPropertyNode> recordPropertiesList)
        {
           var newRecordProperty = new RecordPropertyNode();
           if (LISTAPROPIEDADES(newRecordProperty))
           {
               //if (_currentToken.Type == TokenTypes.PsSentenseEnd)
               //{
               //    _currentToken = _lexer.GetNextToken();
               recordPropertiesList.Add(newRecordProperty);
                if (_currentToken.Type != TokenTypes.End)
                {
                       
                    if (BLOCKRECORD(recordPropertiesList))
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

        private bool TYPEDEF(List<IdNode> typeId, SentenceNode newSentece)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var typeDefinitionType = new ExpressionNode();
                Expression(typeDefinitionType);
                //_currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    if (newSentece is RecordPropertyNode)
                    {
                        var recotdSenence = (RecordPropertyNode) newSentece;
                        recotdSenence.RecordPropertyType = typeDefinitionType.Expressions[0];
                    }
                    else
                    {
                        var typeDefinitionSentence = new TypeDefinitionNode(typeId, typeDefinitionType);
                        newSentece.Sentence.Add(typeDefinitionSentence);    
                    }
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " +
                                 _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool ENUMERATEDTYPE(List<IdNode> typesId, SentenceNode newSentece)
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                var enumConstants = new EnumeratorConstants();
                _currentToken = _lexer.GetNextToken();
                if (ListaId(enumConstants))
                {
                    if (_currentToken.Type == TokenTypes.PsCloseParentesis)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            var enumerationSentence = new EnumerationNode(typesId[0],enumConstants.EnumeratorConstantsExpressions);
                            newSentece.Sentence.Add(enumerationSentence);
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

        private bool CASE(SentenceNode CaseSentece)
        {
            
            if (_currentToken.Type == TokenTypes.Case)
            {
                var caseNode = new CaseNode(_currentToken.Row, _currentToken.Column);
                if (IsNestedSentence == false)
                {
                    caseNode.IsFirstSentence = true;
                }

                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var caseExpr = new ExpressionNode();
                    //_currentToken = _lexer.GetNextToken();
                    //if (INDEX_ACCESS(caseExpression))
                    if (Expression(caseExpr))
                    {
                        caseNode.CaseExpression = caseExpr;
                        if (_currentToken.Type == TokenTypes.Of)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (IsNestedSentence == false)
                            {
                                caseNode.IsFirstSentence = true;
                            }
                            if (CASELIST(caseNode))
                            {
                                if (_currentToken.Type == TokenTypes.End)
                                {
                                    CaseSentece.Sentence.Add(caseNode);
                                    if (caseNode.IsFirstSentence)
                                        IsNestedSentence = false;
                                    
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

        private bool CASELIST(CaseNode caseNode)
        {
            var caseLiteral = new CaseLiteral();
            var caseLiteralSentence = new SentenceNode();
            if (CASELITERAL(caseLiteral) && _currentToken.Type != TokenTypes.Else)
            {
                if (_currentToken.Type == TokenTypes.PsColon)
                { 
                    _currentToken = _lexer.GetNextToken();
                    if (BLOCK(caseLiteralSentence))
                    {
                        caseLiteral.LiteralSentences.AddRange(caseLiteralSentence.Sentence);
                        caseNode.CaseLiterals.Add(caseLiteral);

                        if (CASELIST(caseNode))
                        {
                            return true;
                        }
                        
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
            if (ELSE(caseLiteralSentence))
            {
                caseNode.ElseLiteralSentences.AddRange(caseLiteralSentence.Sentence);
                if (BLOCK(caseNode))
                {
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Sentence expected at: " + _currentToken.Column + " , " +
                                       _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool CASELITERAL(CaseLiteral caseLiteral)
        {

            if (LISTAEXPR( caseLiteral.Expressions))
            {
                return true;
            }
            var rangeExpression = new List<RangeExpression>();
            if (LISTARANGOS(rangeExpression))
            {
                caseLiteral.Expressions.AddRange(rangeExpression);
                return true;
            }
            return false;
        }

        private bool LISTARANGOS(List<RangeExpression> rangesExpr)
        {
            var rangeExpr = new RangeExpression();
            if (RANGO(rangeExpr))
            {
                rangesExpr.Add(rangeExpr);
                if (LISTARANGOS_OP(rangesExpr))
                {
                    return true;
                }
            }
            return false;
        }

        private bool RANGO(RangeExpression rangeExpr)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                var startExpr = new ExpressionNode();
                Expression(startExpr);
                rangeExpr.Start = startExpr.Expressions[0];

                //_currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsArrayRange)
                {
                    if (SUBRANGE(rangeExpr))
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }


        private bool SUBRANGE(RangeExpression rangeExpr)
        {
            //if (Expression())
            //{
                if (_currentToken.Type == TokenTypes.PsArrayRange)
                {
                    _currentToken = _lexer.GetNextToken();
                    var endExpr = new ExpressionNode();
                    if (Expression(endExpr))
                    {
                        rangeExpr.End = endExpr.Expressions[0];
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

        private bool LISTARANGOS_OP(List<RangeExpression> rangeExpr)
        {
            if (_currentToken.Type== TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTARANGOS(rangeExpr))
                {
                    return true;
                }
            }
            return true;

        }

        private bool INDEX_ACCESS(IdNode idNode,ExpressionNode expr)
        {
           
            if (_currentToken.Type == TokenTypes.PsOpenBracket)
            {
                _currentToken = _lexer.GetNextToken();
                var accesoExpr = new AccesorExpresion();
                accesoExpr.AccessorId = idNode;

                var newExpresionAccesor = new ExpressionNode();
                if (Expression(newExpresionAccesor))
                {
                    accesoExpr.AccesorSentences.Add(newExpresionAccesor.Expressions[0]);

                    if (_currentToken.Type == TokenTypes.PsCloseBracket)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (INDEX_ACCESS(accesoExpr.AccessorId, accesoExpr))
                        {

                            if (expr is  AccesorExpresion)
                            {
                                var tempAccesor = (AccesorExpresion)expr;
                                var tempAccesorId = tempAccesor.AccessorId;
                                if (tempAccesorId.Label == idNode.Label)
                                {
                                    tempAccesor.AccesorSentences.AddRange(accesoExpr.AccesorSentences);
                                }
                            }
                            else
                            {
                                expr.Expressions.Clear();
                                expr.Expressions.Add(accesoExpr);
                            }
                            //tempAccesor.AccesorSentences.Add(accesoExpr);
                            //expr.Expressions.Add(accesoExpr);
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
                var accesoExpr = new AccesorExpresion();
                accesoExpr.AccessorId =idNode;
                
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var newExpresionAccesor = new ExpressionNode();
                    Expression(newExpresionAccesor);
                    
                    //_currentToken = _lexer.GetNextToken();
                    if (INDEX_ACCESS(accesoExpr.AccessorId, newExpresionAccesor))
                    {
                        if (newExpresionAccesor.Expressions.Any())
                        {
                            if (expr is AccesorExpresion)
                            {
                                var tempAccesor = (AccesorExpresion)expr;
                                var tempAccesorId = tempAccesor.AccessorId;
                                if (tempAccesorId.Label == idNode.Label)
                                {
                                    tempAccesor.AccesorSentences.AddRange(newExpresionAccesor.Expressions);
                                }
                                
                            }
                            else
                            {
                                accesoExpr.AccesorSentences.Add(newExpresionAccesor.Expressions[0]);
                                expr.Expressions.Clear();
                                expr.Expressions.Add(accesoExpr);
                                
                            }

                            
                        }
                        //expr.Expressions.Clear();
                        //expr.Expressions.Add(accesoExpr);
                        return true;

                    }
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return true;
        }

        private bool CONST(SentenceNode ConstantSentece)
        {
            if (_currentToken.Type == TokenTypes.Const)
            {
                var constNode = new ConstantNode(_currentToken.Row, _currentToken.Column);
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    constNode.ConstantId = new IdNode(_currentToken.Lexeme);
                    _currentToken = _lexer.GetNextToken();
                    if (CONSTDECL(constNode))
                    {
                        ConstantSentece.Sentence.Add(constNode);
                        return true;
                    }
                }
                ParserSyntaxErrors.Add("Syntax Error.Identifier Expected at: " + _currentToken.Column + " , " +
                                    _currentToken.Row);
                return false;
            }
            return false;
        }

        private bool CONSTDECL(ConstantNode constNode)
        {
            if (_currentToken.Type == TokenTypes.OpEquals)
            {
                var constValue = new ExpressionNode();
                _currentToken = _lexer.GetNextToken();
                if (Expression(constValue))
                {
                    constNode.ConstantValue=constValue;
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
                    constNode.ConstantType = _currentToken.Lexeme;

                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.OpEquals)
                    {
                        var constValue = new ExpressionNode();
                        _currentToken = _lexer.GetNextToken();
                        if (Expression(constValue))
                        {
                            constNode.ConstantValue = constValue;
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

        private bool REPEAT(SentenceNode repeatSentece)
        {
            if (_currentToken.Type == TokenTypes.Repeat)
            {
                var repeatNode = new RepeatNode(_currentToken.Row, _currentToken.Column);
                if (IsNestedSentence == false)
                {
                    repeatNode.IsFirstSentence = true;
                }

                _currentToken = _lexer.GetNextToken();
                IsNestedSentence = true;
                if (LS_LOOP(repeatNode))
                {
                    if (_currentToken.Type ==TokenTypes.Until)
                    {
                        _currentToken = _lexer.GetNextToken();
                        
                        var untilCondition = new ExpressionNode();
                        if (Expression(untilCondition))
                        {
                            repeatNode.UntilCondition = untilCondition;

                            repeatSentece.Sentence.Add(repeatNode);
                            if (repeatNode.IsFirstSentence)
                                IsNestedSentence = false;

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

        private bool WHILE(SentenceNode newWhileSentece)
        {
            if (_currentToken.Type == TokenTypes.While)
            {
                _currentToken = _lexer.GetNextToken();
                var whileNode = new WhileNode(_currentToken.Row, _currentToken.Column);
                if (IsNestedSentence == false)
                    whileNode.IsFirstSentece = true;

                var whileConditionExpr = new ExpressionNode();
                if (Expression(whileConditionExpr))
                {
                    whileNode.ConditionExpression = whileConditionExpr;
                    if (_currentToken.Type == TokenTypes.Do)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (LOOPBLOCK(whileNode))
                        {
                            newWhileSentece.Sentence.Add(whileNode);
                            if (whileNode.IsFirstSentece)
                                IsNestedSentence = false;

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

        private bool PREFOR(SentenceNode forSentenceNode)
        {
            if (_currentToken.Type == TokenTypes.For)
            {
                var forNode = new ForNode(_currentToken.Row, _currentToken.Column);
                //forSentenceNode.Sentence.Add(forNode);

                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    var indexVar = new IdNode(_currentToken.Lexeme );
                    _rootForIdex = string.IsNullOrEmpty(_rootForIdex) ? indexVar.Label : _rootForIdex;
                    forNode.IndexVarible = indexVar;

                    _currentToken = _lexer.GetNextToken();
                    if (FORBODY(forNode,forSentenceNode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool FORBODY(ForNode forNode, SentenceNode forSentenceNode)
        {
            if (FOR(forNode, forSentenceNode))
            {
                return true;
            }
            if (FORIN(new ForInNode(forNode.IndexVarible),forSentenceNode))
            {
                return true;
            }
            return false;
        }

        private bool FORIN(ForInNode forInNode,SentenceNode forinSentece)
        {
            if (_currentToken.Type == TokenTypes.In)
            {
                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.Id)
                {
                    forInNode.SourceList = new IdNode(_currentToken.Lexeme);
                    _currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Do)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (LOOPBLOCK(forInNode))
                        {
                            forInNode.ForInSentences.AddRange(forInNode.Sentence.ToList());
                            forInNode.Sentence.Clear();
                            forinSentece.Sentence.Add(forInNode);
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

        private bool FOR(ForNode forNode, SentenceNode forSentenceNode)
        {
            if (_currentToken.Type == TokenTypes.PsAssignment)
            {
                var initialForVal = new ExpressionNode();
                _currentToken = _lexer.GetNextToken();
                if (Expression(initialForVal))
                {
                    if (_currentToken.Type == TokenTypes.To)
                    {
                        var finalForVal = new ExpressionNode();
                        _currentToken = _lexer.GetNextToken();
                        if (Expression(finalForVal))
                        {
                            forNode.InitialValue.Expressions.Add( initialForVal.Expressions[0]);
                            forNode.FinalValue.Expressions.Add(finalForVal.Expressions[0]);
                            
                            if (_currentToken.Type == TokenTypes.Do)
                            {
                                _currentToken = _lexer.GetNextToken();
                                if (LOOPBLOCK(forNode))
                                {
                                    forNode.ForBodySentenses.AddRange(forNode.Sentence.ToList());
                                    forNode.Sentence.Clear();
                                    forSentenceNode.Sentence.Add(forNode);
                                    if (_rootForIdex == forNode.IndexVarible.Label && IsfunOrProc == false)
                                    {
                                        IsNestedSentence = false;
                                    }
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

 

       private bool LOOPBLOCK(SentenceNode exprLoop)
        {
            if (_currentToken.Type == TokenTypes.Begin)
            {
                IsBlockSection = true;
                IsNestedSentence = true;
                _currentToken = _lexer.GetNextToken();
                if (LS_LOOP(exprLoop))
                {
                    IsBlockSection = false;
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.End)
                    {
                        _currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            _currentToken = _lexer.GetNextToken();
                            //IsNestedSentence = false;
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
            IsNestedSentence = true;
            if (LOOP_S(exprLoop))
            {
              
                return true;
            }
            return true;

        }

        private bool LS_LOOP(SentenceNode exprLoop)
        {
            if (LOOP_S(exprLoop))
            {
                return LS_LOOP(exprLoop);
            }
            return true;
        }

        private bool LOOP_S(SentenceNode exprLoop)
        {
            if (_currentToken.Type == TokenTypes.Continue)
            {
                var row = _currentToken.Row;
                var column = _currentToken.Column;

                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    exprLoop.Sentence.Add(new ContinueNode(row, column));
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }

            if (_currentToken.Type == TokenTypes.Break)
            {
                var row = _currentToken.Row;
                var column = _currentToken.Column;

                _currentToken = _lexer.GetNextToken();
                if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                {
                    exprLoop.Sentence.Add(new BreakNode(row, column));
                    _currentToken = _lexer.GetNextToken();
                    return true;
                }
                ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                return false;
            }
            //IsNestedSentence = true;
            if (S(exprLoop))
            {
                //IsNestedSentence = false;
                return true;
            }
            return false;
        }

        private bool PREID(SentenceNode newSentece)
        {
            if (_currentToken.Type == TokenTypes.Id)
            {
                //var idVal = new ExpressionNode();
                //Expression(idVal);
                var idVal = new IdNode(_currentToken.Lexeme);
                _currentToken = _lexer.GetNextToken();
                if (IDBODY(idVal,newSentece))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IDBODY(IdNode idVal, SentenceNode idSentece)
        {
            if (_currentToken.Type== TokenTypes.PsAssignment || _currentToken.Type == TokenTypes.PsOpenBracket)
            {
                if (_currentToken.Type == TokenTypes.PsOpenBracket)
                {
                    var accesorExpr = new AccesorExpresion();
                    accesorExpr.AccessorId = idVal;
                    _currentToken = _lexer.GetNextToken();

                    var accesorSentence = new ExpressionNode();
                    if (Expression(accesorSentence))
                    {
                        accesorExpr.AccesorSentences.Add(accesorSentence.Expressions[0]);
                        if (_currentToken.Type == TokenTypes.PsCloseBracket)
                        {
                            _currentToken = _lexer.GetNextToken();
                            if (INDEX_ACCESS(accesorExpr.AccessorId,accesorExpr))
                            {
                                if (_currentToken.Type == TokenTypes.PsAssignment)
                                {
                                    _currentToken = _lexer.GetNextToken();
                                    var expr = new ExpressionNode();
                                    if (Expression(expr))
                                    {
                                        //_currentToken = _lexer.GetNextToken();
                                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                                        {
                                            idSentece.Sentence.Add(new AssignNode(accesorExpr, expr));
                                            _currentToken = _lexer.GetNextToken();
                                            return true;
                                        }
                                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column +
                                                               " , " + _currentToken.Row);
                                        return false;
                                    }
                                }
                                
                            }

                        }
                    }
                }
                else
                {
                    _currentToken = _lexer.GetNextToken();
                    var assignExpr = new ExpressionNode();
                    if (Expression(assignExpr))
                    {
                        //_currentToken = _lexer.GetNextToken();
                        if (_currentToken.Type == TokenTypes.PsSentenseEnd)
                        {
                            idSentece.Sentence.Add(new AssignNode(idVal, assignExpr));
                            _currentToken = _lexer.GetNextToken();
                            return true;
                        }
                        ParserSyntaxErrors.Add("Syntax Error.Expected symbol ';' at: " + _currentToken.Column + " , " + _currentToken.Row);
                        return false;
                    }
                    ParserSyntaxErrors.Add("Syntax Error.Expression Expected at: " + _currentToken.Column + " , " + _currentToken.Row);
                    return false;
                }
                
            }
            //if (idVal.Expressions[0] is IdNode)
            //{
            //    var id = (IdNode)idVal.Expressions[0];
            //    var idNode = new IdNode(id.Label);
                if (LLAMARFUNCIONSENTENCIA(idVal, idSentece))
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
            //}
            
            return false;
        }

        private bool LLAMARFUNCIONSENTENCIA(IdNode idVal, SentenceNode idSentece)
        {
            if (_currentToken.Type == TokenTypes.PsOpenParentesis)
            {
                _currentToken = _lexer.GetNextToken();
                var listParameter = new List<ExpressionNode>();
                if (LISTAEXPR(listParameter))
                {
                    if (_currentToken.Type==TokenTypes.PsCloseParentesis)
                    {
                        var functionCallNode = new FunctionCallNode(idVal, listParameter);


                        idSentece.Sentence.Add(functionCallNode);
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

        private bool LISTAEXPR(List<ExpressionNode> listParameter)
        {
            var expr = new ExpressionNode();
            if (Expression(expr))
            {
                listParameter.Add(expr.Expressions[0]);
                if (LISTAEXPR_OP(listParameter))
                {
                    //_currentToken = _lexer.GetNextToken();
                    return true;
                }
            }
            return true;
        }

        private bool LISTAEXPR_OP(List<ExpressionNode> listParameter)
        {
            if (_currentToken.Type == TokenTypes.PsComa)
            {
                _currentToken = _lexer.GetNextToken();
                if (LISTAEXPR(listParameter))
                {
                    return true;
                }
            }
            return true;

        }

        private bool IF(SentenceNode ifSentence)
        {
            if (_currentToken.Type == TokenTypes.If)
            {
                var ifNode = new IfNode(_currentToken.Column,_currentToken.Row);
                if (IsNestedSentence == false)
                {
                    ifNode.IsFirstSentece = true;
                }
                _currentToken = _lexer.GetNextToken();
                var ifConditionExpression = new ExpressionNode();
                if (Expression(ifConditionExpression))
                {
                    ifNode.IfConditionExpression = ifConditionExpression;
                    //_currentToken = _lexer.GetNextToken();
                    if (_currentToken.Type == TokenTypes.Then)
                    {
                        _currentToken = _lexer.GetNextToken();
                        var ifBlockSentences = new SentenceNode();
                        if (BLOCK(ifBlockSentences))
                        {
                            ifNode.TrueBlockSentences = ifBlockSentences;
                            var elseBlockSentences= new SentenceNode();
                            if (ELSE(elseBlockSentences))
                            {
                                ifNode.ElseBlockSentences = elseBlockSentences;
                                ifSentence.Sentence.Add(ifNode);

                                if (ifNode.IsFirstSentece)
                                    IsNestedSentence = false;
                                //sentence.Sentence.Add(new IfNode(ifRow, ifColumn, ifConditionExpression, ifBlockSentences, elseBlockSentences));
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
            IsNestedSentence = true;
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
                    var typeDecl = new IdNode(_currentToken.Lexeme);
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
                        var typeDecl = new IdNode(_currentToken.Lexeme);
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
                //var divRealNode = new DivisionRealNode();
                binaryOperation.TypeNode = typeof(DivisionRealNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if(_currentToken.Type == TokenTypes.OpDiv)
            {
                //var divNode = new DivisionNode();
                binaryOperation.TypeNode = typeof(DivisionNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }

            if (_currentToken.Type == TokenTypes.OpMod)
            {
                //var modNode = new ModuleNode();
                binaryOperation.TypeNode = typeof(ModuleNode);

                _currentToken = _lexer.GetNextToken();
                return true;
            }
            if(_currentToken.Type == TokenTypes.OpAnd)
            {
                //var andNode = new AndNode();
                binaryOperation.TypeNode = typeof(AndNode); ;

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
                _currentToken = _lexer.GetNextToken();

                var factorVal = Factor(unaryNode.UnaryOperand);
                unaryexpr.Expressions.Add(unaryNode);

                return factorVal;
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

                    _currentToken = _lexer.GetNextToken();
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
                if (X(idNode, expr))
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

        private bool X(IdNode idNode,ExpressionNode expr)
        {
            var xSentence = new SentenceNode();
            if (LLAMARFUNCIONSENTENCIA(idNode,xSentence ))
            {
                if (xSentence.Sentence[0] is FunctionCallNode) 
                {
                    var convertNode = (FunctionCallNode)xSentence.Sentence[0];

                    foreach (var functionExpr in convertNode.FunctionParams)
                    {
                        if (functionExpr is AccesorExpresion)
                        {
                            IsFunctionCallExpression = true;
                        }
                    }

                    expr.Expressions.Clear();
                   // if (IsFunctionCallExpression)
                   //{
                        var newFunctionCallExpr = new FunctionCallExpression(convertNode.FunctionName,convertNode.FunctionParams);
                        expr.Expressions.Add(newFunctionCallExpr);
                        IsFunctionCallExpression = false;
                //}
                   
                }
                //expr.Expressions.AddRange(convertNode.FunctionParams);
                return true;
            }
            if (INDEX_ACCESS(idNode,expr))
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
                    if (sentence.Sentence[0] is DeclarationNode)
                    {
                        var dec = (DeclarationNode)sentence.Sentence[0];
                        dec.IdsList.Add(idnode);

                    }
                }
                if (sentence is EnumeratorConstants)
                {
                    var constantSentence = (EnumeratorConstants)sentence;
                    constantSentence.EnumeratorConstantsExpressions.Add(idnode);

                }
                else if (sentence is RecordPropertyNode)
                {
                    var recordProperty = (RecordPropertyNode)sentence;
                    recordProperty.RecordPropertyIds.Add(idnode);
                }
                else if (sentence is ParametersIds)
                {
                    var parameterId = (ParametersIds) sentence;
                    parameterId.ParametersIdsExpressions.Add(idnode);

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
