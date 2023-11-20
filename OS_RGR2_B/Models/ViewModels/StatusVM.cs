namespace OS_RGR2_B.Models.ViewModels;

internal class StatusVM
{
    public int Total { get; set; }
    public int Ready { get; set; }
    public int Invalid { get; set; }
    public int Solved { get; set; }
    public int Done { get; set; }
    public int Wrong { get; set; }
    public StatusVM()
    {
        Total = 0;
        Ready = 0;
        Invalid = 0;
        Solved = 0;
        Done = 0;
        Wrong = 0;
    }
}
