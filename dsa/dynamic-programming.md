Dynamic Programming (DP) is a powerful technique used in **algorithm design** for solving problems that can be broken down into **overlapping subproblems** with **optimal substructure**.

---

## 🧠 What is Dynamic Programming (DP)?

Dynamic Programming is an approach to solving **complex problems by breaking them down into simpler subproblems**, solving each subproblem just once, and **storing their results** (usually in an array or table) to avoid redundant work.

> It’s like remembering past answers to avoid repeating the same work.

---

## 🧩 When Should You Use DP?

Use **Dynamic Programming** when:

1. **Overlapping Subproblems**: The same subproblems are solved multiple times.
2. **Optimal Substructure**: The solution to the main problem depends on the optimal solution of its subproblems.

---

## 🔍 Compare with Other Techniques

| Technique               | Description                                           | When to Use                                             |
| ----------------------- | ----------------------------------------------------- | ------------------------------------------------------- |
| **Brute Force**         | Try all possible solutions.                           | Small input sizes. Inefficient for larger input.        |
| **Divide and Conquer**  | Divide problem into subproblems, solve independently. | Subproblems are **independent**.                        |
| **Greedy Algorithms**   | Make locally optimal choices.                         | When local optimum leads to global optimum.             |
| **Dynamic Programming** | Break into overlapping subproblems, reuse solutions.  | Problems with **overlap** and **optimal substructure**. |

---

## 🔄 Two Key Ways to Implement DP

1. **Top-Down (Memoization)** – Recursion + caching results
   Example: `f(n) = f(n-1) + f(n-2)` (cache the result of f(n))
2. **Bottom-Up (Tabulation)** – Iterative + build solution from base
   Example: Start from base cases and move up

---

## 🧮 Simple Example: Fibonacci Numbers

### ❌ Brute Force (Exponential Time)

```csharp
int Fib(int n) {
    if (n <= 1) return n;
    return Fib(n - 1) + Fib(n - 2);
}
```

> Time Complexity: O(2^n)

---

### ✅ Top-Down DP (Memoization)

```csharp
Dictionary<int, int> memo = new();

int Fib(int n) {
    if (n <= 1) return n;
    if (memo.ContainsKey(n)) return memo[n];
    memo[n] = Fib(n - 1) + Fib(n - 2);
    return memo[n];
}
```

> Time Complexity: O(n)

---

### ✅ Bottom-Up DP (Tabulation)

```csharp
int Fib(int n) {
    if (n <= 1) return n;
    int[] dp = new int[n + 1];
    dp[0] = 0;
    dp[1] = 1;
    for (int i = 2; i <= n; i++) {
        dp[i] = dp[i - 1] + dp[i - 2];
    }
    return dp[n];
}
```

> Space Optimized: Use two variables instead of array.

---

## 🧰 Real-World Use Cases

### 1. **Knapsack Problem (0/1 Knapsack)**

* **Problem**: Given weights and values, find the max value that fits into the knapsack.
* **Use DP** because choices depend on smaller subproblems.

### 2. **Edit Distance**

* Find the minimum number of operations to convert one string to another.
* Used in spell checkers, DNA sequence analysis, etc.

### 3. **Matrix Path (Grid Problems)**

* Count the number of ways to reach a cell in a grid.
* Every cell’s result depends on the result of the previous ones.

### 4. **Palindrome Partitioning**

* Partition a string such that each part is a palindrome.
* Solve smaller partitions and build up.

---

## 🔧 How to Approach a DP Problem (Step-by-Step)

1. **Identify Subproblems**
   Can the problem be broken into subproblems?

2. **Define State**
   What parameters define a unique subproblem?

3. **State Transition (Recurrence)**
   How do you build the solution of a state from others?

4. **Base Cases**
   What are the simplest possible subproblems?

5. **Choose Memoization or Tabulation**

6. **Implement and Optimize**
   Use space/time optimizations if needed.

---

## 💡 Example: Climbing Stairs

> Each time you can climb 1 or 2 steps. How many distinct ways to reach the top (n steps)?

### DP Formula:

```
ways(n) = ways(n-1) + ways(n-2)
```

### C# Code:

```csharp
int ClimbStairs(int n) {
    if (n <= 2) return n;
    int first = 1, second = 2, result = 0;
    for (int i = 3; i <= n; i++) {
        result = first + second;
        first = second;
        second = result;
    }
    return result;
}
```

---

## 🧠 Final Thoughts

Dynamic Programming helps you:

* Avoid recalculating
* Improve time efficiency
* Handle complex recursive dependencies

---

Would you like to try a few DP problems with explanations? I can walk you through **Knapsack**, **Longest Increasing Subsequence**, or **Palindrome Partitioning**.
