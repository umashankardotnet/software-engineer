using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_practice
{
    class ChildBaseTest
    {
        static void Main()
        {
            A a = new B();
            a.method1();
            a.method2();


            //Base b = new Base();
            //b.Add();

            //// Child c = new Base(); // error

            //Base bc = new Child();
            //bc.Add();

            //Console.WriteLine("Most recent override example");
            //Child2 c2 = new Child2();
            //c2.Add();

            //// abstract
            //Console.WriteLine("abstract class example");
            //Child1 ch1 = new Child1();
            Console.ReadLine();
        }
    }

    class Base
    {
        public virtual void Add()
        {
            Console.WriteLine("base method");
        }

    }

    class Child : Base
    {
        public override void Add()
        {
            Console.WriteLine("child method");
            base.Add();
        }
    }

    class Child2 : Child
    {

    }

    abstract class Base1
    {
        public Base1()
        {
            Console.WriteLine("base abstract const");
        }
        public virtual void Add()
        {
            Console.WriteLine("base method");
        }

        public abstract void Add1();
    }

    class Child1 : Base1
    {
        public Child1()
        {
            Console.WriteLine("child ctor of abstract base");
        }
        public override void Add()
        {
            Console.WriteLine("child method");
            base.Add();
        }

        public override void Add1()
        {
            Console.WriteLine("abstract method implementation");
        }
    }

    public class A
    {
        public void method1()
        {
            Console.WriteLine("A Method1");
        }

        public virtual void method2()
        {
            Console.WriteLine("A Method2");
        }
    }

    public class B : A
    {
        public new void method1()
        {
            Console.WriteLine("B Method1");

        }
        public override void method2()
        {
            Console.WriteLine("B Method2");
        }
    }
}
