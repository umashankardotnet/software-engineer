namespace DSAPractice
{
    public class CircularBufferImplementation
    {
        const int DefaultBufferSize = 3;
        int writeIndex = 0;
        int readIndex = 0;
        int count = 0;
        int?[] buffer = new int?[DefaultBufferSize];

        public void Add(int item)
        {
            buffer[writeIndex] = item;
            writeIndex = (writeIndex + 1) % buffer.Length;

            if (count < DefaultBufferSize)
            {
                count++;
            }
            else
            {
                Console.WriteLine("Buffer is Full");
                readIndex = (readIndex + 1) % DefaultBufferSize;
            }

        }

        public int? Get()
        {
            if (count == 0) return -1;


            var item = buffer[readIndex];

            readIndex = (readIndex + 1) % DefaultBufferSize;
            count--;

            return item;
        }

        public void PrintAll()
        {
            for (int i = readIndex; i < count; i++)
            {
                Console.WriteLine(buffer[i]);
            }

            for (int i = 0; i < readIndex; i++)
            {
                Console.WriteLine(buffer[i]);
            }
        }
    }

    class BufferClient
    {
        public void Test()
        {
            CircularBufferImplementation buffer = new CircularBufferImplementation();
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4);
            Console.WriteLine(buffer.Get());
            buffer.Add(5);
            buffer.Add(6);
            buffer.PrintAll();
        }
    }
}
