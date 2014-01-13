using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.BlackPieces
{
    public class BlackKnight: BlackPiece
    {
        #region Constructors

        private BlackKnight()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            King = false;
        }

        public BlackKnight(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.BlackKnight;
            PieceColor = PieceColor.Black;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackKnightFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackKnightFieldLight);
            }

            if (pieceMoved)
            {
                base.MovePiece();
                StartCounting();
                WhitePiece.CheckedFieds.Clear();
                WhitePiece.GetCheckedFields();
                CheckIfKingIsChecked();
            }

            return false;
        }

        public void GenerateAvailableMovements(ref List<ChessBoardField> list, bool GenerateAvailable, bool checkIfKingIsChecked)
        {
            if (GenerateAvailable == true)
            {
                AvailableMovements.Clear();
                AvailableToTake.Clear();
                PossibleIndividuals.Clear();
                CoveredPieces.Clear();
            }

            ChessBoard.MoveKnightSideDownLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightSideDownRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightDownLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightDownRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightSideUpLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightSideUpRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightUpLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveKnightUpRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);

            if (GenerateAvailable == true)
            {
                base.GenerateAvailableMovements(AvailableMovements);
                base.SelectPiecesToTake(AvailableToTake);
            }
        }

        #endregion
    }
}
