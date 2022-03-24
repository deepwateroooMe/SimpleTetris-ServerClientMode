using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySquare.Squares;
using MySquare.Utilities;

namespace MySquare.Blocks
{
    public abstract class BaseBlock : ISquare
    {
        protected List<Square> squares = new List<Square>();

        public RotateTag rotateTag;

        public abstract void SetOutterPosition(int indexX, int indexY);//设置组合块外围矩形左上角的坐标
        public abstract BaseBlock InitObjForClone();
        public abstract int CenterIndexX { get; }
        public abstract int CenterIndexY { get; }
        protected abstract void InitSquares();

        public event Action ArriveBottomEvent;
        
        public bool Visible { set; get; }
        public bool CanSplash { get; set; }

        public IEnumerable<Square> Squares
        {
            get
            {
                return squares;
            }
        }
        public void TriggerArriveBottomEvent()
        {
            if (ArriveBottomEvent != null)
            {
                ArriveBottomEvent();
            }
        }

        public BaseBlock(int indexX, int indexY)
        {
            InitSquares();
            FillColor = ColorExt.RandomColor();

            //随机旋转
            Random rand = new Random();
            int cnt = rand.Next(4);
            for (int i = 0; i < cnt; i++)
                Rotate();

            SetOutterPositionAndCenter(indexX, indexY);
        }
        public int OutterBottom
        {
            get
            {
                var orderByTop = squares.OrderBy<Square, int>(s =>
                {
                    return s.IndexTop;
                });
                var last = orderByTop.Last<Square>();
                return last.IndexTop + 1;
            }
        }
        private void SetOutterPositionAndCenter(int x, int y)
        {
            SetOutterPosition(x, y);
            foreach (var s in squares)
            {
                s.SetCenter(CenterIndexX, CenterIndexY);
            }
        }

        public virtual bool CanDown()
        {
            bool tag = true;
            foreach (var s in squares)
            {
                if (!s.CanDown())
                {
                    tag = false;
                    break;
                }
            }
            return tag;
        }
        public virtual void MoveDown()
        {
            if (CanDown())
            {
                foreach (var s in squares)
                {
                    s.MoveDown();
                }
            }
            else
            {
                foreach (var s in squares)
                {
                    GameEngine.Instance.matrix[s.IndexTop, s.IndexLeft] = 1;
                }
                TriggerArriveBottomEvent();
            }
        }

        public virtual bool CanLeft()
        {
            bool tag = true;
            foreach (var s in squares)
            {
                if (!s.CanLeft())
                {
                    tag = false;
                    break;
                }
            }
            return tag;
        }
        public virtual void MoveLeft()
        {
            if (CanLeft())
            {
                foreach (var s in squares)
                {
                    s.MoveLeft();
                }
            }
        }
        public virtual bool CanRight()
        {
            bool tag = true;
            foreach (var s in squares)
            {
                if (!s.CanRight())
                {
                    tag = false;
                    break;
                }
            }
            return tag;
        }
        public virtual void MoveRight()
        {
            if (CanRight())
            {
                foreach (var s in squares)
                {
                    s.MoveRight();
                }
            }
        }

        public virtual void Draw(Graphics g)
        {
            foreach (var s in squares)
            {
                s.Draw(g);
            }
        }

        public Color FillColor
        {
            get
            {
                return squares[0].FillColor;
            }
            set
            {
                foreach (var s in squares)
                {
                    s.FillColor = value;
                }
            }
        }

        public object Clone()
        {
            BaseBlock b = InitObjForClone();
            b.FillColor = this.FillColor;
            return b;
        }

        public void Rotate()
        {
            if (!CanRotate())
                return;
            foreach (var s in squares)
            {
                s.Rotate();
            }

            int y = 0;
            int x = (int)rotateTag + 1;
            y = x > 3 ? 0 : x;
            rotateTag = (RotateTag)y;
        }

        public virtual bool CanRotate()
        {
            bool tag = true;
            foreach (var s in squares)
            {
                if (!s.CanRotate())
                {
                    tag = false;
                    break;
                }
            }
            return tag;
        }


        public bool CanUp()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public bool IsActive
        {
            get
            {
                return squares[0].IsActive;
            }
            set
            {
                foreach (var s in squares)
                {
                    s.IsActive = value;
                }
            }
        }
    }
}
