using System.ComponentModel;
using OS_RGR2_B.Models;
using OS_RGR2_B.Slaves;

namespace OS_RGR2_B;

public partial class Form1 : Form
{
    //list for tests dataGridView
    readonly BindingList<TestFileModel> testsBindingList;
    //current dirrectory
    string curDir;
    //way not to stop UI
    BackgroundWorker taskWorker;
    BackgroundWorker findWorker;
    //stats
    StatusVM status;
    Mutex statusMtx;
    //pause
    ThreadPauseState threadPauseState;
    //
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
        taskWorker.ProgressChanged += TaskWorker_ProgressChanged;
        taskWorker.DoWork += TaskWorker_DoWork;
        taskWorker.RunWorkerCompleted += TaskWorker_RunWorkerCompleted;
        findWorker = new();
        findWorker.DoWork += FindWorker_DoWork;
        findWorker.RunWorkerCompleted += FindWorker_RunWorkerCompleted;
        //intit curDir to empty
        curDir = "";
        //hide tests panel
        testsPanel.Visible = false;
        //add columns to tests dataGridView
        dataGridView1.Columns.AddRange(
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
        dataGridView1.DataSource = testsBindingList;
        status = new();
        statusMtx = new();
        //
        threadPauseState = new();
        //
        solvingPanel.Visible = false;
    }

    private void FindWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        dataGridView1.DataSource = testsBindingList;
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
            runned = false;
            status.total = testsBindingList.Count;
            status.ready = testsBindingList.Count;
            status.invalid = status.solved = status.done = status.wrong = 0;
            UpdateStatTextBox();
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

    private void FindWorker_DoWork(object? sender, DoWorkEventArgs e)
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
        Dictionary<string, FileTestFlag> filesDict = new();
        foreach (var file in files)
        {
            string name = string.Concat(file.Split('.')[..^1]);
            if (!filesDict.ContainsKey(name))
                filesDict.Add(name, FileTestFlag.None);
            filesDict[name] |= (file.EndsWith(".OUT") ? FileTestFlag.OUT : FileTestFlag.IN);
        }

        //clear our list
        testsBindingList.Clear();
        //fill it with valid test files values
        int id = 0;
        foreach (var file in filesDict)
        {
            if (file.Value == (FileTestFlag.OUT | FileTestFlag.IN))
                testsBindingList.Add(new TestFileModel(file.Key, TestStatus.Ready));
        }
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
        
        dataGridView1.DataSource = null;
        findWorker.RunWorkerAsync();
    }

    private void TestsDirTextBox_TextChanged(object sender, EventArgs e)
    {
        //set color to black (in case it was red throw the error)
        testsDirTextBox.ForeColor = Color.Black;
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

    private void TaskWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        threadPauseState = new();
        bool validated = false;
        Mutex validatedMtx = new();

        bool solved = false;
        Mutex solvedMtx = new();

        Queue<ValidatedTest> solveQueue = new();
        Mutex solveQueueMtx = new();

        Queue<ValidatedTest> checkQueue = new();
        Mutex checkQueueMtx = new();

        Thread validator = new(() =>
        {
            Validator.Validate(
                curDir,
                testsBindingList.ToList(),
                solveQueue,
                solveQueueMtx,
                ref (validated),
                validatedMtx,
                taskWorker,
                status,
                statusMtx,
                threadPauseState);
        });
        Thread solver = new(() =>
        {
            Solver.Solve(
                solveQueue,
                solveQueueMtx,
                checkQueue,
                checkQueueMtx,
                ref (validated),
                validatedMtx,
                ref (solved),
                solvedMtx,
                taskWorker,
                status,
                statusMtx,
                threadPauseState);
        });
        Thread usless = new(() =>
        {
            Usless.Use(
                checkQueue,
                checkQueueMtx,
                ref (solved),
                solvedMtx,
                taskWorker,
                status,
                statusMtx,
                threadPauseState);
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

    private void TaskWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        //TODO
    }

    private void TaskWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        pauseBtn.Enabled = false;
        UpdateTestsStats();
        runBtn.Enabled = true;
        solvingPanel.Visible = false;
    }

    private void DataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        dataGridView1.ClearSelection();
    }

    private void UpdateTestsStats()
    {
        UpdateStatTextBox();
        DataGridViewRowCollection l = dataGridView1.Rows;
        for (int i = 0; i < l.Count; i++)
            l[i].Cells[1].Style.ForeColor = testsBindingList[i].Status.StatusColor();
        dataGridView1.Update();
    }

    private void UpdateStatTextBox()
    {
        statTextBox.Text =
              "Total:\t" + status.total +
            "\nReady:\t" + status.ready +
            "\nInvalid:\t" + status.invalid +
            "\nSolved total:\t" + status.solved +
            "\n\tDone:\t" + status.done +
            "\n\tWrong:\t" + status.wrong;
        statTextBox.Update();
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
}