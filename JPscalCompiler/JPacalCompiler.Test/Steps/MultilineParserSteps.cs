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
    public class MultilineParserSteps
    {
        Parser _myMultilineParser;
        private List<string> _parserSyntaxErrors;
        private string _multilineSentence;

        [Given(@"I have a multiline sentence declaration")]
        public void GivenIHaveAMultilineSentenceDeclaration(Table table)
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
            _myMultilineParser = new Parser(new Lexer(new SourceCode(_multilineSentence)));
        }

        [When(@"We Parse")]
        public void WhenWeParse()
        {
            _myMultilineParser.Parse();
            _parserSyntaxErrors = new List<string>(_myMultilineParser.ParserSyntaxErrors);
        }

        [Then(@"the multiline result should be true")]
        public void ThenTheResultShouldBeTrue()
        {
            //Assert.IsTrue(_successfullyCompile);
            Assert.IsTrue(!_parserSyntaxErrors.Any());

        }
    }
}
