# Ultimate Guide: Two Pointer & Sliding Window Techniques


## TWO POINTER TECHNIQUE

### What is It?

It’s an optimization technique where **two indices (pointers)** move through a data structure (usually arrays or strings) from **either direction** or at **different speeds**, allowing us to reduce time complexity — especially helpful in **searching, comparing, or iterating over pairs**.



### Time Complexity Benefits

| Traditional              | Two Pointer                         |
| ------------------------ | ----------------------------------- |
| O(n²) using nested loops | O(n) or O(n log n) in most problems |

Example: Finding all pairs with a given sum in a sorted array

* **Naive approach**: nested loop → O(n²)
* **Two Pointer**: one loop with two pointers → O(n)



### When to Use

* Input is **sorted**
* Looking for **pairs or comparisons** (sum, difference, closest)
* Need to **rearrange** or **check conditions** involving multiple indices
* Problems with **start and end positions**



### When *Not* to Use

* Non-contiguous requirements (e.g. subsets, combinations)
* Inputs not suited for direct linear traversal
* Dynamic conditions (e.g. changing window size)



### Visual: How Two Pointers Work

#### Problem: Find two numbers in sorted array that sum to a target

```
Array: [1, 2, 3, 4, 6, 8, 9]
Target: 10

Start:
       L                 R
       ↓                 ↓
Array: [1, 2, 3, 4, 6, 8, 9]

Steps:
(1+9)=10 ✅ Done
```

```csharp
int[] arr = {1, 2, 3, 4, 6, 8, 9};
int target = 10;
int left = 0, right = arr.Length - 1;

while (left < right)
{
    int sum = arr[left] + arr[right];
    if (sum == target)
    {
        Console.WriteLine($"Pair found: {arr[left]}, {arr[right]}");
        break;
    }
    else if (sum < target) left++;
    else right--;
}
```


### Common Use Cases:

| Task                       | Example                     |
| -------------------------- | --------------------------- |
| Finding sum pairs/triplets | Two Sum, Three Sum          |
| Reversing array or string  | In-place reverse            |
| Sorted merge               | Merging sorted arrays       |
| Partitioning               | Dutch National Flag problem |


### Other Examples:

#### Reverse a String

```csharp
char[] s = {'h','e','l','l','o'};
int left = 0, right = s.Length - 1;

while (left < right)
{
    (s[left], s[right]) = (s[right], s[left]);
    left++; right--;
}
```


#### Remove Duplicates (Sorted Array)

```csharp
int[] nums = {1, 1, 2, 2, 3};
int i = 0;
for (int j = 1; j < nums.Length; j++)
{
    if (nums[j] != nums[i])
    {
        i++;
        nums[i] = nums[j];
    }
}
Console.WriteLine($"New length: {i + 1}");
```

---  

## SLIDING WINDOW TECHNIQUE

### What is It?

The **Sliding Window** is a pattern that maintains a **contiguous block (or window)** over a part of an array or string. As the window **slides across**, you update the internal state (sum, frequency, length, etc.) to efficiently solve problems **without recomputation**.



### Time Complexity Benefits

| Traditional                                | Sliding Window   |
| ------------------------------------------ | ---------------- |
| O(n²) or worse (brute-force subarray scan) | O(n) or O(n + k) |

Why?
Because you **reuse computation** inside the window:

* Add new item
* Remove outgoing item
* Update result — all in **constant time per operation**


## Fixed-Size vs Variable-Size Window

| Feature  | Fixed-Size Window                        | Variable-Size Window                         |
| -------- | ---------------------------------------- | -------------------------------------------- |
| Size     | Constant (k elements)                    | Grows/Shrinks based on condition             |
| Goal     | Max/Min Sum, Count, Avg in k-size chunks | Longest/Shortest valid substring or subarray |
| Control  | Use `if (end >= k-1)`                    | Use condition check inside `while` loop      |
| Examples | Max sum of k elements, max vowels        | Longest substring without repeat, min window |


### Visual: Sliding Window (Fixed)

