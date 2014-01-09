using System;
using System.Windows.Forms;
using Kolejki_LAB3.Model;

namespace Kolejki_LAB3
{
    public class Car
    {
        public int IdCar;
        public int PlannedWaitingTime;
        public int WaitingTime;
        public int ArrivalTime;
        public int iTimeWaitingInMachine;
        public int iTimeInQueueStart;

        public double Time;     
        public double ProgressWashingTime;

        public ProgressBar CurrentProgressBar = null;

        public static int IdCounter = 1;

        public Car(int time/*, WaitingTimeOption waitingOption*/, int plannedWaitingTime )
        {
            IdCar = Car.IdCounter++;

            iTimeWaitingInMachine = 0;
            WaitingTime = 0;
            ProgressWashingTime = 0;
            PlannedWaitingTime = plannedWaitingTime;
            //PlannedWaitingTime = GetWaitingTime(waitingOption);
            ArrivalTime = time;
            iTimeInQueueStart = 0;
        }

        private int GetWaitingTime(WaitingTimeOption waitingOption)
        {
            switch (waitingOption)
            {
                case WaitingTimeOption.Constant:
                {
                    return 60;
                }
                case WaitingTimeOption.Uniform:
                {
                    return Convert.ToInt32(RandomGenerator.SimpleUniformGenerator(120,120));
                }
                case WaitingTimeOption.Exponential:
                {
                    return Convert.ToInt32(TestSimpleRNG.SimpleRNG.GetExponential(60));
                }
                case WaitingTimeOption.Normal:
                {
                    return Convert.ToInt32(TestSimpleRNG.SimpleRNG.GetNormal(60, 20));
                }
            }

            return 0;
        }

        public void setPlannedWaitingTime()
        {
            //RandomGenerator.simpleRandomValue = new Random();
            PlannedWaitingTime = Convert.ToInt32(TestSimpleRNG.SimpleRNG.GetExponential(QueueSystem.Lambda));
            //PlannedWaitingTime = Convert.ToInt32(RandomGenerator.ExponentialGenerator(120, QueueSystem.Lambda, RandomGenerator.simpleRandomValue));
        }

        public void setiTimeWaitingInMachine(int timeWaiting)
        {
            iTimeWaitingInMachine = timeWaiting;
        }

        public static Car findCar(int carIdToFind)
        {
            foreach (Car car in QueueSystem.cars)
            {
                if (car.IdCar == carIdToFind)
                    return car;
            }

            return null;
        }
    }
}
