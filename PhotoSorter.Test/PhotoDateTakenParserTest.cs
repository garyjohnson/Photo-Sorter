﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using PhotoSorter.Test.Properties;

namespace PhotoSorter.Test
{
    [TestClass]
    public class PhotoDateTakenParserTest
    {
        [TestMethod]
        public void Test_WhenProvidedAPhotoWithValidDate_ThenReturnsCorrectDate()
        {
            DateTime expectedDate = DateTime.Parse("8/5/2011 3:04:42 PM");
            DateTime? actualDate = null;
            using (Stream stream = GetResourceStream(@"PhotoSorter.Test.Resources.photo_with_valid_date.jpg"))
            {
                actualDate = PhotoDateTakenParser.GetDateTaken(stream);
            }

            Assert.IsTrue(actualDate.HasValue, "Expected to parse valid date from photo");
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void Test_WhenProvidedAPhotoWithNoDate_ThenReturnsNullDate()
        {
            DateTime? actualDate = DateTime.Now;
            using (Stream stream = GetResourceStream(@"PhotoSorter.Test.Resources.photo_with_no_date.jpg"))
            {
                actualDate = PhotoDateTakenParser.GetDateTaken(stream);
            }

            Assert.IsFalse(actualDate.HasValue, "Expected to not parse valid date from photo");
        }

        [TestMethod]
        public void Test_WhenProvidedAnInvalidFile_ThenThrowsException()
        {
            Boolean didThrowException = false;
            DateTime? actualDate = null;
            using (Stream stream = GetResourceStream(@"PhotoSorter.Test.Resources.file_not_photo.txt"))
            {
                try
                {
                    actualDate = PhotoDateTakenParser.GetDateTaken(stream);
                }
                catch (ArgumentException)
                {
                    didThrowException = true;
                }
            }

            Assert.IsTrue(didThrowException, "Expected an ArgumentException to be thrown when provided an invalid photo.");
        }

        private Stream GetResourceStream(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }
    }
}
