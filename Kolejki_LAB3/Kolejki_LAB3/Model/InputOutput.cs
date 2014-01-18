using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class InputOutput
    {
        public string MachineName { get; set; }
        public CarWash CarWash { get; set; }
        public Label PercentLabel;
        public decimal Percent { get; set; }
        public string State { get; set; }

        public InputOutput()
        {
            
        }
    }
}
