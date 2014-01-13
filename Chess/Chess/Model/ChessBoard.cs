using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chess.MainWindow;
using Chess.Model.BlackPieces;
using Chess.Model.WhitePieces;

namespace Chess.Model
{
    public static class ChessBoard
    {
        #region Public Fields

        public static List<Piece> Pieces = new List<Piece>();
        public static List<ChessBoardField> Fields = new List<ChessBoardField>(); 

        #endregion
        #region Public Methods

        public static void AddPiece(Piece newPiece)
        {
            Pieces.Add(newPiece);
        }

        public static Border GetField(Int16 row, Int16 column)
        {
            foreach (ChessBoardField field in Fields)
            {
                if (field.column == column && field.row == row)
                    return field.field;
            }

            return null;
        }

        public static ChessBoardField GetChessBoardField(Int16 row, Int16 column)
        {
            foreach (ChessBoardField field in Fields)
            {
                if (field.column == column && field.row == row)
                    return field;
            }

            return null;
        }

        public static void DeSelectAvailable(Piece selectedPiece)
        {
            foreach (ChessBoardField piece in selectedPiece.AvailableMovements)
            {
                piece.field.BorderThickness = new Thickness(0, 0, 0, 0);
            }
            foreach (ChessBoardField piece in selectedPiece.AvailableToTake)
            {
                piece.field.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        public static void DeleteSelection(Border currentCell, Piece selectedPiece)
        {
            if (selectedPiece != null)
            {
                selectedPiece.Selected = false;
                selectedPiece.CurrentField.BorderThickness = new Thickness(0, 0, 0, 0);
                DeSelectAvailable(selectedPiece);
            }
        }

        public static void MakeSelection(Border currentCell)
        {
            bool takePiece = false;

            if (!MainWindowController.PlayerReadyToGame())
            {
                return;
            }           

            Piece foundPiece = Piece.FindPiece(currentCell);

            Piece selectedPiece = (from chessBoardPieces in Pieces
                                       select chessBoardPieces).Where(selected => selected.Selected == true).FirstOrDefault();

            if (selectedPiece != null)
            {
                ChessBoardField field = (from FIELD in selectedPiece.AvailableToTake where FIELD.field.Equals(currentCell) select FIELD).FirstOrDefault();
                if (field != null)
                {
                    foundPiece = null;
                    takePiece = true;
                }
            }

            if (foundPiece != null)
            {
                if (foundPiece.PieceColor.Equals(PieceColor.Black) &&  !BlackPiece.turn)
                {
                    return;
                }

                if (foundPiece.PieceColor.Equals(PieceColor.White) && !WhitePiece.turn)
                {
                    return;
                }

                currentCell.BorderBrush = Brushes.Black;
                currentCell.BorderThickness = new Thickness(Constants.SelectionThickness, Constants.SelectionThickness, Constants.SelectionThickness, Constants.SelectionThickness);              

                DeleteSelection(currentCell, selectedPiece);

                foundPiece.Selected = true;

                GenerateAvailableMovementsPiece(foundPiece);
            }
            else
            {
                if (selectedPiece != null)
                {
                    MakeMoveOnChessBoard(selectedPiece, currentCell, takePiece ? selectedPiece.AvailableToTake : selectedPiece.AvailableMovements);
                }
            }
        }

        public static void GenerateAvailableMovementsPiece(Piece foundPiece)
        {
            switch (foundPiece.ChessPiece)
            {
                case ChessPiece.BlackBishop:
                    {
                        ((BlackBishop)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.BlackKing:
                    {
                        ((BlackKing)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, false, false);
                        break;
                    }
                case ChessPiece.BlackKnight:
                    {
                        ((BlackKnight)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.BlackPawn:
                    {
                        ((BlackPawn)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.BlackQueen:
                    {
                        ((BlackQueen)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.BlackRook:
                    {
                        ((BlackRook)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.WhiteBishop:
                    {
                        ((WhiteBishop)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.WhiteKing:
                    {
                        ((WhiteKing)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, false, false);
                        break;
                    }
                case ChessPiece.WhiteKnight:
                    {
                        ((WhiteKnight)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.WhitePawn:
                    {
                        ((WhitePawn)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.WhiteQueen:
                    {
                        ((WhiteQueen)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
                case ChessPiece.WhiteRook:
                    {
                        ((WhiteRook)foundPiece).GenerateAvailableMovements(ref foundPiece.AvailableMovements, true, false);
                        break;
                    }
            }
        }

        public static void MakeMoveOnChessBoard(Piece selectedPiece, Border currentCell, List<ChessBoardField> AvailableCells)
        {
            Int16 destinationRow = -1;
            Int16 destinationColumn = -1;

            foreach (ChessBoardField piece in AvailableCells)
            {
                if (piece.field.Equals(currentCell))
                {
                    destinationRow = piece.row;
                    destinationColumn = piece.column;

                    switch (selectedPiece.ChessPiece)
                    {
                        case ChessPiece.BlackBishop:
                            {
                                ((BlackBishop)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.BlackKing:
                            {
                                ((BlackKing)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.BlackKnight:
                            {
                                 ((BlackKnight) selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell,PieceImage.FieldLight);
                                 ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.BlackPawn:
                            {
                                ((BlackPawn)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.BlackQueen:
                            {
                                ((BlackQueen)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.BlackRook:
                            {
                                ((BlackRook)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhiteBishop:
                            {
                                ((WhiteBishop)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhiteKing:
                            {
                                ((WhiteKing)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhiteKnight:
                            {
                                ((WhiteKnight)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhitePawn:
                            {
                                ((WhitePawn)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhiteQueen:
                            {
                                ((WhiteQueen)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                        case ChessPiece.WhiteRook:
                            {
                                ((WhiteRook)selectedPiece).MakeMove(destinationRow, destinationColumn, currentCell, PieceImage.FieldLight);
                                ChessBoard.Pieces.Remove((from PIEC in Pieces where PIEC.InGame.Equals(false) select PIEC).FirstOrDefault());
                                return;
                            }
                    }                   
                }
            }
        }

        public static void DestroyPieces()
        {
            foreach (Piece piece in Pieces)
            {
                if (!piece.InGame)
                {
                    ChessBoard.Pieces.Remove(piece);
                    piece.Dispose();
                }
            }
        }

        public static FieldColor FindColorField(Int16 row, Int16 column)
        {
            if (row%2 == 0)
            {
                if (column%2 == 0)
                {
                    return FieldColor.Dark;
                }
                else
                {
                    return FieldColor.Light;
                }
            }
            else
            {
                if (column % 2 == 0)
                {
                    return FieldColor.Light;
                }
                else
                {
                    return FieldColor.Dark;
                }
            }
        }

        public static ImageBrush ChessImage(PieceImage chessImage)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();

            switch (chessImage)
            {
                case PieceImage.BlackBishopFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackBishopFieldDark));
                        break;
                    }
                case PieceImage.BlackBishopFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackBishopFieldLight));
                        break;
                    }
                case PieceImage.BlackKingFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackKingFieldDark));
                        break;
                    }
                case PieceImage.BlackKingFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackKingFieldLight));
                        break;
                    }
                case PieceImage.BlackPawnFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackPawnFieldDark));
                        break;
                    }
                case PieceImage.BlackPawnFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackPawnFieldLight));
                        break;
                    }
                case PieceImage.BlackQueenFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackQueenFieldDark));
                        break;
                    }
                case PieceImage.BlackQueenFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackQueenFieldLight));
                        break;
                    }
                case PieceImage.BlackKnightFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackKnightFieldDark));
                        break;
                    }
                case PieceImage.BlackKnightFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackKnightFieldLight));
                        break;
                    }
                case PieceImage.BlackRookFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackRookFieldDark));
                        break;
                    }
                case PieceImage.BlackRookFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.BlackRookFieldLight));
                        break;
                    }
                case PieceImage.WhiteBishopFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteBishopFieldDark));
                        break;
                    }
                case PieceImage.WhiteBishopFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteBishopFieldLight));
                        break;
                    }
                case PieceImage.WhiteKingFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteKingFieldDark));
                        break;
                    }
                case PieceImage.WhiteKingFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteKingFieldLight));
                        break;
                    }
                case PieceImage.WhitePawnFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhitePawnFieldDark));
                        break;
                    }
                case PieceImage.WhitePawnFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhitePawnFieldLight));
                        break;
                    }
                case PieceImage.WhiteQueenFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteQueenFieldDark));
                        break;
                    }
                case PieceImage.WhiteQueenFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteQueenFieldLight));
                        break;
                    }
                case PieceImage.WhiteKnightFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteKnightFieldDark));
                        break;
                    }
                case PieceImage.WhiteKnightFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteKnightFieldLight));
                        break;
                    }
                case PieceImage.WhiteRookFieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteRookFieldDark));
                        break;
                    }
                case PieceImage.WhiteRookFieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.WhiteRookFieldLight));
                        break;
                    }
                case PieceImage.FieldDark:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.FieldDark));
                        break;
                    }
                case PieceImage.FieldLight:
                    {
                        image.Source = new BitmapImage(new Uri(Constants.FieldLight));
                        break;
                    }
            }

            myBrush.ImageSource = image.Source;

            return myBrush;
        }

        public static bool CheckIfFree(Int16 row, Int16 column)
        {
            foreach (var piece in Pieces)
            {
                if (piece.Row == row && piece.Column == column)
                {
                    return false;
                }
            }

            return true;
        }

        #region Moves

        public static void MoveVerticalDown(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            for (int i = 0; i < 8 - Row; i++)
            {
                if ((Row + (i + 1)) < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row + i + 1), (Int16)(Column)))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements((Int16) (Row + i + 1), (Int16) (Column), ref list, false,
                                piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row + (i + 1), Column, piece.ChessPiece));
                        }
                    }
                    else 
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing((Int16) (Row + i + 1), (Int16) (Column)) &&
                            !checkIfKingIsChecked)
                        {
                            piece.SaveSelectedToTake((Int16) (Row + i + 1), (Int16) (Column), piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece((Int16)(Row + (i + 1)), (Int16)(Column)).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row + (i + 1), Column, piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked((Int16)(Row + i + 1), (Int16)(Column), piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveVerticalUp(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            for (int i = 0; i < 8; i++)
            {
                if ((Row - (i + 1)) >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row - (i + 1)), (Int16)(Column)))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements((Int16) (Row - (i + 1)), (Int16) (Column), ref list, false,
                                piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row - (i + 1), Column, piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing((Int16) (Row - (i + 1)), (Int16) (Column)) &&
                            !checkIfKingIsChecked)
                        {
                            piece.SaveSelectedToTake((Int16) (Row - (i + 1)), (Int16) (Column), piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece((Int16)(Row-(i+1)), (Int16)(Column)).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row - (i + 1), Column, piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked((Int16)(Row + -(i + 1)), (Int16)(Column), piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveHorizonthalRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            for (int i = 0; i < 8; i++)
            {
                if ((Column + (i + 1)) < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row), (Int16)(Column + (i + 1))))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements((Int16) (Row), (Int16) (Column + (i + 1)), ref list, false,
                                piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row, Column + (i + 1), piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing((Int16) (Row), (Int16) (Column + (i + 1))) &&
                            !checkIfKingIsChecked)
                        {
                            piece.SaveSelectedToTake((Int16) (Row), (Int16) (Column + (i + 1)), piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(Row, (Int16)(Column + (i + 1))).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row, Column + (i + 1), piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked((Int16)(Row), (Int16)(Column + (i + 1)), piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveHorizonthalLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            for (int i = 0; i < 8; i++)
            {
                if ((Column - (i + 1)) >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row), (Int16)(Column - (i + 1))))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements((Int16) (Row), (Int16) (Column - (i + 1)), ref list, false,piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row, Column - (i + 1), piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing((Int16) (Row), (Int16) (Column - (i + 1))) &&
                            !checkIfKingIsChecked)
                        {
                            piece.SaveSelectedToTake((Int16) (Row), (Int16) (Column - (i + 1)), piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(Row, (Int16)(Column - (i + 1))).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, Row, Column - (i + 1), piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked((Int16)(Row - (i + 1)), (Int16)(Column - (i + 1)), piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveDiagonalDownLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            for (int i = 0; i < 8 - Row; i++)
            {
                rowDestination = (Int16)(Row + (i + 1));
                columnDestination = (Int16)(Column - (i + 1));

                if (rowDestination < 8 && columnDestination >= 0)
                {
                    if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination) && !checkIfKingIsChecked)
                        {
                            piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                        break;
                    }
                    
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveDiagonalDownRigth(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            for (int i = 0; i < 8 - Row; i++)
            {
                rowDestination = (Int16)(Row + (i + 1));
                columnDestination = (Int16)(Column + (i + 1));

                if (rowDestination < 8 && columnDestination < 8)
                {
                    if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                        {
                            piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveDiagonalUpLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            for (int i = 0; i < 8; i++)
            {
                rowDestination = (Int16)(Row - (i + 1));
                columnDestination = (Int16)(Column - (i + 1));

                if (rowDestination >= 0 && columnDestination >= 0)
                {
                    if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                        {
                            piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }

                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveDiagonalUpRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            for (int i = 0; i < 8; i++)
            {
                rowDestination = (Int16)(Row - (i + 1));
                columnDestination = (Int16)(Column + (i + 1));

                if (rowDestination >= 0 && columnDestination < 8)
                {
                    if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                    {
                        if (!checkIfKingIsChecked)
                        {
                            piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination,
                                columnDestination, piece.ChessPiece));
                        }
                    }
                    else
                    {
                        if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                        {
                            piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                            if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                                piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                        }
                        if (checkIfKingIsChecked)
                            CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static void MoveKnightDownLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row + 2);
            columnDestination = (Int16)(Column + 1);

            if (rowDestination < 8 && columnDestination < 8)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void MoveKnightDownRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row + 2);
            columnDestination = (Int16)(Column - 1);

            if (rowDestination < 8 && columnDestination >=0)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void MoveKnightSideDownLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row + 1);
            columnDestination = (Int16)(Column + 2);

            if (rowDestination < 8 && columnDestination < 8)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }


        public static void MoveKnightSideDownRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row + 1);
            columnDestination = (Int16)(Column - 2);

            if (rowDestination < 8 && columnDestination >= 0)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void MoveKnightUpLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row - 2);
            columnDestination = (Int16)(Column - 1);

            if (rowDestination < 8 && columnDestination < 8 && columnDestination >= 0)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void MoveKnightUpRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row - 2);
            columnDestination = (Int16)(Column + 1);

            if (rowDestination < 8 && columnDestination >= 0 && columnDestination < 8)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void MoveKnightSideUpLeft(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row - 1);
            columnDestination = (Int16)(Column - 2);

            if (rowDestination < 8 && columnDestination < 8 && columnDestination >= 0)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }


        public static void MoveKnightSideUpRight(Piece piece, Int16 Row, Int16 Column, ref List<ChessBoardField> list, bool SelectedToTake, bool checkIfKingIsChecked)
        {
            Int16 rowDestination = -1;
            Int16 columnDestination = -1;

            rowDestination = (Int16)(Row - 1);
            columnDestination = (Int16)(Column + 2);

            if (rowDestination < 8 && columnDestination >= 0 && columnDestination <8)
            {
                if (ChessBoard.CheckIfFree(rowDestination, columnDestination))
                {
                    if (!checkIfKingIsChecked)
                    {
                        piece.SaveMovements(rowDestination, columnDestination, ref list, false, piece.PieceColor);
                        piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                }
                else
                {
                    if (SelectedToTake && !CheckIfPieceIsKing(rowDestination, columnDestination))
                    {
                        piece.SaveSelectedToTake(rowDestination, columnDestination, piece.PieceColor);
                        if (!piece.PieceColor.Equals(Piece.FindPiece(rowDestination, columnDestination).PieceColor))
                            piece.PossibleIndividuals.Add(new Individual(piece.Row, piece.Column, rowDestination, columnDestination, piece.ChessPiece));
                    }
                    if (checkIfKingIsChecked)
                        CheckIfKingIsChecked(rowDestination, columnDestination, piece.PieceColor);
                }
            }
        }

        public static void CheckCastlingAvability(Piece piece, Int16 Row, Int16 Column, Castling castling, ref List<ChessBoardField> list, bool SelectedToTake)
        {
            Int16 rowDestination = Row;
            int positiveCheckedFields = 0;

            if (piece.PieceColor.Equals(PieceColor.Black))
            {
                if (((BlackKing)piece).IsChecked == true || ((BlackKing)piece).AlreadyMoved == true)
                    return;
            }
            else 
            {
                if (((WhiteKing)piece).IsChecked == true || ((WhiteKing)piece).AlreadyMoved == true)
                    return;
            }

            if (castling.Equals(Castling.Long))
            {
                for (int i = 0; i < 3; i++)
                    if (ChessBoard.CheckIfFree(rowDestination, (Int16)(Column - (i + 1))))
                        positiveCheckedFields++;
                if (positiveCheckedFields == 3 )
                {
                    for (int i = 0; i < 2; i++)
                        if (!piece.CheckIfFieldIsChecked(GetChessBoardField(rowDestination, (Int16)(Column - (i + 1))), piece.PieceColor))
                            piece.SaveMovements(rowDestination, (Int16)(Column - (i + 1)), ref list, false, piece.PieceColor);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                    if (ChessBoard.CheckIfFree(rowDestination, (Int16)(Column + (i + 1))))
                        positiveCheckedFields++;
                if (positiveCheckedFields == 2)
                {
                    for (int i = 0; i < 2; i++)
                        if (!piece.CheckIfFieldIsChecked(GetChessBoardField(rowDestination, (Int16)(Column + (i + 1))), piece.PieceColor))
                            piece.SaveMovements(rowDestination, (Int16)(Column + (i + 1)), ref list, false, piece.PieceColor);
                }
            }
        }

        public static void MakeCastling(Int16 row, Int16 column, Castling castling, PieceImage pieceImage, ref List<ChessBoardField> list)
        {
            Piece foundPiece = Piece.FindPiece(row, column);

            if (foundPiece != null)
            {
                foundPiece.AvailableMovements.Clear();
                
                if (castling.Equals(Castling.Long))
                {
                    foundPiece.SaveMovements(row, (Int16)(column + 3), ref foundPiece.AvailableMovements, false, foundPiece.PieceColor);
                    foundPiece.MakeMove(row, (Int16) (column + 3), GetField(row, (Int16)(column+3)), pieceImage);
                }
                else
                {
                    foundPiece.SaveMovements(row, (Int16)(column - 2), ref foundPiece.AvailableMovements, false, foundPiece.PieceColor);
                    foundPiece.MakeMove(row, (Int16)(column - 2), GetField(row, (Int16)(column-2)), pieceImage);
                }
            }
        }

        public static bool CheckIfPieceIsKing(Int16 row, Int16 column)
        {
            Piece foundPiece = Piece.FindPiece(row, column);

            if (foundPiece.King)
                return true;
            else
                return false;
        }

        public static void CheckIfKingIsChecked(Int16 row, Int16 column, PieceColor pieceColor)
        {
                Piece foundPiece = Piece.FindPiece(row, column);
                if (foundPiece != null && pieceColor != foundPiece.PieceColor)
                {
                    if (foundPiece.PieceColor.Equals(PieceColor.Black) && foundPiece is BlackKing)
                    {
                        ((BlackKing) foundPiece).IsChecked = true;
                        CheckIfCheckMate(foundPiece);
                        foundPiece.CurrentField.BorderThickness = new Thickness(0, 5, 0, 0);
                        foundPiece.CurrentField.BorderBrush = Brushes.Crimson;
                    }
                    else if (foundPiece.PieceColor.Equals(PieceColor.White) && foundPiece is WhiteKing)
                    {
                        ((WhiteKing) foundPiece).IsChecked = true;
                        CheckIfCheckMate(foundPiece);
                        foundPiece.CurrentField.BorderThickness = new Thickness(0, 5, 0, 0);
                        foundPiece.CurrentField.BorderBrush = Brushes.Crimson;
                    }
                }
        }

        public static void CheckIfCheckMate(Piece piece)
        {
            string WinnerPieceColor = string.Empty;

            if (piece.PieceColor.Equals(PieceColor.Black))
            {
                ((BlackKing) piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                WinnerPieceColor = "białe";
            }
            else if (piece.PieceColor.Equals(PieceColor.White))
            {
                ((WhiteKing) piece).GenerateAvailableMovements(ref piece.AvailableMovements, true, false);
                WinnerPieceColor = "czarne";
            }

            ChessBoard.DeSelectAvailable(piece);

            if (piece.AvailableMovements.Count == 0)
            {
                piece.StopCounting();
                GenerateAvailableMovementsAllPiece(piece.PieceColor);

                foreach (ChessBoardField field in piece.AvailableToTake)
                {
                    if (piece.PieceColor.Equals(PieceColor.Black))
                    {
                        if (CheckCovered(PieceColor.White, field))
                        {
                            ShowEndMessage(WinnerPieceColor);
                            return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        if (CheckCovered(PieceColor.Black, field))
                        {
                            ShowEndMessage(WinnerPieceColor);
                            return;
                        }
                        else
                            return;
                    }
                }      

                ShowEndMessage(WinnerPieceColor);
            }
        }

        public static void ShowEndMessage(string WinnerPieceColor)
        {
            MessageBox.Show(string.Format("Wygrały {0}", WinnerPieceColor), "Koniec gry", MessageBoxButton.OK, MessageBoxImage.Information);                
        }

        public static void GenerateAvailableMovementsAllPiece(PieceColor pieceColor)
        {
            foreach (Piece piece in Pieces)
            {
                piece.Selected = true;
                GenerateAvailableMovementsPiece(piece);              
                piece.Selected = false;
                DeSelectAvailable(piece);
            }
        }

        public static bool CheckCovered(PieceColor pieceColor, ChessBoardField chessToTake)
        {
            foreach (Piece piece in Pieces)
            {
                //if (pieceColor.Equals(PieceColor.Black))
                //{
                    foreach (ChessBoardField field in piece.CoveredPieces)
                    {
                        if (field.Equals(chessToTake))
                            return true;
                    }
                //}
            }

            return false;
        }

        public static List<Piece> CreateCopyOfPiece()
        {
            return Pieces.ToList();
        }

        #endregion

        #endregion
    }

}
