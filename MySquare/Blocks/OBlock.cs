using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare.Blocks
{
    public class OBlock : BlockOfFour
    {
        public OBlock()
            : base((GameEngine.FIELD_W - 2) / 2, 0)
        {
        }
        public override void SetOutterPosition(int indexX, int indexY)
        {
            squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
            squares[0].PixelTop = indexY * GameEngine.SQUARE_WIDTH;

            squares[1].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
            squares[1].PixelTop = squares[0].PixelTop;

            squares[2].PixelLeft = squares[0].PixelLeft;
            squares[2].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;

            squares[3].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
            squares[3].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;
        }
        public override bool CanRotate()
        {
            return false;
        }
        public override BaseBlock InitObjForClone()
        {
            return new OBlock();
        }
        public override int CenterIndexX
        {
            get
            {
                return squares[2].IndexLeft;
            }
        }

        public override int CenterIndexY
        {
            get
            {
                return squares[2].IndexTop;
            }
        }
    }
}