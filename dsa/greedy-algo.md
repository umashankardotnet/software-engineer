# [Greedy Algorithm](https://www.freecodecamp.org/news/greedy-algorithms/)

A Greedy algorithm is a problem-solving approach that makes the locally optimal choice at each step, hoping to find a global optimum. It makes the best possible decision at the current moment without considering the future consequences.

## Key characteristics of Greedy algorithms:
1. Makes locally optimal choices
2. Never reconsiders its choices
3. Simple and straightforward to implement
4. May not always yield the optimal solution

Here are some examples and use cases of Greedy algorithms in C#:

1. Coin Change Problem (Making change with minimum number of coins):

```csharp
public class CoinChange
{
    public static List<int> GetMinimumCoins(int amount, int[] coins)
    {
        List<int> result = new List<int>();
        Array.Sort(coins);
        Array.Reverse(coins);

        int remainingAmount = amount;
        
        foreach (int coin in coins)
        {
            while (remainingAmount >= coin)
            {
                result.Add(coin);
                remainingAmount -= coin;
            }
        }

        return result;
    }

    public static void Main()
    {
        int[] coins = { 25, 10, 5, 1 }; // Quarter, Dime, Nickel, Penny
        int amount = 67;
        
        List<int> solution = GetMinimumCoins(amount, coins);
        Console.WriteLine($"Coins needed for {amount} cents:");
        foreach (int coin in solution)
        {
            Console.Write($"{coin} ");
        }
    }
}
```

2. Activity Selection Problem (Maximum number of activities that can be performed):

```csharp
public class Activity
{
    public int Start { get; set; }
    public int Finish { get; set; }
}

public class ActivitySelection
{
    public static List<Activity> SelectActivities(List<Activity> activities)
    {
        List<Activity> selected = new List<Activity>();
        
        // Sort activities by finish time
        activities.Sort((a, b) => a.Finish.CompareTo(b.Finish));
        
        // Select first activity
        selected.Add(activities[0]);
        int lastFinishTime = activities[0].Finish;

        // Consider rest of the activities
        for (int i = 1; i < activities.Count; i++)
        {
            if (activities[i].Start >= lastFinishTime)
            {
                selected.Add(activities[i]);
                lastFinishTime = activities[i].Finish;
            }
        }

        return selected;
    }

    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Activity { Start = 1, Finish = 4 },
            new Activity { Start = 3, Finish = 5 },
            new Activity { Start = 0, Finish = 6 },
            new Activity { Start = 5, Finish = 7 },
            new Activity { Start = 8, Finish = 9 },
            new Activity { Start = 5, Finish = 9 }
        };

        List<Activity> selected = SelectActivities(activities);
        Console.WriteLine("Selected Activities:");
        foreach (var activity in selected)
        {
            Console.WriteLine($"Start: {activity.Start}, Finish: {activity.Finish}");
        }
    }
}
```

3. Fractional Knapsack Problem:

```csharp
public class Item
{
    public double Weight { get; set; }
    public double Value { get; set; }
    public double Ratio { get; set; }
}

public class FractionalKnapsack
{
    public static double GetMaxValue(List<Item> items, double capacity)
    {
        // Calculate value/weight ratio for each item
        foreach (var item in items)
        {
            item.Ratio = item.Value / item.Weight;
        }

        // Sort items by ratio in descending order
        items.Sort((a, b) => b.Ratio.CompareTo(a.Ratio));

        double totalValue = 0;
        double currentWeight = 0;

        foreach (var item in items)
        {
            if (currentWeight + item.Weight <= capacity)
            {
                // Take whole item
                currentWeight += item.Weight;
                totalValue += item.Value;
            }
            else
            {
                // Take fraction of the item
                double remainingCapacity = capacity - currentWeight;
                totalValue += item.Ratio * remainingCapacity;
                break;
            }
        }

        return totalValue;
    }

    public static void Main()
    {
        List<Item> items = new List<Item>
        {
            new Item { Weight = 10, Value = 60 },
            new Item { Weight = 20, Value = 100 },
            new Item { Weight = 30, Value = 120 }
        };

        double capacity = 50;
        double maxValue = GetMaxValue(items, capacity);
        Console.WriteLine($"Maximum value: {maxValue}");
    }
}
```

### Common problems that can be solved using Greedy algorithms:

1. Minimum Spanning Tree (Kruskal's and Prim's algorithms)
2. Dijkstra's shortest path algorithm
3. Huffman coding for data compression
4. Job scheduling problems
5. Cache replacement policies
6. Task assignment problems
7. Resource allocation problems
8. Network routing protocols

### Advantages of Greedy algorithms:
1. Simple to implement
2. Usually run faster than other algorithms
3. Work well for optimization problems

### Limitations:
1. May not always produce the optimal solution
2. Difficult to prove correctness
3. May need to be combined with other techniques for complex problems

### When to use Greedy algorithms:
1. When the problem has optimal substructure
2. When a locally optimal choice leads to a globally optimal solution
3. When you need a reasonably good solution quickly
4. When the problem can be broken down into smaller subproblems

It's important to note that while Greedy algorithms are powerful and efficient for many problems, they don't always guarantee the optimal solution. In such cases, other approaches like Dynamic Programming or Backtracking might be more appropriate.

