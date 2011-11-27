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
        private const int DATE_TAKEN = 0x9003;

        public static DateTime GetDateTaken(Stream photoStream)
        {
            DateTime dateTaken = DateTime.MinValue;

            using (Image image = Image.FromStream(photoStream))
            {
                PropertyItem dateTakenItem = image.GetPropertyItem(DATE_TAKEN);
                String dateTakenValue = Encoding.ASCII.GetString(dateTakenItem.Value);
                dateTakenValue = dateTakenValue.Replace("\0", string.Empty);
                dateTaken = DateTime.ParseExact(dateTakenValue, "yyyy:MM:dd HH:mm:ss", CultureInfo.CurrentCulture.DateTimeFormat);
            }

            return dateTaken;
        }
    }
}
