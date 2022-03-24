using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare
{
    public interface ISquare : ICloneable
    {
        bool Visible { set; get; }
        bool CanSplash { set; get; }
        Color FillColor { set; get; }
        int CenterIndexX { get; }
        int CenterIndexY { get; }
        bool IsActive { get; set; }
        bool CanUp();
        void MoveUp();
        bool CanDown();
        void MoveDown();
        bool CanLeft();
        void MoveLeft();
        bool CanRight();
        void MoveRight();
        bool CanRotate();
        void Rotate();
        void Draw(Graphics g);
    }
}
