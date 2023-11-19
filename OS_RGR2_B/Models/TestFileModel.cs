using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
