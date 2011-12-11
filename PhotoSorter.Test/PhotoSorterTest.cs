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
            Mock<IFile> mockFile = new Mock<IFile>();
            mockFile.Setup(file => file.GetFileStream())
                .Returns(GetResourceStream(@"PhotoSorter.Test.Resources.photo_with_valid_date.jpg"));
            mockFile.SetupGet(file => file.FileName)
                .Returns("photo_with_valid_date.jpg");
            mockFile.SetupGet(file => file.FullPath)
                .Returns(@"C:\Test\photo_with_valid_date.jpg");

            List<IFile> files = new List<IFile>{mockFile.Object};

            Mock<IFileService> mockFileService = new Mock<IFileService>();
            mockFileService.Setup(service => service.GetFilePathsInFolderRecursively(@"C:\Test"))
                .Returns(files);

            UnityContainer container = new UnityContainer();
            container.RegisterInstance(typeof(IFileService), mockFileService.Object);
            PhotoSorter sorter = container.Resolve<PhotoSorter>();
            sorter.Sort(@"C:\Test", @"C:\Test2");

            mockFileService.Verify(service => service.CopyFile(@"C:\Test\photo_with_valid_date.jpg", 
@"C:\Test2\2011-08-05\photo_with_valid_date.jpg"));
        }

        private Stream GetResourceStream(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }
    }
}
