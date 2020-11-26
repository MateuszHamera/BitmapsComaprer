using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BitmapsComaprer
{
    public static class BitmapUtilities
    {
        public static byte[] ToArray(this Bitmap bitmap)
        {
            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                    ImageLockMode.ReadOnly, 
                    bitmap.PixelFormat);

                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }            
        }
        public static byte[,] To2DArray(this Bitmap bitmap)
        {
            byte[] pixels = bitmap.ToArray();

            byte[,] array2D = new byte[bitmap.Width, bitmap.Height];
            int index = 0;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    array2D[i, j] = pixels[index];
                    index++;
                }
            }

            return array2D;
        }       
    }
}
