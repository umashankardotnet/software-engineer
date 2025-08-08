Here’s a curated list of **Output-Based Interview Questions** from important C# topics like **OOPs**, **Interface**, **Abstract Class**, **Exception Handling**, and **Try-Catch**. These questions test your understanding of **runtime behavior**, **inheritance**, **method resolution**, and **exception flow**.

---

### 🔹 1. **Interface vs Abstract Class - Method Resolution**

```csharp
interface IAnimal
{
    void Speak();
}

abstract class Dog : IAnimal
{
    public abstract void Speak();
}

class Puppy : Dog
{
    public override void Speak()
    {
        Console.WriteLine("Puppy Barks");
    }
}

class Program
{
    static void Main()
    {
        IAnimal animal = new Puppy();
        animal.Speak();
    }
}
```

**Output:**

```
Puppy Barks
```

---

### 🔹 2. **Abstract Class - Constructor Execution Order**

```csharp
abstract class A
{
    public A() { Console.WriteLine("A Constructor"); }
}

class B : A
{
    public B() { Console.WriteLine("B Constructor"); }
}

class Program
{
    static void Main()
    {
        B b = new B();
    }
}
```

**Output:**

```
A Constructor
B Constructor
```

---

### 🔹 3. **Method Hiding vs Overriding**

```csharp
class Base
{
    public void Show() => Console.WriteLine("Base.Show");
}

class Derived : Base
{
    public new void Show() => Console.WriteLine("Derived.Show");
}

class Program
{
    static void Main()
    {
        Base obj = new Derived();
        obj.Show();
    }
}
```

**Output:**

```
Base.Show
```

> ⚠️ Because method hiding (`new`) doesn't override the base class method.

---

### 🔹 4. **Try-Catch-Finally Return Flow**

```csharp
class Program
{
    static int Test()
    {
        try
        {
            Console.WriteLine("Try");
            return 1;
        }
        catch
        {
            Console.WriteLine("Catch");
            return 2;
        }
        finally
        {
            Console.WriteLine("Finally");
        }
    }

    static void Main()
    {
        Console.WriteLine(Test());
    }
}
```

**Output:**

```
Try
Finally
1
```

> ✅ Finally always executes, even after a return statement.

---

### 🔹 5. **Exception Re-Throw Behavior**

```csharp
class Program
{
    static void Main()
    {
        try
        {
            throw new InvalidOperationException("Invalid operation");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Caught: " + ex.Message);
            throw;
        }
        Console.WriteLine("End");
    }
}
```

**Output:**

```
Caught: Invalid operation
Unhandled exception: InvalidOperationException
```

> ⚠️ The re-thrown exception is **not caught again**, so program crashes.

---

### 🔹 6. **Multiple Interfaces with Same Method Signature**

```csharp
interface I1 { void Show(); }
interface I2 { void Show(); }

class MyClass : I1, I2
{
    void I1.Show() => Console.WriteLine("I1 Show");
    void I2.Show() => Console.WriteLine("I2 Show");
}

class Program
{
    static void Main()
    {
        I1 a = new MyClass();
        a.Show();

        I2 b = new MyClass();
        b.Show();
    }
}
```

**Output:**

```
I1 Show
I2 Show
```

> ✅ Explicit interface implementation needed when methods conflict.

---

### 🔹 7. **Static Constructor Execution**

```csharp
class Test
{
    static Test() => Console.WriteLine("Static Constructor");
    public Test() => Console.WriteLine("Instance Constructor");
}

class Program
{
    static void Main()
    {
        Test t1 = new Test();
        Test t2 = new Test();
    }
}
```

**Output:**

```
Static Constructor
Instance Constructor
Instance Constructor
```

> 🔁 Static constructor runs **only once per type**.

---

### 🔹 8. **Multiple Catch Blocks**

```csharp
class Program
{
    static void Main()
    {
        try
        {
            int x = int.Parse("abc");
        }
        catch (FormatException)
        {
            Console.WriteLine("Format Exception");
        }
        catch (Exception)
        {
            Console.WriteLine("General Exception");
        }
    }
}
```

**Output:**

```
Format Exception
```

> ✅ Most specific catch block runs first.

---

### 🔹 9. **Inheritance with Abstract Methods**

```csharp
abstract class Shape
{
    public abstract int Area();
}

class Square : Shape
{
    private int side = 5;
    public override int Area() => side * side;
}

class Program
{
    static void Main()
    {
        Shape s = new Square();
        Console.WriteLine(s.Area());
    }
}
```

**Output:**

```
25
```

---

### 🔹 10. **Virtual Method and Base Class Reference**

```csharp
class A
{
    public virtual void Print() => Console.WriteLine("A");
}

class B : A
{
    public override void Print() => Console.WriteLine("B");
}

class Program
{
    static void Main()
    {
        A obj = new B();
        obj.Print();
    }
}
```

**Output:**

```
B
```

> ✅ **Polymorphism** at work – base class reference calling overridden method.

---

## ✅ Tips for Interviewers Asking These Questions

