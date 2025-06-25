# How code execution works?

Great question. Let’s clearly break down **stack frames** and the **call stack**, especially in the context of recursion and debugging.

---

## 🔹 What is the **Call Stack**?

The **call stack** is a special memory structure used by your program to **keep track of active method/function calls**.

* Whenever a function is called, a new entry (called a **stack frame**) is added to the top of the call stack.
* When the function completes (returns), that entry is **popped off**, and control goes back to the calling function.

---

## 🔹 What is a **Stack Frame**?

A **stack frame** (or activation record) contains everything needed to execute and resume a function call, such as:

* The **function’s parameters**
* The **function’s local variables**
* The **return address** (i.e., where to go back in the code after the function returns)

Each function call gets **its own unique stack frame**, even if the function is called recursively.

---

## 🔄 How It Works (Simplified)

### Example:

```csharp
int Add(int a, int b) {
    return a + b;
}

int main() {
    int result = Add(2, 3);
    Console.WriteLine(result);
}
```

### Call Stack Behavior:

1. Program starts `main()` → a **stack frame** is created for `main`.
2. `main()` calls `Add(2, 3)` → new **stack frame** is added for `Add`.
3. `Add()` returns `5` → its **stack frame is popped** off.
4. Control returns to `main()`, which prints the result.

---

## 📦 Recursion: Every Call Adds a Stack Frame

### Code:

```csharp
int Factorial(int n) {
    if (n == 0) return 1;
    return n * Factorial(n - 1);
}
```

### For `Factorial(3)`, the call stack grows like this:

| Stack Top (Latest Call) | Stack Frame                          |
| ----------------------- | ------------------------------------ |
| 🟢 `Factorial(0)`       | Base case returns 1                  |
| 🔁 `Factorial(1)`       | Waiting for result of `Factorial(0)` |
| 🔁 `Factorial(2)`       | Waiting for result of `Factorial(1)` |
| 🔁 `Factorial(3)`       | Waiting for result of `Factorial(2)` |
| 🟢 `main()`             | Called `Factorial(3)`                |

Then the stack **unwinds** in reverse order as each function returns its value.

---

## 🔍 In Visual Studio

You can **see the call stack** in:

* `Debug > Windows > Call Stack`
* Shows a **live list of active stack frames**
* Lets you inspect **parameters and locals** for each level

---

## 📌 Summary Table

| Term        | Description                                                        |
| ----------- | ------------------------------------------------------------------ |
| Call Stack  | Stack data structure tracking active function calls                |
| Stack Frame | The memory block for one function call (locals, args, return addr) |
| Push        | When a function is called, a new frame is added                    |
| Pop         | When a function returns, its frame is removed                      |

---

Would you like a visual diagram showing how the stack grows and shrinks during recursive calls like Fibonacci or Tree Traversal?
