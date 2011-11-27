using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace PhotoSorter
{
    public static class PhotoDateTakenParser
    {
        private const Int32 DATE_TAKEN = 0x9003;
        private const String DATE_TAKEN_FORMAT = "yyyy:MM:dd HH:mm:ss";
        private const String NULL_CHARACTER = "\0";

        public static DateTime? GetDateTaken(Stream photoStream)
        {
            DateTime? dateTaken = null;
            String dateTakenValue = null;
            using (Image image = Image.FromStream(photoStream))
            {
                dateTakenValue = GetDateTakenStringFromImage(image);
            }

            if (dateTakenValue != null)
            {
                dateTaken = DateTime.ParseExact(dateTakenValue, DATE_TAKEN_FORMAT, CultureInfo.CurrentCulture.DateTimeFormat);
            }

            return dateTaken;
        }

        private static String GetDateTakenStringFromImage(Image image)
        {
            String value = null;
            PropertyItem dateTakenItem = GetDateTakenPropertyItem(image);
            if (dateTakenItem != null)
            {
                value = ConvertToStringWithoutNullTerminator(dateTakenItem.Value);
            }

            return value;
        }

        private static String ConvertToStringWithoutNullTerminator(byte[] data)
        {
            String dateTakenValue = Encoding.ASCII.GetString(data);
            return dateTakenValue.Replace(NULL_CHARACTER, String.Empty);
        }

        private static PropertyItem GetDateTakenPropertyItem(Image image)
        {
            PropertyItem dateTakenItem = null;
            try
            {
                dateTakenItem = image.GetPropertyItem(DATE_TAKEN);
            }
            catch (ArgumentException) { }
            return dateTakenItem;
        }
    }
}
