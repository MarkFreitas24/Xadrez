using board;
using Chess;

namespace Xadrez_console
{
    internal class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines;  i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Part(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPart(board.Part(i,j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintPart(Part part)
        {
            if (part.Color == Color.Branca)
            {
                Console.Write(part);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(part);
                Console.ForegroundColor = aux;
            }
        }
    }
}
