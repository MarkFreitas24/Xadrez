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
        public Part VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayer = Color.Branca;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Parts = new HashSet<Part>();
            Captured = new HashSet<Part>();
            PlaceParts();
        }

        public Part ExecuteMovement(Position origin, Position destiny)
        {
            Part part = Board.RemovePart(origin);
            part.IncreaseQuantityMovement();
            Part capturedPart = Board.RemovePart(destiny);
            Board.PlacePart(part, destiny);
            if (capturedPart != null)
            {
                Captured.Add(capturedPart);
            }

            //LITTLE ROOK
            if (part is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Line, origin.Column + 3);
                Position destinyTower = new Position(origin.Line, origin.Column + 1);
                Part tower = Board.RemovePart(originTower);
                tower.IncreaseQuantityMovement();
                Board.PlacePart(tower, destinyTower);
            }
            //BIG ROOK
            if (part is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Line, origin.Column - 4);
                Position destinyTower = new Position(origin.Line, origin.Column - 1);
                Part tower = Board.RemovePart(originTower);
                tower.IncreaseQuantityMovement();
                Board.PlacePart(tower, destinyTower);
            }

            // En Passant
            if (part is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPart == null)
                {
                    Position positionPawn;
                    if (part.Color == Color.Branca)
                    {
                        positionPawn = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        positionPawn = new Position(destiny.Line - 1, destiny.Column);
                    }

                    capturedPart = Board.RemovePart(positionPawn);
                    Captured.Add(capturedPart);
                }
            }

            return capturedPart;
        }

        public void UndoMovement(Position origin, Position destiny, Part capturedPart)
        {
            Part part = Board.RemovePart(destiny);
            part.DecreaseQuantityMovement();
            if (capturedPart != null)
            {
                Board.PlacePart(capturedPart, destiny);
                Captured.Remove(capturedPart);
            }
            Board.PlacePart(part, origin);

            //LITTLE ROOK
            if (part is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Line, origin.Column + 3);
                Position destinyTower = new Position(origin.Line, origin.Column + 1);
                Part tower = Board.RemovePart(destinyTower);
                tower.DecreaseQuantityMovement();
                Board.PlacePart(tower, originTower);
            }
            //BIG ROOK
            if (part is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Line, origin.Column - 4);
                Position destinyTower = new Position(origin.Line, origin.Column - 1);
                Part tower = Board.RemovePart(destinyTower);
                tower.DecreaseQuantityMovement();
                Board.PlacePart(tower, originTower);
            }

            //En Passant
            if (part is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPart == VulnerableEnPassant)
                {
                    Part pawn = Board.RemovePart(destiny);
                    Position positionPawn;
                    if (part.Color == Color.Branca)
                    {
                        positionPawn = new Position(3, destiny.Column);
                    }
                    else
                    {
                        positionPawn = new Position(4, destiny.Column);
                    }
                    Board.PlacePart(pawn, positionPawn);
                }
            }
        }

        public void MakePlay(Position origin, Position destiny)
        {
            Part capturedPart = ExecuteMovement(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destiny, capturedPart);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Part part = Board.Part(destiny);

            //PROMOTION
            if(part is Pawn)
            {
                if(part.Color == Color.Branca && destiny.Line == 0 || (part.Color == Color.Vermelha && destiny.Line == 7))
                {
                    part = Board.RemovePart(destiny);
                    Parts.Remove(part);
                    Part quenn = new Queen(Board, part.Color);
                    Board.PlacePart(quenn, destiny);
                    Parts.Add(quenn);
                }
            }


            if (IsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (CheckmateTest(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }

            

            //En Passant
            if(part is Pawn && (destiny.Line == origin.Line - 2|| destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = part;
            }
            else
            {
                    VulnerableEnPassant = null;
            }

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
            if (!Board.Part(origin).PossibleMovement(destiny))
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
                bool[,] mat = x.PossiblesMovements();
                if (mat[k.Position.Line, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckmateTest(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach(Part x in PartsInGame(color))
            {
                bool[,] move = x.PossiblesMovements();
                for(int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (move[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Part capturedPart = ExecuteMovement(origin, destiny);
                            bool CheckTest = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPart);
                            if(!CheckTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void InputNewPart(char column, int line, Part part)
        {
            Board.PlacePart(part, new ChessPosition(column, line).ToPosition());
            Parts.Add(part);
        }

        private void PlaceParts()
        {
            
            InputNewPart('a', 1, new Tower(Board, Color.Branca));
            InputNewPart('b', 1, new Horse(Board, Color.Branca));
            InputNewPart('c', 1, new Bishop(Board, Color.Branca));
            InputNewPart('d', 1, new Queen(Board, Color.Branca));
            InputNewPart('e', 1, new King(Board, Color.Branca, this));
            InputNewPart('f', 1, new Bishop(Board, Color.Branca));
            InputNewPart('g', 1, new Horse(Board, Color.Branca));
            InputNewPart('h', 1, new Tower(Board, Color.Branca));
            InputNewPart('a', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('b', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('c', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('d', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('e', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('f', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('g', 2, new Pawn(Board, Color.Branca, this));
            InputNewPart('h', 2, new Pawn(Board, Color.Branca, this));

            InputNewPart('a', 8, new Tower(Board, Color.Vermelha));
            InputNewPart('b', 8, new Horse(Board, Color.Vermelha));
            InputNewPart('c', 8, new Bishop(Board, Color.Vermelha));
            InputNewPart('d', 8, new Queen(Board, Color.Vermelha));
            InputNewPart('e', 8, new King(Board, Color.Vermelha, this));
            InputNewPart('f', 8, new Bishop(Board, Color.Vermelha));
            InputNewPart('g', 8, new Horse(Board, Color.Vermelha));
            InputNewPart('h', 8, new Tower(Board, Color.Vermelha));
            InputNewPart('a', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('b', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('c', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('d', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('e', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('f', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('g', 7, new Pawn(Board, Color.Vermelha, this));
            InputNewPart('h', 7, new Pawn(Board, Color.Vermelha, this));
        }
    }
}
