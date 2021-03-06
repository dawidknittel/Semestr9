﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Kolejki_LAB1
{
    class Program
    {
        private const int CarNumber = 1000000;
        private const int MeanWatingTime = 120;

        private static void Main(string[] args)
        {
            Console.WriteLine("Myjnia samochodowa LAB-2 M/M/2");

            /*Console.WriteLine("Czas obsługi równy 60 sekund");
            for (int i = 0; i < 1; i++)
                RunSimulation(WaitingTimeOption.Constant);
            /*Console.WriteLine("Czas obsługi jest zmienną losową o rozkładzie U(0,120)");
            /*for (int i = 0; i < 10; i++)
                RunSimulation(WaitingTimeOption.Uniform);*/
            Console.WriteLine("Czas obsługi jest zmienną losową o rozkładzie E(60)");
            for (int i = 0; i < 10; i++)
                RunSimulation(WaitingTimeOption.Exponential);
            /*Console.WriteLine("Czas obsługi jest zmienną losową o rozkładzie N(60,20)");
            for (int i = 0; i < 10; i++)
                RunSimulation(WaitingTimeOption.Normal);*/
           
            Console.Read();
        }

        private static void RunSimulation(WaitingTimeOption waitingOption)
        {
            Random randomValue = new Random();
            Queue<int> timeQueue = new Queue<int>();
            int LastTime = 0;

            timeQueue.Enqueue(0);

            for (int i = 0; i < CarNumber-1; i++)
            {               
                LastTime += Convert.ToInt32(RandomGenerator.ExponentialGenerator(120, MeanWatingTime, randomValue));
                timeQueue.Enqueue(LastTime);
            }

            CarWash CarWash1 = new CarWash();
            CarWash CarWash2 = new CarWash();

            CarWash.meanQueueCount = 0;
            CarWash.NumberOfSucceed = 0;

            LastTime += 500;
            RandomGenerator.simpleRandomValue = new Random();

            for (int time = 0; time < LastTime; time++)
            {
                Car newCar = null;

                if (timeQueue.Count > 0 && time == timeQueue.Peek())
                {
                    newCar = new Car(time, waitingOption);

                    if (CarWash1.CurrentCar == null)
                        CarWash1.NewCarArrival(newCar);
                    else
                        CarWash2.NewCarArrival(newCar);

                    timeQueue.Dequeue();
                }

                CarWash1.ProcessProgressCurrentCar(time, newCar);
                CarWash2.ProcessProgressCurrentCar(time, newCar);

                CarWash.ProcessProgressCarQueue(newCar);

                if (CarWash.CarQueue.Count > 0)
                    CarWash.currentCarQueueTime++;
            }

            CarWash1.ShowAverageWaitingTime();
            CarWash1.ShowMeanQueueCount(LastTime);
            CarWash1.ShowNumberOfSucceedArrivals(CarNumber);
            CarWash1.ShowNumberOfFailedArrivals(CarNumber);
        }
    }
}
