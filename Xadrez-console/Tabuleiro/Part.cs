
using System.Drawing;

namespace Tabuleiro
{
    internal class Part
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Part() { }

        public Part(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            Board = board;
            QuantityMovements = 0;
        }


    }
}
