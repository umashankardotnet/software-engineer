# Complete Guide to Arrays and Related Operations in C\#

## 1. What is an Array?

An array is a **fixed-size**, **zero-indexed**, **homogeneous** (same data type) collection of elements stored in **contiguous memory locations**.

### Syntax

```csharp
int[] numbers = new int[5];       // Declaration with size
int[] numbers2 = {1, 2, 3, 4, 5}; // Initialization
```

## 2. Key Characteristics

* Fixed size (defined at creation)
* Zero-based indexing
* Efficient access: O(1) time complexity for element access
* Stored in contiguous memory

## 3. Types of Arrays

### Single-Dimensional Array

```csharp
int[] arr = new int[5];
```

### Multi-Dimensional Array

```csharp
int[,] matrix = new int[3, 3];
```

### Jagged Array (Array of Arrays)

```csharp
int[][] jagged = new int[2][];
jagged[0] = new int[] {1, 2};
jagged[1] = new int[] {3, 4, 5};
```

## 4. Common Operations

### Traversal

```csharp
for (int i = 0; i < arr.Length; i++)
    Console.WriteLine(arr[i]);

foreach (int value in arr)
    Console.WriteLine(value);
```

### Searching

```csharp
int index = Array.IndexOf(arr, 3);
```

### Sorting

```csharp
Array.Sort(arr);
```

### Reversing

```csharp
Array.Reverse(arr);
```

### Copying

```csharp
int[] newArr = new int[arr.Length];
Array.Copy(arr, newArr, arr.Length);
```

### Resizing

```csharp
Array.Resize(ref arr, 10);
```

## 5. Common Use Cases

* Storing static data (e.g., days of week)
* Looping through fixed-size data
* Buffering data (e.g., circular buffer)
* Working with APIs that expect arrays
* Implementing algorithms and data structures

## 6. Real-World Examples

### Temperature Logger

```csharp
class TemperatureLogger
{
    private int[] temperatures = new int[7];

    public void Log(int day, int temp)
    {
        if (day >= 0 && day < 7)
            temperatures[day] = temp;
    }

    public double Average()
    {
        return temperatures.Average();
    }
}
```

## 7. Array Techniques for Problem Solving

### Brute Force

Nested loops for all combinations (O(n^2))

### Sorting + Two Pointers

Used for sum pairs, duplicates, etc. (O(n log n))

### Hashing

For quick lookups, frequency count (O(n) time, O(n) space)

### Sliding Window

Used for subarray sums, averages, max (O(n) time)

### Prefix Sum

For range sums or subarrays (O(n) time, O(n) space)

### Binary Search

For sorted arrays (O(log n))

## 8. Best Practices

* Prefer `List<T>` for dynamic data
* Avoid resizing arrays frequently
* Use meaningful names and constants
* Consider memory vs performance tradeoffs

## 9. When Not to Use Arrays

* When the size is unknown or dynamic
* When you need insertions/removals in the middle
* When built-in collections (like List<T>, Dictionary<T>) are better suited

## 10. Alternatives

* `List<T>`: Dynamic array with more features
* `LinkedList<T>`: For frequent insertions/removals
* `Dictionary<TKey, TValue>`: For key-value mappings
* `Queue<T>` / `Stack<T>`: For FIFO/LIFO access patterns
