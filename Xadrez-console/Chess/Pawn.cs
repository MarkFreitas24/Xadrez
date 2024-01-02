using board;

namespace Chess
{
    internal class Pawn : Part
    {
        private ChessMatch Match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ThereEnemy(Position position)
        {
            Part part = Board.Part(position);
            return part != null && part.Color != Color;
        }

        private bool Free(Position position)
        {
            return Board.Part(position) == null;
        }

        public override bool[,] PossiblesMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);


            if(Color == Color.Branca)
            {
                position.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line - 2, Position.Column);
                Position position2 = new Position(position.Line - 1, position.Column);
                if (Board.ValidPosition(position2) && Free(position2) && Board.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }

                // En Passant
                if(Position.Line == 3)
                {
                    Position west = new Position(Position.Line, Position.Column - 1);
                    if(Board.ValidPosition(west) && ThereEnemy(west) && Board.Part(west) == Match.VulnerableEnPassant)
                    {
                        movements[west.Line - 1, west.Column] = true;
                    }

                    Position East = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(East) && ThereEnemy(East) && Board.Part(East) == Match.VulnerableEnPassant)
                    {
                        movements[East.Line - 1, East.Column] = true;
                    }
                }

            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 2, Position.Column);
                Position position2 = new Position(position.Line + 1, position.Column);
                if (Board.ValidPosition(position2) && Free(position2) && Board.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }

                // En Passant
                if (Position.Line == 4)
                {
                    Position west = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(west) && ThereEnemy(west) && Board.Part(west) == Match.VulnerableEnPassant)
                    {
                        movements[west.Line + 1, west.Column] = true;
                    }

                    Position East = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(East) && ThereEnemy(East) && Board.Part(East) == Match.VulnerableEnPassant)
                    {
                        movements[East.Line + 1, East.Column] = true;
                    }
                }
            }

            return movements;
        }
    }
}
