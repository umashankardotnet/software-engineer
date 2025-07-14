# Guide: Internal Working of Dictionary in C\#

## Overview

A `Dictionary<TKey, TValue>` in C# is a powerful collection type that provides fast lookups, insertions, and deletions. Internally, it is implemented using a **hash table** and handles **collisions** using a method called **chaining**.

---

## Components of a Dictionary

### 1. **Buckets Array**

* An integer array that stores indices of entries in the `Entry[]` array.
* Each index in the bucket represents a **hash bucket**.

### 2. **Entries Array**

```csharp
struct Entry
{
    public int hashCode;
    public int next;        // Index of the next item in the same bucket
    public TKey key;
    public TValue value;
}
```

* Stores the actual data (key-value pairs).
* `next` is used to chain entries in the same bucket (in case of collisions).

### 3. **Count**

* Tracks the number of entries.

### 4. **Comparer**

* Optional custom `IEqualityComparer<TKey>` used to compute hash codes and test equality.

---

## Key Concepts

### Hash Code

* C# uses `key.GetHashCode()` to convert a key into an integer.
* The hash code is then mapped to a bucket using:

```csharp
int index = hashCode % buckets.Length;
```

### Collision

* Occurs when **two different keys** hash to the **same bucket index**.
* Example:

```csharp
"apple".GetHashCode() % 5 = 0
"grape".GetHashCode() % 5 = 0
```

* Both keys go into **bucket 0**.

### Chaining

* C# handles collisions by chaining multiple entries in the same bucket.
* Each entry has a `next` field pointing to the next entry in the chain.
* This is known as **separate chaining**.

### Adding an Entry

1. Compute hash code
2. Find bucket index
3. If bucket is empty, insert entry
4. If bucket is occupied:

   * Traverse the chain using `.next`
   * If key exists, update value or throw exception
   * If not, append new entry to the chain

### Lookup (Get)

1. Compute hash code
2. Find bucket index
3. Traverse entries in the chain
4. Compare keys using `.Equals()`

### Resizing

* When the load factor exceeds a threshold (\~0.72), the dictionary resizes:

  * New bucket and entry arrays are created (usually 2x size)
  * All entries are **rehashed** and reinserted

---

## Performance

| Operation      | Average Time | Worst Case (many collisions) |
| -------------- | ------------ | ---------------------------- |
| Add/Get/Remove | O(1)         | O(n)                         |
| Resize         | O(n)         | O(n)                         |

---

## Summary Table

| Feature            | Details                              |
| ------------------ | ------------------------------------ |
| Data Structure     | Hash table with separate chaining    |
| Collision Handling | Linked entries using `next` pointers |
| Lookup Time        | O(1) average, O(n) worst             |
| Custom Comparer    | Yes (`IEqualityComparer<TKey>`)      |
| Auto-Resizing      | Yes, on exceeding load factor        |

---

## Real-World Tip

> In interviews or systems work, say: "C# Dictionary uses a hash table with separate chaining. Collisions are handled via linked `Entry` chains. Keys are compared using `Equals()` and `GetHashCode()`. The structure resizes dynamically based on load factor."

---

## Optional: Force a Collision Example (for Demo)

```csharp
var dict = new Dictionary<string, string>();
dict.Add("key1", "value1");
dict.Add("key2", "value2"); // Try creating a custom type that forces same hash for both
```

You'd need a custom class with overridden `GetHashCode()` for testing collision handling explicitly.
