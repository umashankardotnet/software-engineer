# Complete Guide: Substring, Subsequence, and Subarray


## 🔤 1. Substring

### ✅ Definition:

A **substring** is a **contiguous** sequence of characters within a string.

### ✅ Key Properties:

* Must be **continuous**
* Preserves **character order**

### ✅ Count:

* For a string of length `n`, there are O(n^2) possible substrings

### ✅ Techniques:

* Sliding Window
* Expand Around Center
* HashSet/Map for uniqueness
* Dynamic Programming (for substring-related DP problems)

### ✅ Common Interview Problems:

| Problem                                        | Technique                 | Link         |
| ---------------------------------------------- | ------------------------- | ------------ |
| Longest Substring Without Repeating Characters | Sliding Window + HashSet  | Leetcode #3  |
| Longest Palindromic Substring                  | Expand Around Center / DP | Leetcode #5  |
| Minimum Window Substring                       | Sliding Window + HashMap  | Leetcode #76 |

---

## 🔠 2. Subsequence

### ✅ Definition:

A **subsequence** is a sequence derived from another sequence by **removing some or no elements without changing the order**.

### ✅ Key Properties:

* **Not necessarily continuous**
* Preserves **element order**

### ✅ Count:

* 2^n subsequences for string/array of length n

### ✅ Techniques:

* Recursion + Memoization (for matching problems)
* Dynamic Programming (LCS, LIS)
* Two Pointers (Is Subsequence)

### ✅ Common Interview Problems:

| Problem                        | Technique          | Link           |
| ------------------------------ | ------------------ | -------------- |
| Longest Common Subsequence     | DP (2D Table)      | Leetcode #1143 |
| Is Subsequence                 | Two Pointers       | Leetcode #392  |
| Longest Increasing Subsequence | DP / Binary Search | Leetcode #300  |

---

## 🔢 3. Subarray

### ✅ Definition:

A **subarray** is a **contiguous** portion of an array.

### ✅ Key Properties:

* Must be **continuous**
* Applies only to **arrays**, not strings

### ✅ Count:

* O(n^2) subarrays for array of length n

### ✅ Techniques:

* Prefix Sum
* Sliding Window
* Kadane’s Algorithm
* HashMap for prefix sum frequency

### ✅ Common Interview Problems:

| Problem                   | Technique            | Link          |
| ------------------------- | -------------------- | ------------- |
| Maximum Subarray          | Kadane’s Algorithm   | Leetcode #53  |
| Subarray Sum Equals K     | Prefix Sum + HashMap | Leetcode #560 |
| Minimum Size Subarray Sum | Sliding Window       | Leetcode #209 |

---

## 🔁 4. Compare and Summarize

| Feature           | Substring | Subsequence    | Subarray |
| ----------------- | --------- | -------------- | -------- |
| Data Type         | String    | String / Array | Array    |
| Continuous?       | ✅ Yes     | ❌ No           | ✅ Yes    |
| Order Maintained? | ✅ Yes     | ✅ Yes          | ✅ Yes    |
| Count             | O(n^2)    | O(2^n)         | O(n^2)   |

---

## 📌 Practice Strategy

### ✅ Focus Topics:

1. Substring → Sliding Window, Hashing, Expand Around Center
2. Subsequence → DP and Two Pointers
3. Subarray → Prefix Sum, Kadane’s Algo, Sliding Window

### ✅ Targeted Practice:

* 2 questions per topic per technique
* Vary between easy, medium, and hard
