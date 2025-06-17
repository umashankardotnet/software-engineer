using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_practice
{
    class StaticTest
    {
        public static void Main()
        {
            Empl o = new Empl();
            Console.ReadLine();
        }
    }

   public class Empl
    {
        public Empl()
        {
            Console.WriteLine("instance ctor");
        }

        static Empl()
        {
            Console.WriteLine("static ctor");
        }
    }

    public static class St1
    {
        // will ctonain only static members
       // int x = 0;
    }
}
