Got it — I’ll prepare you a **Complete C# Collections Guide** that is:

* **Exhaustive** — covers all major collections in `System.Collections`, `System.Collections.Generic`, `System.Collections.Concurrent`, and `System.Collections.Immutable`.
* Includes **time complexity** & **space complexity** for `Insert`, `Get`, and `Remove` operations.
* Explains **when to use** each collection.
* Shows **example usage** for every collection.

---

# **📚 Complete Guide to C# Collections**

C# collections fall into **four main families**:

1. **Non-generic collections** (`ArrayList`, `Hashtable`, etc.) — legacy, not type-safe, avoid in modern code.
2. **Generic collections** (`List<T>`, `Dictionary<TKey,TValue>`, etc.) — type-safe, most commonly used.
3. **Concurrent collections** (`ConcurrentDictionary`, `BlockingCollection`, etc.) — thread-safe.
4. **Immutable collections** (`ImmutableList<T>`, etc.) — read-only after creation.

---

## **1. Array**

* **Namespace:** `System`
* **Ordering:** Preserves index order
* **Duplicates:** Allowed
* **Resizing:** Not possible
* **Time Complexity:**

| Operation              | Time          | Space |
| ---------------------- | ------------- | ----- |
| Get by index           | O(1)          | O(1)  |
| Insert (end, if space) | O(1)          | O(1)  |
| Insert (resize)        | Not supported | -     |
| Remove (by shifting)   | O(n)          | O(1)  |

**When to use:** Fixed-size, index-based operations with high performance needs.

**Example:**

```csharp
int[] numbers = { 1, 2, 3 };
Console.WriteLine(numbers[1]); // 2
```

---

## **2. List<T>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Preserves insertion order
* **Duplicates:** Allowed
* **Resizing:** Yes (automatic doubling strategy)
* **Time Complexity:**

| Operation       | Time           | Space          |
| --------------- | -------------- | -------------- |
| Get by index    | O(1)           | O(1)           |
| Add (end)       | O(1) amortized | O(1) amortized |
| Insert (middle) | O(n)           | O(1)           |
| Remove by index | O(n)           | O(1)           |

**When to use:** Dynamic array for ordered, index-based access.

**Example:**

```csharp
var list = new List<string> { "A", "B" };
list.Add("C");
Console.WriteLine(list[2]); // "C"
```

---

## **3. Dictionary\<TKey, TValue>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Unordered (C# 9+ preserves insertion order but not guaranteed for logic)
* **Duplicates:** Keys: No, Values: Yes
* **Time Complexity:**

| Operation | Time (avg) | Space |
| --------- | ---------- | ----- |
| Get       | O(1)       | O(1)  |
| Insert    | O(1)       | O(1)  |
| Remove    | O(1)       | O(1)  |

**When to use:** Fast lookups by key.

**Example:**

```csharp
var dict = new Dictionary<int, string>();
dict[1] = "One";
Console.WriteLine(dict[1]); // "One"
```

---

## **4. HashSet<T>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Unordered
* **Duplicates:** Not allowed
* **Time Complexity:**

| Operation | Time (avg) | Space |
| --------- | ---------- | ----- |
| Add       | O(1)       | O(1)  |
| Contains  | O(1)       | O(1)  |
| Remove    | O(1)       | O(1)  |

**When to use:** Unique element storage, fast membership checks.

**Example:**

```csharp
var set = new HashSet<int> { 1, 2, 3 };
set.Add(3); // ignored
Console.WriteLine(set.Contains(2)); // True
```

---

## **5. Queue<T>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** FIFO (First In First Out)
* **Duplicates:** Allowed
* **Time Complexity:**

| Operation | Time | Space |
| --------- | ---- | ----- |
| Enqueue   | O(1) | O(1)  |
| Dequeue   | O(1) | O(1)  |
| Peek      | O(1) | O(1)  |

**When to use:** Processing items in arrival order.

**Example:**

