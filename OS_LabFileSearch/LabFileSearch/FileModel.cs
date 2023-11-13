using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabFileSearch;

internal class FileModel
{
    public string Path { get; set; }
    public string Name => Path.Split('\\')[^1];
    public FileModel (string path)
    {
        Path = path;
    }
}
