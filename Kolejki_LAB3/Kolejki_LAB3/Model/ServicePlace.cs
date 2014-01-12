using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class ServicePlace
    {
        private Car currentCar = null;
        private ProgressBar progressBar = null;
        private Label labelCurrentCar = null;

        public Car CurrentCar
        {
            get { return currentCar; }
            set { currentCar = value; }
        }

        public ProgressBar ProgressBar
        {
            get { return progressBar; }
            set { progressBar = value; }
        }

        public Label LabelCurrentCar
        {
            get { return labelCurrentCar; }
            set { labelCurrentCar = value; }
        }
    }
}
