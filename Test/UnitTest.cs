﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDll;
using Moq.Protected;

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
            var test = new Mock<TestClass> { CallBase = true };
            test.Setup(t => t.NormalMethod()).Returns("Mock");
            test.Setup(t => t.VirtualMethod()).Returns("Mock");
            test.Setup(t => t.SealedMethod()).Returns("Mock");
            test.Setup(t => t.AbstractMethod()).Returns("Mock");
            test.Protected().Setup<string>("PrivateMethod").Returns("Mock");

            Assert.AreEqual(test.Object.NormalMethod(), "Mock");
            Assert.AreEqual(test.Object.VirtualMethod(), "Mock");
            Assert.AreEqual(test.Object.SealedMethod(), "Mock");
            Assert.AreEqual(test.Object.AbstractMethod(), "Mock");
            Assert.AreEqual(test.Object.CallPrivateMethod(), "Mock");
        }
    }
}
