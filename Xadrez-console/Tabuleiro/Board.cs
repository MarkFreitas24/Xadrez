namespace Tabuleiro
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

        public void PlacePart(Part p, Position pos)
        {
            Parts[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
    }
}
