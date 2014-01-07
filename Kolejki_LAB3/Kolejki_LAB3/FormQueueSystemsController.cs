using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Kolejki_LAB3.AboutWindow;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3
{
    class FormQueueSystemsController
    {
        #region Private Fields

        private BasicConfiguration basicConfiguration = null;
        private List<SystemConfiguration> systemConfiguration = null; 
        private FormQueueSystems View = null;
        private bool noFile = false;
        private Random gen = null;
        public int iActualTime = 0;

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
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.ShowDialog();
        }

        public void CreateAboutBox()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// Załadowanie podstawowych danych z pliku
        /// </summary>
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

         /// <summary>
         /// Załadowanie maszyn z pliku
         /// </summary>
        public void LoadSystems()
        {
            //systemConfiguration = new List<SystemConfiguration>();

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

        /// <summary>
        /// Załadowanie danych z pliku
        /// </summary>
        public void LoadConfiguration()
        {
            LoadBasicConfiguration();

            if (!noFile)
                SetLoadedConfiguration();

            if (!noFile && basicConfiguration.SystemQuantity > 0)
                LoadSystems();
        }

        /// <summary>
        /// Zapisanie konfiguracji maszyn
        /// </summary>
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
                    });

                    // generuje komunikat
                    string comTxt = "ADDED_TO_SERVICE_PLACE";
                    if (bFromQueue)
                        comTxt = "GET_FROM_QUEUE";
                    addComunicateToStack(new Comunicates(comTxt, time, car, carWash));
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
            if (carwash.ListBox.Items.Count < ((carwash.QueueLength) - 1))
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
            /*
             * @Dawid : Dawid, nie wiem jak sprawdzić aktualną długość kolejki więc poprawiam to. W razie czego przywrócisz to sobi e.
             * 
             * int długośćkolejki = carWash.ListBox.Items.Count;    !!
             */
            //carWash.ListBox.BeginInvoke(new Action(() => carWash.ListBox.Items.Add(car.IdCar.ToString())));
            //carWash.ListBox.Items.Add(car.IdCar.ToString());
            View.Invoke((MethodInvoker) delegate { carWash.ListBox.Items.Add(car.IdCar.ToString()); });

            // generuje komunikat
            addComunicateToStack(new Comunicates("ADDED_TO_QUEUE", time, car, carWash));
            
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
            //RandomGenerator.simpleRandomValue = new Random();
            //iActualTime += Convert.ToInt32(RandomGenerator.ExponentialGenerator(120, QueueSystem.Lambda, RandomGenerator.simpleRandomValue));
            iActualTime += Convert.ToInt32(TestSimpleRNG.SimpleRNG.GetExponential(QueueSystem.Lambda));

            Car newCar = new Car(iActualTime, 0);
            QueueSystem.cars.Add(newCar);
            addComunicateToStack(new Comunicates("IN", newCar.ArrivalTime, newCar));
        }

        public void addComunicateToStack(Comunicates com)
        {
            QueueSystem.comunicates.Add(com);
            //View.ListBoxComunicates.Items.Add(com.sComunicateContent);

            //Console.WriteLine(com.sComunicateContent);
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
