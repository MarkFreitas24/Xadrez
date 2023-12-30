using board;
using Chess;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('c', 7);

            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());

            Console.WriteLine();
        }
    }
}