using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Net.Http.Headers;

namespace LabFileSearch
{
    public partial class Form1 : Form
    {
        List<BindingList<FileModel>> bindingLists;
        List<List<string>> dataLists;
        List<ListBox> listBoxes;
        string dir;
        int maxDepth;
        string pattern;

        public Form1()
        {
            InitializeComponent();
            DepthTextBox.Enabled = false;
            bindingLists = new()
                { new(), new() };
            listBoxes = new()
                { listBox1, listBox2 };
            for (int i = 0; i < 2; i++)
            {
                listBoxes[i].DataSource = bindingLists[i];
                listBoxes[i].DisplayMember = "Name";
            }
            dataLists = new()
                { new(), new() };
            dir = string.Empty;
            maxDepth = int.MaxValue;
            pattern = string.Empty;
        }

        private void SelectDirBtn_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                DirTextBox.Text = fbd.SelectedPath;
        } 

        private void DepthTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) || DepthTextBox.Text.Length >= 3;
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            dir = DirTextBox.Text;
            pattern = PatternTextBox.Text;
            maxDepth = int.MaxValue;
            if (DepthTextBox.Text.Length > 0)
                maxDepth = int.Parse(DepthTextBox.Text);

            if (!Directory.Exists(dir))
            {
                DirTextBox.ForeColor = Color.Red;
                MessageBox.Show(
                    "Such directory dosent exist",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DirTextBox.Focus();
                return;
            }

            SearchBtn.Enabled = false;
            result1Label.Text = "Thread 1 result: (computing)";
            result2Label.Text = "Thread 2 result: (computing)";
            BackgroundWorker worker = new();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            result1Label.Text = $"Thread 1 result: ({dataLists[0].Count} items)";
            result2Label.Text = $"Thread 2 result: ({dataLists[1].Count} items)";
            Update(); //too long UI output
            foreach (var l in listBoxes)
                l.BeginUpdate();
            foreach (var l in bindingLists)
                l.Clear();
            for (int i = 0; i < 2; i++)
                foreach (var el in dataLists[i])
                    bindingLists[i].Add(new FileModel(el));
            foreach (var el in listBoxes)
                el.EndUpdate();
            SearchBtn.Enabled = true;
        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
        {   
            foreach (var l in dataLists)
                l.Clear();
            Queue<(string, int)> dirsQueue = new();
            dirsQueue.Enqueue((dir, 0));

            async Task Search((string, int) _dir, int threadId)
            {
                string[] dirs;
                //COSTYl for denied directories
                try
                {
                    dirs = Directory.GetDirectories(_dir.Item1);
                }
                catch
                {
                    return;
                }
                if (SubDir—heckBox.Checked && _dir.Item2 < maxDepth)
                    foreach (var d in dirs)
                        dirsQueue.Enqueue((d, _dir.Item2 + 1));
                if (FilesCheckBox.Checked)
                    dataLists[threadId].AddRange(Directory.GetFiles(_dir.Item1, pattern));
                if (DirsCheckBox.Checked)
                    dataLists[threadId].AddRange(Directory.GetDirectories(_dir.Item1, pattern));
                //due to the fact that the first thread is working too fast, the second has nowhere to work
                //and everything comes out in one list, but you can connect artificial delays and everything will be OK
                //await Task.Delay(new Random().Next(100));
            }
            List<Task> tasks = new();
            int id = 0;
            while (dirsQueue.Count > 0)
            {
                if (tasks.Count == 2)
                    tasks[id] = Search(dirsQueue.Dequeue(), id);
                else
                    tasks.Add(Search(dirsQueue.Dequeue(), tasks.Count));
                id = Task.WaitAny(tasks.ToArray());
            }
            Task.WaitAll(tasks.ToArray());
        }

        private void DirTextBox_TextChanged(object sender, EventArgs e)
        {
            DirTextBox.ForeColor = Color.Black;
        }

        private void SubDir—heckBox_CheckedChanged(object sender, EventArgs e)
        {
            DepthTextBox.Enabled = SubDir—heckBox.Checked;
        }

        private void PatternTextBox_TextChanged(object sender, EventArgs e)
        {
            PatternTextBox.ForeColor = Color.Black;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.Items[listBox1.SelectedIndex] is FileModel fm)
                OpenFile(fm.Path);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1 && listBox2.Items[listBox2.SelectedIndex] is FileModel fm)
                OpenFile(fm.Path);
        }

        private void OpenFile(string path)
        {
            try
            {
                Process.Start("explorer.exe", path);
            }
            catch
            {
                MessageBox.Show(
                    "Open file denied",
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}