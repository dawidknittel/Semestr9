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
                View.TextBoxMeanNumberOfApplicationInQueue.Text = Math.Round(CarWash.calculateMeanNumberOfApplicationInQueue(currentCarWash), 4).ToString();
                View.TextBoxMeanTimeApplicationInQueue.Text = Math.Round(CarWash.calculateMeanTimeApplicationInQueue(currentCarWash), 4).ToString();
                View.TextBoxRelativeSystemAbility.Text = Math.Round(CarWash.calculateRelativeSystemAbility(currentCarWash), 4).ToString();
                View.TextBoxAbsoluteSystemAbility.Text = Math.Round(CarWash.calculateAbsoluteSystemAbility(currentCarWash), 4).ToString();
            }
        }


        #endregion
    }
}
