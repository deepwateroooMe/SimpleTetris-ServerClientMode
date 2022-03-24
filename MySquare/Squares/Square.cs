using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MySquare.Squares
{
    public class Square:ISquare
    {
        public Color FillColor { set; get; }
        public int PixelLeft { set; get; }
        public int PixelTop { set; get; }
        public bool IsActive { set; get; }

        public int IndexLeft 
        {
            get
            {
                if (PixelLeft < 0)
                    return 0;
                return PixelLeft / GameEngine.SQUARE_WIDTH;
            }
        }
        public int IndexTop
        {
            get
            {
                if (PixelTop < 0)
                    return 0;
                return PixelTop / GameEngine.SQUARE_WIDTH;
            }
        }
        public virtual bool CanDown()
        {
            if (GameEngine.Instance.IsSelfGameOver)
                return false;
            if (PixelTop + GameEngine.SQUARE_WIDTH >= GameEngine.Instance.FieldIndexHeight)
                return false;
            if (GameEngine.Instance.matrix[IndexTop + 1, IndexLeft] == 1)
                return false;
            return true;
        }
        public void MoveDown()
        {
            if (CanDown())
            {
                var same = GameEngine.Instance[IndexLeft, IndexTop];
                if (same.Count == 1)//[IndexLeft, IndexTop]位置处只有当前方块本身时，矩阵置0；如果有两个，则说明当前位置还有别的方块，不能置0。
                    GameEngine.Instance.matrix[IndexTop, IndexLeft] = 0;

                PixelTop += GameEngine.SQUARE_WIDTH;
                _CenterIndexY++;
            }
            else
            {
                GameEngine.Instance.matrix[IndexTop, IndexLeft] = 1;
            }
        }

        public bool CanLeft() 
        {
            if (GameEngine.Instance.IsSelfGameOver)
                return false;
            if (PixelLeft <= 0)
                return false;
            if (GameEngine.Instance.matrix[IndexTop, IndexLeft - 1] == 1)
                return false;
            return true; 
        }
        public void MoveLeft()
        {
            if (CanLeft())
            {
                PixelLeft -= GameEngine.SQUARE_WIDTH;
                _CenterIndexX--;
            }
        }

        public bool CanRight()
        {
            if (GameEngine.Instance.IsSelfGameOver)
                return false;
            if (PixelLeft + GameEngine.SQUARE_WIDTH >= GameEngine.Instance.FieldIndexWidth)
                return false;
            if (GameEngine.Instance.matrix[IndexTop, IndexLeft + 1] == 1)
                return false;
            return true; 
        }
        public void MoveRight()
        {
            if (CanRight())
            {
                PixelLeft += GameEngine.SQUARE_WIDTH;
                _CenterIndexX++;
            }
        }

        public virtual void Draw(Graphics g)
        {
            if (!Visible)
                return;
            Brush b = new HatchBrush(HatchStyle.DiagonalBrick, FillColor.GetOpposite(), FillColor);
            g.FillRectangle(b, PixelLeft, PixelTop, GameEngine.SQUARE_WIDTH, GameEngine.SQUARE_WIDTH);
            g.DrawRectangle(Pens.Yellow, PixelLeft, PixelTop, GameEngine.SQUARE_WIDTH, GameEngine.SQUARE_WIDTH);
            //if (IsActive)
            //    g.DrawString("A", new Font("宋体", 12), Brushes.Red, PixelLeft, PixelTop);
            //g.DrawString(GameEngine.Instance.matrix[IndexTop, IndexLeft].ToString(), new Font("宋体", 12), Brushes.Red, PixelLeft, PixelTop);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Rotate()//顺时针旋转90度
        {
            if (!CanRotate())
                return;
            int tempLeft = PixelLeft;
            int tempTop = PixelTop;
            PixelLeft = CenterIndexX * GameEngine.SQUARE_WIDTH + CenterIndexY * GameEngine.SQUARE_WIDTH - tempTop;
            PixelTop = CenterIndexY * GameEngine.SQUARE_WIDTH + tempLeft - CenterIndexX * GameEngine.SQUARE_WIDTH;
        }

        public bool CanRotate()
        {
            if (GameEngine.Instance.IsSelfGameOver)
                return false;
            int targetIndexLeft = CenterIndexX  + CenterIndexY - IndexTop;
            int targetIndexTop = CenterIndexY + IndexLeft - CenterIndexX;
            if (targetIndexLeft < 0 || targetIndexLeft >= GameEngine.FIELD_W || targetIndexTop < 0 || targetIndexTop >= GameEngine.FIELD_H)
                return false;
            return GameEngine.Instance.matrix[targetIndexTop, targetIndexLeft] == 0;
        }
        public void SetCenter(int x, int y)
        {
            _CenterIndexX = x;
            _CenterIndexY = y;
        }
        private int _CenterIndexX;
        public int CenterIndexX
        {
            get { return _CenterIndexX; }
        }
        private int _CenterIndexY;
        public int CenterIndexY
        {
            get { return _CenterIndexY; }
        }
        private bool _Visible = true;
        public bool Visible 
        {
            set
            {
                _Visible = value;
            }
            get
            {
                return _Visible;
            }
        }
        private bool _CanSplash = false;
        public bool CanSplash
        {
            get
            {
                return _CanSplash;
            }
            set
            {
                _CanSplash = value;
                if (!_CanSplash)
                    Visible = true;
            }
        }


        public bool CanUp()
        {
            return PixelTop - GameEngine.SQUARE_WIDTH < 0 ? false : true;
        }

        public void MoveUp()
        {
            if (CanUp())
            {
                GameEngine.Instance.matrix[IndexTop, IndexLeft] = 0;
                PixelTop -= GameEngine.SQUARE_WIDTH;
                GameEngine.Instance.matrix[IndexTop, IndexLeft] = 1;
                _CenterIndexY--;
            }
        }
    }
}
