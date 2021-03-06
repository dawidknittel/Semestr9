﻿using System;
using System.Linq;

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

        #region summary
        /* Mamy różne rodzaje komunikatów:
        * 1) IN - Wejściowy, jak samochód wchodzi do systemu
        * 2) ADDED_TO_SERVICE_PLACE - w momencie jak samochód dodawany jest do stanowiska (myjni)
        * 3) ADDED_TO_QUEUE - samochód dodawany do kolejki - ten komunikat nie jest obsługiwany. Samochód wchodzi z kolejki do myjni, gdy myjnia się zwolni, więc w momencie jej
        *                      zwalniania wykonywana jest operacja przeniesienia samochodu z kolejki do myjni
        * 4) GET_FROM_QUEUE - w momencie gdy samochód wchodzi z kolejki do stanowiska (myjni)
        * 5) SERVICE_PLACE_FINISHED - w momencie gdy samochód schodzi ze stanowiska (myjni)
        * 6) REMOVED_FROM_SYSTEM - samochód nie jest obsłużony (brak miejsc w wejściowym obiekcie myjni
        * 7) OUT - samochód opuszcza system (obsłużony)
        * 
        */
        #endregion

        public Comunicates(string sType, int iTime, Car car, CarWash carWash = null)
        {
            string MachineName = "";
            if (carWash != null)
                MachineName = carWash.MachineName;
            sComunicateType = sType;
            oComunicateCar = car;
            oComunicateCarWash = carWash;
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
                    //sContent = "Zgłoszenie nr " + iCarId + " weszło na maszynę " + MachineName;
                    sContent = "Z (" + iCarId + ") weszło na " + MachineName;
                    break;
                case "ADDED_TO_QUEUE":
                    //sContent = "Zgłoszenie nr " + iCarId + " weszło do bufora maszyny " + MachineName;
                    sContent = "Z (" + iCarId + ") weszło do bufora " + MachineName;
                    break;
                case "GET_FROM_QUEUE":
                    //sContent = "Zgłoszenie nr " + iCarId + " weszło z bufora na maszynę " + MachineName;
                    sContent = "Z (" + iCarId + ") weszło z bufora do " + MachineName;
                    break;
                case "REMOVED_FROM_SYSTEM":
                    //sContent = "Zgłoszenie nr " + iCarId + " nie zostało obsłużone";
                    sContent = "!! Z (" + iCarId + ") nie zostało obsłużone !!";
                    break;
                case "SERVICE_PLACE_FINISHED":
                    // sContent = "Zgłoszenie nr " + iCarId + " wykonało się na maszynie " + MachineName;
                    sContent = "Z (" + iCarId + ") skończyło na " + MachineName;
                    break;
                case "OUT":
                    // sContent = "Zgłoszenie nr " + iCarId + " wyszło z systemu";
                    sContent = "Z (" + iCarId + ") wyszło z systemu";
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
            // wrzucamy komunikat do listy już obsłużonych komunikatów
            QueueSystem.comunicatesUsed.Add(QueueSystem.comunicates[index]);
            QueueSystem.comunicates.RemoveAt(index);
        }



    }
}
