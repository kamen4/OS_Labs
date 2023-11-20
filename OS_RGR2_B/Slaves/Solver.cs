using OS_RGR2_B.Models;
using OS_RGR2_B.Models.Enums;
using OS_RGR2_B.Models.ViewModels;
using OS_RGR2_B.TaskClasses;
using System.ComponentModel;

namespace OS_RGR2_B.Slaves;

internal class Solver
{
    //queue with validated tests
    readonly Queue<FileTestVM> solveQueue;
    readonly Mutex solveQueueMtx;

    //queue with solved tests
    readonly Queue<FileTestVM> checkQueue;
    readonly Mutex checkQueueMutex;

    //determines whether all tests are validated
    readonly SyncFlag validated;

    //determines whether all tests are solved
    readonly SyncFlag solved;

    //status panel VM
    readonly StatusVM status;
    readonly Mutex statusMtx;

    //pause controler
    readonly ThreadPauseState threadPauseState;

    public Solver(
        Queue<FileTestVM> solveQueue,
        Mutex solveQueueMtx,
        Queue<FileTestVM> checkQueue,
        Mutex checkQueueMutex,
        SyncFlag validated,
        SyncFlag solved,
        StatusVM status,
        Mutex statusMtx,
        ThreadPauseState threadPauseState)
    {
        this.solveQueue = solveQueue;
        this.solveQueueMtx = solveQueueMtx;
        this.checkQueue = checkQueue;
        this.checkQueueMutex = checkQueueMutex;
        this.validated = validated;
        this.solved = solved;
        this.status = status;
        this.statusMtx = statusMtx;
        this.threadPauseState = threadPauseState;
    }

    private void SolveOne(FileTestVM currentTest)
    {
        //create new decryptor
        Decryptor d = new(
            currentTest.Tests ??
            throw new NullReferenceException($"Test with [Name:{currentTest.Name} Id:{currentTest.Id}] is null"));

        //solve and write answer
        List<bool> res = d.Solve();
        using (StreamWriter sw = new($"{currentTest.Dir}\\{currentTest.Name}.MYOUT"))
        {
            res.ForEach(b => sw.WriteLine(b ? "YES" : "NO"));
        }

        //add answer to check queue
        checkQueueMutex.WaitOne();
        checkQueue.Enqueue(currentTest);
        checkQueueMutex.ReleaseMutex();
        
        //set status
        currentTest.Status = TestStatus.Checking;
        statusMtx.WaitOne();
        status.Ready--;
        status.Solved++;
        statusMtx.ReleaseMutex();
    }

    public void SolveAll()
    {
        while (true)
        {
            threadPauseState.Wait();

            FileTestVM? currentTest = null;

            //determines the presence of tests
            solveQueueMtx.WaitOne();
            if (solveQueue.Count > 0)
                currentTest = solveQueue.Dequeue();
            solveQueueMtx.ReleaseMutex();
            if (currentTest == null)
            {
                if (!validated.Get())
                    continue;
                else
                    break;
            }

            SolveOne(currentTest);
        }
        solved.Set(true);
    }
}
