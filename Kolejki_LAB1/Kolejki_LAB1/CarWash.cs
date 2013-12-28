using System;
using System.Collections.Generic;
using System.Linq;

namespace Kolejki_LAB1
{
    public class CarWash
    {
        private const string PLUS = "+";
        private const string MINUS = "-";

        private string ShowLog = "N"; 

        public Car CurrentCar = null;
        public Queue<Car> CarQueue = new Queue<Car>(); 
        public Queue<int> CarWaitingTime = new Queue<int>();

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
            Console.WriteLine("Średni czas oczekiwania samochodu w kolejce wynosi: " + CarWaitingTime.Average() + " sekund");
        }

        private void ReleaseCar(int time)
        {
            CarWaitingTime.Enqueue(CurrentCar.WaitingTime);
            Car WashedCar = CurrentCar;
            CurrentCar = null;

            if (CarQueue.Count > 0)
            {
                CarQueue.Peek().WaitingTime++;
                CurrentCar = CarQueue.Dequeue();
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
    }
}
