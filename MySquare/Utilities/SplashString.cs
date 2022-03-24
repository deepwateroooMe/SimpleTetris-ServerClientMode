using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MySquare.Utilities
{
    public class SplashString : SplashBase<string>
    {
        public SplashString(PictureBox canv,string content, Size initSize)
            : base(canv, content, initSize)
        {

        }
        protected override void Draw(Graphics g)
        {
            if (Content == null)
                return;
            var font = new Font("宋体", objSize.Width, FontStyle.Bold, GraphicsUnit.Pixel);
            var size = g.MeasureString(Content, font);
            objSize = new Size((int)size.Width, (int)size.Height);
            g.DrawString(Content, font, Brushes.Red, Loc);
        }
    }
}
