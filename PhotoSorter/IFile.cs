using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoSorter
{
    public interface IFile
    {
        String FullPath { get; set; }
        Byte[] Data { get; set; }
    }
}
