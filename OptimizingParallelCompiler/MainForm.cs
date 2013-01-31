﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace OptimizingParallelCompiler
{
    public partial class MainForm : Form
    {
        private readonly List<string> _reserveWords;
        private string _output = "Out.exe";
        private CompilerResults _results;
        private int _labelCounter = 0;

        const string ErrorFile = "error.txt";
        const string ResultsFile = "results.txt";

        public MainForm()
        {
            InitializeComponent();

            _reserveWords = new List<string>(25)
                {
                    "let",
                    "title",
                    "var",
                    "int",
                    "list",
                    "rem",
                    "label",
                    "if",
                    "endfor",
                    "then",
                    "goto",
                    "input",
                    "print",
                    "prompt",
                    "end",
                    "for",
                    "while",
                };
        }

        private void RtbColor()
        {
            for (int i = 0; i < txtOneilCode.Lines.Count(); ++i)
            {
                foreach (var reserveWord in _reserveWords)
                {
                    if (txtOneilCode.Lines[i].Contains(reserveWord))
                    {
                        if (i > 0)
                        {
                            txtOneilCode.Select(
                                txtOneilCode.Lines[i].IndexOf(reserveWord) + txtOneilCode.Lines[i].Count() + 1,
                                reserveWord.Length);
                            txtOneilCode.SelectionColor = Color.DodgerBlue;
                        }
                        else if (i == 0)
                        {
                            txtOneilCode.Select(
                                txtOneilCode.Lines[i].IndexOf(reserveWord),
                                reserveWord.Length);
                            txtOneilCode.SelectionColor = Color.DodgerBlue;
                        }
                    }
                }
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
            if (e.KeyChar.Equals((char) Keys.Enter))
            {
                Console.WriteLine(true);
                RtbColor();
            }
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var test = txtOneilCode.Lines.ToList();

            for (int i = 0; i < test.Count; i++)
            {
                foreach (var reserveWord in _reserveWords)
                {
                    if (test[i].Contains(reserveWord))
                    {
                        if (reserveWord.Equals("title"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                            sentence = "//" + sentence;
                            test[i] = sentence;
                            if (i == 0)
                            {
                                const string usingstatements = "using System;\n"  + "class Program\n" + "{";
                                test.Insert(1, usingstatements);
                            }
                        }
                        else if (reserveWord.Equals("while",StringComparison.Ordinal) && !test[i].Contains("end"))
                        {
                            txtError.AppendText(Environment.NewLine.ToString() + reserveWord + " : " +
                                                test[i].ToString() + " : " + string.Equals(reserveWord, "while"));
                            var statement = test[i];
                            var label = "label" + _labelCounter;
                            ++_labelCounter;
                            var label1 = "label" + _labelCounter;
                            var sentence = "goto " + label + ";\n";
                            sentence += label1 + ":";
                            test[i] = sentence;
                            var other = new List<string>(test.GetRange(0,test.Count));
                            for (int x =0;x<other.Count;++x)
                            {
                                other[x] = other[x].Trim('\t', ' ');
                            }
                            var endWhile = other.IndexOf("endwhile");

                            test[endWhile] = label + ":";
                            statement = statement.Replace("while", "if");
                            statement = statement.TrimEnd(' ');
                            statement = statement + " goto " + label1 + ";";

                            test[endWhile] += "\n" + statement;
                            txtError.AppendText(Environment.NewLine + statement.ToString());

                            ++_labelCounter;
                        }
                        else if (reserveWord.Equals("for"))
                        {
                            test[i] = test[i].Replace("\t", string.Empty);
                            var index = test[i].IndexOf("for") + reserveWord.Count() + 1;
                            var end = test[i].IndexOf("to") - 1;
                            var value = "\t\t" + test[i].Substring(index, end - index) + ";\n";
                            var value1 = "\t" + value.Substring(2, value.IndexOf("=") - 2) + "=" +
                                         value.Substring(2, value.IndexOf("=") - 2) + " + 1;";
                            var label = "Label" + _labelCounter.ToString();
                            var sentence = value + "\t" + label + ":";
                            var number = test[i].IndexOf("to") + 2;
                            test[i] = test[i].TrimEnd(' ');
                            var lastvalue = test[i].Last();
                            var number1 = test[i].IndexOf(lastvalue);
                            var last = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= " +
                                       test[i].Substring(number, number1 - number + 1) +
                                       " ) goto " + label + ";";

                            test[i] = sentence;
                            test.Insert(i + 2, value1);
                            test.Insert(i + 3, last);

                            ++_labelCounter; 
                        }
                        else if(reserveWord.Equals("var"))
                        {
                            test[i] = "\tstatic void Main()\n\t{";
                        }
                        else if (reserveWord.Equals("end") && test[i].Equals(reserveWord))
                        {
                            test[i] = test[i].Replace("\t", "");
                            test[i] = "\t}\n}";
                        }
                        else if (reserveWord.Equals("prompt") || reserveWord.Equals("print"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("\t", string.Empty);
                            sentence = sentence.Replace(";", "");
                            sentence = sentence.TrimEnd(' ');
                            sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                            sentence = "\tConsole.WriteLine(" + sentence + ");";
                            test[i] = sentence;
                        }
                        else if (reserveWord.Equals("rem"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("\t", string.Empty);
                            sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                            sentence = "\t//" + sentence;
                            test[i] = sentence;
                        }
                        else if (reserveWord.Equals("list"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("\t", string.Empty);
                            var arrayBegin = sentence.IndexOf("[") + 1;
                            var arrayEnd = sentence.IndexOf("]");
                            var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                            var number = sentence.IndexOf(" ");
                            sentence = sentence.Substring(number, sentence.Length - number);
                            sentence = "\tint[]" + sentence + " = new int[" + value + "];";
                            test[i] = sentence;
                        }
                        else if (reserveWord.Equals("let"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("\t", string.Empty);
                            sentence = sentence.Substring(reserveWord.Count(), sentence.Length - reserveWord.Count());
                            sentence = "\t" + sentence + ";";
                            test[i] = sentence;
                        }
                        else if (reserveWord.Equals("endfor"))
                        {
                            test[i] = "";
                        }
                        else if (reserveWord.Equals("input"))
                        {

                        }
                        else if (reserveWord.Equals("int"))
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("\t", string.Empty);
                            sentence = "\t" + sentence + ";";
                            test[i] = sentence;
                        }
                        else if (reserveWord.Equals("if") && !test[i].Contains("goto"))
                        {
                            var label = "label" + _labelCounter;
                            test[i] = test[i].Trim(' ', '\t');
                            var sentence = test[i].Substring(0, test[i].Count() - "then".Length);
                            sentence += "goto " + label + ";";

                            test[i] = sentence;
                            test.Insert(i + 2, label + ":");

                            ++_labelCounter;
                        }
                    }
                    else if (test[i].Contains("begin"))
                    {
                        test[i] = test[i].Replace("\t", "");
                        test[i] = "\t";
                    }
                }  
            }

            txtCSharpCode.Clear();

            foreach (var lines in test)
            {
                txtCSharpCode.Text += lines + "\n";
            }
        }

        /// <summary>
        /// Menu item to compile the code to an executable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var codeProvider = CodeDomProvider.CreateProvider("CSharp");

            txtError.Text = "";

            //generate exe not dll
            var par = new CompilerParameters
                {
                    GenerateExecutable = true,
                    OutputAssembly = _output + ".exe",
                    CompilerOptions = "/platform:x86"
                };

            //Assembly a;
            //par.ReferencedAssemblies.Add("C:/Program Files (x86)/Microsoft XNA/XNA Game Studio/v4.0/References/Windows/x86/Microsoft.Xna.Framework.dll");
            //par.ReferencedAssemblies.Add("C:/Program Files (x86)/Microsoft XNA/XNA Game Studio/v4.0/References/Windows/x86/Microsoft.Xna.Framework.Game.dll");
            //par.ReferencedAssemblies.Add("C:/Program Files (x86)/Microsoft XNA/XNA Game Studio/v4.0/References/Windows/x86/Microsoft.Xna.Framework.Graphics.dll");


            txtError.Text = par.LinkedResources + Environment.NewLine;

            _results = codeProvider.CompileAssemblyFromSource(par, txtCSharpCode.Text);


            if (_results.Errors.Count > 0)
            {
                txtError.ForeColor = Color.FromArgb(122, 77, 198);

                foreach (CompilerError item in _results.Errors)
                {
                    txtError.Text += "line number " + item.Line + ", error num" + item.ErrorNumber +
                                    " , " + item.ErrorText + Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Successful Compile
                txtError.ForeColor = Color.Blue;
                txtError.Text = "Success!";

                foreach (string text in par.ReferencedAssemblies)
                {
                    txtError.Text += text;
                }
            }

        }

        private void WriteResultsToFile(string results)
        {
            //Code to capture Results
            try
            {
                //Check if file already exists
                if (File.Exists(ResultsFile))
                {
                    //File Exists - delete file
                    File.Delete(ResultsFile);
                }
                
                //Code to write results
                var resultsWriter = new StreamWriter(ResultsFile);

                resultsWriter.Write(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred trying to write results file!");
                if (File.Exists(ErrorFile))
                {
                    File.AppendAllText(ErrorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(ErrorFile, ex.Message);
                }
            }
        }

        /// <summary>
        /// Read execution results from a file.
        /// </summary>
        private void ReadResultsFromFile()
        {
            //Try to read file
            try
            {
                //Check if file exists
                if (File.Exists(ResultsFile))
                {
                    {
                        //Code to read results
                        var resultsReader = new StreamReader(ResultsFile);
                        
                        //Write Results to Screen?
                        //txtResults.Text = resultsReader.ReadToEnd();
                    }
                }
                else
                {
                    MessageBox.Show("The results file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred trying to read results file!");
                if (File.Exists(ErrorFile))
                {
                    File.AppendAllText(ErrorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(ErrorFile, ex.Message);
                }
            }
        }

        #region Sample program loading

        private void automatonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("automaton");
        }

        private void binrepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("binrep");
        }

        private void fibonacciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("fibonacci");
        }

        private void jacobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("jacobi");
        }

        private void mandelbrotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("mandelbrot");
        }

        private void movingedgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("movingedge");
        }

        private void multiplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("multiply");
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("sort");
        }

        private void sortinsertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("sortinsertion");
        }

        private void taxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("tax");
        }

        private void triviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOneilCode("trivia");
        }

        private void LoadOneilCode(string fileName)
        {
            //Try to read file
            try
            {
                //Check if file exists
                if (File.Exists(Environment.CurrentDirectory + "\\oneilcode\\" + fileName + ".txt"))
                {
                    //Code to read results
                    var resultsReader =
                        new StreamReader(Environment.CurrentDirectory + "\\oneilcode\\" + fileName + ".txt");

                    txtOneilCode.Clear();

                    //Write O'Neil code to to Screen?
                    txtOneilCode.Text = resultsReader.ReadToEnd();

                    resultsReader.Close();

                    RtbColor();

                    _output = fileName;
                }
                else
                {
                    MessageBox.Show("The " + fileName + " file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred trying to read " + fileName + " file!");
                if (File.Exists(ErrorFile))
                {
                    File.AppendAllText(ErrorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(ErrorFile, ex.Message);
                }
            }
        }

        #endregion

        private void txtOneilCode_TextChanged(object sender, EventArgs e)
        {
            /*
            if (txtOneilCode.Text.Contains("hi"))
            {
                txtOneilCode.Select(txtOneilCode.Text.IndexOf("hi"), "hi".Length);
                txtOneilCode.SelectionColor = Color.Aqua;
                txtOneilCode.Select("hi".Length, 0);
            }
             * */
        }
    }
}