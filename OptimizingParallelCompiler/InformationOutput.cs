using System;
using System.Windows.Forms;

namespace OptimizingParallelCompiler
{
    public static class InformationOutput
    {
        public static RichTextBox MainFormTextBox { get; set; }

        public static void InformationPrint(string print)
        {
            MainFormTextBox.AppendText(print + Environment.NewLine);
        }
    }
}
