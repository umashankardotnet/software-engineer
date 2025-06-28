# Ultimate Guide: Two Pointer & Sliding Window Techniques


## 🔷 TWO POINTER TECHNIQUE

### 🔍 What is It?

It’s an optimization technique where **two indices (pointers)** move through a data structure (usually arrays or strings) from **either direction** or at **different speeds**, allowing us to reduce time complexity — especially helpful in **searching, comparing, or iterating over pairs**.



### ✅ Time Complexity Benefits

| Traditional              | Two Pointer                         |
| ------------------------ | ----------------------------------- |
| O(n²) using nested loops | O(n) or O(n log n) in most problems |

✅ Example: Finding all pairs with a given sum in a sorted array

* **Naive approach**: nested loop → O(n²)
* **Two Pointer**: one loop with two pointers → O(n)



### ✅ When to Use

* Input is **sorted**
* Looking for **pairs or comparisons** (sum, difference, closest)
* Need to **rearrange** or **check conditions** involving multiple indices
* Problems with **start and end positions**



### ❌ When *Not* to Use

* Non-contiguous requirements (e.g. subsets, combinations)
* Inputs not suited for direct linear traversal
* Dynamic conditions (e.g. changing window size)



### 🧪 Examples (Recap)

1. **Pair with target sum in sorted array**
2. **Reverse a string in-place**
3. **Remove duplicates from sorted array**
4. **Container with most water**

(See previous message for detailed C# implementations.)





## 🔶 SLIDING WINDOW TECHNIQUE

### 🔍 What is It?

The **Sliding Window** is a pattern that maintains a **contiguous block (or window)** over a part of an array or string. As the window **slides across**, you update the internal state (sum, frequency, length, etc.) to efficiently solve problems **without recomputation**.



### ✅ Time Complexity Benefits

| Traditional                                | Sliding Window   |
| ------------------------------------------ | ---------------- |
| O(n²) or worse (brute-force subarray scan) | O(n) or O(n + k) |

✅ Why?
Because you **reuse computation** inside the window:

* Add new item
* Remove outgoing item
* Update result — all in **constant time per operation**


## 🧱 Fixed-Size vs Variable-Size Window

| Feature  | Fixed-Size Window                        | Variable-Size Window                         |
| -------- | ---------------------------------------- | -------------------------------------------- |
| Size     | Constant (k elements)                    | Grows/Shrinks based on condition             |
| Goal     | Max/Min Sum, Count, Avg in k-size chunks | Longest/Shortest valid substring or subarray |
| Control  | Use `if (end >= k-1)`                    | Use condition check inside `while` loop      |
| Examples | Max sum of k elements, max vowels        | Longest substring without repeat, min window |



### ✅ Fixed-size Example: **Max Sum of K Elements**

```csharp
int[] arr = {2, 1, 5, 1, 3, 2};
int k = 3;
int maxSum = 0, windowSum = 0, start = 0;

for (int end = 0; end < arr.Length; end++)
{
    windowSum += arr[end];
    if (end >= k - 1)
    {
        maxSum = Math.Max(maxSum, windowSum);
        windowSum -= arr[start];
        start++;
    }
}
Console.WriteLine("Max sum: " + maxSum);  // Output: 9
```


### ✅ Variable-size Example: **Longest Substring Without Repeating Characters**

```csharp
string s = "abcabcbb";
int maxLength = 0, left = 0;
HashSet<char> seen = new HashSet<char>();

for (int right = 0; right < s.Length; right++)
{
    while (seen.Contains(s[right]))
    {
        seen.Remove(s[left]);
        left++;
    }
    seen.Add(s[right]);
    maxLength = Math.Max(maxLength, right - left + 1);
}
Console.WriteLine("Max length: " + maxLength);  // Output: 3
```


## 🔁 Two Pointer vs Sliding Window: Final Comparison

| Feature          | Two Pointer                        | Sliding Window                             |
| ---------------- | ---------------------------------- | ------------------------------------------ |
| Input            | Often sorted                       | Any array or string                        |
| Pointer Movement | Independent or from opposite sides | One expands, one shrinks to form a window  |
| Use Case         | Pair sums, reversal, partitioning  | Contiguous subarrays/substrings            |
| Window Size      | Not necessarily a window           | Fixed or variable window maintained        |
| Example Problems | Container With Most Water, Merge   | Max Sum Subarray, Longest Unique Substring |

---

## 🧩 When to Use What?

| Problem Type                              | Use This Technique        |
| ----------------------------------------- | ------------------------- |
| Pair/Triplet with Target Sum              | Two Pointer               |
| Reverse a String                          | Two Pointer               |
| Longest substring without repeating chars | Sliding Window (variable) |
| Max sum of subarray of size K             | Sliding Window (fixed)    |
| Minimum window containing all characters  | Sliding Window (variable) |
| Sorted Array Partitioning                 | Two Pointer               |


## 🧠 Key Benefits of Both

### ✅ Two Pointer:

* Eliminates nested loops
* Great for comparisons, sorted data
* Simplifies logic for reversing, merging

### ✅ Sliding Window:

* Optimized for contiguous blocks
* Avoids recomputation using incremental updates
* Handles dynamic conditions like longest, shortest, etc.


## ❗ Important Caveats

* **Don’t use sliding window** if the elements are not **contiguous**
* **Don’t use two pointer** if the problem involves **global searching**
* **Choose based on constraints** — especially if the problem hints at linear time or sorted input


## ✅ Practice Problems to Try

| Problem                                              | Type                      |
| ---------------------------------------------------- | ------------------------- |
| Two Sum (sorted array)                               | Two Pointer               |
| Max Sum Subarray of Size K                           | Sliding Window (fixed)    |
| Longest Substring with At Most K Distinct Characters | Sliding Window (variable) |
| Minimum Window Substring                             | Sliding Window (variable) |
| Container With Most Water                            | Two Pointer               |
| Remove Duplicates from Sorted Array                  | Two Pointer               |

