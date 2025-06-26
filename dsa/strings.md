Here is the **complete, combined, and structured guide on Strings in DSA**, specially curated for interviews at **big tech companies** like Amazon, Google, Microsoft, Meta, and others. It includes all fundamental concepts, advanced approaches, techniques, and interview-focused problems with detailed explanations.

---

# Data Structures and Algorithms: Strings (Comprehensive Guide for Big Tech Interviews)

---

## What is a String?

A **string** is a sequence of characters. Strings are immutable in most languages like Java, Python, and C#, meaning their contents cannot be changed once created. They are commonly used for text processing, pattern matching, and data manipulation.

In languages like C#, strings are reference types and stored on the heap. Internally, a string is a sequence of Unicode characters, often backed by a `char[]` array.

---

## String Representation in Memory

* In C#, a string is stored as a `char[]`.
* Strings are immutable, so every modification results in a new string object.
* Frequent modifications should use `StringBuilder`.
* Strings are often interned (shared references for identical literals).

---

## Common String Operations

| Operation                    | Description                             | Time Complexity |
| ---------------------------- | --------------------------------------- | --------------- |
| **Access `s[i]`**            | Access character at index               | O(1)            |
| **Length**                   | `s.Length`                              | O(1)            |
| **Concatenation**            | `s1 + s2`                               | O(n + m)        |
| **Substring**                | `s.Substring(start, len)`               | O(k)            |
| **Search (indexOf)**         | Find a character or substring           | O(n)            |
| **Replace**                  | Replace one char/substring              | O(n)            |
| **Split / Join**             | Split into array or join array          | O(n)            |
| **Reverse**                  | Reverse characters                      | O(n)            |
| **Trim / ToLower / ToUpper** | Change casing / remove spaces           | O(n)            |
| **Comparison**               | Check equality or lexicographical order | O(n)            |

---

## Common Approaches and Techniques on Strings

### 1. Two Pointer Technique

Used to compare characters from both ends or traverse two strings simultaneously.

Example: Check if a string is a palindrome, reverse vowels.

### 2. Sliding Window

Maintain a window of characters and move it over the string to track substrings of interest.

Example: Longest substring without repeating characters, minimum window substring.

### 3. Hashing / Frequency Counting

Count character frequencies using a map or array to compare patterns.

Example: Anagram check, group anagrams, first non-repeating character.

### 4. Trie (Prefix Tree)

Used for prefix-based problems like auto-complete, word dictionary, prefix matching.

Efficient for:

* Inserting and searching words: O(L), where L is the word length.
* Space-optimized with shared prefixes.

### 5. Dynamic Programming (DP)

Used when solving problems involving subsequences or transformations (e.g., palindromes, edit distance).

Example: Longest palindromic subsequence, minimum insertions to make palindrome.

### 6. Rolling Hash (Rabin-Karp)

Used to compute hash values of substrings for pattern matching.

Used in:

* Duplicate substring detection
* Pattern search

### 7. Z-Algorithm

Computes longest substring starting at each index that matches the prefix.

Useful for:

* Pattern matching
* Finding repeated substrings

### 8. Suffix Arrays and LCP Arrays

Used for lexicographic comparisons and longest repeated substrings.

More advanced but powerful for problems requiring sorted suffixes.

### 9. Manacher’s Algorithm

Finds the longest palindromic substring in linear time O(n).

More efficient than the naive O(n²) center expansion method.

### 10. Bitmasking

Used to represent the presence/absence of characters (e.g., unique characters).

Efficient when dealing with lowercase letters (a–z) using a 32-bit integer mask.

---

## Common and High-Frequency Interview Problems

### 1. Reverse a String

```csharp
char[] chars = s.ToCharArray();
Array.Reverse(chars);
return new string(chars);
```

Use two-pointer swapping for character arrays.

---

### 2. Check for Palindrome

```csharp
int i = 0, j = s.Length - 1;
while (i < j)
{
    if (s[i++] != s[j--])
        return false;
}
return true;
```

---

### 3. Longest Substring Without Repeating Characters (Sliding Window)

