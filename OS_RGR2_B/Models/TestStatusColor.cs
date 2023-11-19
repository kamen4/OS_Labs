using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_RGR2_B.Models;

enum TestStatusColor
{
    Ready = 0x000000,
    Validation = 0xFFBF00,
    Invalid = 0xFF0000,
    Solving = 0xFCAE1E,
    Checking = 0xFC6A03,
    Done = 0x00FF00,
    Wrong = 0xAA0000
}