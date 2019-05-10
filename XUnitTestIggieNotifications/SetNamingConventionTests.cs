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
    public class SetNamingConventionTests
    {
        private readonly ILogger _log;

        public SetNamingConventionTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _log = output.ToLogger();
        }

        private class FakeUserInput1 : IUserInput
        {
            public FakeUserInput1()
            {
                InputStack = new Stack();
                InputStack.Push("myra");
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
            _log.Information("SetNamingConvention: OneIggyTest started");
            UserSpecifications userSpecifications = new UserSpecifications {
                NumberOfIggies = 1
            };

            var expectation = "Myra";
            var result = userSpecifications.SetNamingConvention(_log, new FakeUserInput1());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput2 : IUserInput
        {
            public FakeUserInput2()
            {
                InputStack = new Stack();
                InputStack.Push("myra");
                InputStack.Push("Cerin");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void TwoIggyTest()
        {
            _log.Information("SetNamingConvention: TwoIggyTest started");
            UserSpecifications userSpecifications = new UserSpecifications {
                NumberOfIggies = 2
            };

            var expectation = "Cerin and Myra";
            var result = userSpecifications.SetNamingConvention(_log, new FakeUserInput2());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }

        private class FakeUserInput3 : IUserInput
        {
            public FakeUserInput3()
            {
                InputStack = new Stack();
                InputStack.Push("myra");
                InputStack.Push("Cerin");
                InputStack.Push("paris");
            }

            public Stack InputStack { get; private set; }

            public string GetInput()
            {
                return InputStack.Pop().ToString();
            }
        }

        [Fact]
        public void ThreeIggyTest()
        {
            _log.Information("SetNamingConvention: OneIggyTest started");
            UserSpecifications userSpecifications = new UserSpecifications {
                NumberOfIggies = 3
            };

            var expectation = "Paris, Cerin and Myra";
            var result = userSpecifications.SetNamingConvention(_log, new FakeUserInput3());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }


        [Fact]
        public void FourIggiesTest()
        {
            _log.Information("SetNamingConvention: FourIggiesTest started");
            UserSpecifications userSpecifications = new UserSpecifications {
                NumberOfIggies = 4
            };

            var expectation = "your italian greyhounds";
            var result = userSpecifications.SetNamingConvention(_log, new FakeUserInput1());

            _log.Information("Test Run:\nExpectation: {expectation}\nResult: {result}", expectation, result);

            Assert.Equal(expectation, result);
        }
    }
}

