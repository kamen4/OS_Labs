using OS_RGR2_B.Models;
using System.ComponentModel;
using OS_RGR2_B.Models.ViewModels;
using OS_RGR2_B.Models.Enums;

namespace OS_RGR2_B.Slaves;

internal class Validator
{
    //ref for testdataGridView data source
    readonly List<FileTestVM> testFiles;

    //queue with validated tests
    readonly Queue<FileTestVM> solveQueue;
    readonly Mutex solveQueueMtx;

    //determines whether all tests are validated
    readonly SyncFlag validated;

    //status panel VM
    readonly StatusVM status;
    readonly Mutex statusMtx;

    //pause controler
    readonly ThreadPauseState threadPauseState;

    //current test index in testFiles list
    int idx;

    public Validator(
        List<FileTestVM> testFiles,
        Queue<FileTestVM> solveQueue,
        Mutex solveQueueMtx,
        SyncFlag validated,
        StatusVM status,
        Mutex statusMtx,
        ThreadPauseState threadPauseState)
    {
        this.testFiles = testFiles;
        this.solveQueue = solveQueue;
        this.solveQueueMtx = solveQueueMtx;
        this.validated = validated;
        this.status = status;
        this.statusMtx = statusMtx;
        this.threadPauseState = threadPauseState;
        idx = -1;
    }

    private void ValidateCur()
    {
        //fetching current test
        FileTestVM curFileTest = testFiles[idx];

        //set status to VALIDATION
        testFiles[idx].Status = TestStatus.Validation;
        
        //creatung test
        List<TestCase> testCases = new();

        //read test from file
        using (StreamReader sr = new($"{curFileTest.Dir}\\{curFileTest.Name}.IN"))
        {
            //validate tests count
            if (!int.TryParse(sr.ReadLine(), out int K) || K < 1 || K > 5)
            {
                curFileTest.Status = TestStatus.Invalid;
               
                statusMtx.WaitOne();
                status.Ready--;
                status.Invalid++;
                statusMtx.ReleaseMutex();
                
                return;
            }

            //is test invalid
            bool err = false;

            //for determine whether there more than K test cases
            int cnt = 0;
            while (!sr.EndOfStream)
            {
                if (++cnt > K)
                {
                    err = true;
                    break;
                }
                try
                {
                    string mes = sr.ReadLine() ?? throw new NullReferenceException();
                    string cod = sr.ReadLine() ?? throw new NullReferenceException();
                    if (mes.Length > 100 ||
                        cod.Length > 100 ||
                        string.IsNullOrEmpty(mes) ||
                        string.IsNullOrEmpty(cod))
                    {
                        err = true;
                        break;
                    }
                    testCases.Add(new TestCase(mes, cod));
                }
                catch (Exception)
                {
                    err = true;
                    break;
                }
            }
            if (err)
            {
                curFileTest.Status = TestStatus.Invalid;
                statusMtx.WaitOne();
                status.Ready--;
                status.Invalid++;
                statusMtx.ReleaseMutex();
                return;
            }
        }
        //change tests params
        curFileTest.Status = TestStatus.Solving;
        curFileTest.Tests = testCases;
        curFileTest.Id = idx;

        //push test to sole queue
        solveQueueMtx.WaitOne();
        solveQueue.Enqueue(curFileTest);
        solveQueueMtx.ReleaseMutex();
    }

    public void ValidateAll()
    {
        while (++idx < testFiles.Count)
        {
            threadPauseState.Wait();
            ValidateCur();
        }
        validated.Set(true);
    }
}
