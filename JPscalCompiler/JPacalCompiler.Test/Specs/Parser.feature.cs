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
    public partial class ParserFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Parser.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Parser", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Parser")))
            {
                JPacalCompiler.Test.Specs.ParserFeature.FeatureSetup(null);
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have the simple declaration sentence without initialization")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("mytag")]
        public virtual void HaveTheSimpleDeclarationSentenceWithoutInitialization()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have the simple declaration sentence without initialization", new string[] {
                        "mytag"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I have a sentence declaration \'Var s : String ;\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.When("We parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
 testRunner.Then("the result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have the multiple declaration sentence without initialization")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveTheMultipleDeclarationSentenceWithoutInitialization()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have the multiple declaration sentence without initialization", ((string[])(null)));
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
 testRunner.Given("I have a sentence declaration \'Var S,p : String ;\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.When("We parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("the result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have the string declaration sentence with initialization")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveTheStringDeclarationSentenceWithInitialization()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have the string declaration sentence with initialization", ((string[])(null)));
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
 testRunner.Given("I have a sentence declaration \'Var str : String = \'patito\';\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
 testRunner.When("We parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
 testRunner.Then("the result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have the declaration sentence with expresion initialization")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveTheDeclarationSentenceWithExpresionInitialization()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have the declaration sentence with expresion initialization", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("I have a sentence declaration \'Var number : integer = (1+5)*3;\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("We parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("the result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline if sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineIfSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline if sentence", ((string[])(null)));
#line 27
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table1.AddRow(new string[] {
                        "if (a = 10)  then"});
            table1.AddRow(new string[] {
                        "writeln(\'Value of a is 10\' );"});
#line 28
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table1, "Given ");
#line 32
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline if then sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineIfThenSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline if then sentence", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table2.AddRow(new string[] {
                        "if (a = 10)  then"});
            table2.AddRow(new string[] {
                        "writeln(\'Value of a is 10\' );"});
            table2.AddRow(new string[] {
                        "else if ( a = 20 ) then"});
            table2.AddRow(new string[] {
                        "writeln(\'Value of a is 20\' );"});
            table2.AddRow(new string[] {
                        "else"});
            table2.AddRow(new string[] {
                        "writeln(\'None of the values is matching\' );"});
            table2.AddRow(new string[] {
                        "writeln(\'Exact value of a is: \', a );"});
#line 36
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table2, "Given ");
#line 45
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline simple for sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineSimpleForSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline simple for sentence", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table3.AddRow(new string[] {
                        "for i:= 1 to 10 do"});
            table3.AddRow(new string[] {
                        "writeln(i);"});
#line 49
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table3, "Given ");
#line 53
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 54
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline complex for sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineComplexForSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline complex for sentence", ((string[])(null)));
#line 56
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table4.AddRow(new string[] {
                        "for i:=1 to 3 do"});
            table4.AddRow(new string[] {
                        "begin"});
            table4.AddRow(new string[] {
                        "for j:=1 to 3 do"});
            table4.AddRow(new string[] {
                        "begin"});
            table4.AddRow(new string[] {
                        "write(a2,s);"});
            table4.AddRow(new string[] {
                        "end;"});
            table4.AddRow(new string[] {
                        "writeln();"});
            table4.AddRow(new string[] {
                        "end;"});
#line 57
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table4, "Given ");
#line 67
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline for-in sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineFor_InSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline for-in sentence", ((string[])(null)));
#line 70
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table5.AddRow(new string[] {
                        "for Color in TColor do"});
            table5.AddRow(new string[] {
                        "DoSomething(Color);"});
#line 71
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table5, "Given ");
#line 75
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 76
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline simple while sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineSimpleWhileSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline simple while sentence", ((string[])(null)));
#line 78
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table6.AddRow(new string[] {
                        "while a + 6 do"});
            table6.AddRow(new string[] {
                        "writeln (a);"});
#line 79
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table6, "Given ");
#line 83
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline complex while sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineComplexWhileSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline complex while sentence", ((string[])(null)));
#line 86
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table7.AddRow(new string[] {
                        "WHILE NOT true DO"});
            table7.AddRow(new string[] {
                        "BEGIN"});
            table7.AddRow(new string[] {
                        "IF NOT true <> false THEN"});
            table7.AddRow(new string[] {
                        "size := size + 1;"});
            table7.AddRow(new string[] {
                        "END;"});
#line 87
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table7, "Given ");
#line 94
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 95
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline complex repeat sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineComplexRepeatSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline complex repeat sentence", ((string[])(null)));
#line 97
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table8.AddRow(new string[] {
                        "repeat"});
            table8.AddRow(new string[] {
                        "DoSomethingHere(x);"});
            table8.AddRow(new string[] {
                        "x := x + 1;"});
            table8.AddRow(new string[] {
                        "while a + 6 do"});
            table8.AddRow(new string[] {
                        "writeln (a);"});
            table8.AddRow(new string[] {
                        "until x = 10;"});
#line 98
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table8, "Given ");
#line 106
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 107
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline simple const declaration")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineSimpleConstDeclaration()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline simple const declaration", ((string[])(null)));
#line 109
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table9.AddRow(new string[] {
                        "const"});
            table9.AddRow(new string[] {
                        "i: Integer = 0;"});
#line 110
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table9, "Given ");
#line 114
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 115
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline simple case sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineSimpleCaseSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline simple case sentence", ((string[])(null)));
#line 117
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table10.AddRow(new string[] {
                        "case place of"});
            table10.AddRow(new string[] {
                        "1: ShowMessage(\'sds\');"});
            table10.AddRow(new string[] {
                        "2: ShowMessage(sdds);"});
            table10.AddRow(new string[] {
                        "3: ShowMessage(sd.test);"});
            table10.AddRow(new string[] {
                        "else ShowMessage(sdsd);"});
            table10.AddRow(new string[] {
                        "end;"});
#line 118
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table10, "Given ");
#line 126
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 127
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Have a multiline complex record sentence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Parser")]
        public virtual void HaveAMultilineComplexRecordSentence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Have a multiline complex record sentence", ((string[])(null)));
#line 129
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Sentences"});
            table11.AddRow(new string[] {
                        "type TMember = record"});
            table11.AddRow(new string[] {
                        "firstname, lastname : string;"});
            table11.AddRow(new string[] {
                        "address: array [1 .. 3] of string;"});
            table11.AddRow(new string[] {
                        "phone: string;"});
            table11.AddRow(new string[] {
                        "birthdate: TDateTime;"});
            table11.AddRow(new string[] {
                        "paidCurrentSubscription: boolean;"});
            table11.AddRow(new string[] {
                        "end;"});
#line 130
 testRunner.Given("I have a multiline sentence declaration", ((string)(null)), table11, "Given ");
#line 139
 testRunner.When("We Parse", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 140
 testRunner.Then("the multiline result should be true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
