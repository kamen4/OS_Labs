using OS_RGR2_B.Models.Enums;

namespace OS_RGR2_B.Models.ViewModels;

internal class FileTestVM
{
    public string Name { get; set; }
    public TestStatus Status { get; set; }
    public int Id;
    public string Dir;
    public List<TestCase>? Tests;
    public FileTestVM()
    {
        Name = "";
        Status = TestStatus.Ready;
        Id = 0;
        Dir = "";
        Tests = null;
    }
}
