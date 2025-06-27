# ✅ Complete Guide: Knuth-Morris-Pratt (KMP) String Matching Algorithm


## 🔍 What is KMP Algorithm?

KMP is a **pattern matching algorithm** that finds all occurrences of a **pattern string (P)** in a **text string (T)** in **linear time**, using a clever preprocessing step.

Unlike the **naive approach**, which starts over on every mismatch, KMP avoids redundant comparisons using a precomputed array called the **LPS (Longest Prefix Suffix)** array.

## 📌 KMP = Two Main Phases:

1. **Preprocessing:** Build the LPS array from the pattern.
2. **Search:** Use the LPS array to skip unnecessary comparisons in the text.


## 🧠 Core Concept

When a **mismatch** occurs during matching, KMP uses the LPS array to **jump to the next best possible position in the pattern**, instead of restarting from scratch.


## 🧮 Time and Space Complexity

| Operation | Time     | Space |
| --------- | -------- | ----- |
| Build LPS | O(M)     | O(M)  |
| Search    | O(N)     | O(1)  |
| Total     | O(N + M) | O(M)  |

* `N` = length of text
* `M` = length of pattern


## 🔧 LPS Array: Deep Explanation

### ✅ What is LPS?

LPS\[i] = Length of the longest **proper prefix** of `pattern[0..i]` that is also a **suffix**.

* **Prefix**: starts at index 0.
* **Suffix**: ends at index i.
* **Proper**: prefix and suffix should **not be equal** to the full string.


### 🔁 How LPS Helps?

Suppose we matched some characters and a mismatch happens — instead of going back to the start of the pattern, we use LPS to **reuse past knowledge** and **continue from the right position**.


## 🪜 Step-by-Step: Build LPS Array for Pattern `"ABABAC"`

Pattern = `"A B A B A C"`
Indexes = ` 0 1 2 3 4 5`

We initialize:

```csharp
lps[0] = 0
len = 0  // length of previous longest prefix which is also suffix
i = 1
```


### 🧾 LPS Table Build Flow

| i | pattern\[i] | pattern\[len] | Match? | Action                   | len | lps\[i] |
| - | ----------- | ------------- | ------ | ------------------------ | --- | ------- |
| 1 | B           | A             | ❌      | lps\[1] = 0, i++         | 0   | 0       |
| 2 | A           | A             | ✅      | len++, lps\[2] = 1, i++  | 1   | 1       |
| 3 | B           | B             | ✅      | len++, lps\[3] = 2, i++  | 2   | 2       |
| 4 | A           | A             | ✅      | len++, lps\[4] = 3, i++  | 3   | 3       |
| 5 | C           | B             | ❌      | reduce len → lps\[2] = 1 | 1   | ?       |
| 5 | C           | B             | ❌      | reduce len → lps\[0] = 0 | 0   | ?       |
| 5 | C           | A             | ❌      | lps\[5] = 0, i++         | 0   | 0       |

### ✅ Final LPS Array

```
lps = [0, 0, 1, 2, 3, 0]
```


### 🔄 Detailed Doubt Resolution at i = 5 (pattern\[i] = C):

* We had matched 3 characters (len = 3).
* Now `C ≠ B`, so we fallback to `lps[len-1] = lps[2] = 1`.
* Still mismatch? Fallback again: `lps[0] = 0`.
* Still mismatch? Give up → `lps[5] = 0`.

👉 This avoids restarting the match process and efficiently skips rechecking.


## 🎯 KMP Search Algorithm

### 🧪 Goal: Search pattern in text using LPS

Use two indices:

* `i` → text index
* `j` → pattern index

#### Match Flow:

1. If `text[i] == pattern[j]` → advance both.
2. If `j == M` → match found, print `i - j`, and `j = lps[j - 1]`
3. If mismatch:

   * If `j != 0` → set `j = lps[j - 1]`
   * Else → just move `i++`


## 🧑‍💻 Full Working C# Code

```csharp
// KMP Search: Find all occurrences of 'pattern' in 'text'
void KMPSearch(string text, string pattern)
{
    int N = text.Length;    // Length of the text
    int M = pattern.Length; // Length of the pattern
    int[] lps = new int[M]; // LPS array of size equal to pattern length

    // Step 1: Preprocess pattern to build LPS array
    ComputeLPS(pattern, M, lps);

    // Step 2: Start comparing pattern with text
    int i = 0; // Index for text
    int j = 0; // Index for pattern

    while (i < N)
    {
        if (pattern[j] == text[i])
        {
            i++;
            j++;
        }

        // Full match found
        if (j == M)
        {
            Console.WriteLine("Pattern found at index " + (i - j));
            j = lps[j - 1]; // Continue searching for next match
        }
        // Mismatch after some matches
        else if (i < N && pattern[j] != text[i])
        {
            if (j != 0)
                j = lps[j - 1]; // Jump to last known good prefix index
            else
                i++; // No prefix matched, move to next character in text
        }
    }
}

// Preprocess the pattern to build the LPS array
void ComputeLPS(string pattern, int M, int[] lps)
{
    // len stores the length of the longest proper prefix which is also suffix
    int len = 0;

    // The first character has no proper prefix/suffix
    lps[0] = 0;

    // Start from second character (i = 1) — because we need at least two characters to compare
    int i = 1;

    while (i < M)
    {
        if (pattern[i] == pattern[len])
        {
            // Characters match: extend current prefix-suffix
            len++;
            lps[i] = len;
            i++;
        }
        else
        {
            // Mismatch after some matches
            if (len != 0)
            {
                // Try the previous possible shorter prefix
                len = lps[len - 1];
                // Note: no increment of i here — we retry the same i with shorter len
            }
            else
            {
                // No prefix matched — start fresh
                lps[i] = 0;
                i++;
            }
        }
    }
}


```
## 📌 Why `i = 1` in LPS?

We start at `i = 1` because:

* `lps[0]` is always 0 — a single character has no proper prefix and suffix.
* We need **at least two characters** to form a prefix and suffix for comparison.

## 📌 When to Use KMP

✅ Use KMP when:

* You need to search a **pattern multiple times** in a text.
* You want **linear time search**.
* The pattern has **repetitive sub-patterns**.
* You want consistent performance (unlike naive, which can degrade).

🚫 Avoid KMP when:

* One-off small search (use `IndexOf` or naive).
* You need **approximate/fuzzy matching**.
* You care more about simplicity than performance.


## 🆚 KMP vs Naive

| Feature              | Naive    | KMP                 |
| -------------------- | -------- | ------------------- |
| Time (Worst)         | O(N × M) | O(N + M)            |
| Skips Rechecks?      | ❌ No     | ✅ Yes               |
| Extra Space          | None     | LPS array (O(M))    |
| Preprocessing Needed | ❌ No     | ✅ Yes (LPS array)   |
| Easy to Implement    | ✅ Very   | ⚠️ Slightly Complex |


## 🧠 Final Analogy: How LPS Helps

Think of typing a password.

* You typed: `A B A B A` — ✅ so far.
* Then mistake on last character `C` ❌.
* KMP says: "Wait! You already typed `ABA` correctly before. Let’s restart from there, not from zero."

