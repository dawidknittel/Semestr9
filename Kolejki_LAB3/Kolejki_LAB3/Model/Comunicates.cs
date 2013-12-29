﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kolejki_LAB3.Model
{
    public class Comunicates
    {
        public Car oComunicateCar;
        public CarWash oComunicateCarWash;
        public int iComunicateTime;
        public string sComunicateType { get; set; }
        public string sComunicateContent { get; set; }

        public static int IdCounter = 0;

        public Comunicates(string sType, int iTime, Car car, CarWash carWash = null)
        {
            string MachineName = "";
            if (carWash != null)
                MachineName = carWash.MachineName;
            sComunicateType = sType;
            oComunicateCar = car;
            sComunicateContent = iTime + "\t" + getComunicateContentByType(sType, car.IdCar, MachineName);
            iComunicateTime = iTime;
        }

        /// <summary>
        /// Sortuje komunikaty wg czasu nadejscia
        /// </summary>
        public static void sortComunicates()
        {
            QueueSystem.comunicates = QueueSystem.comunicates.OrderBy(o => o.iComunicateTime).ToList();
        }

        public static Comunicates getComunicateToHandle()
        {
            sortComunicates();
            if (QueueSystem.comunicates.Count > 0)
            {
                // pobiera najwcześniejsze zdarzenie do obsługi
                Comunicates first = QueueSystem.comunicates[0];
                markComunicateAsUsed(0);

                // jeśli w kolejce komunikatów nie istnieje komunikat typu IN -> wylosuj nowy czas przybycia kolejnego zdarzenia typu IN
                //if (!checkComunicateINExists())
                // generateINComunicate

                return first;
            }

            return null;
        }

        public static bool checkComunicateINExists()
        {
            Comunicates inCom = QueueSystem.comunicates.Find(o => o.sComunicateType == "IN");
            if (inCom == null)
                return false;

            return true;
        }

        private string getComunicateContentByType(string sType, int iCarId, string MachineName = "")
        {
            string sContent = "";

            switch (sType)
            {
                case "IN":
                    sContent = "Nadeszło nowe zgłoszenie nr " + iCarId;
                    break;
                case "ADDED_TO_SERVICE_PLACE":
                    sContent = "Zgłoszenie nr " + iCarId + " weszło na maszynę " + MachineName;
                    break;
                case "ADDED_TO_QUEUE":
                    sContent = "Zgłoszenie nr " + iCarId + " weszło do bufora maszyny " + MachineName;
                    break;
                case "REMOVED_FROM_SYSTEM":
                    sContent = "Zgłoszenie nr " + iCarId + " nie zostało obsłużone";
                    break;
                case "SERVICE_PLACE_FINISHED":
                    sContent = "Zgłoszenie nr " + iCarId + " zeszło z maszyny " + MachineName;
                    break;



            }

            return sContent;
        }

        /// <summary>
        /// Póki co usuwamy komunikaty, lecz jeśli zajdzie taka potrzeba będziemy je jakoś oznaczać, albo wrzucać do innej listy
        /// </summary>
        /// <param name="index"></param>
        public static void markComunicateAsUsed(int index)
        {
            QueueSystem.comunicates.RemoveAt(index);
        }



    }
}
