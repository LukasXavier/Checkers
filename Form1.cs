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
            Brush redBrush = new SolidBrush(Color.Red);

            foreach (Tuple<int,int> key in checkerBoard.board.Keys)
            {
                //System.Diagnostics.Debug.WriteLine(key);
                Piece cur = checkerBoard.board[key];
                if (cur == null)
                {
                    continue;
                }
                // 62 pixels is exactly halfway between each square on the board
                // and an offset of 8 pixels is given so the pieces are centered
                int x = cur.pos.Item1 * 62 + 8;
                int y = cur.pos.Item2 * 62 + 8;
                String color = cur.Color;
                if (color.Equals("blue"))
                {
                    e.Graphics.FillEllipse(blueBrush, x, y, 50, 50);
                }
                else
                {
                    e.Graphics.FillEllipse(redBrush, x, y, 50, 50);
                }
            }
            //this.Invalidate();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Tuple<int, int> test = new(3,2);
            checkerBoard.board[test].Move("left");
            this.Refresh();
        }
    }
}
