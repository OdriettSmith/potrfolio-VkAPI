using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TestVK.Utils
{
    internal static class Util
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static Bitmap LoadBitmap(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                return new Bitmap(fs);
        }

        public static bool CompareImages(Bitmap exeptedImg, Bitmap actualImg)
        {
            if (!exeptedImg.Size.Equals(actualImg.Size))
            {
                return false;
            }
            for (int x = 0; x < exeptedImg.Width; ++x)
            {
                for (int y = 0; y < exeptedImg.Height; ++y)
                {
                    if (exeptedImg.GetPixel(x, y) != actualImg.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
