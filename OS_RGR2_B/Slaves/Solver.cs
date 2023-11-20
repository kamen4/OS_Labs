using OS_RGR2_B.Models;
using System.ComponentModel;

namespace OS_RGR2_B.Slaves;

internal static class Solver
{
    public static void Solve(
        Queue<ValidatedTest> solveQueue,
        Mutex solveQueueMtx,
        Queue<ValidatedTest> checkQueue,
        Mutex checkQueueMutex,
        ref bool validated,
        Mutex validatedMtx,
        ref bool solved,
        Mutex solvedMtx,
        BackgroundWorker worker,
        StatusVM status,
        Mutex statusMtx,
        ThreadPauseState threadPauseState)
    {
        while (true)
        {
            threadPauseState.Wait();

            ValidatedTest current = null;
            solveQueueMtx.WaitOne();
            if (solveQueue.Count > 0)
                current = solveQueue.Dequeue();
            solveQueueMtx.ReleaseMutex();

            if (current == null)
            {
                validatedMtx.WaitOne();
                if (!validated)
                {
                    validatedMtx.ReleaseMutex();
                    continue;
                }
                else
                {
                    validatedMtx.ReleaseMutex();
                    break;
                }
            }

            Decryptor.Decryptor d = new(current.test);
            var res = d.Solve();
            using (StreamWriter sw = new($"{current.dir}\\{current.model.Name}.MYOUT"))
            {
                foreach (bool b in res)
                    sw.WriteLine(b ? "YES" : "NO");
            }
            checkQueueMutex.WaitOne();
            checkQueue.Enqueue(current);
            checkQueueMutex.ReleaseMutex();
            current.model.Status = TestStatus.Checking;
            statusMtx.WaitOne();
            status.ready--;
            status.solved++;
            statusMtx.ReleaseMutex();
        }
        solvedMtx.WaitOne();
        solved = true;
        solvedMtx.ReleaseMutex();
    }
}
