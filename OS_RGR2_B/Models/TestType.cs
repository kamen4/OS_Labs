using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_RGR2_B.Models;

[Flags]
enum TestType
{ 
    None = 0,
    IN = 0b1,
    OUT = 0b10
}
