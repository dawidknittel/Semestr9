using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.WhitePieces
{
    public class WhiteKnight: WhitePiece
    {
        #region Constructors

        private WhiteKnight()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            King = false;
        }

        public WhiteKnight(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.WhiteKnight;
            PieceColor = PieceColor.White;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteKnightFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhiteKnightFieldLight);
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
