namespace OS_RGR2_B.Models.Enums;

internal enum TestStatus
{
    Ready = 0x000000,       //black
    Validation = 0xFFBF00,  //dark yellow
    Invalid = 0xFF0000,     //red
    Solving = 0xFCAE1E,     //dark dark yellow
    Checking = 0xFC6A03,    //dark orange
    Done = 0x00FF00,        //green
    Wrong = 0xAA0000        //dark red
}

internal static class TestStatusExtensions
{
    public static Color StatusColor(this TestStatus self)
    {
        return Color.FromArgb((int)self);
    }
    public static string StatusString(this TestStatus self)
    {
        return self.ToString();
    }
}