using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IggieNotifications
{
    public class NightlyTemperature
    {
        public List<EntryResults> NightEntries { get; set; }

        public decimal ExpectedMinNightlyTemperature { get; set; }

        public DateTime ExpectedMinNightlyTemperatureTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is NightlyTemperature r) {
                return (
                    this.NightEntries.SequenceEqual(r.NightEntries)
                    && this.ExpectedMinNightlyTemperature == r.ExpectedMinNightlyTemperature
                    && this.ExpectedMinNightlyTemperatureTime == r.ExpectedMinNightlyTemperatureTime
                );
            }
            return false;
        }
    }

}
