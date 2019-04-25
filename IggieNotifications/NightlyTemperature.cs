using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IggieNotifications
{
    public class NightlyTemperature
    {
        public NightlyTemperature(List<EntryResults> entryResults)
        {
            NightEntries = entryResults.Where(e => IsNightTime(e.DateTime)).ToList();
            ExpectedMinNightlyTemperature = NightEntries.Min(e => e.Temperature);
            ExpectedMinNightlyTemperatureTime = NightEntries.First(e => e.Temperature == ExpectedMinNightlyTemperature).DateTime;
            
        }


        private bool IsNightTime(DateTime dateTime)
        {
            return dateTime.Hour >= 18 || dateTime.Hour <= 6;
        }

        public List<EntryResults> NightEntries { get; set; }

        public decimal ExpectedMinNightlyTemperature { get; set; }

        public DateTime ExpectedMinNightlyTemperatureTime { get; set; }
    }
}
