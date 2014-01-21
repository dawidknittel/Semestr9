using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Kolejki_LAB3.AboutWindow;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3
{
    public class FormQueueSystemsController
    {
        #region Private Fields

        private BasicConfiguration basicConfiguration = null;
        private ProgressBar currentProgressBar = null;
        public StatisticsWindow statisticsWindow = null;
        private List<SystemConfiguration> systemConfiguration = null; 
        private FormQueueSystems View = null;
        private bool noFile = false;
        private Random gen = null;
        public int iActualTime = 0;
        public bool reset = false;
        private bool pause = false;
        public bool simulationStarted = false;

        private const string FILE_NAME = "ConfigurationData.xml";

        #endregion
        #region Constructors

        public FormQueueSystemsController(FormQueueSystems view)
        {
            View = view;
            gen = new Random();
        }

        #endregion
        #region Public Methods

        public void CreateStatisticsWindow()
        {
            statisticsWindow = new StatisticsWindow(View);
            statisticsWindow.Show();
        }

        public void CreateAboutBox()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        public void LoadBasicConfiguration()
        {
            try
            {
                XDocument configurationData = XDocument.Load(FILE_NAME);
                basicConfiguration = (from configuration in configurationData.Root.Elements("BasicConfiguration")
                                      select new BasicConfiguration(
                                              int.Parse(configuration.Element("lambda").Value.ToString()),
                                              int.Parse(configuration.Element("mi").Value.ToString()),
                                              int.Parse(configuration.Element("systemQuantity").Value.ToString()))).FirstOrDefault();
            }
            catch (Exception)
            {
                MessageBox.Show("Brak pliku konfiguracyjnego lub plik jest uszkodzony", "Brak pliku",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                noFile = true;
            }
           
        }

        public void StartPauseSimulation()
        {
            if (int.Parse(View.NumericUpDownServiceIntensity.Value.ToString()) == 0)
            {
                View.ErrorProviderServiceTimeIntensity.SetError(View.NumericUpDownServiceIntensity, "Nie wybrano wartości");
                return;
            }

            if (int.Parse(View.NumericUpDownStreamApplicationIntensity.Value.ToString()) == 0)
            {
                View.ErrorProviderStreamApplicationIntensity.SetError(View.NumericUpDownStreamApplicationIntensity, "Nie wybrano wartośći");
                return;
            }

            if (View.ButtonStart.Text.Equals("START"))
            {
                if (QueueSystem.carWashList.Count > 0)
                {
                    if (!simulationStarted)
                    {
                        reset = false;
                        View.BackgroundWorkerUpdateInterface.RunWorkerAsync();                  
                    }

                    View.Invoke((MethodInvoker)delegate { pause = false; });
                    simulationStarted = true;
                    View.ButtonStart.Text = "ZATRZYMAJ";
                    View.SkasujModelToolStripMenuItem.Enabled = false;
                    View.ResetujSymulacjęToolStripMenuItem.Enabled = true;
                }
            }
            else if (View.BackgroundWorkerUpdateInterface.IsBusy)
            {
                View.Invoke((MethodInvoker)delegate { pause = true; });
                View.ButtonStart.Text = "START";
            }
        }

        public void RunSimulation()
        {
            while (true)
            {
                if (reset)
                {
                    View.ResetSimulation();
                    break;
                }

                if (pause)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    if (!Comunicates.checkComunicateINExists())
                    {
                        generateNewCar();
                    }

                    // obsługa komunikatu
                    handleComunicate();
                    UpdateChartData(iActualTime);               
                    UpdateStatistics();                  

                    View.Invoke((MethodInvoker)delegate
                    {
                        Comunicates.sortComunicates();
                        View.ListBoxComunicates.DataSource = null;
                        View.ListBoxComunicates.DataSource = QueueSystem.comunicates;
                        View.ListBoxComunicates.DisplayMember = "sComunicateContent";
                    });

                    View.Invoke((MethodInvoker)delegate
                    {
                        Comunicates.sortComunicates();
                        View.ListBoxArchiveComunicates.DataSource = null;
                        View.ListBoxArchiveComunicates.DataSource = QueueSystem.comunicatesUsed;
                        View.ListBoxArchiveComunicates.DisplayMember = "sComunicateContent";
                        View.ListBoxArchiveComunicates.SelectedIndex = View.ListBoxArchiveComunicates.Items.Count - 1;
                    });

                    Thread.Sleep(500);
                }
            }
        }

        public void UpdateStatistics()
        {
            if (statisticsWindow != null)
            {
                View.Invoke((MethodInvoker)delegate
                {
                    string machineName = string.Empty;

                    if (string.IsNullOrEmpty(statisticsWindow.ComboBoxMachinseName.Text))
                    {
                        machineName = QueueSystem.carWashList[0].MachineName;
                        statisticsWindow.ComboBoxMachinseName.Text = machineName;
                    }
                    else
                    {
                        machineName = statisticsWindow.ComboBoxMachinseName.Text;
                    }

                    statisticsWindow.StatisticsController.ShowStatistics(machineName);
                    statisticsWindow.StatisticsController.FillChartWithData();
                });
            }         
        }

        public void UpdateChartData(int time)
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                if (carWash.ChartData.Count >= 1000)
                {
                    carWash.ChartData.RemoveAt(0);  
                }

                carWash.ChartData.Add(new MeanTimeInQueue()
                {
                    CurrentTime = time,
                    MeanTime = CarWash.calculateMeanTimeApplicationInQueue(carWash)              
                });

                carWash.ChartData = carWash.ChartData.OrderBy(o => o.CurrentTime).ToList();
            }
        }

        public void handleComunicate()
        {
            Comunicates actualComunicate = Comunicates.getComunicateToHandle();

            // ustaw total time myjni
            if (actualComunicate.oComunicateCarWash != null)
                actualComunicate.oComunicateCarWash.timeTotal = iActualTime;

            // zaznacza aktualny komunikat w listboxie
            View.Invoke((MethodInvoker)delegate
            {
                View.ListBoxComunicates.SelectedItem = actualComunicate;
            });


            switch (actualComunicate.sComunicateType)
            {
                case "IN":
                    handleINComunicate(actualComunicate);
                    break;
                case "ADDED_TO_SERVICE_PLACE":
                case "GET_FROM_QUEUE":
                    handleAddedToServicePlaceComunicate(actualComunicate);
                    break;
                case "SERVICE_PLACE_FINISHED":
                    handleServicePlaceFinishedComunicate(actualComunicate);
                    break;

            }
        }

        /// <summary>
        /// Obsługa komunkatów 
        /// </summary>
        /// <param name="actualComunicate"></param>
        public void handleINComunicate(Comunicates actualComunicate)
        {
            // sprawdź gdzie auto może wyjść
            List<CarWash> carWashesFromINState = QueueSystem.getPossibleMovesFromINState();
            int choosenCarWash = QueueSystem.chooseMoveFromStartState(carWashesFromINState);

            // jeśli jest miejsce w maszynie to obsłuż zadanie
            if (CheckFreeServicePlaces(QueueSystem.carWashList[choosenCarWash]))
            {
                AddApplicationToServicePlace(QueueSystem.carWashList[choosenCarWash], actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
            }
            else if (CheckFreePlaceInQueue(QueueSystem.carWashList[choosenCarWash]))
            {
                AddApplicationToQueue(QueueSystem.carWashList[choosenCarWash], actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
            }
            else
            {
                // zwieksza licznik wszystkich aut myjni
                QueueSystem.carWashList[choosenCarWash].numberOfCarsTotal++;
                // zwieksza licznik nieobsłużonych aut myjni
                QueueSystem.carWashList[choosenCarWash].numberOfFailed++;

                RemoveApplicationFromSystem(actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
            }
        }

        public void handleAddedToServicePlaceComunicate(Comunicates actualComunicate)
        {
            // "odczekaj" czas mycia
            actualComunicate.oComunicateCar.setPlannedWaitingTime();

            foreach (ServicePlace servicePlace in actualComunicate.oComunicateCarWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar == actualComunicate.oComunicateCar)
                {
                    for (int i = 1; i <= actualComunicate.oComunicateCar.PlannedWaitingTime; i++)
                    {
                        View.Invoke((MethodInvoker)delegate
                        {
                            currentProgressBar = servicePlace.ProgressBar;
                            servicePlace.ProgressBar.Maximum = actualComunicate.oComunicateCar.PlannedWaitingTime;
                            servicePlace.ProgressBar.Minimum = 0;
                            servicePlace.ProgressBar.Step = 1;
                            servicePlace.ProgressBar.PerformStep();                          
                            decimal percent = Math.Round((Convert.ToDecimal(currentProgressBar.Value) / Convert.ToDecimal(currentProgressBar.Maximum) * 100), 2);
                            servicePlace.ProgressBar.Paint += new PaintEventHandler((object sender, PaintEventArgs e) =>
                                e.Graphics.DrawString(percent + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(servicePlace.ProgressBar.Width / 2 - 10, servicePlace.ProgressBar.Height / 2 - 7)));
                            
                        });                       
                    }

                    break;
                }

            }

            int time = actualComunicate.oComunicateCar.PlannedWaitingTime + actualComunicate.iComunicateTime;

            // generuje komunikat
            addComunicateToStack(new Comunicates("SERVICE_PLACE_FINISHED", time, actualComunicate.oComunicateCar, actualComunicate.oComunicateCarWash));
        }

        public void ProgressBarPercent(object sender, PaintEventArgs e)
        {
            View.Invoke((MethodInvoker)delegate
            {
                decimal percent = Math.Round((Convert.ToDecimal(currentProgressBar.Value) / Convert.ToDecimal(currentProgressBar.Maximum) * 100), 2);
                currentProgressBar.CreateGraphics().DrawString(percent + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(currentProgressBar.Width / 2 - 10, currentProgressBar.Height / 2 - 7));
            });
        }

        public void handleServicePlaceFinishedComunicate(Comunicates actualComunicate)
        {

            // sprawdź wszystkie możliwe wyjścia z maszyny
            List<InputOutput> outputCarWashes = actualComunicate.oComunicateCarWash.getOutputs();
            InputOutput choosenCarWashOutput = QueueSystem.chooseMoveFromMachine(outputCarWashes);

            // jeśli wylosowany został stan końcowy
            if (choosenCarWashOutput.CarWash == null && choosenCarWashOutput.State == "End")
            {
                // usuwa zadanie z maszyny
                RemoveApplicationFromServicePlace(actualComunicate.oComunicateCarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);

                // generuje komunikat
                addComunicateToStack(new Comunicates("OUT", actualComunicate.iComunicateTime, actualComunicate.oComunicateCar, actualComunicate.oComunicateCarWash));

            }
            else
            {
                // jeśli jest miejsce w maszynie to obsłuż zadanie
                if (CheckFreeServicePlaces(choosenCarWashOutput.CarWash))
                {
                    // usuwa zadanie z maszyny
                    RemoveApplicationFromServicePlace(
                        actualComunicate.oComunicateCarWash,
                        actualComunicate.oComunicateCar,
                        actualComunicate.iComunicateTime);

                    // dodaje nowe do stanowiska obsługi
                    AddApplicationToServicePlace(choosenCarWashOutput.CarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
                }
                else if (CheckFreePlaceInQueue(choosenCarWashOutput.CarWash))
                {
                    // usuwa zadanie z maszyny
                    RemoveApplicationFromServicePlace(actualComunicate.oComunicateCarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);

                    // dodaje nowe do kolejki
                    AddApplicationToQueue(choosenCarWashOutput.CarWash, actualComunicate.oComunicateCar, actualComunicate.iComunicateTime);
                }
                else
                {
                    actualComunicate.oComunicateCar.setiTimeWaitingInMachine(actualComunicate.iComunicateTime);
                }
            }

        }

        public void LoadSystems()
        {
            try
            {
                XmlDocument configurationData = new XmlDocument();
                configurationData.Load(FILE_NAME);
                XmlNodeList systems = configurationData.SelectNodes("//System");
                foreach (XmlNode system in systems)
                {
                    CarWash carWash = new CarWash()
                    {
                        ServicePlacesLength = int.Parse(system.ChildNodes[0].InnerText),
                        Algorithm = SelectAlgorithm(system.ChildNodes[1].InnerText),
                        QueueLength = int.Parse(system.ChildNodes[2].InnerText),            
                        MachineName = system.ChildNodes[3].InnerText
                    };

                    XmlNodeList inputs = system.ChildNodes;

                    if (inputs != null)
                    {
                        foreach (XmlNode inputOutput in inputs)
                        {
                            if (inputOutput.Name.Equals("Input"))
                            {
                                carWash.InputSystems.Add(new InputOutput()
                                {
                                    MachineName = inputOutput.ChildNodes[0].InnerText,
                                    Percent = int.Parse(inputOutput.ChildNodes[1].InnerText),
                                    State = inputOutput.ChildNodes[2].InnerText
                                });
                            }
                        }
                    }

                    XmlNodeList outputs = system.ChildNodes;

                    if (inputs != null)
                    {
                        foreach (XmlNode inputOutput in outputs)
                        {
                            if (inputOutput.Name.Equals("Output"))
                            {
                                carWash.OutputSystems.Add(new InputOutput()
                                {
                                    MachineName = inputOutput.ChildNodes[0].InnerText,
                                    Percent = int.Parse(inputOutput.ChildNodes[1].InnerText),
                                    State = inputOutput.ChildNodes[2].InnerText
                                });
                            }
                        }
                    }

                    QueueSystem.carWashList.Add(carWash);
                    View.AddMachineToInterface(carWash);
                }

                QueueSystem.UpdateCarWashReferenceAccordingToName();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak pliku konfiguracyjnego lub plik jest uszkodzony", "Brak pliku",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                noFile = true;
            }
           
        }

        /// <summary>
        /// Ustawienie interfejsu według załadowanych danych
        /// </summary>
        public void SetLoadedConfiguration()
        {
            View.NumericUpDownServiceIntensity.Value = basicConfiguration.Lambda;
            View.NumericUpDownStreamApplicationIntensity.Value = basicConfiguration.Mi;
        }

        public void LoadConfiguration()
        {
            LoadBasicConfiguration();

            if (!noFile)
                SetLoadedConfiguration();

            if (!noFile && basicConfiguration.SystemQuantity > 0)
                LoadSystems();
        }

        public void SaveConfiguration()
        {
            FileStream fs = null;

            if (!(File.Exists("ConfigurationData.xml")))
            {
                using (fs = File.Create("ConfigurationData.xml"))
                {
                }
            }

            var configurationXml = new XDocument();
            var rootElement = new XElement("Configurations");
            configurationXml.Add(rootElement);

            var basicConf = new XElement("BasicConfiguration");
            var lambda = new XElement("lambda", QueueSystem.Lambda);
            basicConf.Add(lambda);
            var mi = new XElement("mi", QueueSystem.Mi);
            basicConf.Add(mi);
            var systemQuantity = new XElement("systemQuantity", QueueSystem.carWashList.Count);
            basicConf.Add(systemQuantity);
            rootElement.Add(basicConf);

            if (QueueSystem.carWashList.Count != 0)
            {
                foreach (CarWash carWash in QueueSystem.carWashList)
                {
                    var confName = new XElement("System");
                    var servicePlacesQuantity = new XElement("servicePlacesQuantity", carWash.ServicePlacesLength);
                    confName.Add(servicePlacesQuantity);
                    var algorithm = new XElement("algorithm", carWash.Algorithm);
                    confName.Add(algorithm);
                    var queueLength = new XElement("queueLength", carWash.QueueLength);
                    confName.Add(queueLength);
                    var machineName = new XElement("machineName", carWash.MachineName);
                    confName.Add(machineName);
                    rootElement.Add(confName);

                    foreach (InputOutput inputOutput in carWash.InputSystems)
                    {
                        var input = new XElement("Input");
                        XElement relatedInputMachine = null;

                        if (inputOutput.CarWash != null)
                            relatedInputMachine = new XElement("relatedInputMachine", inputOutput.CarWash.MachineName);
                        else
                            relatedInputMachine = new XElement("relatedInputMachine", SystemState.None.ToString());

                        input.Add(relatedInputMachine);
                        var relatedPercent = new XElement("relatedPercent", inputOutput.Percent);
                        input.Add(relatedPercent);
                        var machineSystemState = new XElement("machineSystemState", inputOutput.State);
                        input.Add(machineSystemState);
                        confName.Add(input);
                    }

                    foreach (InputOutput inputOutput in carWash.OutputSystems)
                    {
                        var input = new XElement("Output");
                        XElement relatedOutputMachine = null;

                        if (inputOutput.CarWash != null)
                            relatedOutputMachine = new XElement("relatedOutputMachine", inputOutput.CarWash.MachineName);
                        else
                            relatedOutputMachine = new XElement("relatedOutputMachine", SystemState.None.ToString());

                        input.Add(relatedOutputMachine);
                        var relatedPercent = new XElement("relatedPercent", inputOutput.Percent);
                        input.Add(relatedPercent);
                        var machineSystemState = new XElement("machineSystemState", inputOutput.State);
                        input.Add(machineSystemState);
                        confName.Add(input);
                    }
                }
            }

            configurationXml.Save(FILE_NAME);
        }

        /// <summary>
        /// Dodanie zgłoszenia do stanowiska obsługi(myjni), Podaje się obiekt konkretnej myjni oraz samochód który ma wejść
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void AddApplicationToServicePlace(CarWash carWash, Car car, int time, bool bFromQueue = false)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar == null)
                {
                    servicePlace.CurrentCar = car;
                    View.Invoke((MethodInvoker)delegate 
                    {  
                        servicePlace.ProgressBar.Maximum = car.PlannedWaitingTime;
                        servicePlace.ProgressBar.Minimum = 0;
                        servicePlace.ProgressBar.Step = 1;
                        servicePlace.ProgressBar.PerformStep();                        
                        servicePlace.LabelCurrentCar.Text = car.IdCar.ToString();
                        //decimal percent = 0;
                        //servicePlace.ProgressBar.CreateGraphics().DrawString(percent + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(servicePlace.ProgressBar.Width / 2 - 10, servicePlace.ProgressBar.Height / 2 - 7));
                    });

                    // generuje komunikat
                    string comTxt = "ADDED_TO_SERVICE_PLACE";
                    if (bFromQueue)
                        comTxt = "GET_FROM_QUEUE";
                    addComunicateToStack(new Comunicates(comTxt, time, car, carWash));

                    // zwieksza licznik wszystkich aut myjni
                    carWash.numberOfCarsTotal++;
                    // zwiększenie licznika obsłużonych aut
                    carWash.numberOfSucceeded++;
                    break;
                }
            }
        }

        /// <summary>
        /// Sprawdzenie czy któreś stawisko obłsugi jest wolne, jeśli tak to zwróć true
        /// </summary>
        /// <param name="carWash"></param>
        /// <returns></returns>
        public bool CheckFreeServicePlaces(CarWash carWash)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar == null)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Sprawdza czy jest miejsce w kolejce
        /// </summary>
        /// <param name="carWash"></param>
        /// <returns></returns>
        public bool CheckFreePlaceInQueue(CarWash carwash)
        {
            if (carwash.ListBox.Items.Count < (carwash.QueueLength))
                return true;

            return false;
        }

        /// <summary>
        /// Sprawdza czy zdarzenia czekają w kolejce
        /// </summary>
        /// <param name="carwash"></param>
        /// <returns></returns>
        public bool checkCarWashHasQueue(CarWash carwash)
        {
            if (carwash.ListBox.Items.Count > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Sprawdza czy w którymkolwiek wejściu do danej maszyny czeka samochód na obsługę
        /// </summary>
        /// <param name="carwash"></param>
        /// <returns></returns>
        public void checkCarWashWaitingApplications(CarWash carwash, int time)
        {
            foreach (InputOutput input in carwash.InputSystems)
            {
                if (input.CarWash != null) {
                    foreach (ServicePlace servicePlace in input.CarWash.ServicePlaces)
                    {
                        if ((servicePlace.CurrentCar != null) && (servicePlace.CurrentCar.iTimeWaitingInMachine != 0))
                        {
                            Car tempCar = servicePlace.CurrentCar;
                            // wyzeruj czas czekania na zwolnienie maszyny
                            servicePlace.CurrentCar.iTimeWaitingInMachine = 0;
                            
                            // usuwa zadanie z maszyny
                            RemoveApplicationFromServicePlace(input.CarWash, tempCar, time);

                            // dodaj samochód do kolejki
                            AddApplicationToQueue(carwash, tempCar, time);

                            return;
                        }
                    }
                }
               
            }
        }

        /// <summary>
        /// Usuń samochód ze stanowiska obsługi
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void RemoveApplicationFromServicePlace(CarWash carWash, Car car, int time)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if ((servicePlace.CurrentCar != null) && (servicePlace.CurrentCar.Equals(car)))
                {
                    servicePlace.CurrentCar = null;                  

                    View.Invoke((MethodInvoker)delegate
                    {
                        servicePlace.ProgressBar.Value = 0;
                        servicePlace.LabelCurrentCar.Text = string.Empty;
                    });

                    break;
                }
            }

            // sprawdź czy w maszynie są zgłoszenia w kolejce
            if (checkCarWashHasQueue(carWash))
            {
                // @todo rozróżnic algorytmy maszyn, na razie tylko dla FIFO
                Car carFromQueue = getApplicationFromQueue(carWash);

                // usuń samochód z kolejki
                RemoveApplicationFromQueue(carWash, carFromQueue,time);

                // dodaj samochód z kolejki na stanowisko obsługi
                AddApplicationToServicePlace(carWash, carFromQueue, time, true);
            }
        }

        /// <summary>
        /// Dodaj samochód do kolejki
        /// </summary>
        /// <param name="listBoxQueue"></param>
        /// <param name="car"></param>
        public void AddApplicationToQueue(CarWash carWash, Car car, int time)
        {
           
            View.Invoke((MethodInvoker) delegate { carWash.ListBox.Items.Add(car.IdCar.ToString()); });

            // generuje komunikat
            addComunicateToStack(new Comunicates("ADDED_TO_QUEUE", time, car, carWash));

            // zwieksza licznik wszystkich aut czekających w kolejce w danej myjni
            carWash.numberOfCarsInQueueTotal++;

            // ustawia czas wejścia do kolejki
            car.iTimeInQueueStart = time;
            
        }

        /// <summary>
        /// Usuń samochód z kolejki
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void RemoveApplicationFromQueue(CarWash carWash, Car car, int time)
        {
            foreach (var item in carWash.ListBox.Items)
            {
                if (item.ToString().Equals(car.IdCar.ToString()))
                {
                    View.Invoke((MethodInvoker) delegate {
                        carWash.ListBox.Items.Remove(item);
                    });
                    break;
                }
            }

            // zwieksza licznik czasu spędzonego przez wszystkie auta w kolejce
            carWash.timeInQueueTotal = carWash.timeInQueueTotal + (time - car.iTimeInQueueStart);

            // licznik sredniej dlugosci kolejki
            carWash.MeanNumberOfApplicationInQueue += (carWash.ListBox.Items.Count + 1);

            // zeruje czas wejścia do kolejki danego auta
            car.iTimeInQueueStart = 0;

            checkCarWashWaitingApplications(carWash, time);

        }

        /// <summary>
        /// Pobiera samochód z kolejki do dalszej obsługi
        /// </summary>
        /// <param name="carWash"></param>
        /// <returns></returns>
        public Car getApplicationFromQueue(CarWash carWash)
        {
            int carId = 0;

            if (carWash.ListBox.Items.Count == 1)
            {
                carId = Convert.ToInt32(carWash.ListBox.Items[0].ToString());
            }
            else
            {
                switch (carWash.Algorithm)
                {
                    case Algorithm.FIFO:
                        carId = Convert.ToInt32(carWash.ListBox.Items[0].ToString());
                        break;
                    case Algorithm.LIFO:
                        carId = Convert.ToInt32(carWash.ListBox.Items[carWash.ListBox.Items.Count - 1].ToString());
                        break;
                    case Algorithm.RSS:                       
                        int choosen = gen.Next(0, carWash.ListBox.Items.Count - 1);
                        carId = Convert.ToInt32(carWash.ListBox.Items[choosen].ToString());
                        break;
                }
            }

            Car car = Car.findCar(carId);

            return car;
        }

        /// <summary>
        /// Usuwa zadanie z systemu w momencie gdy nie ma wolnych miejsc w maszynach wychodzących ze stanu START (lub "IN")
        /// </summary>
        /// <param name="car"></param>
        public void RemoveApplicationFromSystem(Car car, int time)
        {
            // generuje komunikat
            addComunicateToStack(new Comunicates("REMOVED_FROM_SYSTEM", time, car));

            // jako że jest to informacja o usunięciu zadania z systemu nie potrzebujemy jej obsługiwać
            Comunicates.markComunicateAsUsed(QueueSystem.comunicates.Count - 1);
        }

        public void Comunicate(string comunicate)
        {
            View.Invoke((MethodInvoker)delegate {  
                 View.ListBoxComunicates.Items.Add(comunicate);
            });
        }

        /// <summary>
        /// Aktualizuj stan progressbara (myjni)
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void SetCarWashProgress(CarWash carWash, Car car)      
        {
            View.Invoke((MethodInvoker)delegate {
                ProgressBar currentProgressBar = GetProgressBar(carWash, car);
                currentProgressBar.PerformStep();
                //decimal percent = Math.Round(Convert.ToDecimal((currentProgressBar.Value / currentProgressBar.Maximum) * 100), 2);
                //currentProgressBar.CreateGraphics().DrawString(percent + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(currentProgressBar.Width / 2 - 10, currentProgressBar.Height / 2 - 7));
            }); 
        }

        public void DrawStartupLine()
        {
            Graphics g = View.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.SaddleBrown, 5);
            g.DrawLine(p, 10, 30, 10, 650);
            p.Dispose();
        }

        public void DrawEndLine()
        {
            Graphics g = View.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.SaddleBrown, 5);
            g.DrawLine(p, 1010, 30, 1010, 650);
            p.Dispose();
        }

        public void UpdateMachineName(CarWash currentCarWash)
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                foreach (InputOutput input in carWash.InputSystems)
                {
                    if (input.State.Equals(SystemState.None.ToString()) && input.CarWash == null)
                    {
                        input.CarWash = currentCarWash;
                    }
                }

                foreach (InputOutput output in carWash.OutputSystems)
                {
                    if (output.State.Equals(SystemState.None.ToString()) && output.CarWash == null)
                    {
                        output.CarWash = currentCarWash;
                    }
                }
            }
        }

        public void generateNewCar()
        {
            iActualTime += Convert.ToInt32(TestSimpleRNG.SimpleRNG.GetExponential(QueueSystem.Lambda));

            Car newCar = new Car(iActualTime, 0);
            QueueSystem.cars.Add(newCar);
            addComunicateToStack(new Comunicates("IN", newCar.ArrivalTime, newCar));
        }

        public void addComunicateToStack(Comunicates com)
        {
            if (QueueSystem.comunicates.Count > 100)
            {
                QueueSystem.comunicates.RemoveAt(0);
            }

            QueueSystem.comunicates.Add(com);
        }

        #endregion
        #region Private Methods

        private ProgressBar GetProgressBar(CarWash carWash, Car car)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar.Equals(car))
                    return servicePlace.ProgressBar;
            }

            return null;
        }

        private Algorithm SelectAlgorithm(string selectedAlgoritm)
        {
            if (selectedAlgoritm.Equals(Algorithm.FIFO.ToString()))
                return Algorithm.FIFO;
            if (selectedAlgoritm.Equals(Algorithm.LIFO.ToString()))
                return Algorithm.LIFO;
            if (selectedAlgoritm.Equals(Algorithm.RSS.ToString()))
                return Algorithm.RSS;

            return Algorithm.NONE;
        }

        #endregion
    }
}
