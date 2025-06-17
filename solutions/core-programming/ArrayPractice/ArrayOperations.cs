using System;

namespace ArrayPractice
{
    public class ArrayOperations<T>
    {
        T[] array;
        int length = 10;

        public ArrayOperations()
        {
            array = new T[length];
        }

        public void InitializeArray(T[] arr)
        {
            array = arr;
        }

        public void Display()
        {
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        public void DeleteFromFront()
        {
            if (length == 0)
                return;

            for (int i = 1; i < length; i++)
            {
                array[i - 1] = array[i];
            }
            length--;
        }

        public void DeleteFromIndex(int index)
        {
            if (length == 0)
                return;

            for (int i = index + 1; i < length; i++)
            {
                array[i - 1] = array[i];
            }
            length--;
        }

        public void InsertLast(T item)
        {
            if (length == array.Length)
            {
                Console.WriteLine("Array is Full");
                return;
            }

            array[length++] = item;
        }

        public void InsertAtIndex(int index, T item)
        {
            if (length == array.Length)
            {
                Console.WriteLine("Array is Full");
                return;
            }

            for (int i = length; i > index; i--)
            {
                array[i] = array[i - 1];
            }

            array[index] = item;
            length++;
        }


    }
}
