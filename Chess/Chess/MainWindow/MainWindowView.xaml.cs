using System;
using System.Speech.Recognition;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.ConfigurationWindow;
using Chess.Model;
using Chess.Model.BlackPieces;
using Chess.Model.WhitePieces;
using Label = System.Windows.Controls.Label;

namespace Chess.MainWindow
{
    public partial class MainWindowView : Window
    {
        #region Private Fields

        private MainWindowController Controller = null;

        #endregion
        #region Grammar

        private Choices choices = new Choices();
        private GrammarBuilder GrammarBuilder = null;
        private Grammar Grammar = null;
        private int? indexStart = null;
        private int? indexEnd = null;
        private string lastString = string.Empty;
        private TextRecognitionEnums previous = TextRecognitionEnums.None;
        private int parametersCounter = 0;
        private int coordinatesCounter = 0;

        #endregion
        #region SpeechRecognition

        SpeechRecognizer speechRecognition = new SpeechRecognizer();

        #endregion
        #region Constructors

        public MainWindowView()
        {
            InitializeComponent();

            speechRecognition.SpeechRecognized += rec_SpeechRecognized;
            InitializeGrammar();
            speechRecognition.LoadGrammar(Grammar);

            Controller = new MainWindowController(this);
            InitializeChessBoard();
            EnableRecognitionControls(false);

            Player.mode = GameMode.HumanVsHuman;
            LoadPieces();
        }

        #endregion
        #region Private Methods

        private void LoadPieces()
        {
            comboBoxPiece.Items.Add(Constants.ROOK);
            comboBoxPiece.Items.Add(Constants.PAWN);
            comboBoxPiece.Items.Add(Constants.QUEEN);
            comboBoxPiece.Items.Add(Constants.KNIGHT);
            comboBoxPiece.Items.Add(Constants.KING);
            comboBoxPiece.Items.Add(Constants.BISHOP);
            comboBoxPiece.Items.Add(Constants.CASTLE_LONG);
            comboBoxPiece.Items.Add(Constants.CASTLE_SHORT);
        }

        private void InitializeGrammar()
        {
            for (int i = 1; i <= 8; i++)
                choices.Add(i.ToString());

            choices.Add(Constants.A);
            choices.Add(Constants.B);
            choices.Add(Constants.C);
            choices.Add(Constants.D);
            choices.Add(Constants.E);
            choices.Add(Constants.F);
            choices.Add(Constants.G);
            choices.Add(Constants.H);

            choices.Add(Constants.ROOK);
            choices.Add(Constants.PAWN);
            choices.Add(Constants.QUEEN);
            choices.Add(Constants.KNIGHT);
            choices.Add(Constants.KING);
            choices.Add(Constants.BISHOP);

            choices.Add(Constants.CASTLE_LONG);
            choices.Add(Constants.CASTLE_SHORT);

            GrammarBuilder = new GrammarBuilder(choices);
            Grammar = new Grammar(GrammarBuilder);
        }

        private void InitializeChessBoard()
        {
            for (int i = 0, counter=0; i < chessBoardGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < chessBoardGrid.ColumnDefinitions.Count; j++)
                {
                    ChessBoardField fieldData = new ChessBoardField();
                    fieldData.field = WPFControls.GetChildOfType<Border>(chessBoardGrid, counter++);
                    fieldData.row = (Int16)i;
                    fieldData.column = (Int16)j;

                    if (i == 0 || i == 1)
                    {
                        fieldData.PieceColor = PieceColor.Black;
                    }
                    else if (i == 7 || i == 8)
                    {
                         fieldData.PieceColor = PieceColor.White;
                    }
                        
                    ChessBoard.Fields.Add(fieldData);
                }
            }
        }

