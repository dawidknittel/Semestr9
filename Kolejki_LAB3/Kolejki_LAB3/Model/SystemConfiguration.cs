using System.Collections.Generic;

namespace Kolejki_LAB3.Model
{
    public class SystemConfiguration
    {
        public int ServicePlacesQuantity;
        public string Algorithm;
        public int QueueLength;
        public string MachineName;

        public List<InputOutput> InputSystems = new List<InputOutput>();
        public List<InputOutput> OutputSystems = new List<InputOutput>();

        public SystemConfiguration(int servicePlacesQuantity, string algorithm, int queueLength, string machineName)
        {
            ServicePlacesQuantity = servicePlacesQuantity;
            Algorithm = algorithm;
            QueueLength = queueLength;
            MachineName = machineName;
        }

        public SystemConfiguration()
        {
        }
    }
}