* Focus on **runtime behavior**, not just compilation.
* Check understanding of:

  * Method resolution (`new` vs `override`)
  * Exception flow
  * Inheritance and constructor execution
  * Interface conflicts
  * `finally` vs `return`
  * Static initialization timing

Here’s a comprehensive list of **C# output-based interview questions** covering **OOPs concepts**, **constructors**, **inheritance**, **abstract classes**, **interfaces**, **exception handling**, and more — **with explanations and expected outputs**.

---

### ✅ **1. Constructor Chaining (Base to Derived)**

```csharp
class A
{
    public A()
    {
        Console.WriteLine("A Constructor");
    }
}

class B : A
{
    public B()
    {
        Console.WriteLine("B Constructor");
    }
}

class Program
{
    static void Main()
    {
        B obj = new B();
    }
}
```

**Output:**

```
A Constructor  
B Constructor
```

> 🔍 **Explanation**: Base class constructor is always called first before derived class constructor.

---

### ✅ **2. Constructor Overloading and Chaining**

```csharp
class A
{
    public A() : this(5)
    {
        Console.WriteLine("Default Constructor");
    }

    public A(int x)
    {
        Console.WriteLine("Parameterized Constructor: " + x);
    }
}

class Program
{
    static void Main()
    {
        A obj = new A();
    }
}
```

**Output:**

```
Parameterized Constructor: 5  
Default Constructor
```

---

### ✅ **3. Abstract Class with Abstract and Non-Abstract Members**

```csharp
abstract class Shape
{
    public abstract void Draw();
    public void Print() => Console.WriteLine("Print Shape");
}

class Circle : Shape
{
    public override void Draw() => Console.WriteLine("Draw Circle");
}

class Program
{
    static void Main()
    {
        Shape shape = new Circle();
        shape.Draw();
        shape.Print();
    }
}
```

**Output:**

```
Draw Circle  
Print Shape
```

---

### ✅ **4. Interface vs Abstract - Multiple Inheritance**

```csharp
interface IA
{
    void Display();
}

interface IB
{
    void Display();
}

class MyClass : IA, IB
{
    void IA.Display() => Console.WriteLine("IA Display");
    void IB.Display() => Console.WriteLine("IB Display");
}

class Program
{
    static void Main()
    {
        IA a = new MyClass();
        a.Display();

        IB b = new MyClass();
        b.Display();
    }
}
```

**Output:**

```
IA Display  
IB Display
```

> 🔍 **Explanation**: Explicit interface implementation is used to avoid conflict.

---

### ✅ **5. Exception Handling Output**

```csharp
try
{
    Console.WriteLine("Try block");
    int x = 10 / 0;
}
catch (DivideByZeroException)
{
    Console.WriteLine("Divide by zero");
}
catch (Exception)
{
    Console.WriteLine("General exception");
}
finally
{
    Console.WriteLine("Finally block");
}
```

**Output:**

```
Try block  
Divide by zero  
Finally block
```

---

### ✅ **6. Interface Default Implementation (C# 8+)**

```csharp
interface ITest
{
    void Show();
    void Print() => Console.WriteLine("Default Implementation");
}

class Demo : ITest
{
    public void Show() => Console.WriteLine("Show from Demo");
}

class Program
{
    static void Main()
    {
        ITest obj = new Demo();
        obj.Show();
        obj.Print();
    }
}
```

