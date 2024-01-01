using board;


namespace Chess
{
    internal class King : Part
    {
        public King(Board board,Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "K";
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

            //North
            position.SetValues(Position.Line - 1, Position.Column);
            if(Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //North East
            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //East
            position.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //South East
            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //South
            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //South West
            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //West
            position.SetValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            //North West
            position.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
            }

            return movements;
        }
    }
}
