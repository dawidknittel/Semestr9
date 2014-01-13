using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Chess.Model;

namespace Chess.ConfigurationWindow
{
    public partial class ConfigurationWindowView : Form
    {
        private ConfigurationWindowController _mConfigurationWindowController;
        private WindowExitStatus _windowStatus;
        public string _mode;
        public int _mTime;
        

        public ConfigurationWindowView(int time, GameModeConfiguration mode)
        {
            InitializeComponent();
            _mTime = time;
            _mConfigurationWindowController = new ConfigurationWindowController();
            _mConfigurationWindowController.LoadCurrentTime(numericUpDownTime, _mTime);
            _mConfigurationWindowController.LoadGameModes(comboBoxModes);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _windowStatus = WindowExitStatus.Canceled;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _windowStatus = WindowExitStatus.Ok;
            Close();
        }

        private void numericUpDownTime_ValueChanged(object sender, EventArgs e)
        {
            _mTime = Convert.ToInt32(numericUpDownTime.Value);
        }

        private void comboBoxModes_SelectedValueChanged(object sender, EventArgs e)
        {
            _mode = comboBoxModes.SelectedItem.ToString();
        }
    }
}
