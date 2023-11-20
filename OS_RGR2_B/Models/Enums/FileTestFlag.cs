namespace OS_RGR2_B.Models.Enums;

[Flags]
internal enum FileTestFlag
{
    None = 0,
    IN = 0b1,
    OUT = 0b10,
    BOTH = IN | OUT
}
