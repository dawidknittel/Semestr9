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

        public double Time;     
        public double ProgressWashingTime;

        public ProgressBar CurrentProgressBar = null;

        public static int IdCounter = 1;

        public Car(int time/*, WaitingTimeOption waitingOption*/, int plannedWaitingTime)
        {
            IdCar = Car.IdCounter++;

            WaitingTime = 0;
            ProgressWashingTime = 0;
            PlannedWaitingTime = plannedWaitingTime;
            //PlannedWaitingTime = GetWaitingTime(waitingOption);
            ArrivalTime = time;
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
    }
}
