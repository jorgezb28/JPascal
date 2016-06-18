using System;
using System.Collections.Generic;
using System.Linq;
using JPascalCompiler.LexerFolder;
using JPascalCompiler.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace JPacalCompiler.Test.Steps
{
    [Binding]
    public class ParserSteps
    {
        Parser _myParser;
        private List<string> _parserSyntaxErrors;
        //private bool _successfullyCompile = false;


        [Given(@"I have a sentence declaration '(.*)'")]
        public void GivenIHaveASentenceDeclarationThisIsAnInitializedString(string p0)
        {
            _myParser= new Parser(new Lexer(new SourceCode(p0)));

        }
        
        [When(@"We parse")]
        public void WhenWeParse()
        {
           _myParser.Parse();
           _parserSyntaxErrors = new List<string>(_myParser.ParserSyntaxErrors);
        }
        
        [Then(@"the result should be true")]
        public void ThenTheResultShouldBeTrue()
        {
            //Assert.IsTrue(_successfullyCompile);
            Assert.IsTrue(!_parserSyntaxErrors.Any());

        }
    }
}
