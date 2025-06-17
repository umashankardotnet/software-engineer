using System;

namespace QueuePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoQueue();
            Console.ReadLine();
        }

        private static void DemoQueue()
        {
            QueueUsingArray<string> queue = new QueueUsingArray<string>();
            queue.Enqueue("bhanu");
            queue.Enqueue("krishna");
            queue.Enqueue("Shaurya");
            queue.Display();
            queue.Dequeue();
            queue.Dequeue();
            Console.WriteLine("items of queue after Dequeue operation: \n");
            queue.Display();
        }
    }
}
