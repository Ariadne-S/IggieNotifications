using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace IggieNotifications
{
    class JumperNotification
    {
        private readonly UserSpecifications _localuserSpecification;
        private readonly ILogger _log;
        private readonly NightlyTemperature _data;

        public JumperNotification(UserSpecifications userSpecification, ILogger log, NightlyTemperature data)
        {
            _localuserSpecification = userSpecification;
            _log = log;
            _data = data;
        }

        public string SetJumperIsNeeded()
        {
            var localNamingConvention = OutputHelpers.CapitaliseFirstLetter(_localuserSpecification.NamingConvention);
            if (_localuserSpecification.JumperThreshold >= _data.ExpectedMinNightlyTemperature) {
                var message = $"{localNamingConvention} will need a jumper tonight\nThe temperature will be {_data.ExpectedMinNightlyTemperature} at {_data.ExpectedMinNightlyTemperatureTime}";

                JumperIsNeeded = true;
                return (message);
            } else {
                var message = $"{localNamingConvention} will not need a jumper tonight\nThe temperature will be {_data.ExpectedMinNightlyTemperature} at {_data.ExpectedMinNightlyTemperatureTime}";

                JumperIsNeeded = false;
                return (message);
            }
        }

        public bool JumperIsNeeded { get; set; }

    }
}
