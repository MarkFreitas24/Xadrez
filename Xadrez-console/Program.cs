using board;
using Chess;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board gameBoard = new Board(8, 8);

                gameBoard.PlacePart(new Tower(gameBoard, Color.Preta), new Position(0, 0));
                gameBoard.PlacePart(new Tower(gameBoard, Color.Preta), new Position(1, 3));
                gameBoard.PlacePart(new King(gameBoard, Color.Preta), new Position(2, 4));

                Screen.PrintBoard(gameBoard);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}