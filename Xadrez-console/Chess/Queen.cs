using board;

namespace Chess
{
    internal class Queen : Part
    {
        public Queen(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "Q";
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

            //West
            position.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line, position.Column - 1);
            }

            //East
            position.SetValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line, position.Column + 1);
            }

            //North
            position.SetValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line - 1, position.Column);
            }

            //South
            position.SetValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.SetValues(position.Line + 1, position.Column);
            }

            //North West
            position.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column - 1);
            }

            //North East
            position.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column + 1);
            }

            //South East
            position.SetValues(Position.Line + 1, position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Column + 1);
            }

            //South West
            position.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Column - 1);
            }

            return movements;
        }
    }
}
