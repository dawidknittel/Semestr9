using System;
using System.Collections.Generic;
using System.Linq;

namespace Kolejki_LAB1
{
    public class CarWash
    {
        private const string PLUS = "+";
        private const string MINUS = "-";
        public int NumberOfSucceed = 0;

        private string ShowLog = "N"; 

        public Car CurrentCar = null;
        public Queue<Car> CarQueue = new Queue<Car>(); 
        public Queue<int> CarWaitingTime = new Queue<int>();

        public static int meanQueueCount = 0;
        public static int currentCarQueueTime = 0;

        public void NewCarArrival(Car newCar)
        {
            if (CurrentCar == null)
            {
                CurrentCar = newCar;
            }
            else
            {
                CarQueue.Enqueue(newCar);
            }

            NumberOfSucceed++;
            meanQueueCount += CarQueue.Count * currentCarQueueTime;
            currentCarQueueTime = 0;

            if (ShowLog.Equals("T"))
            {
                Console.Write(PLUS + newCar.IdCar + "   " + newCar.ArrivalTime + "   " + CurrentCar.IdCar + ": ");
                ShowCarQueue();
                Console.WriteLine("Czas obłusgi: " + newCar.PlannedWaitingTime);
            }
        }

        public void ProcessProgressCars(int time, Car newCar)
        {
            if (CurrentCar != null)
            {
                if (!CurrentCar.Equals(newCar))
                    CurrentCar.ProgressWashingTime++;

                if (CurrentCar.ProgressWashingTime.Equals(CurrentCar.PlannedWaitingTime))
                    ReleaseCar(time);
            }

            foreach (Car car in CarQueue)
            {
                if (!car.Equals(newCar))
                    car.WaitingTime++;
            }
        }

        public void ShowAverageWaitingTime()
        {
            Console.WriteLine("Średni czas oczekiwania samochodu w kolejce wynosi: " + CarWaitingTime.Average().ToString("#0.0000") + " sekund");
        }

        private void ReleaseCar(int time)
        {
            CarWaitingTime.Enqueue(CurrentCar.WaitingTime);
            Car WashedCar = CurrentCar;
            CurrentCar = null;

            meanQueueCount += CarQueue.Count * currentCarQueueTime;
            //Console.WriteLine("Średnia liczba zgłoszeń w kolejce " + CarQueue.Count + "*" + currentCarQueueTime);

            if (CarQueue.Count > 0)
            {
                CarQueue.Peek().WaitingTime++;
                CurrentCar = CarQueue.Dequeue();
                currentCarQueueTime = 0;
            }

            if (ShowLog.Equals("T"))
            {
                Console.Write(MINUS + WashedCar.IdCar + "   " + time);

                if (CurrentCar != null)
                {
                    Console.Write("   " + CurrentCar.IdCar + ": ");
                    ShowCarQueue();
                }
                else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("Czas oczekiwania samochodu wyniósł: " + WashedCar.WaitingTime);
            }
        }

        public void ShowCarQueue()
        {
            foreach (Car carFromQueue in CarQueue)
            {
                Console.Write(carFromQueue.IdCar + " ");
                if (CarQueue.Count > 20)
                    Console.Read();
            }
            Console.WriteLine();
        }

        public void ShowMeanQueueCount(int lastTime)
        {
            Console.WriteLine("Średnia liczba zgłoszeń w kolejce " + (Convert.ToDecimal(meanQueueCount)/Convert.ToDecimal(lastTime)).ToString("#0.0000"));
        }

        public void ShowNumberOfSucceedArrivals(int CarNumber)
        {
            Console.WriteLine("Względna zdolność obsługi systemu " + (Convert.ToDecimal(NumberOfSucceed) / Convert.ToDecimal(CarNumber)).ToString("#0.0000"));
        }

        public void ShowNumberOfFailedArrivals(int CarNumber)
        {
            Console.WriteLine("Bezwzględna zdolność obsługi systemu " + ((Convert.ToDecimal(NumberOfSucceed) / Convert.ToDecimal(CarNumber)) * Convert.ToDecimal(1 / Convert.ToDecimal(120))).ToString("#0.0000"));
        }
    }
}
