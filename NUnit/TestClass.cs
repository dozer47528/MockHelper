using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using TestDll;

namespace NUnit
{
    [TestFixture]
    public class UnitTest
    {
        [TestCase]
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