```csharp
var queue = new Queue<string>();
queue.Enqueue("Task1");
Console.WriteLine(queue.Dequeue()); // Task1
```

---

## **6. Stack<T>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** LIFO (Last In First Out)
* **Duplicates:** Allowed
* **Time Complexity:**

| Operation | Time | Space |
| --------- | ---- | ----- |
| Push      | O(1) | O(1)  |
| Pop       | O(1) | O(1)  |
| Peek      | O(1) | O(1)  |

**When to use:** Reverse-order processing, undo operations.

**Example:**

```csharp
var stack = new Stack<int>();
stack.Push(10);
Console.WriteLine(stack.Pop()); // 10
```

---

## **7. LinkedList<T>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Insertion order
* **Duplicates:** Allowed
* **Time Complexity:**

| Operation        | Time | Space |
| ---------------- | ---- | ----- |
| AddFirst/AddLast | O(1) | O(1)  |
| Remove (by node) | O(1) | O(1)  |
| Search           | O(n) | O(1)  |

**When to use:** Frequent insertions/removals in middle of list.

**Example:**

```csharp
var linked = new LinkedList<string>();
linked.AddFirst("First");
linked.AddLast("Last");
```

---

## **8. SortedList\<TKey, TValue>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Sorted by key
* **Duplicates:** Keys: No, Values: Yes
* **Time Complexity:**

| Operation  | Time     | Space |
| ---------- | -------- | ----- |
| Get by key | O(log n) | O(1)  |
| Insert     | O(n)     | O(1)  |
| Remove     | O(n)     | O(1)  |

**When to use:** Small sorted datasets.

**Example:**

```csharp
var sortedList = new SortedList<int, string> { { 2, "B" }, { 1, "A" } };
```

---

## **9. SortedDictionary\<TKey, TValue>**

* **Namespace:** `System.Collections.Generic`
* **Ordering:** Sorted by key
* **Duplicates:** Keys: No, Values: Yes
* **Time Complexity:**

| Operation | Time     | Space |
| --------- | -------- | ----- |
| Get       | O(log n) | O(1)  |
| Insert    | O(log n) | O(1)  |
| Remove    | O(log n) | O(1)  |

**When to use:** Large sorted datasets with frequent inserts.

**Example:**

```csharp
var sortedDict = new SortedDictionary<string, int> { { "Banana", 2 } };
```

---

## **10. ObservableCollection<T>**

* **Namespace:** `System.Collections.ObjectModel`
* **Ordering:** Insertion order
* **Duplicates:** Allowed
* **Performance:** Similar to `List<T>`, plus event overhead.

**When to use:** Data binding in UI, auto notifications.

**Example:**

```csharp
var oc = new ObservableCollection<string>();
oc.CollectionChanged += (s, e) => Console.WriteLine("Changed");
oc.Add("Item1");
```

---

## **11. ConcurrentDictionary\<TKey, TValue>**

* **Namespace:** `System.Collections.Concurrent`
* **Ordering:** Unordered
* **Duplicates:** Keys: No, Values: Yes
* **Time Complexity:** O(1) average, thread-safe.

**When to use:** Multi-threaded dictionary access.

**Example:**

```csharp
var cd = new ConcurrentDictionary<int, string>();
cd.TryAdd(1, "One");
```

---

## **12. ConcurrentQueue<T>**

* **Thread-safe FIFO** queue.

**Example:**

```csharp
var cq = new ConcurrentQueue<int>();
cq.Enqueue(42);
cq.TryDequeue(out var result);
```

---

## **13. ConcurrentStack<T>**

* **Thread-safe LIFO** stack.

---

## **14. BlockingCollection<T>**

* Thread-safe wrapper for producer-consumer.

---

## **15. Immutable Collections**

* ImmutableList, ImmutableDictionary, etc.
* **O(1)** reads, **O(n)** updates (copies).


# **Concurrent Collections Overview**

