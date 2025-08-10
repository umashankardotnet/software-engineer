Alright — here’s a **complete guide to Singleton Pattern in C#** with different safe implementation styles you’ll actually see in .NET projects.

---

## 🔷 **What is the Singleton Pattern?**

The **Singleton Pattern** ensures that:

1. Only **one instance** of a class exists in the application.
2. That instance is **globally accessible**.

> Commonly used for things like **logging services, configuration managers, caching layers, or shared resources**.

---

## **1️⃣ Basic Thread-Safe Singleton (Lazy Initialization)**

```csharp
public sealed class Singleton
{
    private static Singleton _instance = null;
    private static readonly object _lock = new object();

    // Private constructor prevents instantiation from other classes
    private Singleton() 
    {
        Console.WriteLine("Singleton Instance Created");
    }

    public static Singleton Instance
    {
        get
        {
            // Double-check locking for thread safety
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }

    public void DoWork()
    {
        Console.WriteLine("Singleton is doing work!");
    }
}
```

**Usage:**

```csharp
class Program
{
    static void Main(string[] args)
    {
        Singleton.Instance.DoWork();
        Singleton.Instance.DoWork(); // Same instance used
    }
}
```

---

## **2️⃣ Eager Initialization (Simplest)**

If the instance is cheap to create and you want it immediately:

```csharp
public sealed class Singleton
{
    private static readonly Singleton _instance = new Singleton();

    private Singleton()
    {
        Console.WriteLine("Singleton Instance Created");
    }

    public static Singleton Instance => _instance;

    public void DoWork()
    {
        Console.WriteLine("Singleton is doing work!");
    }
}
```

**Pros:** Simple, thread-safe without locking.
**Cons:** Instance is created even if never used.

---

## **3️⃣ Lazy<T> Implementation (Best Practice in .NET)**

This is **my recommended approach** for .NET because it’s clean, thread-safe, and supports lazy loading without manual locking.

```csharp
public sealed class Singleton
{
    private static readonly Lazy<Singleton> _lazyInstance =
        new Lazy<Singleton>(() => new Singleton());

    private Singleton()
    {
        Console.WriteLine("Singleton Instance Created");
    }

    public static Singleton Instance => _lazyInstance.Value;

    public void DoWork()
    {
        Console.WriteLine("Singleton is doing work!");
    }
}
```

**Usage:**

```csharp
Singleton.Instance.DoWork();
```

---

## **4️⃣ Singleton with Dependency Injection (ASP.NET Core)**

In ASP.NET Core, you can register singletons via DI:

```csharp
public interface IMyService
{
    void DoWork();
}

public class MyService : IMyService
{
    public void DoWork()
    {
        Console.WriteLine("Service is working!");
    }
}

// In Program.cs or Startup.cs
builder.Services.AddSingleton<IMyService, MyService>();
```

**Usage via constructor injection:**

```csharp
public class HomeController
{
    private readonly IMyService _service;
    public HomeController(IMyService service)
    {
        _service = service;
    }

    public void Index()
    {
        _service.DoWork();
    }
}
```

---

## 🔹 **When to Use Singleton Pattern**

✅ When you need **only one instance** across the application (e.g., configuration, logger).
✅ When the instance is **shared and expensive to create**.
❌ Avoid if you need multiple independent instances (overuse causes hidden dependencies).

We mark a **Singleton class** as `sealed` in C# to make sure **nobody can inherit it**, which is important for keeping the **"only one instance" guarantee** intact.

---

## 🔹 Why `sealed` in Singleton?

If the class wasn’t sealed:

1. **Inheritance can break the Singleton guarantee**

   * A derived class can have its own static instance, creating **multiple instances** across types.
   * This violates the "only one instance" rule.

2. **Security & Predictability**

   * Prevents unexpected behavior due to subclass overrides.
   * Keeps the Singleton lifecycle controlled and predictable.

3. **Reflection + Inheritance combo risk**

   * If another developer inherits your Singleton and exposes a public constructor, they could bypass the private constructor.

---

### 📌 Example: Singleton without `sealed` – Broken

```csharp
public class Singleton
{
    public static Singleton Instance { get; } = new Singleton();
    protected Singleton() { }
}

public class ChildSingleton : Singleton
{
    public static new ChildSingleton Instance { get; } = new ChildSingleton();
    private ChildSingleton() { }
}

class Program
{
    static void Main()
    {
        var parentInstance = Singleton.Instance;
        var childInstance = ChildSingleton.Instance; // ❌ Now we have 2 instances!
    }
}
```

**Result:** Singleton guarantee broken.

---

### 📌 Correct: Singleton with `sealed`

```csharp
public sealed class Singleton
{
    public static Singleton Instance { get; } = new Singleton();
    private Singleton() { }
}
```

Now:

* No class can inherit it.
* Only **one** instance is possible.

---

✅ **In summary:**
We use `sealed` in Singleton to **prevent inheritance**, which could create multiple instances and break the Singleton’s core purpose.

---

If you want, I can show you **how reflection can still break Singleton** unless we handle it — that’s another subtle Singleton trap in C#. Would you like me to?

Alright — here’s the subtle trap with **Singleton + Reflection** in C#.

Even if you mark a Singleton as **`sealed`** and make the constructor `private`, **reflection** can still create a new instance because it can bypass access modifiers.

---

## 🔹 Breaking Singleton with Reflection

```csharp
using System;
using System.Reflection;

public sealed class Singleton
{
    private static readonly Singleton _instance = new Singleton();
    public static Singleton Instance => _instance;

    private Singleton()
    {
        Console.WriteLine("Singleton constructor called");
    }
}

class Program
{
    static void Main()
    {
        var instance1 = Singleton.Instance;

        // Bypass private constructor using reflection
        ConstructorInfo ctor = typeof(Singleton).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null
        );

        var instance2 = (Singleton)ctor.Invoke(null);

        Console.WriteLine(instance1 == instance2 
            ? "Same instance" 
            : "Different instance ❌ Singleton broken");
    }
}
```

---

### **Output**

```
Singleton constructor called
Singleton constructor called
Different instance ❌ Singleton broken
```

💡 Even though the Singleton is `sealed`, Reflection **doesn’t care** about inheritance rules — it just calls the private constructor.

---

## 🔹 How to Protect Singleton from Reflection

We can **add a guard** inside the constructor to throw an exception if someone tries to create a second instance.

```csharp
public sealed class Singleton
{
    private static bool _isInstanceCreated = false;
    private static readonly Singleton _instance = new Singleton();
    public static Singleton Instance => _instance;

    private Singleton()
    {
        if (_isInstanceCreated)
        {
            throw new InvalidOperationException("Cannot create multiple instances of Singleton");
        }
        _isInstanceCreated = true;
        Console.WriteLine("Singleton instance created");
    }
}
```

Now if Reflection tries to create a new instance:

```
InvalidOperationException: Cannot create multiple instances of Singleton
```

---

## 🔹 Extra Note – Dependency Injection

In ASP.NET Core, singletons are usually managed by the **DI container** (e.g., `AddSingleton()`), which handles lifetime and avoids reflection issues in normal scenarios.
The reflection problem mainly arises in **low-level libraries or utility classes** where you manually control instance creation.

---

If you want, I can also show **how serialization can break Singleton** and how to fix it — because that’s another sneaky edge case many developers miss.
Do you want me to go into that?

