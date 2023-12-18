using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace RockPaperScissorsAI
{
    public class Picture
    {
        public static Bitmap ConvertToBlackAndWhite(Bitmap originalImage)
        {
            Bitmap blackAndWhiteImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    System.Drawing.Color pixelColor = originalImage.GetPixel(x, y);
                    int avgColor = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    var newColor = System.Drawing.Color.FromArgb(avgColor, avgColor, avgColor);
                    blackAndWhiteImage.SetPixel(x, y, newColor);
                }
            }
            return blackAndWhiteImage;
        }

        public static Bitmap GetFirstQuarter(Bitmap originalImage)
        {
            int Width = originalImage.Width;
            int Height = originalImage.Height / 4;

            var firstQuarterRect = new System.Drawing.Rectangle(0, 0, Width, Height);
            Bitmap firstQuarter = originalImage.Clone(firstQuarterRect, originalImage.PixelFormat);

            return firstQuarter;
        }

        public static int CountBlackPixels(Bitmap image)
        {
            int blackPixelCount = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixelColor = image.GetPixel(x, y);
                    if (pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0)
                    {
                        blackPixelCount++;
                    }
                }
            }
            return blackPixelCount;
        }

        public static Bitmap GetBitmapFromButton(Button button)
        {
            Bitmap bitmap = null;
            if (button.Background is System.Windows.Media.ImageBrush imageBrush && imageBrush.ImageSource != null)
            {
                System.Windows.Media.Imaging.BitmapSource bitmapSource = (System.Windows.Media.Imaging.BitmapSource)imageBrush.ImageSource;
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    System.Windows.Media.Imaging.BitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapSource));
                    encoder.Save(stream);
                    bitmap = new Bitmap(stream);
                }
            }
            return bitmap;
        }

        public static ImageBrush GetBackgroundFromBitmap(Bitmap bitmap)
        {
            System.Windows.Media.ImageBrush imageBrush = null;
            if (bitmap != null)
            {
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                imageBrush = new System.Windows.Media.ImageBrush(bitmapImage);
            }
            return imageBrush;
        }
    }
}
