using System;
using System.Windows.Controls;

namespace Chess.Model
{
    public class ChessBoardField
    {
        public Int16 row { get; set; }
        public Int16 column { get; set; }
        public Border field { get; set; }
        public PieceColor PieceColor { get; set; }
    }
}
