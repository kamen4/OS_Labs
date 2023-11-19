using System.ComponentModel;
using System.Security.Policy;
using System.Windows.Forms;
using OS_RGR2_B.Decryptor;
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
    BackgroundWorker worker;
    Mutex workerMtx;
    //stats
    StatusViewModel status;
    Mutex statusMtx;
    public Form1()
    {
        InitializeComponent();
        //set up BackgroundWorker
        worker = new()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.DoWork += Worker_DoWork;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        workerMtx = new();
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

        //set cuurent directory
        curDir = testsDirTextBox.Text;

        //find files ends with .OUT ot .IN
        //and select their filename withot full path
        var files = Directory
            .GetFiles(curDir)
            .Where(x => x.EndsWith(".OUT") || x.EndsWith(".IN"))
            .Select(x => x.Split('\\')[^1]);

        //creating dictionary to check only tests wich have both .OUT and .IN files
        Dictionary<string, TestType> filesDict = new();
        foreach (var file in files)
        {
            string name = string.Concat(file.Split('.')[..^1]);
            if (!filesDict.ContainsKey(name))
                filesDict.Add(name, TestType.None);
            filesDict[name] |= (file.EndsWith(".OUT") ? TestType.OUT : TestType.IN);
        }

        //clear our list
        testsBindingList.Clear();
        //fill it with valid test files values
        int id = 0;
        foreach (var file in filesDict)
        {
            if (file.Value == (TestType.OUT | TestType.IN))
                testsBindingList.Add(new TestFileModel(file.Key, TestStatus.Ready));
        }

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
            status.total = testsBindingList.Count;
            status.ready = testsBindingList.Count;
            status.invalid = status.solved = status.done = status.wrong = 0;
            UpdateStatTextBox();
            testsPanel.Visible = true;
        }

    }

    private void TestsDirTextBox_TextChanged(object sender, EventArgs e)
    {
        //set color to black (in case it was red throw the error)
        testsDirTextBox.ForeColor = Color.Black;
    }

    private void RunBtn_Click(object sender, EventArgs e)
    {
        worker.RunWorkerAsync();
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
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
                worker,
                workerMtx,
                status,
                statusMtx);
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
                worker,
                workerMtx,
                status,
                statusMtx);
        });
        Thread usless = new(() =>
        {
            Usless.Use(
                checkQueue,
                checkQueueMtx,
                ref (solved),
                solvedMtx,
                worker,
                workerMtx,
                status,
                statusMtx);
        });
        validator.Start();
        solver.Start();
        usless.Start();
        validator.Join();
        solver.Join();
        usless.Join();
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        int row = e.ProgressPercentage;
        TestStatusColor stat = testsBindingList[row].Status switch
        {
            TestStatus.Ready => TestStatusColor.Ready,
            TestStatus.Validation => TestStatusColor.Validation,
            TestStatus.Invalid => TestStatusColor.Invalid,
            TestStatus.Solving => TestStatusColor.Solving,
            TestStatus.Checking => TestStatusColor.Checking,
            TestStatus.Done => TestStatusColor.Done,
            TestStatus.Wrong => TestStatusColor.Wrong,
            _ => TestStatusColor.Ready
        };
        dataGridView1.UpdateCellValue(1, row);
        dataGridView1.Rows[row].Cells[1].Style.ForeColor = Color.FromArgb((int)stat);
        dataGridView1.Update();
        UpdateStatTextBox();
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        // Method intentionally left empty.
    }

    private void DataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        dataGridView1.ClearSelection();
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
}