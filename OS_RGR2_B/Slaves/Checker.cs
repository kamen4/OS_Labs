using OS_RGR2_B.Models;
using OS_RGR2_B.Models.Enums;
using OS_RGR2_B.Models.ViewModels;
using System.ComponentModel;

namespace OS_RGR2_B.Slaves;

internal class Checker
{
    //queue with solved tests
    readonly Queue<FileTestVM> checkQueue;
    readonly Mutex checkQueueMutex;

    //determines whether all tests are solved
    readonly SyncFlag solved;

    //status panel VM
    readonly StatusVM status;
    readonly Mutex statusMtx;

    //pause controler
    readonly ThreadPauseState threadPauseState;

    public Checker(
        Queue<FileTestVM> checkQueue,
        Mutex checkQueueMutex,
        SyncFlag solved,
        StatusVM status,
        Mutex statusMtx,
        ThreadPauseState threadPauseState)
    {
        this.checkQueue = checkQueue;
        this.checkQueueMutex = checkQueueMutex;
        this.solved = solved;
        this.status = status;
        this.statusMtx = statusMtx;
        this.threadPauseState = threadPauseState;
    }

    private void CheckOne(FileTestVM currentTest)
    {
        using StreamReader srOut = new($"{currentTest.Dir}\\{currentTest.Name}.OUT");
        using StreamReader srSolved = new($"{currentTest.Dir}\\{currentTest.Name}.MYOUT");
        if (srOut.ReadToEnd() == srSolved.ReadToEnd())
        {
            currentTest.Status = TestStatus.Done;
            statusMtx.WaitOne();
            status.Done++;
            statusMtx.ReleaseMutex();
        }
        else
        {
            currentTest.Status = TestStatus.Wrong;
            statusMtx.WaitOne();
            status.Wrong++;
            statusMtx.ReleaseMutex();
        }
    }

    public void CheckAll()
    {
        while (true)
        {
            threadPauseState.Wait();

            FileTestVM? currentTest = null;
            checkQueueMutex.WaitOne();
            if (checkQueue.Count > 0)
                currentTest = checkQueue.Dequeue();
            checkQueueMutex.ReleaseMutex();

            if (currentTest == null)
            {
                if (!solved.Get())
                    continue;
                else
                    break;
            }

            CheckOne(currentTest);
        }
    }
}
