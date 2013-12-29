using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public static class QueueSystem
    {
        private static int lambda;
        private static int mi;

        public static int Lambda
        {
            get { return lambda; }
            set { lambda = value; }
        }

        public static int Mi
        {
            get { return mi; }
            set { mi = value; }
        }

        public static List<CarWash> carWashList = new List<CarWash>();
        public static List<GroupBox> groupBoxList = new List<GroupBox>(); 
        public static List<Point> pointsList = new List<Point>();
        public static List<Comunicates> comunicates = new List<Comunicates>();
        public static List<Comunicates> comunicatesUsed = new List<Comunicates>();
        public static List<Car> cars = new List<Car>();

        public static List<int> startPoints = new List<int>();
        public static List<int> endPoints = new List<int>();

        public static CarWash GetLastAddedCarWash()
        {
            if (carWashList == null)
            {
                throw new ArgumentException();
            }

            return carWashList[carWashList.Count - 1];
        }

        public static void FillListPoints()
        {
            pointsList.Add(new Point(59, 32));
            pointsList.Add(new Point(332, 80));
            pointsList.Add(new Point(93, 400));
            pointsList.Add(new Point(476, 358));
            pointsList.Add(new Point(597, 55));
            pointsList.Add(new Point(713, 332));
        }

        public static void FillStartPoints()
        {
            startPoints.Add(50);
            startPoints.Add(230);
            startPoints.Add(50);
            startPoints.Add(30);
            startPoints.Add(60);
            startPoints.Add(150);
        }

        public static void FillEndPoints()
        {
            endPoints.Add(10);
            endPoints.Add(230);
            endPoints.Add(230);
            endPoints.Add(240);
            endPoints.Add(50);
            endPoints.Add(60);
        }

        /// <summary>
        /// Zwraca wszystkie indexy maszyn, do których można dotrzeć ze stanu START
        /// </summary>
        public static List<int> getPossibleMovesFromINState()
        {
            List<int> carWashesFromINState = new List<int>();

            foreach (CarWash carWash in carWashList)
            {
                if (CarWash.checkCarWashHasInputStartState(carWash))
                    carWashesFromINState.Add(carWashList.IndexOf(carWash));
            }

            // @todo dorobić null exception
            return carWashesFromINState;
        }
    }
}