#### Problem: Max sum of 3 consecutive elements

```
Array: [2, 1, 5, 1, 3, 2]
Window size (k) = 3

Sliding:
[2 1 5] → sum=8
  [1 5 1] → sum=7
    [5 1 3] → sum=9 ✅
      [1 3 2] → sum=6
```

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
```

---

### Visual: Sliding Window (Variable)

#### Problem: Longest substring without repeating characters

```
String: "abcabcbb"
Valid Window grows until repeat, then shrinks:

Step-by-step:
Window = "a"
Window = "ab"
Window = "abc" ✅
Then 'a' repeats → shrink from left
```

```csharp
string s = "abcabcbb";
int maxLength = 0, left = 0;
HashSet<char> set = new HashSet<char>();

for (int right = 0; right < s.Length; right++)
{
    while (set.Contains(s[right]))
    {
        set.Remove(s[left]);
        left++;
    }
    set.Add(s[right]);
    maxLength = Math.Max(maxLength, right - left + 1);
}
```

## Relationship: Two Pointer vs Sliding Window

| Aspect                | Two Pointer Technique                         | Sliding Window Technique                                      |
| --------------------- | --------------------------------------------- | ------------------------------------------------------------- |
| **Purpose**           | Find elements meeting a condition (e.g., sum) | Find a subarray/substring with specific property              |
| **Pointers**          | Two pointers may move independently           | One pointer expands, the other shrinks to maintain a "window" |
| **Typical Use Case**  | Pairs, distances, sorting, comparison         | Fixed/variable length subarrays/substrings                    |
| **Input Requirement** | Often works best on sorted arrays             | Works on any sequence                                         |
| **Common Problems**   | Two sum, reverse, container with water        | Longest substring with no repeat, max sum subarray            |


## How They Overlap

* **Sliding window** is often **implemented using two pointers**.
* The **"window"** is the portion of the array or string between the two pointers.
* One pointer (usually the right) expands the window, and the other (left) contracts it when needed to maintain a constraint.

## When to Use Which?

| Problem Type                             | Use This Technique        |
| ---------------------------------------- | ------------------------- |
| Pair/Triplet with Specific Sum           | Two Pointer               |
| Reverse or Compare Opposite Sides        | Two Pointer               |
| Longest Substring with Unique Chars      | Sliding Window (variable) |
| Max Sum Subarray of Size K               | Sliding Window (fixed)    |
| Minimum Window Containing All Characters | Sliding Window (variable) |
| Removing Duplicates (sorted input)       | Two Pointer               |
| Find All Anagrams in String              | Sliding Window + HashMap  |


## Real-World Coding Problems (Practice List)

| Problem                                        | Technique              |
| ---------------------------------------------- | ---------------------- |
| Two Sum (sorted)                               | Two Pointer            |
| Max Sum Subarray of Size K                     | Sliding Window (fixed) |
| Longest Substring Without Repeating Characters | Sliding Window         |
| Minimum Window Substring                       | Sliding Window         |
| Container With Most Water                      | Two Pointer            |
| Remove Duplicates in Sorted Array              | Two Pointer            |
| Find All Anagrams in a String                  | Sliding Window         |


## Key Benefits of Both

### Two Pointer:

* Eliminates nested loops
* Great for comparisons, sorted data
* Simplifies logic for reversing, merging

### Sliding Window:

* Optimized for contiguous blocks
* Avoids recomputation using incremental updates
* Handles dynamic conditions like longest, shortest, etc.


## ❗ Important Caveats

* **Don’t use sliding window** if the elements are not **contiguous**
* **Don’t use two pointer** if the problem involves **global searching**
* **Choose based on constraints** — especially if the problem hints at linear time or sorted input


## Final Takeaways

* Use **Two Pointer** for searching and comparing elements efficiently (often sorted inputs).
* Use **Sliding Window** when you’re dealing with contiguous elements and need to track things like **length**, **sum**, or **frequency**.
* Both techniques aim to **reduce time complexity** by avoiding nested iterations.
