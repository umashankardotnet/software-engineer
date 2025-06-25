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


