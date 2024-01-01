using System.Collections.Generic;
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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblesMovements = match.Board.Part(origin).PossiblesMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblesMovements);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position detiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, detiny);

                        match.MakePlay(origin, detiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);


            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}