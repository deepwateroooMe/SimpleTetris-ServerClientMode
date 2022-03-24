using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MySquare.Utilities
{
    public class SplashImg : SplashBase<Image>
    {
        public SplashImg(PictureBox canv, Image content, Size initSize)
            : base(canv, content, initSize)
        { }
        protected override void Draw(Graphics g)
        {
            if (Content == null)
                return;
            g.DrawImage(Content, new Rectangle(Loc, objSize));
        }
    }
}
