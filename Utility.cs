using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace CRUD_Workout
{
    internal class Utility
    {
        public static byte[] ImageToByteArray(FileStream file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (MemoryStream memory = new MemoryStream(byteArray))
            {
                BitmapImage imgSource = new BitmapImage();

                if (memory != null && memory.Length > 0)
                {
                    memory.Position = 0;
                    imgSource.BeginInit();
                    imgSource.CacheOption = BitmapCacheOption.OnLoad;
                    imgSource.StreamSource = memory;
                    imgSource.EndInit();
                }

                return imgSource;
            }
        }
    }
}