Concurrent collections in **`System.Collections.Concurrent`** namespace are **thread-safe** and optimized for multi-threaded read/write access without manual locking.

They are designed to:

* Avoid explicit `lock` for common scenarios.
* Minimize contention via fine-grained locking or lock-free algorithms.
* Support high scalability in concurrent environments.

---

### 3. **Concurrent Collections List & Details**

#### **3.1 ConcurrentDictionary\<TKey, TValue>**

* **What it is**: Thread-safe dictionary for key-value pairs.
* **Best for**: When multiple threads add, read, or update items.
* **Key features**:

  * Atomic add/update (`TryAdd`, `TryUpdate`).
  * `GetOrAdd` and `AddOrUpdate` methods for safe mutations.
* **Example**:

```csharp
using System;
using System.Collections.Concurrent;

class Program
{
    static void Main()
    {
        var dict = new ConcurrentDictionary<string, int>();

        dict.TryAdd("A", 1);
        dict.AddOrUpdate("A", 2, (key, oldValue) => oldValue + 1); // updates to 2
        int value = dict.GetOrAdd("B", 5); // adds B=5

        foreach (var kvp in dict)
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
}
```

---

#### **3.2 ConcurrentQueue<T>**

* **What it is**: Thread-safe FIFO (First-In-First-Out) queue.
* **Best for**: Producer-consumer scenarios where multiple threads enqueue/dequeue.
* **Example**:

```csharp
var queue = new ConcurrentQueue<int>();

queue.Enqueue(1);
queue.Enqueue(2);

if (queue.TryDequeue(out int result))
    Console.WriteLine($"Dequeued: {result}");
```

---

#### **3.3 ConcurrentStack<T>**

* **What it is**: Thread-safe LIFO (Last-In-First-Out) stack.
* **Best for**: Multi-threaded scenarios requiring stack behavior.
* **Example**:

```csharp
var stack = new ConcurrentStack<string>();

stack.Push("A");
stack.Push("B");

if (stack.TryPop(out string item))
    Console.WriteLine($"Popped: {item}");
```

---

#### **3.4 ConcurrentBag<T>**

* **What it is**: Thread-safe unordered collection allowing duplicates.
* **Best for**: Storing items in parallel without needing order.
* **Example**:

```csharp
var bag = new ConcurrentBag<int>();

bag.Add(1);
bag.Add(2);

if (bag.TryTake(out int num))
    Console.WriteLine($"Removed: {num}");
```

---

#### **3.5 BlockingCollection<T>**

* **What it is**: Thread-safe collection that blocks on add/remove when full/empty.
* **Best for**: Producer-consumer with bounded capacity.
* **Wraps**: Any `IProducerConsumerCollection<T>` (like `ConcurrentQueue<T>`).
* **Example**:

```csharp
var collection = new BlockingCollection<int>(boundedCapacity: 5);

collection.Add(1);
collection.Add(2);

int item = collection.Take(); // waits if empty
```

---

#### **3.6 IProducerConsumerCollection<T>**

* **What it is**: Base interface for thread-safe producer-consumer collections.
* **Implementations**: `ConcurrentQueue<T>`, `ConcurrentStack<T>`, `ConcurrentBag<T>`.
* **Usage**: Generally not used directly—used via implementations.

---

#### **3.7 Partitioner**

* **What it is**: Splits data into partitions for parallel processing.
* **Best for**: When processing large data sets with `Parallel.ForEach`.
* **Example**:

```csharp
using System.Collections.Concurrent;
using System.Threading.Tasks;

var rangePartitioner = Partitioner.Create(0, 100);
Parallel.ForEach(rangePartitioner, (range) =>
{
    for (int i = range.Item1; i < range.Item2; i++)
    {
        Console.WriteLine(i);
    }
});
```

---

#### **3.8 ConcurrentExclusiveSchedulerPair**

* **What it is**: Schedules tasks for concurrent reads and exclusive writes.
* **Best for**: Balancing read/write workloads in async code.

