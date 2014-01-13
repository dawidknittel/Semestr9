using System;
using System.Collections.Generic;
using System.Windows.Controls;
namespace Chess.Model.BlackPieces
{
    public class BlackRook: BlackPiece
    {
        #region Public Fields

        public bool AlreadyMoved;

        #endregion
        #region Constructors

        private BlackRook()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            King = false;
        }

        public BlackRook(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.BlackRook;
            PieceColor = PieceColor.Black;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;
            AlreadyMoved = true;

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackRookFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.BlackRookFieldLight);
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

            ChessBoard.MoveHorizonthalLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveHorizonthalRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveVerticalDown(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveVerticalUp(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);

            if (GenerateAvailable == true)
            {
                base.GenerateAvailableMovements(AvailableMovements);
                base.SelectPiecesToTake(AvailableToTake);
            }
        }

        #endregion
    }
}
