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
    public class UserIntInputTests
    {
        private readonly ILogger _log;

        public UserIntInputTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        private class FakeUserInput1 : IUserInput
        {
            public string GetInput()
            {
                return "1";
            }
        }

        [Fact]
        public void UserInputSuccessFirstTryTest()
        {
            _log.Information("UserInputSuccessFirstTryTest started");

            var requestMessage = "The Fake User Interface will input 1";
            var expectation = 1;
            var result = UserIntInput(requestMessage, _log, new FakeUserInput1());

            _log.Information("Test Run:\nexpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput2 : IUserInput
        {
            public FakeUserInput2()
            {
                InputStack = new Stack();
                InputStack.Push("1");
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

            var requestMessage = "The Fake User Interface will input a";
            var expectation = 1;
            var result = UserIntInput(requestMessage, _log, new FakeUserInput2());

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

            var requestMessage = "The Fake User Interface will input a";
            var expectation = 1;
            var result = UserIntInput(requestMessage, _log, new FakeUserInput3());

            _log.Information("Test Run:\nexpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }
    }
}
        
