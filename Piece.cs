namespace Checkers
{
    class Piece
    {
        public string Color;

        /// <summary>
        /// a single Piece of a checkers game
        /// </summary>
        /// <param name="color">A string, should be either 'blue' or 'red'</param>
        public Piece(string color)
        {
            Color = color;
        }

        /// <summary>
        /// Converts a regular piece to a king piece
        /// </summary>
        public void Promote()
        {
            Color = "king" + Color;
        }
    }
}