**Output (C# 8+):**

```
Show from Demo  
Default Implementation
```

---

### ✅ **7. Virtual and Override**

```csharp
class Animal
{
    public virtual void Sound() => Console.WriteLine("Animal sound");
}

class Dog : Animal
{
    public override void Sound() => Console.WriteLine("Dog bark");
}

class Program
{
    static void Main()
    {
        Animal a = new Dog();
        a.Sound();
    }
}
```

**Output:**

```
Dog bark
```

---

### ✅ **8. Polymorphism Without Override**

```csharp
class Base
{
    public void Show() => Console.WriteLine("Base Show");
}

class Derived : Base
{
    public new void Show() => Console.WriteLine("Derived Show");
}

class Program
{
    static void Main()
    {
        Base b = new Derived();
        b.Show();
    }
}
```

**Output:**

```
Base Show
```

> 🔍 Use of `new` keyword hides the base method; it is not overridden.

---

### ✅ **9. Finalizer (Destructor)**

```csharp
class Test
{
    ~Test()
    {
        Console.WriteLine("Destructor called");
    }
}

class Program
{
    static void Main()
    {
        Test t = new Test();
    }
}
```

**Output:**

```
(No guaranteed output — destructor called by GC)
```

> 🔍 Destructors are called by the Garbage Collector — not deterministically.

---

### ✅ **10. Multiple Catch Blocks and Rethrow**

```csharp
try
{
    int[] arr = new int[2];
    Console.WriteLine(arr[5]);
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine("Caught IndexOutOfRange");
    throw;
}
catch (Exception)
{
    Console.WriteLine("Caught General");
}
```

**Output:**

```
Caught IndexOutOfRange  
(Unhandled exception thrown after rethrow)
```
---
Here's a **detailed comparative guide and examples** covering all the topics you've listed — focusing on **abstract classes vs interfaces in multi-level inheritance**, **interface in diamond problem**, **constructor hiding**, **try-catch inside loops**, **throwing from finally**, **static constructor behavior**, and **C# 9+ features like record types and pattern matching**.



## ✅ **1. Abstract Class vs Interface in Multi-level Inheritance**

### 🔷 Abstract Class

* Can have fields and constructors.
* Supports default implementation.
* Only one abstract class can be inherited.

```csharp
abstract class Animal {
    public abstract void MakeSound();
    public void Eat() => Console.WriteLine("Animal eats");
}

class Dog : Animal {
    public override void MakeSound() => Console.WriteLine("Bark");
}

class Puppy : Dog { }

var puppy = new Puppy();
puppy.MakeSound();  // Bark
puppy.Eat();        // Animal eats
```

### 🔷 Interface

* Cannot have fields (until C# 8+, where static members are supported with limitations).
* Supports multiple inheritance.

```csharp
interface IMovable {
    void Move();
}

interface IRunnable : IMovable {
    void Run();
}

class Human : IRunnable {
    public void Move() => Console.WriteLine("Moving");
    public void Run() => Console.WriteLine("Running");
}
```

---

## ✅ **2. Interface Implementation in Diamond Problem**

C# **avoids diamond problem** using **explicit interface implementation**:

```csharp
interface IA { void Show(); }
interface IB : IA { }
interface IC : IA { }

class MyClass : IB, IC {
    void IA.Show() => Console.WriteLine("IA");
}

var obj = new MyClass();
// obj.Show(); // ❌ Compile-time error
((IA)obj).Show();  // ✅ Outputs: IA
```

> Since C# does not support multiple class inheritance, it avoids the ambiguity of diamond inheritance by forcing explicit interface implementation.

---

## ✅ **3. Constructor Hiding (New vs Override)**

```csharp
class Base {
    public Base() => Console.WriteLine("Base Constructor");
}

class Derived : Base {
    public new Derived() => Console.WriteLine("Derived Constructor");
}
```

### ❗Explanation:

* Constructors **can’t be overridden**, only hidden.
* `new` just hides the base class method; base constructor will **always run first**.
* Use `base()` to explicitly call parameterized base constructor.

---

## ✅ **4. Try-Catch Inside Loops**

```csharp
for (int i = 0; i < 3; i++) {
    try {
        if (i == 1) throw new Exception("Test");
        Console.WriteLine($"i={i}");
    }
    catch {
        Console.WriteLine("Exception caught");
    }
}
```

**Output:**

```
i=0
Exception caught
i=2
```

> ✅ Try-catch handles exception without breaking the loop. Useful for retry mechanisms.

---

## ✅ **5. Throwing Exceptions from Finally Block**

```csharp
try {
    throw new Exception("Try");
}
catch {
    Console.WriteLine("Catch");
}
finally {
    Console.WriteLine("Finally");
    throw new Exception("From Finally");
}
```

**Output:**

```
Catch
Finally
Unhandled Exception: From Finally
```

> ❗ Exception in `finally` **overrides** any previous exception and **can hide the original error** — be cautious.

---

## ✅ **6. Static Constructor Behavior**

```csharp
class Test {
    static Test() {
        Console.WriteLine("Static constructor");
    }

    public Test() {
        Console.WriteLine("Instance constructor");
    }
}

var t1 = new Test();
var t2 = new Test();
```

**Output:**

```
Static constructor
Instance constructor
Instance constructor
```

> Static constructor runs **once per type** before first access or instance creation. No parameters allowed, no explicit call.

---

## ✅ **7. Record Types & Pattern Matching (C# 9/10+)**

### 🔷 Record Types (Immutable by default)

```csharp
public record Person(string Name, int Age);

var p1 = new Person("Alice", 30);
var p2 = p1 with { Age = 35 };

Console.WriteLine(p2); // Person { Name = Alice, Age = 35 }
```

### 🔷 Positional Pattern Matching

```csharp
Person person = new("John", 20);
if (person is ("John", >18)) {
    Console.WriteLine("Adult named John");
}
```

> ✅ Records support:

* Value-based equality
* Deconstruction
* With-expressions
* Concise syntax

---

## 🧠 Summary Table

| Concept                     | Notes                                               |
| --------------------------- | --------------------------------------------------- |
| Abstract class vs Interface | Abstract class = one base; Interface = multiple     |
| Diamond problem             | Avoided via explicit interface implementation       |
| Constructor hiding          | `new` keyword hides base constructor, not override  |
| Try-catch in loop           | Useful for fault isolation                          |
| Finally throwing            | Dangerous – can suppress original exceptions        |
| Static constructor          | Runs once per type                                  |
| Record types (C# 9+)        | Value-based, immutable types with built-in features |
| Pattern matching (C# 9/10+) | Powerful condition handling syntax                  |

