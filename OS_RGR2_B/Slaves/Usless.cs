using OS_RGR2_B.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_RGR2_B.Slaves;

internal static class Usless
{
    public static void Use(
        Queue<ValidatedTest> checkQueue,
        Mutex checkQueueMutex,
        ref bool solved,
        Mutex solvedMtx,
        BackgroundWorker worker,
        Mutex workerMtx,
        StatusViewModel status,
        Mutex statusMtx)
    {
        while (true)
        {
            ValidatedTest current = null;
            checkQueueMutex.WaitOne();
            if (checkQueue.Count > 0)
                current = checkQueue.Dequeue();
            checkQueueMutex.ReleaseMutex();

            if (current == null)
            {
                solvedMtx.WaitOne();
                if (!solved)
                {
                    solvedMtx.ReleaseMutex();
                    continue;
                }
                else
                {
                    solvedMtx.ReleaseMutex();
                    break;
                }
            }
            using StreamReader srOut = new($"{current.dir}\\{current.model.Name}.OUT");
            using StreamReader srSolved = new($"{current.dir}\\{current.model.Name}.MYOUT");
            if (srOut.ReadToEnd() == srSolved.ReadToEnd())
            {
                current.model.Status = TestStatus.Done;
                statusMtx.WaitOne();
                status.done++;
                statusMtx.ReleaseMutex();
            }
            else
            {
                current.model.Status = TestStatus.Wrong;
                statusMtx.WaitOne();
                status.wrong++;
                statusMtx.ReleaseMutex();
            }

            Report(current.id, worker, workerMtx);
        }
    }

    private static void Report(int i, BackgroundWorker worker, Mutex mutex)
    {
        mutex.WaitOne();
        worker.ReportProgress(i);
        mutex.ReleaseMutex();
    }
}
