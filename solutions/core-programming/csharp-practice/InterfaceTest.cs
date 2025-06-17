using System;

namespace csharp_practice
{
    class InterfaceTest
    {
        public static void Main()
        {
            IA obj = new IAB();
            IB obj1 = new IAB();
            obj.Method1();
            obj1.Method1();
        }
    }

    class IAB : IA, IB
    {
        void IB.Method1()
        {
            Console.WriteLine("Hello IB method");
        }

        void IA.Method1()
        {
            Console.WriteLine("Hello IA method");
        }
    }

    interface IA
    {
        public void Method1();
    }

    interface IB
    {
        public void Method1();
    }
}
