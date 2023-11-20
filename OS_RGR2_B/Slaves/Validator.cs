﻿using OS_RGR2_B.Models;
using OS_RGR2_B.Decryptor;
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
        StatusVM status,
        Mutex statusMtx,
        ThreadPauseState threadPauseState)
    {
        for (int i = 0; i < testFiles.Count; ++i)
        {
            threadPauseState.Wait();

            var file = testFiles[i];
            file.Status = TestStatus.Validation;
            List<TestCase> tests = new();
            using (StreamReader sr = new($"{dir}\\{file.Name}.IN"))
            {
                if (!int.TryParse(sr.ReadLine(), out int K) || K < 1 || K > 5)
                {
                    file.Status = TestStatus.Invalid;
                    statusMtx.WaitOne();
                    status.ready--;
                    status.invalid++;
                    statusMtx.ReleaseMutex();
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
                        string mes = sr.ReadLine() ?? throw new NullReferenceException();
                        string cod = sr.ReadLine() ?? throw new NullReferenceException();
                        if (mes.Length > 100 ||
                            string.IsNullOrEmpty(mes) ||
                            string.IsNullOrEmpty(cod) ||
                            cod.Length > 100)
                        {
                            err = true;
                            break;
                        }
                        tests.Add(new TestCase(mes, cod));
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
                    continue;
                }
            }
            file.Status = TestStatus.Solving;
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
}
