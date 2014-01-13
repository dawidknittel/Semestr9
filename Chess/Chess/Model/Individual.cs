using System;
using System.Collections;
using System.Collections.Generic;

namespace Chess.Model
{
    public class Individual
    {
        public int StartupRow;
        public int StartupColumn;

        public int DestinationRow;
        public int DestinationColumn;

        public ChessPiece ChessPiece;

        public BitArray StartupRowBits;
        public BitArray StartupColumnBits;

        public BitArray DestinationRowBits;
        public BitArray DestinationColumnBits;

        public int Rate;

        public static List<Individual> Population = new List<Individual>();
        public static List<Individual> RandomPopulation = new List<Individual>(); 
        public static Individual firstIndividual = null;
        public static Individual secondIndividual = null;

        public static int sumAllRates = 0;

        public Individual(int startupRow, int startupColumn, int destinationRow, int destinationColumn, ChessPiece chessPiece)
        {
            StartupRow = startupRow;
            StartupColumn = startupColumn;
            DestinationRow = destinationRow;
            DestinationColumn = destinationColumn;

            ChessPiece = chessPiece;

            StartupRowBits = new BitArray(3);
            ConvertIntToBits(StartupRow, StartupRowBits);
            StartupColumnBits = new BitArray(3);
            ConvertIntToBits(StartupColumn, StartupColumnBits);
            DestinationRowBits = new BitArray(3);
            ConvertIntToBits(DestinationRow, DestinationRowBits);
            DestinationColumnBits = new BitArray(3);
            ConvertIntToBits(DestinationColumn, DestinationColumnBits);

            Rate = GetRate();
            Population.Add(this);
            sumAllRates += Rate;

            InsertIndividuals();
        }

        public static void ConvertIntToBits(int integer, BitArray bitArray)
        {
            int indexer = 0;
            do
            {
                bitArray[indexer++] = ((integer % 2 == 0) ? false : true);
                integer = integer / 2;
            } 
            while (integer != 0);
        }

        public static int GetIntFromBitArray(BitArray bitArray)
        {
            int value = 0;

            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, i));
            }

            return value;
        }

        private int GetRate()
        {
            Piece piece = FindPiece(DestinationRow, DestinationColumn);

            if (piece == null)
            {
                Piece currentPiece = FindPiece(StartupRow, StartupColumn);
                if (currentPiece.ChessPiece.Equals(ChessPiece.BlackPawn) && currentPiece.Row == 1 && currentPiece.Column == 5)
                    return 15;
                if (currentPiece.ChessPiece.Equals(ChessPiece.BlackPawn) && currentPiece.Row == 1 && currentPiece.Column == 4)
                    return 12;
                if (currentPiece.ChessPiece.Equals(ChessPiece.BlackKnight) && currentPiece.Row == 0 && (currentPiece.Column == 1 || currentPiece.Column == 6 ));
                    return 10;

                return 1;
            }

            switch (piece.ChessPiece)
            {             
                case ChessPiece.WhiteBishop:
                {
                    return 400;
                }
                case ChessPiece.WhiteKing:
                {
                    return 10000;
                }
                case ChessPiece.WhiteKnight:
                {
                    return 400;
                }
                case ChessPiece.WhitePawn:
                {
                    return 200;
                }
                case ChessPiece.WhiteQueen:
                {
                    return 800;
                }
                case ChessPiece.WhiteRook:
                {
                    return 600;
                }
                default:
                {
                    return 1;
                }
            }
        }

        private Piece FindPiece(int row, int column)
        {
            foreach (Piece piece in ChessBoard.Pieces)
            {
                if (piece.Row == row && piece.Column == column)
                {
                    return piece;
                }
            }

            return null;
        }

        private void InsertIndividuals()
        {
            for (int i = 0; i < Rate; i++)
            {
                RandomPopulation.Add(this);   
            }
        }
    }
}
