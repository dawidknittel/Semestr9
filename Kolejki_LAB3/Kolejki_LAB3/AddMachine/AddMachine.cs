using System;
using System.Windows.Forms;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3.AddMachine
{
    public partial class AddMachine : Form
    {
        #region Public Fields
        
        public AddMachineController _addMachineController = null;

        #endregion
        #region Properties

        public AddMachineStateWindow windowState = AddMachineStateWindow.CANCEL;

        public ComboBox ComboBoxInput
        {
            get
            {
                return comboBoxInput;
            }
        }

        public ComboBox ComboBoxOutput
        {
            get
            {
                return comboBoxOutput;
            }
        }

        public ListBox ListBoxInput
        {
            get
            {
                return listBoxInputs;
            }
        }

        public ListBox ListBoxOutput
        {
            get
            {
                return listBoxOutput;
            }
        }

        public NumericUpDown NumericUpDownInput
        {
            get
            {
                return numericUpDownInput;
            }
        }

        public NumericUpDown NumericUpDownOutput
        {
            get
            {
                return numericUpDownOutput;
            }
        }

        #endregion
        #region Constructors

        public AddMachine()
        {
            InitializeComponent();

            _addMachineController = new AddMachineController(this);
            _addMachineController.LoadAlgorithms(comboBoxAlgorithm);
            _addMachineController.LoadInputSystems();
            _addMachineController.LoadOutputSystems();
            _addMachineController.InitializeCarhWashObject();
        }

        #endregion 
        #region Event Handlers

        private void comboBoxOutput_SelectedValueChanged(object sender, EventArgs e)
        {
            _addMachineController.CalculateAvailablePercent(_addMachineController.GetStream(comboBoxOutput.Text), false);
        }

        private void comboBoxInput_SelectedValueChanged(object sender, EventArgs e)
        {
            _addMachineController.CalculateAvailablePercent(_addMachineController.GetStream(comboBoxInput.Text), true);        
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            labeLNumerOfitems.Text = numericUpDownServicePlacesNumber.Value.ToString();
            _addMachineController.CurrentCarWash.ServicePlacesLength = int.Parse(numericUpDownServicePlacesNumber.Value.ToString());
        }

        private void comboBoxAlgorithm_SelectedValueChanged(object sender, EventArgs e)
        {
            labelSelectedAlgorithm.Text = comboBoxAlgorithm.SelectedItem.ToString();
            _addMachineController.CurrentCarWash.Algorithm = _addMachineController.SelectAlgorithm(comboBoxAlgorithm.SelectedItem.ToString());
        }

        private void numericUpDownQueueLenght_ValueChanged(object sender, EventArgs e)
        {
            labelQueueLenghtPattern.Text = numericUpDownQueueLenght.Value.ToString();
            _addMachineController.CurrentCarWash.QueueLength = int.Parse(numericUpDownQueueLenght.Value.ToString());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            windowState = AddMachineStateWindow.ADD;
            if(_addMachineController.AddMachineToList())
                Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            windowState  = AddMachineStateWindow.CANCEL;
            Close();
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (int.Parse(numericUpDownInput.Value.ToString()) == 0)
            {
                errorProviderInput.SetError(numericUpDownInput, "Wartość nie może być równa zero");
                return;
            }

            if (comboBoxOutput.Items.Contains(comboBoxInput.Text))
                comboBoxOutput.Items.Remove(comboBoxInput.Text);

            if (!string.IsNullOrEmpty(comboBoxInput.Text))
            {
                listBoxInputs.Items.Add(comboBoxInput.Text);

                if (comboBoxInput.Text.Equals(InputOutputEnum.Strumień_wejściowy.ToString()))
                {
                    _addMachineController.CurrentCarWash.InputSystems.Add(new InputOutput()
                    {
                        CarWash = null,
                        Percent = int.Parse(numericUpDownInput.Value.ToString()),
                        State = SystemState.Start.ToString()
                    });
                }
                else
                {
                    _addMachineController.CurrentCarWash.InputSystems.Add(new InputOutput()
                    {
                        MachineName = comboBoxInput.Text,
                        CarWash = CarWash.FindCarWash(comboBoxInput.Text),
                        Percent = int.Parse(numericUpDownInput.Value.ToString()),
                        State = SystemState.None.ToString()
                    });
                    UpdateOtherMachineUnputOutputs(_addMachineController.CurrentCarWash, CarWash.FindCarWash(comboBoxInput.Text), Convert.ToDecimal(numericUpDownInput.Value.ToString()), RelationType.Input);
                }

                comboBoxInput.Text = string.Empty;
                numericUpDownInput.Value = 0;
            }
        }

        private void UpdateOtherMachineUnputOutputs(CarWash currentCarWash, CarWash destinationCarWash, decimal percent, RelationType diretion)
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                if (carWash.MachineName.Equals(destinationCarWash.MachineName))
                {
                    if (diretion.Equals(RelationType.Input))
                    {
                        carWash.OutputSystems.Add(new InputOutput() { CarWash = CarWash.FindCarWash(currentCarWash.MachineName), Percent = percent, State = SystemState.None.ToString() });
                    }
                    else
                    {
                        carWash.InputSystems.Add(new InputOutput() { CarWash = CarWash.FindCarWash(currentCarWash.MachineName), Percent = percent, State = SystemState.None.ToString() });
                    }
                }
            }
        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            if (int.Parse(numericUpDownOutput.Value.ToString()) == 0)
            {
                errorProviderOutput.SetError(numericUpDownOutput, "Wartość nie może być równa zero");
                return;
            }

            if (!string.IsNullOrEmpty(comboBoxOutput.Text))
            {
                listBoxOutput.Items.Add(ComboBoxOutput.Text);

                if (comboBoxOutput.Text.Equals(InputOutputEnum.Strumień_wyjściowy.ToString()))
                {
                    _addMachineController.CurrentCarWash.OutputSystems.Add(new InputOutput()
                    {
                        CarWash = null,
                        Percent = int.Parse(numericUpDownOutput.Value.ToString()),
                        State = SystemState.End.ToString()
                    });
                }
                else
                {
                    _addMachineController.CurrentCarWash.OutputSystems.Add(new InputOutput()
                    {
                        MachineName = comboBoxOutput.Text,
                        CarWash = CarWash.FindCarWash(comboBoxOutput.Text),
                        Percent = int.Parse(numericUpDownOutput.Value.ToString()),
                        State = SystemState.None.ToString()
                    });
                    UpdateOtherMachineUnputOutputs(_addMachineController.CurrentCarWash, CarWash.FindCarWash(comboBoxOutput.Text), Convert.ToDecimal(numericUpDownInput.Value.ToString()), RelationType.Output);
                }

                comboBoxOutput.Text = string.Empty;
                numericUpDownOutput.Value = 0;
            }
        }

        #endregion            
    }
}
