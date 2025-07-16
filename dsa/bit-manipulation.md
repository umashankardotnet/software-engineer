# Complete Guide to Bit Manipulation in C\#

Bit manipulation is the act of directly operating on the binary bits of integers. This is used to optimize performance or manage space efficiently.

Although bit manipulation works directly on integers, you can also apply it to **characters** and **strings** by treating characters as integers using their **ASCII or Unicode values**. This allows powerful string-processing techniques using integer bitmasks.


## Binary Basics – Essential to Understand Bit Manipulation

### How Numbers Are Stored

Every number is stored as a binary sequence of bits (0 or 1). For example:

* `5` in binary (8-bit) → `00000101`
* `7` in binary → `00000111`

### What is a Bit?

A **bit** is the smallest unit of data (either 0 or 1).

* A **bit is set** when it is `1`
* A **bit is unset (cleared)** when it is `0`

### Indexing Bits

Bits are indexed from **right to left**, starting at position `0`.

Example: `0101` (binary of 5)

* Bit 0 → 1 (set)
* Bit 1 → 0 (unset)
* Bit 2 → 1 (set)
* Bit 3 → 0 (unset)

### How Binary Representation Is Calculated

To convert a decimal number to binary, you repeatedly divide by 2 and write down the remainder:

Example: Convert `13` to binary

```
13 ÷ 2 = 6 remainder 1
6 ÷ 2 = 3 remainder 0
3 ÷ 2 = 1 remainder 1
1 ÷ 2 = 0 remainder 1
```

Read the remainders from bottom to top → `1101`
So, `13` in binary = `1101`

Confirm in C#:

```csharp
int num = 13;
string binary = Convert.ToString(num, 2); // "1101"
```

### How Binary Helps Bit Manipulation

Each bit in the binary representation can represent a flag, state, or index:

* Useful for tracking seen characters
* Can be used to turn features on/off (bitmasking)
* XOR trick cancels repeated numbers


## ASCII and Bit Manipulation

### What is ASCII?

ASCII (American Standard Code for Information Interchange) maps characters to numerical codes (integers).
Each character has a unique 7-bit binary representation.

Examples:

* `'A'` = 65 = `1000001`
* `'a'` = 97 = `1100001`
* `'z'` = 122 = `1111010`

### Using ASCII in Bit Manipulation

Characters can be converted to their ASCII values and manipulated as integers.

```csharp
char ch = 'a';
int ascii = ch; // ascii = 97
```

To get the index of a lowercase character in the alphabet (0 to 25):

```csharp
int bitIndex = ch - 'a';
```

This allows you to map letters to individual bits for compact storage or uniqueness checks.


## Bit Masking

### What is Bit Masking?

**Bit masking** involves using a binary number (mask) to perform operations on specific bits:

* Preserve bits (set to `1`)
* Ignore/clear bits (set to `0`)

### Use Cases:

* Check if a bit is set
* Set a bit
* Clear a bit
* Toggle a bit
* Efficient storage of flags or character sets

### Bitmasking Operations in C# (with comments)

#### Set a Bit (Turn ON a Bit)

```csharp
int mask = 1 << i;   // Shift 1 to the left by i positions to create a mask like 0001000
num |= mask;         // Use OR to set the i-th bit in num to 1
```

#### Unset a Bit (Turn OFF a Bit)

```csharp
int mask = ~(1 << i); // Shift and negate to create mask with 0 at i-th position
num &= mask;          // Use AND to clear the i-th bit in num
```

#### Toggle a Bit

```csharp
int mask = 1 << i;    // Shift to the i-th bit
num ^= mask;          // Use XOR to flip the i-th bit
```

#### Check if a Bit is Set

```csharp
bool isSet = (num & (1 << i)) != 0; // AND with mask to check if bit is 1
```

#### Example: Set bits 0, 2, and 4

```csharp
int mask = (1 << 0) | (1 << 2) | (1 << 4); // OR together: 00010101 = 21
```

You can then apply this `mask` to other numbers to manipulate multiple bits.

Bit masking is powerful in performance-critical applications like:

* Character set tracking (a–z using 26 bits)
* State encoding (e.g., ON/OFF flags)
* Competitive programming


## Bitwise Operators in C\#

