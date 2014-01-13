using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Chess.Model.WhitePieces
{
    public class WhitePawn: WhitePiece
    {
        #region Constructors

        private WhitePawn()
        {
            Selected = false;
            InGame = true;
            CurrentField = null;
            DestinationField = null;
            King = false;
        }

        public WhitePawn(Int16 rowStart, Int16 columnStart, Border currentGrid)
            : this()
        {
            Row = rowStart;
            Column = columnStart;
            CurrentField = currentGrid;
            ChessPiece = ChessPiece.WhitePawn;
            PieceColor = PieceColor.White;
        }

        #endregion
        #region Methods

        public override bool MakeMove(Int16 rowDestination, Int16 columnDestination, Border currentCell, PieceImage image)
        {
            bool pieceMoved = false;

            if (ChessBoard.FindColorField(rowDestination, columnDestination).Equals(FieldColor.Dark))
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhitePawnFieldDark);
            }
            else
            {
                pieceMoved = base.MakeMove(rowDestination, columnDestination, currentCell, PieceImage.WhitePawnFieldLight);
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

           if (Row == 6)
           {
               for (int i = 0; i < 2; i++)
               {
                   if (ChessBoard.CheckIfFree((Int16)(Row - (i + 1)), Column))
                   {
                       if (!checkIfKingIsChecked)
                       {
                           SaveMovements((Int16) (Row - (i + 1)), Column, ref list, false, PieceColor);
                           PossibleIndividuals.Add(new Individual(Row, Column, (Int16)(Row - (i + 1)), Column, ChessPiece));
                       }

                   }
                   else
                   {
                       break;
                   }
               }
           }
           else
           {
               if (ChessBoard.CheckIfFree((Int16)(Row - 1), Column))
               {
                   if (!checkIfKingIsChecked)
                   {
                       SaveMovements((Int16) (Row - 1), Column, ref list, false, PieceColor);
                       PossibleIndividuals.Add(new Individual(Row, Column, (Int16)(Row - 1), Column, ChessPiece));
                   }
               }             
           }
            if (!checkIfKingIsChecked)
            {
                if ((Row - 1) < 8 && (Column + 1) < 8 &&
                    !ChessBoard.CheckIfFree((Int16) (Row - 1), (Int16) (Column + 1)))
                {
                    SaveSelectedToTake((Int16) (Row - 1), (Int16) (Column + 1), PieceColor.White);
                    PossibleIndividuals.Add(new Individual(Row, Column, (Int16)(Row - 1), Column + 1, ChessPiece));
                }

                if ((Row - 1) < 8 && (Column - 1) < 8 &&
                    !ChessBoard.CheckIfFree((Int16) (Row - 1), (Int16) (Column - 1)))
                {
                    SaveSelectedToTake((Int16) (Row - 1), (Int16) (Column - 1), PieceColor.White);
                    PossibleIndividuals.Add(new Individual(Row, Column, (Int16)(Row - 1), Column - 1, ChessPiece));
                }

                if (GenerateAvailable == true)
                {
                    base.GenerateAvailableMovements(AvailableMovements);
                    base.SelectPiecesToTake(AvailableToTake);
                }
            }
       }

        public void GenerateCheckedFields()
        {
            if ((Row - 1) < 8 && (Column + 1) < 8 && ChessBoard.CheckIfFree((Int16)(Row - 1), (Int16)(Column + 1)))
            {
                SaveMovements((Int16)(Row - 1), (Int16)(Column + 1), ref BlackPiece.CheckedFieds, false, PieceColor.White);
            }

            if ((Row - 1) < 8 && (Column - 1) >= 0 && ChessBoard.CheckIfFree((Int16)(Row - 1), (Int16)(Column - 1)))
            {
                SaveMovements((Int16)(Row - 1), (Int16)(Column - 1), ref BlackPiece.CheckedFieds, false, PieceColor.White);
            }
        }

        #endregion
    }
}
