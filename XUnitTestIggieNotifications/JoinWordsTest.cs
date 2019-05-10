using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Serilog;
using IggieNotifications;

namespace XUnitTestIggieNotifications
{
    public class JoinWordsTest
    {
        private readonly ILogger _log;

        public JoinWordsTest(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        [Fact]
        public void ZeroWordJoinWordsTest()
        {
            _log.Information("ZeroWordJoinWordsTest started");

            List<string> testDataList = new List<string> {};

            var expectation = "";

            var actual = OutputHelpers.JoinWords(testDataList);

            _log.Information("Test Run: {expectation} - {result}", expectation, actual);
            Assert.Equal(expectation, actual);
        }

        [Fact]
        public void OneWordJoinWordsTest()
        {
            _log.Information("OneWordJoinWordsTest started");

            List<string> testDataList = new List<string> {
                "sophie"
            };

            var expectation = "sophie";

            var actual = OutputHelpers.JoinWords(testDataList);

            _log.Information("Test Run: {expectation} - {result}", expectation, actual);
            Assert.Equal(expectation, actual);
        }

        [Fact]
        public void TwoWordJoinWordsTest()
        {
            _log.Information("TwoWordJoinWordsTest started");

            List<string> testDataList = new List<string> {
                "myra",
                "cerin"
            };

            var expectation = "myra and cerin";

            var actual = OutputHelpers.JoinWords(testDataList);

            _log.Information("Test Run: {expectation} - {result}", expectation, actual);
            Assert.Equal(expectation, actual);
        }

        [Fact]
        public void ThreeWordJoinWordsTest()
        {
            _log.Information("ThreeWordJoinWordsTest started");

            List<string> testDataList = new List<string> {
                "apple",
                "windows",
                "linux"
            };

            var expectation = "apple, windows and linux";

            var actual = OutputHelpers.JoinWords(testDataList);

            _log.Information("Test Run: {expectation} - {result}", expectation, actual);
            Assert.Equal(expectation, actual);
        }

        [Fact]
        public void FourWordJoinWordsTest()
        {
            _log.Information("FourWordJoinWordsTest started");

            List<string> testDataList = new List<string> {
                "Daffodils",
                "daisies",
                "lillies",
                "Roses"
            };

            var expectation = "Daffodils, daisies, lillies and Roses";

            var actual = OutputHelpers.JoinWords(testDataList);

            _log.Information("Test Run: {expectation} - {result}", expectation, actual);
            Assert.Equal(expectation, actual);
        }
    }
}
