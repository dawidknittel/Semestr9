using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.WhitePieces
{
    public class WhiteKing: WhitePiece
    {
        #region Public Fields

        public  bool AlreadyMoved;
        public bool IsChecked;

        #endregion
        #region Constructors

        private WhiteKing()
        {
            Selected = false;
            InGame = true;
            AlreadyMoved = false;
            CurrentField = null;
            DestinationField = null;
            IsChecked = false;
            AlreadyMoved = false;
            King = true;
        }

        public WhiteKing(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.WhiteKing;
            PieceColor = PieceColor.White;

            WhitePiece.kingColumn = columnStart;
            WhitePiece.kingRow = rowStart;
            WhitePiece.kingField = currentGrid;
            WhitePiece.WhiteKing = this;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;

            if (columnDestination - Column == 2)
            {
                if (AlreadyMoved)
                    return false;
                if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
                    ChessBoard.MakeCastling(Row, (Int16)(Column + 3), Castling.Short, PieceImage.WhiteRookFieldDark, ref AvailableMovements);
                else
                    ChessBoard.MakeCastling(Row, (Int16)(Column + 3), Castling.Short, PieceImage.WhiteRookFieldLight, ref AvailableMovements);
            }
            else if (columnDestination - Column == -2)
            {
                if (AlreadyMoved)
                    return false;
                if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
                    ChessBoard.MakeCastling(Row, (Int16)(Column - 4), Castling.Long, PieceImage.WhiteRookFieldDark, ref AvailableMovements);
                else
                    ChessBoard.MakeCastling(Row, (Int16)(Column - 4), Castling.Long, PieceImage.WhiteRookFieldLight, ref AvailableMovements);
            }

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteKingFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteKingFieldLight);
            }

            if (pieceMoved)
            {
                base.MovePiece();
                StartCounting();
                WhitePiece.kingColumn = columnDestination;
                WhitePiece.kingRow = rowDestination;
                WhitePiece.kingField = currentCell;
            }

            AlreadyMoved = true;

            if (Player.mode.Equals(GameMode.ComputerVsHuman))
                ArtficalInteligence.StartAlgorithm();

            return false;
        }

        public void GenerateAvailableMovements(ref List<ChessBoardField> list, bool king, bool checkIfKingIsChecked)
        {
            if (!checkIfKingIsChecked)
            {
                AvailableMovements.Clear();
                AvailableToTake.Clear();
                PossibleIndividuals.Clear();
                CoveredPieces.Clear();

                if (Row + 1 < 8 && Column + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row + 1), (Int16)(Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row + 1), (Int16)(Column + 1)), PieceColor))
                            SaveMovements((Int16)(Row + 1), (Int16)(Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row + 1), (Int16)(Column + 1), PieceColor);
                }

                if (Row + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row + 1), (Int16)(Column)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row + 1), (Int16)(Column)), PieceColor))
                            SaveMovements((Int16)(Row + 1), (Int16)(Column), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row + 1), (Int16)(Column), PieceColor);
                }

                if (Row + 1 < 8 && Column - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row + 1), (Int16)(Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row + 1), (Int16)(Column - 1)), PieceColor))
                            SaveMovements((Int16)(Row + 1), (Int16)(Column - 1), ref list, true, PieceColor);
                    }
                }
                else
                    SaveSelectedToTake((Int16)(Row + 1), (Int16)(Column - 1), PieceColor);

                if (Column - 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row), (Int16)(Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row), (Int16)(Column - 1)), PieceColor))
                            SaveMovements((Int16)(Row), (Int16)(Column - 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row), (Int16)(Column - 1), PieceColor);
                }

                if (Row - 1 >= 0 && Column - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row - 1), (Int16)(Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row - 1), (Int16)(Column - 1)), PieceColor))
                            SaveMovements((Int16)(Row - 1), (Int16)(Column - 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row - 1), (Int16)(Column - 1), PieceColor);
                }

                if (Row - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row - 1), (Int16)(Column)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row - 1), (Int16)(Column)), PieceColor))
                            SaveMovements((Int16)(Row - 1), (Int16)(Column), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row - 1), (Int16)(Column), PieceColor);
                }

                if (Row - 1 >= 0 && Column + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row - 1), (Int16)(Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row - 1), (Int16)(Column + 1)), PieceColor))
                            SaveMovements((Int16)(Row - 1), (Int16)(Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row - 1), (Int16)(Column + 1), PieceColor);
                }

                if (Column + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16)(Row), (Int16)(Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16)(Row), (Int16)(Column + 1)), PieceColor))
                            SaveMovements((Int16)(Row), (Int16)(Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16)(Row), (Int16)(Column + 1), PieceColor);
                }

                ChessBoard.CheckCastlingAvability(this, Row, Column, Castling.Short, ref AvailableMovements, true);
                ChessBoard.CheckCastlingAvability(this, Row, Column, Castling.Long, ref AvailableMovements, true);

                base.GenerateAvailableMovements(AvailableMovements);
                base.SelectPiecesToTake(AvailableToTake);
            }
        }

        #endregion
    }
}
