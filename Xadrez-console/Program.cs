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

                gameBoard.PlacePart(new Tower(gameBoard, Color.Branca), new Position(3, 5));
                gameBoard.PlacePart(new Tower(gameBoard, Color.Branca), new Position(5, 6));
                gameBoard.PlacePart(new King(gameBoard, Color.Branca), new Position(6, 6));


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