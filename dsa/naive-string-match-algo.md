# Naive String Matching Algorithm

**Naive String Matching Algorithm** is the most basic and straightforward way to find a substring (called the **pattern**) within a larger string (called the **text**). It checks all possible positions in the text where the pattern could occur, one by one.
The **naive approach** tries to match the pattern at every position of the text from left to right.
If a mismatch is found, it shifts the pattern by one position and starts matching again.


## ⚙️ How It Works

Let’s say:

* **Text = "ABCABCDABCD"**
* **Pattern = "ABCD"**

The algorithm:

1. Starts matching pattern from the beginning of the text.
2. Compares each character of pattern with the corresponding character in the text.
3. If all characters match → pattern found.
4. If mismatch → move pattern one position to the right and repeat.


## 🔁 Step-by-Step Example

Text = `"ABCABCDABCD"`
Pattern = `"ABCD"`

```
Step 1: Compare text[0..3] = "ABCA" with pattern "ABCD" → ❌ mismatch
Step 2: Compare text[1..4] = "BCAB" with pattern "ABCD" → ❌ mismatch
Step 3: Compare text[2..5] = "CABC" with pattern "ABCD" → ❌ mismatch
Step 4: Compare text[3..6] = "ABCD" with pattern "ABCD" → ✅ match found at index 3
Step 5: Continue comparing text[4..7] = "BCDA" → ❌
Step 6: Compare text[5..8] = "CDAB" → ❌
Step 7: Compare text[6..9] = "DABC" → ❌
Step 8: Compare text[7..10] = "ABCD" → ✅ match found at index 7
```

So, matches are found at **index 3 and 7**.


## 🧮 Time and Space Complexity

| Scenario     | Time Complexity                                 |
| ------------ | ----------------------------------------------- |
| Best Case    | O(N) (when first char doesn't match repeatedly) |
| Average Case | O(N×M)                                          |
| Worst Case   | O(N×M) (e.g., repetitive characters)            |

* **N = length of text**
* **M = length of pattern**

> **Space Complexity** = O(1) (no extra space is used)


## ✅ Advantages (Pros)

| Feature                | Benefit                                |
| ---------------------- | -------------------------------------- |
| ✅ Simple               | Easy to understand and implement       |
| 🧠 No preprocessing    | No extra memory for LPS or hash tables |
| 🧰 Works for all cases | No assumptions about input             |


## ❌ Disadvantages (Cons)

| Limitation              | Explanation                                           |
| ----------------------- | ----------------------------------------------------- |
| 🚫 Slow on large inputs | Repeats comparisons unnecessarily                     |
| ⚠️ No optimization      | Doesn’t skip already matched characters               |
| 🔁 Inefficient          | For repeated patterns or long texts, becomes too slow |


## 🧠 When to Use

* For **small inputs** where performance isn’t critical.
* For **educational purposes** to understand the basics of pattern matching.
* When you need a **quick prototype** with minimum code.


## 🚫 When Not to Use

* On **large texts or patterns**, especially with repeated characters (e.g., `"aaaaaaaaaa"`).
* When you require **high performance** or real-time matching.
* When patterns are searched **frequently** — better use KMP or Boyer-Moore.


## 🧪 C# Code Example of Naive Approach

```csharp
void NaiveSearch(string text, string pattern)
{
    int N = text.Length;
    int M = pattern.Length;

    for (int i = 0; i <= N - M; i++)
    {
        int j;
        for (j = 0; j < M; j++)
        {
            if (text[i + j] != pattern[j])
                break;
        }

        if (j == M)
            Console.WriteLine("Pattern found at index " + i);
    }
}
```


## 🔄 Comparison with Other Algorithms

| Algorithm   | Time Complexity          | Suitable For                 |
| ----------- | ------------------------ | ---------------------------- |
| Naive       | O(N×M)                   | Small input, simple use      |
| KMP         | O(N + M)                 | Large input, repeated search |
| Boyer-Moore | Best < O(N)              | Large alphabets, real-time   |
| Rabin-Karp  | Avg O(N+M), worst O(N×M) | Multiple patterns            |

