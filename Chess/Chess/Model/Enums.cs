namespace Chess.Model
{
    enum TextRecognitionEnums
    {
        None,
        PreviousPiece,
        PreviousLetter,
        PreviousDigit,
        PreviousCastling
    }

    public enum PieceImage
    {
        FieldLight, 
        FieldDark,
        BlackPawnFieldLight,
        BlackPawnFieldDark,
        BlackKingFieldLight,
        BlackKingFieldDark,
        BlackBishopFieldLight,
        BlackBishopFieldDark,
        BlackRookFieldLight,
        BlackRookFieldDark,
        BlackKnightFieldLight,
        BlackKnightFieldDark,
        BlackQueenFieldLight,
        BlackQueenFieldDark,
        WhitePawnFieldLight,
        WhitePawnFieldDark,
        WhiteKingFieldLight,
        WhiteKingFieldDark,
        WhiteBishopFieldLight,
        WhiteBishopFieldDark,
        WhiteRookFieldLight,
        WhiteRookFieldDark,
        WhiteKnightFieldLight,
        WhiteKnightFieldDark,
        WhiteQueenFieldLight,
        WhiteQueenFieldDark
    }

    public enum ChessPiece
    {
        BlackPawn,
        BlackRook,
        BlackKnight,
        BlackBishop,
        BlackQueen,
        BlackKing,
        WhitePawn,
        WhiteRook,
        WhiteKnight,
        WhiteBishop,
        WhiteQueen,
        WhiteKing
    }

    public enum FieldColor
    {
        Light,
        Dark
    }

    public enum PieceColor
    {
        White,
        Black,
        None
    }

    public enum PlayerState
    {
        Ready,
        Unready
    }

    public enum GameMode
    {
        ComputerVsHuman,
        HumanVsHuman
    }

    public enum GameModeConfiguration
    {
        Człowiek,
        Komputer
    }

    public enum Castling
    {
        Short,
        Long
    }

    public enum CastlingMode
    {
        Castling_short,
        Castling_long
    }

    public enum WindowExitStatus
    {
        Canceled,
        Ok
    }
}
