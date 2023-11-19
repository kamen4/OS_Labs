using OS_RGR2_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_RGR2_B.Decryptor;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace OS_RGR2_B.Slaves;

internal static class Validator
{
    public static void Validate(
        string dir,
        List<TestFileModel> testFiles,
        Queue<ValidatedTest> solveQueue,
        Mutex solveQueueMtx,
        ref bool validated,
        Mutex validatedMtx,
        BackgroundWorker worker,
        Mutex workerMtx,
        StatusViewModel status,
        Mutex statusMtx)
    {
        for (int i = 0; i < testFiles.Count; ++i)
        {
            var file = testFiles[i];
            file.Status = TestStatus.Validation;
            Report(i, worker, workerMtx);
            List<Test> tests = new();
            using (StreamReader sr = new($"{dir}\\{file.Name}.IN"))
            {
                if (!int.TryParse(sr.ReadLine(), out int K) || K < 1 || K > 5)
                {
                    file.Status = TestStatus.Invalid;
                    statusMtx.WaitOne();
                    status.ready--;
                    status.invalid++;
                    statusMtx.ReleaseMutex();
                    Report(i, worker, workerMtx);
                    continue;
                }
                bool err = false;
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
                        tests.Add(new Test(
                            sr.ReadLine() ?? throw new NullReferenceException(),
                            sr.ReadLine() ?? throw new NullReferenceException()));
                    }
                    catch (Exception)
                    {
                        err = true;
                        break;
                    }
                }
                if (err)
                {
                    file.Status = TestStatus.Invalid;
                    statusMtx.WaitOne();
                    status.ready--;
                    status.invalid++;
                    statusMtx.ReleaseMutex();
                    Report(i, worker, workerMtx);
                    continue;
                }
            }
            file.Status = TestStatus.Solving;
            Report(i, worker, workerMtx);
            solveQueueMtx.WaitOne();
            solveQueue.Enqueue(new ValidatedTest
            {
                id = i,
                model = file,
                dir = dir,
                test = tests
            });
            solveQueueMtx.ReleaseMutex();

        }

        validatedMtx.WaitOne();
        validated = true;
        validatedMtx.ReleaseMutex();
    }

    private static void Report(int i, BackgroundWorker worker, Mutex mutex)
    {
        mutex.WaitOne();
        worker.ReportProgress(i);
        mutex.ReleaseMutex();
    }
}
