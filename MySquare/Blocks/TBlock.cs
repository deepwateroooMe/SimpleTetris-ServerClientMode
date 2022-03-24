﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MySquare.Blocks
{
    public class TBlock : BlockOfFour
    {
        public override void SetOutterPosition(int indexX, int indexY)
        {
            switch (rotateTag)
            {
                case RotateTag.UP:
                    squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
                    squares[0].PixelTop = (indexY + 1) * GameEngine.SQUARE_WIDTH;

                    squares[1].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
                    squares[1].PixelTop = squares[0].PixelTop;

                    squares[2].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH * 2;
                    squares[2].PixelTop = squares[0].PixelTop;

                    squares[3].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
                    squares[3].PixelTop = squares[0].PixelTop - GameEngine.SQUARE_WIDTH;
                    break;
                case RotateTag.RIGHT:
                    squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
                    squares[0].PixelTop = indexY * GameEngine.SQUARE_WIDTH;

                    squares[1].PixelLeft = squares[0].PixelLeft;
                    squares[1].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;

                    squares[2].PixelLeft = squares[0].PixelLeft ;
                    squares[2].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH * 2;

                    squares[3].PixelLeft = squares[0].PixelLeft + GameEngine.SQUARE_WIDTH;
                    squares[3].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;
                    break;
                case RotateTag.DOWN:
                    squares[0].PixelLeft = (indexX + 2) * GameEngine.SQUARE_WIDTH;
                    squares[0].PixelTop = indexY * GameEngine.SQUARE_WIDTH;

                    squares[1].PixelLeft = squares[0].PixelLeft - GameEngine.SQUARE_WIDTH;
                    squares[1].PixelTop = squares[0].PixelTop;

                    squares[2].PixelLeft = squares[0].PixelLeft - GameEngine.SQUARE_WIDTH * 2;
                    squares[2].PixelTop = squares[0].PixelTop;

                    squares[3].PixelLeft = squares[0].PixelLeft - GameEngine.SQUARE_WIDTH;
                    squares[3].PixelTop = squares[0].PixelTop + GameEngine.SQUARE_WIDTH;
                    break;
                case RotateTag.LEFT:
                    squares[0].PixelLeft = (indexX + 1) * GameEngine.SQUARE_WIDTH;
                    squares[0].PixelTop = (indexY + 2) * GameEngine.SQUARE_WIDTH;

                    squares[1].PixelLeft = squares[0].PixelLeft;
                    squares[1].PixelTop = squares[0].PixelTop - GameEngine.SQUARE_WIDTH;

                    squares[2].PixelLeft = squares[0].PixelLeft;
                    squares[2].PixelTop = squares[0].PixelTop - GameEngine.SQUARE_WIDTH * 2;

                    squares[3].PixelLeft = squares[0].PixelLeft - GameEngine.SQUARE_WIDTH;
                    squares[3].PixelTop = squares[0].PixelTop - GameEngine.SQUARE_WIDTH;
                    break;
            }
        }
        public TBlock()
            : base((GameEngine.FIELD_W - 3) / 2, 0)
        {
        }

        public override BaseBlock InitObjForClone()
        {
            return new TBlock();
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
