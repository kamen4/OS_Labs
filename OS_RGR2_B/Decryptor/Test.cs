using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_RGR2_B.Decryptor;

internal class Test
{
    public string Message { get; set; }
    public string Code { get; set; }
    public Test(string message, string code)
    {
        Message = message;
        Code = code;
    }
}
