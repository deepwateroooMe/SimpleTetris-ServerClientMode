using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySquare.Blocks;

namespace MySquare
{
    public class BlockFactory
    {
        private static Random rand = new Random();
        public static BaseBlock Create()
        {
            //return new CrossBlock() { IsActive = true };

            BaseBlock r = null;
            int type = rand.Next(11);
            switch (type)
            {
                case 0:
                    r = new LBlock();
                    break;
                case 1:
                    r = new SuperSingleBlock();
                    break;
                case 2:
                    r = new ZBlock();
                    break;
                case 3:
                    r = new OBlock();
                    break;
                case 4:
                    r = new LineBlock();
                    break;
                case 5:
                    r = new SingleBlock();
                    break;
                case 6:
                    r = new TBlock();
                    break;
                case 7:
                    r = new JBlock();
                    break;
                case 8:
                    r = new UBlock();
                    break;
                case 9:
                    r = new BigZBlock();
                    break;
                case 10:
                    r = new CrossBlock();
                    break;
            }
            r.IsActive = true;
            return r;
        }
    }
}
