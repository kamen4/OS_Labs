namespace LabFileSearch
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            DirTextBox = new TextBox();
            SubDirСheckBox = new CheckBox();
            PatternTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            FilesCheckBox = new CheckBox();
            label4 = new Label();
            DirsCheckBox = new CheckBox();
            listBox1 = new ListBox();
            result1Label = new Label();
            SearchBtn = new Button();
            SelectDirBtn = new Button();
            result2Label = new Label();
            listBox2 = new ListBox();
            DepthTextBox = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(113, 15);
            label1.TabIndex = 0;
            label1.Text = "Enter start directory:";
            // 
            // DirTextBox
            // 
            DirTextBox.Location = new Point(12, 27);
            DirTextBox.Name = "DirTextBox";
            DirTextBox.Size = new Size(138, 23);
            DirTextBox.TabIndex = 1;
            DirTextBox.TextChanged += DirTextBox_TextChanged;
            // 
            // SubDirСheckBox
            // 
            SubDirСheckBox.AutoSize = true;
            SubDirСheckBox.Location = new Point(12, 56);
            SubDirСheckBox.Name = "SubDirСheckBox";
            SubDirСheckBox.Size = new Size(150, 19);
            SubDirСheckBox.TabIndex = 2;
            SubDirСheckBox.Text = "search in subdirectories";
            SubDirСheckBox.UseVisualStyleBackColor = true;
            SubDirСheckBox.CheckedChanged += SubDirСheckBox_CheckedChanged;
            // 
            // PatternTextBox
            // 
            PatternTextBox.Location = new Point(12, 125);
            PatternTextBox.Name = "PatternTextBox";
            PatternTextBox.Size = new Size(170, 23);
            PatternTextBox.TabIndex = 4;
            PatternTextBox.TextChanged += PatternTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 107);
            label2.Name = "label2";
            label2.Size = new Size(141, 15);
            label2.TabIndex = 3;
            label2.Text = "Enter file name or pattern";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 84);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 6;
            label3.Text = "Search depth:";
            // 
            // FilesCheckBox
            // 
            FilesCheckBox.AutoSize = true;
            FilesCheckBox.Location = new Point(32, 169);
            FilesCheckBox.Name = "FilesCheckBox";
            FilesCheckBox.Size = new Size(49, 19);
            FilesCheckBox.TabIndex = 7;
            FilesCheckBox.Text = "Files";
            FilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 151);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 8;
            label4.Text = "Search:";
            // 
            // DirsCheckBox
            // 
            DirsCheckBox.AutoSize = true;
            DirsCheckBox.Location = new Point(32, 194);
            DirsCheckBox.Name = "DirsCheckBox";
            DirsCheckBox.Size = new Size(82, 19);
            DirsCheckBox.TabIndex = 9;
            DirsCheckBox.Text = "Directories";
            DirsCheckBox.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(207, 27);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(266, 214);
            listBox1.TabIndex = 10;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // result1Label
            // 
            result1Label.AutoSize = true;
            result1Label.Location = new Point(207, 9);
            result1Label.Name = "result1Label";
            result1Label.Size = new Size(87, 15);
            result1Label.TabIndex = 11;
            result1Label.Text = "Thread 1 result:";
            // 
            // SearchBtn
            // 
            SearchBtn.Location = new Point(12, 219);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new Size(170, 23);
            SearchBtn.TabIndex = 12;
            SearchBtn.Text = "Search";
            SearchBtn.UseVisualStyleBackColor = true;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // SelectDirBtn
            // 
            SelectDirBtn.Location = new Point(156, 27);
            SelectDirBtn.Name = "SelectDirBtn";
            SelectDirBtn.Size = new Size(26, 23);
            SelectDirBtn.TabIndex = 13;
            SelectDirBtn.Text = "...";
            SelectDirBtn.UseVisualStyleBackColor = true;
            SelectDirBtn.Click += SelectDirBtn_Click;
            // 
            // result2Label
            // 
            result2Label.AutoSize = true;
            result2Label.Location = new Point(479, 9);
            result2Label.Name = "result2Label";
            result2Label.Size = new Size(87, 15);
            result2Label.TabIndex = 15;
            result2Label.Text = "Thread 2 result:";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(479, 27);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(266, 214);
            listBox2.TabIndex = 14;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // DepthTextBox
            // 
            DepthTextBox.Location = new Point(97, 81);
            DepthTextBox.Name = "DepthTextBox";
            DepthTextBox.Size = new Size(85, 23);
            DepthTextBox.TabIndex = 16;
            DepthTextBox.KeyPress += DepthTextBox_KeyPress;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 255);
            Controls.Add(DepthTextBox);
            Controls.Add(result2Label);
            Controls.Add(listBox2);
            Controls.Add(SelectDirBtn);
            Controls.Add(SearchBtn);
            Controls.Add(result1Label);
            Controls.Add(listBox1);
            Controls.Add(DirsCheckBox);
            Controls.Add(label4);
            Controls.Add(FilesCheckBox);
            Controls.Add(label3);
            Controls.Add(PatternTextBox);
            Controls.Add(label2);
            Controls.Add(SubDirСheckBox);
            Controls.Add(DirTextBox);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(776, 294);
            MinimizeBox = false;
            MinimumSize = new Size(776, 294);
            Name = "Form1";
            Text = "Files Searcher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox DirTextBox;
        private CheckBox SubDirСheckBox;
        private TextBox PatternTextBox;
        private Label label2;
        private Label label3;
        private CheckBox FilesCheckBox;
        private Label label4;
        private CheckBox DirsCheckBox;
        private ListBox listBox1;
        private Label result1Label;
        private Button SearchBtn;
        private Button SelectDirBtn;
        private Label result2Label;
        private ListBox listBox2;
        private TextBox DepthTextBox;
    }
}