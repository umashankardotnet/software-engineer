# Recursion
Let’s break down **how recursion works**, with simple language, practical use cases, and step-by-step examples (including C# code).



## What is Recursion?

**Recursion** is when a function **calls itself** to solve a smaller part of a problem until it reaches a **base case**.

### Key Concepts:

* **Base Case**: The simplest case that can be solved directly.
* **Recursive Case**: The part where the function calls itself with a simpler input.



## How Recursion Works Internally

Every recursive call is added to the **call stack**. The function keeps calling itself until it reaches the base case. Then, it **unwinds** the stack, solving each level on the way back.



## Example 1: Factorial (Classic Recursion)

### Problem: Find factorial of a number `n`

`factorial(n) = n * factorial(n-1)`
Base case: `factorial(0) = 1`

### C# Code:

```csharp
int Factorial(int n) {
    if (n == 0) return 1; // Base case
    return n * Factorial(n - 1); // Recursive call
}
```

### Execution for `Factorial(4)`:

```
Factorial(4)
→ 4 * Factorial(3)
    → 3 * Factorial(2)
        → 2 * Factorial(1)
            → 1 * Factorial(0)
                → 1
            ← returns 1
        ← returns 1 * 1 = 1
    ← returns 2 * 1 = 2
← returns 3 * 2 = 6
← returns 4 * 6 = 24
```



## Example 2: Sum of First N Numbers

```csharp
int Sum(int n) {
    if (n == 0) return 0;
    return n + Sum(n - 1);
}
```

Calling `Sum(3)`:

```
Sum(3) = 3 + Sum(2)
        = 3 + (2 + Sum(1))
        = 3 + (2 + (1 + Sum(0)))
        = 3 + 2 + 1 + 0 = 6
```



## Example 3: Recursive Tree Traversal

**Use Case**: Tree traversal is a natural fit for recursion.

```csharp
class Node {
    public int val;
    public Node left, right;
}

void InOrder(Node root) {
    if (root == null) return;
    InOrder(root.left);
    Console.WriteLine(root.val);
    InOrder(root.right);
}
```

### Why Recursion Fits:

* You repeat the same logic for left and right subtrees.
* Stack implicitly keeps track of which node to return to.



## When to Use Recursion

### Best for:

* **Tree traversal** (binary trees, n-ary trees)
* **Divide and conquer** (Merge Sort, Quick Sort)
* **Backtracking** (N-Queens, Sudoku, Subsets)
* **Dynamic Programming** (Top-down memoization)
* **Graph DFS traversal**



## When to Avoid Recursion

* When the depth is too large (risk of stack overflow).
* When performance is critical and iteration is more efficient.
* When the same subproblem is solved multiple times — use **memoization** or **bottom-up DP** instead.



## Tail Recursion (Advanced Note)

Some languages (not C#) can optimize **tail-recursive** functions, where the recursive call is the last action.

Example (not optimized in C#, but shown for clarity):

```csharp
int TailSum(int n, int acc) {
    if (n == 0) return acc;
    return TailSum(n - 1, acc + n); // Tail-recursive
}
```

Call with `TailSum(5, 0)` gives `15`



## Summary

| Term           | Meaning                              |
| -------------- | ------------------------------------ |
| Recursion      | Function calling itself              |
| Base case      | Terminates recursion                 |
| Recursive case | Problem divided into smaller parts   |
| Call Stack     | Tracks recursive calls               |
| Tail Recursion | Recursive call is the last operation |


## Real-World Recursive Problems from Interviews

These are **popular at big tech companies (FAANG and others)** because recursion tests problem-solving and abstraction skills.



### 1. **Binary Tree Traversals**

* **Inorder**, **Preorder**, **Postorder**
* **Use Case**: Process each node in a structured order

```csharp
void InOrder(Node root) {
    if (root == null) return;
    InOrder(root.left);
    Console.WriteLine(root.val);
    InOrder(root.right);
}
```

* **Why recursive?** Tree structures naturally use recursion since each subtree is a smaller version of the tree.



### 2. **Permutations / Subsets (Backtracking)**

**Problem**: Given a set of numbers, return all permutations.

```csharp
void Permute(List<int> nums, List<int> path, bool[] used) {
    if (path.Count == nums.Count) {
        Console.WriteLine(string.Join(",", path));
        return;
    }

    for (int i = 0; i < nums.Count; i++) {
        if (used[i]) continue;
        used[i] = true;
        path.Add(nums[i]);
        Permute(nums, path, used);
        path.RemoveAt(path.Count - 1);
        used[i] = false;
    }
}
```

* **Why recursive?** Every decision creates a branching choice tree. Recursion + backtracking efficiently navigates the tree.



### 3. **N-Queens Problem**

* **Place N queens** on an NxN chessboard such that no two threaten each other.
* Classic recursion + backtracking.



### 4. **Merge Sort**

```csharp
void MergeSort(int[] arr, int left, int right) {
    if (left >= right) return;

    int mid = (left + right) / 2;
    MergeSort(arr, left, mid);
    MergeSort(arr, mid + 1, right);
    Merge(arr, left, mid, right);
}
```

* **Why recursive?** Problem is naturally divided into smaller halves, merged back after sorting.



### 5. **Generate Balanced Parentheses**

**Problem**: For `n` pairs of parentheses, generate all valid combinations.

```csharp
void Generate(int open, int close, string current) {
    if (open == 0 && close == 0) {
        Console.WriteLine(current);
        return;
    }
    if (open > 0)
        Generate(open - 1, close, current + "(");
    if (close > open)
        Generate(open, close - 1, current + ")");
}
```



### 6. **Graph DFS Traversal**

```csharp
void DFS(int node, HashSet<int> visited, Dictionary<int, List<int>> graph) {
    if (visited.Contains(node)) return;
    visited.Add(node);
    foreach (var neighbor in graph[node]) {
        DFS(neighbor, visited, graph);
    }
}
```

* Used for pathfinding, cycle detection, topological sort.



### 7. **Palindrome Partitioning**

* Partition a string into substrings such that each is a palindrome.



### 8. **Word Break Problem**

* Check if a word can be broken into space-separated valid dictionary words.



## Visual Explanation: How Call Stack Works in Recursion

Let’s use the **Factorial** example:

```csharp
int Factorial(int n) {
    if (n == 0) return 1;
    return n * Factorial(n - 1);
}
```

### Input: `Factorial(3)`



### Call Stack Growth:

```
Call Stack:
Factorial(3)
→ calls Factorial(2)
    → calls Factorial(1)
        → calls Factorial(0)
            → returns 1
```

### Call Stack Unwinds:

```
Factorial(0) returns 1
→ Factorial(1) = 1 * 1 = 1
→ Factorial(2) = 2 * 1 = 2
→ Factorial(3) = 3 * 2 = 6
```

So, the final return value is **6**.



## Summary of How Recursion and Call Stack Work

| Step | Action                     | What’s on Call Stack                                        |
| - | -- | -- |
| 1    | `Factorial(3)` starts      | `Factorial(3)`                                              |
| 2    | Calls `Factorial(2)`       | `Factorial(3) → Factorial(2)`                               |
| 3    | Calls `Factorial(1)`       | `Factorial(3) → Factorial(2) → Factorial(1)`                |
| 4    | Calls `Factorial(0)`       | `Factorial(3) → Factorial(2) → Factorial(1) → Factorial(0)` |
| 5    | `Factorial(0)` returns 1   | Now stack unwinds                                           |
| 6    | `Factorial(1)` returns 1×1 |                                                             |
| 7    | `Factorial(2)` returns 2×1 |                                                             |
| 8    | `Factorial(3)` returns 3×2 |                                                             |


Great question — it touches on the **core of how recursion really works**.

Let’s clarify it:

---

## ❓ Does Recursion "Remember" Previous Return Values?

**No, recursion itself doesn't automatically "remember" previous return values.**
Instead, it relies on the **call stack** to hold the state (arguments, local variables, return values) for each recursive call.

So, it’s not that recursion "remembers" — it's that **each recursive call is paused and stored** on the call stack until its recursive sub-call finishes and returns a value.

## How Return Values Are Passed Back

Here’s how it works step by step:

### Example:

```csharp
int Factorial(int n) {
    if (n == 0) return 1;
    return n * Factorial(n - 1);
}
```

If you call `Factorial(3)`, here’s what happens:

1. `Factorial(3)` → needs `Factorial(2)`
2. `Factorial(2)` → needs `Factorial(1)`
3. `Factorial(1)` → needs `Factorial(0)`
4. `Factorial(0)` → returns `1`

Now it unwinds:

* `Factorial(1)` returns `1 * 1 = 1`
* `Factorial(2)` returns `2 * 1 = 2`
* `Factorial(3)` returns `3 * 2 = 6`

**Each return value is passed up** to the previous frame. The call stack ensures the correct value goes to the right function.

## What Does the Call Stack Store?

Each function call stores:

* The **arguments** passed in
* The **local variables**
* The **return address** (where to continue after the call returns)

That’s how C# knows exactly where to continue once a recursive call finishes — it **doesn’t "remember" values**, it just **waits for them** and resumes execution.


## If You Want to "Remember" Return Values

If you want to actually **cache or reuse** return values (like in DP), you must implement that yourself using:

### Memoization Example:

```csharp
Dictionary<int, int> memo = new();

int Fib(int n) {
    if (n <= 1) return n;
    if (memo.ContainsKey(n)) return memo[n];

    memo[n] = Fib(n - 1) + Fib(n - 2);
    return memo[n];
}
```

Now you are explicitly **storing results** so you don’t compute them again.

## Summary

| Concept          | Behavior                               |
| ---------------- | -------------------------------------- |
| Recursion        | Calls itself and waits for return      |
| Call stack       | Remembers state of each call           |
| Return value     | Passed back after base case is reached |
| Memoization (DP) | Stores results for reuse across calls  |
