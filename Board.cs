using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Checkers
{

    class Board
    {
        /**
         *  Represents a piece on the Gameboard, can have a color value of "red",
         *  "blue" or null. All spaces on the Gameboard have a Piece in them but 
         *  spaces with no displayed piece have a color value of null.
         */
        public struct Piece
        {
            public String Color;
            public Piece(string color)
            {
                Color = color;
            }
        }

        // the data structure for storing the pieces
        public Dictionary<Tuple<int,int>, Piece> Gameboard;

        /// <summary>
        /// this creates the Gameboard and populates it with red/blue pieces
        /// </summary>
        public Board()
        {
            // C# allows us to not need to specify type when constructing a new instance if it is already defined somewhere else
            Gameboard = new();

            // loops through all the locations on the Gameboard and populates them with pieces
            for (int x = 0; x < 8; x += 2)
            {
                for (int y = 0; y < 8; y++)
                {
                    // the keys need to be established before we set the pieces down
                    Gameboard.Add(new(x, y), new(null));
                    Gameboard.Add(new(x + 1, y), new(null));
                    // for the values between [0,2] are blue pieces but the middle row is offset from the others

                    if (y % 2 == 0)
                    {
                        if (y < 3)
                        {
                            Gameboard[new(x + 1, y)] = new("blue");
                        }
                        else if (y > 4)
                        {
                            Gameboard[new(x + 1, y)] = new("red");
                        }
                    }
                    else
                    {
                        if (y < 3)
                        {
                            Gameboard[new(x, y)] = new("blue");
                        }
                        else if (y > 4)
                        {
                            Gameboard[new(x, y)] = new("red");
                        }
                    }
                    
                }
            }

        }

        /// <summary>
        /// This returns the current status of the game as an int
        /// 0 = game in progress
        /// 1 = blue won
        /// 2 = red won
        /// 3 = draw
        /// </summary>
        /// <returns> 0 for game not over, 1 for blue wins, 2 for red wins, 3 for draw</returns>
        public int GameOver()
        {
            int blue = 0;
            int red = 0;
            int redMoves = 0;
            int blueMoves = 0;
            // counts sides pieces
            foreach (Tuple<int, int> key in Gameboard.Keys)
            {
                if (Gameboard[key].Color != null)
                {
                    // since a piece and either be red, blue, kingred, kingblue, we just check if it contains red or blue
                    if (Gameboard[key].Color.Contains("red")) {
                        red++;
                        redMoves += this.PossibleMoves(key).Count;
                    }
                    else if (Gameboard[key].Color.Contains("blue")) {
                        blue++;
                        blueMoves += this.PossibleMoves(key).Count;
                    }
                }
            }
            if (redMoves == 0 && blueMoves == 0)
            {
                return 3;
            }
            else if ((blue > 0 && red == 0) || redMoves == 0)
            {
                return 1;
            }
            else if ((red > 0 && blue == 0) || blueMoves == 0) 
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// This is the logic for capturing a piece
        /// </summary>
        /// <param name="prevPos">(int, int) of the pieces current position</param>
        /// <param name="pos">(int, int) of the pieces new position</param>
        /// <param name="check">true if the user is doing a test whether a move is 
        /// valid or not and false if the user is making a move</param>
        /// <returns>true if a capture was made and false otherwise</returns>
        private bool Capture(Tuple<int, int> prevPos, Tuple<int, int> pos, bool check)
        {
            Tuple<int, int> between;
            // this offset is the change in y from the old to new position, the two finds the piece it jumps over
            int offset = (pos.Item2 - prevPos.Item2) / 2;
            // checks the piece is moving left, the else covers moving right
            if (prevPos.Item1 - pos.Item1 > 0)
            {
                between = new(prevPos.Item1 - 1, prevPos.Item2 + offset);
            }
            else
            {   
                between = new(prevPos.Item1 + 1, prevPos.Item2 + offset);
            }
            // checks that you're trying to capture the other player's piece
            if (Gameboard[between].Color == null || Gameboard[pos].Color != null)
            {
                return false;
            }
            if (Gameboard[prevPos].Color.Contains("red") == Gameboard[between].Color.Contains("blue"))
            {
                // capturing a piece is the same as just deleting it
                if (!check)
                {
                    Gameboard[pos] = Gameboard[prevPos];
                    Gameboard[between] = new Piece(null);
                    Gameboard[prevPos] = new Piece(null);
                    // if a blue piece makes it all the way to the 'top' it is promoted to a king
                    if (pos.Item2 == 7 && Gameboard[pos].Color.Equals("blue"))
                    {
                        Gameboard[pos] = new Piece("king" + Gameboard[pos].Color);
                    }
                    // if a red piece makes it all the way to the 'bottom' it is promoted to a king
                    if (pos.Item2 == 0 && Gameboard[pos].Color.Equals("red"))
                    {
                        Gameboard[pos] = new Piece("king" + Gameboard[pos].Color);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// finds every possible valid move that can be made from the given position
        /// </summary>
        /// <param name="prevPos">a spot on the Gameboard</param>
        /// <returns>a list of all valid moves that can be made from prevPos</returns>
        public List<Tuple<int, int>> PossibleMoves(Tuple<int, int> prevPos)
        {
            List<Tuple<int, int>> validMoves = new();
            if (prevPos == null)
            {
                return validMoves;
            }
            for (int i = 2; i > 0; i--)
            {
                Tuple<int, int> pos = new(prevPos.Item1 + i, prevPos.Item2 + i);
                Tuple<int, int> pos2 = new(prevPos.Item1 - i, prevPos.Item2 - i);
                Tuple<int, int> pos3 = new(prevPos.Item1 + i, prevPos.Item2 - i);
                Tuple<int, int> pos4 = new(prevPos.Item1 - i, prevPos.Item2 + i);
                if (Move(prevPos, pos, true))
                {
                    validMoves.Add(pos);
                }
                if (Move(prevPos, pos2, true))
                {
                    validMoves.Add(pos2);
                }
                if (Move(prevPos, pos3, true))
                {
                    validMoves.Add(pos3);
                }
                if (Move(prevPos, pos4, true))
                {
                    validMoves.Add(pos4);
                }
            }
            return validMoves;
        }

        /// <summary>
        /// checks to see if a move is valid then makes a move
        /// </summary>
        /// <param name="prevPos">the starting position for the move</param>
        /// <param name="pos">where the piece is trying to move to</param>
        /// <param name="check">true if the user is doing a test to see if a move 
        /// is valid or not and false if a move is being made</param>
        /// <returns>true if the move is valid and false otherwise</returns>
        public bool Move(Tuple<int, int> prevPos, Tuple<int, int> pos, bool check)
        {
            // gets the current piece object and the change in x and y
            // x is set to absolute since left and right moves are color independent
            Piece cur = Gameboard[prevPos];
            int x = Math.Abs(pos.Item1 - prevPos.Item1);
            int y = pos.Item2 - prevPos.Item2;

            // if the move is out of bounds then it is invalid
            if (pos.Item1 < 0 || pos.Item2 < 0 || pos.Item1 > 7 || pos.Item2 > 7)
            {
                return false;
            }
            // checks that we are trying to move a piece to a blank spot
            if (cur.Color == null || Gameboard[pos].Color != null)
            {
                return false;
            }
            // both blue and red kings act the same in terms of moving logic
            if (cur.Color.Contains("king"))
            {
                // since we don't care if a king is trying to move up or down depending on its color
                y = Math.Abs(y);
                // checks that we are moving 1 square diagonally and updates the Gameboard
                if (x == 1 && y == 1)
                {
                    if (!check)
                    {
                        Gameboard[pos] = cur;
                        Gameboard[prevPos] = new Piece(null);
                    }
                    return true;
                }
                // attempts to capture
                else if (x == 2 && y == 2)
                {
                    return Capture(prevPos, pos, check);
                }
            }
            // non-king blue pieces can only move 'down' the Gameboard so the y bound is positive 1
            else if (cur.Color.Equals("blue"))
            {
                // checks that we are moving 1 square diagonally and updates the Gameboard
                if (x == 1 && y == 1)
                {
                    if (!check)
                    {
                        Gameboard[pos] = cur;
                        Gameboard[prevPos] = new Piece(null);
                        // if a blue piece makes it all the way to the 'top' it is promoted to a king
                        if (pos.Item2 == 7)
                        {
                            Gameboard[pos] = new Piece("king" + Gameboard[pos].Color);
                        }
                    }
                    return true;
                }
                // attempts to capture
                else if (x == 2 && y == 2)
                {
                    return Capture(prevPos, pos, check);
                }
            }
            // non-king red pieces can only move 'down' the Gameboard so the y bound is positive 1
            else if (cur.Color.Equals("red"))
            {
                // checks that we are moving 1 square diagonally and updates the Gameboard
                if (x == 1 && y == -1)
                {
                    if (!check)
                    {
                        Gameboard[pos] = cur;
                        Gameboard[prevPos] = new Piece(null);
                        // if a red piece makes it all the way to the 'bottom' it is promoted to a king
                        if (pos.Item2 == 0)
                        {
                            Gameboard[pos] = new Piece("king" + Gameboard[pos].Color);
                        }
                    }
                    return true;
                }
                // attempts to capture
                else if (x == 2 && y == -2)
                {
                    return Capture(prevPos, pos, check);
                }
            }
            return false;
        }

        /// <summary>
        /// checks to see if a move that was made captured a piece
        /// </summary>
        /// <param name="prevPos">the starting position of the move</param>
        /// <param name="pos">the end position of the move</param>
        /// <returns>true if the move was a capture and false otherwise</returns>
        public bool TookPiece(Tuple<int, int> prevPos, Tuple<int, int> pos)
        {
            if (prevPos == null || pos == null)
            {
                return false;
            }
            if (Math.Abs(prevPos.Item1 - pos.Item1) == 2)
            {
                return true;
            }
            return false;
        }
    }
}
