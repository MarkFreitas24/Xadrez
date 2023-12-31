using System.Collections.Generic;
using board;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Round {  get; private set; }
        public Color CurrentPlayer {  get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Part> Parts;
        private HashSet<Part> Captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.Branca;
            Finished = false;
            Check = false;
            Parts = new HashSet<Part>();
            Captured = new HashSet<Part>();
            PlaceParts();
        }

        public Part ExecuteMovement(Position origin, Position destiny)
        {
            Part p = Board.RemovePart(origin);
            p.IncreaseQuantityMovement();
            Part capturedPart = Board.RemovePart(destiny);
            Board.PlacePart(p, destiny);
            if (capturedPart != null)
            {
                Captured.Add(capturedPart);
            }
            return capturedPart;
        }

        public void UndoMovement(Position origin, Position destiny, Part capturedPart)
        {
            Part p = Board.RemovePart(destiny);
            p.DecreaseQuantityMovement();
            if (capturedPart != null)
            {
                Board.PlacePart(capturedPart, destiny);
                Captured.Remove(capturedPart);
            }
            Board.PlacePart(p, origin);
        }

        public void MakePlay(Position origin, Position destiny)
        {
            Part capturedPart = ExecuteMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPart);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

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

        public HashSet<Part> CapturedParts(Color color)
        {
            HashSet<Part> parts = new HashSet<Part>();
            foreach (Part x in Captured)
            {
                if(x.Color == color)
                {
                    parts.Add(x);
                }
            }
            return parts;
        }

        public HashSet<Part> PartsInGame(Color color)
        {
            HashSet<Part> parts = new HashSet<Part>();
            foreach (Part x in Parts)
            {
                if (x.Color == color)
                {
                    parts.Add(x);
                }
            }
            parts.ExceptWith(CapturedParts(color));
            return parts;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.Branca)
            {
                return Color.Vermelha;
            }
            else
            {
                return Color.Branca;
            }
        }

        private Part King(Color color)
        {
            foreach(Part x in PartsInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Part k = King(color);
            if (k == null)
            {
                throw new BoardException("Não tem rei da cor" + color + " no tabuleiro!");
            }
            foreach(Part x in PartsInGame(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public void InputNewPart(char column, int line, Part part)
        {
            Board.PlacePart(part, new ChessPosition(column, line).ToPosition());
            Parts.Add(part);
        }

        private void PlaceParts()
        {
            InputNewPart('c', 1, new Tower(Board, Color.Branca));
            InputNewPart('c', 2, new Tower(Board, Color.Branca));
            InputNewPart('d', 2, new Tower(Board, Color.Branca));
            InputNewPart('e', 2, new Tower(Board, Color.Branca));
            InputNewPart('e', 1, new Tower(Board, Color.Branca));
            InputNewPart('d', 1, new King(Board, Color.Branca));

            InputNewPart('c', 7, new Tower(Board, Color.Vermelha));
            InputNewPart('c', 8, new Tower(Board, Color.Vermelha));
            InputNewPart('d', 7, new Tower(Board, Color.Vermelha));
            InputNewPart('e', 7, new Tower(Board, Color.Vermelha));
            InputNewPart('e', 8, new Tower(Board, Color.Vermelha));
            InputNewPart('d', 8, new King(Board, Color.Vermelha));
        }
    }
}
