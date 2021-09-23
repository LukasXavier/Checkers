using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Piece
    {
        public string Color;
        public int[] pos;
        public int King;

        public Piece(string color, int[] pos)
        {
            this.Color = color;
            this.pos = pos;
            this.King = 0;
        }

        public void SetPos(int x, int y)
        {
            pos[0] = x;
            pos[1] = y;
        }

        public int[] Move(string direction)
        {
            if (Color.Equals("blue"))
            {
                if (direction.Equals("left"))
                {
                    pos[0] -= 1;
                    pos[1] += 1;
                }
                else
                {
                    pos[0] += 1;
                    pos[1] += 1;
                }
            }
            else
            {
                if (direction.Equals("left"))
                {
                    pos[0] -= 1;
                    pos[1] -= 1;
                }
                else
                {
                    pos[0] += 1;
                    pos[1] -= 1;
                }
            }
            if (King.Equals(1))
            {
                if (direction.Equals("left_down"))
                {
                    pos[0] -= 1;
                    pos[1] += 1;
                }
                else if (direction.Equals("left_up"))
                {
                    pos[0] -= 1;
                    pos[1] -= 1;
                }
                else if (direction.Equals("right_down"))
                {
                    pos[0] += 1;
                    pos[1] += 1;
                }
                else
                {
                    pos[0] += 1;
                    pos[1] -= 1;
                }
            }
            return new int[2] { pos[0], pos[1] };
        }

    }
}
