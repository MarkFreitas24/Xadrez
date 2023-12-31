using board;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Round {  get; private set; }
        public Color CurrentPlayer {  get; private set; }
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

        public void MakePlay(Position origin, Position destiny)
        {
            ExecuteMovement(origin, destiny);
            Round++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position)
        {
            if(Board.Part(position) == null)
            {
                throw new BoardException("Não Existe peça na posição de origem escolhida!");
            }
            if(CurrentPlayer != Board.Part(position).Color)
            {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }
            if (!Board.Part(position).TherePossibleMovements())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Part(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Posição de destino inválida");
            }
        }

        private void ChangePlayer()
        {
            if(CurrentPlayer == Color.Branca)
            {
                CurrentPlayer = Color.Vermelha;
            }
            else
            {
                CurrentPlayer = Color.Branca;
            }
        }

        private void PlaceParts()
        {
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('c', 1).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('c', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('d', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('e', 2).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Branca), new ChessPosition('e', 1).ToPosition());
            Board.PlacePart(new King(Board, Color.Branca), new ChessPosition('d', 1).ToPosition());

            Board.PlacePart(new Tower(Board, Color.Vermelha), new ChessPosition('c', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Vermelha), new ChessPosition('c', 8).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Vermelha), new ChessPosition('d', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Vermelha), new ChessPosition('e', 7).ToPosition());
            Board.PlacePart(new Tower(Board, Color.Vermelha), new ChessPosition('e', 8).ToPosition());
            Board.PlacePart(new King(Board, Color.Vermelha), new ChessPosition('d', 8).ToPosition());
        }
    }
}
