using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scb
{
    [TestClass]
    public class LoggableCollectionAssertUnitTest
    {
        [TestMethod]
        public void ContainsPassesForContains()
        {
            LoggableCollectionAssert.Contains(new[] { 1 }, 1);
        }

        [TestMethod]
        public void ContainsFailsForNotContains()
        {
            ExceptionAssert.Propagates<UnitTestAssertException>(() => LoggableCollectionAssert.Contains(new[] { 2 }, 1));
        }

        [TestMethod]
        public void AreEqualPassesForEqual()
        {
            LoggableCollectionAssert.AreEqual(new[] { 1 }, new[] { 1 });
        }

        [TestMethod]
        public void AreEqualFailsForUnequal()
        {
            ExceptionAssert.Propagates<UnitTestAssertException>(() => LoggableCollectionAssert.AreEqual(new[] { 0 }, new[] { 1 }));
        }

        [TestMethod]
        public void AreEquivalentPassesForSameExceptForOrder()
        {
            LoggableCollectionAssert.AreEquivalent(new[] { 1, 2 }, new[] { 2, 1 });
        }

        [TestMethod]
        public void AreEquivalentFailsForUnequal()
        {
            ExceptionAssert.Propagates<UnitTestAssertException>(() => LoggableCollectionAssert.AreEquivalent(new[] { 0 }, new[] { 1 }));
        }
    }
}
