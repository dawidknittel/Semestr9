using System;
using System.Windows.Forms;
using Kolejki_LAB3.Statistics;

namespace Kolejki_LAB3
{
    public partial class StatisticsWindow : Form
    {
        #region Private Fields

        private StatisticsController _StatisticsController = null;

        #endregion
        #region Properties

        public ComboBox ComboBoxMachinseName
        {
            get { return comboBoxMachinesName; }
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

        public StatisticsWindow()
        {
            InitializeComponent();

            _StatisticsController = new StatisticsController(this);
            _StatisticsController.LoadSystems();
        }

        #endregion
        #region Event Handlers

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxMachinesName_SelectedValueChanged(object sender, EventArgs e)
        {
           _StatisticsController.ShowStatistics();
        }

        #endregion
    }
}
