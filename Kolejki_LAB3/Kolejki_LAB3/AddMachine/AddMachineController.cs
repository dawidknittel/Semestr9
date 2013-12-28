using System.Collections.Generic;
using System.Windows.Forms;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3.AddMachine
{
    public class AddMachineController
    {
        #region Public Fields

        public CarWash CurrentCarWash = null;
        public AddMachine View = null;

        #endregion
        #region Constructors

        public AddMachineController(AddMachine view)
        {
            View = view;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Ładowanie nazw algorytmów do comboBox
        /// </summary>
        /// <param name="comboBoxAlgorithms"></param>
        public void LoadAlgorithms(ComboBox comboBoxAlgorithms)
        {
            comboBoxAlgorithms.Items.Add(Algorithm.FIFO.ToString());
            comboBoxAlgorithms.Items.Add(Algorithm.LIFO.ToString());
            comboBoxAlgorithms.Items.Add(Algorithm.RSS.ToString());
        }

        public void LoadInputSystems()
        {
            View.ComboBoxInput.Items.Add(InputOutputEnum.Strumień_wejściowy);

            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                View.ComboBoxInput.Items.Add(carWash.MachineName);
            }         
        }

        public void LoadOutputSystems()
        {
            View.ComboBoxOutput.Items.Add(InputOutputEnum.Strumień_wyjściowy);

            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                View.ComboBoxOutput.Items.Add(carWash.MachineName);
            }
        }

        public void InitializeCarhWashObject()
        {
            CurrentCarWash = new CarWash();
            CurrentCarWash.QueueLength = 1;
            CurrentCarWash.ServicePlacesLength = 1;
        }

        public Algorithm SelectAlgorithm(string selectedAlgoritm)
        {
            if (selectedAlgoritm.Equals(Algorithm.FIFO.ToString()))
                return Algorithm.FIFO;
            if (selectedAlgoritm.Equals(Algorithm.LIFO.ToString()))
                return Algorithm.LIFO;
            if (selectedAlgoritm.Equals(Algorithm.RSS.ToString()))
                return Algorithm.RSS;

            return Algorithm.NONE;
        }

        /// <summary>
        /// Dodaj maszynę do listy wszystkich maszyn
        /// </summary>
        public bool AddMachineToList()
        {
            if (CurrentCarWash.Algorithm != null && CurrentCarWash.QueueLength != null &&
                CurrentCarWash.ServicePlacesLength != null &&
                (View.ListBoxInput.Items.Count > 0 || View.ListBoxOutput.Items.Count > 0))
            {
                QueueSystem.carWashList.Add(CurrentCarWash);
                return true;
            }
            else
            {
                MessageBox.Show("Nie można dodać maszyny, ponieważ nie określono wszystkich parametrów",
                    "Brak parametrów", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public string GetStream(string streamName)
        {
            if (streamName.Equals(InputOutputEnum.Strumień_wejściowy.ToString()))
                return SystemState.Start.ToString();
            if (streamName.Equals(InputOutputEnum.Strumień_wyjściowy.ToString()))
                return SystemState.End.ToString();
            return streamName;
        }

        public void CalculateAvailablePercent(string inputOutputName, bool input)
        {
            decimal calculatedPercent = 0;

            if (inputOutputName.Equals(SystemState.Start.ToString()) || inputOutputName.Equals(SystemState.End.ToString()))
            {
                    foreach (CarWash carWash in QueueSystem.carWashList)
                    {
                       List<InputOutput> inputOutputStartList = input ? carWash.InputSystems : carWash.OutputSystems;

                       foreach (InputOutput inputOutput in inputOutputStartList)
                       {
                           if (inputOutput.State.Equals(inputOutputName))
                               calculatedPercent += inputOutput.Percent;
                       }
                    }
            }
            else
            {
                CarWash foundCarWash = CarWash.FindCarWash(inputOutputName);

                List<InputOutput> inputOutputList = input ? foundCarWash.OutputSystems : foundCarWash.InputSystems;

                foreach (InputOutput inputOutput in inputOutputList)
                {
                    calculatedPercent += inputOutput.Percent;
                }
            }                  

            if (input)
            {
                View.NumericUpDownInput.Maximum = 100 - calculatedPercent;
                View.NumericUpDownInput.Value = 100 - calculatedPercent;
            }
            else
            {
                View.NumericUpDownOutput.Maximum = 100 - calculatedPercent;
                View.NumericUpDownOutput.Value = 100 - calculatedPercent;
            }
        }

        #endregion
    }
}
