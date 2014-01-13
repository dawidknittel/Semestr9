using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.BlackPieces
{
    public class BlackKing: BlackPiece
    {
        #region Public Fields

        public bool AlreadyMoved;
        public bool IsChecked;

        #endregion
        #region Constructors

        private BlackKing()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            IsChecked = false;
            AlreadyMoved = false;
            King = true;
        }

        public BlackKing(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.BlackKing;
            PieceColor = PieceColor.Black;

            BlackPiece.kingColumn = columnStart;
            BlackPiece.kingRow = rowStart;
            BlackPiece.kingField = currentGrid;
            BlackPiece.BlackKing = this;
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
                    ChessBoard.MakeCastling(Row, (Int16)(Column + 3), Castling.Short, PieceImage.BlackRookFieldDark, ref AvailableMovements);
                else
                    ChessBoard.MakeCastling(Row, (Int16)(Column + 3), Castling.Short, PieceImage.BlackRookFieldLight, ref AvailableMovements);
            }
            else if (columnDestination - Column == -2)
            {
                if (AlreadyMoved)
                    return false;
                if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
                    ChessBoard.MakeCastling(Row, (Int16)(Column - 4), Castling.Long, PieceImage.BlackRookFieldDark, ref AvailableMovements);
                else
                    ChessBoard.MakeCastling(Row, (Int16)(Column - 4), Castling.Long, PieceImage.BlackRookFieldLight, ref AvailableMovements);
            }

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackKingFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackKingFieldLight);
            }

            if (pieceMoved)
            {
                base.MovePiece();
                StartCounting();
                BlackPiece.kingColumn = columnDestination;
                BlackPiece.kingRow = rowDestination;
                BlackPiece.kingField = currentCell;
            }

            AlreadyMoved = true;
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
                    if (ChessBoard.CheckIfFree((Int16) (Row + 1), (Int16) (Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row + 1), (Int16) (Column + 1)), PieceColor))
                            SaveMovements((Int16) (Row + 1), (Int16) (Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row + 1), (Int16) (Column + 1), PieceColor);
                }

                if (Row + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row + 1), (Int16) (Column)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row + 1), (Int16) (Column)),PieceColor))
                            SaveMovements((Int16) (Row + 1), (Int16) (Column), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row + 1), (Int16) (Column), PieceColor);
                }

                if (Row + 1 < 8 && Column - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row + 1), (Int16) (Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row + 1), (Int16) (Column - 1)), PieceColor))
                            SaveMovements((Int16) (Row + 1), (Int16) (Column - 1), ref list, true, PieceColor);
                    }
                }
                else
                    SaveSelectedToTake((Int16) (Row + 1), (Int16) (Column - 1), PieceColor);

                if (Column - 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row), (Int16) (Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row), (Int16) (Column - 1)),PieceColor))
                            SaveMovements((Int16) (Row), (Int16) (Column - 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row), (Int16) (Column - 1), PieceColor);
                }

                if (Row - 1 >= 0 && Column - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row - 1), (Int16) (Column - 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row - 1), (Int16) (Column - 1)), PieceColor))
                            SaveMovements((Int16) (Row - 1), (Int16) (Column - 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row - 1), (Int16) (Column - 1), PieceColor);
                }

                if (Row - 1 >= 0)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row - 1), (Int16) (Column)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row - 1), (Int16) (Column)),PieceColor))
                            SaveMovements((Int16) (Row - 1), (Int16) (Column), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row - 1), (Int16) (Column), PieceColor);
                }

                if (Row - 1 >= 0 && Column + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row - 1), (Int16) (Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row - 1), (Int16) (Column + 1)), PieceColor))
                            SaveMovements((Int16) (Row - 1), (Int16) (Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row - 1), (Int16) (Column + 1), PieceColor);
                }

                if (Column + 1 < 8)
                {
                    if (ChessBoard.CheckIfFree((Int16) (Row), (Int16) (Column + 1)))
                    {
                        if (!CheckIfFieldIsChecked(ChessBoard.GetChessBoardField((Int16) (Row), (Int16) (Column + 1)),PieceColor))
                            SaveMovements((Int16) (Row), (Int16) (Column + 1), ref list, true, PieceColor);
                    }
                    else
                        SaveSelectedToTake((Int16) (Row), (Int16) (Column + 1), PieceColor);
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
