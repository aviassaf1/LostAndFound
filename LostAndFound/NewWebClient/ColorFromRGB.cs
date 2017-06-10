using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace NewWebClient
{
    public class ColorFromRGB
    {
        public static int getColor(int r, int g, int b)
        {
            List<Color> colors = new List<Color>() {Color.Pink, Color.Black, Color.Blue, Color.Red, Color.Green,
                Color.Yellow, Color.White ,Color.Purple, Color.Orange, Color.Gray, Color.Brown, Color.Gold,
                Color.Silver };
            Color target = Color.FromArgb(r, g, b);
            var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
            return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
        }

        private static int ColorDiff(Color c1, Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }

    }
}