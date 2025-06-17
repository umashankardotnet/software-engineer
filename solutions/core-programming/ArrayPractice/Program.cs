using System;

namespace ArrayPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // DVD.process();

            // ArrayOperations();

            int[] arr = new int[] { 0, 4, 1, 0, 0, 8, 0, 0, 3 };
            DuplicateZeros(arr);
            Console.ReadLine();
        }

        private static void ArrayOperations()
        {
            ArrayOperations<int> operations = new ArrayOperations<int>();
            int[] arr = new int[] { 2, 3, 5, 1, 8, 3, 1, 9, 0, 2 };
            operations.InitializeArray(arr);

            operations.DeleteFromFront();
            Console.WriteLine("Array Items after Deletion:");
            operations.Display();

            operations.InsertLast(99);
            operations.InsertLast(101);
            Console.WriteLine("Array Items after insertion:");
            operations.Display();

            operations.DeleteFromIndex(1);
            Console.WriteLine("Array Items after Deleting from Index:");
            operations.Display();

            Console.WriteLine("Insertion at index");
            operations.InsertAtIndex(9, 11);
            operations.InsertAtIndex(1, 12);
            Console.WriteLine("Array Items after inserting at Index:");
            operations.Display();
        }

        public static void DuplicateZeros(int[] arr)
        {

            for (int i = 0; i < arr.Length - 1; i++)
            {

                if (arr[i] == 0)
                {

                    for (int j = arr.Length - 1; j > i; j--)
                    {

                        arr[j] = arr[j - 1];
                    }

                    // just skip next zero which was duplicated in previous step
                    i++;
                }
            }
        }
    }


    public class DVD
    {
        public string name;
        public int releaseYear;
        public string director;

        public DVD(string name, int relYear, string director)
        {
            this.name = name;
            releaseYear = relYear;
            this.director = director;
        }

        public override string ToString() => $"{name}, directed by {director}, released in {releaseYear}";

        public static void process()
        {
            DVD o = new DVD("QFLES", 2019, "bhanu");
            Console.WriteLine(o.ToString());
            DVD[] dvds = new DVD[15];
            dvds[7] = o;
            int[] arr = new int[4];
            Console.WriteLine(arr[3]);
        }
    }
}
