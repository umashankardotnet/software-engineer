# Dynamic Programming - Detailed Explanation with 15 Use Cases

## What is Dynamic Programming (DP)?

Dynamic Programming (DP) is a method used in computer science to solve problems by breaking them down into smaller subproblems, solving each subproblem once, and storing their solutions. It is ideal when problems exhibit:


### 1. **Overlapping Subproblems**

You solve the **same subproblem** multiple times.

**What it means:**
Instead of solving different pieces of the problem, you keep solving the same thing over and over during recursion.

**DP helps by**:
*Storing already computed results (memoization or table)* so you **don’t repeat the same work**.


### 2. **Optimal Substructure**

The solution to a problem can be built using solutions of its **subproblems**.

**What it means:**
You can **combine smaller solutions** to form the final answer, and the *best* answer to the overall problem comes from *best answers of subparts*.

**DP helps by**:
*Ensuring that each step contributes optimally* to the final solution, rather than brute-forcing every combination.


## Compare with Other Techniques

| Technique               | Description                                           | When to Use                                             |
| ----------------------- | ----------------------------------------------------- | ------------------------------------------------------- |
| **Brute Force**         | Try all possible solutions.                           | Small input sizes. Inefficient for larger input.        |
| **Divide and Conquer**  | Divide problem into subproblems, solve independently. | Subproblems are **independent**.                        |
| **Greedy Algorithms**   | Make locally optimal choices.                         | When local optimum leads to global optimum.             |
| **Dynamic Programming** | Break into overlapping subproblems, reuse solutions.  | Problems with **overlap** and **optimal substructure**. |


## Two Key Ways to Implement DP

1. **Top-Down (Memoization)** – Recursion + caching results
   Example: `f(n) = f(n-1) + f(n-2)` (cache the result of f(n))
2. **Bottom-Up (Tabulation)** – Iterative + build solution from base
   Example: Start from base cases and move up


## Simple Example: Fibonacci Numbers

### Brute Force (Exponential Time)

```csharp
int Fib(int n) {
    if (n <= 1) return n;
    return Fib(n - 1) + Fib(n - 2);
}
```

> Time Complexity: O(2^n)


### Top-Down DP (Memoization)

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


### Bottom-Up DP (Tabulation)

```csharp
int Fib(int n) {
    if (n <= 1) return n;
    
    int prev2 = 0; // Fib(0)
    int prev1 = 1; // Fib(1)
    int current = 0;

    for (int i = 2; i <= n; i++) {
        current = prev1 + prev2;
        prev2 = prev1;
        prev1 = current;
    }

    return current;
}
```

> Space Optimized: Use two variables instead of array.


## Visualizing Overlapping Subproblems (Example: Fibonacci)

Recursive Tree for Fib(5):

```
        Fib(5)
       /      \
   Fib(4)     Fib(3)
   /   \       /   \
Fib(3) Fib(2) Fib(2) Fib(1)
 /   \          \
...
```

Notice how **Fib(3)** and **Fib(2)** appear multiple times. DP saves their results to avoid recomputation.


## Top-Down (Memoization) vs Bottom-Up (Tabulation)

| Criteria                        | Top-Down (Memoization)                                  | Bottom-Up (Tabulation)                        |
| ------------------------------- | ------------------------------------------------------- | --------------------------------------------- |
| **Approach**                    | Recursive + cache                                       | Iterative + table                             |
| **Ease of Implementation**      | Often more intuitive, especially for recursive problems | May require careful planning for loop order   |
| **Space Usage**                 | Uses recursion stack + memo table                       | Uses only DP table (can be optimized further) |
| **Performance**                 | Slightly slower due to recursion overhead               | Generally faster (no recursion overhead)      |
| **Stack Overflow Risk**         | Yes (for deep recursion)                                | No (safe for large inputs)                    |
| **When All Subproblems Needed** | Inefficient in such cases                               | More efficient                                |
| **Can Optimize Space?**         | Not always easy                                         | Often yes (e.g., with two variables)          |
| **Use Case Examples**           | Tree-based problems, game recursion, DFS-style DP       | Grid problems, Knapsack, Fibonacci, LIS       |


## When to Prefer Each Approach

### Use **Top-Down (Memoization)** When:

