using System;
using System.Collections.Generic;
using System.Text;

namespace IggieNotifications
{
    public class WillyWeatherForecastTempResponse
    {

        public Location Location { get; set; }
        public Forecasts Forecasts { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string TimeZone { get; set; }
    }

    public class Forecasts
    {
        public TemperatureResults Temperature { get; set; }
    }

    public class TemperatureResults
    {
        public List<DaysResults> Days { get; set; }
        public UnitResults Units { get; set; }
    }

    public class DaysResults
    {
        public DateTime DateTime { get; set; }
        public List<EntryResults> Entries { get; set; }
    }

    public class EntryResults
    {
        public DateTime DateTime { get; set; }
        public decimal Temperature { get; set; }
    }

    public class UnitResults
    {
        public string Temperature { get; set; }
    }
}
