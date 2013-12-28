using System;

namespace Kolejki_LAB1
{
    public static class RandomGenerator
    {
        public static Random simpleRandomValue;

        public static double UniformGenerator(int max, double meanWaitingTime, Random randomValue)
        {
            return (double)(randomValue.Next(1, max) / meanWaitingTime);
        }

        public static double ExponentialGenerator(int max, double meanWaitingTime, Random randomValue)
        {
            return Math.Round(-Math.Log(1 - UniformGenerator(max, meanWaitingTime, randomValue)) * meanWaitingTime, 3);
        }

        public static double SimpleUniformGenerator(int max, double meanWaitingTime)
        {
            return (double)(simpleRandomValue.Next(1, max));
        }

        public static double SimpleExponentialGenerator(int max, double meanWaitingTime)
        {
            return Math.Round(-Math.Log((1 - (simpleRandomValue.Next(1, max) / meanWaitingTime))) * 64, 3);
        }
    }
}
