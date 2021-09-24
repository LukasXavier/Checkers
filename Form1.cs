using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {

        private Board checkerBoard = new();
        private int clickMemory;
        private Tuple<int,int> prevPos;

        public Form1()
        {
            InitializeComponent();
            this.Width = 517;
            this.Height = 540;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Image imageFile = Image.FromFile("board.png");

            Graphics newGraphics = Graphics.FromImage(imageFile);

            e.Graphics.DrawImage(imageFile, 0, 0, 500, 500);

            Brush blueBrush = new SolidBrush(Color.FromArgb(0, 150, 255));
            Brush kingBlueBrush = new SolidBrush(Color.FromArgb(0, 0, 100));
            Brush redBrush = new SolidBrush(Color.FromArgb(175, 0, 0));
            Brush kingRedBrush = new SolidBrush(Color.FromArgb(100, 0, 0));

            Font drawFont = new Font("Arial", 45);
            SolidBrush drawBrush = new SolidBrush(Color.White);
            float stringX = this.Width / 2 - 150;
            float stringY = this.Height / 2 - 70;
            StringFormat drawFormat = new();
            drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;

            foreach (Tuple<int,int> key in checkerBoard.board.Keys)
            {
                Piece cur = checkerBoard.board[key];
                if (cur == null)
                {
                    continue;
                }
                // 62.5 pixels is the distance between each square on the board
                // and an offset of 8 pixels is given so the pieces are centered
                int x = key.Item1 * 62 + 8;
                int y = key.Item2 * 62 + 8;
                String color = cur.Color;
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
                else
                {
                    e.Graphics.FillEllipse(kingBlueBrush, x, y, 50, 50);
                }
            }
            if (checkerBoard.gameOver() == 1)
            {
                e.Graphics.FillRectangle(kingBlueBrush, 0, 0, 517, 540);
                e.Graphics.DrawString("Blue Wins", drawFont, drawBrush, stringX, stringY, drawFormat);
            }
            else if (checkerBoard.gameOver() == 2)
            {
                e.Graphics.FillRectangle(kingRedBrush, 0, 0, 517, 540);
                e.Graphics.DrawString("Red Wins", drawFont, drawBrush, stringX, stringY, drawFormat);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Tuple<int, int> coord = new(e.X/62, e.Y/62);
            if (clickMemory == 0)
            {
                if (checkerBoard.board[coord] != null)
                {
                    this.prevPos = coord;
                    clickMemory++;
                }
            }
            else
            {
                clickMemory = 0;
                checkerBoard.Move(this.prevPos, coord);
                this.Refresh();
            }
        }

    }
}
