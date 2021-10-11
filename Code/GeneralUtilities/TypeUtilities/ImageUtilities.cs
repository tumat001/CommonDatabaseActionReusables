using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities
{
    public class ImageUtilities
    {
        public static byte[] ConvertImageToBytes(Image image)
        {
            if (image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    return ms.ToArray();
                }
            }
            else
            {
                return null;
            }
        }

        public static Image ConvertBytesToImage(byte[] byteArray)
        {
            if (byteArray != null)
            {
                MemoryStream ms = new MemoryStream(byteArray);
                Image image = Image.FromStream(ms);
                return image;
            }
            else
            {
                return null;
            }
        }

    }
}