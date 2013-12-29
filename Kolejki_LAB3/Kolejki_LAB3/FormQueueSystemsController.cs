using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
        public int iActualTime = 0;

        private const string FILE_NAME = "ConfigurationData.xml";

        #endregion
        #region Constructors

        public FormQueueSystemsController(FormQueueSystems view)
        {
            View = view;
        }

        #endregion
        #region Public Methods

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
                                    CarWash = CarWash.FindCarWash(inputOutput.ChildNodes[0].InnerText),
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
                                    CarWash = CarWash.FindCarWash(inputOutput.ChildNodes[0].InnerText),
                                    Percent = int.Parse(inputOutput.ChildNodes[1].InnerText),
                                    State = inputOutput.ChildNodes[2].InnerText
                                });
                            }
                        }
                    }

                    QueueSystem.carWashList.Add(carWash);
                    View.AddMachineToInterface(carWash);
                }

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
        public void AddApplicationToServicePlace(CarWash carWash, Car car)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar == null)
                {
                    servicePlace.CurrentCar = car;
                    servicePlace.ProgressBar.Maximum = car.PlannedWaitingTime;
                    servicePlace.ProgressBar.Minimum = 0;
                    servicePlace.ProgressBar.Step = 1;
                    servicePlace.ProgressBar.PerformStep();

                    // generuje komunikat
                    addComunicateToStack(new Comunicates("ADDED_TO_SERVICE_PLACE", iActualTime, car, carWash));
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
        /// Usuń samochód ze stanowiska obsługi
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void RemoveApplicationFromServicePlace(CarWash carWash, Car car)
        {
            foreach (ServicePlace servicePlace in carWash.ServicePlaces)
            {
                if (servicePlace.CurrentCar.Equals(car))
                {
                    servicePlace.CurrentCar = null;
                }
            }
        }

        /// <summary>
        /// Dodaj samochód do kolejki
        /// </summary>
        /// <param name="listBoxQueue"></param>
        /// <param name="car"></param>
        public void AddApplicationToQueue(CarWash carWash, Car car)
        {
            /*
             * @Dawid : Dawid, nie wiem jak sprawdzić aktualną długość kolejki więc poprawiam to. W razie czego przywrócisz to sobi e.
             */
            //carWash.ListBox.BeginInvoke(new Action(() => carWash.ListBox.Items.Add(car.IdCar.ToString())));
            carWash.ListBox.Items.Add(car.IdCar.ToString());

            // generuje komunikat
            addComunicateToStack(new Comunicates("ADDED_TO_QUEUE", iActualTime, car, carWash));
            
        }

        /// <summary>
        /// Usuń samochód z kolejki
        /// </summary>
        /// <param name="listBoxQueue"></param>
        /// <param name="car"></param>
        public void RemoveApplicationFromQueue(ListBox listBoxQueue, Car car)
        {
            foreach (var item in listBoxQueue.Items)
            {
                if (item.ToString().Equals(car.IdCar.ToString()))
                {
                    listBoxQueue.Items.Remove(item);
                }
                return;
            }    
        }

        /// <summary>
        /// Usuwa zadanie z systemu w momencie gdy nie ma wolnych miejsc w maszynach wychodzących ze stanu START (lub "IN")
        /// </summary>
        /// <param name="car"></param>
        public void RemoveApplicationFromSystem(Car car)
        {
            // generuje komunikat
            addComunicateToStack(new Comunicates("REMOVED_FROM_SYSTEM", car.ArrivalTime, car));

            // jako że jest to informacja o usunięciu zadania z systemu nie potrzebujemy jej przechowywać
            Comunicates.markComunicateAsUsed(QueueSystem.comunicates.Count - 1);

        }

        public void Comunicate(string comunicate)
        {
            View.ListBoxComunicates.Items.Add(comunicate);
        }

        /// <summary>
        /// Aktualizuj stan progressbara (myjni)
        /// </summary>
        /// <param name="carWash"></param>
        /// <param name="car"></param>
        public void SetCarWashProgress(CarWash carWash, Car car)
        {
            ProgressBar currentProgressBar = GetProgressBar(carWash, car);
            currentProgressBar.PerformStep();
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
            RandomGenerator.simpleRandomValue = new Random();
            iActualTime += Convert.ToInt32(RandomGenerator.ExponentialGenerator(120, QueueSystem.Lambda, RandomGenerator.simpleRandomValue));

            Car newCar = new Car(iActualTime);
            QueueSystem.cars.Add(newCar);
            addComunicateToStack(new Comunicates("IN", newCar.ArrivalTime, newCar));
        }

        public void addComunicateToStack(Comunicates com)
        {
            QueueSystem.comunicates.Add(com);
            View.ListBoxComunicates.Items.Add(com.sComunicateContent);

            Console.WriteLine(com.sComunicateContent);
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
