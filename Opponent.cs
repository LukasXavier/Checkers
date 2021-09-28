using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Opponent
    {
        public bool captured;
        private Tuple<int, int> lastMove;

        public Opponent()
        {
            captured = false;
            lastMove = null;
        }

        public void OpponentMove(Board checkerBoard, bool playerTurn)
        {
            if (playerTurn)
            {
                return;
            }
            Random rnd = new Random();
            Tuple<int, int>[] randomMove = new Tuple<int, int>[2];
            Tuple<int, int>[] opponentMove = new Tuple<int, int>[2];
            List<Tuple<int, int>[]> opponentMoves = new();
            foreach (Tuple<int, int> key in checkerBoard.board.Keys)
            {
                if (checkerBoard.board[key].Color == null)
                {
                    continue;
                }
                if (checkerBoard.board[key].Color.Contains("blue"))
                {
                    List<Tuple<int, int>> validMoves = checkerBoard.PossibleMoves(key);

                    foreach (Tuple<int, int> move in validMoves)
                    {
                        opponentMove = new Tuple<int, int>[2];
                        opponentMove[0] = key;
                        opponentMove[1] = move;
                        opponentMoves.Add(opponentMove);
                    }
                }
            }

            if (lastMove != null)
            {
                List<Tuple<int, int>> captureMoves = checkerBoard.PossibleMoves(lastMove);
                foreach (Tuple<int, int> move in captureMoves)
                {
                    if (!checkerBoard.TookPiece(lastMove, move))
                    {
                        captureMoves.Remove(move);
                    }
                }

                if (captured && captureMoves.Count > 0)
                {
                    randomMove[0] = lastMove;
                    randomMove[1] = captureMoves[0];
                }
                else
                {
                    randomMove = opponentMoves[rnd.Next(0, opponentMoves.Count)];
                }
            } 
            else
            {
                randomMove = opponentMoves[rnd.Next(0, opponentMoves.Count)];
            }
            checkerBoard.Move(randomMove[0], randomMove[1], false);
            if (checkerBoard.TookPiece(randomMove[0], randomMove[1]))
            {
                captured = true;
                lastMove = randomMove[1];
            }
            else
            {
                captured = false;
                lastMove = null;
            }
        }
    }
}
