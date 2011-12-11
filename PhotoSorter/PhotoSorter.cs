using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhotoSorter
{
    public class PhotoSorter
    {
        private IFileService _fileService;

        public PhotoSorter(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void Sort(String sourcePath, String destPath)
        {
            foreach (IFile file in _fileService.GetFilePathsInFolderRecursively(sourcePath))
            {
                DateTime? dateTaken = null;
                using (Stream stream = file.GetFileStream())
                {
                    dateTaken = PhotoDateTakenParser.GetDateTaken(stream);
                }

                if(dateTaken.HasValue)
                {
                    String destFilePath = String.Format("{0}{3}{1}{3}{2}", destPath, 
                        dateTaken.Value.ToString("yyyy-MM-dd"),
                        file.FileName,
                        Path.DirectorySeparatorChar);
                    _fileService.CopyFile(file.FullPath, destFilePath);
                }
            }
        }
    }
}
