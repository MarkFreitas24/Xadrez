namespace Tabuleiro
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public Part[,] Parts;

        public Board() { }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Parts = new Part[Lines, Columns];
        }
    }
}
