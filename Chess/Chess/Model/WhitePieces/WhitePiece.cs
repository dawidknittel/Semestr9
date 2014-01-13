using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Chess.MainWindow;
using Chess.Model.BlackPieces;
using Chess.Model.WhitePieces;

namespace Chess.Model
{
    public abstract class WhitePiece: Piece
    {
        public static bool turn { get; set; }

        public static Int16 kingRow { get; set; }
        public static Int16 kingColumn { get; set; }
        public static Border kingField { get; set; }

        public static WhiteKing WhiteKing;

        public static Piece GetWhiteKing 
        {
            get
            {
                return (from PIECE in ChessBoard.Pieces where PIECE.ChessPiece.Equals(ChessPiece.WhiteKing) select PIECE).FirstOrDefault();
            }
        }

        public static bool check = false;

        public static List<ChessBoardField> CheckedFieds = new List<ChessBoardField>(); 

        public void MovePiece()
        {
            WhitePiece.turn = false;
            BlackPiece.turn = true;
        }

        public void StartCounting()
        {
            Player playerWithBlackPiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.Black select PLAY).FirstOrDefault();
            Player playerWithWhitePiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.White select PLAY).FirstOrDefault();

            playerWithWhitePiece.timer.Stop();
            playerWithBlackPiece.timer.Start();          
        }

        public static void GetCheckedFields()
        {
            CheckedFieds.Clear();

            foreach (Piece piece in ChessBoard.Pieces)
            {
                if (piece.PieceColor.Equals(PieceColor.Black))
                {
                    switch (piece.ChessPiece)
                    {
                        case ChessPiece.BlackBishop:
                            {
                                ((BlackBishop)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                                break;
                            }
                        case ChessPiece.BlackKnight:
                            {
                                ((BlackKnight)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                                break;
                            }
                        case ChessPiece.BlackPawn:
                            {
                                ((BlackPawn)piece).GenerateCheckedFields();
                                break;
                            }
                        case ChessPiece.BlackQueen:
                            {
                                ((BlackQueen)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                                break;
                            }
                        case ChessPiece.BlackRook:
                            {
                                ((BlackRook)piece).GenerateAvailableMovements(ref CheckedFieds, false, false);
                                break;
                            }
                    }
                }
            }
        }
    }
}