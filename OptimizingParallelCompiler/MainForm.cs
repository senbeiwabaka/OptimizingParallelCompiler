using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace OptimizingParallelCompiler
{
    public partial class MainForm : Form
    {
        private readonly List<string> _reserveWords;

        const string ErrorFile = "error.txt";
        const string ResultsFile = "results.txt";

        public MainForm()
        {
            InitializeComponent();

            _reserveWords = new List<string>(31)
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
                    "endwhile",
                    "then",
                    "goto",
                    "input",
                    "print",
                    "prompt",
                    "<",
                    "|",
                    "==",
                    "+",
                    "-",
                    "<=",
                    ">=",
                    "!=",
                    "*",
                    "/",
                    "%",
                    "end",
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
                            Console.WriteLine(sentence);
                            sentence = "//" + sentence;
                            Console.WriteLine(sentence);
                            test[i] = sentence;
                            Console.WriteLine(test[i]);
                            if (i == 0)
                            {
                                const string usingstatements = "System;\n"  + "class Program\n" + "{";
                                test.Insert(1, usingstatements);
                            }
                        }
                        else if (reserveWord.Equals("while"))
                        {
                            
                        }
                        else if (reserveWord.Equals("for"))
                        {
                            
                        }
                        else if(reserveWord.Equals("var"))
                        {
                            test[i] = "static void Main()\n{";
                        }
                        else if (reserveWord.Equals("end") && test[i].Equals(reserveWord))
                        {
                            test[i] = "}";
                        }
                    }
                }  
            }

            txtCSharpCode.Clear();

            foreach (var lines in test)
            {
                txtCSharpCode.Text += lines + "\n";
            }

            Console.WriteLine("help");
        }

        /// <summary>
        /// Menu item to run code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

                    //Write O'Neil code to to Screen?
                    txtOneilCode.Text = resultsReader.ReadToEnd();

                    resultsReader.Close();

                    RtbColor();
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