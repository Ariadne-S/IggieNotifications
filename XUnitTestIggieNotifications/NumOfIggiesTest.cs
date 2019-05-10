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
    public class NumOfIggiesTest
    {
        private readonly ILogger _log;

        public NumOfIggiesTest(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        private class FakeUserInput1 : IUserInput
        {
            public FakeUserInput1()
            {
                InputStack = new Stack();
                InputStack.Push("1");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void OneIggyTest()
        {
            _log.Information("OneIggyTest started");

            var expectation = 1;
            UserSpecifications userSpecifications = new UserSpecifications();
            var result = userSpecifications.NumOfIggies(_log, new FakeUserInput1());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

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
        public void IggyTestFailFirstAttemptDueToLetter()
        {
            _log.Information("IggyTestFailFirstAttemptDueToLetter started");

            var expectation = 1;
            UserSpecifications userSpecifications = new UserSpecifications();
            var result = userSpecifications.NumOfIggies(_log, new FakeUserInput2());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput3 : IUserInput
        {
            public FakeUserInput3()
            {
                InputStack = new Stack();
                InputStack.Push("3");
                InputStack.Push("2.5");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void IggyTestFailFirstAttemptDuetoDecimal()
        {
            _log.Information("IggyTestFailFirstAttemptDuetoDecimal started");

            var expectation = 3;
            UserSpecifications userSpecifications = new UserSpecifications();
            var result = userSpecifications.NumOfIggies(_log, new FakeUserInput3());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }
    }
}
        
