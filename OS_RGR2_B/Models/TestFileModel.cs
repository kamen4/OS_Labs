namespace OS_RGR2_B.Models;

class TestFileModel
{
    public string Name { get; private set; }
    public TestStatus Status { get; set; }
    public TestFileModel(string name, TestStatus status)
    {
        Name = name;
        Status = status;
    }
}
