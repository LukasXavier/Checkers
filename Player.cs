using System;
using System.Collections.Generic;

namespace Checkers
{
    class Player
    {
        public bool captured;
        private Tuple<int, int> lastMove;

        public Player()
        {
            captured = false;
            lastMove = null;
        }

        public bool CanCapture(Board checkerBoard)
        {
            if (lastMove == null)
            {
                captured = false;
                return false;
            }
            List<Tuple<int, int>> afterCaptureMoves = checkerBoard.PossibleMoves(lastMove);
            foreach (Tuple<int, int> move in afterCaptureMoves)
            {
                if (checkerBoard.TookPiece(lastMove, move))
                {
                    return true;
                }
            }
            captured = false;
            lastMove = null;
            return false;
        }

        public void CPUMove(Board checkerBoard, bool playerTurn)
        {
            if (playerTurn)
            {
                return;
            }
            Random rnd = new Random();
            Tuple<int, int>[] randomMove = new Tuple<int, int>[2];
            Tuple<int, int>[] playerMove = new Tuple<int, int>[2];
            List<Tuple<int, int>[]> playerMoves = new();
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
                        playerMove = new Tuple<int, int>[2];
                        playerMove[0] = key;
                        playerMove[1] = move;
                        playerMoves.Add(playerMove);
                    }
                }
            }

            if (lastMove != null)
            {
                List<Tuple<int, int>> afterCaptureMoves = checkerBoard.PossibleMoves(lastMove);
                List<Tuple<int, int>> captureMoves = new();
                foreach (Tuple<int, int> move in afterCaptureMoves)
                {
                    if (checkerBoard.TookPiece(lastMove, move))
                    {
                        captureMoves.Add(move);
                    }
                }

                if (captured && captureMoves.Count > 0)
                {
                    randomMove[0] = lastMove;
                    randomMove[1] = captureMoves[0];
                    checkerBoard.Move(randomMove[0], randomMove[1], false);
                }
            } 
            else
            {
                randomMove = playerMoves[rnd.Next(0, playerMoves.Count)];
                checkerBoard.Move(randomMove[0], randomMove[1], false);
            }
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

        public void PlayerMove(Board checkerBoard, Tuple<int, int> prevPos, Tuple<int, int> pos, bool playerTurn)
        {
            if (!playerTurn)
            {
                captured = false;
                lastMove = null;
                return;
            }
            if (lastMove != null)
            {
                List<Tuple<int, int>> afterCaptureMoves = checkerBoard.PossibleMoves(lastMove);
                List<Tuple<int, int>> captureMoves = new();
                foreach (Tuple<int, int> move in afterCaptureMoves)
                {
                    if (checkerBoard.TookPiece(lastMove, move))
                    {
                        captureMoves.Add(move);
                    }
                }

                if (captured && captureMoves.Count > 0)
                {
                    if (checkerBoard.TookPiece(prevPos, pos))
                    {
                        checkerBoard.Move(prevPos, pos, false);
                    } 
                }
            }
            else
            {
                checkerBoard.Move(prevPos, pos, false);
            }
            if (checkerBoard.TookPiece(prevPos, pos))
            {
                captured = true;
                lastMove = pos;
            }
            else
            {
                captured = false;
                lastMove = null;
            }
        }
    }
}
