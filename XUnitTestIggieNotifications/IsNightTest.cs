using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Serilog;
using IggieNotifications;

namespace XUnitTestIggieNotifications
{
    public class IsNightTest
    {
        private readonly ILogger _log;

        public IsNightTest(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        [Fact]
        public void IsNightTestTrue()
        {
            _log.Information("IsNightTestTrue started");

            var testDateTimeList = new List<DateTime> {
                new DateTime(2000, 12, 1, 2, 30, 00),
                new DateTime(2000, 12, 1, 23, 59, 00),
                new DateTime(2000, 12, 1, 5, 59, 59),
                new DateTime(2000, 12, 1, 18, 00, 00),
                new DateTime(2000, 12, 1, 6, 00, 00)
            };

            foreach (var testDateTime in testDateTimeList) {
                var result = DateHelperFunctions.IsNightTime(testDateTime);
                _log.Information("Test Run for {time:HH:mm} [{result}]", testDateTime, result);
                Assert.True(result);
            }
        }


        [Fact]
        public void IsNightTestFalse()
        {
            _log.Information("IsNightTestFalse started");


            var testDateTimeList = new List<DateTime> {
                new DateTime(2000, 12, 1, 7, 00, 00),
                new DateTime(2000, 12, 1, 15, 30, 00),
                new DateTime(2000, 12, 1, 12, 30, 00),
                new DateTime(2000, 12, 1, 17, 59, 59),
            };

            foreach (var testDateTime in testDateTimeList) {
                var result = DateHelperFunctions.IsNightTime(testDateTime);
                _log.Information("Test Run for {time:HH:mm} [{result}]", testDateTime, result);
                Assert.False(result);
            }

        }
    }
}
