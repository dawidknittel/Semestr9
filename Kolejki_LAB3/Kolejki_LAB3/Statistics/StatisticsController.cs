using System;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3.Statistics
{
    public class StatisticsController
    {
        #region Private Fields

        private StatisticsWindow View;

        #endregion
        #region Constructors

        public StatisticsController(StatisticsWindow view)
        {
            View = view;
        }

        #endregion
        #region Public Methods

        public void LoadSystems()
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                View.ComboBoxMachinseName.Items.Add(carWash.MachineName);
            }
        }

        public void ShowStatistics()
        {
            CarWash currentCarWash = CarWash.FindCarWash(View.ComboBoxMachinseName.Text);

            if (currentCarWash != null)
            {
                View.TextBoxMeanNumberOfApplicationInQueue.Text = Math.Round(currentCarWash.MeanNumberOfApplicationInQueue, 2).ToString();
                View.TextBoxMeanTimeApplicationInQueue.Text = Math.Round(currentCarWash.MeanTimeApplicationInQueue, 2).ToString();
                View.TextBoxRelativeSystemAbility.Text = Math.Round(currentCarWash.RelativeSystemAbility, 2).ToString();
                View.TextBoxAbsoluteSystemAbility.Text = Math.Round(currentCarWash.AbsoluteSystemAbility, 2).ToString();
            }
        }

        #endregion
    }
}
