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

        public static DateTime GetDateTaken(Stream photoStream)
        {
            DateTime dateTaken = DateTime.MinValue;
            using (Image image = Image.FromStream(photoStream))
            {
                String dateTakenValue = GetDateTakenStringFromImage(image);
                dateTaken = DateTime.ParseExact(dateTakenValue, DATE_TAKEN_FORMAT, CultureInfo.CurrentCulture.DateTimeFormat);
            }

            return dateTaken;
        }

        private static String GetDateTakenStringFromImage(Image image)
        {
            PropertyItem dateTakenItem = image.GetPropertyItem(DATE_TAKEN);
            String dateTakenValue = Encoding.ASCII.GetString(dateTakenItem.Value);
            return dateTakenValue.Replace(NULL_CHARACTER, String.Empty);
        }
    }
}
