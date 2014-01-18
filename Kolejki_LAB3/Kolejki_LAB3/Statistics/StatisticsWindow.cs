using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Kolejki_LAB3.Statistics;

namespace Kolejki_LAB3
{
    public partial class StatisticsWindow : Form
    {
        #region Private Fields

        private StatisticsController _StatisticsController = null;
        private FormQueueSystems _MainWindowView = null;

        #endregion
        #region Properties

        public Chart ChartMeanTimeInQueue
        {
            get { return chartMeanTimeInQueue; }
        }

        public StatisticsController StatisticsController
        {
            get { return _StatisticsController; }
        }

        public ComboBox ComboBoxMachinseName
        {
            get { return comboBoxMachinesName; }
        }

        public TextBox TextBoxApplicationNumber
        {
            get { return textBoxApplicationNumber; }
        }

        public TextBox TextBoxRelativeSystemAbility
        {
            get { return textBoxRelativeSystemAbility;  }
        }

        public TextBox TextBoxMeanTimeApplicationInQueue
        {
            get { return textBoxMeanTimeApplicationInQueue; }
        }

        public TextBox TextBoxAbsoluteSystemAbility
        {
            get { return textBoxAbsoluteSystemAbility; }
        }

        public TextBox TextBoxMeanNumberOfApplicationInQueue
        {
            get { return textBoxMeanNumberOfApplicationInQueue; }
        }

        #endregion
        #region Constructors

        public StatisticsWindow(FormQueueSystems mainWindowView)
        {
            InitializeComponent();

            _StatisticsController = new StatisticsController(this);
            _StatisticsController.LoadSystems();
            _MainWindowView = mainWindowView;
        }

        #endregion
        #region Event Handlers

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxMachinesName_SelectedValueChanged(object sender, EventArgs e)
        {
            _StatisticsController.ShowStatistics(comboBoxMachinesName.Text);
        }

        private void StatisticsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MainWindowView._formQueueSystemsController.statisticsWindow = null;
        }

        #endregion 
    }
}
