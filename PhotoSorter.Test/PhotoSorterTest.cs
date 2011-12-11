using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PhotoSorter.Test
{
    [TestClass]
    public class PhotoSorterTest
    {
        [TestMethod]
        public void TestGivenAPhotoWithValidDateThenCreatesAFolderWithThatDateAndCopiesPhoto()
        {
            byte[] buffer;
            using (Stream stream = GetResourceStream(@"PhotoSorter.Test.Resources.photo_with_valid_date.jpg"))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }

            Mock<IFile> mockFile = new Mock<IFile>();
            mockFile.SetupGet(file => file.Data)
                .Returns(buffer);
            mockFile.SetupGet(file => file.FullPath)
                .Returns(@"C:\Test\photo_with_valid_date.jpg");

            List<IFile> files = new List<IFile>{mockFile.Object};

            Mock<IFileService> mockFileService = new Mock<IFileService>();
            mockFileService.Setup(service => service.GetFilePathsInFolderRecursively(@"C:\Test\"))
                .Returns(files);

            mockFileService.Verify(service => service.CopyFile(@"C:\Test\photo_with_valid_date.jpg",
                @"C:\Test2\2011-03-08\photo_with_valid_date.jpg"));

            UnityContainer container = new UnityContainer();
            container.RegisterInstance(typeof (IFileService), mockFileService);
            PhotoSorter sorter = container.Resolve<PhotoSorter>();
            sorter.Sort(@"C:\Test\", @"C:\Test2\");
        }

        private Stream GetResourceStream(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }
    }
}
