using System;

namespace StackPractice
{
    public class StackUsingArray
    {
        static int capacity = 1000;
        int[] stack = new int[capacity];
        int top;

        public StackUsingArray()
        {
            top = -1;
        }

        public bool IsEmpty()
        {
            return (top < 0);
        }

        // push
        public void Push(int x)
        {
            if (top >= capacity)
                Console.WriteLine("Stack overflow");

            stack[++top] = x;
        }

        // pop
        public int Pop()
        {
            if (top < 0)
                Console.WriteLine("Stack underflow");
            return stack[top--];
        }

        // peek
        public int Peek()
        {
            if (top < 0)
                Console.WriteLine("Stack is empty");

            return stack[top];
        }
    }
}
