# Complete Guide on Reversal Techniques (with Use Cases, Diagrams & Complexity)

Reversal is one of the most common techniques in solving array, string, number, and linked list problems. It forms the basis of many interview questions.


## Why Reversal Techniques?

* Reverse **arrays** or **strings**
* Reverse **words in a sentence**
* Reverse **numbers** (integer reversal)
* Reverse **linked lists**
* Rotate arrays using reversal


## Core Patterns

* **Two-Pointer Technique**
* **In-place swapping**
* **Mathematical approach** (for integers)
* **Recursive reversal**
* **Stack/Queue based reversal**

## Complexity Table

| Problem                   | Time Complexity | Space Complexity |
| ------------------------- | --------------- | ---------------- |
| Reverse Array             | O(n)            | O(1)             |
| Reverse String            | O(n)            | O(1)             |
| Reverse Words in Sentence | O(n)            | O(n) (for split) |
| Reverse Integer           | O(log n)        | O(1)             |
| Reverse Linked List       | O(n)            | O(1)             |
| Rotate Array              | O(n)            | O(1)             |
| Reverse using Stack       | O(n)            | O(n)             |
| Reverse using Queue       | O(n)            | O(n)             |

# Section 1: Reverse an Array

### **C# Example:**

```csharp
void ReverseArray(int[] arr)
{
    int left = 0, right = arr.Length - 1;
    while (left < right)
    {
        int temp = arr[left];
        arr[left] = arr[right];
        arr[right] = temp;
        left++;
        right--;
    }
}
```

**Diagram:**

```
Input:  [1,2,3,4,5]
Step 1: Swap arr[0] and arr[4] → [5,2,3,4,1]
Step 2: Swap arr[1] and arr[3] → [5,4,3,2,1]
```

Use Cases: Reverse subarrays, prepare for rotations.


# Section 2: Reverse a String

```csharp
string ReverseString(string s)
{
    char[] chars = s.ToCharArray();
    int left = 0, right = chars.Length - 1;
    while (left < right)
    {
        char temp = chars[left];
        chars[left] = chars[right];
        chars[right] = temp;
        left++;
        right--;
    }
    return new string(chars);
}
```

# Section 3: Reverse Words in a Sentence

Input: `"the sky is blue"` → Output: `"blue is sky the"`

```csharp
string ReverseWords(string s)
{
    string[] words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    Array.Reverse(words);
    return string.Join(" ", words);
}
```

Advanced: Reverse words in place.


# Section 4: Reverse an Integer

### String Approach:

```csharp
int ReverseInteger_String(int x)
{
    bool isNegative = x < 0;
    char[] chars = Math.Abs(x).ToString().ToCharArray();
    int left = 0, right = chars.Length - 1;
    while (left < right)
    {
        char temp = chars[left];
        chars[left] = chars[right];
        chars[right] = temp;
        left++;
        right--;
    }
    if (int.TryParse(new string(chars), out int result))
        return isNegative ? -result : result;
    return 0;
}
```

### Math Approach (Preferred):

```csharp
int ReverseInteger_Math(int x)
{
    int rev = 0;
    while (x != 0)
    {
        int digit = x % 10;
        x /= 10;
        if (rev > int.MaxValue / 10 || rev < int.MinValue / 10) return 0;
        rev = rev * 10 + digit;
    }
    return rev;
}
```

# Section 5: Reverse a Linked List

### **Iterative C# Example:**

```csharp
ListNode ReverseList(ListNode head)
{
    ListNode prev = null;
    ListNode current = head;
    while (current != null)
    {
        ListNode next = current.Next;
        current.Next = prev;
        prev = current;
        current = next;
    }
    return prev;
}
```


# Section 6: Rotate Array (Left & Right) Using Reversal

### Left Rotation by k Steps:

Steps:

1. Reverse first k elements
2. Reverse remaining elements
3. Reverse entire array

```csharp
void LeftRotate(int[] arr, int k)
{
    k %= arr.Length;
    Reverse(arr, 0, k - 1);
    Reverse(arr, k, arr.Length - 1);
    Reverse(arr, 0, arr.Length - 1);
}
```

### Right Rotation by k Steps:

```csharp
void RightRotate(int[] arr, int k)
{
    k %= arr.Length;
    Reverse(arr, 0, arr.Length - 1);
    Reverse(arr, 0, k - 1);
    Reverse(arr, k, arr.Length - 1);
}
```

### Helper Method:

```csharp
void Reverse(int[] arr, int left, int right)
{
    while (left < right)
    {
        int temp = arr[left];
        arr[left] = arr[right];
        arr[right] = temp;
        left++;
        right--;
    }
}
```

**Diagram for Right Rotate by 3:**

```
Input: [1,2,3,4,5,6,7], k=3
Step 1: Reverse entire → [7,6,5,4,3,2,1]
Step 2: Reverse first 3 → [5,6,7,4,3,2,1]
Step 3: Reverse last 4 → [5,6,7,1,2,3,4]
```

# Section 7: Reverse Using Stack

### Idea:

Push all elements onto a stack, then pop them back to reverse order.

```csharp
void ReverseUsingStack<T>(T[] arr)
{
    Stack<T> stack = new Stack<T>(arr);
    int i = 0;
    while (stack.Count > 0)
    {
        arr[i++] = stack.Pop();
    }
}
```

Works for arrays, strings (char array), or linked lists (with push/pop).


# Section 8: Reverse Using Queue

Use **Queue with Deque logic** (insert at front).

```csharp
void ReverseUsingQueue<T>(T[] arr)
{
    Queue<T> queue = new Queue<T>(arr);
    T[] reversed = new T[arr.Length];
    for (int i = arr.Length - 1; i >= 0; i--)
    {
        reversed[i] = queue.Dequeue();
    }
    Array.Copy(reversed, arr, arr.Length);
}
```

More commonly, **Deque** is better for such problems.


## Common Interview Problems Using Reversal:

* Reverse Linked List (LeetCode 206)
* Reverse Integer (LeetCode 7)
* Rotate Array (LeetCode 189)
* Reverse Words in String II (LeetCode 186)
* Reverse Nodes in k-Group (LeetCode 25)