        private void LoadBlackPieces()
        {
            Controller.LoadImage(GridField00, PieceImage.BlackRookFieldDark);
            ChessBoard.AddPiece(new BlackRook(0, 0, GridField00));

            Controller.LoadImage(GridField01, PieceImage.BlackKnightFieldLight);
            ChessBoard.AddPiece(new BlackKnight(0, 1, GridField01));

            Controller.LoadImage(GridField02, PieceImage.BlackBishopFieldDark);
            ChessBoard.AddPiece(new BlackBishop(0, 2, GridField02));

            Controller.LoadImage(GridField03, PieceImage.BlackQueenFieldLight);
            ChessBoard.AddPiece(new BlackQueen(0, 3, GridField03));

            Controller.LoadImage(GridField04, PieceImage.BlackKingFieldDark);
            ChessBoard.AddPiece(new BlackKing(0, 4, GridField04));

            Controller.LoadImage(GridField05, PieceImage.BlackBishopFieldLight);
            ChessBoard.AddPiece(new BlackBishop(0, 5, GridField05));

            Controller.LoadImage(GridField06, PieceImage.BlackKnightFieldDark);
            ChessBoard.AddPiece(new BlackKnight(0, 6, GridField06));

            Controller.LoadImage(GridField07, PieceImage.BlackRookFieldLight);
            ChessBoard.AddPiece(new BlackRook(0, 7, GridField07));

            Controller.LoadImage(GridField10, PieceImage.BlackPawnFieldLight);
            ChessBoard.AddPiece(new BlackPawn(1, 0, GridField10));

            Controller.LoadImage(GridField11, PieceImage.BlackPawnFieldDark);
            ChessBoard.AddPiece(new BlackPawn(1, 1, GridField11));

            Controller.LoadImage(GridField12, PieceImage.BlackPawnFieldLight);
            ChessBoard.AddPiece(new BlackPawn(1, 2, GridField12));

            Controller.LoadImage(GridField13, PieceImage.BlackPawnFieldDark);
            ChessBoard.AddPiece(new BlackPawn(1, 3, GridField13));

            Controller.LoadImage(GridField14, PieceImage.BlackPawnFieldLight);
            ChessBoard.AddPiece(new BlackPawn(1, 4, GridField14));

            Controller.LoadImage(GridField15, PieceImage.BlackPawnFieldDark);
            ChessBoard.AddPiece(new BlackPawn(1, 5, GridField15));

            Controller.LoadImage(GridField16, PieceImage.BlackPawnFieldLight);
            ChessBoard.AddPiece(new BlackPawn(1, 6, GridField16));

            Controller.LoadImage(GridField17, PieceImage.BlackPawnFieldDark);
            ChessBoard.AddPiece(new BlackPawn(1, 7, GridField17));
        }

        private void LoadWhitePieces()
        {
            Controller.LoadImage(GridField60, PieceImage.WhitePawnFieldDark);
            ChessBoard.AddPiece(new WhitePawn(6, 0, GridField60));

            Controller.LoadImage(GridField61, PieceImage.WhitePawnFieldLight);
            ChessBoard.AddPiece(new WhitePawn(6, 1, GridField61));

            Controller.LoadImage(GridField62, PieceImage.WhitePawnFieldDark);
            ChessBoard.AddPiece(new WhitePawn(6, 2, GridField62));

            Controller.LoadImage(GridField63, PieceImage.WhitePawnFieldLight);
            ChessBoard.AddPiece(new WhitePawn(6, 3, GridField63));

            Controller.LoadImage(GridField64, PieceImage.WhitePawnFieldDark);
            ChessBoard.AddPiece(new WhitePawn(6, 4, GridField64));

            Controller.LoadImage(GridField65, PieceImage.WhitePawnFieldLight);
            ChessBoard.AddPiece(new WhitePawn(6, 5, GridField65));

            Controller.LoadImage(GridField66, PieceImage.WhitePawnFieldDark);
            ChessBoard.AddPiece(new WhitePawn(6, 6, GridField66));

            Controller.LoadImage(GridField67, PieceImage.WhitePawnFieldLight);
            ChessBoard.AddPiece(new WhitePawn(6, 7, GridField67));

            Controller.LoadImage(GridField70, PieceImage.WhiteRookFieldLight);
            ChessBoard.AddPiece(new WhiteRook(7, 0, GridField70));

            Controller.LoadImage(GridField71, PieceImage.WhiteKnightFieldDark);
            ChessBoard.AddPiece(new WhiteKnight(7, 1, GridField71));

            Controller.LoadImage(GridField72, PieceImage.WhiteBishopFieldLight);
            ChessBoard.AddPiece(new WhiteBishop(7, 2, GridField72));

            Controller.LoadImage(GridField73, PieceImage.WhiteQueenFieldDark);
            ChessBoard.AddPiece(new WhiteQueen(7, 3, GridField73));

            Controller.LoadImage(GridField74, PieceImage.WhiteKingFieldLight);
            ChessBoard.AddPiece(new WhiteKing(7, 4, GridField74));

            Controller.LoadImage(GridField75, PieceImage.WhiteBishopFieldDark);
            ChessBoard.AddPiece(new WhiteBishop(7, 5, GridField75));

            Controller.LoadImage(GridField76, PieceImage.WhiteKnightFieldLight);
            ChessBoard.AddPiece(new WhiteKnight(7, 6, GridField76));

            Controller.LoadImage(GridField77, PieceImage.WhiteRookFieldDark);
            ChessBoard.AddPiece(new WhiteRook(7, 7, GridField77));
        }

