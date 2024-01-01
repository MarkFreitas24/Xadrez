using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_console.Chess
{
    internal class Pawn : Part
    {
        public Pawn(Board board, Color color) : base(board, color) { }

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

        private bool CanMove(Position position)
        {
            Part part = Board.Part(position);
            return part == null || part.Color != Color;
        }
        public override bool[,] PossiblesMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            if(Color == Color.Branca)
            {
                position.SetValues(position.Line - 1, position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line - 2, position.Column);
                if (Board.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line - 1, position.Column - 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line - 1, position.Column + 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }

            }
            else
            {
                position.SetValues(position.Line + 1, position.Column);
                if (Board.ValidPosition(position) && Free(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line + 2, position.Column);
                if (Board.ValidPosition(position) && Free(position) && QuantityMovements == 0)
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line + 1, position.Column - 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }
                position.SetValues(position.Line + 1, position.Column + 1);
                if (Board.ValidPosition(position) && ThereEnemy(position))
                {
                    movements[position.Line, position.Column] = true;
                }
            }

            return movements;
        }
    }
}
