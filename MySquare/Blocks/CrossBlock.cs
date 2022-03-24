using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySquare.Blocks
{
    public class CrossBlock : BlockOfFive
    {
        public CrossBlock()
            : base((GameEngine.FIELD_W - 3) / 2, 0)
        { }
        public override void SetOutterPosition(int indexX, int indexY)
        {
            squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
            squares[0].PixelTop = (indexY + 1) * GameEngine.SQUARE_WIDTH;

            squares[1].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
            squares[1].PixelTop = squares[0].PixelTop;

            squares[2].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH * 2;
            squares[2].PixelTop = squares[0].PixelTop;

            squares[3].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
            squares[3].PixelTop = squares[0].PixelTop - GameEngine.SQUARE_WIDTH;

            squares[4].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
            squares[4].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;
        }

        public override MySquare.Blocks.BaseBlock InitObjForClone()
        {
            return new CrossBlock();
        }
        public override bool CanRotate()
        {
            return false;
        }
        public override int CenterIndexX
        {
            get
            {
                return squares[1].IndexLeft;
            }
        }

        public override int CenterIndexY
        {
            get
            {
                return squares[1].IndexTop;
            }
        }
    }
}
