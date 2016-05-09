﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.0.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace JPacalCompiler.Test.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class LexerFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Lexer.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Lexer", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Lexer")))
            {
                JPacalCompiler.Test.Specs.LexerFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is empty")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("mytag")]
        public virtual void SourceCodeIsEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is empty", new string[] {
                        "mytag"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have an input of \'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table1.AddRow(new string[] {
                        "EOF",
                        "$",
                        "0",
                        "0"});
#line 10
 testRunner.Then("the result should be", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is an Id with digit")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeIsAnIdWithDigit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is an Id with digit", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I have an input of \'a1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table2.AddRow(new string[] {
                        "Id",
                        "a1",
                        "0",
                        "0"});
            table2.AddRow(new string[] {
                        "EOF",
                        "$",
                        "2",
                        "0"});
#line 17
 testRunner.Then("the result should be", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is an string Id")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeIsAnStringId()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is an string Id", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I have an input of \'patito\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table3.AddRow(new string[] {
                        "Id",
                        "patito",
                        "0",
                        "0"});
            table3.AddRow(new string[] {
                        "EOF",
                        "$",
                        "6",
                        "0"});
#line 25
 testRunner.Then("the result should be", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is an reserved word")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeIsAnReservedWord()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is an reserved word", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line 31
 testRunner.Given("I have an input of \'type\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 32
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table4.AddRow(new string[] {
                        "Type",
                        "type",
                        "0",
                        "0"});
            table4.AddRow(new string[] {
                        "EOF",
                        "$",
                        "4",
                        "0"});
#line 33
 testRunner.Then("the result should be", ((string)(null)), table4, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is an integer")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeIsAnInteger()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is an integer", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("I have an input of \'2016\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table5.AddRow(new string[] {
                        "Integer",
                        "2016",
                        "0",
                        "0"});
            table5.AddRow(new string[] {
                        "EOF",
                        "$",
                        "4",
                        "0"});
#line 41
 testRunner.Then("the result should be", ((string)(null)), table5, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code is a float")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeIsAFloat()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code is a float", ((string[])(null)));
#line 46
this.ScenarioSetup(scenarioInfo);
#line 47
 testRunner.Given("I have an input of \'1024.633\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 48
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table6.AddRow(new string[] {
                        "Float",
                        "1024.633",
                        "0",
                        "0"});
            table6.AddRow(new string[] {
                        "EOF",
                        "$",
                        "8",
                        "0"});
#line 49
 testRunner.Then("the result should be", ((string)(null)), table6, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have two id separated by space")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveTwoIdSeparatedBySpace()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have two id separated by space", ((string[])(null)));
#line 54
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.Given("I have an input of \'var myArray\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table7.AddRow(new string[] {
                        "Var",
                        "var",
                        "0",
                        "0"});
            table7.AddRow(new string[] {
                        "Id",
                        "myArray",
                        "4",
                        "0"});
            table7.AddRow(new string[] {
                        "EOF",
                        "$",
                        "11",
                        "0"});
#line 57
 testRunner.Then("the result should be", ((string)(null)), table7, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have an aritmethic operation with reserved word")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveAnAritmethicOperationWithReservedWord()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have an aritmethic operation with reserved word", ((string[])(null)));
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("I have an input of \'10 mod 5\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 65
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table8.AddRow(new string[] {
                        "Integer",
                        "10",
                        "0",
                        "0"});
            table8.AddRow(new string[] {
                        "OpMod",
                        "mod",
                        "3",
                        "0"});
            table8.AddRow(new string[] {
                        "Integer",
                        "5",
                        "7",
                        "0"});
            table8.AddRow(new string[] {
                        "EOF",
                        "$",
                        "8",
                        "0"});
#line 66
 testRunner.Then("the result should be", ((string)(null)), table8, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have an logic operation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveAnLogicOperation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have an logic operation", ((string[])(null)));
#line 73
this.ScenarioSetup(scenarioInfo);
#line 74
 testRunner.Given("I have an input of \'true and false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 75
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table9.AddRow(new string[] {
                        "True",
                        "true",
                        "0",
                        "0"});
            table9.AddRow(new string[] {
                        "OpAnd",
                        "and",
                        "5",
                        "0"});
            table9.AddRow(new string[] {
                        "False",
                        "false",
                        "9",
                        "0"});
            table9.AddRow(new string[] {
                        "EOF",
                        "$",
                        "14",
                        "0"});
#line 76
 testRunner.Then("the result should be", ((string)(null)), table9, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have an sum aritmethic operation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveAnSumAritmethicOperation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have an sum aritmethic operation", ((string[])(null)));
#line 83
this.ScenarioSetup(scenarioInfo);
#line 84
 testRunner.Given("I have an input of \'10 + 5\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 85
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table10.AddRow(new string[] {
                        "Integer",
                        "10",
                        "0",
                        "0"});
            table10.AddRow(new string[] {
                        "OpSum",
                        "+",
                        "3",
                        "0"});
            table10.AddRow(new string[] {
                        "Integer",
                        "5",
                        "5",
                        "0"});
            table10.AddRow(new string[] {
                        "EOF",
                        "$",
                        "6",
                        "0"});
#line 86
 testRunner.Then("the result should be", ((string)(null)), table10, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have an mult aritmethic operation")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveAnMultAritmethicOperation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have an mult aritmethic operation", ((string[])(null)));
#line 93
this.ScenarioSetup(scenarioInfo);
#line 94
 testRunner.Given("I have an input of \'10 * 5\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 95
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table11.AddRow(new string[] {
                        "Integer",
                        "10",
                        "0",
                        "0"});
            table11.AddRow(new string[] {
                        "OpMult",
                        "*",
                        "3",
                        "0"});
            table11.AddRow(new string[] {
                        "Integer",
                        "5",
                        "5",
                        "0"});
            table11.AddRow(new string[] {
                        "EOF",
                        "$",
                        "6",
                        "0"});
#line 96
 testRunner.Then("the result should be", ((string)(null)), table11, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Source code have an unidimensional array")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Lexer")]
        public virtual void SourceCodeHaveAnUnidimensionalArray()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Source code have an unidimensional array", ((string[])(null)));
#line 103
this.ScenarioSetup(scenarioInfo);
#line 104
 testRunner.Given("I have an input of \'type vector = array [ 0..24] of float;\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 105
 testRunner.When("We Tokenize", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Type",
                        "Lexeme",
                        "Column",
                        "Row"});
            table12.AddRow(new string[] {
                        "Type",
                        "type",
                        "0",
                        "0"});
            table12.AddRow(new string[] {
                        "Id",
                        "vector",
                        "5",
                        "0"});
            table12.AddRow(new string[] {
                        "OpEquals",
                        "=",
                        "12",
                        "0"});
            table12.AddRow(new string[] {
                        "Array",
                        "array",
                        "14",
                        "0"});
            table12.AddRow(new string[] {
                        "PsOpenBracket",
                        "[",
                        "19",
                        "0"});
            table12.AddRow(new string[] {
                        "Integer",
                        "0",
                        "21",
                        "0"});
            table12.AddRow(new string[] {
                        "PsArrayRange",
                        "..",
                        "22",
                        "0"});
            table12.AddRow(new string[] {
                        "Integer",
                        "24",
                        "24",
                        "0"});
            table12.AddRow(new string[] {
                        "PsCloseBracket",
                        "]",
                        "27",
                        "0"});
            table12.AddRow(new string[] {
                        "Of",
                        "of",
                        "29",
                        "0"});
            table12.AddRow(new string[] {
                        "Float",
                        "float",
                        "31",
                        "0"});
            table12.AddRow(new string[] {
                        "PsSentenseEnd",
                        ";",
                        "35",
                        "0"});
            table12.AddRow(new string[] {
                        "EOF",
                        "$",
                        "36",
                        "0"});
#line 106
 testRunner.Then("the result should be", ((string)(null)), table12, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
