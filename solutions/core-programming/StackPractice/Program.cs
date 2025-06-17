using System;

namespace StackPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            StackUsingArrayTest();
        }


        private static void StackUsingArrayTest()
        {
            StackUsingArray st = new StackUsingArray();
            st.Push(1);
            st.Push(2);
            st.Push(3);
            int top = st.Peek();
            Console.WriteLine(top);
            st.Pop();
            top = st.Peek();
            Console.WriteLine(top);
        }
    }
}
