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

        public void DecreaseQuantityMovement()
        {
            QuantityMovements--;
        }

        public bool TherePossibleMovements()
        {
            bool[,] possible = PossiblesMovements();
            for (int i = 0; i < Board.Lines;  i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possible[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position position)
        {
            return PossiblesMovements()[position.Line, position.Column];
        }

        public abstract bool[,] PossiblesMovements();
        


    }
}
