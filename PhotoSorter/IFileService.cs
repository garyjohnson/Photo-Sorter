using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoSorter
{
    public interface IFileService
    {
        IEnumerable<IFile> GetFilePathsInFolderRecursively(String path);
        void CopyFile(String sourcePath, String destinationPath);
    }
}
