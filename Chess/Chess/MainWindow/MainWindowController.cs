using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Chess.Model;

namespace Chess.MainWindow
{
    public class MainWindowController
    {
        #region Private Properties

        private ImageBrush chessImage
        {
            get
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Constants.FieldDark));
                myBrush.ImageSource = image.Source;

                return myBrush;
            }
        }

        #endregion
        #region Public Fields

        public MainWindowView View = null;
        public static List<Player> players = new List<Player>();

        #endregion
        #region Constructors

        public MainWindowController(MainWindowView view)
        {
            View = view;
        }

        #endregion
        #region Public Methods

        public void LoadImage(Border chessBoardCell, PieceImage image)
        {
            chessBoardCell.Background = ChessBoard.ChessImage(image);
        }

        public void AddComputerPlayer(TextBox textBoxPlayerName, Button buttonStart, TextBlock textBlock, PieceColor pieceColor)
        {
            textBoxPlayerName.IsEnabled = false;
            buttonStart.Content = Constants.Stop;
            players.Add(new Player() { PlayerName = textBoxPlayerName.Text, PlayerState = PlayerState.Ready, TimeBlock = textBlock, PieceColor = pieceColor });
        }

        public void EnableStartButton(TextBox textBoxPlayerName, Button buttonStart, TextBlock textBlock, PieceColor pieceColor, MenuItem MenuItemConfigure)
        {
            switch (buttonStart.Content.ToString())
            {
                case Constants.Start:
                {
                    if (textBoxPlayerName.Text.Length > 3)
                    {
                        textBoxPlayerName.IsEnabled = false;
                        buttonStart.Content = Constants.Stop;
                        players.Add(new Player() { PlayerName = textBoxPlayerName.Text, PlayerState = PlayerState.Ready, TimeBlock = textBlock, PieceColor = pieceColor});

                       if (players.Count == 2)
                       {
                            Player player = (from PLAY in players where PLAY.PieceColor == PieceColor.White select PLAY).FirstOrDefault();
                            players[0].SetStartupTime();
                            players[1].SetStartupTime();
                            player.timer.Start();
                            MenuItemConfigure.IsEnabled = false;
                            View.EnableRecognitionControls(true);
                       }
                    }
                    break;
                }
                case Constants.Stop:
                {
                    Player player = (from PLAY in players where PLAY.PlayerName == textBoxPlayerName.Text select PLAY).FirstOrDefault();
                    player.PlayerState = PlayerState.Unready;
                    buttonStart.Content = Constants.Continue;
                    break;
                }
                case Constants.Continue:
                {
                    Player player = (from PLAY in players where PLAY.PlayerName == textBoxPlayerName.Text select PLAY).FirstOrDefault();
                    player.PlayerState = PlayerState.Ready;
                    buttonStart.Content = Constants.Stop;
                    break;
                }
            }
        }

        public void ChangeOptions(int time, string gameMode, Button button, TextBox textBox)
        {
            Player.Time = time;

            if (gameMode.Equals(GameModeConfiguration.Komputer.ToString()))
            {
                Player.mode  = GameMode.ComputerVsHuman;
                button.Visibility = Visibility.Hidden;
                textBox.Text = "Komputer";
            }
            else
            {
                Player.mode = GameMode.HumanVsHuman;
                button.Visibility = Visibility.Visible;
                textBox.Text = "";
            }
        }

        public static bool PlayerReadyToGame()
        {
            if (players.Count < 2)
            {
                return false;
            }

            foreach (Player player in players)
            {
                if (player.PlayerState.Equals(PlayerState.Unready))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
