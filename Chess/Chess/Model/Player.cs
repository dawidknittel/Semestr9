using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Chess.Model
{
    public class Player
    {
        #region Public Fields

        public static GameMode mode;
        public DispatcherTimer timer;
        public PieceColor PieceColor;
        public static int Time = 900;

        #endregion
        #region Properties

        public string PlayerName { get; set; }
        public PlayerState PlayerState { get; set; }
        public TextBlock TimeBlock { get; set; }

        #endregion
        #region Constructors

        public Player()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        #endregion
        #region Public Methods

        public void SetStartupTime()
        {
            TimeBlock.Text = string.Format("00:{0}:{1}", Time / 60, Time % 60);
            ShowTime(Time);
        }

        private void ShowTime(int time)
        {
            if ((time / 60) > 10)
            {
                if ((time % 60) >= 10)
                    TimeBlock.Text = string.Format("00:{0}:{1}", time / 60, time % 60);
                else
                    TimeBlock.Text = string.Format("00:{0}:0{1}", time / 60, time % 60);
            }
            else
            {
                if ((time % 60) >= 10)
                    TimeBlock.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                else
                    TimeBlock.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);
            }   
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Time--;
            ShowTime(Time);

            if (Time < 60)
            {
                TimeBlock.Foreground = Brushes.Red;
            }

            if (Time < 10)
            {
                TimeBlock.Foreground = Brushes.Coral;
            }

            if (Time == 0)
            {
                timer.Stop();
                MessageBox.Show("Koniec Gry!", "Zakończenie gry", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        #endregion
    }
}
