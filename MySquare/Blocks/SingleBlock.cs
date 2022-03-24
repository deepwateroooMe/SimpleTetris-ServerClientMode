using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySquare.Squares;

namespace MySquare.Blocks
{
    public class SingleBlock : BaseBlock
    {
        public SingleBlock()
            : base((GameEngine.FIELD_W - 1) / 2, 0)
        {
        }
        public override int CenterIndexX
        {
            get { return squares[0].IndexLeft; }
        }

        public override int CenterIndexY
        {
            get { return squares[0].IndexTop; }
        }

        public override void SetOutterPosition(int indexX, int indexY)
        {
            squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
            squares[0].PixelTop = indexY * GameEngine.SQUARE_WIDTH;
        }

        public override BaseBlock InitObjForClone()
        {
            return new SingleBlock();
        }
        public override bool CanRotate()
        {
            return false;
        }
        public override void Draw(Graphics g)
        {
            squares[0].Draw(g);
        }
        public override void MoveLeft()
        {
            if (CanLeft())
                squares[0].MoveLeft();
        }
        public override void MoveRight()
        {
            if (CanRight())
                squares[0].MoveRight();
        }
        public override bool CanDown()
        {
            return squares[0].CanDown();
        }
        public override bool CanLeft()
        {
            return squares[0].CanLeft();
        }
        public override bool CanRight()
        {
            return squares[0].CanRight();
        }
        public override void MoveDown()
        {
            if (CanDown())
            {
                squares[0].MoveDown();
            }
            else
            {
                if (squares != null)
                {
                    squares[0].CanSplash = false;
                }
                GameEngine.Instance.matrix[squares[0].IndexTop, squares[0].IndexLeft] = 1;
                TriggerArriveBottomEvent();
            }
        }

        protected override void InitSquares()
        {
            squares.Add(new Square());
        }
    }
}
