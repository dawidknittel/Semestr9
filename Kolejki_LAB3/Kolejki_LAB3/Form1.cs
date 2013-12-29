using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Kolejki_LAB3.Model;
using System.Reflection;

namespace Kolejki_LAB3
{
    public partial class FormQueueSystems : Form
    {
        #region Private Fields

        private FormQueueSystemsController _formQueueSystemsController = null;

        #endregion
        #region Properties

        public NumericUpDown NumericUpDownServiceIntensity
        {
            get { return numericUpDownServiceIntensity; }
        }

        public NumericUpDown NumericUpDownStreamApplicationIntensity
        {
            get { return numericUpDownStreamApplicationIntensity; }
        }

        public ListBox ListBoxComunicates
        {
            get { return listBoxComunicates; }
        }

        #endregion
        #region Constructors

        public FormQueueSystems()
        {
            InitializeComponent();

            _formQueueSystemsController = new FormQueueSystemsController(this);

            QueueSystem.FillListPoints();
            QueueSystem.FillStartPoints();
            QueueSystem.FillEndPoints();     
        }

        #endregion
        #region Event Handlers

        private void FormQueueSystems_Paint(object sender, PaintEventArgs e)
        {
            _formQueueSystemsController.DrawStartupLine();
            _formQueueSystemsController.DrawEndLine();
        }

        /// <summary>
        /// Otworzenie okna dodawania maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMachine.AddMachine form = new AddMachine.AddMachine();
            form.ShowDialog();

            if (form.windowState.Equals(AddMachineStateWindow.ADD))
            {
                AddMachineToInterface(form._addMachineController.CurrentCarWash);
                _formQueueSystemsController.UpdateMachineName(QueueSystem.carWashList[QueueSystem.carWashList.Count-1]);
            }           

            //sprawdzenie czy już jest 6 maszyn, jeśli tak to wyszarz przycisk dodawania
            if (QueueSystem.carWashList.Count == 6)
                dodajToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Wywołanie zapisania stanu do pliku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zapiszModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.SaveConfiguration();
        }

