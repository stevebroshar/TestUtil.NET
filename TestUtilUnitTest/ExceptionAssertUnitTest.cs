using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scb
{
    [TestClass]
    public class ExceptionAssertUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(AssertFailedException))]
        public void NonThrowingBlockIsTestFailure()
        {
            ExceptionAssert.Propagates<ExceptionClass>(() => { });
        }

        [TestMethod]
        public void TestFailureMessageContainsFullNameOfExceptionClass()
        {
            try
            {
                ExceptionAssert.Propagates<ExceptionClass>(() => { });
            }
            catch (AssertFailedException e)
            {
                StringAssert.Contains(e.Message, typeof(ExceptionClass).FullName);
            }
        }

        [TestMethod]
        public void TestFailureMessageEndsWithMessageParameter()
        {
            try
            {
                ExceptionAssert.Propagates<ExceptionClass>(() => { }, "special msg");
            }
            catch (AssertFailedException e)
            {
                StringAssert.EndsWith(e.Message, "special msg");
            }
        }

        [TestMethod]
        public void TestFailureMessageEndsWithMessageParameterWhenIncludeValidation()
        {
            try
            {
                ExceptionAssert.Propagates<ExceptionClass>(() => { }, e => { }, "special msg");
            }
            catch (AssertFailedException e)
            {
                StringAssert.EndsWith(e.Message, "special msg");
            }
        }

        [TestMethod]
        public void BlockThatThrowsExpectedTypeIsNotTestFailure()
        {
            ExceptionAssert.Propagates<ExceptionClass>(() => { throw new ExceptionClass(); });
        }

        [TestMethod]
        public void BlockThatThrowsExpectedTypeIsNotTestFailureForValidationMethod()
        {
            ExceptionAssert.Propagates<ExceptionClass>(() => { throw new ExceptionClass(); }, e => { });
        }

        [TestMethod]
        public void BlockThatThrowsSubclassOfExpectedTypeIsNotTestFailure()
        {
            ExceptionAssert.Propagates<ExceptionClass>(() => { throw new ExceptionSubClass(); });
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionClass))]
        public void BlockThatThrowsSuperclassOfExpectedTypePropagatesSuperclass()
        {
            ExceptionAssert.Propagates<ExceptionSubClass>(() => { throw new ExceptionClass(); });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception3))]
        public void ValidationIsInvoked()
        {
            ExceptionAssert.Propagates<ExceptionClass>(
                () => { throw new ExceptionClass(); },
                e => { throw new Exception3(); });
        }

        private class ExceptionClass : Exception { }
        private class ExceptionSubClass : ExceptionClass { }
        private class Exception3 : ExceptionClass { }
    }
}
