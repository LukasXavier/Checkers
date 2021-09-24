using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    board[pos] = new Piece("blue");
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
                    board[pos] = new Piece("red");
                }
            }

        }

        public int gameOver()
        {
            int blue = 0;
            int red = 0;
            foreach (Tuple<int, int> key in board.Keys)
            {
                if (board[key] != null)
                {
                    if (board[key].Color.Contains("red")) {
                        red++;
                    }
                    else if (board[key].Color.Contains("blue")) {
                        blue++;
                    }
                }
            }
            if (blue > 0 && red > 0)
            {
                return 0;
            }
            else if (blue > 0 && red <= 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private bool pieceExists(Tuple<int, int> prevPos, Tuple<int, int> pos)
        {
            Tuple<int, int> between;
            int offset;
            if (prevPos.Item1 - pos.Item1 > 0)
            {
                offset = (pos.Item2 - prevPos.Item2) / 2;
                between = new(prevPos.Item1 - 1, prevPos.Item2 + offset);
            }
            else
            {
                offset = (pos.Item2 - prevPos.Item2) / 2;
                between = new(prevPos.Item1 + 1, prevPos.Item2 + offset);
            }
            if (board[between] == null || board[between].Color.Equals(board[prevPos].Color) || board[pos] != null)
            {
                return false;
            }
            board[between] = null;
            return true;
        }

        public void Move(Tuple<int, int> prevPos, Tuple<int, int> pos)
        {
            Piece cur = board[prevPos];
            if (cur == null)
            {
                return;
            }
            if (cur.Color.Contains("king"))
            {
                if (Math.Abs(pos.Item1 - prevPos.Item1) == 1)
                {
                    if (Math.Abs(pos.Item2 - prevPos.Item2) == 1)
                    {
                        if (board[pos] == null)
                        {
                            board[pos] = cur;
                            board[prevPos] = null;
                        }
                    }
                }
                if (Math.Abs(pos.Item1 - prevPos.Item1) == 2)
                {
                    if (Math.Abs(pos.Item2 - prevPos.Item2) == 2 && pieceExists(prevPos, pos))
                    {
                        if (board[pos] == null)
                        {
                            board[pos] = cur;
                            board[prevPos] = null;
                        }
                    }
                }
            }
            else
            {
                if (cur.Color.Equals("blue"))
                {
                    if (Math.Abs(pos.Item1 - prevPos.Item1) == 1)
                    {
                        if (pos.Item2 - prevPos.Item2 == 1)
                        {
                            if (board[pos] == null)
                            {
                                board[pos] = cur;
                                board[prevPos] = null;
                            }
                        }
                    }
                    else if (Math.Abs(pos.Item1 - prevPos.Item1) == 2)
                    {
                        if (pos.Item2 - prevPos.Item2 == 2 && pieceExists(prevPos, pos))
                        {
                            if (board[pos] == null)
                            {
                                board[pos] = cur;
                                board[prevPos] = null;
                            }
                        }
                    }
                }
                else
                {
                    if (Math.Abs(pos.Item1 - prevPos.Item1) == 1)
                    {
                        if (pos.Item2 - prevPos.Item2 == -1)
                        {
                            if (board[pos] == null)
                            {
                                board[pos] = cur;
                                board[prevPos] = null;
                            }
                        }
                    }
                    else if (Math.Abs(pos.Item1 - prevPos.Item1) == 2)
                    {
                        if (pos.Item2 - prevPos.Item2 == -2 && pieceExists(prevPos, pos))
                        {
                            if (board[pos] == null)
                            {
                                board[pos] = cur;
                                board[prevPos] = null;
                            }
                        }
                    }
                }
            }

            if (board[pos] == null)
            {
                return;
            }

            if (board[pos].Color.Equals("red") && pos.Item2 == 0)
            {
                board[pos].promote();
            }
            if (board[pos].Color.Equals("blue") && pos.Item2 == 7)
            {
                board[pos].promote();
            }

        }

    }
}
