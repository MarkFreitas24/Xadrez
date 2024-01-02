using board;


namespace Chess
{
    internal class King : Part
    {
        private ChessMatch Match;
        public King(Board board,Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
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

        private bool RookTowerTest(Position position)
        {
            Part part = Board.Part(position);
            return part != null && part is Tower && part.Color == Color && part.QuantityMovements == 0;
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

            // ROOK
            //LITTLE ROOK

            if (QuantityMovements == 0 && !Match.Check)
            {
                Position positionTower1 = new Position(Position.Line, Position.Column + 3);
                if (RookTowerTest(positionTower1))
                {
                    Position position1 = new Position(Position.Line, Position.Column + 1);
                    Position position2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.Part(position1) == null && Board.Part(position2) == null)
                    {
                        movements[Position.Line, Position.Column + 2] = true;
                    }
                }
            }

            // BIG ROOK
            if (QuantityMovements == 0 && !Match.Check)
            {
                Position positionTower2 = new Position(Position.Line, Position.Column - 4);
                if (RookTowerTest(positionTower2))
                {
                    Position position1 = new Position(Position.Line, Position.Column - 1);
                    Position position2 = new Position(Position.Line, Position.Column - 2);
                    Position position3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Part(position1) == null && Board.Part(position2) == null && Board.Part(position3) == null)
                    {
                        movements[Position.Line, Position.Column - 2] = true;
                    }
                }
            }
            return movements;
        }
    }
}
