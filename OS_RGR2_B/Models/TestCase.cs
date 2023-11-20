namespace OS_RGR2_B.Models;

internal class TestCase
{
    public string Message { get; set; }
    public string Code { get; set; }
    public TestCase(string message = "", string code = "")
    {
        Message = message;
        Code = code;
    }
}
