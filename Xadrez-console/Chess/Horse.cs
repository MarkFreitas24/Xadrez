using board;

namespace Chess
{
    internal class Horse : Part
    {
        public Horse(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position position)
        {
            Part part = Board.Part(position);
            return part == null || part.Color != Color;
        }
        public override bool[,] PossiblesMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            
            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            return movements;
        }
    }
}
