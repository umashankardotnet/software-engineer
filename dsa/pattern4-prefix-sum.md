# prefix sum array

## **What is a Prefix Sum Array?**

A **prefix sum array** stores cumulative sums so you can quickly calculate the sum of any subarray in **O(1)** time after an **O(n)** preprocessing step.

---

### **Example**

Given:

```
arr = [2, 4, 6, 8]
```

Prefix sum array:

```
prefix[0] = arr[0]                 = 2
prefix[1] = arr[0] + arr[1]         = 6
prefix[2] = arr[0] + arr[1] + arr[2]= 12
prefix[3] = arr[0] + arr[1] + arr[2] + arr[3] = 20
```

So:

```
prefix = [2, 6, 12, 20]
```

---

### **Formula**

For `i > 0`:

```
prefix[i] = prefix[i-1] + arr[i]
```

For `i = 0`:

```
prefix[0] = arr[0]
```

---

### **Use Case**

To find the sum of `arr[l..r]`:

```
sum = prefix[r] - prefix[l-1]   // if l > 0
sum = prefix[r]                 // if l = 0
```

**Time:** O(1) per query after building prefix.
**Space:** O(n).

---

### **C# Example**

```csharp
public static int[] BuildPrefixSum(int[] arr)
{
    int n = arr.Length;
    int[] prefix = new int[n];
    prefix[0] = arr[0];

    for (int i = 1; i < n; i++)
    {
        prefix[i] = prefix[i - 1] + arr[i];
    }

    return prefix;
}

public static int RangeSum(int[] prefix, int l, int r)
{
    if (l == 0) return prefix[r];
    return prefix[r] - prefix[l - 1];
}
```

---

⏳ **Time Complexity**:

* Build: **O(n)**
* Query: **O(1)**

📦 **Space Complexity**: **O(n)**


## **Prefix Sum Pattern — Complete Guide**

### 1️⃣ What is the Prefix Sum Pattern?

The prefix sum pattern is a **preprocessing technique** where you store **cumulative sums** (or other cumulative computations) in an auxiliary array so you can **answer range-based queries in O(1)** instead of O(n).

The **core idea**:

```
prefix[i] = prefix[i-1] + arr[i]
```

For any range sum query:

```
sum(l, r) = prefix[r] - prefix[l-1]    // if l > 0
sum(0, r) = prefix[r]                  // if l = 0
```

---

### 2️⃣ When to Use This Pattern?

Use **Prefix Sum** when:

* You have **multiple range queries** on the same array.
* You need **sum of subarrays** quickly.
* You need to **transform range updates** or solve **count-based queries**.
* The array doesn't change often (because updates require recomputation in normal prefix sum).

---

### 3️⃣ Common Interview Problems Using Prefix Sum

#### **Problem 1: Range Sum Query**

**Given**: Array of integers, answer multiple queries for sum between `l` and `r`.
**Solution**: Build prefix sum once (O(n)), answer each query in O(1).
**Time**: O(n + q)
**Space**: O(n)

---

#### **Problem 2: Number of Subarrays with Given Sum**

**Example**: Find number of subarrays whose sum = k.
**Approach**: Store prefix sums in a hash map (count occurrences), for each `prefix[i]`, check if `(prefix[i] - k)` exists.
**Time**: O(n)
**Space**: O(n)
**Note**: This is an extension where we use prefix sum + hashing.

---

#### **Problem 3: Equilibrium Index**

**Definition**: Index where sum of elements on the left = sum on the right.
**Solution**: Use total sum and prefix sum:

```
If prefix[i-1] == totalSum - prefix[i], then i is equilibrium.
```

**Time**: O(n)
**Space**: O(n) or O(1) (if using running sum).

---

#### **Problem 4: Continuous Subarray Sum Divisible by k**

**Given**: Check if there exists a subarray whose sum is multiple of k.
**Approach**: Store prefix sum mod k in hash set; if mod repeats, subarray sum is divisible by k.
**Time**: O(n)
**Space**: O(min(n, k))

---

#### **Problem 5: 2D Prefix Sum (Matrix Sum Queries)**

**Given**: 2D matrix, find sum of sub-rectangle quickly.
**Solution**: Build 2D prefix sum:

```
prefix[i][j] = matrix[i][j] 
             + prefix[i-1][j] 
             + prefix[i][j-1] 
             - prefix[i-1][j-1]
```

