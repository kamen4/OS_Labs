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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            label1 = new Label();
            testsDirTextBox = new TextBox();
            testsDirBtn = new Button();
            findTestsBtn = new Button();
            testsPanel = new Panel();
            solvingPanel = new Panel();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            cancelBtn = new Button();
            label3 = new Label();
            pauseBtn = new Button();
            statTextBox = new RichTextBox();
            runBtn = new Button();
            testDataGridView = new DataGridView();
            label2 = new Label();
            searchingPanel = new Panel();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            testsPanel.SuspendLayout();
            solvingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)testDataGridView).BeginInit();
            searchingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            testsDirTextBox.Size = new Size(608, 32);
            testsDirTextBox.TabIndex = 1;
            testsDirTextBox.TextChanged += TestsDirTextBox_TextChanged;
            testsDirTextBox.DoubleClick += TestsDirBtn_Click;
            // 
            // testsDirBtn
            // 
            testsDirBtn.BackColor = Color.Orange;
            testsDirBtn.FlatStyle = FlatStyle.Flat;
            testsDirBtn.Font = new Font("Verdana", 8F, FontStyle.Bold, GraphicsUnit.Point);
            testsDirBtn.Location = new Point(615, 36);
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
            findTestsBtn.Size = new Size(654, 51);
            findTestsBtn.TabIndex = 3;
            findTestsBtn.Text = "Find test cases";
            findTestsBtn.UseVisualStyleBackColor = false;
            findTestsBtn.Click += FindTestsBtn_Click;
            // 
            // testsPanel
            // 
            testsPanel.AutoSize = true;
            testsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            testsPanel.Controls.Add(solvingPanel);
            testsPanel.Controls.Add(cancelBtn);
            testsPanel.Controls.Add(label3);
            testsPanel.Controls.Add(pauseBtn);
            testsPanel.Controls.Add(statTextBox);
            testsPanel.Controls.Add(runBtn);
            testsPanel.Controls.Add(testDataGridView);
            testsPanel.Controls.Add(label2);
            testsPanel.Location = new Point(3, 131);
            testsPanel.Name = "testsPanel";
            testsPanel.Size = new Size(654, 458);
            testsPanel.TabIndex = 4;
            // 
            // solvingPanel
            // 
            solvingPanel.BackColor = Color.Moccasin;
            solvingPanel.Controls.Add(pictureBox1);
            solvingPanel.Controls.Add(label4);
            solvingPanel.Location = new Point(0, 36);
            solvingPanel.Name = "solvingPanel";
            solvingPanel.Size = new Size(421, 419);
            solvingPanel.TabIndex = 11;
            solvingPanel.UseWaitCursor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(92, 155);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.UseWaitCursor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Ubuntu", 47.9999924F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(34, 57);
            label4.Name = "label4";
            label4.Size = new Size(351, 75);
            label4.TabIndex = 11;
            label4.Text = "SOLVING...";
            label4.UseWaitCursor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = Color.Crimson;
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            cancelBtn.Location = new Point(427, 344);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(224, 61);
            cancelBtn.TabIndex = 10;
            cancelBtn.Text = "CANCEL";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(427, 0);
            label3.Name = "label3";
            label3.Size = new Size(143, 33);
            label3.TabIndex = 9;
            label3.Text = "Statistics:";
            // 
            // pauseBtn
            // 
            pauseBtn.BackColor = Color.Yellow;
            pauseBtn.FlatStyle = FlatStyle.Flat;
            pauseBtn.Font = new Font("Ubuntu", 20.2499981F, FontStyle.Bold, GraphicsUnit.Point);
            pauseBtn.Location = new Point(427, 277);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new Size(224, 61);
            pauseBtn.TabIndex = 8;
            pauseBtn.Text = "PAUSE";
            pauseBtn.UseVisualStyleBackColor = false;
            pauseBtn.Click += PauseBtn_Click;
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
            // testDataGridView
            // 
            testDataGridView.AllowUserToAddRows = false;
            testDataGridView.AllowUserToDeleteRows = false;
            testDataGridView.AllowUserToResizeColumns = false;
            testDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = Color.Moccasin;
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            testDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            testDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            testDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            testDataGridView.BackgroundColor = Color.Moccasin;
            testDataGridView.CausesValidation = false;
            testDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.Moccasin;
            dataGridViewCellStyle7.Font = new Font("Ubuntu", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            testDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            testDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.Moccasin;
            dataGridViewCellStyle8.Font = new Font("Ubuntu", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            testDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            testDataGridView.GridColor = Color.Black;
            testDataGridView.Location = new Point(-2, 36);
            testDataGridView.MultiSelect = false;
            testDataGridView.Name = "testDataGridView";
            testDataGridView.ReadOnly = true;
            testDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.Moccasin;
            dataGridViewCellStyle9.Font = new Font("Ubuntu", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle9.SelectionForeColor = Color.Black;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            testDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            testDataGridView.RowHeadersVisible = false;
            testDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.BackColor = Color.Moccasin;
            dataGridViewCellStyle10.ForeColor = Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = Color.Moccasin;
            dataGridViewCellStyle10.SelectionForeColor = Color.Black;
            testDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            testDataGridView.RowTemplate.Height = 25;
            testDataGridView.ScrollBars = ScrollBars.Vertical;
            testDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            testDataGridView.Size = new Size(423, 419);
            testDataGridView.TabIndex = 7;
            testDataGridView.SelectionChanged += TaskDataGridView_SelectionChanged;
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
            // searchingPanel
            // 
            searchingPanel.Controls.Add(label5);
            searchingPanel.Controls.Add(pictureBox2);
            searchingPanel.Location = new Point(1, 131);
            searchingPanel.Name = "searchingPanel";
            searchingPanel.Size = new Size(656, 458);
            searchingPanel.TabIndex = 5;
            searchingPanel.UseWaitCursor = true;
            searchingPanel.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Ubuntu", 47.9999924F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(118, 66);
            label5.Name = "label5";
            label5.Size = new Size(438, 75);
            label5.TabIndex = 13;
            label5.Text = "SEARCHING...";
            label5.UseWaitCursor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(220, 157);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.UseWaitCursor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Bisque;
            ClientSize = new Size(756, 607);
            Controls.Add(searchingPanel);
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
            solvingPanel.ResumeLayout(false);
            solvingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)testDataGridView).EndInit();
            searchingPanel.ResumeLayout(false);
            searchingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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
        private DataGridView testDataGridView;
        private Button runBtn;
        private RichTextBox statTextBox;
        private Button pauseBtn;
        private Label label3;
        private Button cancelBtn;
        private Panel solvingPanel;
        private PictureBox pictureBox1;
        private Label label4;
        private Panel searchingPanel;
        private Label label5;
        private PictureBox pictureBox2;
    }
}