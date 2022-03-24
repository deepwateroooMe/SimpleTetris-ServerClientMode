using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySquare.Squares;

namespace MySquare.Blocks
{
    public abstract class BlockOfFour : BaseBlock
    {
        public BlockOfFour(int indexX, int indexY)
            : base(indexX, indexY)
        { }
        protected override void InitSquares()
        {
            squares.Add(new Square());
            squares.Add(new Square());
            squares.Add(new Square());
            squares.Add(new Square());
        }
    }
}
