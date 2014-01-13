using System.Windows.Forms;
using Chess.Model;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Chess.ConfigurationWindow
{
    public class ConfigurationWindowController
    {
        #region Constructors

        public ConfigurationWindowController()
        {
            
        }

        #endregion
        #region Public Methods

        public void LoadGameModes(System.Windows.Forms.ComboBox comboBoxModes)
        {
            comboBoxModes.Items.Add(GameModeConfiguration.Człowiek.ToString());
            comboBoxModes.Items.Add(GameModeConfiguration.Komputer.ToString());
        }

        public void LoadCurrentTime(NumericUpDown numericUpDownTime, int time)
        {
            numericUpDownTime.Value = time;
        }

        #endregion
    }
}
