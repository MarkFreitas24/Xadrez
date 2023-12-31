using board;


namespace Chess
{
    internal class Tower : Part
    {
        public Tower(Board board,Color color) : base(board, color)
        {
        }
        public override string ToString()
        {
            return "T";
        }

        private bool CanMove(Position position)
        {
            Part part = Board.Part(position);
            return part == null || part.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //North
            position.SetValues(Position.Line - 1, Position.Column);
            while(Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if(Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.Line = position.Line - 1;
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

                position.Line = position.Line + 1;
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

                position.Column = position.Column + 1;
            }

            //West
            position.SetValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                movements[position.Line, position.Column] = true;
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }

                position.Column = position.Column - 1;
            }

            return movements;
        }
    }
}
