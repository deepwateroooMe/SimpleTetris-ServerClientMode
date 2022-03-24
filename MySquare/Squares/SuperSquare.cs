using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare.Squares
{
    public class SuperSquare : Square
    {
        public SuperSquare()
            : base()
        {
            CanSplash = true;
        }
        public override bool CanDown()
        {
            if (PixelTop + GameEngine.SQUARE_WIDTH >= GameEngine.Instance.FieldIndexHeight)
                return false;
            bool tag = true;
            for (int i = IndexTop + 1; i < GameEngine.FIELD_H; i++)
            {
                if (GameEngine.Instance.matrix[i, IndexLeft] == 0)
                {
                    tag = false;
                    break;
                }
            }
            return !tag;
        }
        private int spashTag = 0;
        public override void Draw(System.Drawing.Graphics g)
        {
            if (Visible)
            {
                base.Draw(g);
                int delta = 2;
                Pen pen = new Pen(Color.Red, delta);
                g.DrawRectangle(pen, PixelLeft + delta, PixelTop + delta, GameEngine.SQUARE_WIDTH - delta * 2, GameEngine.SQUARE_WIDTH - delta * 2);
            }

            //闪烁效果
            if (CanSplash && spashTag % 20 == 0)
                Visible = !Visible;
            if (spashTag + 1 < int.MaxValue)
                spashTag++;
            else
                spashTag = 0;
        }
    }
}
