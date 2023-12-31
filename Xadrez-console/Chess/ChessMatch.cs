using board;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Round;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.Branca;
            Finished = false;
            PlaceParts();
        }

        public void ExecuteMovement(Position origin, Position destiny)
        {
            Part p = Board.RemovePart(origin);
            p.IncreaseQuantityMovement();
            Part capturedPart = Board.RemovePart(destiny);
            Board.PlacePart(p, destiny);
        }

        private void PlaceParts()
        {
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('c', 1).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('c', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('d', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('e', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('e', 1).ToPosition());
            Board.PlacePart(new King(Board, Color.Branca), new ChessPosition('d', 1).ToPosition());

            Board.PlacePart(new Tower(Board, Color.Preta), new ChessPosition('c', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Preta), new ChessPosition('c', 8).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Preta), new ChessPosition('d', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Preta), new ChessPosition('e', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Preta), new ChessPosition('e', 8).ToPosition());
            Board.PlacePart(new King(Board, Color.Preta), new ChessPosition('d', 8).ToPosition());
        }
    }
}
