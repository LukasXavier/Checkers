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
        public Tuple<int, int> pos;
        public int King;

        public Piece(string color, Tuple<int,int> pos)
        {
            this.Color = color;
            this.pos = pos;
            this.King = 0;
        }

        public void SetPos(int x, int y)
        {
            pos = new(x, y);
        }

        public Tuple<int,int> Move(string direction)
        {
            if (Color.Equals("blue"))
            {
                if (direction.Equals("left"))
                {
                    pos = new(pos.Item1 - 1, pos.Item2 + 1);
                }
                else
                {
                    pos = new(pos.Item1 + 1, pos.Item2 + 1);
                }
            }
            else
            {
                if (direction.Equals("left"))
                {
                    pos = new(pos.Item1 - 1, pos.Item2 - 1);
                }
                else
                {
                    pos = new(pos.Item1 + 1, pos.Item2 - 1);
                }
            }
            if (King.Equals(1))
            {
                if (direction.Equals("left_down"))
                {
                    pos = new(pos.Item1 - 1, pos.Item2 + 1);
                }
                else if (direction.Equals("left_up"))
                {
                    pos = new(pos.Item1 - 1, pos.Item2 - 1);
                }
                else if (direction.Equals("right_down"))
                {
                    pos = new(pos.Item1 + 1, pos.Item2 + 1);
                }
                else
                {
                    pos = new(pos.Item1 + 1, pos.Item2 - 1);
                }
            }
            return pos;
        }

    }
}
