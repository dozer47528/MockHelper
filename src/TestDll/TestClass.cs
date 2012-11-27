using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDll
{
    public sealed class TestClass : TestClassBase
    {
        public string Method1()
        {
            return "TestClass";
        }
        public override string Method2()
        {
            return base.Method2();
        }
    }

    public class TestClassBase
    {
        public virtual string Method2()
        {
            return "TestClass";
        }
    }
}
