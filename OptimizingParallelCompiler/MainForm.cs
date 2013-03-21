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
        private List<string> _foundRedundantVariables;
        private List<string> _checkedRedundantVariables;
        private string _possibleLeftSideRedundant;
        private string _possibleRightSideRedundant;
        private List<string> _boundVariables;
        private List<string> _incrementalVariables;
        private List<ThreeOPCreation> letOPCode = new List<ThreeOPCreation>();
        private List<ThreeOPCreation> intStatements = new List<ThreeOPCreation>();

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
            var code = new List<string>(txtDeadCode.Lines);
           
            Parser.Transform(code, letOPCode, intStatements);

            //clears the box of previous everything
            txtCSharpCode.Clear();

            //puts the transformed code to the other text box for visual inspection
            foreach (var lines in code)
            {
                txtCSharpCode.Text += lines + "\n";
            }

            tabFirstTransform.SelectTab(2);

            compileToolStripMenuItem.Enabled = true;
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
                    txtError.Clear();

                    //Write O'Neil code to to Screen?
                    txtOneilCode.Text = resultsReader.ReadToEnd();

                    resultsReader.Close();

                    //RtbColor();

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

            intStatements.Clear();
            letOPCode.Clear();

            ThreeOPConverter.Transform(threeOPCode, intStatements, letOPCode);

            rtbThreeOPCode.Clear();

            foreach (var variable in threeOPCode)
            {
                if (!variable.StartsWith("end"))
                {
                    rtbThreeOPCode.Text += variable + "\n";
                }
                else
                {
                    rtbThreeOPCode.Text += variable;
                }
            }

            tabFirstTransform.SelectTab(0);

            deadCodeRemovalToolStripMenuItem.Enabled = true;
        }

        private void RemoveRedundentStatements(List<string> code, int CodePosition)
        {
            //create list of lines of code
            var lines = new List<string>(code.ToList());

            //set redundentStatementFound = false;
            bool redundentStatementFound = false;

            //Loop through all lines
            foreach (string line in code)
            {
                //do not recheck lines that you have already checked

                //get current index of line in code
                int currentIndex;
                currentIndex = code.FindIndex(s => s == line);
                if (currentIndex <= CodePosition)
                {
                    //skip, you already checked those lines
                }
                else //continue
                {
                    //trim tab and any spaces
                    string currentline = line.Trim(' ', '\t');

                    //check if line starts with "var"
                    if (currentline.IndexOf("let", StringComparison.Ordinal) == 0)
                    {
                        //the statement could contain a variable that has redundancy
                        //parse the let statement ex. let t_1 = idx

                        //remove accidental 2 and 3 spaces from line
                        currentline = currentline.Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");

                        //create an array out of the line, split by spaces
                        string[] lineArray;
                        lineArray = line.Split(' ');

                        //create temp varibles a check if far is already been checked
                        string LeftSide = "";
                        LeftSide = lineArray[1];

                        string RightSide = "";
                        //TO DO right side is everything after equal sign?
                        // = ""

                        if (_foundRedundantVariables.Contains(LeftSide) || _checkedRedundantVariables.Contains(LeftSide))
                        {
                            //the var was already checked, do nothing continue to next line in for loop
                        }
                        else
                        {
                            //add to checked list
                            _checkedRedundantVariables.Add(LeftSide);

                            //Call function to check for redundency
                            if (removeRedundantStatements(LeftSide, RightSide, code, currentIndex + 1) == true)
                            {
                                //redundant statements removed, add to list
                                _foundRedundantVariables.Add(LeftSide);
                            }
                        }
                    }
                    else
                    {
                        //do nothing and continue to the next line
                    }
                }   
            }
        }

        bool removeRedundantStatements(string LeftSide, string RightSide, List<string> code, int startingIndex)
        {
            bool redundencysFound = false;

            //create variable to determin if the variable stored in "LeftSide" is reset to anything
            bool variableValueChange = false;

            //starting at startingIndex, look for statements where the variable stored in "LeftSide" is used
            //continue until the variable is reset,
            //you get to the end of the code,
            int counter = startingIndex;
            while ((counter <= code.Count - 1) && variableValueChange == false)
            {
                //trim tab and any spaces
                string currentline = code[counter].Trim(' ', '\t');

                //check to see if the current line has the variable in it.
                if (currentline.Contains(LeftSide))
                {
                    //split statement
                    //remove accidental 2 and 3 spaces from line
                    currentline = currentline.Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");

                    //create an array out of the line, split by spaces
                    string[] lineArray;
                    lineArray = currentline.Split(' ');

                    //create temp varibles a check if far is already been checked
                    //compare index of equal side and left side

                    //see if there is an equal sign. 
                    if (currentline.Contains("=") == false)
                    {
                        //no equal sign, we know the variable isn't being changed
                    }
                    else
                    {
                       //see if variable is in left or right side of equal sign
                        int indexOfEqual;
                        int indexOfVar;
                        indexOfEqual = currentline.IndexOf("=");
                        indexOfVar = currentline.IndexOf(LeftSide);

                        if (indexOfVar < indexOfEqual)
                        {
                            //variable is on left side, high probability of change
                            variableValueChange = true;
                        }
                        else
                        {
                            //variable is on right side, so the value hasn't changed
                            //we can change this variable to the right side
                            code[counter].Replace(LeftSide, RightSide);

                            //a redundancy was found! redundencysFound

                            //continue until end of code is reached, or the variable is found on left side further in code.
                        }
                    }   
                }
                else
                {
                    //done, go to next line
                }
            }
            return redundencysFound;
        }

        void getBoundAndIncrementalVariables(List<string> code)
        {
            //get bound and incremental variables and store in a list 
            //then we will use that list to look for the incrementer and it's redundencies
            var lines = new List<string>(code.ToList());

            //Loop through all lines, a redundent statement is removed
            foreach (string line in code)
            {
                //trim tab and any spaces
                string currentline = line.Trim(' ', '\t');

                //check if line starts with "if"
                if (currentline.IndexOf("if", StringComparison.Ordinal) == 0)
                {
                    //the statement has a bound value 
                    //parse bound and incremental variable
                    //example:
                    //if (idx <= bound – 1)
                    string incremental = "";
                    string bound = "";

                    //remove accidental 2 and 3 spaces from line
                    currentline = currentline.Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                    string[] lineArray;
                    lineArray = line.Split(' ');
                    incremental = lineArray[1].Replace("(", "");
                    bound = lineArray[3].Replace("-", "").Replace("+", "");

                    //add the incremental and bound variables to lists
                    _boundVariables.Add(bound);
                    _incrementalVariables.Add(incremental);
                }
                else
                {
                    //do nothing and continue to the next line
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deadCodeRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtDeadCode.Text = rtbThreeOPCode.Text;
            tabFirstTransform.SelectTab(1);
            convertToolStripMenuItem.Enabled = true;
        }
    }
}