        /// <summary>
        /// Odczyt danych z pliku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wczytajModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.LoadConfiguration();
        }

        /// <summary>
        /// Zamknięcie programu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Reakcja na zmianę wartości strumienia wejściowego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownServiceIntensity_ValueChanged(object sender, EventArgs e)
        {
            QueueSystem.Lambda = int.Parse(numericUpDownServiceIntensity.Value.ToString());
            errorProviderServiceTimeIntensity.Clear();
        }

        /// <summary>
        /// Reakcja na zmianę wartości intensywaności strumienia zgłoszeń
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownStreamApplicationIntensity_ValueChanged(object sender, EventArgs e)
        {
            QueueSystem.Mi = int.Parse(numericUpDownStreamApplicationIntensity.Value.ToString());
            errorProviderStreamApplicationIntensity.Clear();
        }


        /// <summary>
        /// Reakcja na kliknięcie przycisku start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (int.Parse(numericUpDownServiceIntensity.Value.ToString()) == 0)
            {
                errorProviderServiceTimeIntensity.SetError(numericUpDownServiceIntensity, "Nie wybrano wartości");
            }

            if (int.Parse(numericUpDownStreamApplicationIntensity.Value.ToString()) == 0)
            {
                errorProviderStreamApplicationIntensity.SetError(numericUpDownStreamApplicationIntensity, "Nie wybrano wartośći");
            }

            if (QueueSystem.carWashList.Count > 0)
            {
                //LoadExample();
                RunSimulation();
                //buttonStart.Enabled = false;
            }
        }

        /// <summary>
        /// Obsługa wyczyszczenia okna komunikatów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            //listBoxComunicates.Items.Clear();
            listBoxComunicates.DataSource = null;
        }

        private void backgroundWorkerUpdateInterface_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            listBoxComunicates.BeginInvoke(new Action(() => listBoxComunicates.Items.Add("sdfsdf")));
            //listBoxComunicates.Items.Add("Sdf");
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Tworzenie nowej maszyny w interfejcie
        /// </summary>
        /// <param name="currentCarWash"></param>
        public void AddMachineToInterface(CarWash currentCarWash)
        {
            GroupBox groupBoxMachine = new GroupBox();
            groupBoxMachine.Location = QueueSystem.pointsList[QueueSystem.carWashList.Count-1];
            groupBoxMachine.Name = "groupBoxMachine" + QueueSystem.carWashList.Count;
            groupBoxMachine.Size = new System.Drawing.Size(180, 250);
            groupBoxMachine.TabIndex = 1;
            groupBoxMachine.TabStop = false;
            groupBoxMachine.Text = string.Format("Maszyna{0} - {1}", (QueueSystem.carWashList.Count), string.Format("M/M/{0}/{1}/{2}", QueueSystem.GetLastAddedCarWash().ServicePlacesLength, QueueSystem.GetLastAddedCarWash().Algorithm, QueueSystem.GetLastAddedCarWash().QueueLength));
            
            QueueSystem.carWashList[QueueSystem.carWashList.Count - 1].GroupBox = groupBoxMachine;

            for (int i = 0; i < QueueSystem.GetLastAddedCarWash().ServicePlacesLength; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Location = new Point(10, 20 + 30*i);
                progressBar.Size = new System.Drawing.Size(100, 23);
                progressBar.Name = "progressBar" + (i+1).ToString();
                progressBar.TabIndex = 1;
                groupBoxMachine.Controls.Add(progressBar);
                currentCarWash.ServicePlaces.Add(new ServicePlace() {CurrentCar = null, ProgressBar = progressBar});
            }

            Label labelFirstMachineQueue = new Label();

            labelFirstMachineQueue.AutoSize = true;
            labelFirstMachineQueue.Location = new System.Drawing.Point(10, 135);
            labelFirstMachineQueue.Name = "labelFirstMachineQueue";
            labelFirstMachineQueue.Size = new System.Drawing.Size(45, 13);
            labelFirstMachineQueue.TabIndex = 1;
            labelFirstMachineQueue.Text = "Kolejka:";

            ListBox listBoxQueue = new ListBox();

            listBoxQueue.FormattingEnabled = true;
            listBoxQueue.Location = new Point(10, 150);
            listBoxQueue.Name = "listBoxFirstMachineQueue";
            listBoxQueue.Size = new Size(120, 95);
            listBoxQueue.TabIndex = 0;

            groupBoxMachine.Controls.Add(labelFirstMachineQueue);
            groupBoxMachine.Controls.Add(listBoxQueue);
            Controls.Add(groupBoxMachine);

            groupBoxMachine.ResumeLayout(false);
            groupBoxMachine.PerformLayout();

            currentCarWash.GroupBox = groupBoxMachine;
            currentCarWash.ListBox = listBoxQueue;
            currentCarWash.MachineName = groupBoxMachine.Text;

            DrawRelations(currentCarWash);
        }


        /// <summary>
        /// Załadowanie przykładu działania
        /// </summary>
        private void LoadExample()
        {
            /*
            List<Car> cars = new List<Car>();
            cars.Add(new Car(1, 60));
            cars.Add(new Car(2, 60));
            cars.Add(new Car(6, 60));
            cars.Add(new Car(10, 60));
            cars.Add(new Car(13, 60));
            cars.Add(new Car(90, 60));
            cars.Add(new Car(99, 60));

            int counter = 0;

            for (int i = 0; i < 60; i++)
            {
                if (cars[counter].ArrivalTime == i)
                {
                    if (_formQueueSystemsController.CheckFreeServicePlaces(
                            QueueSystem.carWashList[0]))
                    {
                        _formQueueSystemsController.AddApplicationToServicePlace(QueueSystem.carWashList[0], cars[counter]);
                    }
                    else
                    {                      
                        _formQueueSystemsController.AddApplicationToQueue(QueueSystem.carWashList[0].ListBox, cars[counter]);                
                        //backgroundWorkerUpdateInterface.RunWorkerAsync();
                    }

                    counter++;
                }

                foreach (ServicePlace ServicePlace in QueueSystem.carWashList[0].ServicePlaces)
                {
                    if (ServicePlace.CurrentCar != null)
                    {
                        ServicePlace.CurrentCar.ProgressWashingTime++;
                        _formQueueSystemsController.SetCarWashProgress(QueueSystem.carWashList[0], ServicePlace.CurrentCar);
                    }
                }           

                Thread.Sleep(300);
            }
            */
        }

        /// <summary>
        /// Odpalenie symulacji
        /// </summary>
        private void RunSimulation()
        {
            //QueueSystem.comunicates.Add(new Comunicates("IaN", 0));
            //QueueSystem.comunicates.Add(new Comunicates("IaN", 1));
            //QueueSystem.comunicates.Add(new Comunicates("IaN", 5));
            //QueueSystem.comunicates.Add(new Comunicates("IaN", 2));


            //Comunicates.sortComunicates();
            //Comunicates.getComunicateToHandle();
            //Comunicates.getComunicateToHandle();

            //if (QueueSystem.comunicates.Count == 0)
            //{
            //    QueueSystem.comunicates.Add(new Comunicates("IN", 0));
            //    listBoxComunicates.Items.Add(QueueSystem.comunicates[0].iComunicateTime + " - " + QueueSystem.comunicates[0].sComunicateContent);
            //}
            //else
            //{
            //    listBoxComunicates.Items.Add("TEST2");
            //}


            //while (true)
            //{
                if (!Comunicates.checkComunicateINExists())
                {
                    _formQueueSystemsController.generateNewCar();
                }

                handleComunicate();

                //if (_formQueueSystemsController.iActualTime > 5000)
                    //break;


                // sortuj
                listBoxComunicates.DataSource = null;
                listBoxComunicates.DataSource = QueueSystem.comunicatesUsed;
                listBoxComunicates.DisplayMember = "sComunicateContent";
            //}
        }

        public void DrawRelations(CarWash carWash)
        {
            foreach (InputOutput inputOutput in carWash.InputSystems)
            {
                if (inputOutput.CarWash == null && inputOutput.State.Equals(SystemState.Start.ToString()))
                {
                    DrawStartupRelations(carWash, inputOutput.Percent);
                    continue;
                }    

                if (inputOutput.CarWash != null)
                {
                    DrawFromMachineToMachine(inputOutput.CarWash, carWash, inputOutput.Percent);
                }
            }

            foreach (InputOutput inputOutput in carWash.OutputSystems)
            {
                if (inputOutput.CarWash == null && inputOutput.State.Equals(SystemState.End.ToString()))
                {
                    DrawEndRelations(carWash, inputOutput.Percent);
                    continue;
                }

                if (inputOutput.CarWash != null)
                {
                    DrawFromMachineToMachine(carWash, inputOutput.CarWash, inputOutput.Percent);
                }
            }
        }

        public void DrawStartupRelations(CarWash carWash, decimal percent)
        {
            int y = QueueSystem.startPoints[int.Parse(carWash.MachineName.Substring(7, 1)) - 1];
            DrawArrow(Constants.StartX, carWash.GroupBox.Location.Y + y, carWash.GroupBox.Location.X, carWash.GroupBox.Location.Y + y, percent);
        }

        public void DrawEndRelations(CarWash carWash, decimal percent)
        {
            int y = QueueSystem.endPoints[int.Parse(carWash.MachineName.Substring(7, 1)) - 1];
            DrawArrow(carWash.GroupBox.Location.X + 175, carWash.GroupBox.Location.Y + y, Constants.EndX, carWash.GroupBox.Location.Y + y, percent);
        }

        public void DrawFromMachineToMachine(CarWash carWashFrom, CarWash carWashTo, decimal percent)
        {
            int endX = 0, endY = 0;

            if ((int.Parse(carWashTo.MachineName.Substring(7, 1)) == 3 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 4 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 6) &&
               (int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 1 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 2 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 5))
            {
                endX = carWashTo.GroupBox.Location.X + 90;
                endY = carWashTo.GroupBox.Location.Y;
            }

            if ((int.Parse(carWashTo.MachineName.Substring(7, 1)) == 1 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 2 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 5) &&
               (int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 3 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 4 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 6))
            {
                endX = carWashTo.GroupBox.Location.X + 90;
                endY = carWashTo.GroupBox.Location.Y + 250;
            }

            if (((int.Parse(carWashTo.MachineName.Substring(7, 1)) == 1 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 2 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 5) &&
              (int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 1 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 2 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 5)) ||
              ((int.Parse(carWashTo.MachineName.Substring(7, 1)) == 3 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 4 || int.Parse(carWashTo.MachineName.Substring(7, 1)) == 6) &&
              (int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 3 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 4 || int.Parse(carWashFrom.MachineName.Substring(7, 1)) == 6))) 
            {
                if (carWashFrom.GroupBox.Location.X > carWashTo.GroupBox.Location.X)
                    endX = carWashTo.GroupBox.Location.X + 180;
                else
                    endX = carWashTo.GroupBox.Location.X;

                endY = carWashTo.GroupBox.Location.Y + 125; 
            }

            DrawArrow(carWashFrom.GroupBox.Location.X + 180 - 10, carWashFrom.GroupBox.Location.Y + 120, endX, endY, percent);
        }

        public void DrawArrow(int startX, int startY, int endX, int endY, decimal percent)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 5);
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, startX, startY, endX, endY);
            p.Dispose();

            if (startY - endY > 100)
                CreatePercentLabel(endX + 8, endY + 10, percent);
            else if ((endY - startY) < 50 && (endY - startY) > 20)
                CreatePercentLabel(endX + 20, endY - 40, percent);
            else if (endY - startY > 100)
                CreatePercentLabel(endX + 20, endY - 18, percent);
            else
                CreatePercentLabel(endX - 35, endY - 18, percent);
        }

        public void CreatePercentLabel(int x, int y, decimal percent)
        {
            Label percentLabel = new Label();
            percentLabel.Location = new Point(x, y);
            percentLabel.Text = string.Format("{0}%", percent);
            percentLabel.Name = percentLabel + x.ToString() + y.ToString() + percent;
            percentLabel.Size = new Size(40, 13);
            Controls.Add(percentLabel);
        }

        #endregion 
   
        public static void var_dump(object obj)
        {
            Console.WriteLine("{0,-18} {1}", "Name", "Value");
            string ln = @"-------------------------------------   
               ----------------------------";
            Console.WriteLine(ln);

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    Console.WriteLine("{0,-18} {1}",
                          props[i].Name, props[i].GetValue(obj, null));
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);   
                }
            }
            Console.WriteLine();
            //QueueSystem.comunicates.ForEach(i => Console.Write("{0}\n", i));
        }

        public void handleComunicate()
        {
            Comunicates actualComunicate = Comunicates.getComunicateToHandle();

            switch (actualComunicate.sComunicateType)
            {
                case "IN":
                    handleINComunicate(actualComunicate);
                    break;
                case "ADDED_TO_SERVICE_PLACE":
                    handleAddedToServicePlaceComunicate(actualComunicate);
                    break;
                case "SERVICE_PLACE_FINISHED":
                    handleServicePlaceFinishedComunicate(actualComunicate);
                    break;

            }

            //Thread.Sleep(100);
        }

        public void handleINComunicate(Comunicates actualComunicate)
        {
            // sprawdź gdzie auto może wyjść
            List<int> carWashesFromINState = QueueSystem.getPossibleMovesFromINState();
            /*
             *  @todo   dorobić losowanie z określonym prawdopodobieństwem
             *          wylosuj prawdopodobieństwo i wybierz maszynę
             */
            int choosenCarWash = carWashesFromINState[0];

            // jeśli jest miejsce w maszynie to obsłuż zadanie
            if (_formQueueSystemsController.CheckFreeServicePlaces(QueueSystem.carWashList[choosenCarWash]))
            {
                _formQueueSystemsController.AddApplicationToServicePlace(QueueSystem.carWashList[choosenCarWash], actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
            }
            else if (_formQueueSystemsController.CheckFreePlaceInQueue(QueueSystem.carWashList[choosenCarWash]))
            {
                _formQueueSystemsController.AddApplicationToQueue(QueueSystem.carWashList[choosenCarWash], actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
                //backgroundWorkerUpdateInterface.RunWorkerAsync();
            }
            else
            {
                _formQueueSystemsController.RemoveApplicationFromSystem(actualComunicate.oComunicateCar);
            }
        }

        public void handleAddedToServicePlaceComunicate(Comunicates actualComunicate)
        {
            // "odczekaj" czas mycia - tutaj pewnie podepnie się funkcjonalność progressBara
            actualComunicate.oComunicateCar.setPlannedWaitingTime();
            int time = actualComunicate.oComunicateCar.PlannedWaitingTime + actualComunicate.iComunicateTime;

            // usuwa zadanie z maszyny
            _formQueueSystemsController.RemoveApplicationFromServicePlace(actualComunicate.oComunicateCarWash, actualComunicate.oComunicateCar);
            // generuje komunikat
            _formQueueSystemsController.addComunicateToStack(new Comunicates("SERVICE_PLACE_FINISHED", time, actualComunicate.oComunicateCar, actualComunicate.oComunicateCarWash));
        }

        public void handleServicePlaceFinishedComunicate(Comunicates actualComunicate)
        {
            // sprawdź wszystkie możliwe wyjścia z maszyny
            List<InputOutput> outputCarWashes = actualComunicate.oComunicateCarWash.getOutputs();
            /*
             *  @todo   dorobić losowanie z określonym prawdopodobieństwem
             *          wylosuj prawdopodobieństwo i wybierz maszynę
             */
            //CarWash choosenCarWash = outputCarWashes[0].CarWash;
            CarWash choosenCarWash = QueueSystem.carWashList[1];

            // jeśli jest miejsce w maszynie to obsłuż zadanie
            if (_formQueueSystemsController.CheckFreeServicePlaces(choosenCarWash))
            {
                _formQueueSystemsController.AddApplicationToServicePlace(choosenCarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
            }
            else if (_formQueueSystemsController.CheckFreePlaceInQueue(choosenCarWash))
            {
                _formQueueSystemsController.AddApplicationToQueue(choosenCarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
                //backgroundWorkerUpdateInterface.RunWorkerAsync();
            }
            else
            {
                _formQueueSystemsController.RemoveApplicationFromSystem(actualComunicate.oComunicateCar);
            }

        }
    }
}
