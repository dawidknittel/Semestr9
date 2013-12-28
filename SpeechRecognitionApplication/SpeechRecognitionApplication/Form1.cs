using System;
using System.Speech.Recognition;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SpeechRecognitionApplication
{
    public partial class Form1 : Form
    {
        #region Grammar

        private Choices choices = new Choices();
        private GrammarBuilder GrammarBuilder = null;
        private Grammar Grammar = null;
        private int? indexStart = null;
        private int? indexEnd = null;
        private string lastString = string.Empty;
        private string TemporaryMove = string.Empty;
        private TextRecognitionEnums previous = TextRecognitionEnums.None;
        private int parametersCounter = 0;
        private int coordinatesCounter = 0;

        #endregion
        #region SpeechRecognition

        SpeechRecognizer speechRecognition = new SpeechRecognizer();

        #endregion

        public Form1()
        {
            InitializeComponent();
            speechRecognition.SpeechRecognized += rec_SpeechRecognized;
            LoadPieces();
        }

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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            labelResult.Text = "";
            labelChosenPiece.Text = "";
            labelColumnStart.Text = "Kolumna: ";
            labelRowStart.Text = "Row: ";
            labelColumnDestination.Text = "Kolumna: ";
            labelRowDestination.Text = "Row: ";
            textBoxMove.Clear();
            parametersCounter = 0;
            coordinatesCounter = 0;
            previous = TextRecognitionEnums.None;
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            if (buttonListen.Text.Equals("Słuchaj")) 
                BeginListen();
            else
                EndListen();
        }

        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (labelResult.Text.Length == 0 || string.IsNullOrEmpty(labelResult.Text))
                indexStart = 0;
            else
                indexStart = labelResult.Text.Length - 1;

            labelResult.Text += e.Result.Text;         
            indexEnd = labelResult.Text.Length - 1;
            labelResultTextChanged();
            lastString = labelResult.Text;
        }

        private void BeginListen()
        {
            InitializeGrammar();
            speechRecognition.LoadGrammar(Grammar);
            speechRecognition.Enabled = true;
            buttonListen.Text = "Zatrzymaj";
        }

        private void EndListen()
        {
            speechRecognition.Enabled = false;
            buttonListen.Text = "Słuchaj";
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
            ReadCoordinates(move.Substring(move.Length-2, 2), labelRowDestination, labelColumnDestination);
        }

        private bool CheckIfPositionIsFirst(string move)
        {
            return Regex.IsMatch(move, "^[A-Z1-8]{1}[1-8A-H]{1}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            labelResultTextChanged();
            if (labelResult.Text != "")
                ParseMove(labelResult.Text);
        }

        private void ReadCoordinates(string coordinate, Label startRow, Label startColumn)
        {
            if (!string.IsNullOrEmpty(coordinate))
            {
                if (Char.IsDigit(coordinate[0]))
                {
                    startRow.Text += (int.Parse(coordinate[0].ToString()) - 1).ToString();
                    startColumn.Text += GetRowNumberAccordingToLetter(coordinate[1]).ToString();
                }
                else
                {
                    startRow.Text += (int.Parse(coordinate[1].ToString()) - 1).ToString();
                    startColumn.Text += GetRowNumberAccordingToLetter(coordinate[0]).ToString();
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

        private void labelResultTextChanged()
        {
            string move = labelResult.Text;

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
                        labelResult.Text = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                else if (move.Length > 3)
                {
                    if (CheckIfFirstIsPiece(move) && parametersCounter < 4 && !(move.Equals(Constants.CASTLE_LONG) || move.Equals(Constants.CASTLE_SHORT)))
                    {
                        previous = TextRecognitionEnums.PreviousPiece;
                        labelChosenPiece.Text = move;
                        parametersCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Text = lastString;
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
                        labelResult.Text = lastString;
                        textBoxMove.Text = lastString;
                        textBoxMove.Select(textBoxMove.Text.Length, 0);
                    }
                }
                else if (move.Length > 3 && parametersCounter < 4 && !(move.Equals(Constants.CASTLE_LONG) || move.Equals(Constants.CASTLE_SHORT)))
                {
                    if (CheckIfFirstIsPiece(move))
                    {
                        previous = TextRecognitionEnums.PreviousPiece;
                        labelChosenPiece.Text = move;
                        parametersCounter++;
                        CheckIfReadyToMakeMove();
                    }
                    else
                    {
                        labelResult.Text = lastString;
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
                    labelResult.Text = lastString;
                    textBoxMove.Text = lastString;
                    textBoxMove.Select(textBoxMove.Text.Length, 0);
                }
                return;
            }

            if (previous.Equals(TextRecognitionEnums.PreviousCastling))
            {
                labelResult.Text = lastString;
                textBoxMove.Text = lastString;
                textBoxMove.Select(textBoxMove.Text.Length, 0);
                return;
            }

            if (previous.Equals(TextRecognitionEnums.None))
            {
                if (CheckIfFirstIsPiece(move))
                {
                    previous = TextRecognitionEnums.PreviousPiece;
                    labelChosenPiece.Text = move;
                }
                else if (CheckIfCastling(move))
                {
                    previous = TextRecognitionEnums.PreviousCastling;
                    labelChosenPiece.Text = move;
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

                parametersCounter++;
                CheckIfReadyToMakeMove();
            }
        }

        private void CheckIfReadyToMakeMove()
        {
            if (parametersCounter == 5 || coordinatesCounter == 4)
            {
                ParseMove(labelResult.Text);
            }
        }

        private bool IsLetter(string letter)
        {
            return Regex.IsMatch(letter, "^[A-H]{1}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private bool IsDigit(string move)
        {
            return Regex.IsMatch(move, "^[1-8]{1}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private void textBoxMove_TextChanged(object sender, EventArgs e)
        {
            if (labelResult.Text.Length == 0 || string.IsNullOrEmpty(labelResult.Text))
                indexStart = 0;
            else
                indexStart = labelResult.Text.Length;

            if ((textBoxMove.Text.Length - labelResult.Text.Length) == 1)
                labelResult.Text += textBoxMove.Text[textBoxMove.Text.Length - 1].ToString().ToUpper();
            else
                labelResult.Text += textBoxMove.Text.Substring(labelResult.Text.Length, textBoxMove.Text.Length - labelResult.Text.Length);
            
            indexEnd = labelResult.Text.Length;
            textBoxMove.TextChanged -= textBoxMove_TextChanged;
            labelResultTextChanged();
            textBoxMove.TextChanged += textBoxMove_TextChanged;
            lastString = labelResult.Text;
        }

        private void buttonPiece_Click(object sender, EventArgs e)
        {
            textBoxMove.Text += comboBoxPiece.Text;
        }

        private void textBoxMove_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
