using System;
using System.Windows.Forms.DataVisualization.Charting;
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

        public void ShowStatistics(string machineName)
        {
            CarWash currentCarWash = CarWash.FindCarWash(machineName);

            if (currentCarWash != null)
            {
                View.TextBoxMeanNumberOfApplicationInQueue.Text = Math.Round(CarWash.calculateMeanNumberOfApplicationInQueue(currentCarWash), 4).ToString();
                View.TextBoxMeanTimeApplicationInQueue.Text = Math.Round(CarWash.calculateMeanTimeApplicationInQueue(currentCarWash), 4).ToString();
                View.TextBoxRelativeSystemAbility.Text = Math.Round(CarWash.calculateRelativeSystemAbility(currentCarWash), 4).ToString();
                View.TextBoxAbsoluteSystemAbility.Text = Math.Round(CarWash.calculateAbsoluteSystemAbility(currentCarWash), 4).ToString();
                View.TextBoxApplicationNumber.Text = currentCarWash.numberOfCarsTotal.ToString();
                FillChartWithData();
            }
        }

        public void FillChartWithData()
        {
            if (View != null && View.ChartMeanTimeInQueue != null)
            {
                View.ChartMeanTimeInQueue.Series["SeriesMeanTimeInQueue"].Points.Clear();

                CarWash carWash = CarWash.FindCarWash(View.ComboBoxMachinseName.Text);

                foreach (MeanTimeInQueue meanTimeInQueue in carWash.ChartData)
                {
                    View.ChartMeanTimeInQueue.Series["SeriesMeanTimeInQueue"].Points.Add(new DataPoint(meanTimeInQueue.CurrentTime, Convert.ToDouble(meanTimeInQueue.MeanTime)));
                }
            }
        }

        #endregion
    }
}
