using System;
using System.Collections.Generic;

namespace Checkers
{

    // test
    class Board
    {
        /**
         *  original Piece was an object but we kept reducing it's functionally until it was just a
         *  String with a single 1 line function, to further demonstrate C# we pulled it out and made
         *  it a struct, we could have made the board a map from (int, int) --> String but thought
         *  since we're using C# might as well use a struct add some flare.
         */
        public struct Piece
        {
            public String Color;
            public Piece(string color)
            {
                Color = color;
            }
            // this isn't need for Checkers but I thought it was interesting that structs can have methods in C#
            //public override string ToString() => $"{Color}";
        }

        // this is our data structure for storing the pieces
        public Dictionary<Tuple<int,int>, Piece> board;

        /// <summary>
        /// this creates the board and populates it with red/blue pieces
        /// </summary>
        public Board()
        {
            // C# allows us to not need to specify type when constructing a new instance if it is already defined somewhere else
            board = new();

            // loops through all the locations on the board and populates them with pieces
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    // the keys need to be established before we set the pieces down
                    board.Add(new(x, y), new(null));
                    // for the values between [0,2] are blue pieces but the middle row is offset from the others
                    if (y == 0 || y == 2)
                    {
                        if ((x + 1) % 2 == 0)
                        {
                            board[new(x, y)] = new("blue");
                        }
                    }
                    if (y == 1)
                    {
                        if (x % 2 == 0)
                        {
                            board[new(x, y)] = new("blue");
                        }
                    }
                    // for the values between [5,7] are red pieces but the middle row is offset from the others
                    if (y == 5 || y == 7)
                    {
                        if (x % 2 == 0)
                        {
                            board[new(x, y)] = new("red");
                        }
                    }
                    if (y == 6)
                    {
                        if ((x + 1) % 2 == 0)
                        {
                            board[new(x, y)] = new("red");
                        }
                    }
                }
            }

        }

        /// <summary>
        /// This returns the current status of the game as int from [0,2]
        /// </summary>
        /// <returns>0 for game not over, 1 for blue wins, 2 for red wins</returns>
        public int GameOver()
        {
            int blue = 0;
            int red = 0;
            // counts sides pieces
            foreach (Tuple<int, int> key in board.Keys)
            {
                if (board[key].Color != null)
                {
                    // since a piece and either be red, blue, kingred, kingblue, we just check if it contains red or blue
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
            else if (blue > 0 && red == 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// This is the logic for capturing a piece
        /// </summary>
        /// <param name="prevPos">(int, int) of the pieces current position</param>
        /// <param name="pos">(int, int) of the pieces new position</param>
        /// <returns></returns>
        private void Capture(Tuple<int, int> prevPos, Tuple<int, int> pos)
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
            if (board[between].Color == null || board[pos].Color != null)
            {
                return;
            }
            if (board[prevPos].Color.Contains("red") == board[between].Color.Contains("blue"))
            {
                // capturing a piece is the same as just deleting it
                board[pos] = board[prevPos];
                board[between] = new Piece(null);
                board[prevPos] = new Piece(null);
            }
        }

        /// <summary>
        /// This does all the logic for a valid move
        /// </summary>
        /// <param name="prevPos">(int, int) where the piece currently is</param>
        /// <param name="pos">(int, int) where the piece is going</param>
        public void Move(Tuple<int, int> prevPos, Tuple<int, int> pos)
        {
            // gets the current piece object and the change in x and y
            // x is set to absolute since left and right moves are color independent
            Piece cur = board[prevPos];
            int x = Math.Abs(pos.Item1 - prevPos.Item1);
            int y = pos.Item2 - prevPos.Item2;
            
            // checks that we are trying to move a piece to a blank spot
            if (cur.Color == null || board[pos].Color != null)
            {
                return;
            }
            // both blue and red kings act the same in terms of moving logic
            if (cur.Color.Contains("king"))
            {
                // since we don't care if a king is trying to move up or down depending on its color
                y = Math.Abs(y);
                // checks that we are moving 1 square diagonally and updates the board
                if (x == 1 && y == 1)
                {
                    board[pos] = cur;
                    board[prevPos] = new Piece(null);
                }
                // attempts to capture
                else if (x == 2 && y == 2)
                {
                    Capture(prevPos, pos);
                }
            }
            // non-king blue pieces can only move 'down' the board so the y bound is positive 1
            else if (cur.Color.Equals("blue"))
            {
                // checks that we are moving 1 square diagonally and updates the board
                if (x == 1 && y == 1)
                {
                    board[pos] = cur;
                    board[prevPos] = new Piece(null);
                }
                // attempts to capture
                else if (x == 2 && y == 2)
                {
                    Capture(prevPos, pos);
                }
                // if a blue piece makes it all the way to the 'bottom' it is promoted to a king
                if (pos.Item2 == 7)
                {
                    board[pos] = new Piece("king" + board[pos].Color);
                }
            }
            // non-king red pieces can only move 'down' the board so the y bound is positive 1
            else if (cur.Color.Equals("red"))
            {
                // checks that we are moving 1 square diagonally and updates the board
                if (x == 1 && y == -1)
                {
                    board[pos] = cur;
                    board[prevPos] = new Piece(null);
                }
                // attempts to capture
                else if (x == 2 && y == -2)
                {
                    Capture(prevPos, pos);
                }
                // if a red piece makes it all the way to the 'top' it is promoted to a king
                if (pos.Item2 == 0)
                {
                    board[pos] = new Piece("king" + board[pos].Color);
                }
            }
        }
    }
}
