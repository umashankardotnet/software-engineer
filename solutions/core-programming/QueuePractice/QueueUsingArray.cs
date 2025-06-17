using System;
using System.Collections.Generic;
using System.Text;

namespace QueuePractice
{
    public class QueueUsingArray<T>
    {
        static int capacity = 100;
        T[] queue;
        int front, rear;

        public QueueUsingArray()
        {
            front = rear = 0;
            queue = new T[capacity];
        }

        // is empty
        public bool IsEmpy() => front == rear;

        // enqueue
        public void Enqueue(T item)
        {
            if (rear == capacity)
            {
                Console.WriteLine("Array is full");
                return;
            }
            queue[rear++] = item;
        }

        // dequeue
        public void Dequeue()
        {
            // check queue is empty
            if (front == rear)
            {
                Console.WriteLine("Queue is Empty.");
                return;
            }

            // delete from front
            for (int i = 0; i < rear - 1; i++)
            {
                queue[i] = queue[i + 1];
            }

            rear--;
        }

        // print all
        public void Display()
        {
            // check queue is empty
            if (front == rear)
            {
                Console.WriteLine("Queue is Empty.");
                return;
            }

            Console.WriteLine("Queue items are: \n");
            for (int i = front; i < rear; i++)
            {
                Console.WriteLine(queue[i]);
            }
        }
    }
}
