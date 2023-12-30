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
                ChessMatch match = new ChessMatch();

                Screen.PrintBoard(match.Board);

            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}