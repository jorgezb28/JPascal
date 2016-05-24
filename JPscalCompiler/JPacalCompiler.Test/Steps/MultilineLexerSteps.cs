using System;
using System.Collections.Generic;
using JPascalCompiler.LexerFolder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace JPacalCompiler.Test.Steps
{
    [Binding]
    public class MultilineLexerSteps
    {
        Lexer _lexer;
        private readonly List<Token> _tokenList = new List<Token>();
        private string _multilineSentence;


        [Given(@"I have the next record definition:")]
        public void GivenIHaveTheNextRecordDefinition(Table table)
        {
            _multilineSentence = string.Empty;

            for (var i = 0; i < table.RowCount; i++)
            {
                var linestr = table.Rows[i]["Sentences"];
                _multilineSentence += linestr;
                if (i + 1 != table.RowCount)
                {
                    _multilineSentence += "\n";
                }
            }

            _lexer = new Lexer( new SourceCode(_multilineSentence));
        }

        [When(@"We tokenize")]
        public void WhenWeTokenize()
        {
            var currentToken = _lexer.GetNextToken();

            while (currentToken.Type != TokenTypes.EOF)
            {
                _tokenList.Add(currentToken);
                currentToken = _lexer.GetNextToken();
            }
            _tokenList.Add(currentToken);
        }

        [Then(@"the multiline result should be")]
        public void ThenTheResultShouldBe(Table table)
        {
            for (var i = 0; i < table.RowCount; i++)
            {
                Assert.AreEqual(table.Rows[i]["Type"], _tokenList[i].Type.ToString(), "The TokenTypes do not match.");
                Assert.AreEqual(table.Rows[i]["Lexeme"], _tokenList[i].Lexeme, "The TokenLexeme do not match.");
                Assert.AreEqual(table.Rows[i]["Row"], _tokenList[i].Row.ToString(), "The TokenRow do not match.");
                Assert.AreEqual(table.Rows[i]["Column"], _tokenList[i].Column.ToString(), "The TokenColumn do not match.");
            }
        }

    }
}
