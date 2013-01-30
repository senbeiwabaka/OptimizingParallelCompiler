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
        private List<string> _reserveWords;
        private Thread _wordColoringThread;

        const string _errorFile = "error.txt";
        const string _resultsFile = "results.txt";

        public MainForm()
        {
            InitializeComponent();

            _reserveWords = new List<string>()
                {
                    "let",
                    "title",
                    "var",
                    "int",
                    "list",
                    "rem",
                    "label",
                    "if",
                    "(",
                    ")",
                    "then",
                    "goto",
                    "input",
                    "[",
                    "]",
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
                    "%"
                };

            //_wordColoringThread = new Thread(RtbColor);
            //_wordColoringThread.Start();
        }

        private void RtbColor()
        {
            int index = txtOneilCode.Text.IndexOf("if");

            Console.WriteLine(index);

            for (int i = 0; i < txtOneilCode.Lines.Length; i++)
            {
                var words = txtOneilCode.Lines[i];

                for (int k = 0; k < _reserveWords.Count; k++)
                {
                    var start = txtOneilCode.Lines[i].IndexOf(_reserveWords[k]);
                    if (start > -1)
                    {
                        txtOneilCode.Select(start, _reserveWords[k].Length);
                        txtOneilCode.SelectionColor = Color.DodgerBlue;
                        txtOneilCode.Select(0, 0);
                    }
                }
            }

            /*
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                var text = richTextBox1.Lines.ToList();

                foreach (var variable in text)
                {
                    foreach (var word in _reserveWords)
                    {
                        richTextBox1.Select(richTextBox1.Lines[i].IndexOf(variable), variable.Length);
                        richTextBox1.SelectionColor = ColorForWord(variable, word);
                    }
                }
            }
             * */
        }

        private Color ColorForWord(string selectedWord, string reservedWord)
        {
            if (selectedWord != reservedWord)
            {
                return Color.Black;
            }

            return Color.DodgerBlue;
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
            var words = txtOneilCode.Text.ToArray();

            var test = new List<string>();

            foreach (var line in txtOneilCode.Lines)
            {
                test.Add(line);
            }

            

            Console.WriteLine("help");
        }


        /// <summary>
        /// Menu item to run code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Run .created exe and get results.
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void runCode()
        {

        }

        private void writeResultsToFile(string results)
        {
            //Code to capture Results
            try
            {
                //Check if file already exists
                if (File.Exists(_resultsFile))
                {
                    //File Exists - delete file
                    File.Delete(_resultsFile);
                }
                
                //Code to write results
                StreamWriter resultsWriter = new StreamWriter(_resultsFile);

                resultsWriter.Write(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured trying to write results file!");
                if (File.Exists(_errorFile))
                {
                    File.AppendAllText(_errorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(_errorFile, ex.Message);
                }
            }
        }

        /// <summary>
        /// Read execution results from a file.
        /// </summary>
        private void readResultsFromFile()
        {
            //Try to read file
            try
            {
                //Check if file exists
                if (File.Exists(_resultsFile))
                {
                    {
                        //Code to read results
                        StreamReader resultsReader = new StreamReader(_resultsFile);
                        
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
                MessageBox.Show("An error has occured trying to read results file!");
                if (File.Exists(_errorFile))
                {
                    File.AppendAllText(_errorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(_errorFile, ex.Message);
                }
            }
        }

        private void automatonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("automaton");
        }

        private void binrepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("binrep");
        }

        private void fibonacciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("fibonacci");
        }

        private void jacobiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("jacobi");
        }

        private void mandelbrotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("mandelbrot");
        }

        private void movingedgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("movingedge");
        }

        private void multiplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("multiply");
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("sort");
        }

        private void sortinsertionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("sortinsertion");
        }

        private void taxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("tax");
        }

        private void triviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadOneilCode("trivia");
        }
        private void loadOneilCode(string fileName)
        {
            //Try to read file
            try
            {
                //Check if file exists
                if (File.Exists(Environment.CurrentDirectory + "\\oneilcode\\" + fileName + ".txt"))
                {
                    {
                        //Code to read results
                        StreamReader resultsReader = new StreamReader(Environment.CurrentDirectory + "\\oneilcode\\" + fileName + ".txt");

                        //Write Oneil code to to Screen?
                        txtOneilCode.Text = resultsReader.ReadToEnd();
                    }
                }
                else
                {
                    MessageBox.Show("The " + fileName + " file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured trying to read " + fileName + " file!");
                if (File.Exists(_errorFile))
                {
                    File.AppendAllText(_errorFile, ex.Message);
                }
                else
                {
                    File.WriteAllText(_errorFile, ex.Message);
                }
            }
        }
    }
}