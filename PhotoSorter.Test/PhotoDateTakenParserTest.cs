using System;
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
        public void Test_WhenProvidedAPhotoWithValidDate_ThenParsesCorrectDate()
        {
            DateTime expectedDate = DateTime.Parse("8/5/2011 3:04:42 PM");
            DateTime actualDate = DateTime.MinValue;
            using (MemoryStream stream = new MemoryStream(Resources.photo_with_valid_date_jpg))
            {
                actualDate = PhotoDateTakenParser.GetDateTaken(stream);
            }

            Assert.AreEqual(expectedDate, actualDate);
        }
    }
}
