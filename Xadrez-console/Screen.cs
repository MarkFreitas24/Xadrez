using System.Collections.Generic;
using board;
using Chess;

namespace Xadrez_console
{
    internal class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedParts(match);
            Console.WriteLine();
            Console.WriteLine("Turno: " + match.Round);
            Console.WriteLine("Aguardando jogada:" + match.CurrentPlayer);
            if (match.Check)
            {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void PrintCapturedParts(ChessMatch match)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            PrintSet(match.CapturedParts(Color.Branca));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Vermelhas: ");
            PrintSet(match.CapturedParts(Color.Vermelha));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Part> set)
        {
            Console.Write("[");
            foreach(Part x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines;  i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                        PrintPart(board.Part(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkCyan;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMovements[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPart(board.Part(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string play = Console.ReadLine();
            char column = play[0];
            int line = int.Parse(play[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPart(Part part)
        {
            if (part == null)
            {
                Console.Write("■ ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
