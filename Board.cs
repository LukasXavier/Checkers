using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Board
    {
        public Dictionary<Tuple<int,int>, Piece> board;

        public Board()
        {
            board = new();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Tuple<int,int> pos = new(x, y);
                    board.Add(pos, null);
                }
            }

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 8; x += 2)
                {
                    Tuple<int, int> pos = new(x, y);
                    if (y == 0 || y == 2)
                    {
                        pos = new(x+1, y); 
                    }
                    board[pos] = new Piece("blue", pos);
                }
            }

            for (int y = 5; y < 8; y++)
            {
                for (int x = 0; x < 8; x += 2)
                {
                    Tuple<int, int> pos = new(x, y);
                    if (y == 6)
                    {
                        pos = new(x + 1, y); 
                    }
                    board[pos] = new Piece("red", pos);
                }
            }

        }
    }
}
