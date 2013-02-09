namespace OptimizingParallelCompiler
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOneilCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automatonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binrepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fibonacciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jacobiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mandelbrotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movingedgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multiplyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortinsertionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triviaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOneilCode = new System.Windows.Forms.RichTextBox();
            this.txtCSharpCode = new System.Windows.Forms.RichTextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineNumbersForRichText1 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.lineNumbersForRichText2 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(962, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadOneilCodeToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.compileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadOneilCodeToolStripMenuItem
            // 
            this.loadOneilCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automatonToolStripMenuItem,
            this.binrepToolStripMenuItem,
            this.fibonacciToolStripMenuItem,
            this.jacobiToolStripMenuItem,
            this.mandelbrotToolStripMenuItem,
            this.movingedgeToolStripMenuItem,
            this.multiplyToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.sortinsertionToolStripMenuItem,
            this.taxToolStripMenuItem,
            this.triviaToolStripMenuItem});
            this.loadOneilCodeToolStripMenuItem.Name = "loadOneilCodeToolStripMenuItem";
            this.loadOneilCodeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.loadOneilCodeToolStripMenuItem.Text = "Load Oneil Code";
            // 
            // automatonToolStripMenuItem
            // 
            this.automatonToolStripMenuItem.Name = "automatonToolStripMenuItem";
            this.automatonToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.automatonToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.automatonToolStripMenuItem.Text = "automaton";
            this.automatonToolStripMenuItem.Click += new System.EventHandler(this.automatonToolStripMenuItem_Click);
            // 
            // binrepToolStripMenuItem
            // 
            this.binrepToolStripMenuItem.Name = "binrepToolStripMenuItem";
            this.binrepToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.binrepToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.binrepToolStripMenuItem.Text = "binrep";
            this.binrepToolStripMenuItem.Click += new System.EventHandler(this.binrepToolStripMenuItem_Click);
            // 
            // fibonacciToolStripMenuItem
            // 
            this.fibonacciToolStripMenuItem.Name = "fibonacciToolStripMenuItem";
            this.fibonacciToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fibonacciToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.fibonacciToolStripMenuItem.Text = "fibonacci";
            this.fibonacciToolStripMenuItem.Click += new System.EventHandler(this.fibonacciToolStripMenuItem_Click);
            // 
            // jacobiToolStripMenuItem
            // 
            this.jacobiToolStripMenuItem.Name = "jacobiToolStripMenuItem";
            this.jacobiToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.jacobiToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.jacobiToolStripMenuItem.Text = "jacobi";
            this.jacobiToolStripMenuItem.Click += new System.EventHandler(this.jacobiToolStripMenuItem_Click);
            // 
            // mandelbrotToolStripMenuItem
            // 
            this.mandelbrotToolStripMenuItem.Name = "mandelbrotToolStripMenuItem";
            this.mandelbrotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mandelbrotToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.mandelbrotToolStripMenuItem.Text = "mandelbrot";
            this.mandelbrotToolStripMenuItem.Click += new System.EventHandler(this.mandelbrotToolStripMenuItem_Click);
            // 
            // movingedgeToolStripMenuItem
            // 
            this.movingedgeToolStripMenuItem.Name = "movingedgeToolStripMenuItem";
            this.movingedgeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.movingedgeToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.movingedgeToolStripMenuItem.Text = "movingedge";
            this.movingedgeToolStripMenuItem.Click += new System.EventHandler(this.movingedgeToolStripMenuItem_Click);
            // 
            // multiplyToolStripMenuItem
            // 
            this.multiplyToolStripMenuItem.Name = "multiplyToolStripMenuItem";
            this.multiplyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.multiplyToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.multiplyToolStripMenuItem.Text = "multiply";
            this.multiplyToolStripMenuItem.Click += new System.EventHandler(this.multiplyToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sortToolStripMenuItem.Text = "sort";
            this.sortToolStripMenuItem.Click += new System.EventHandler(this.sortToolStripMenuItem_Click);
            // 
            // sortinsertionToolStripMenuItem
            // 
            this.sortinsertionToolStripMenuItem.Name = "sortinsertionToolStripMenuItem";
            this.sortinsertionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.sortinsertionToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sortinsertionToolStripMenuItem.Text = "sort_insertion";
            this.sortinsertionToolStripMenuItem.Click += new System.EventHandler(this.sortinsertionToolStripMenuItem_Click);
            // 
            // taxToolStripMenuItem
            // 
            this.taxToolStripMenuItem.Name = "taxToolStripMenuItem";
            this.taxToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.taxToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.taxToolStripMenuItem.Text = "tax";
            this.taxToolStripMenuItem.Click += new System.EventHandler(this.taxToolStripMenuItem_Click);
            // 
            // triviaToolStripMenuItem
            // 
            this.triviaToolStripMenuItem.Name = "triviaToolStripMenuItem";
            this.triviaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.triviaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.triviaToolStripMenuItem.Text = "trivia";
            this.triviaToolStripMenuItem.Click += new System.EventHandler(this.triviaToolStripMenuItem_Click);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            this.convertToolStripMenuItem.Click += new System.EventHandler(this.convertToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem1.Text = "Run";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // txtOneilCode
            // 
            this.txtOneilCode.Location = new System.Drawing.Point(24, 29);
            this.txtOneilCode.Name = "txtOneilCode";
            this.txtOneilCode.Size = new System.Drawing.Size(406, 421);
            this.txtOneilCode.TabIndex = 1;
            this.txtOneilCode.Text = "";
            this.txtOneilCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // txtCSharpCode
            // 
            this.txtCSharpCode.Location = new System.Drawing.Point(499, 27);
            this.txtCSharpCode.Name = "txtCSharpCode";
            this.txtCSharpCode.ReadOnly = true;
            this.txtCSharpCode.Size = new System.Drawing.Size(451, 422);
            this.txtCSharpCode.TabIndex = 2;
            this.txtCSharpCode.Text = "";
            this.txtCSharpCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCSharpCode_MouseDown);
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.SystemColors.Window;
            this.txtError.Location = new System.Drawing.Point(13, 456);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError.Size = new System.Drawing.Size(937, 105);
            this.txtError.TabIndex = 3;
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // lineNumbersForRichText1
            // 
            this.lineNumbersForRichText1.AutoSizing = true;
            this.lineNumbersForRichText1.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText1.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText1.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText1.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText1.BorderLinesThickness = 1F;
            this.lineNumbersForRichText1.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText1.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText1.GridLinesThickness = 1F;
            this.lineNumbersForRichText1.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText1.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText1.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText1.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText1.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText1.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText1.Location = new System.Drawing.Point(478, 27);
            this.lineNumbersForRichText1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText1.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText1.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText1.MarginLinesThickness = 1F;
            this.lineNumbersForRichText1.Name = "lineNumbersForRichText1";
            this.lineNumbersForRichText1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText1.ParentRichTextBox = this.txtCSharpCode;
            this.lineNumbersForRichText1.SeeThroughMode = false;
            this.lineNumbersForRichText1.ShowBackgroundGradient = true;
            this.lineNumbersForRichText1.ShowBorderLines = true;
            this.lineNumbersForRichText1.ShowGridLines = true;
            this.lineNumbersForRichText1.ShowLineNumbers = true;
            this.lineNumbersForRichText1.ShowMarginLines = true;
            this.lineNumbersForRichText1.Size = new System.Drawing.Size(20, 422);
            this.lineNumbersForRichText1.TabIndex = 4;
            // 
            // lineNumbersForRichText2
            // 
            this.lineNumbersForRichText2.AutoSizing = true;
            this.lineNumbersForRichText2.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText2.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText2.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText2.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText2.BorderLinesThickness = 1F;
            this.lineNumbersForRichText2.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText2.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText2.GridLinesThickness = 1F;
            this.lineNumbersForRichText2.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText2.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText2.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText2.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText2.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText2.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText2.Location = new System.Drawing.Point(3, 29);
            this.lineNumbersForRichText2.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText2.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText2.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText2.MarginLinesThickness = 1F;
            this.lineNumbersForRichText2.Name = "lineNumbersForRichText2";
            this.lineNumbersForRichText2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText2.ParentRichTextBox = this.txtOneilCode;
            this.lineNumbersForRichText2.SeeThroughMode = false;
            this.lineNumbersForRichText2.ShowBackgroundGradient = true;
            this.lineNumbersForRichText2.ShowBorderLines = true;
            this.lineNumbersForRichText2.ShowGridLines = true;
            this.lineNumbersForRichText2.ShowLineNumbers = true;
            this.lineNumbersForRichText2.ShowMarginLines = true;
            this.lineNumbersForRichText2.Size = new System.Drawing.Size(20, 421);
            this.lineNumbersForRichText2.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 573);
            this.Controls.Add(this.lineNumbersForRichText2);
            this.Controls.Add(this.lineNumbersForRichText1);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.txtCSharpCode);
            this.Controls.Add(this.txtOneilCode);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Optimizing Parallel Complilers";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtOneilCode;
        private System.Windows.Forms.RichTextBox txtCSharpCode;
        private System.Windows.Forms.ToolStripMenuItem loadOneilCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automatonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binrepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fibonacciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jacobiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mandelbrotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movingedgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multiplyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortinsertionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triviaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText1;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText2;
    }
}

