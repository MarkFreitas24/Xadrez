using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_console.Chess
{
    internal class Bishop : Part
    {
        public Bishop(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "B";
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

            //North West
            position.SetValues(position.Line - 1, position.Column - 1);
            while(Board.ValidPosition(position) && CanMove(position))
            {
                if(Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column - 1);
            }

            //North East
            position.SetValues(position.Line - 1, position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Column + 1);
            }

            //South East
            position.SetValues(position.Line + 1, position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                if (Board.Part(position) != null && Board.Part(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Column + 1);
            }

            //South West
            position.SetValues(position.Line + 1, position.Column - 1);
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
