using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.WhitePieces
{
    class WhiteBishop: WhitePiece
    {
        #region Constructors

        private WhiteBishop()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            King = false;
        }

        public WhiteBishop(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.WhiteBishop;
            PieceColor = PieceColor.White;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteBishopFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteBishopFieldLight);
            }

            if (pieceMoved)
            {
                base.MovePiece();
                StartCounting();
                BlackPiece.CheckedFieds.Clear();
                BlackPiece.GetCheckedFields();
                CheckIfKingIsChecked();
            }

            if (Player.mode.Equals(GameMode.ComputerVsHuman))
                ArtficalInteligence.StartAlgorithm();

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

            ChessBoard.MoveDiagonalDownLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveDiagonalDownRigth(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveDiagonalUpLeft(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);
            ChessBoard.MoveDiagonalUpRight(this, Row, Column, ref list, GenerateAvailable, checkIfKingIsChecked);

            if (GenerateAvailable == true)
            {
                base.GenerateAvailableMovements(AvailableMovements);
                base.SelectPiecesToTake(AvailableToTake);
            }
        }

        #endregion
    }
}
