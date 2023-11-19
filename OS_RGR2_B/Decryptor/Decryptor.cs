using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OS_RGR2_B.Decryptor;

internal class Decryptor
{
    public List<Test> Tests { get; set; }

    public Decryptor(List<Test> tests)
    {
        this.Tests = tests;
    }

    public List<bool> Solve()
    {
        List<bool> result = new();
        foreach (Test test in Tests)
            result.Add(Decrypt(test));
        return result;
    }

    private bool Decrypt(Test test)
    {
        int[] codeInv = Invariant(test.Code);
        int[] messInv = Invariant(test.Message);

        if (codeInv.Length != messInv.Length)
            return false;

        for (int i = 0; i < codeInv.Length; i++)
            if (codeInv[i] != messInv[i])
                return false;
        return true;
    }

    private int[] Invariant(string s)
    {
        return s.GroupBy(x => x).Select(x => x.Count()).OrderBy(x => x).ToArray();
    }
}
