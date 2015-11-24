using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForTest;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void SumTest()
        {
            MabLib lib = new MabLib();
            int t1 = 1;
            int t2 = 3;
            int expected = 5;
            int actual = lib.Sum(t1, t2);
            Assert.AreEqual(expected, actual, "和预期不符！");
        }


    }
}
