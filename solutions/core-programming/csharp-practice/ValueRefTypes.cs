using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_practice
{
    class ValueRefTypes
    {
        public static void Main()
        {
            A1 o = new A1();
            o.disp();
            Console.ReadLine();
        }
    }

    class A1
    {
        public void disp()
        {
            Emp e = new Emp();
            e.Name = "bhanu";
            disp2(e);
            Console.WriteLine(e.Age);

            // what will be output
            Emp e1 = e;
            e.Name = "New- Name";
            Console.WriteLine(e1.Name);
        }

        void disp2(Emp e)
        {
            e.Age = 22;
            Console.WriteLine(e.Name);
        }
    }

    class Emp
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}


// Deep copy and shallow copy (how ref impact parameter passing class type, if dont want modification on new object then how)
// How GC works for memory allocation and deallocation
// How variables are stored in Stack and Heap
// What do you mean by memory when running a C# program or application
// Indexing in details (Clustered and Non-clustered) where data stores in each case
//
