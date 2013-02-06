﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OptimizingParallelCompiler
{
    public partial class MainForm : Form
    {
        private readonly List<string> _reserveWords;
        private string _output = "Out.exe";
        private CompilerResults _results;
        private int _labelCounter;
        private List<string> _ListOfEndFors;
        private const string ErrorFile = "error.txt";
        private const string ResultsFile = "results.txt";
        private const string BatFile = "batfile.bat";

        public MainForm()
        {
            InitializeComponent();

            List<string> ListOfEndFors = new List<string>();
            _ListOfEndFors = ListOfEndFors;

            _reserveWords = new List<string>(25)
                {
                    "title",
                    "var",
                    "label",
                    "goto",
                    "let",
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
                    "begin",
                    "==",
                };

            
        }

        private void RtbColor()
        {
            foreach (var reserveWord in _reserveWords)
            {
                var start = 0;
                const RichTextBoxFinds options = RichTextBoxFinds.MatchCase;
                start = txtOneilCode.Find(reserveWord, start, options);
                while (start >= 0)
                {
                    txtOneilCode.SelectionStart = start;
                    txtOneilCode.SelectionLength = reserveWord.Length;
                    txtOneilCode.SelectionColor = Color.DodgerBlue;

                    var current = start + reserveWord.Length;
                    if (current < txtOneilCode.TextLength)
                        start = txtOneilCode.Find(reserveWord, current, options);
                    else
                        break;
                }
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char) Keys.Enter))
            {
                RtbColor();
            }
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var test = new List<string>(txtOneilCode.Lines);

            

            test[0] = test[0].Replace("title", "//");
            const string usingstatements = "using System;\n" + "class Program\n" + "{";
            test.Insert(1, usingstatements);

            var works = test.ToList().FindIndex(x => x.Contains("let"));
            txtError.AppendText(works + "\n");
            
            for (var i = 2; i < test.Count; i++)
            {
                test[i] = test[i].TrimStart(' ', '\t');
                test[i] = test[i].TrimEnd(' ', '\t');



                foreach (var reserveWord in _reserveWords)
                {
                    if (test[i].Contains(reserveWord))
                    {
                        if (reserveWord.Equals("while", StringComparison.Ordinal) && !test[i].Contains("end"))
                        {
                            txtError.AppendText(Environment.NewLine + reserveWord + " : " +
                                                test[i] + " : " + string.Equals(reserveWord, "while"));
                            var statement = test[i];
                            var label = "label" + _labelCounter;
                            ++_labelCounter;
                            var label1 = "label" + _labelCounter;
                            var sentence = "goto " + label + ";\n";
                            sentence += label1 + ":";
                            test[i] = sentence;
                            var other = new List<string>(test.GetRange(0, test.Count));
                            for (var x = 0; x < other.Count; ++x)
                            {
                                other[x] = other[x].Trim('\t', ' ');
                            }
                            var endWhile = other.IndexOf("endwhile");

                            test[endWhile] = label + ":";
                            statement = statement.Replace("while", "if");
                            statement = statement.TrimEnd(' ');
                            statement = statement + " goto " + label1 + ";";

                            test[endWhile] += "\n" + statement;

                            ++_labelCounter;
                        }
                        else if (reserveWord.Equals("for"))
                        {
                            string endForStringPart1;
                            string endForStringPart2;

                            //variable for list of statements
                            string endForString = "";
                            //_tempEndFor =;

                            //look ahead until you find the end for
                            //int tempI = i;
                            //int endOfFor = 0;
                            //bool endForFound = false;
                            //while (endForFound == false)
                            //{
                            //    if (test[tempI] == "endfor")
                            //    {
                            //        endForFound = true;
                            //        endOfFor = tempI;
                            //    }
                            //    //add statement to list
                            //    //stmtList += "\n\t" + test[tempI];
                            //    tempI++;
                            //}



                            //test[i] = test[i].Replace("\t", string.Empty);
                            var id = test[i].IndexOf("for") + reserveWord.Count() + 1;
                            var end = test[i].IndexOf("to") - 1;
                            var value = "\t\t" + test[i].Substring(id, end - id) + ";\n";
                            var value1 = "\t" + value.Substring(2, value.IndexOf("=") - 2) + "=" +
                                            value.Substring(2, value.IndexOf("=") - 2) + " + 1;";
                            string bound = "";

                            int a1 = test[i].IndexOf("to") + 2;
                            int a2 = test[i].Length - 1;
                            int a3 = test[i].IndexOf("to") + 1;
                            int a4 = a2 - a3;

                            bound = test[i].Substring(a1, a4);
                            var label = "Label" + _labelCounter.ToString();
                            endForStringPart1 = value1;
                            
                            var sentence = value + "\t" + label + ":";
                            var number = test[i].IndexOf("to") + 2;
                            //test[i] = test[i].TrimEnd(' ');
                            var lastvalue = test[i].Last();
                            var number1 = test[i].IndexOf(lastvalue);

//Oneil Code                    //Translation
//for idx = 0 to bound – 1      let idx = 0
//                              label L_0
//let array[idx] = -1           let array[idx] = -1 
//endfor                        let idx = idx + 1 
//                              if (idx <= bound – 1) then goto L_0
                             
//for i = 1 to size -1          "\t\ti = 1;\n\tLabel3:"
//statements                    statements
//endfor                        "\ti =i  + 1;" + "\n"      //this is endForStringPart1
//                               + "\n"     this is endForStringPart2

                            //idx
                            string idx = value.Substring(2, value.IndexOf("=") - 2);
                            



                            endForStringPart2 = "if (" + idx + " <= " + bound + ") then goto " + label + ";";

                            endForStringPart2 += "\n";

                            //var temp1 = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= ";
                            //var temp2 = test[i].Substring(number, number1 - number + 1);
                            //var temp3 = ") goto " + label + ";";

                            //var last = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= " +
                            //              test[i].Substring(number, number1 - number + 1) +
                            //              " ) goto " + label + ";";

                            test[i] = sentence;
                            endForString = endForStringPart1+="\n";
                            
                            endForString += "\t" + endForStringPart2;

                            //test.Insert(i + 2, endForStringPart1);
                            //test.Insert(i + 3, last);
                            _ListOfEndFors.Add(endForString);
                            ++_labelCounter;
                        }
                        else if (reserveWord.Equals("var"))
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
                            //take the last string off the list
                            test[i] = _ListOfEndFors[_ListOfEndFors.Count - 1];
                            _ListOfEndFors.RemoveAt(_ListOfEndFors.Count - 1);
                           
                        }
                        else if (reserveWord.Equals("input") && test[i].IndexOf("input") == 0)
                        {
                            var sentence = test[i];
                            sentence = sentence.Replace("input", "");
                            test[i] = "\t" + sentence + " = Convert.ToInt32(Console.ReadLine());";
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
                        else if (reserveWord.Equals("=="))
                        {
                            test[i] = test[i].Replace("==", "!=");
                        }
                        else if (reserveWord.Equals("goto"))
                        {
                            //var something = test[i].Length - 1;
                            //test[i] = test[i].Replace(test[i].ElementAt(something), ';');
                        }
                        else if (reserveWord.Equals("label"))
                        {
                            //var something = test[i].Length - 1;
                            //test[i] = test[i].Replace(test[i].ElementAt(something), ':');
                        }
                    }
                    else if (test[i].Contains("begin"))
                    {
                        test[i] = test[i].Replace("\t", "");
                        test[i] = "\t";
                    }
                }
            }
             

            //Parser.Change(ref test, _reserveWords, txtOneilCode.Text);

            txtCSharpCode.Clear();

            foreach (var lines in test)
            {
                txtCSharpCode.Text += lines + "\n";
            }
        }

        /// <summary>
        ///     Menu item to compile the code to an executable
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

            //txtError.Text = par.LinkedResources + Environment.NewLine;

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
                txtError.BackColor = Color.Blue;
                txtError.ForeColor = Color.White;
                txtError.Text = "Success!";

                foreach (var text in par.ReferencedAssemblies)
                {
                    txtError.Text += text;
                }
            }
        }

        private void WriteBatFile(string exePath, string exeName)
        {
            //Code to capture Results
            try
            {
                //Check if file already exists
                if (File.Exists(BatFile))
                {
                    //File Exists - delete file
                    File.Delete(BatFile);
                }

                //Code to write results
                var resultsWriter = new StreamWriter(BatFile);

                resultsWriter.Write("start " + exePath + " %1" + Environment.NewLine  +
                    "ECHO Press any key to exit" + Environment.NewLine  +
   "PAUSE >NUL" + Environment.NewLine +
   "EXIT /B" + Environment.NewLine);

                resultsWriter.Close();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Create bat file
            //WriteBatFile(_output + ".exe", _output);

            //Call bat file
            var batPath = Environment.CurrentDirectory + "\\" + _output;
            System.Diagnostics.Process.Start("cmd.exe", "/k " + batPath);
            //System.Diagnostics.Process p = new System.Diagnostics.Process();
            //p.StartInfo.WorkingDirectory = firebirdInstallationPath;
            //p.StartInfo.FileName = _output + ".exe";
            //p.Start();
            //p.WaitForExit();
        }
    }
}