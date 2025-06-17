using System;

namespace StringPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringEquality();
            StringOperations();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void StringEquality()
        {
            string s1 = "bhanu";
            string s2 = s1;
            string s3 = "Bhanu";
            string s4 = new string(s1);
            Console.WriteLine(s1 == s2);
            Console.WriteLine(s1 == s3);
            Console.WriteLine(s4 == s1);

            Console.WriteLine($"reference equals - {string.ReferenceEquals(s4, s1)}");
            Console.WriteLine($"reference equals - {string.ReferenceEquals(s2, s1)}");
            Console.WriteLine(s1.Equals(s3, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(string.Equals(s1, s3, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("using compare");
            Console.WriteLine(s1.CompareTo(s3));
            Console.WriteLine(string.Compare(s1, s3, StringComparison.OrdinalIgnoreCase));

            s1 = "bhanu Pratap";
            s1 = s1.ToUpper();
            Console.WriteLine($"after modification to s1 {s2}");

            string s = new string(new char[] { 'a', 'c' });
            Console.WriteLine($"string is {s}");
        }

        /// <summary>
        /// can not modify the string, so string is immutable in C#.
        /// </summary>
        public void CheckStringImmutability()
        {
            string s = "Hello World";
            // s[5] = ',';
            Console.WriteLine(s);
        }

        public static void StringOperations()
        {
            string s = "Hello World";
            Console.WriteLine(s.Substring(2));
            Console.WriteLine($"index is - {s.IndexOf('e')}");
            Console.WriteLine($"last index is - {s.LastIndexOf('l')}");

            Console.WriteLine($"ends with - {s.EndsWith('w')}");

            var s1 = s.Clone();
            s = "Hi";
            Console.WriteLine($"Clone of string is - {s1.ToString()}");


            Console.WriteLine($"Hash code is - {s.GetHashCode()}");


            Console.WriteLine($"contains - {s.Contains('i')}");
        }
    }
}
