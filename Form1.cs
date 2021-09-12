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


            // black squares are spaced 125px apart
            int offset = 125;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 1)
                    {
                        e.Graphics.FillEllipse(blueBrush, 69 + (j * offset), 7 + (i * (offset / 2)), 50, 50);
                    }
                    else
                    {
                        e.Graphics.FillEllipse(blueBrush, 6 + (j * offset), 7 + (i * (offset / 2)), 50, 50);
                    }
                    
                }
            }

        }
    }
}
