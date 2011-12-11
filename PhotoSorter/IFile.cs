using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhotoSorter
{
    public interface IFile
    {
        String FullPath { get; set; }
        String FileName { get; set; }
        Stream GetFileStream();
    }
}
