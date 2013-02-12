using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                    "if",
                    "endfor",
                    "then",
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
                //RtbColor();
            }
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var test = new List<string>(txtOneilCode.Lines);

            test[0] = test[0].Replace("title", "//");
            const string usingstatements = "using System;\n" + "class Program\n" + "{";
            test.Insert(1, usingstatements);

            //var works = test.ToList().FindIndex(x => x.Contains("let"));
            //txtError.AppendText(works + "\n");

            test.ForEach(delegate(string s)
                {
                    var other = s;
                    s = s.TrimEnd('\t', ' ');
                    var count = s.Count(x => x.Equals('\t'));
                    s = s.TrimStart('\t', ' ');

                    if (s.IndexOf("while") == 0)
                    {
                        string tab = null;
                        for (var i = 0; i < count; i++)
                        {
                            tab += "\t";
                        }

                        var label = "label" + _labelCounter;
                        var newif = tab + label + ":\n" + s;
                        ++_labelCounter;
                        var sentence = "goto " + label + ";\n";
                        var statement = "label" + _labelCounter + ":";
                        newif = newif.Replace("while", "if");
                        newif += " goto label" + _labelCounter + ";";
                        statement = tab + statement;

                        sentence += statement;
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence);
                        ++_labelCounter;

                        var whilecount = test.Count(s1 => s1.Contains("while") && !s1.Contains("endwhile"));
                        txtError.AppendText("while : " + whilecount.ToString() + "\n");
                        var endwhile = test.Count(s1 => s1.Contains("endwhile"));
                        txtError.AppendText("endwhile : " + endwhile.ToString() + "\n");
                        var space = index;
                        while (whilecount > 0)
                        {
                            var whileindex = test.FindIndex(space,
                                                            s1 => s1.Contains("while") && !s1.Contains("endwhile"));
                            var endwhileindex = test.FindIndex(index, s1 => s1.Contains("endwhile"));

                            if (whilecount > 1)
                            {
                                endwhileindex = test.FindIndex(endwhileindex + 1, s1 => s1.Contains("endwhile"));
                            }

                            if (whileindex > index && whileindex < endwhileindex)
                            {
                                space = whileindex;

                                var tcount = test[whileindex].Count(x => x.Equals('\t'));
                                string nestedTabCount = null;
                                for (var i = 0; i < tcount; ++i)
                                {
                                    nestedTabCount += "\t";
                                }

                                txtError.AppendText("true\n");
                                label = "label" + _labelCounter;
                                var whileif = nestedTabCount + label + ":\n" + test[whileindex];
                                ++_labelCounter;
                                sentence = "goto " + label + ";\n";
                                statement = "label" + _labelCounter + ":";
                                whileif = whileif.Replace("while", "if");
                                whileif += " goto label" + _labelCounter + ";";
                                sentence = nestedTabCount + sentence + statement;

                                test[whileindex] = sentence;
                                ++_labelCounter;

                                test[endwhileindex] = whileif;
                            }

                            --whilecount;
                        }

                        var endindex = test.FindIndex(x => x.Contains("endwhile"));
                        var tabcount = test[endindex].Count(x => x.Equals('\t'));
                        txtError.AppendText("tab count : " + tabcount + "\n");
                        test[endindex] = newif;
                    }
                    else if (s.IndexOf("endfor") == 0)
                    {
                        var index = test.IndexOf(other);
                        //take the last string off the list
                        test[index] = _ListOfEndFors[_ListOfEndFors.Count - 1];
                        _ListOfEndFors.RemoveAt(_ListOfEndFors.Count - 1);
                    }
                    else if (s.IndexOf("for") == 0)
                    {
                        var index = test.IndexOf(other);
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
                        //var id = s.IndexOf("for") + reserveWord.Count() + 1;
                        var id = s.IndexOf("for") + 4;
                        var end = s.IndexOf("to") - 1;
                        var value = "\t\t" + s.Substring(id, end - id) + ";\n";
                        var value1 = "\t" + value.Substring(2, value.IndexOf("=") - 2) + "=" +
                                        value.Substring(2, value.IndexOf("=") - 2) + " + 1;";
                        string bound = "";

                        var a1 = s.IndexOf("to") + 2;
                        int a2 = s.Length - 1;
                        int a3 = s.IndexOf("to") + 1;
                        int a4 = a2 - a3;

                        bound = s.Substring(a1, a4);
                        var label = "Label" + _labelCounter.ToString();
                        endForStringPart1 = value1;

                        var sentence = value + "\t" + label + ":";
                        var number = s.IndexOf("to") + 2;
                        //test[i] = test[i].TrimEnd(' ');
                        var lastvalue = s.Last();
                        var number1 = s.IndexOf(lastvalue);

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

                        endForStringPart2 = "if (" + idx + " <= " + bound + ") goto " + label + ";";
                        endForStringPart2 += "\n";

                        //var temp1 = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= ";
                        //var temp2 = test[i].Substring(number, number1 - number + 1);
                        //var temp3 = ") goto " + label + ";";

                        //var last = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= " +
                        //              test[i].Substring(number, number1 - number + 1) +
                        //              " ) goto " + label + ";";

                        test[index] = sentence;
                        endForString = endForStringPart1 += "\n";

                        endForString += "\t" + endForStringPart2;

                        //test.Insert(i + 2, endForStringPart1);
                        //test.Insert(i + 3, last);
                        _ListOfEndFors.Add(endForString);
                        ++_labelCounter;
                    }
                    else if (s.IndexOf("var") == 0)
                    {
                        var index = test.IndexOf(other);
                        test[index] = "\tstatic void Main()\n\t{";
                    }
                    else if (s.IndexOf("end") == 0 && s.Length == 3)
                    {
                        var index = test.IndexOf(other);
                        test[index] = "\t}\n}";
                    }
                    else if (s.IndexOf("prompt") == 0 || s.IndexOf("print") == 0)
                    {
                        var sentence = s;
                        int index;
                        if (s.IndexOf("prompt") == 0)
                        {
                            sentence = sentence.Replace("prompt", "Console.Write(");
                            index = test.IndexOf(other);
                        }
                        else
                        {
                            sentence = sentence.Replace("print", "Console.Write(");
                            index = test.IndexOf(other);
                        }
                        sentence += ");";
                        test[index] = test[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("rem") == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("rem", "//");
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence);
                    }

                    else if (s.IndexOf("list") == 0)
                    {
                        var sentence = s;
                        var arrayBegin = sentence.IndexOf("[") + 1;
                        var arrayEnd = sentence.IndexOf("]");
                        var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                        sentence = sentence.Substring(arrayEnd + 1, sentence.Length - (arrayEnd + 1));
                        sentence = "int[] " + sentence + " = new int[" + value + "];";
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("let") == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Substring("let".Length, sentence.Length - "let".Length);
                        sentence = sentence + ";";
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("input") == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("input", "");
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence + " = Convert.ToInt32(Console.ReadLine());");

                    }
                    else if (s.IndexOf("int") == 0)
                    {
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, s + ";");
                    }
                    else if (s.IndexOf("if") == 0 && s.Contains("then"))
                    {
                        var index = test.IndexOf(other);
                        var statement = s.Substring(s.IndexOf("then") + "then".Length,
                                                   s.Length - (s.IndexOf("then") + "then".Length));
                        var equator = s.Substring(0, s.IndexOf(")") + 1);

                        if (equator.Contains("!="))
                        {
                            equator = equator.Replace("!=", "==");
                        }
                        else if (equator.Contains("=="))
                        {
                            equator = equator.Replace("==", "!=");
                        }
                        else if (equator.Contains("<") && !equator.Contains("<="))
                        {
                            equator = equator.Replace("<", ">=");
                        }
                        else if (equator.Contains(">") && !equator.Contains(">="))
                        {
                            equator = equator.Replace(">", "<=");
                        }
                        else if (equator.Contains("<="))
                        {
                            equator = equator.Replace("<=", ">");
                        }
                        else if (equator.Contains(">="))
                        {
                            equator = equator.Replace(">=", "<");
                        }

                        if (statement.Contains("goto"))
                        {
                            //statement += ";";
                            var sentence = s;
                            sentence = sentence.Replace(statement, "");
                            sentence = sentence.Replace("then", statement);
                            sentence += ";";
                            test[index] = test[index].Replace(s, sentence);
                        }
                        else if (statement.Contains("print"))
                        {
                            statement = statement.Replace("print", "Console.Write(");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            test[index] = test[index].Replace(s, equator);
                            test.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else if (statement.Contains("prompt"))
                        {
                            statement = statement.Replace("prompt", "Console.Write(");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            test[index] = test[index].Replace(s, equator);
                            test.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else if (statement.Contains("let"))
                        {
                            statement = statement.Replace("let", "");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ";";
                            equator += statement;
                            test[index] = test[index].Replace(s, equator);
                            test.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else
                        {
                            var label = "label" + _labelCounter;
                            statement += "goto " + label + ";";
                            equator += statement;
                            test[index] = test[index].Replace(s, equator);
                            test.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                    }
                    else if (s.IndexOf("goto") == 0)
                    {
                        var sentence = s;
                        sentence += ";";
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("label") == 0 && !s.Contains(":"))
                    {
                        var sentence = s;
                        var index = test.IndexOf(other);
                        sentence += ":";
                        if (s.Contains(" "))
                        {
                            sentence = sentence.Replace("label", "");
                            sentence = sentence.Replace(" ", "");
                        }
                        test[index] = test[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("begin") == 0)
                    {
                        var statement = s;
                        statement = statement.Replace("begin", "");
                        var index = test.IndexOf(other);
                        test[index] = test[index].Replace(s, statement);
                    }
                });

            _labelCounter = 0;

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

        private void txtCSharpCode_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Left):
                    break;
                case (MouseButtons.Middle):
                    break;
                case (MouseButtons.Right):
                    txtCSharpCode.Copy();
                    break;
            }
        }
    }
}