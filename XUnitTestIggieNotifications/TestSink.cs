using Serilog.Core;
using Serilog.Events;
using Xunit.Abstractions;

namespace XUnitTestIggieNotifications
{
    internal class TestSink : ILogEventSink
    {
        private ITestOutputHelper output;

        public TestSink(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Emit(LogEvent logEvent)
        {
            output.WriteLine(logEvent.RenderMessage());
        }
    }
}
