using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySquare.Squares;

namespace MySquare.Blocks
{
    public class RandomBlock:BaseBlock
    {
        private Random random = new Random();
        private int squareCount = 0;

        public RandomBlock()
            : base((GameEngine.FIELD_W - 3) / 2, 0)
        {
            
        }
        public override void SetOutterPosition(int indexX, int indexY)
        {
            Random rand = new Random();

            squares[0].PixelLeft = indexX * GameEngine.SQUARE_WIDTH;
            squares[0].PixelTop = indexY * GameEngine.SQUARE_WIDTH;

            for (int i = 1; i < squareCount; i++)
            {
                squares[i].PixelLeft = squares[0].PixelLeft + rand.Next(6) * GameEngine.SQUARE_WIDTH;
                squares[i].PixelTop = squares[0].PixelTop + rand.Next(6) * GameEngine.SQUARE_WIDTH;
            }
        }

        public override BaseBlock InitObjForClone()
        {
            return new RandomBlock();
        }

        public override int CenterIndexX
        {
            get { return squares[squareCount / 2].IndexLeft; }
        }

        public override int CenterIndexY
        {
            get { return squares[squareCount / 2].IndexTop; }
        }

        protected override void InitSquares()
        {
            squareCount = random.Next(6, 10);
            for (int i = 0; i < squareCount; i++)
            {
                squares.Add(new Square());
            }
        }
    }
}
