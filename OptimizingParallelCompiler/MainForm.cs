using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OptimizingParallelCompiler
{
    public partial class MainForm : Form
    {
        private List<string> _reserveWords;
        private Thread _wordColoringThread;
 
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

            _wordColoringThread = new Thread(RtbColor);
            _wordColoringThread.Start();
        }

        private void RtbColor()
        {
            lock (richTextBox1)
            {
                Console.WriteLine(richTextBox1.SelectionLength);

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
            }
        }

        private Color ColorForWord(string variable, string word)
        {
            if (variable == word)
                return Color.DodgerBlue;

            return Color.Black;
        }
    }
}
