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

                while(!match.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    Console.WriteLine("Destino: ");
                    Position detiny = Screen.ReadChessPosition().ToPosition();

                    match.ExecuteMovement(origin, detiny);
                }

                

            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}