| Operator    | Symbol | Description                      | Example                     | Output |          |        |   |
| ----------- | ------ | -------------------------------- | --------------------------- | ------ | -------- | ------ | - |
| AND         | `&`    | Bit is 1 only if both bits are 1 | `5 & 3` → `0101 & 0011`     | 1      |          |        |   |
| OR          | \`     | \`                               | Bit is 1 if either bit is 1 | \`5    | 3`→`0101 | 0011\` | 7 |
| XOR         | `^`    | Bit is 1 if bits are different   | `5 ^ 3` → `0101 ^ 0011`     | 6      |          |        |   |
| NOT         | `~`    | Flip all bits                    | `~5` (32-bit)               | -6     |          |        |   |
| Left Shift  | `<<`   | Shift bits left (multiply by 2)  | `5 << 1`                    | 10     |          |        |   |
| Right Shift | `>>`   | Shift bits right (divide by 2)   | `5 >> 1`                    | 2      |          |        |   |


## Can Bit Manipulation Be Used with Characters or Strings?

Yes! Characters can be treated as integers based on their ASCII values.

### Example: Check if all characters in a string are unique (lowercase a–z)

```csharp
bool AreAllCharactersUnique(string s)
{
    int checker = 0; // Bitmask to track characters seen so far
    foreach (char ch in s)
    {
        int bitIndex = ch - 'a';          // Get position (0 to 25)
        if ((checker & (1 << bitIndex)) > 0)
            return false;                 // Bit already set — duplicate found
        checker |= (1 << bitIndex);       // Set bit to mark character as seen
    }
    return true;
}
```

### Example: Can a string be rearranged to form a palindrome?

```csharp
bool CanFormPalindrome(string s)
{
    int bitMask = 0; // Each bit represents odd/even count for a character
    foreach (char ch in s)
    {
        int bit = ch - 'a';          // Map character to bit position
        bitMask ^= (1 << bit);       // Toggle bit (odd/even toggle)
    }
    return (bitMask & (bitMask - 1)) == 0; // At most one bit set = can be palindrome
}
```

### Example: Find Missing Number (0 to n, one missing)

```csharp
int FindMissingNumber(int[] nums)
{
    int n = nums.Length;
    int xorAll = 0, xorNums = 0;

    for (int i = 0; i <= n; i++)
        xorAll ^= i;          // XOR of all numbers from 0 to n

    foreach (int num in nums)
        xorNums ^= num;       // XOR of all array values

    return xorAll ^ xorNums;  // XOR of both gives the missing number
}
```

## Subset Generation Using Bits

To generate all subsets of a set of size `n`, use the numbers `0` to `2^n - 1` as bitmasks.

```csharp
void GenerateSubsets(string[] items)
{
    int n = items.Length;
    int total = 1 << n; // 2^n subsets

    for (int mask = 0; mask < total; mask++)
    {
        Console.Write("{");
        for (int i = 0; i < n; i++)
        {
            if ((mask & (1 << i)) != 0) // Check if i-th item is included
                Console.Write(items[i] + " ");
        }
        Console.WriteLine("}");
    }
}
```


## Bit Counting and Parity

### Count Set Bits (Brian Kernighan’s Algorithm)

```csharp
int CountSetBits(int num)
{
    int count = 0;
    while (num > 0)
    {
        num &= (num - 1); // Clears the lowest set bit
        count++;
    }
    return count;
}
```

### Check Even or Odd Parity

```csharp
bool IsEven(int num)
{
    return (num & 1) == 0; // If last bit is 0 → even
}
```


## Encoding/Decoding Bit Flags

### Example: Encoding Multiple Boolean Flags

```csharp
// Assume 0th bit = isAdmin, 1st bit = isActive, 2nd bit = isVerified
int EncodeFlags(bool isAdmin, bool isActive, bool isVerified)
{
    int flags = 0;
    if (isAdmin) flags |= (1 << 0);
    if (isActive) flags |= (1 << 1);
    if (isVerified) flags |= (1 << 2);
    return flags;
}
```

### Decode Flags from Integer

```csharp
void DecodeFlags(int flags)
{
    bool isAdmin = (flags & (1 << 0)) != 0;
    bool isActive = (flags & (1 << 1)) != 0;
    bool isVerified = (flags & (1 << 2)) != 0;

    Console.WriteLine($"Admin: {isAdmin}, Active: {isActive}, Verified: {isVerified}");
}
```
