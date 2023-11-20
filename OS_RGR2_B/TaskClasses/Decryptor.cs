using OS_RGR2_B.Models;

namespace OS_RGR2_B.TaskClasses;

internal class Decryptor
{
    public List<TestCase> Tests { get; set; }

    public Decryptor(List<TestCase> tests)
    {
        Tests = tests;
    }

    public List<bool> Solve()
    {
        List<bool> result = new();
        foreach (TestCase test in Tests)
            result.Add(Decrypt(test));
        return result;
    }

    private bool Decrypt(TestCase test)
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
