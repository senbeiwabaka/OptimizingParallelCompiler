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
            this.dependencyAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constantProprogationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runtoolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOneilCode = new System.Windows.Forms.RichTextBox();
            this.txtCSharpCode = new System.Windows.Forms.RichTextBox();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineNumbersForRichText1 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.lineNumbersForRichText2 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.tabTransform = new System.Windows.Forms.TabControl();
            this.tbpOutPut = new System.Windows.Forms.TabPage();
            this.rtbDependencyOutput = new System.Windows.Forms.RichTextBox();
            this.txtDependecy = new System.Windows.Forms.TextBox();
            this.tbpConstantPropragation = new System.Windows.Forms.TabPage();
            this.txtDeadCode = new System.Windows.Forms.RichTextBox();
            this.lineNumbersForRichText4 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.tbpThreeOPCode = new System.Windows.Forms.TabPage();
            this.lineNumbersForRichText3 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.rtbThreeOPCode = new System.Windows.Forms.RichTextBox();
            this.tbpCSharp = new System.Windows.Forms.TabPage();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tabTransform.SuspendLayout();
            this.tbpOutPut.SuspendLayout();
            this.tbpConstantPropragation.SuspendLayout();
            this.tbpThreeOPCode.SuspendLayout();
            this.tbpCSharp.SuspendLayout();
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
            this.dependencyAnalysisToolStripMenuItem,
            this.constantProprogationToolStripMenuItem,
            this.oPCodeToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.compileToolStripMenuItem,
            this.runtoolStripMenu,
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
            this.loadOneilCodeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
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
            // dependencyAnalysisToolStripMenuItem
            // 
            this.dependencyAnalysisToolStripMenuItem.Name = "dependencyAnalysisToolStripMenuItem";
            this.dependencyAnalysisToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.dependencyAnalysisToolStripMenuItem.Text = "Dependency Analysis";
            this.dependencyAnalysisToolStripMenuItem.Click += new System.EventHandler(this.dependencyAnalysisToolStripMenuItem_Click);
            // 
            // constantProprogationToolStripMenuItem
            // 
            this.constantProprogationToolStripMenuItem.Enabled = false;
            this.constantProprogationToolStripMenuItem.Name = "constantProprogationToolStripMenuItem";
            this.constantProprogationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.constantProprogationToolStripMenuItem.Text = "Constant Proprogation";
            this.constantProprogationToolStripMenuItem.Click += new System.EventHandler(this.deadCodeRemovalToolStripMenuItem_Click);
            // 
            // oPCodeToolStripMenuItem
            // 
            this.oPCodeToolStripMenuItem.Enabled = false;
            this.oPCodeToolStripMenuItem.Name = "oPCodeToolStripMenuItem";
            this.oPCodeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.oPCodeToolStripMenuItem.Text = "3 OP Code";
            this.oPCodeToolStripMenuItem.Click += new System.EventHandler(this.oPCodeToolStripMenuItem_Click);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Enabled = false;
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            this.convertToolStripMenuItem.Click += new System.EventHandler(this.convertToolStripMenuItem_Click);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Enabled = false;
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // runtoolStripMenu
            // 
            this.runtoolStripMenu.Enabled = false;
            this.runtoolStripMenu.Name = "runtoolStripMenu";
            this.runtoolStripMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.runtoolStripMenu.Size = new System.Drawing.Size(195, 22);
            this.runtoolStripMenu.Text = "Run";
            this.runtoolStripMenu.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // txtOneilCode
            // 
            this.txtOneilCode.Location = new System.Drawing.Point(24, 29);
            this.txtOneilCode.Name = "txtOneilCode";
            this.txtOneilCode.Size = new System.Drawing.Size(406, 389);
            this.txtOneilCode.TabIndex = 1;
            this.txtOneilCode.Text = "";
            // 
            // txtCSharpCode
            // 
            this.txtCSharpCode.Location = new System.Drawing.Point(25, 0);
            this.txtCSharpCode.Name = "txtCSharpCode";
            this.txtCSharpCode.ReadOnly = true;
            this.txtCSharpCode.Size = new System.Drawing.Size(481, 363);
            this.txtCSharpCode.TabIndex = 2;
            this.txtCSharpCode.Text = "";
            this.txtCSharpCode.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtCSharpCode_MouseDown);
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
            this.lineNumbersForRichText1.Location = new System.Drawing.Point(4, 0);
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
            this.lineNumbersForRichText1.Size = new System.Drawing.Size(20, 363);
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
            this.lineNumbersForRichText2.Size = new System.Drawing.Size(20, 389);
            this.lineNumbersForRichText2.TabIndex = 4;
            // 
            // tabTransform
            // 
            this.tabTransform.Controls.Add(this.tbpOutPut);
            this.tabTransform.Controls.Add(this.tbpConstantPropragation);
            this.tabTransform.Controls.Add(this.tbpThreeOPCode);
            this.tabTransform.Controls.Add(this.tbpCSharp);
            this.tabTransform.Location = new System.Drawing.Point(436, 29);
            this.tabTransform.Name = "tabTransform";
            this.tabTransform.SelectedIndex = 0;
            this.tabTransform.Size = new System.Drawing.Size(514, 389);
            this.tabTransform.TabIndex = 5;
            // 
            // tbpOutPut
            // 
            this.tbpOutPut.Controls.Add(this.rtbDependencyOutput);
            this.tbpOutPut.Controls.Add(this.txtDependecy);
            this.tbpOutPut.Location = new System.Drawing.Point(4, 22);
            this.tbpOutPut.Name = "tbpOutPut";
            this.tbpOutPut.Size = new System.Drawing.Size(506, 363);
            this.tbpOutPut.TabIndex = 3;
            this.tbpOutPut.Text = "Depenency Output";
            this.tbpOutPut.UseVisualStyleBackColor = true;
            // 
            // rtbDependencyOutput
            // 
            this.rtbDependencyOutput.Location = new System.Drawing.Point(3, 3);
            this.rtbDependencyOutput.Name = "rtbDependencyOutput";
            this.rtbDependencyOutput.Size = new System.Drawing.Size(500, 357);
            this.rtbDependencyOutput.TabIndex = 3;
            this.rtbDependencyOutput.Text = "";
            // 
            // txtDependecy
            // 
            this.txtDependecy.Location = new System.Drawing.Point(3, 3);
            this.txtDependecy.Multiline = true;
            this.txtDependecy.Name = "txtDependecy";
            this.txtDependecy.Size = new System.Drawing.Size(500, 357);
            this.txtDependecy.TabIndex = 0;
            // 
            // tbpConstantPropragation
            // 
            this.tbpConstantPropragation.Controls.Add(this.txtDeadCode);
            this.tbpConstantPropragation.Controls.Add(this.lineNumbersForRichText4);
            this.tbpConstantPropragation.Location = new System.Drawing.Point(4, 22);
            this.tbpConstantPropragation.Name = "tbpConstantPropragation";
            this.tbpConstantPropragation.Size = new System.Drawing.Size(506, 363);
            this.tbpConstantPropragation.TabIndex = 2;
            this.tbpConstantPropragation.Text = "Constant Propragation";
            this.tbpConstantPropragation.UseVisualStyleBackColor = true;
            // 
            // txtDeadCode
            // 
            this.txtDeadCode.Location = new System.Drawing.Point(33, 0);
            this.txtDeadCode.Name = "txtDeadCode";
            this.txtDeadCode.ReadOnly = true;
            this.txtDeadCode.Size = new System.Drawing.Size(469, 360);
            this.txtDeadCode.TabIndex = 4;
            this.txtDeadCode.Text = "";
            // 
            // lineNumbersForRichText4
            // 
            this.lineNumbersForRichText4.AutoSizing = true;
            this.lineNumbersForRichText4.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText4.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText4.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText4.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText4.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText4.BorderLinesThickness = 1F;
            this.lineNumbersForRichText4.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText4.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText4.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText4.GridLinesThickness = 1F;
            this.lineNumbersForRichText4.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText4.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText4.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText4.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText4.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText4.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText4.Location = new System.Drawing.Point(12, 0);
            this.lineNumbersForRichText4.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText4.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText4.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText4.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText4.MarginLinesThickness = 1F;
            this.lineNumbersForRichText4.Name = "lineNumbersForRichText4";
            this.lineNumbersForRichText4.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText4.ParentRichTextBox = this.txtDeadCode;
            this.lineNumbersForRichText4.SeeThroughMode = false;
            this.lineNumbersForRichText4.ShowBackgroundGradient = true;
            this.lineNumbersForRichText4.ShowBorderLines = true;
            this.lineNumbersForRichText4.ShowGridLines = true;
            this.lineNumbersForRichText4.ShowLineNumbers = true;
            this.lineNumbersForRichText4.ShowMarginLines = true;
            this.lineNumbersForRichText4.Size = new System.Drawing.Size(20, 360);
            this.lineNumbersForRichText4.TabIndex = 3;
            // 
            // tbpThreeOPCode
            // 
            this.tbpThreeOPCode.Controls.Add(this.lineNumbersForRichText3);
            this.tbpThreeOPCode.Controls.Add(this.rtbThreeOPCode);
            this.tbpThreeOPCode.Location = new System.Drawing.Point(4, 22);
            this.tbpThreeOPCode.Name = "tbpThreeOPCode";
            this.tbpThreeOPCode.Padding = new System.Windows.Forms.Padding(3);
            this.tbpThreeOPCode.Size = new System.Drawing.Size(506, 363);
            this.tbpThreeOPCode.TabIndex = 0;
            this.tbpThreeOPCode.Text = "3 OP Code";
            this.tbpThreeOPCode.UseVisualStyleBackColor = true;
            // 
            // lineNumbersForRichText3
            // 
            this.lineNumbersForRichText3.AutoSizing = true;
            this.lineNumbersForRichText3.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText3.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText3.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText3.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText3.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText3.BorderLinesThickness = 1F;
            this.lineNumbersForRichText3.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText3.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText3.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText3.GridLinesThickness = 1F;
            this.lineNumbersForRichText3.LineNumbersAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lineNumbersForRichText3.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText3.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText3.LineNumbersClippedByItemRectangle = false;
            this.lineNumbersForRichText3.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText3.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText3.Location = new System.Drawing.Point(6, 0);
            this.lineNumbersForRichText3.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText3.MarginLinesColor = System.Drawing.Color.PowderBlue;
            this.lineNumbersForRichText3.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText3.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText3.MarginLinesThickness = 1F;
            this.lineNumbersForRichText3.Name = "lineNumbersForRichText3";
            this.lineNumbersForRichText3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText3.ParentRichTextBox = this.rtbThreeOPCode;
            this.lineNumbersForRichText3.SeeThroughMode = false;
            this.lineNumbersForRichText3.ShowBackgroundGradient = true;
            this.lineNumbersForRichText3.ShowBorderLines = true;
            this.lineNumbersForRichText3.ShowGridLines = true;
            this.lineNumbersForRichText3.ShowLineNumbers = true;
            this.lineNumbersForRichText3.ShowMarginLines = true;
            this.lineNumbersForRichText3.Size = new System.Drawing.Size(20, 363);
            this.lineNumbersForRichText3.TabIndex = 3;
            // 
            // rtbThreeOPCode
            // 
            this.rtbThreeOPCode.Location = new System.Drawing.Point(27, 0);
            this.rtbThreeOPCode.Name = "rtbThreeOPCode";
            this.rtbThreeOPCode.ReadOnly = true;
            this.rtbThreeOPCode.Size = new System.Drawing.Size(479, 363);
            this.rtbThreeOPCode.TabIndex = 2;
            this.rtbThreeOPCode.Text = "";
            // 
            // tbpCSharp
            // 
            this.tbpCSharp.Controls.Add(this.lineNumbersForRichText1);
            this.tbpCSharp.Controls.Add(this.txtCSharpCode);
            this.tbpCSharp.Location = new System.Drawing.Point(4, 22);
            this.tbpCSharp.Name = "tbpCSharp";
            this.tbpCSharp.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCSharp.Size = new System.Drawing.Size(506, 363);
            this.tbpCSharp.TabIndex = 1;
            this.tbpCSharp.Text = "O\'Neil Code - C#";
            this.tbpCSharp.UseVisualStyleBackColor = true;
            // 
            // rtbError
            // 
            this.rtbError.Location = new System.Drawing.Point(3, 424);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbError.Size = new System.Drawing.Size(943, 137);
            this.rtbError.TabIndex = 6;
            this.rtbError.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 573);
            this.Controls.Add(this.rtbError);
            this.Controls.Add(this.tabTransform);
            this.Controls.Add(this.lineNumbersForRichText2);
            this.Controls.Add(this.txtOneilCode);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Optimizing Parallel Complilers";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabTransform.ResumeLayout(false);
            this.tbpOutPut.ResumeLayout(false);
            this.tbpOutPut.PerformLayout();
            this.tbpConstantPropragation.ResumeLayout(false);
            this.tbpThreeOPCode.ResumeLayout(false);
            this.tbpCSharp.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem runtoolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText1;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText2;
        private System.Windows.Forms.TabControl tabTransform;
        private System.Windows.Forms.TabPage tbpThreeOPCode;
        private System.Windows.Forms.TabPage tbpCSharp;
        private System.Windows.Forms.RichTextBox rtbThreeOPCode;
        private System.Windows.Forms.ToolStripMenuItem oPCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constantProprogationToolStripMenuItem;
        private System.Windows.Forms.TabPage tbpConstantPropragation;
        private System.Windows.Forms.RichTextBox txtDeadCode;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText4;
        private System.Windows.Forms.TabPage tbpOutPut;
        private System.Windows.Forms.RichTextBox rtbError;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText3;
        private System.Windows.Forms.TextBox txtDependecy;
        private System.Windows.Forms.ToolStripMenuItem dependencyAnalysisToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbDependencyOutput;
    }
}

