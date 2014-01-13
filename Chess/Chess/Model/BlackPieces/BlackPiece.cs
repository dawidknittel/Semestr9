using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Chess.MainWindow;
using Chess.Model.BlackPieces;
using Chess.Model.WhitePieces;

namespace Chess.Model
{
    public abstract class BlackPiece: Piece
    {
        public static bool turn { get; set; }

        public static Int16 kingRow { get; set; }
        public static Int16 kingColumn { get; set; }
        public static Border kingField { get; set; }

        public static BlackKing BlackKing;

        public static Piece GetBlackKing
        {
            get
            {
                return (from PIECE in ChessBoard.Pieces where PIECE.ChessPiece.Equals(ChessPiece.BlackKing) select PIECE).FirstOrDefault();
            }
        }

        public static bool check = false;

        public static List<ChessBoardField> CheckedFieds = new List<ChessBoardField>(); 

        public void MovePiece()
        {
            WhitePiece.turn = true;
            BlackPiece.turn = false;
        }

        public void StartCounting()
        {
            Player playerWithBlackPiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.Black select PLAY).FirstOrDefault();
            Player playerWithWhitePiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.White select PLAY).FirstOrDefault();

            playerWithBlackPiece.timer.Stop();
            playerWithWhitePiece.timer.Start();
        }

        public static void GetCheckedFields()
        {
            CheckedFieds.Clear();

            foreach (Piece piece in ChessBoard.Pieces)
            {
                if (piece.PieceColor.Equals(PieceColor.White))
                {
                    switch (piece.ChessPiece)
                    {
                        case ChessPiece.WhiteBishop:
                        {
                            ((WhiteBishop)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                            break;
                        }
                        case ChessPiece.WhiteKnight:
                        {
                            ((WhiteKnight)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                            break;
                        }
                        case ChessPiece.WhitePawn:
                        {
                            ((WhitePawn)piece).GenerateCheckedFields();
                            break;
                        }
                        case ChessPiece.WhiteQueen:
                        {
                            ((WhiteQueen)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                            break;
                        }
                        case ChessPiece.WhiteRook:
                        {
                            ((WhiteRook)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                            break;
                        }
                    }
                }
            }
        }
    }
}
