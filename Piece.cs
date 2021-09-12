using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Piece
    {
        private string color;
        private int[] pos;

        public Piece(string color)
        {
            this.color = color;
            this.pos = new int[2];
        }

        public void setPos(int x, int y)
        {
            pos[0] = x;
            pos[1] = y;
        }
    }
}
