using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Piece
    {
        public string Color;

        public Piece(string color)
        {
            this.Color = color;
        }

        public void promote()
        {
            Color = "king" + Color;
        }

    }
}
