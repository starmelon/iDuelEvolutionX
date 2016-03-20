using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace iDuel_EvolutionX.Tools
{
    class BitmapImagehandle
    {
        #region <-- 获得卡片引用 -->

        public static BitmapImage GetBitmapImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();

            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = new MemoryStream(File.ReadAllBytes(path));
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        #endregion

        #region <-- 2进制数组转换为图像 -->

        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }

            return bmp;
        }

        #endregion

        #region <-- 图片转换为2进制数组 -->

        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;

            try
            {
                Stream sMarket = bmp.StreamSource;

                if (sMarket != null && sMarket.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。
                    sMarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling
            }

            return byteArray;
        }

        #endregion
    }
}
