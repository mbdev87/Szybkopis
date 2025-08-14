using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Szybkopis
{
    public static class PolishFlagIcons
    {
        public static Icon CreateOnIcon()
        {
            return CreateIcon(true);
        }

        public static Icon CreateOffIcon()
        {
            return CreateIcon(false);
        }

        private static Icon CreateIcon(bool enabled)
        {
            var bitmap = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
var whiteColor = enabled ? Color.White : Color.FromArgb(200, 200, 200);
                var redColor = enabled ? Color.FromArgb(220, 20, 60) : Color.FromArgb(100, 100, 100);
                
                using (var whiteBrush = new SolidBrush(whiteColor))
                using (var redBrush = new SolidBrush(redColor))
                {
                    g.FillRectangle(whiteBrush, 0, 0, 16, 8);
                    g.FillRectangle(redBrush, 0, 8, 16, 8);
                }
                
var modeText = ";PL";
                var textColor = Color.Black;
                using (var font = new Font("Arial", 4.5f, FontStyle.Bold))
                using (var textBrush = new SolidBrush(textColor))
                {
                    var textSize = g.MeasureString(modeText, font);
                    var x = (16 - textSize.Width) / 2;
                    var y = (16 - textSize.Height) / 2;
                    
var bgColor = Color.FromArgb(220, Color.White);
                    using (var bgBrush = new SolidBrush(bgColor))
                    {
                        g.FillRectangle(bgBrush, x - 1, y - 1, textSize.Width + 2, textSize.Height + 2);
                    }
                    
                    g.DrawString(modeText, font, textBrush, x, y);
                }
                
var borderColor = enabled ? Color.DarkBlue : Color.DarkGray;
                using (var borderPen = new Pen(borderColor, 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, 15, 15);
                }
            }
            
            return Icon.FromHandle(bitmap.GetHicon());
        }
    }
}