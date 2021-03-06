﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3
{
    public partial class FormQueueSystems : Form
    {
        #region Public Fields

        public FormQueueSystemsController _formQueueSystemsController = null;

        #endregion
        #region Properties

        public ToolStripMenuItem SkasujModelToolStripMenuItem
        {
            get { return skasujModelToolStripMenuItem; }
        }

        public ToolStripMenuItem ResetujSymulacjęToolStripMenuItem
        {
            get { return resetujSymulacjęToolStripMenuItem; }
        }

        public BackgroundWorker BackgroundWorkerUpdateInterface
        {
            get { return backgroundWorkerUpdateInterface; }
        }

        public ErrorProvider ErrorProviderServiceTimeIntensity
        {
            get { return errorProviderServiceTimeIntensity; }
        }

        public ErrorProvider ErrorProviderStreamApplicationIntensity
        {
            get { return errorProviderStreamApplicationIntensity; }
        }

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

        public ListBox ListBoxArchiveComunicates
        {
            get { return listBoxArchiveComunicates; }
        }

        public Button ButtonStart
        {
            get { return buttonStart; }
        }

        #endregion
        #region Constructors

        public FormQueueSystems()
        {
            InitializeComponent();

            _formQueueSystemsController = new FormQueueSystemsController(this);
            resetujSymulacjęToolStripMenuItem.Enabled = false;        

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
            ReDrawAllRelations();
        }

        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMachine.AddMachine form = new AddMachine.AddMachine();
            form.ShowDialog();

            if (form.windowState.Equals(AddMachineStateWindow.ADD))
            {
                AddMachineToInterface(form._addMachineController.CurrentCarWash);
                _formQueueSystemsController.UpdateMachineName(QueueSystem.carWashList[QueueSystem.carWashList.Count-1]);
            }           

            if (QueueSystem.carWashList.Count == 6)
                dodajToolStripMenuItem.Enabled = false;
        }

        private void FormQueueSystems_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorkerUpdateInterface.IsBusy)
            {
                backgroundWorkerUpdateInterface.CancelAsync();
            }
        }

        private void zapiszModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.SaveConfiguration();
        }

        private void wczytajModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.LoadConfiguration();
            ReDrawAllRelations();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDownServiceIntensity_ValueChanged(object sender, EventArgs e)
        {
            QueueSystem.Lambda = int.Parse(numericUpDownServiceIntensity.Value.ToString());
            errorProviderServiceTimeIntensity.Clear();
        }

        private void numericUpDownStreamApplicationIntensity_ValueChanged(object sender, EventArgs e)
        {
            QueueSystem.Mi = int.Parse(numericUpDownStreamApplicationIntensity.Value.ToString());
            errorProviderStreamApplicationIntensity.Clear();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            backgroundWorkerUpdateInterface.DoWork += backgroundWorkerUpdateInterface_DoWork;
            _formQueueSystemsController.StartPauseSimulation();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker) delegate
            {
                listBoxComunicates.DataSource = null;
            });
        }

        private void backgroundWorkerUpdateInterface_DoWork(object sender, DoWorkEventArgs e)
        {
            _formQueueSystemsController.RunSimulation();
        }

        private void statystykiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.CreateStatisticsWindow();
        }

        #endregion
        #region Public Methods

        public void AddMachineToInterface(CarWash currentCarWash)
        {
            GroupBox groupBoxMachine = new GroupBox();
            groupBoxMachine.Location = QueueSystem.pointsList[QueueSystem.carWashList.Count-1];
            groupBoxMachine.Name = "groupBoxMachine" + QueueSystem.carWashList.Count;
            groupBoxMachine.Size = new Size(180, 250);
            groupBoxMachine.TabIndex = 1;
            groupBoxMachine.TabStop = false;
            groupBoxMachine.Text = string.Format("Maszyna{0} - {1}", (QueueSystem.carWashList.Count), string.Format("{0}/{1}/{2}", QueueSystem.GetLastAddedCarWash().ServicePlacesLength, QueueSystem.GetLastAddedCarWash().Algorithm, QueueSystem.GetLastAddedCarWash().QueueLength + QueueSystem.GetLastAddedCarWash().ServicePlacesLength));
            
            QueueSystem.carWashList[QueueSystem.carWashList.Count - 1].GroupBox = groupBoxMachine;

            for (int i = 0; i < QueueSystem.GetLastAddedCarWash().ServicePlacesLength; i++)
            {
                PercentProgressBar progressBar = new PercentProgressBar();
                progressBar.Location = new Point(10, 20 + 30*i);
                progressBar.Size = new Size(100, 23);
                progressBar.Name = "progressBar" + (i+1);
                progressBar.TabIndex = 1;
                groupBoxMachine.Controls.Add(progressBar);
                Label newLabel = new Label();
                newLabel.Location = new Point(120, 20 + 30*i + 5);
                newLabel.Name = string.Format("label{0} - {1}", i+1, currentCarWash.MachineName);
                newLabel.Text = string.Empty;
                groupBoxMachine.Controls.Add(newLabel);
                currentCarWash.ServicePlaces.Add(new ServicePlace() {CurrentCar = null, ProgressBar = progressBar, LabelCurrentCar = newLabel });
            }

            Label labelFirstMachineQueue = new Label();

            labelFirstMachineQueue.AutoSize = true;
            labelFirstMachineQueue.Location = new Point(10, 135);
            labelFirstMachineQueue.Name = "labelFirstMachineQueue";
            labelFirstMachineQueue.Size = new Size(45, 13);
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

        public void ReDrawAllRelations()
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                DrawRelations(carWash);
            }
        }

        public void DrawRelations(CarWash carWash)
        {
            foreach (InputOutput inputOutput in carWash.InputSystems)
            {
                if (inputOutput.CarWash == null && inputOutput.State.Equals(SystemState.Start.ToString()))
                {
                    DrawStartupRelations(carWash, inputOutput.Percent, ref inputOutput.PercentLabel);
                    continue;
                }    

                if (inputOutput.CarWash != null)
                {
                    DrawFromMachineToMachine(inputOutput.CarWash, carWash, inputOutput.Percent, ref inputOutput.PercentLabel);
                }
            }

            foreach (InputOutput inputOutput in carWash.OutputSystems)
            {
                if (inputOutput.CarWash == null && inputOutput.State.Equals(SystemState.End.ToString()))
                {
                    DrawEndRelations(carWash, inputOutput.Percent, ref inputOutput.PercentLabel);
                    continue;
                }

                if (inputOutput.CarWash != null)
                {
                    DrawFromMachineToMachine(carWash, inputOutput.CarWash, inputOutput.Percent, ref inputOutput.PercentLabel);
                }
            }
        }

        public void DrawStartupRelations(CarWash carWash, decimal percent, ref Label percentLabel)
        {
            int y = QueueSystem.startPoints[int.Parse(carWash.MachineName.Substring(7, 1)) - 1];
            DrawArrow(Constants.StartX, carWash.GroupBox.Location.Y + y, carWash.GroupBox.Location.X, carWash.GroupBox.Location.Y + y, percent, ref percentLabel);
        }

        public void DrawEndRelations(CarWash carWash, decimal percent, ref Label percentLabel)
        {
            int y = QueueSystem.endPoints[int.Parse(carWash.MachineName.Substring(7, 1)) - 1];
            DrawArrow(carWash.GroupBox.Location.X + 175, carWash.GroupBox.Location.Y + y, Constants.EndX, carWash.GroupBox.Location.Y + y, percent, ref percentLabel);
        }

        public void DrawFromMachineToMachine(CarWash carWashFrom, CarWash carWashTo, decimal percent, ref Label percentLabel)
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

            DrawArrow(carWashFrom.GroupBox.Location.X + 180 - 10, carWashFrom.GroupBox.Location.Y + 120, endX, endY, percent, ref percentLabel);
        }

        public void DrawArrow(int startX, int startY, int endX, int endY, decimal percent, ref Label percentLabel)
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black, 5);
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(p, startX, startY, endX, endY);
            p.Dispose();

            if (startY - endY > 100)
                CreatePercentLabel(endX + 8, endY + 10, percent, ref percentLabel);
            else if ((endY - startY) < 50 && (endY - startY) > 20)
                CreatePercentLabel(endX + 20, endY - 40, percent, ref percentLabel);
            else if (endY - startY > 100)
                CreatePercentLabel(endX + 20, endY - 18, percent, ref percentLabel);
            else
                CreatePercentLabel(endX - 35, endY - 18, percent, ref percentLabel);
        }

        public void CreatePercentLabel(int x, int y, decimal percent, ref Label percentLabel)
        {
            percentLabel = new Label();
            percentLabel.Location = new Point(x, y);
            percentLabel.Text = string.Format("{0}%", percent);
            percentLabel.Name = "PERCENTLABEL";
            percentLabel.Size = new Size(40, 13);
            Controls.Add(percentLabel);
        }
   
        private void oProgramieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _formQueueSystemsController.CreateAboutBox();
        }

        private void buttonClearArchiveComunicates_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                listBoxArchiveComunicates.DataSource = null;
            });
        }

        private void resetujSymulacjęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetujSymulacjęToolStripMenuItem.Enabled = false;
            _formQueueSystemsController.simulationStarted = false;
            ResetSimulation();
        }

        private void skasujModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteModel();
        }

        public void ResetSimulation()
        {
            _formQueueSystemsController.reset = true;

            if (backgroundWorkerUpdateInterface.IsBusy)
            {
                backgroundWorkerUpdateInterface.DoWork -= backgroundWorkerUpdateInterface_DoWork;
                backgroundWorkerUpdateInterface.WorkerSupportsCancellation = true;
                backgroundWorkerUpdateInterface.CancelAsync();
            }

            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                carWash.AbsoluteSystemAbility = 0;
                carWash.ChartData.Clear();
                carWash.MeanNumberOfApplicationInQueue = 0;
                carWash.MeanTimeApplicationInQueue = 0;
                carWash.RelativeSystemAbility = 0;
                carWash.numberOfCarsInQueueTotal = 0;
                carWash.numberOfCarsTotal = 0;
                carWash.timeTotal = 0;
                carWash.numberOfSucceeded = 0;
                _formQueueSystemsController.iActualTime = 0;

                foreach (ServicePlace servicePlace in carWash.ServicePlaces)
                {
                    servicePlace.CurrentCar = null;
                    servicePlace.LabelCurrentCar.Text = string.Empty;
                    servicePlace.ProgressBar.Value = 0;
                    servicePlace.ProgressBar.Maximum = 0;
                }

                Invoke((MethodInvoker)delegate
                {
                    carWash.ListBox.Items.Clear();
                    foreach (ServicePlace servicePlace in carWash.ServicePlaces)
                    {
                        servicePlace.ProgressBar.Value = 0;
                    }
                });
            }

            QueueSystem.comunicates.Clear();
            QueueSystem.comunicatesUsed.Clear();
            Car.IdCounter = 1;
            Invoke((MethodInvoker)delegate
            {
                listBoxArchiveComunicates.DataSource = null;
                listBoxComunicates.DataSource = null;
                buttonStart.Text = "START";
            });
        }

        public void DeleteModel()
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                foreach (InputOutput input in carWash.InputSystems)
                {
                    if (input.PercentLabel != null)
                        input.PercentLabel.Dispose();
                }
                foreach (InputOutput output in carWash.OutputSystems)
                {
                    if (output.PercentLabel != null)
                        output.PercentLabel.Dispose();
                }

                carWash.GroupBox.Dispose();
            }

            this.Paint -= FormQueueSystems_Paint;
            this.Invalidate();

            QueueSystem.carWashList.Clear();
            QueueSystem.FillListPoints();
            QueueSystem.FillStartPoints();
            QueueSystem.FillEndPoints();
            this.Paint += FormQueueSystems_Paint;
        }

        #endregion         
    }
}
