using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.MainWindow;
using Chess.Model.BlackPieces;
using Chess.Model.WhitePieces;

namespace Chess.Model
{
    public abstract class Piece: IDisposable
    {
        #region Public Properties

        public bool Selected { get; set; }
        public bool InGame { set; get; }
        public bool King { get; set; }

        public Int16 Row { get; set; }
        public Int16 Column { get; set; }

        public Border CurrentField { get; set; }
        public Border DestinationField { get; set; }

        public List<ChessBoardField> AvailableMovements = new List<ChessBoardField>();
        public List<ChessBoardField> AvailableToTake = new List<ChessBoardField>();
        public List<ChessBoardField> CoveredPieces = new List<ChessBoardField>();
        public List<Individual> PossibleIndividuals = new List<Individual>(); 

        public ChessPiece ChessPiece { get; set; }
        public FieldColor FieldColorProperty { get; set; }
        public PieceColor PieceColor { get; set; }

        #endregion
        #region Private Fields

        protected ImageBrush fieldDark
        {
            get
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Constants.FieldDark));
                myBrush.ImageSource = image.Source;

                return myBrush;
            }
        }

        protected ImageBrush fieldLight
        {
            get
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Constants.FieldLight));
                myBrush.ImageSource = image.Source;

                return myBrush;
            }
        }

        #endregion
        #region Static Methods

        public static Piece FindPiece(Border chessBoardCell)
        {
            foreach (Piece piece in ChessBoard.Pieces)
            {
                if (chessBoardCell.Equals(piece.CurrentField))
                {
                    return piece;
                }
            }

            return null;
        }

        public static Piece FindPiece(Int16 row, Int16 column)
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

        #endregion
        #region Public Methods

        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public void GenerateAvailableMovements(List<ChessBoardField> list)
        {
            foreach (ChessBoardField border in list)
            {
                border.field.BorderBrush = Brushes.LightGreen;
                border.field.BorderThickness = new Thickness(2, 2, 2, 2);
            }
        }

        public void SelectPiecesToTake(List<ChessBoardField> list)
        {
            foreach (ChessBoardField border in list)
            {
                border.field.BorderBrush = Brushes.Red;
                border.field.BorderThickness = new Thickness(2, 2, 2, 2);
            }
        }

        public void SaveMovements(Int16 row, Int16 column, ref List<ChessBoardField> list, bool king, PieceColor pieceColor)
        {
            foreach (ChessBoardField field in ChessBoard.Fields)
            {
                if (field.column == column && field.row == row)
                {
                    if (king)
                    {
                        if (CheckIfFieldIsChecked(field, pieceColor))
                            continue;
                        else
                            list.Add(field);
                    }
                    else
                    {
                        if (CheckUniqueChessBoardField(list, field))
                            list.Add(field);
                    }
                }
            }
        }

        public bool CheckUniqueChessBoardField(List<ChessBoardField> list, ChessBoardField field)
        {
            ChessBoardField foundField = (from FIELD in list where FIELD.field.Equals(field.field) select FIELD).FirstOrDefault();

            if (foundField == null)
                return true;
            else
                return false;
        }

        public bool CheckIfFieldIsChecked(ChessBoardField field, PieceColor pieceColor)
        {
            List<ChessBoardField> list = pieceColor.Equals(PieceColor.Black) ? BlackPiece.CheckedFieds : WhitePiece.CheckedFieds;
            ChessBoardField checkedField = (from FIELD in list where FIELD.field.Equals(field.field) select FIELD).FirstOrDefault();

            if (checkedField == null)
                return false;

            return true;
        }

        public void SaveSelectedToTake(Int16 row, Int16 column, PieceColor pieceColor)
        {
            foreach (ChessBoardField field in ChessBoard.Fields)
            {
                if (field.column == column && field.row == row)
                {
                    if (pieceColor != field.PieceColor)
                        AvailableToTake.Add(field);
                    else
                        CoveredPieces.Add(field);
                }
            }
        }

        public void UpdateChessFields(Int16 row, Int16 column, PieceColor pieceColore)
        {
            foreach (ChessBoardField field in ChessBoard.Fields)
            {
                if (field.column == column && field.row == row)
                {
                    field.PieceColor = PieceColor;
                }
            }
        }

        public virtual bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage pieceImage)
        {
            ChessBoardField chessfield = (from available in this.AvailableMovements
                                          select available).Where(chessField => chessField.field == currentCell).FirstOrDefault();

            if (chessfield == null)
            {
                chessfield = (from available in this.AvailableToTake
                              select available).Where(chessField => chessField.field == currentCell).FirstOrDefault();
                Piece foundPiece = Piece.FindPiece(currentCell);
                foundPiece.InGame = false;
            }

            if (chessfield != null)
            {
                if (ChessBoard.FindColorField(Row, Column).Equals(FieldColor.Dark))
                {
                    CurrentField.Background = fieldDark;
                }
                else
                {
                    CurrentField.Background = fieldLight;
                }

                UpdateChessFields(Row, Column, PieceColor.None);

                ChessBoard.DeleteSelection(CurrentField, this);
                Row = rowDestination;
                Column = columnDestination;
                CurrentField = currentCell;

                UpdateChessFields(Row, Column, PieceColor.White);

                CurrentField.Background = ChessBoard.ChessImage(pieceImage);

                Selected = false;

                Piece king = PieceColor.Equals(PieceColor.Black) ? BlackPiece.GetBlackKing : WhitePiece.GetWhiteKing;

                if (king.PieceColor.Equals(PieceColor.Black))
                    ((BlackKing)king).IsChecked = false;
                else
                    ((WhiteKing)king).IsChecked = false;

               
                ChessBoard.DeleteSelection(king.CurrentField, king);

                return true;
            }

            return false;
        }

        private bool CheckIfMoveIsAvailable(Int16 destinationRow, Int16 destinationColumn)
        {
            Int16 startupRow = Row;
            Int16 startupColumn = Column;

            Row = destinationRow;
            Column = destinationColumn;

            if (this.PieceColor.Equals(PieceColor.White))
                BlackPiece.GetCheckedFields();
            else
                WhitePiece.GetCheckedFields();

            CheckIfKingIsChecked();

            if (WhitePiece.WhiteKing.IsChecked == true || BlackPiece.BlackKing.IsChecked == true)
            {
                BackToDefaultParameters(startupRow, startupColumn);
                return true;
            }
            else
            {
                BackToDefaultParameters(startupRow, startupColumn);
                return false;
            }
        }

        private void BackToDefaultParameters(Int16 startupRow, Int16 startupColumn)
        {
            Row = startupRow;
            Column = startupColumn;

            if (this.PieceColor.Equals(PieceColor.White))
                BlackPiece.GetCheckedFields();
            else
                WhitePiece.GetCheckedFields();

            WhitePiece.WhiteKing.IsChecked = false;
            BlackPiece.BlackKing.IsChecked = false;
        }

        public void CheckIfKingIsChecked()
        {
            foreach (Piece piece in ChessBoard.Pieces)
            {
                    switch (piece.ChessPiece)
                    {
                        case ChessPiece.BlackBishop:
                        {
                            ((BlackBishop)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.BlackKing:
                        {
                            ((BlackKing)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.BlackKnight:
                        {
                            ((BlackKnight)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.BlackPawn:
                        {
                            ((BlackPawn)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.BlackQueen:
                        {
                            ((BlackQueen)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.BlackRook:
                        {
                            ((BlackRook)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhiteBishop:
                        {
                            ((WhiteBishop)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhiteKing:
                        {
                            ((WhiteKing)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhiteKnight:
                        {
                            ((WhiteKnight)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhitePawn:
                        {
                            ((WhitePawn)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhiteQueen:
                        {
                            ((WhiteQueen)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                        case ChessPiece.WhiteRook:
                        {
                            ((WhiteRook)piece).GenerateAvailableMovements(ref piece.AvailableMovements, false, true);
                            break;
                        }
                    }
                    ChessBoard.DeSelectAvailable(piece);
            }
        }

        public void StopCounting()
        {
            Player playerWithBlackPiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.Black select PLAY).FirstOrDefault();
            Player playerWithWhitePiece = (from PLAY in MainWindowController.players where PLAY.PieceColor == PieceColor.White select PLAY).FirstOrDefault();

            playerWithWhitePiece.timer.Stop();
            playerWithBlackPiece.timer.Stop();
        }

        #endregion
    }
}
