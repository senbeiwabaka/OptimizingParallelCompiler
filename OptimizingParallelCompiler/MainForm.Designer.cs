﻿namespace OptimizingParallelCompiler
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
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtOneilCode = new System.Windows.Forms.RichTextBox();
            this.txtCSharpCode = new System.Windows.Forms.RichTextBox();
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
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(917, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadOneilCodeToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.runToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.convertToolStripMenuItem.Text = "Convert";
            this.convertToolStripMenuItem.Click += new System.EventHandler(this.convertToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // txtOneilCode
            // 
            this.txtOneilCode.Location = new System.Drawing.Point(13, 28);
            this.txtOneilCode.Name = "txtOneilCode";
            this.txtOneilCode.Size = new System.Drawing.Size(435, 554);
            this.txtOneilCode.TabIndex = 1;
            this.txtOneilCode.Text = "";
            this.txtOneilCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // txtCSharpCode
            // 
            this.txtCSharpCode.Location = new System.Drawing.Point(454, 27);
            this.txtCSharpCode.Name = "txtCSharpCode";
            this.txtCSharpCode.Size = new System.Drawing.Size(451, 554);
            this.txtCSharpCode.TabIndex = 2;
            this.txtCSharpCode.Text = "";
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
            this.loadOneilCodeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.loadOneilCodeToolStripMenuItem.Text = "Load Oneil Code";
            //this.loadOneilCodeToolStripMenuItem.Click += new System.EventHandler(this.loadOneilCodeToolStripMenuItem_Click);
            // 
            // automatonToolStripMenuItem
            // 
            this.automatonToolStripMenuItem.Name = "automatonToolStripMenuItem";
            this.automatonToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.automatonToolStripMenuItem.Text = "automaton";
            this.automatonToolStripMenuItem.Click += new System.EventHandler(this.automatonToolStripMenuItem_Click);
            // 
            // binrepToolStripMenuItem
            // 
            this.binrepToolStripMenuItem.Name = "binrepToolStripMenuItem";
            this.binrepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.binrepToolStripMenuItem.Text = "binrep";
            this.binrepToolStripMenuItem.Click += new System.EventHandler(this.binrepToolStripMenuItem_Click);
            // 
            // fibonacciToolStripMenuItem
            // 
            this.fibonacciToolStripMenuItem.Name = "fibonacciToolStripMenuItem";
            this.fibonacciToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fibonacciToolStripMenuItem.Text = "fibonacci";
            this.fibonacciToolStripMenuItem.Click += new System.EventHandler(this.fibonacciToolStripMenuItem_Click);
            // 
            // jacobiToolStripMenuItem
            // 
            this.jacobiToolStripMenuItem.Name = "jacobiToolStripMenuItem";
            this.jacobiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.jacobiToolStripMenuItem.Text = "jacobi";
            this.jacobiToolStripMenuItem.Click += new System.EventHandler(this.jacobiToolStripMenuItem_Click);
            // 
            // mandelbrotToolStripMenuItem
            // 
            this.mandelbrotToolStripMenuItem.Name = "mandelbrotToolStripMenuItem";
            this.mandelbrotToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mandelbrotToolStripMenuItem.Text = "mandelbrot";
            this.mandelbrotToolStripMenuItem.Click += new System.EventHandler(this.mandelbrotToolStripMenuItem_Click);
            // 
            // movingedgeToolStripMenuItem
            // 
            this.movingedgeToolStripMenuItem.Name = "movingedgeToolStripMenuItem";
            this.movingedgeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.movingedgeToolStripMenuItem.Text = "movingedge";
            this.movingedgeToolStripMenuItem.Click += new System.EventHandler(this.movingedgeToolStripMenuItem_Click);
            // 
            // multiplyToolStripMenuItem
            // 
            this.multiplyToolStripMenuItem.Name = "multiplyToolStripMenuItem";
            this.multiplyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.multiplyToolStripMenuItem.Text = "multiply";
            this.multiplyToolStripMenuItem.Click += new System.EventHandler(this.multiplyToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sortToolStripMenuItem.Text = "sort";
            this.sortToolStripMenuItem.Click += new System.EventHandler(this.sortToolStripMenuItem_Click);
            // 
            // sortinsertionToolStripMenuItem
            // 
            this.sortinsertionToolStripMenuItem.Name = "sortinsertionToolStripMenuItem";
            this.sortinsertionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sortinsertionToolStripMenuItem.Text = "sort_insertion";
            this.sortinsertionToolStripMenuItem.Click += new System.EventHandler(this.sortinsertionToolStripMenuItem_Click);
            // 
            // taxToolStripMenuItem
            // 
            this.taxToolStripMenuItem.Name = "taxToolStripMenuItem";
            this.taxToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.taxToolStripMenuItem.Text = "tax";
            this.taxToolStripMenuItem.Click += new System.EventHandler(this.taxToolStripMenuItem_Click);
            // 
            // triviaToolStripMenuItem
            // 
            this.triviaToolStripMenuItem.Name = "triviaToolStripMenuItem";
            this.triviaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.triviaToolStripMenuItem.Text = "trivia";
            this.triviaToolStripMenuItem.Click += new System.EventHandler(this.triviaToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 573);
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
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
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
    }
}

