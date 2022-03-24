using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare.DataGram
{
    [Serializable]
    public class SquareData
    {
        public Color fillColor;
        public int pixelLeft;
        public int pixelTop;
        public bool visible;
        public bool canSplash;
    }
}
