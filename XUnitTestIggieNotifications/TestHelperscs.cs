using Serilog;
using Xunit;


namespace XUnitTestIggieNotifications
{
    public static class TestHelpers
    {
        public static ILogger ToLogger(this Xunit.Abstractions.ITestOutputHelper output)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Sink(new TestSink(output))
                .CreateLogger();
        }
    }
}
