# Reversal Technique on Arrays

The **Reversal Technique** is a smart and efficient method to **rotate an array** to the left or right **in-place** with `O(n)` time and `O(1)` space.


## Problem Example:

**Rotate an array to the left by `d` positions.**

Input:

```
arr = [1, 2, 3, 4, 5, 6, 7], d = 2
```

Expected Output:

```
[3, 4, 5, 6, 7, 1, 2]
```


## Idea:

To left rotate an array by `d`:

1. Reverse the first `d` elements.
2. Reverse the remaining `n - d` elements.
3. Reverse the whole array.

### Why It Works:

This works based on reversing the order of parts to reposition elements to their final rotated position.


## Implementation in C\#

```csharp
public class ArrayRotation
{
    public static void RotateLeft(int[] arr, int d)
    {
        int n = arr.Length;
        d = d % n; // In case d > n

        Reverse(arr, 0, d - 1);
        Reverse(arr, d, n - 1);
        Reverse(arr, 0, n - 1);
    }

    private static void Reverse(int[] arr, int start, int end)
    {
        while (start < end)
        {
            int temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;
            start++;
            end--;
        }
    }
}
```

### Example Usage:

```csharp
class Program
{
    static void Main()
    {
        int[] arr = {1, 2, 3, 4, 5, 6, 7};
        ArrayRotation.RotateLeft(arr, 2);

        Console.WriteLine(string.Join(", ", arr));  // Output: 3, 4, 5, 6, 7, 1, 2
    }
}
```

## For Right Rotation by `d`

Steps change slightly:

1. Reverse the whole array.
2. Reverse first `n - d` elements.
3. Reverse last `d` elements.

---

## 🧮 Time and Space Complexity

* **Time:** O(n)
* **Space:** O(1) (in-place)


## Use Cases:

* Rotating buffers
* Circular queues
* Memory-efficient array manipulations

Would you like to see this adapted for **strings** or **2D arrays** as well?
