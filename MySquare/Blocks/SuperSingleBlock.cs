using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySquare.Squares;

namespace MySquare.Blocks
{
    public class SuperSingleBlock : SingleBlock
    {
        protected override void InitSquares()
        {
            squares.Add(new SuperSquare());
        }
        public override BaseBlock InitObjForClone()
        {
            return new SuperSingleBlock();
        }
    }
}
