namespace OS_RGR2_B
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            testsDirTextBox = new TextBox();
            testsDirBtn = new Button();
            findTestsBtn = new Button();
            testsPanel = new Panel();
            statTextBox = new RichTextBox();
            runBtn = new Button();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            testsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(-1, 0);
            label1.Name = "label1";
            label1.Size = new Size(470, 33);
            label1.TabIndex = 0;
            label1.Text = "Enter the location of the test suite";
            // 
            // testsDirTextBox
            // 
            testsDirTextBox.BackColor = Color.Moccasin;
            testsDirTextBox.BorderStyle = BorderStyle.FixedSingle;
            testsDirTextBox.Font = new Font("Ubuntu", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            testsDirTextBox.Location = new Point(1, 36);
            testsDirTextBox.Name = "testsDirTextBox";
            testsDirTextBox.PlaceholderText = "C:\\example\\tests";
            testsDirTextBox.Size = new Size(553, 32);
            testsDirTextBox.TabIndex = 1;
            testsDirTextBox.TextChanged += TestsDirTextBox_TextChanged;
            testsDirTextBox.DoubleClick += TestsDirBtn_Click;
            // 
            // testsDirBtn
            // 
            testsDirBtn.BackColor = Color.Orange;
            testsDirBtn.FlatStyle = FlatStyle.Flat;
            testsDirBtn.Font = new Font("Verdana", 8F, FontStyle.Bold, GraphicsUnit.Point);
            testsDirBtn.Location = new Point(560, 36);
            testsDirBtn.Name = "testsDirBtn";
            testsDirBtn.Size = new Size(42, 32);
            testsDirBtn.TabIndex = 2;
            testsDirBtn.Text = "•••";
            testsDirBtn.UseVisualStyleBackColor = false;
            testsDirBtn.Click += TestsDirBtn_Click;
            // 
            // findTestsBtn
            // 
            findTestsBtn.BackColor = Color.Orange;
            findTestsBtn.FlatStyle = FlatStyle.Flat;
            findTestsBtn.Font = new Font("Ubuntu", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            findTestsBtn.Location = new Point(3, 74);
            findTestsBtn.Name = "findTestsBtn";
            findTestsBtn.Size = new Size(599, 51);
            findTestsBtn.TabIndex = 3;
            findTestsBtn.Text = "Find test cases";
            findTestsBtn.UseVisualStyleBackColor = false;
            findTestsBtn.Click += FindTestsBtn_Click;
            // 
            // testsPanel
            // 
            testsPanel.AutoSize = true;
            testsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            testsPanel.Controls.Add(statTextBox);
            testsPanel.Controls.Add(runBtn);
            testsPanel.Controls.Add(dataGridView1);
            testsPanel.Controls.Add(label2);
            testsPanel.Location = new Point(3, 131);
            testsPanel.Name = "testsPanel";
            testsPanel.Size = new Size(654, 458);
            testsPanel.TabIndex = 4;
            // 
            // statTextBox
            // 
            statTextBox.BackColor = Color.Moccasin;
            statTextBox.BorderStyle = BorderStyle.FixedSingle;
            statTextBox.Font = new Font("Ubuntu", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            statTextBox.Location = new Point(427, 36);
            statTextBox.Name = "statTextBox";
            statTextBox.ScrollBars = RichTextBoxScrollBars.None;
            statTextBox.Size = new Size(224, 168);
            statTextBox.TabIndex = 5;
            statTextBox.Text = "";
            // 
            // runBtn
            // 
            runBtn.BackColor = Color.YellowGreen;
            runBtn.FlatStyle = FlatStyle.Flat;
            runBtn.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            runBtn.Location = new Point(427, 210);
            runBtn.Name = "runBtn";
            runBtn.Size = new Size(224, 61);
            runBtn.TabIndex = 5;
            runBtn.Text = "RUN";
            runBtn.UseVisualStyleBackColor = false;
            runBtn.Click += RunBtn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.Moccasin;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.BackgroundColor = Color.Moccasin;
            dataGridView1.CausesValidation = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Moccasin;
            dataGridViewCellStyle2.Font = new Font("Ubuntu", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.Moccasin;
            dataGridViewCellStyle3.Font = new Font("Ubuntu", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.GridColor = Color.Black;
            dataGridView1.Location = new Point(-2, 36);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Moccasin;
            dataGridViewCellStyle4.Font = new Font("Ubuntu", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = Color.Moccasin;
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(423, 419);
            dataGridView1.TabIndex = 7;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(156, 33);
            label2.TabIndex = 5;
            label2.Text = "Test cases:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Bisque;
            ClientSize = new Size(756, 607);
            Controls.Add(testsPanel);
            Controls.Add(label1);
            Controls.Add(testsDirBtn);
            Controls.Add(testsDirTextBox);
            Controls.Add(findTestsBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "RGR 2";
            testsPanel.ResumeLayout(false);
            testsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox testsDirTextBox;
        private Button testsDirBtn;
        private Button findTestsBtn;
        private Panel testsPanel;
        private Label label2;
        private DataGridView dataGridView1;
        private Button runBtn;
        private RichTextBox statTextBox;
    }
}