---

#### **3.9 OrderablePartitioner<TSource>**

* **What it is**: Like `Partitioner`, but preserves element ordering for parallel loops.

---

#### **3.10 Immutable Collections (Thread-safe read)**

While not truly *concurrent* for write operations, immutable collections (`ImmutableList<T>`, etc.) are inherently thread-safe for reads.

---

### 4. **When to Use Which Concurrent Collection**

| Collection               | Ordering              | Allows Duplicates | Best For                            |
| ------------------------ | --------------------- | ----------------- | ----------------------------------- |
| **ConcurrentDictionary** | Key-based             | No (unique keys)  | Shared key-value store              |
| **ConcurrentQueue**      | FIFO                  | Yes               | Ordered task queue                  |
| **ConcurrentStack**      | LIFO                  | Yes               | Backtracking / undo stacks          |
| **ConcurrentBag**        | Unordered             | Yes               | Fast unordered storage              |
| **BlockingCollection**   | Depends on inner type | Yes               | Producer-consumer with backpressure |

---

### 5. **Performance & Thread Safety Notes**

* For **read-heavy** workloads: Consider **immutable collections** or normal collections with `ReaderWriterLockSlim`.
* For **write-heavy** workloads: Use **concurrent collections** to avoid lock contention.
* For **ordered processing**: Use `ConcurrentQueue` or `BlockingCollection`.
* Avoid **ConcurrentBag** if order matters—it’s optimized for unordered parallel access.

---

# **C# Collections Master Guide**



### **1. Introduction**

* **Why C# Collections Matter in Interviews**
* The **.NET Collections Framework** categories:

  * Non-Generic (legacy, `ArrayList`, `Queue`, `Stack`, `Hashtable`)
  * Generic (`List<T>`, `Dictionary<TKey, TValue>`, etc.)
  * Sorted (`SortedList`, `SortedDictionary`, `SortedSet`)
  * Immutable (`ImmutableList<T>`, `ImmutableDictionary`, etc.)
  * Concurrent (`ConcurrentDictionary`, `ConcurrentQueue`, etc.)
  * Specialized (`BitArray`, `NameValueCollection`, `BlockingCollection`)

---

### **2. Master Complexity Table**

*(Full Insert / Get / Remove complexities — merged from earlier work)*

