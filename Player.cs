using System;
using System.Collections.Generic;

namespace Checkers
{
    class Player
    {
        // captured stores if the previous move was a capture move
        // lastMove stores the position that the player moved last
        public bool captured;
        private Tuple<int, int> lastMove;

        public Player()
        {
            captured = false;
            lastMove = null;
        }

        /// <summary>
        /// determines whether the Player can capture or not
        /// </summary>
        /// <param name="checkerBoard">the board and its methods for making a move</param>
        /// <returns>true if the Player can capture and false otherwise</returns>
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

        /// <summary>
        /// makes a random move for the CPU player
        /// </summary>
        /// <param name="checkerBoard">the board and its methods for making a move</param>
        /// <param name="playerTurn">a bool that signifies if the CPU should be 
        /// making a move or not</param>
        public void CPUMove(Board checkerBoard, bool playerTurn)
        {
            // terminates if the cpu player should not be making a turn
            if (playerTurn)
            {
                return;
            }
            Random rnd = new Random();
            Tuple<int, int>[] randomMove = new Tuple<int, int>[2];
            List<Tuple<int, int>[]> CPUMoves = new();
            // finds all blue pieces and adds all of their valid moves to CPUMoves
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
                        Tuple<int, int>[] CPUMove = new Tuple<int, int>[2];
                        CPUMove[0] = key;
                        CPUMove[1] = move;
                        CPUMoves.Add(CPUMove);
                    }
                }
            }

            // handles repeated captures for the cpu player
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
            // handles moves that are not captures
            else
            {
                randomMove = CPUMoves[rnd.Next(0, CPUMoves.Count)];
                checkerBoard.Move(randomMove[0], randomMove[1], false);
            }
            // stores the state of the piece after a capture
            if (checkerBoard.TookPiece(randomMove[0], randomMove[1]))
            {
                captured = true;
                lastMove = randomMove[1];
            }
            // resets the state of the piece if a capture was not made
            else
            {
                captured = false;
                lastMove = null;
            }
        }

        /// <summary>
        /// handles moves that the human player is making
        /// </summary>
        /// <param name="checkerBoard">the board and its methods for making a move</param>
        /// <param name="prevPos">the position a move is being made from</param>
        /// <param name="pos">the position the player is trying to move to</param>
        /// <param name="playerTurn">a bool that signifies if the human player should be 
        /// making a move or not</param>
        public void PlayerMove(Board checkerBoard, Tuple<int, int> prevPos, Tuple<int, int> pos, bool playerTurn)
        {
            // terminates if the human player should not be making a turn
            if (!playerTurn)
            {
                captured = false;
                lastMove = null;
                return;
            }
            // handles when the last move was a capture
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
            // handles when the last move was not a capture
            else
            {
                checkerBoard.Move(prevPos, pos, false);
            }
            // stores the state of the piece after a capture
            if (checkerBoard.TookPiece(prevPos, pos))
            {
                captured = true;
                lastMove = pos;
            }
            // resets the state of the piece if a capture was not made
            else
            {
                captured = false;
                lastMove = null;
            }
        }
    }
}
