using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Piece
    {
        public string color;
        public int[] pos;

        public Piece(string color, int[] pos)
        {
            this.color = color;
            this.pos = pos;
        }

        public void setPos(int x, int y)
        {
            pos[0] = x;
            pos[1] = y;
        }
    }
}
