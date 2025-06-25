# How code execution works?

Understanding **how memory is allocated** in .NET (or any modern runtime) is crucial, especially for interviews and debugging performance issues.

Let’s break down **Stack vs Heap memory** clearly, in the context of C#/.NET.


## 🔹 Overview: Stack vs Heap

| Aspect           | Stack                                                 | Heap                                                    |
| ---------------- | ----------------------------------------------------- | ------------------------------------------------------- |
| **Usage**        | Stores **local variables** and **function call data** | Stores **objects**, reference types, and dynamic memory |
| **Memory Type**  | Small, fast, structured memory                        | Large, slower, flexible memory                          |
| **Allocation**   | Automatically managed (LIFO)                          | Managed by **Garbage Collector**                        |
| **Lifetime**     | Ends when function ends                               | Continues until GC removes it                           |
| **Access Speed** | Very fast                                             | Slower (needs pointer dereferencing)                    |
| **Thread-safe**  | Yes — each thread has its own stack                   | No — needs synchronization                              |


## 🔹 What Goes Where?

### ✅ Stored on the **Stack**:

* Value types (like `int`, `bool`, `char`, `struct`) declared as **local variables** in methods.
* Function parameters (if value types).
* Return addresses, stack frames for function calls.

```csharp
void Foo() {
    int x = 10;           // x is on the stack
}
```


### ✅ Stored on the **Heap**:

* **Reference types** (classes, arrays, strings, delegates, etc.).
* Any object created using `new`.
* Members of reference types (even if value types).

```csharp
class Person {
    public int Age;
}

void Bar() {
    Person p = new Person();   // p is a reference on stack, actual object is on heap
    p.Age = 30;                // Age is stored on heap as part of the object
}
```


## 🔄 Example: Stack + Heap Together

```csharp
class Book {
    public string Title;
}

void Run() {
    int number = 5;              // Stack
    Book b = new Book();         // 'b' is on stack, Book object is on heap
    b.Title = "C# in Depth";     // Title is a string on heap
}
```

* `number` → stored on **stack**
* `b` → reference (pointer) on **stack**
* `Book object` → stored on **heap**
* `"C# in Depth"` → also on **heap** (strings are reference types)


## 🔹 How Garbage Collection Works (for Heap)

* The **.NET GC (Garbage Collector)** automatically frees memory from the heap when:

  * An object is no longer **reachable** (no reference points to it).
  * It runs in **generations** (Gen 0, 1, 2) to optimize performance.

You don’t manually free heap memory in C# (unless using unmanaged resources + `Dispose()`).


## 🔹 Struct vs Class: Stack vs Heap?

* `struct` → value type → stored on the **stack** (unless inside a reference type).
* `class` → reference type → stored on the **heap**

```csharp
struct Point { int X, Y; }
class Shape { public Point P; }

void Foo() {
    Point pt = new Point();     // Entire struct is on the stack
    Shape s = new Shape();      // s on stack, Shape object on heap, P on heap
}
```


## 🔹 Summary Diagram

```
Method Call: Foo()
--------------------------
| Stack (LIFO)            |
|------------------------|
| Return address         |
| Local int: x = 5       |
| Reference: p (ptr)     |
|------------------------|

Heap (dynamic memory)
--------------------------
| Object: new Person()   |
| → Age = 30             |
|------------------------|
| String: "Hello World"  |
|------------------------|
```

---

## 🧠 Summary

| Memory Area | Stores What                              | Managed By             | Lifetime                  |
| ----------- | ---------------------------------------- | ---------------------- | ------------------------- |
| Stack       | Local value types, method calls, frames  | Compiler/runtime       | Ends with method scope    |
| Heap        | Objects, reference types, dynamic memory | .NET Garbage Collector | Until no reference exists |

---

# Call Stack and Stack Frame
Let’s clearly break down **stack frames** and the **call stack**, especially in the context of recursion and debugging.


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
