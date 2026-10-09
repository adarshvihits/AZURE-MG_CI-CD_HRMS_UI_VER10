using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using SkiaSharp;
//using SkiaSharp.Views.Desktop;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public class CaptchaGenerator
    {
        private Random random = new Random();
        public (int, string) GenerateCaptcha(int width, int height, int fontSize, int captchaLength)
        {
            //string captchaText = GenerateRandomCaptchaText(captchaLength);
            (int result, string captchaText) = GenerateRandomCaptchaText(captchaLength);

            SKBitmap image = GenerateCaptchaImage(captchaText, width, height, fontSize);
            string base64Image = ConvertBitmapToBase64(image);
            return (result, base64Image);
        }

        private (int, string) GenerateRandomCaptchaText(int length)
        {
            Random random = new Random();
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string nums = "123456789";
            string num1 = new string(Enumerable.Repeat(nums, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string num2 = new string(Enumerable.Repeat(nums, 1).Select(s => s[random.Next(s.Length)]).ToArray());
            int result = Convert.ToInt32(num1) + Convert.ToInt32(num2);
            string captchaText = num1 + " + " + num2;
            return (result, captchaText);
        }

        //        private SKBitmap GenerateCaptchaImage(string text, int width, int height, int fontSize)
        //        {
        //            SKBitmap bitmap = new SKBitmap(width, height);
        //            using (SKCanvas canvas = new SKCanvas(bitmap))
        //            using (SKPaint paint = new SKPaint())
        //            {
        //                canvas.Clear(SKColors.GhostWhite);

        //                //paint.Color = SKColors.White;
        //                paint.TextSize = fontSize;
        //                paint.Style = SKPaintStyle.Stroke;
        //                //paint.FakeBoldText = true;
        //                // Adjust the position and style of the text as needed
        //                SKRect textBounds = new SKRect();
        //                paint.MeasureText(text, ref textBounds);
        //                float x = (width - textBounds.Width) / 2;
        //                float y = (height + textBounds.Height) / 2;
        //                //canvas.DrawLine(x, y, (x+2), (y+2), paint);
        //                SKRect rect = new SKRect(x, y, x + 200, y + 200);
        //                paint.Shader = SKShader.CreateLinearGradient(
        //                                new SKPoint(rect.Left, rect.Top),
        //                                new SKPoint(rect.Right, rect.Bottom),
        //                                new SKColor[] { SKColors.Red, SKColors.Blue },
        //                                new float[] { 0.05f, 0.95f },
        //                                SKShaderTileMode.Repeat);

        //                Random rand = new Random();                
        //                float[] degrees = { -5,-12,5,12 };
        //                int index = rand.Next(degrees.Length); ;



        //                canvas.RotateDegrees(degrees[index]);
        //                canvas.DrawText(text, x, (y+3), paint);
        //            }
        ////#8799ad
        //            return bitmap;
        //        }


        private SKBitmap GenerateCaptchaImage(string text, int width, int height, int fontSize)
        {
            SKBitmap bitmap = new SKBitmap(width, height);

            using (SKCanvas canvas = new SKCanvas(bitmap))
            using (SKPaint paint = new SKPaint())
            using (SKFont font = new SKFont())
            {
                canvas.Clear(SKColors.GhostWhite);

                // SkiaSharp 3.x: TextSize is now part of SKFont
                font.Size = fontSize;

                paint.Style = SKPaintStyle.Stroke;

                // Measure text using SKFont
                float textWidth = font.MeasureText(text, paint);

                // Center the text
                float x = width / 2f;
                float y = (height + fontSize) / 2f;

                SKRect rect = new SKRect(
                    x - 100,
                    y - 100,
                    x + 100,
                    y + 100);

                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(rect.Left, rect.Top),
                    new SKPoint(rect.Right, rect.Bottom),
                    new SKColor[]
                    {
                SKColors.Red,
                SKColors.Blue
                    },
                    new float[]
                    {
                0.05f,
                0.95f
                    },
                    SKShaderTileMode.Repeat);

                Random rand = new Random();

                float[] degrees = { -5, -12, 5, 12 };
                int index = rand.Next(degrees.Length);

                canvas.RotateDegrees(degrees[index]);

                // SkiaSharp 3.x DrawText API
                canvas.DrawText(
                    text,
                    x,
                    y + 3,
                    SKTextAlign.Center,
                    font,
                    paint);
            }

            return bitmap;
        }

        private string ConvertBitmapToBase64(SKBitmap bitmap)
        {
            using (SKImage image = SKImage.FromBitmap(bitmap))
            using (SKData data = image.Encode())
            {
                byte[] imageBytes = data.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
