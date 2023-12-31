﻿

namespace board
{
    abstract class Part
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Part() { }

        public Part(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            QuantityMovements = 0;
        }

        public void IncreaseQuantityMovement()
        {
            QuantityMovements++;
        }

        public abstract bool[,] PossibleMovements();
        


    }
}
