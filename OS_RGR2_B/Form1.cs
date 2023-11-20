using System.ComponentModel;
using OS_RGR2_B.Models;
using OS_RGR2_B.Models.Enums;
using OS_RGR2_B.Models.ViewModels;

using OS_RGR2_B.Slaves;

namespace OS_RGR2_B;

public partial class Form1 : Form
{
    //list for tests tetsDataGridView
    readonly BindingList<FileTestVM> testsBindingList;
    //current dirrectory
    string curDir;
    //way not to stop UI
    readonly BackgroundWorker taskWorker;
    readonly BackgroundWorker findWorker;
    //stats
    readonly StatusVM status;
    readonly Mutex statusMtx;
    //pause
    ThreadPauseState threadPauseState;
    //run button lock
    bool runned = false;

    public Form1()
    {
        InitializeComponent();
        //set up BackgroundWorker
        taskWorker = new()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        taskWorker.DoWork += DoTask;
        taskWorker.RunWorkerCompleted += TaskCompleted;
        findWorker = new();
        findWorker.DoWork += LoadFiles;
        findWorker.RunWorkerCompleted += FilesLoadingCompleted;
        //intit curDir to empty
        curDir = "";
        //hide tests panel
        testsPanel.Visible = false;
        //add columns to tests dataGridView
        testDataGridView.Columns.AddRange(
            new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Name",
                HeaderText = "Name"
            },
            new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Status",
                HeaderText = "Status"
            });
        //init binding list
        testsBindingList = new();
        //set data source for dataGridView
        testDataGridView.DataSource = testsBindingList;
        status = new();
        statusMtx = new();
        //
        threadPauseState = new();
        //
        solvingPanel.Visible = false;
    }

    private void LoadFiles(object? sender, DoWorkEventArgs e)
    {
        //set cuurent directory
        curDir = testsDirTextBox.Text;

        //find files ends with .OUT ot .IN
        //and select their filename withot full path
        var files = Directory
            .GetFiles(curDir)
            .Where(x => x.EndsWith(".OUT") || x.EndsWith(".IN"))
            .Select(x => x.Split('\\')[^1]);

        //creating dictionary to check only tests wich have both .OUT and .IN files
        Dictionary<string, FileTestFlag> filesDictionry = new();
        foreach (var file in files)
        {
            string name = string.Concat(file.Split('.')[..^1]);
            if (!filesDictionry.ContainsKey(name))
                filesDictionry.Add(name, FileTestFlag.None);
            filesDictionry[name] |= file.EndsWith(".OUT") ? FileTestFlag.OUT : FileTestFlag.IN;
        }

        //clear our list
        testsBindingList.Clear();
        //fill it with valid test files values
        int id = 0;
        foreach (var file in filesDictionry)
        {
            if (file.Value == FileTestFlag.BOTH)
                testsBindingList.Add(new FileTestVM()
                {
                    Name = file.Key,
                    Status = TestStatus.Ready,
                    Id = id++,
                    Dir = curDir
                });
        }
    }

    private void FilesLoadingCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        testDataGridView.DataSource = testsBindingList;
        //check if tests are empty
        if (testsBindingList.Count == 0)
        {
            testsPanel.Visible = false;
            MessageBox.Show(
               $"There are no tests in this directory: {curDir}\n\nThe test should include 2 files, for example: \"test.IN\" and \"test.OUT\"",
               "Lack of tests",
               MessageBoxButtons.OK,
               MessageBoxIcon.Warning);
        }
        else
        {

            status.Total = testsBindingList.Count;
            status.Ready = testsBindingList.Count;
            status.Invalid = status.Solved = status.Done = status.Wrong = 0;
            UpdateStatTextBox();

            runned = false;
            runBtn.Enabled = true;

            pauseBtn.Text = "PAUSE";
            pauseBtn.BackColor = Color.Yellow;

            taskWorker.CancelAsync();

            testsPanel.Visible = true;
        }

        searchingPanel.Visible = false;
        findTestsBtn.Enabled = true;
        Update();
    }

    private void TestsDirTextBox_TextChanged(object sender, EventArgs e)
    {
        //set color to black (in case it was red throw the error)
        testsDirTextBox.ForeColor = Color.Black;
    }

    private void DoTask(object? sender, DoWorkEventArgs e)
    {
        threadPauseState = new();

        SyncFlag validated = new(false);

        SyncFlag solved = new(false);

        Queue<FileTestVM> solveQueue = new();
        Mutex solveQueueMtx = new();

        Queue<FileTestVM> checkQueue = new();
        Mutex checkQueueMtx = new();

        Thread validator = new(() =>
        {
            new Validator(
                testsBindingList.ToList(),
                solveQueue,
                solveQueueMtx,
                validated,
                status,
                statusMtx,
                threadPauseState).ValidateAll();
        });
        Thread solver = new(() =>
        {
            new Solver(
                solveQueue,
                solveQueueMtx,
                checkQueue,
                checkQueueMtx,
                validated,
                solved,
                status,
                statusMtx,
                threadPauseState).SolveAll();
        });
        Thread usless = new(() =>
        {
            new Checker(
                checkQueue,
                checkQueueMtx,
                solved,
                status,
                statusMtx,
                threadPauseState).CheckAll();
        });
        
        validator.Start();
        solver.Start();
        usless.Start();
        
        while (validator.IsAlive || solver.IsAlive || usless.IsAlive)
        {
            if ((sender is BackgroundWorker w) && w.CancellationPending)
                return;
        }
        
        validator.Join();
        solver.Join();
        usless.Join();
    }

    private void TaskCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        pauseBtn.Enabled = false;
        UpdateTestsStats();
        runBtn.Enabled = true;
        solvingPanel.Visible = false;
    }

    private void UpdateTestsStats()
    {
        UpdateStatTextBox();
        DataGridViewRowCollection l = testDataGridView.Rows;
        for (int i = 0; i < l.Count; i++)
            l[i].Cells[1].Style.ForeColor = testsBindingList[i].Status.StatusColor();
        testDataGridView.Update();
    }

    private void UpdateStatTextBox()
    {
        statTextBox.Text =
              "Total:\t" + status.Total +
            "\nReady:\t" + status.Ready +
            "\nInvalid:\t" + status.Invalid +
            "\nSolved total:\t" + status.Solved +
            "\n\tDone:\t" + status.Done +
            "\n\tWrong:\t" + status.Wrong;
        statTextBox.Update();
    }

    private void TestsDirBtn_Click(object sender, EventArgs e)
    {
        //create and execute fileDialog
        using var fbDialog = new FolderBrowserDialog();
        DialogResult result = fbDialog.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbDialog.SelectedPath))
            testsDirTextBox.Text = fbDialog.SelectedPath;
    }

    private void FindTestsBtn_Click(object sender, EventArgs e)
    {
        //check directory existing
        if (!Directory.Exists(testsDirTextBox.Text))
        {
            testsDirTextBox.ForeColor = Color.Red;
            MessageBox.Show(
                "Such directory dosent exist",
                "ERROR",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            testsDirTextBox.Focus();
            return;
        }

        searchingPanel.Visible = true;
        findTestsBtn.Enabled = false;
        Update();

        testDataGridView.DataSource = null;
        findWorker.RunWorkerAsync();
    }

    private void RunBtn_Click(object sender, EventArgs e)
    {
        if (runned) return;
        pauseBtn.Enabled = true;
        runned = true;
        solvingPanel.Visible = true;
        runBtn.Enabled = false;
        taskWorker.RunWorkerAsync();
    }

    private void PauseBtn_Click(object sender, EventArgs e)
    {
        if (pauseBtn.Text == "RESUME")
        {
            pauseBtn.Text = "PAUSE";
            pauseBtn.BackColor = Color.Yellow;
            threadPauseState.Paused = false;
            solvingPanel.Visible = true;
        }
        else
        {
            pauseBtn.Text = "RESUME";
            pauseBtn.BackColor = Color.DarkGreen;
            threadPauseState.Paused = true;
            UpdateTestsStats();
            solvingPanel.Visible = false;
        }
    }

    private void CancelBtn_Click(object sender, EventArgs e)
    {
        taskWorker.CancelAsync();
        testsPanel.Visible = false;
    }
    
    //Undo selection
    private void TaskDataGridView_SelectionChanged(object sender, EventArgs e)
    {
        testDataGridView.ClearSelection();
    }
}