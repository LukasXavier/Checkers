using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        // constructs the Board
        // both clickMemory and prevPos is used for handling user input
        private readonly Board checkerBoard = new();
        private Opponent blue = new();
        private int clickMemory;
        private Tuple<int, int> prevPos;
        private bool playerTurn = true;

        /// <summary>
        /// A form object is a specific class provided by Microsoft that is modified by the user
        /// to generate a GUI. Functions in the form class need to have a specific naming
        /// convention that matches the documentation provided by Microsoft
        /// </summary>
        public Form1()
        {
            // here we set the form to exist and it's size
            InitializeComponent();
            Width = 517;
            Height = 540;
        }

        /// <summary>
        /// A paint function is used to draw on the form window that is generated.
        /// Like Tkinter in python there are functions that handle the complex part of drawing
        /// specific shapes and importing images
        /// </summary>
        /// <param name="sender">a reference to the contorl/object that riased the event, i.e. Form</param>
        /// <param name="e">the event data provided by the control object</param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // images are loaded into an image object then placed into the canvas
            // this line of code is technically bad style, we couldn't figure out how to get the correct
            // path for a 'resource/img/' directory or something so we just placed it where the code runs
            Image imageFile = Image.FromFile("board.png");

            // this draws the image onto the canvas.
            e.Graphics.DrawImage(imageFile, 0, 0, 500, 500);

            // to draw something that isn't an image, you need to make a brush object of that color
            SolidBrush blueBrush = new(Color.FromArgb(0, 150, 255));
            SolidBrush kingBlueBrush = new(Color.FromArgb(0, 0, 100));
            SolidBrush redBrush = new(Color.FromArgb(175, 0, 0));
            SolidBrush kingRedBrush = new(Color.FromArgb(100, 0, 0));
            Pen pen = new(Brushes.Black);
            pen.Width = 3;

            // this chunk of code is for the game over screen, we set some text to be the color white
            // the font size 45, about center with a little offset to adjust for text being draw from
            // the top left of where you specify
            Font drawFont = new("Arial", 45);
            SolidBrush drawBrush = new(Color.White);
            float stringX = Width / 2 - 150;
            float stringY = Height / 2 - 70;
            StringFormat drawFormat = new();
            drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;

            // a foreach loop syntax somewhere between javascript and python
            // we go over each key of the board which stores where pieces are currently located at
            foreach (Tuple<int, int> key in checkerBoard.board.Keys)
            {
                // cur is the current Piece we are drawing
                String color = checkerBoard.board[key].Color;
                // since half or more of the board is empty we need to check if we're on a blank one or not
                if (color == null)
                {
                    continue;
                }
                // 62.5 pixels is the distance between each square on the board
                // and an offset of 8 pixels is given so the pieces are centered
                int x = key.Item1 * 62 + 8;
                int y = key.Item2 * 62 + 8;

                // a Piece is determined by it's color so this how we know what color to make it
                // we place each piece at x,y
                if (color.Equals("blue"))
                {
                    e.Graphics.FillEllipse(blueBrush, x, y, 50, 50);
                }
                else if (color.Equals("red"))
                {
                    e.Graphics.FillEllipse(redBrush, x, y, 50, 50);
                }
                else if (color.Equals("kingred"))
                {
                    e.Graphics.FillEllipse(kingRedBrush, x, y, 50, 50);
                }
                // Since we control the color field, we know the last option is the kingblue
                else
                {
                    e.Graphics.FillEllipse(kingBlueBrush, x, y, 50, 50);
                }

                foreach (Tuple<int, int> newPos in checkerBoard.PossibleMoves(prevPos))
                {
                    x = newPos.Item1 * 62 + 8;
                    y = newPos.Item2 * 62 + 8;
                    e.Graphics.DrawEllipse(pen, x, y, 50, 50);
                }
            }

            if (clickMemory == 1 && prevPos != null)
            {
                int indicatorX = prevPos.Item1 * 62 + 8;
                int indicatorY = prevPos.Item2 * 62 + 8;
                e.Graphics.DrawEllipse(pen, indicatorX, indicatorY, 50, 50);
            }

            // gameOver() == 0 means there are still pieces on the board
            // gameOver() == 1 means that blue won and 2 means that red won
            if (checkerBoard.GameOver() == 1)
            {
                // draws a square the size of the canvas and displays 'Blue Wins'
                e.Graphics.FillRectangle(kingBlueBrush, 0, 0, 517, 540);
                e.Graphics.DrawString("Blue Wins", drawFont, drawBrush, stringX, stringY, drawFormat);
            }
            else if (checkerBoard.GameOver() == 2)
            {
                // draws a square the size of the canvas and displays 'Red Wins'
                e.Graphics.FillRectangle(kingRedBrush, 0, 0, 517, 540);
                e.Graphics.DrawString("Red Wins", drawFont, drawBrush, stringX, stringY, drawFormat);
            }

            if (!playerTurn)
            {
                blue.OpponentMove(checkerBoard, playerTurn);
                Thread.Sleep(1500);
                if (!blue.captured)
                {
                    playerTurn = true;
                }
                Refresh();
            }

        }

        /// <summary>
        /// Checks if either a piece is being selected or being moved,
        /// When a peice is selected checks if there is a piece where the user clicked,
        /// When a peice is moving checks if that move is valid
        /// </summary>
        /// <param name="sender">a reference to the contorl/object that riased the event, i.e. Form</param>
        /// <param name="e">the event data provided by the control object</param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!playerTurn)
            {
                return;
            }
            // we get the integer value of where the user clicked
            Tuple<int, int> coord = new(e.X / 62, e.Y / 62);
            // clickMemory 0 means that we are selecting a piece
            if (clickMemory == 0)
            {
                // checks if the user clicked on not a piece
                if (checkerBoard.board[coord].Color != null)
                {
                    // temporarly stores the pieces location
                    if (checkerBoard.board[coord].Color.Contains("red"))
                    {
                        prevPos = coord;
                        clickMemory++;
                        Refresh();
                    }
                }
            }
            // clickMemory 1 means that the position is the move-to position
            else
            {
                clickMemory = 0;
                if (checkerBoard.Move(prevPos, coord, true) 
                    && checkerBoard.board[prevPos].Color.Contains("red"))
                {
                    if (!checkerBoard.TookPiece(prevPos, coord))
                    {
                        playerTurn = false;
                    }
                    checkerBoard.Move(prevPos, coord, false);
                }
                prevPos = null;
                Refresh();
            }
        }

    }
}