**Time**: O(n\*m) preprocessing, O(1) per query.

---

#### **Problem 6: Trapping Rain Water**

**Prefix Sum Use**: Store prefix max from left and right to compute trapped water at each position in O(1).
**Time**: O(n)
**Space**: O(n) (can optimize to O(1)).

---

#### **Problem 7: Count Subarrays with Even Sum / Odd Sum**

**Approach**: Prefix sum parity pattern — count how many prefix sums are even/odd so far.

---

#### **Problem 8: Maximum Sum Subarray of Fixed Size k**

**Approach**: Build prefix sum, then:

```
maxSum = max(prefix[i] - prefix[i-k])
```

**Time**: O(n)

---

### 4️⃣ Steps to Apply Prefix Sum Pattern

1. **Preprocess**:

   * Build `prefix[i]` so it contains sum of `arr[0..i]`.

2. **Answer Query in O(1)**:

   ```
   sum(l, r) = prefix[r] - prefix[l-1]
   ```

3. **Handle Edge Cases**:

   * If `l = 0`, sum = `prefix[r]`.
   * Negative numbers? Works fine — no change needed.

4. **For Dynamic Updates**:

   * Normal prefix sum isn't good for frequent updates → use Fenwick Tree (Binary Indexed Tree) or Segment Tree.

---

### 5️⃣ Time & Space Complexity

| Operation        | Complexity |
| ---------------- | ---------- |
| Build prefix sum | O(n)       |
| Query sum        | O(1)       |
| Space            | O(n)       |

---

### 6️⃣ C# Example: Range Sum Queries

```csharp
public class PrefixSum
{
    private int[] prefix;

    public PrefixSum(int[] arr)
    {
        prefix = new int[arr.Length];
        prefix[0] = arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            prefix[i] = prefix[i - 1] + arr[i];
        }
    }

    public int RangeSum(int l, int r)
    {
        if (l == 0) return prefix[r];
        return prefix[r] - prefix[l - 1];
    }
}
```
## Prefix Sum Pattern Cheatsheet
Here’s your **Prefix Sum Pattern Cheatsheet** — compact, ready for interviews:

| **Problem**                                         | **Prefix Sum Approach**                                                                     | **Time Complexity**       | **Space Complexity**     |
| --------------------------------------------------- | ------------------------------------------------------------------------------------------- | ------------------------- | ------------------------ |
| **Range Sum Query**                                 | Precompute `prefix[i] = prefix[i-1] + arr[i]`, then `sum(l,r) = prefix[r] - prefix[l-1]`    | O(n) build, O(1) query    | O(n)                     |
| **Number of Subarrays with Given Sum (k)**          | Use prefix sum + dictionary to count occurrences of `(prefix[i] - k)`                       | O(n)                      | O(n)                     |
| **Equilibrium Index**                               | Compute total sum, check if `prefix[i-1] == totalSum - prefix[i]`                           | O(n)                      | O(1)                     |
| **Continuous Subarray Sum Divisible by k**          | Store `prefixSum % k` in set/dictionary; if repeats, subarray sum divisible by k            | O(n)                      | O(min(n,k))              |
| **2D Matrix Sum Queries**                           | Build 2D prefix sum: `P[i][j] = mat[i][j] + P[i-1][j] + P[i][j-1] - P[i-1][j-1]`            | O(n\*m) build, O(1) query | O(n\*m)                  |
| **Trapping Rain Water**                             | Store `leftMax[i]` & `rightMax[i]` using prefix & suffix max arrays, then sum trapped water | O(n)                      | O(n) (O(1) if optimized) |
| **Count Subarrays with Even/Odd Sum**               | Track parity of prefix sums; count how many even/odd seen so far                            | O(n)                      | O(1)                     |
| **Max Sum Subarray of Fixed Size k**                | `max = max(prefix[i] - prefix[i-k])` for i ≥ k                                              | O(n)                      | O(n)                     |
| **Binary Array Count Subarrays with Equal 0 and 1** | Treat 0 as -1, use prefix sum + dictionary to count zero-sum subarrays                      | O(n)                      | O(n)                     |
| **Find Pivot Index (LeetCode)**                     | Similar to equilibrium index; check running sum while iterating                             | O(n)                      | O(1)                     |
