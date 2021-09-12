﻿using System;
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
            this.Width = 500;
            this.Height = 500;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            /*
            Pen myPen = new Pen(Color.Black, 5);
            Brush myBrush = new SolidBrush(Color.Blue);
            g.DrawLine(myPen, 200, 200, 400, 100);
            */
            //DialogResult result = MessageBox.Show("test", "caption");

            string ImagesDirectory = Application.ExecutablePath;
            //ImagesDirectory.Remove(ImagesDirectory.);

            //Image newIamge = Image.FromFile(ImagesDirectory);

            DialogResult result = MessageBox.Show(ImagesDirectory, "caption");
        }
    }
}
