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
        private string _output = "Out.exe";
        private CompilerResults _results;
        private const string ErrorFile = "error.txt";
        private List<ThreeOPCreation> letOPCode = new List<ThreeOPCreation>();
        private List<ThreeOPCreation> intStatements = new List<ThreeOPCreation>();

        public MainForm()
        {
            InitializeComponent();

            InformationOutput.MainFormTextBox = rtbError;
        }

        /// <summary>
        /// Colors the keywords of the language
        /// Initial code was found online from stackoverflow.com - We have since done a little modification
        /// </summary>
        private void RtbColor()
        {
            Regex regExp = new Regex("^for|^while|if|then|title|", RegexOptions.IgnorePatternWhitespace);

            foreach (Match match in regExp.Matches(txtOneilCode.Text))
            {
                txtOneilCode.Select(match.Index, match.Length);
                txtOneilCode.SelectionColor = Color.Blue;
            }

            txtOneilCode.Select(0, 0);
        }

        /// <summary>
        /// Converts the code from O'Neil Language to C#
        /// </summary>
        /// <param name="sender">Who sent the event</param>
        /// <param name="e">The event thingy</param>
        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var code = new List<string>(rtbThreeOPCode.Lines);
           
            Parser.Transform(code, letOPCode, intStatements);

            //clears the box of previous everything
            txtCSharpCode.Clear();

            txtCSharpCode.Lines = code.ToArray();

            tabTransform.SelectTab("tbpCSharp");

            compileToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Menu item to compile the code to an executable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var codeProvider = CodeDomProvider.CreateProvider("CSharp");

            rtbError.Text = "";

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
                rtbError.ForeColor = Color.DarkRed;
                rtbError.Font = new System.Drawing.Font("Times New Roman", 10f, FontStyle.Italic);

                foreach (CompilerError item in _results.Errors)
                {
                    rtbError.Text += @"line number " + item.Line + @", error num" + item.ErrorNumber +
                                     @" , " + item.ErrorText + Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Successful Compile
                rtbError.ForeColor = Color.Green;
                rtbError.Font = new System.Drawing.Font("Times New Roman", 16f, FontStyle.Bold);
                rtbError.Text = @"Success!";
                runtoolStripMenu.Enabled = true;
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
                    rtbThreeOPCode.Clear();
                    txtCSharpCode.Clear();
                    txtDeadCode.Clear();
                    rtbError.Clear();

                    constantProprogationToolStripMenuItem.Enabled = false;
                    convertToolStripMenuItem.Enabled = false;
                    compileToolStripMenuItem.Enabled = false;
                    runtoolStripMenu.Enabled = false;
                    oPCodeToolStripMenuItem.Enabled = false;

                    //Write O'Neil code to to Screen?
                    txtOneilCode.Text = resultsReader.ReadToEnd();

                    resultsReader.Close();

                    //RtbColor();

                    _output = fileName;

                    rtbError.ForeColor = Color.Black;
                    rtbError.Font = new Font("Times New Roman", 12, FontStyle.Regular);
                }
                else
                {
                    MessageBox.Show(@"The " + fileName + @" file does not exist!");

                    constantProprogationToolStripMenuItem.Enabled = false;
                    convertToolStripMenuItem.Enabled = false;
                    compileToolStripMenuItem.Enabled = false;
                    runtoolStripMenu.Enabled = false;
                    oPCodeToolStripMenuItem.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error has occurred trying to read " + fileName + @" file!");

                File.AppendAllText(ErrorFile, ex.Message);
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
            intStatements.Clear();
            letOPCode.Clear();

            rtbThreeOPCode.Clear();

            rtbThreeOPCode.Lines = ThreeOPConverter.Transform(txtDeadCode.Lines.ToList(), intStatements, letOPCode);

            tabTransform.SelectTab("tbpThreeOPCode");

            convertToolStripMenuItem.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deadCodeRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            txtDeadCode.Lines = ConstantPropagation.Constants(txtOneilCode.Lines.ToList());
            tabTransform.SelectTab("tbpConstantPropragation");
            oPCodeToolStripMenuItem.Enabled = true;
        }

        private void dependencyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDependencyOutput.Clear();
            rtbDependencyOutput.Lines = DependencyOutputGenerator.Generator(txtOneilCode.Lines.ToList());
            tabTransform.SelectTab(0);
            constantProprogationToolStripMenuItem.Enabled = true;
        }
    }
}