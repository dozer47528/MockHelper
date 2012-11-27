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
            test.Setup(t => t.NormalMethod()).Returns("Mock");
            test.Setup(t => t.VirtualMethod()).Returns("Mock");
            test.Setup(t => t.SealedMethod()).Returns("Mock");
            test.Setup(t => t.AbstractMethod()).Returns("Mock");

            Assert.AreEqual(test.Object.NormalMethod(), "Mock");
            Assert.AreEqual(test.Object.VirtualMethod(), "Mock");
            Assert.AreEqual(test.Object.SealedMethod(), "Mock");
            Assert.AreEqual(test.Object.AbstractMethod(), "Mock");
        }
    }
}
