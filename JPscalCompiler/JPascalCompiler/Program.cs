using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JPascalCompiler.LexerFolder;

namespace JPascalCompiler
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"E:\UNIVERSIDAD\COMPILADORES I\2016";
            openFileDialog1.Title = "Browse JPascal Files";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "JPascal files (*.jpascal)|*.jpascal";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                var sourceCode = sr.ReadToEnd();
                sr.Close();

               
                var parser = new Parser.Parser(new Lexer(new SourceCode(sourceCode)));
                try
                {
                    parser.Parse();
                    var tree = parser.SentenceList;
                    
                    if (parser.ParserSyntaxErrors.Any())
                    {
                        foreach (var parserSyntaxError in parser.ParserSyntaxErrors)
                        {
                            Console.WriteLine(parserSyntaxError);
                        }
                    }
                    else
                    {
                        foreach (var sentenceNode in tree)
                        {
                            sentenceNode.ValidateSemantic();
                        }
                        Console.WriteLine("No errors found.");
                    }
                    
                    //var javaCode = GenerateMain.InitJavaCode(tree.TreeGenerateDeclarationCode(), tree.TreeGenerateCode());
                    //Console.Write(javaCode);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

                Console.ReadKey();


            }

            


        }
    }
}
