using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class CarWash
    {
        #region Public Fields - Statistics

        public decimal MeanNumberOfApplicationInQueue = 0;  //średnia liczba zgłoszeń w kolejce
        public decimal MeanTimeApplicationInQueue = 0;      // Średnia czas przebywania zgłoszenia w kolejce 
        public decimal RelativeSystemAbility = 0;           // Wględna zdolność obsługi systemu
        public decimal AbsoluteSystemAbility = 0;           // Bezwględna zdolność obsługi systemu

        public int timeTotal = 0;                           // Czas działania myjni
        public int timeInQueueTotal = 0;                    // Czas przebywania w kolejce (wszystkich aut)
        public int numberOfCarsTotal = 0;                   // Liczba wszystkich aut
        public int numberOfSucceeded = 0;                   // Liczba obsłużonych aut
        public int numberOfFailed = 0;                      // Liczba nieobsłużonych aut
        public int numberOfCarsInQueueTotal = 0;            // Liczba aut w kolejce (total)


        #endregion
        #region Properties

        public string MachineName { get; set; }
        public int? ServicePlacesLength = null;
        public int? QueueLength = null;
        public Algorithm? Algorithm = null;
        public GroupBox GroupBox = null;
        public ListBox ListBox = null;
        public List<ServicePlace> ServicePlaces = new List<ServicePlace>(); 

        public List<InputOutput> InputSystems = new List<InputOutput>();
        public List<InputOutput> OutputSystems = new List<InputOutput>();

        public List<MeanTimeInQueue> ChartData = new List<MeanTimeInQueue>(); 

        #endregion
        #region Public Methods

        public static CarWash FindCarWash(string carWashNameToFind)
        {
            foreach (CarWash carWash in QueueSystem.carWashList)
            {
                if (carWash.MachineName != null && carWash.MachineName.Equals(carWashNameToFind))
                    return carWash;
            }

            return null;
        }

        public static bool checkCarWashHasInputStartState(CarWash carwash)
        {
            bool bHas = false;

            foreach (InputOutput inputs in carwash.InputSystems)
            {
                if (inputs.State == "Start")
                    bHas = true;
            }

            return bHas;
        }

        public static decimal getCarWashInputStartStatePercent(CarWash carwash)
        {
            decimal percent = 0;

            foreach (InputOutput inputs in carwash.InputSystems)
            {
                if (inputs.State == "Start")
                    percent = inputs.Percent;
            }

            return percent;
        }

        public List<InputOutput> getOutputs()
        {
            return OutputSystems;
        }


        public static decimal calculateMeanNumberOfApplicationInQueue(CarWash carwash)
        {
            if (carwash.timeTotal == 0 || carwash.numberOfCarsTotal == 0)
                return 0;

            // Średnia liczba samochodów obsłużonych w jednostce czasu. (Liczba aut obsłużonych / time total)
            return (Convert.ToDecimal(carwash.numberOfCarsInQueueTotal) / Convert.ToDecimal(carwash.timeTotal));

        }

        public static decimal calculateMeanTimeApplicationInQueue(CarWash carwash)
        {
            if (carwash.numberOfCarsInQueueTotal == 0 || carwash.numberOfCarsTotal == 0)
                return 0;

            // Średni czas przebywania w kolejce
            return (Convert.ToDecimal(carwash.timeInQueueTotal) / Convert.ToDecimal(carwash.numberOfCarsTotal));
        }

        public static decimal calculateRelativeSystemAbility(CarWash carwash)
        {
            if (carwash.numberOfCarsTotal == 0 || carwash.numberOfCarsTotal == 0)
                return 0;

            // Liczba aut obsłużonych / liczba wszystkich aut
            return (Convert.ToDecimal(carwash.numberOfSucceeded) / Convert.ToDecimal(carwash.numberOfCarsTotal));
        }

        public static decimal calculateAbsoluteSystemAbility(CarWash carwash)
        {
            if (carwash.timeTotal == 0 || carwash.numberOfCarsTotal == 0)
                return 0;

            // Średnia liczba samochodów obsłużonych w jednostce czasu. (Liczba aut obsłużonych / time total)
            return 1 - (Convert.ToDecimal(carwash.numberOfSucceeded) / Convert.ToDecimal(carwash.numberOfCarsTotal));

        }
        /*

        // średnia liczba zgłoszeń w kolejce
        public double calculateMeanNumberOfApplicationInQueue()
        {
            double r = (double)QueueSystem.Mi / (double)QueueSystem.Lambda;
            double k = (double)ServicePlacesLength;
            double po = calculateInitialProbability();
            double N = (double)QueueLength;

            double Nb = (po * Math.Pow(r, k) / getFactorialOfNumber(Convert.ToInt32(k))) * ((Math.Pow(r, 1 - k) * (Math.Pow(k, N + 1) * Math.Pow(r, k) - Math.Pow(k, k + 1) * Math.Pow(r, N) * (N + r + 1) + Math.Pow(k, k + 2) * Math.Pow(r, N) + Math.Pow(k, k) * N * Math.Pow(r, N + 1))) / (Math.Pow(k, N) * (k - r) * (k - r)));
            
            return Nb;
        }

        // prawdopodobieństwo p0
        public double calculateInitialProbability()
        {
            double r = (double)QueueSystem.Mi / (double)QueueSystem.Lambda;
            int k = (int)ServicePlacesLength;
            int N = (int)QueueLength;

            double sumE = 0;
            for (int i = 0; i <= k; i++)
            {
                sumE = sumE + (Math.Pow(r, i) / getFactorialOfNumber(i));
            }

            double test = (Math.Pow(r, k + 1) * (1 - Math.Pow(r / k, N - k)));
            double test2 = (getFactorialOfNumber(k) * (k - r));


            double po = Math.Pow((sumE + ((Math.Pow(r, k + 1) * (1 - Math.Pow(r / k, N - k))) / (getFactorialOfNumber(k) * (k - r)))), -1);

            return po;
        }

        // silnia
        private int getFactorialOfNumber(int number)
        {
            if (number == 1 || number == 2)
                return number;

            int fact = 1;
            for (int i = number; i > 1; i--)
            {
                fact = fact * i;
            }

            return fact;
        }
         
         */

        #endregion
    }
}
