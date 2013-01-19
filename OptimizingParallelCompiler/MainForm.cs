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

            //_wordColoringThread = new Thread(RtbColor);
            //_wordColoringThread.Start();
        }

        private void RtbColor()
        {
            int index = richTextBox1.Text.IndexOf("if");

            Console.WriteLine(index);

            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                var words = richTextBox1.Lines[i];

                for (int k = 0; k < _reserveWords.Count; k++)
                {
                    var start = richTextBox1.Lines[i].IndexOf(_reserveWords[k]);
                    if (start > -1)
                    {
                        richTextBox1.Select(start, _reserveWords[k].Length);
                        richTextBox1.SelectionColor = Color.DodgerBlue;
                        richTextBox1.Select(0, 0);
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
            var words = richTextBox1.Text.ToArray();

            var test = new List<string>();

            foreach (var line in richTextBox1.Lines)
            {
                test.Add(line);
            }

            

            Console.WriteLine("help");
        }
    }
}