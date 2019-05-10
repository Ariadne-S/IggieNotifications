using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Serilog;
using IggieNotifications;
using static IggieNotifications.UserInputRequests;
using System.Collections;

namespace XUnitTestIggieNotifications
{
    public class UserDecInputTests
    {
        private readonly ILogger _log;

        public UserDecInputTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        private class FakeUserInput1 : IUserInput
        {
            public string GetInput()
            {
                return "12.5";
            }
        }

        [Fact]
        public void UserInputSuccessFirstTryTest()
        {
            _log.Information("UserInputSuccessFirstTryTest started");

            var requestMessage = "The Fake User Interface will input 12.5";
            var expectation = 12.5m;
            var result = UserDecInput(requestMessage, _log, new FakeUserInput1());

            _log.Information("Test Run:\nexpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput2 : IUserInput
        {
            public FakeUserInput2()
            {
                InputStack = new Stack();
                InputStack.Push("0.25");
                InputStack.Push("a");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void UserInputFailFirstTryTest()
        {
            _log.Information("UserInputFailFirstTryTest started");

            var requestMessage = "The Fake User Interface will input a, then 0.25";
            var expectation = 0.25m;
            var result = UserDecInput(requestMessage, _log, new FakeUserInput2());
            _log.Information("Test Run:\nexpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput3 : IUserInput
        {

            public FakeUserInput3()
            {
                InputStack = new Stack();
                InputStack.Push("1");
                InputStack.Push("a");
                InputStack.Push("&");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void UserInputFailMultipleTimesTest()
        {
            _log.Information("UserInputFailFirstTryTest started");

            var requestMessage = "The Fake User Interface will input &, then a, then 1";
            var expectation = 1m;
            var result = UserDecInput(requestMessage, _log, new FakeUserInput3());

            _log.Information("Test Run:\nexpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }
    }
}
        
