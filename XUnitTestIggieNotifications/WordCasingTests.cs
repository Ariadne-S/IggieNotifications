using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Serilog;
using IggieNotifications;

namespace XUnitTestIggieNotifications
{
    public class WordCasingTests
    {
        private readonly ILogger _log;

        public WordCasingTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        [Fact]
        public void OneWordCasingTest()
        {
            _log.Information("OneWordCasingTest started");

            var testdata = "pen";
            var expectation = "Pen";
            var result = OutputHelpers.CasedString(testdata);

            _log.Information("Test Run:\n Expectation: {expectation} Result: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        [Fact]
        public void OneWordWithLeadingSpaceCasingTest()
        {
            _log.Information("OneWordWithLeadingSpaceCasingTest 1 started");

            var testdata = " pen";
            var expectation = " Pen";
            var result = OutputHelpers.CasedString(testdata);

            _log.Information("Test Run:\n Expectation: {expectation} Result: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        [Fact]
        public void TwoWordsCasingTest()
        {
            _log.Information("TwoWordsCasingTest 1 started");

            var testdata = "pencil case";
            var expectation = "Pencil Case";
            var result = OutputHelpers.CasedString(testdata);

            _log.Information("Test Run:\n Expectation: {expectation} Result: {result}", expectation, result);

            Assert.Equal(expectation, result);

        }
    }
}
        
