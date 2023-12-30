namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Part[,] Parts;

        public Board() { }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Parts = new Part[Lines, Columns];
        }

        public Part Part(int line, int column)
        {
            return Parts[line, column];
        }

        public Part Part(Position position)
        {
            return Parts[position.Line, position.Column];
        }

        public bool PartExists(Position position)
        {
            ValidatePosition(position);
            return Part(position) != null;

        }

        public void PlacePart(Part p, Position position)
        {
            if(PartExists(position))
            {
                throw new BoardException("Já existe uma peça nessa posição!");
            }
            Parts[position.Line, position.Column] = p;
            p.Position = position;
        }

        public bool ValidPosition(Position position)
        {
            if(position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Posição inválida!");
            }
        }
    }
}
