using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_RGR2_B.Models;

internal class SyncFlag
{
    bool flag;
    readonly Mutex mutex;
    public SyncFlag (bool flag)
    {
        this.flag = flag;
        mutex = new();
    }

    public void Set(bool flag)
    {
        mutex.WaitOne();
        this.flag = flag;
        mutex.ReleaseMutex();
    }

    public bool Get()
    {
        bool ret;
        mutex.WaitOne();
        ret = flag;
        mutex.ReleaseMutex();
        return ret;
    }
}
