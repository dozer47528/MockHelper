using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDll;

namespace Test
{
    /// <summary>
    /// UnitTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = new Mock<TestClass>();
            test.Setup(t => t.Method1()).Returns("Mock");
            test.Setup(t => t.Method2()).Returns("Mock");
            Assert.AreEqual(test.Object.Method1(), "Mock");
            Assert.AreEqual(test.Object.Method2(), "Mock");
        }
    }
}
