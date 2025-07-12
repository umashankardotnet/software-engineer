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

* Try all combinations using nested loops
* Time Complexity: O(n^2) or worse

```csharp
for (int i = 0; i < arr.Length; i++)
    for (int j = i + 1; j < arr.Length; j++)
        // logic
```

### Sorting + Two Pointers

* Sort array first, then use left/right pointers to search
* Used in problems like Two Sum, Three Sum, etc.
* Time Complexity: O(n log n) for sort + O(n) for scan

### Hashing (Dictionary or HashSet)

* Store frequencies or fast lookups
* Common in duplicate detection, pair sums
* Time: O(n), Space: O(n)

### Sliding Window

* Used for contiguous subarrays
* Maintain a fixed-size or variable-size window while iterating
* Time: O(n)

```csharp
int maxSum = 0, windowSum = 0;
for (int i = 0; i < k; i++) windowSum += arr[i];
for (int i = k; i < arr.Length; i++) {
    windowSum += arr[i] - arr[i - k];
    maxSum = Math.Max(maxSum, windowSum);
}
```

### Prefix Sum

* Precompute cumulative sums to answer range queries quickly
* Used in subarray sum problems
* Time: O(n), Space: O(n)

### Binary Search

* Use when array is sorted
* Can be used for exact match, bounds, peaks, etc.
* Time: O(log n)

### Kadane’s Algorithm

* Maximum subarray sum in O(n)
* Maintain a running sum and max

### Stack/Queue

* Use in monotonic stack problems, Next Greater Element, etc.
* Queue is useful in sliding window max/min

### Two Pointers

* Used when array is sorted or when scanning from both ends
* Common in duplicate removal, pair sums, palindrome check, etc.

### Frequency Array

* Use fixed-size arrays for counting characters/numbers when possible
* Ideal for small fixed-size domains (e.g., lowercase letters)

### Backtracking

* Useful in permutations, combinations, subsets problems

### Recursion + Memoization (DP)

* Used when subproblems repeat, e.g., max path, coin change

### Greedy

* Used when local optimum leads to global optimum
* Examples: Jump Game, Activity Selection


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