        private void labelResultTextChanged()
        {
            string move = labelResult.Content.ToString();

            if (!string.IsNullOrEmpty(move))
            {
                CheckMove(move.Substring((int)indexStart, (int)(indexEnd - indexStart)));
            }
        }

        private void CheckMove(string move)
        {
            if (previous.Equals(TextRecognitionEnums.PreviousDigit))
            {
                if (move.Length == 1)
                {
                    if (IsLetter(move))
                    {
                        previous = TextRecognitionEnums.PreviousLetter;
                        parametersCounter++;
                        coordinatesCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Content = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                else if (move.Length > 3)
                {
                    if (CheckIfFirstIsPiece(move) && parametersCounter < 4 && !(move.Equals(Constants.CASTLE_LONG) || move.Equals(Constants.CASTLE_SHORT)))
                    {
                        previous = TextRecognitionEnums.PreviousPiece;
                        labelChosenPiece.Content = move;
                        parametersCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Content = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                return;
            }

            if (previous.Equals(TextRecognitionEnums.PreviousLetter))
            {
                if (move.Length == 1)
                {
                    if (IsDigit(move))
                    {
                        previous = TextRecognitionEnums.PreviousDigit;
                        parametersCounter++;
                        coordinatesCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Content = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                else if (move.Length > 3 && parametersCounter < 4 && !(move.Equals(Constants.CASTLE_LONG) || move.Equals(Constants.CASTLE_SHORT)))
                {
                    if (CheckIfFirstIsPiece(move))
                    {
                        previous = TextRecognitionEnums.PreviousPiece;
                        labelChosenPiece.Content = move;
                        parametersCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Content = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                return;
            }

            if (previous.Equals(TextRecognitionEnums.PreviousPiece))
            {
                if (move.Length == 1)
                {
                    if (IsDigit(move))
                    {
                        previous = TextRecognitionEnums.PreviousDigit;
                        parametersCounter++;
                        coordinatesCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else if (IsLetter(move))
                    {
                        previous = TextRecognitionEnums.PreviousLetter;
                        parametersCounter++;
                        coordinatesCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    return;
                }

                if (move.Length > 1)
                {
                    labelResult.Content = lastString;
                    textBoxMove.Text = lastString;
                    textBoxMove.Select(textBoxMove.Text.Length, 0);
                }
                return;
            }

            if (previous.Equals(TextRecognitionEnums.PreviousCastling))
            {
                labelResult.Content = lastString;
                textBoxMove.Text = lastString;
                textBoxMove.Select(textBoxMove.Text.Length, 0);
                return;
            }

            if (previous.Equals(TextRecognitionEnums.None))
            {
                if (CheckIfFirstIsPiece(move))
                {
                    previous = TextRecognitionEnums.PreviousPiece;
                    labelChosenPiece.Content = move;
                }
                else if (CheckIfCastling(move))
                {
                    previous = TextRecognitionEnums.PreviousCastling;
                    labelChosenPiece.Content = move;
                }
                else if (IsDigit(move))
                {
                    previous = TextRecognitionEnums.PreviousDigit;
                    coordinatesCounter++;
                }
                else if (IsLetter(move))
                {
                    previous = TextRecognitionEnums.PreviousLetter;
                    coordinatesCounter++;
                }
                else
                {
                    labelResult.Content = lastString;
                    textBoxMove.Text = lastString;
                    return;
                }

                parametersCounter++;
                CheckIfReadyToMakeMove();
            }
        }

        private void CheckIfReadyToMakeMove()
        {
            if (parametersCounter == 5 || coordinatesCounter == 4 || labelResult.Content.ToString().Equals("Castle long") || labelResult.Content.ToString().Equals("Castle short"))
            {               
                if (!(labelResult.Content.ToString().Equals("Castle long") || labelResult.Content.ToString().Equals("Castle short")))
                {
                    ParseMove(labelResult.Content.ToString());

                    Int16 rowStart = (Int16)(7 - Convert.ToInt16(labelRowStart.Content.ToString()[labelRowStart.Content.ToString().Length - 1].ToString()));
                    Int16 columnStart = Convert.ToInt16(labelColumnStart.Content.ToString()[labelColumnStart.Content.ToString().Length - 1].ToString());
                    Int16 rowDestination = (Int16)(7 - Convert.ToInt16(labelRowDestination.Content.ToString()[labelRowStart.Content.ToString().Length - 1].ToString()));
                    Int16 columnDestination = Convert.ToInt16(labelColumnDestination.Content.ToString()[labelColumnStart.Content.ToString().Length - 1].ToString());

                    ChessBoardField chessBoardField = ChessBoard.GetChessBoardField(rowStart, columnStart);

                    if (chessBoardField != null && chessBoardField.field != null)
                    {
                        Piece piece = Piece.FindPiece(chessBoardField.field);
                        if (piece != null)
                        {
                            ChessBoard.MakeSelection(chessBoardField.field);
                            ChessBoardField chessBoardFieldFirstLocalization = chessBoardField;
                            chessBoardField = ChessBoard.GetChessBoardField(rowDestination, columnDestination);
                            if (chessBoardField != null && chessBoardField.field != null)
                                ChessBoard.MakeSelection(ChessBoard.GetChessBoardField(rowDestination, columnDestination).field);
                            else
                                ChessBoard.MakeSelection(chessBoardFieldFirstLocalization.field);

                        }
                    }
                }
                else
                {
                    MakeCastling(labelResult.Content.ToString());
                }

                textBoxMove.TextChanged -= textBoxMove_TextChanged;
                ClearCoordinates();
                speechRecognition.Enabled = false;
                buttonListen.IsEnabled = true;
                textBoxMove.TextChanged += textBoxMove_TextChanged;
                speechRecognition.Enabled = true;
            }
        }

        private bool IsLetter(string letter)
        {
            return Regex.IsMatch(letter, "^[A-H]{1}", RegexOptions.IgnoreCase);
        }

        private bool IsDigit(string move)
        {
            return Regex.IsMatch(move, "^[1-8]{1}", RegexOptions.IgnoreCase);
        }

        private void ParseMove(string move)
        {
            string startupCoordinate = string.Empty;

            if (!CheckIfFirstIsPiece(move.Substring(0, move.Length - 4)))
            {
                startupCoordinate = move.Substring(0, 2);
            }
            else
            {
                if (CheckIfFirstIsPiece(move.Substring(0, move.Length - 4)))
                {
                    startupCoordinate = move.Substring(move.Length - 4, 2);
                }
            }

            ReadCoordinates(startupCoordinate, labelRowStart, labelColumnStart);
            ReadCoordinates(move.Substring(move.Length - 2, 2), labelRowDestination, labelColumnDestination);
        }

        private void ReadCoordinates(string coordinate, Label startRow, Label startColumn)
        {
            if (!string.IsNullOrEmpty(coordinate))
            {
                if (Char.IsDigit(coordinate[0]))
                {
                    startRow.Content += (int.Parse(coordinate[0].ToString()) - 1).ToString();
                    startColumn.Content += GetRowNumberAccordingToLetter(coordinate[1]).ToString();
                }
                else
                {
                    startRow.Content += (int.Parse(coordinate[1].ToString()) - 1).ToString();
                    startColumn.Content += GetRowNumberAccordingToLetter(coordinate[0]).ToString();
                }
            }
        }

        private int GetRowNumberAccordingToLetter(char letter)
        {
            return Convert.ToInt32(letter) - 65;
        }

        private bool CheckIfFirstIsPiece(string move)
        {
            switch (move)
            {
                case Constants.BISHOP:
                    return true;
                case Constants.KING:
                    return true;
                case Constants.KNIGHT:
                    return true;
                case Constants.PAWN:
                    return true;
                case Constants.ROOK:
                    return true;
                case Constants.QUEEN:
                    return true;
                default:
                    return false;
            }
        }

        private bool CheckIfCastling(string move)
        {
            switch (move)
            {
                case Constants.CASTLE_SHORT:
                    return true;
                case Constants.CASTLE_LONG:
                    return true;
                default:
                    return false;
            }
        }

        private void ClearCoordinates()
        {
            labelResult.Content = "";
            labelChosenPiece.Content = "";
            labelColumnStart.Content = "Kolumna: ";
            labelRowStart.Content = "Wiersz: ";
            labelColumnDestination.Content = "Kolumna: ";
            labelRowDestination.Content = "Wiersz: ";
            textBoxMove.Clear();
            parametersCounter = 0;
            coordinatesCounter = 0;
            previous = TextRecognitionEnums.None;
        }

        private void BeginListen()
        {
            speechRecognition.Enabled = true;
            buttonListen.Content = "Zatrzymaj";
        }

        private void EndListen()
        {
            speechRecognition.Enabled = false;
            buttonListen.Content = "Słuchaj";
        }

        #endregion
        #region Event Handlers

        private void chessBoard_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBlackPieces();
            LoadWhitePieces();

            WhitePiece.turn = true;
            BlackPiece.turn = false;
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemConfiguration_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationWindowView window = new ConfigurationWindowView(Player.Time, GameMode.HumanVsHuman.Equals(GameMode.HumanVsHuman) ? GameModeConfiguration.Człowiek : GameModeConfiguration.Komputer);
            window.ShowDialog();

            Controller.ChangeOptions(window._mTime, window._mode, buttonStartPlayerTwo, textBoxPlayerTwo);
            Controller.AddComputerPlayer(textBoxPlayerTwo, buttonStartPlayerTwo, TextPlayerTwoTime, PieceColor.Black);
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            textBoxMove.Text += e.Result.Text;

            if (labelResult.Content.ToString().Length == 0 || string.IsNullOrEmpty(labelResult.Content.ToString()))
                indexStart = 0;
            else
                indexStart = labelResult.Content.ToString().Length;

            if ((textBoxMove.Text.Length - labelResult.Content.ToString().Length) == 1)
                labelResult.Content += textBoxMove.Text[textBoxMove.Text.Length - 1].ToString().ToUpper();
            else
                labelResult.Content += textBoxMove.Text.Substring(labelResult.Content.ToString().Length, textBoxMove.Text.Length - labelResult.Content.ToString().Length);

            indexEnd = labelResult.Content.ToString().Length;
            textBoxMove.TextChanged -= textBoxMove_TextChanged;
            labelResultTextChanged();
            textBoxMove.TextChanged += textBoxMove_TextChanged;
            lastString = labelResult.Content.ToString();
        }

        private void Grid_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField00);
        }

        private void Grid_MouseLeftButtonDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField01);
        }

        private void Grid_MouseLeftButtonDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField02);
        }

        private void Grid_MouseLeftButtonDown_4(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField03);
        }

        private void Grid_MouseLeftButtonDown_5(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField04);
        }

        private void Grid_MouseLeftButtonDown_6(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField05);
        }