### Note 
Yes, absolutely! The Coin Change problem can be solved using both Greedy and Dynamic Programming approaches. While the Greedy approach is simpler and works for some coin systems (like US coins), it doesn't always give the optimal solution for all coin denominations. The Dynamic Programming approach, although more complex, guarantees the optimal solution for any set of coin denominations.

Here's an implementation of the Coin Change problem using Dynamic Programming:

```csharp
public class CoinChangeDP
{
    public static int[] GetMinimumCoins(int amount, int[] coins)
    {
        // dp[i] represents the minimum number of coins needed for amount i
        int[] dp = new int[amount + 1];
        // track[i] stores the coin used for amount i
        int[] track = new int[amount + 1];
        
        // Initialize dp array with maximum possible value
        Array.Fill(dp, amount + 1);
        dp[0] = 0;

        for (int i = 1; i <= amount; i++)
        {
            foreach (int coin in coins)
            {
                if (coin <= i)
                {
                    if (dp[i - coin] + 1 < dp[i])
                    {
                        dp[i] = dp[i - coin] + 1;
                        track[i] = coin;
                    }
                }
            }
        }

        // If no solution exists
        if (dp[amount] > amount)
        {
            return Array.Empty<int>();
        }

        // Reconstruct the solution
        List<int> result = new List<int>();
        int remaining = amount;
        while (remaining > 0)
        {
            result.Add(track[remaining]);
            remaining -= track[remaining];
        }

        return result.ToArray();
    }

    public static void Main()
    {
        // Example where Greedy approach fails but DP works
        int[] coins = { 9, 6, 5, 1 };
        int amount = 11;

        Console.WriteLine("Dynamic Programming Solution:");
        int[] dpSolution = GetMinimumCoins(amount, coins);
        Console.WriteLine($"Minimum coins needed for {amount}:");
        Console.WriteLine($"Number of coins: {dpSolution.Length}");
        Console.WriteLine($"Coins used: {string.Join(", ", dpSolution)}");

        // Compare with Greedy approach
        Console.WriteLine("\nGreedy Solution:");
        List<int> greedySolution = GetMinimumCoinsGreedy(amount, coins);
        Console.WriteLine($"Number of coins: {greedySolution.Count}");
        Console.WriteLine($"Coins used: {string.Join(", ", greedySolution)}");
    }

    // Greedy approach for comparison
    public static List<int> GetMinimumCoinsGreedy(int amount, int[] coins)
    {
        List<int> result = new List<int>();
        Array.Sort(coins);
        Array.Reverse(coins);

        int remainingAmount = amount;
        foreach (int coin in coins)
        {
            while (remainingAmount >= coin)
            {
                result.Add(coin);
                remainingAmount -= coin;
            }
        }

        return result;
    }
}
```

Let's see why DP is better with an example:

```csharp
// Example output:
// Amount: 11
// Coins available: [9, 6, 5, 1]

// Dynamic Programming Solution:
// Minimum coins needed for 11:
// Number of coins: 2
// Coins used: 5, 6

// Greedy Solution:
// Number of coins: 3
// Coins used: 9, 1, 1
```

### Key differences between DP and Greedy approaches for the Coin Change problem:

1. **Optimality**:
   - DP always finds the optimal solution
   - Greedy might not find the optimal solution for all coin systems

2. **Time Complexity**:
   - DP: O(amount * number of coins)
   - Greedy: O(amount) - faster but not always correct

3. **Space Complexity**:
   - DP: O(amount) - needs additional space for dp array
   - Greedy: O(1) - only needs space for output

4. **When to use which**:
   - Use DP when:
     * You need the guaranteed optimal solution
     * The coin denominations are arbitrary
     * You can afford the extra time and space complexity
   
   - Use Greedy when:
     * The coin system has a special property (like US coins)
     * Speed is more important than getting the absolute minimum
     * Memory is very limited

Here's another example where Greedy fails but DP works:

```csharp
public static void AnotherExample()
{
    int[] coins = { 1, 3, 4 };
    int amount = 6;

    Console.WriteLine("\nAnother Example:");
    Console.WriteLine($"Amount: {amount}");
    Console.WriteLine($"Coins available: [{string.Join(", ", coins)}]");

    int[] dpSolution = GetMinimumCoins(amount, coins);
    List<int> greedySolution = GetMinimumCoinsGreedy(amount, coins);

    Console.WriteLine("\nDP Solution:");
    Console.WriteLine($"Number of coins: {dpSolution.Length}");
    Console.WriteLine($"Coins used: {string.Join(", ", dpSolution)}");

    Console.WriteLine("\nGreedy Solution:");
    Console.WriteLine($"Number of coins: {greedySolution.Count}");
    Console.WriteLine($"Coins used: {string.Join(", ", greedySolution)}");
}

// Output:
// Amount: 6
// Coins available: [1, 3, 4]

// DP Solution:
// Number of coins: 2
// Coins used: 3, 3

// Greedy Solution:
// Number of coins: 3
// Coins used: 4, 1, 1
```

In this case, the greedy approach takes the largest coin (4) first, leading to a solution with 3 coins (4+1+1), while the optimal solution found by DP uses just 2 coins (3+3).