| Collection                 | Category    | Ordering              | Duplicates | Insert (Avg/Worst) | Get (Avg/Worst) | Remove (Avg/Worst) | Space     | Notes / Use Case                  |
| -------------------------- | ----------- | --------------------- | ---------- | ------------------ | --------------- | ------------------ | --------- | --------------------------------- |
| Array                      | Non-Generic | Yes                   | Yes        | O(1)/O(n)          | O(1)            | O(n)               | O(n)      | Fixed size, fastest random access |
| ArrayList                  | Non-Generic | Yes                   | Yes        | O(1)/O(n)          | O(1)            | O(n)               | O(n)      | Legacy, boxing overhead           |
| List<T>                    | Generic     | Yes                   | Yes        | O(1)/O(n)          | O(1)            | O(n)               | O(n)      | Most used, dynamic array          |
| LinkedList<T>              | Generic     | Yes                   | Yes        | O(1)               | O(n)            | O(1)               | O(n)      | Best for frequent inserts/removes |
| Queue<T>                   | Generic     | FIFO                  | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Scheduling, BFS                   |
| Stack<T>                   | Generic     | LIFO                  | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Undo, backtracking                |
| HashSet<T>                 | Generic     | No                    | No         | O(1)/O(n)          | O(1)/O(n)       | O(1)/O(n)          | O(n)      | Unique storage                    |
| SortedSet<T>               | Sorted      | Yes                   | No         | O(log n)           | O(log n)        | O(log n)           | O(n)      | Auto-sorted unique items          |
| Dictionary\<K,V>           | Generic     | No                    | Keys: No   | O(1)/O(n)          | O(1)/O(n)       | O(1)/O(n)          | O(n)      | Fastest lookups                   |
| SortedDictionary\<K,V>     | Sorted      | Yes                   | Keys: No   | O(log n)           | O(log n)        | O(log n)           | O(n)      | Keys always sorted                |
| SortedList\<K,V>           | Sorted      | Yes                   | Keys: No   | O(log n)+O(n)      | O(log n)        | O(n)               | O(n)      | Memory efficient sorted map       |
| ObservableCollection<T>    | Generic     | Yes                   | Yes        | O(1)/O(n)          | O(1)            | O(n)               | O(n)      | UI data binding                   |
| ImmutableList<T>           | Immutable   | Yes                   | Yes        | O(n)               | O(1)            | O(n)               | O(n)      | Thread-safe list                  |
| ImmutableDictionary\<K,V>  | Immutable   | No                    | Keys: No   | O(log n)           | O(log n)        | O(log n)           | O(n)      | Thread-safe dictionary            |
| BitArray                   | Specialized | Index-based bits      | N/A        | O(1)               | O(1)            | O(1)               | O(n) bits | Memory-efficient flags            |
| ConcurrentDictionary\<K,V> | Concurrent  | No                    | Keys: No   | O(1)/O(n)          | O(1)/O(n)       | O(1)/O(n)          | O(n)      | Multi-threaded key-value          |
| ConcurrentQueue<T>         | Concurrent  | FIFO                  | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Thread-safe FIFO                  |
| ConcurrentStack<T>         | Concurrent  | LIFO                  | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Thread-safe stack                 |
| ConcurrentBag<T>           | Concurrent  | No                    | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Thread-safe unordered bag         |
| BlockingCollection<T>      | Concurrent  | Depends on inner type | Yes        | O(1)               | O(1)            | O(1)               | O(n)      | Producer-consumer queue           |
| NameValueCollection        | Specialized | Yes (by key order)    | Keys: Yes  | O(1)/O(n)          | O(1)/O(n)       | O(1)/O(n)          | O(n)      | Multiple values per key           |

---

### **3. Detailed Usage & Examples for Each Collection**

For each collection:

* **When to use**
* **Code snippet**
* **Performance considerations**
* **Common pitfalls**
* **Real-world example**
  *(Example: Employee list, Product catalog, Thread-safe logging queue, etc.)*

Example for **List<T>**:

```csharp
var employees = new List<string> { "Alice", "Bob" };
employees.Add("Charlie"); // O(1) amortized
Console.WriteLine(employees[1]); // O(1) - "Bob"
employees.Remove("Alice"); // O(n) - shift
```

---

### **4. Decision Flowchart: Choosing the Right Collection**

```
Start
│
├── Do you need thread safety?
│     ├── Yes → Concurrent Collections
│     └── No
│
├── Do you need ordering?
│     ├── Yes → List<T> / LinkedList<T> / Sorted*
│     └── No → HashSet<T>, Dictionary<K,V>
│
├── Do you need sorting?
│     ├── Yes → SortedSet<T>, SortedList<K,V>, SortedDictionary<K,V>
│     └── No → Normal list/set/dictionary
│
└── Do you need immutability?
      ├── Yes → Immutable Collections
      └── No → Normal collections
```

---

### **5. Common Interview Traps**

* **Hash collisions** and chaining in `Dictionary`
* `LinkedList<T>` O(n) access vs O(1) insertion
* `SortedList<K,V>` costly mid-inserts
* `ImmutableList<T>` full copy on change
* `ConcurrentBag<T>` is unordered → don’t expect sequence

---

### **6. Space-Time Tradeoffs**

* **Array vs LinkedList** — array is faster for access, linked list for frequent middle insert/remove.
* **HashSet vs List** — hashset has faster lookup but higher memory usage.
* **SortedDictionary vs SortedList** — dictionary better for large dynamic data, list better for small static sorted data.
