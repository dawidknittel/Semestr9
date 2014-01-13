using System;
using System.Collections;
using System.Linq;
using Chess.Model.BlackPieces;

namespace Chess.Model
{
    public static class ArtficalInteligence
    {
        private static int NewStartupRow;
        private static int NewStartupColumn;
        private static int NewDestinationRow;
        private static int NewDestinationColumn;

        #region Public Methods

        public static void StartAlgorithm()
        {
            do
            {
                GeneratePossibleIndividuals();

                Individual.firstIndividual = Selection();
                Individual.secondIndividual = Selection();

                Crossing();

            }
            while (!CheckRandomPiece(NewStartupRow, NewStartupColumn, NewDestinationRow, NewDestinationColumn));

            MakeMove();
        }

        #endregion
        #region Private Methods

        private static void GeneratePossibleIndividuals()
        {
            foreach (Piece piece in ChessBoard.Pieces)
            {
                switch (piece.ChessPiece)
                {
                    case ChessPiece.BlackBishop:
                        {
                            ((BlackBishop)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                    case ChessPiece.BlackKing:
                        {
                            ((BlackKing)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                    case ChessPiece.BlackKnight:
                        {
                            ((BlackKnight)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                    case ChessPiece.BlackPawn:
                        {
                            ((BlackPawn)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                    case ChessPiece.BlackQueen:
                        {
                            ((BlackQueen)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                    case ChessPiece.BlackRook:
                        {
                            ((BlackRook)piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                            ChessBoard.DeSelectAvailable(piece);
                            break;
                        }
                }
            }
        }

        private static Individual Selection()
        {
            Random generator = new Random(DateTime.Now.Millisecond);

            int IndexOfRandomIndividual = generator.Next(1, Individual.sumAllRates);

            return Individual.RandomPopulation.ElementAt(IndexOfRandomIndividual);
        }

        private static void Crossing()
        {
            int randomIndex;
            Random generator = new Random(DateTime.Now.Millisecond);

            randomIndex = generator.Next(1, 3);

            NewStartupRow = Individual.GetIntFromBitArray(newBitArray(randomIndex, Individual.firstIndividual.StartupRowBits, Individual.secondIndividual.StartupRowBits[randomIndex]));
            NewStartupColumn = Individual.GetIntFromBitArray(newBitArray(randomIndex, Individual.firstIndividual.StartupColumnBits, Individual.secondIndividual.StartupColumnBits[randomIndex]));

            randomIndex = generator.Next(1, 3);
            
            NewDestinationRow = Individual.GetIntFromBitArray(newBitArray(randomIndex, Individual.firstIndividual.DestinationRowBits, Individual.secondIndividual.DestinationRowBits[randomIndex]));
            NewDestinationColumn = Individual.GetIntFromBitArray(newBitArray(randomIndex, Individual.firstIndividual.DestinationColumnBits, Individual.secondIndividual.DestinationColumnBits[randomIndex]));
        }

        private static BitArray newBitArray(int randomIndex, BitArray bitArray, bool bit)
        {
            BitArray newBitArray = new BitArray(3);

            for (int i = 0; i < 3; i++)
            {
                if (i == randomIndex)
                    newBitArray[i] = bit;
                else
                    newBitArray[i] = bitArray[i];
            }

            return newBitArray;
        }

        private static bool CheckRandomPiece(int newRow, int newColumn, int newDestinationRow, int newDestinationColumn)
        {
            foreach (Piece piece in ChessBoard.Pieces)
            {
                if (piece.Row == newRow && piece.Column == newColumn && piece.PieceColor.Equals(PieceColor.Black))
                {
                    foreach (Individual individual in piece.PossibleIndividuals)
                    {
                        if (individual.DestinationRow == newDestinationRow && individual.DestinationColumn == newDestinationColumn)
                            return true;
                    }               
                }
            }
            return false;
        }

        private static void MakeMove()
        {
            ChessBoard.MakeSelection(ChessBoard.GetField((Int16)NewStartupRow, (Int16)NewStartupColumn));
            if (Piece.FindPiece((Int16) NewStartupRow, (Int16) NewStartupColumn).Selected)
                ChessBoard.MakeSelection(ChessBoard.GetField((Int16) NewDestinationRow, (Int16) NewDestinationColumn));
        }

        #endregion
    }
}
