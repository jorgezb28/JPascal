using System.Collections.Generic;
using JPascalCompiler.LexerFolder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace JPacalCompiler.Test.Steps
{
    [Binding]
    public class LexerSteps
    {
        Lexer lexer;
        List<Token> tokenList = new List<Token>();

        [Given(@"I have an input of '(.*)'")]
        public void GivenIHaveAnInputOf(string p0)
        {
            lexer = new Lexer(new SourceCode(p0));
        }
        
        [When(@"We Tokenize")]
        public void WhenWeTokenize()
        {
            Token currentToken = lexer.GetNextToken();

            while (currentToken.Type != TokenTypes.EOF)
            {
                tokenList.Add(currentToken);
                currentToken = lexer.GetNextToken();
            }
            tokenList.Add(currentToken);

        }
        
        [Then(@"the result should be")]
        public void ThenTheResultShouldBe(Table table)
        {
            for (int i = 0; i < table.RowCount; i++)
            {
                Assert.AreEqual(table.Rows[i]["Type"], tokenList[i].Type.ToString(), "The TokenTypes do not match.");
                Assert.AreEqual(table.Rows[i]["Lexeme"], tokenList[i].Lexeme, "The TokenLexeme do not match.");
                Assert.AreEqual(table.Rows[i]["Row"], tokenList[i].Row.ToString(), "The TokenRow do not match.");
                Assert.AreEqual(table.Rows[i]["Column"], tokenList[i].Column.ToString(), "The TokenColumn do not match.");
            }
        }
    }
}
