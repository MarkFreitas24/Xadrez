using Tabuleiro;
using Chess;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.PlacePart(new Tower(board, Color.Preta), new Position(0, 0));
            
            board.PlacePart(new Tower(board, Color.Preta), new Position(1, 3));
            board.PlacePart(new King(board, Color.Preta), new Position(2, 4));

            Screen.PrintBoard(board);

            Console.WriteLine();
        }
    }
}