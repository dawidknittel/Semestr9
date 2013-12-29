using System.Collections.Generic;
using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class CarWash
    {
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

        public List<InputOutput> getOutputs()
        {
            return OutputSystems;
        }

        #endregion
    }
}