* The problem has a **natural recursive structure** (e.g., tree problems).
* You may **not need to compute all subproblems**.
* You're solving problems with **variable or selective recursion**.
* Readability and simplicity are important.
* Examples:

  * Recursive tree DP (e.g., finding diameter of a tree)
  * Game state evaluation (e.g., can player win?)
  * Word break problem

### Use **Bottom-Up (Tabulation)** When:

* All subproblems are guaranteed to be needed to compute the final answer.
* You want **better performance** by avoiding recursion overhead.
* You want to **optimize space**, especially in linear DP cases.
* You want to write an **iterative and memory-efficient solution**.
* Examples:

  * 0/1 Knapsack
  * Longest Common Subsequence (LCS)
  * Coin Change
  * Longest Increasing Subsequence (LIS)


## Heuristic Rule of Thumb

* If your recursive function revisits the **same states**, and recursion is **natural**, start with **Top-Down + Memoization**.
* If you're computing all values in **a range or table**, and recursion is not necessary, prefer **Bottom-Up Tabulation**.


## How to Approach a DP Problem (Step-by-Step)

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

## Popular use cases:

### 1. **Longest Common Subsequence (LCS)**

**Problem:** Find the longest sequence common to both strings (order matters, not continuity).

#### Why DP?

* *Overlapping Subproblems*:
  You keep comparing the same substrings multiple times. Example: comparing `str1[0…i]` and `str2[0…j]` happens repeatedly.

* *Optimal Substructure*:
  LCS of two strings depends on the LCS of smaller prefixes:

  * If last characters match: `LCS(i, j) = 1 + LCS(i-1, j-1)`
  * If not: `LCS(i, j) = max(LCS(i-1, j), LCS(i, j-1))`

---

### 2. **0/1 Knapsack**

**Problem:** Choose items to maximize value without exceeding weight.

#### Why DP?

* *Overlapping Subproblems*:
  The same decisions (include/exclude an item) are repeated for different combinations of weights.

* *Optimal Substructure*:
  The max value at weight `W` depends on:

  * Whether you include the item: `value[i] + dp[i-1][W - weight[i]]`
  * Or exclude it: `dp[i-1][W]`

---

### 3. **Coin Change**

**Problem:** Minimum number of coins to make an amount.

#### Why DP?

* *Overlapping Subproblems*:
  To make amount `7`, you’ll need results of `6`, `5`, `4`… which you already computed for other amounts.

* *Optimal Substructure*:
  `minCoins(7) = 1 + min(minCoins(6), minCoins(4), ...)` for each coin type

---

### 4. **Unique Paths in a Grid**

**Problem:** Count how many ways you can reach from top-left to bottom-right in a grid.

#### Why DP?

* *Overlapping Subproblems*:
  To reach cell `(i, j)`, you must have come from `(i-1, j)` or `(i, j-1)`. Those cells are reused often.

* *Optimal Substructure*:
  Total paths to a cell = paths from top + paths from left.

---

### 5. **Longest Increasing Subsequence (LIS)**

**Problem:** Find the length of the longest subsequence where each number is larger than the previous one.

#### Why DP?

* *Overlapping Subproblems*:
  You compute LIS ending at each index multiple times while trying different combinations.

* *Optimal Substructure*:
  `LIS[i] = max(1 + LIS[j])` for all `j < i` and `arr[j] < arr[i]`.

---

### 6. **Game Strategy Problems (e.g., Predict Winner)**

**Problem:** Decide if a player can win a game given a set of moves and current score.

#### Why DP?

* *Overlapping Subproblems*:
  Same game states (remaining moves, current scores) appear in different game paths.

* *Optimal Substructure*:
  Current player’s winning possibility depends on best choices made in subgames.


## Summary

| Problem Type                  | Why DP Works Well                                                     |
| ----------------------------- | --------------------------------------------------------------------- |
| **Strings (LCS, Palindrome)** | Same substring pairs re-evaluated, result builds from smaller strings |
| **Optimization (Knapsack)**   | Many ways to choose, but max profit comes from best subchoices        |
| **Counting (Paths, Coins)**   | Number of ways reuses counts of earlier states                        |
| **Sequence (LIS)**            | Subsequence decisions rely on earlier results                         |
| **Game Strategy**             | Recurring game states + decision tree = perfect for memoization       |

## Final Thoughts

Dynamic Programming helps you:

* Avoid recalculating
* Improve time efficiency
* Handle complex recursive dependencies
