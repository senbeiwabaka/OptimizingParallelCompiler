using System;
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
        private const string ErrorFile = "error.txt";

        public MainForm()
        {
            InitializeComponent();

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
           
            Parser.Transform(code);

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
                txtError.ForeColor = Color.DarkRed;

                foreach (CompilerError item in _results.Errors)
                {
                    txtError.Text += @"line number " + item.Line + @", error num" + item.ErrorNumber +
                                     @" , " + item.ErrorText + Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Successful Compile
                txtError.ForeColor = Color.Green;
                txtError.Text = @"Success!";
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

        private void oPCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var threeOPCode = new List<string>(txtOneilCode.Lines);

            ThreeOPConverter.Transform(threeOPCode);

            txtTransform.Clear();

            foreach (var variable in threeOPCode)
            {
                txtTransform.Text += variable + "\n";
            }


        }
    }
}