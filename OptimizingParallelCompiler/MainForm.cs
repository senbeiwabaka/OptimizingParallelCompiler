using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OptimizingParallelCompiler
{
    public partial class MainForm : Form
    {
        private readonly List<string> _reserveWords;
        private string _output = "Out.exe";
        private CompilerResults _results;
        private int _labelCounter;
        private readonly List<string> _listOfEndFors;
        private const string ErrorFile = "error.txt";
        private Thread _thread;
        private bool _threadStop;

        public MainForm()
        {
            InitializeComponent();

            var listOfEndFors = new List<string>();
            _listOfEndFors = listOfEndFors;

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
                };
        }

        /// <summary>
        /// Colors the keywords of the language
        /// Initial code was found online from stackoverflow.com - We have since done a little modification
        /// </summary>
        private void RtbColor()
        {
            const RichTextBoxFinds options = RichTextBoxFinds.MatchCase;

            foreach (var reserveWord in _reserveWords)
            {
                var start = 0;
                
                start = txtOneilCode.Find(reserveWord, start, options);

                var count = txtOneilCode.Lines.Count(x => x.Contains(reserveWord));
                while (count > 0)
                {
                    txtOneilCode.SelectionStart = start;
                    txtOneilCode.SelectionLength = reserveWord.Length;
                    txtOneilCode.SelectionColor = Color.MediumBlue;

                    start = txtOneilCode.Find(reserveWord, start + reserveWord.Length, options);

                    --count;
                }
            }

            txtOneilCode.SelectionStart = 0;
            txtOneilCode.SelectionLength = 0;
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char) Keys.Enter))
            {
                //RtbColor();
            }
        }

        /// <summary>
        /// Converts the code from O'Neil Language to C#
        /// </summary>
        /// <param name="sender">Who sent the event</param>
        /// <param name="e">The event thingy</param>
        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var code = new List<string>(txtOneilCode.Lines);

            //starts a new thread for doing code analysis for making code 3 op and dead code removal
            _thread = new Thread(() => ThreeOPCode(code));
            _thread.Start();

            code[0] = code[0].Replace("title", "//");
            const string usingstatements = "using System;\n" + "class Program\n" + "{";
            code.Insert(1, usingstatements);

            code.ForEach(s => 
                {
                    var other = s;
                    s = s.TrimEnd('\t', ' ');
                    var count = s.Count(x => x.Equals('\t') || x.Equals(' '));
                    s = s.TrimStart('\t', ' ');

                    if (s.IndexOf("while", StringComparison.Ordinal) == 0)
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
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                        ++_labelCounter;

                        var whilecount = code.Count(s1 => s1.Contains("while") && !s1.Contains("endwhile"));
                        var space = index;
                        while (whilecount > 0)
                        {
                            var whileindex = code.FindIndex(space,
                                                            s1 => s1.Contains("while") && !s1.Contains("endwhile"));
                            var endwhile = code.FindIndex(index, s1 => s1.Contains("endwhile"));

                            if (whilecount > 1)
                            {
                                endwhile = code.FindIndex(endwhile + 1, s1 => s1.Contains("endwhile"));
                            }

                            if (whileindex > index && whileindex < endwhile)
                            {
                                space = whileindex;

                                var tcount = code[whileindex].Count(x => x.Equals('\t'));
                                string nestedTabCount = null;
                                for (var i = 0; i < tcount; ++i)
                                {
                                    nestedTabCount += "\t";
                                }

                                label = "label" + _labelCounter;
                                var whileif = nestedTabCount + label + ":\n" + code[whileindex];
                                ++_labelCounter;
                                sentence = "goto " + label + ";\n";
                                statement = "label" + _labelCounter + ":";
                                whileif = whileif.Replace("while", "if");
                                whileif += " goto label" + _labelCounter + ";";
                                sentence = nestedTabCount + sentence + statement;

                                code[whileindex] = sentence;
                                ++_labelCounter;

                                code[endwhile] = whileif;
                            }

                            --whilecount;
                        }

                        var endindex = code.FindIndex(x => x.Contains("endwhile"));
                        code[endindex] = newif;
                    }
                    else if (s.IndexOf("endfor", StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);
                        //take the last string off the list
                        code[index] = _listOfEndFors[_listOfEndFors.Count - 1];
                        _listOfEndFors.RemoveAt(_listOfEndFors.Count - 1);
                    }
                    else if (s.IndexOf("for", StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);

                        //variable for list of statements
                        string endForString;
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

                        var id = s.IndexOf("for", StringComparison.Ordinal) + 4;
                        var end = s.IndexOf("to", StringComparison.Ordinal) - 1;
                        var value = "\t\t" + s.Substring(id, end - id) + ";\n";
                        var value1 = "\t" + value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2) + "=" +
                                        value.Substring(2, value.IndexOf("=", StringComparison.Ordinal) - 2) + " + 1;";
                        string bound;

                        var a1 = s.IndexOf("to", System.StringComparison.Ordinal) + 2;
                        var a2 = s.Length - 1;
                        var a3 = s.IndexOf("to", System.StringComparison.Ordinal) + 1;
                        var a4 = a2 - a3;

                        bound = s.Substring(a1, a4);
                        var label = "Label" + _labelCounter.ToString();
                        var endForStringPart1 = value1;

                        var sentence = value + "\t" + label + ":";

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
                        var idx = value.Substring(2, value.IndexOf("=", System.StringComparison.Ordinal) - 2);

                        var endForStringPart2 = "if (" + idx + " <= " + bound + ") goto " + label + ";";
                        endForStringPart2 += "\n";

                        //var temp1 = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= ";
                        //var temp2 = test[i].Substring(number, number1 - number + 1);
                        //var temp3 = ") goto " + label + ";";

                        //var last = "\tif( " + value.Substring(2, value.IndexOf("=") - 2) + " <= " +
                        //              test[i].Substring(number, number1 - number + 1) +
                        //              " ) goto " + label + ";";

                        code[index] = sentence;
                        endForString = endForStringPart1 + "\n";

                        endForString += "\t" + endForStringPart2;

                        //test.Insert(i + 2, endForStringPart1);
                        //test.Insert(i + 3, last);
                        _listOfEndFors.Add(endForString);
                        ++_labelCounter;
                    }
                    else if (s.IndexOf("var", System.StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);
                        code[index] = "\tstatic void Main()\n\t{";
                    }
                    else if (s.IndexOf("end", System.StringComparison.Ordinal) == 0 && s.Length == 3)
                    {
                        var index = code.IndexOf(other);
                        code[index] = "\t}\n}";
                    }
                    else if (s.IndexOf("prompt", System.StringComparison.Ordinal) == 0 || s.IndexOf("print") == 0)
                    {
                        var sentence = s;
                        int index;
                        if (s.IndexOf("prompt", System.StringComparison.Ordinal) == 0)
                        {
                            sentence = sentence.Replace("prompt", "Console.Write(");
                            index = code.IndexOf(other);
                        }
                        else
                        {
                            sentence = sentence.Replace("print", "Console.Write(");
                            index = code.IndexOf(other);
                        }
                        sentence += ");";
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("rem", System.StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("rem", "//");
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }

                    else if (s.IndexOf("list", System.StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        var arrayBegin = sentence.IndexOf("[", System.StringComparison.Ordinal) + 1;
                        var arrayEnd = sentence.IndexOf("]", System.StringComparison.Ordinal);
                        var value = sentence.Substring(arrayBegin, arrayEnd - arrayBegin);
                        sentence = sentence.Substring(arrayEnd + 1, sentence.Length - (arrayEnd + 1));
                        sentence = "int[] " + sentence + " = new int[" + value + "];";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("let", System.StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Substring("let".Length, sentence.Length - "let".Length);
                        sentence = sentence + ";";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("input", System.StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence = sentence.Replace("input", "");
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence + " = Convert.ToInt32(Console.ReadLine());");

                    }
                    else if (s.IndexOf("int", StringComparison.Ordinal) == 0)
                    {
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, s + ";");
                    }
                    else if (s.IndexOf("if", StringComparison.Ordinal) == 0 && s.Contains("then"))
                    {
                        var index = code.IndexOf(other);
                        var statement = s.Substring(s.IndexOf("then", StringComparison.Ordinal) + "then".Length,
                                                   s.Length - (s.IndexOf("then", StringComparison.Ordinal) + "then".Length));
                        var equator = s.Substring(0, s.IndexOf(")", StringComparison.Ordinal) + 1);

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
                            var sentence = s;
                            sentence = sentence.Replace(statement, "");
                            sentence = sentence.Replace("then", statement);
                            sentence += ";";
                            code[index] = code[index].Replace(s, sentence);
                        }
                        else if (statement.Contains("print"))
                        {
                            statement = statement.Replace("print", "Console.Write(");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            code.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else if (statement.Contains("prompt"))
                        {
                            statement = statement.Replace("prompt", "Console.Write(");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ");";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            code.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else if (statement.Contains("let"))
                        {
                            statement = statement.Replace("let", "");
                            var label = "label" + _labelCounter;
                            statement = " goto " + label + "; " + statement;
                            statement += ";";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            code.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                        else
                        {
                            var label = "label" + _labelCounter;
                            statement += "goto " + label + ";";
                            equator += statement;
                            code[index] = code[index].Replace(s, equator);
                            code.Insert(index + 2, label + ":");
                            ++_labelCounter;
                        }
                    }
                    else if (s.IndexOf("goto", StringComparison.Ordinal) == 0)
                    {
                        var sentence = s;
                        sentence += ";";
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("label", StringComparison.Ordinal) == 0 && !s.Contains(":"))
                    {
                        var sentence = s;
                        var index = code.IndexOf(other);
                        sentence += ":";
                        if (s.Contains(" "))
                        {
                            sentence = sentence.Replace("label", "");
                            sentence = sentence.Replace(" ", "");
                        }
                        code[index] = code[index].Replace(s, sentence);
                    }
                    else if (s.IndexOf("begin", StringComparison.Ordinal) == 0)
                    {
                        var statement = s;
                        statement = statement.Replace("begin", "");
                        var index = code.IndexOf(other);
                        code[index] = code[index].Replace(s, statement);
                    }
                });

            //resets the label counter incase you want to transform another code and need to inspect it
            _labelCounter = 0;

            //clears the box of previous everything
            txtCSharpCode.Clear();

            //puts the transformed code to the other text box for visual inspection
            foreach (var lines in code)
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
                if (File.Exists(Environment.CurrentDirectory + @"\oneilcode\" + fileName + ".txt"))
                {
                    //Code to read results
                    var resultsReader =
                        new StreamReader(Environment.CurrentDirectory + @"\oneilcode\" + fileName + ".txt");

                    txtOneilCode.Clear();

                    //Write O'Neil code to to Screen?
                    txtOneilCode.Text = resultsReader.ReadToEnd();

                    resultsReader.Close();

                    RtbColor();

                    _output = fileName;
                }
                else
                {
                    MessageBox.Show(@"The " + fileName + @" file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error has occurred trying to read " + fileName + @" file!");
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
            //Call bat file
            var batPath = Environment.CurrentDirectory + @"\" + _output;
            System.Diagnostics.Process.Start("cmd.exe", "/k " + batPath);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _threadStop = true;
            //_thread.Abort();
            while (_thread.IsAlive)
            {
                _thread.Abort();
            }
        }


        private void ThreeOPCode(List<string> code)
        {
            var lines = new List<string>(code.ToList());

            var index = lines.IndexOf("begin");

            lines.RemoveRange(0, index + 1);

            var stop = false;
            while (_thread.IsAlive && !_threadStop && !stop)
            {


                stop = true;
            }
        }
    }
}