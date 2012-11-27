using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestDll
{
    public sealed class TestClass : TestClassBase
    {
        public string NormalMethod()
        {
            return "TestClass";
        }

        public override string VirtualMethod()
        {
            return base.VirtualMethod();
        }

        public sealed override string SealedMethod()
        {
            return base.VirtualMethod();
        }

        public override string AbstractMethod()
        {
            return "TestClass";
        }
    }

    public abstract class TestClassBase
    {
        public virtual string VirtualMethod()
        {
            return "TestClass";
        }

        public virtual string SealedMethod()
        {
            return "TestClass";
        }

        public abstract string AbstractMethod();
    }
}