```csharp
int left = 0, right = 0, maxLen = 0;
var set = new HashSet<char>();

while (right < s.Length)
{
    if (!set.Contains(s[right]))
    {
        set.Add(s[right++]);
        maxLen = Math.Max(maxLen, set.Count);
    }
    else
    {
        set.Remove(s[left++]);
    }
}
return maxLen;
```

---

### 4. Check if Two Strings Are Anagrams

```csharp
int[] freq = new int[26];
foreach (char c in s1) freq[c - 'a']++;
foreach (char c in s2) freq[c - 'a']--;
return freq.All(f => f == 0);
```

---

### 5. Group Anagrams

```csharp
var dict = new Dictionary<string, List<string>>();
foreach (var str in strs)
{
    var chars = str.ToCharArray();
    Array.Sort(chars);
    string key = new string(chars);

    if (!dict.ContainsKey(key))
        dict[key] = new List<string>();
    dict[key].Add(str);
}
return dict.Values.ToList();
```

---

### 6. Longest Palindromic Substring (Expand Around Center)

```csharp
int start = 0, end = 0;

for (int i = 0; i < s.Length; i++)
{
    int len1 = Expand(s, i, i);
    int len2 = Expand(s, i, i + 1);
    int len = Math.Max(len1, len2);
    if (len > end - start)
    {
        start = i - (len - 1) / 2;
        end = i + len / 2;
    }
}
return s.Substring(start, end - start + 1);

int Expand(string s, int l, int r)
{
    while (l >= 0 && r < s.Length && s[l] == s[r])
    {
        l--; r++;
    }
    return r - l - 1;
}
```

---

### 7. Minimum Window Substring (Sliding Window + HashMap)

Find the smallest window in `s` that contains all characters from `t`.

---

### 8. Edit Distance (Levenshtein Distance)

Use dynamic programming to compute minimum operations (insert, delete, replace) to convert one string into another.

Time complexity: O(m × n)

---

### 9. Decode Ways

Number of ways to decode a digit string (e.g., "12" → "AB" or "L").

Use DP to solve.

---

### 10. Remove Duplicate Letters

Return the smallest lexicographical string that contains all letters once.

Approach: Greedy + Stack + Last Seen Index

---

## C# Specific String Handling Tools

| Tool              | Use Case                                                  |
| ----------------- | --------------------------------------------------------- |
| `StringBuilder`   | Efficient appending/modifying                             |
| `Regex`           | Pattern matching/search                                   |
| `Span<char>`      | High-performance substring views (available in .NET Core) |
| `string.Intern()` | Memory optimization for duplicates                        |

---

## Tips for Solving String Questions in Interviews

1. Ask for input constraints: length of strings, character set (a–z or Unicode).
2. Be mindful of edge cases: empty strings, strings with same characters, upper/lower case.
3. Use the right data structure (e.g., `Dictionary<char, int>`, `HashSet`, `Stack`).
4. Know how to use two-pointer and sliding window efficiently.
5. Consider space complexity, especially in problems involving large input sizes.

---

## Most Common Interview Questions (By Topic)

| Problem                          | Pattern/Approach               |
| -------------------------------- | ------------------------------ |
| Longest Substring Without Repeat | Sliding Window                 |
| Longest Palindromic Substring    | Expand Around Center, Manacher |
| Group Anagrams                   | Sorting, HashMap               |
| Valid Anagram                    | Frequency Map                  |
| Minimum Window Substring         | Sliding Window + HashMap       |
| Reverse Words in a String        | Tokenization + Reverse         |
| Edit Distance                    | DP                             |
| Implement strStr() (KMP)         | KMP Algorithm                  |
| Multiply Strings                 | Manual Digit Multiplication    |
| String to Integer (Atoi)         | State Machine                  |
| Decode Ways                      | DP                             |
| Reorganize String                | Greedy + Heap                  |
| Remove Duplicate Letters         | Stack + Greedy                 |

---

## Learning Roadmap for Strings in DSA

1. Master basic string handling and operations
2. Learn two-pointer and sliding window techniques
3. Understand character frequency counting and hashing
4. Practice Trie for prefix-related problems
5. Learn dynamic programming for transformation and subsequence problems
6. Study advanced pattern matching (KMP, Rabin-Karp, Z-algorithm)
7. Explore suffix arrays and Manacher’s algorithm for advanced topics
