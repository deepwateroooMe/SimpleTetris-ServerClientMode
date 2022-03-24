using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare
{
    public static class ColorExt
    {
        private static Random rand = new Random();
        public static Color RandomColor()
        {
            Color c = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            return c;
        }
        public static Color GetOpposite(this Color c)
        {
            return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }
    }
}