        private void Grid_MouseLeftButtonDown_7(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField06);
        }

        private void Grid_MouseLeftButtonDown_8(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField07);
        }

        private void GridField10_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField10);
        }

        private void GridField11_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField11);
        }

        private void GridField12_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField12);
        }

        private void GridField13_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField13);
        }

        private void GridField14_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField14);
        }

        private void GridField15_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField15);
        }

        private void GridField16_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField16);
        }

        private void GridField17_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField17);
        }

        private void GridField20_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField20);
        }

        private void GridField21_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField21);
        }

        private void GridField22_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField22);
        }

        private void GridField23_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField23);
        }

        private void GridField24_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField24);
        }

        private void GridField25_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField25);
        }

        private void GridField26_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField26);
        }

        private void GridField27_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField27);
        }

        private void GridField30_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField30);
        }

        private void GridField31_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField31);
        }

        private void GridField32_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField32);
        }

        private void GridField33_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField33);
        }

        private void GridField34_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField34);
        }

        private void GridField35_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField35);
        }

        private void GridField36_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField36);
        }

        private void GridField37_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField37);
        }

        private void GridField40_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField40);
        }

        private void GridField41_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField41);
        }

        private void GridField42_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField42);
        }

        private void GridField43_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField43);
        }

        private void GridField44_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField44);
        }

        private void GridField45_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField45);
        }

        private void GridField46_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField46);
        }

        private void GridField47_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField47);
        }

        private void GridField50_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField50);
        }

        private void GridField51_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField51);
        }

        private void GridField52_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField52);
        }

        private void GridField53_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField53);
        }

        private void GridField54_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField54);
        }

        private void GridField55_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField55);
        }

        private void GridField56_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField56);
        }

        private void GridField57_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField57);
        }

        private void GridField60_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField60);
        }

        private void GridField61_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField61);
        }

        private void GridField62_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField62);
        }

        private void GridField63_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField63);
        }

        private void GridField64_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField64);
        }

        private void GridField65_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField65);
        }

        private void GridField66_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField66);
        }

        private void GridField67_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField67);
        }

        private void GridField70_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField70);
        }

        private void GridField71_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField71);
        }

        private void GridField72_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField72);
        }

        private void GridField73_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField73);
        }

        private void GridField74_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField74);
        }

        private void GridField75_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField75);
        }

        private void GridField76_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField76);
        }

        private void GridField77_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChessBoard.MakeSelection(GridField77);
        }

        private void buttonStartPlayerOne_Click(object sender, RoutedEventArgs e)
        {
            Controller.EnableStartButton(textBoxPlayerOne, buttonStartPlayerOne, TextBlockPlayerOneTime, PieceColor.White, MenuItemConfigure);
        }

        private void buttonStartPlayerTwo_Click(object sender, RoutedEventArgs e)
        {
            Controller.EnableStartButton(textBoxPlayerTwo, buttonStartPlayerTwo, TextPlayerTwoTime, PieceColor.Black, MenuItemConfigure);
        }

        private void textBoxMove_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (labelResult.Content.ToString().Length == 0 || string.IsNullOrEmpty(labelResult.Content.ToString()))
                indexStart = 0;
            else
                indexStart = labelResult.Content.ToString().Length;

            if ((textBoxMove.Text.Length - labelResult.Content.ToString().Length) == 1)
                labelResult.Content += textBoxMove.Text[textBoxMove.Text.Length - 1].ToString().ToUpper();
            else
                labelResult.Content += textBoxMove.Text.Substring(labelResult.Content.ToString().Length, textBoxMove.Text.Length - labelResult.Content.ToString().Length);

            indexEnd = labelResult.Content.ToString().Length;
            textBoxMove.TextChanged -= textBoxMove_TextChanged;
            labelResultTextChanged();
            textBoxMove.TextChanged += textBoxMove_TextChanged;
            lastString = labelResult.Content.ToString();
        }  

        private void buttonPiece_Click(object sender, RoutedEventArgs e)
        {
            textBoxMove.Text += comboBoxPiece.Text;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearCoordinates();
        }

        private void textBoxMove_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
            }
        }

        private void buttonListen_Click(object sender, RoutedEventArgs e)
        {
            textBoxMove.IsEnabled = false;
            textBoxMove.TextChanged -= textBoxMove_TextChanged;

            if (buttonListen.Content.ToString().Equals("Słuchaj"))
                BeginListen();
            else
                EndListen();
        }

        #endregion   
        #region Public Methods

        public void EnableRecognitionControls(bool state)
        {
            textBoxMove.IsEnabled = state;
            buttonClear.IsEnabled = state;
            buttonListen.IsEnabled = state;
            buttonPiece.IsEnabled = state;
            comboBoxPiece.IsEnabled = state;
        }

        public void EnableTextMove(bool state)
        {
            textBoxMove.IsEnabled = state;
        }

        private void MakeCastling(string mode)
        {
            ChessBoard.MakeSelection(WhitePiece.turn ? WhitePiece.WhiteKing.CurrentField : BlackPiece.BlackKing.CurrentField);

            if (WhitePiece.turn)
            {
                ChessBoard.MakeSelection(mode.Equals("Castle long") ? ChessBoard.GetField(7, 2) : ChessBoard.GetField(7, 6));
            }
            else
            {
                ChessBoard.MakeSelection(mode.Equals("Castle long") ? ChessBoard.GetField(0, 2) : ChessBoard.GetField(0, 6));
            }

            ChessBoard.DeSelectAvailableAllPieces();
        }

        #endregion
    }
}
