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

---

If you want, I can make you a **memory diagram** showing how Singleton in C# is stored and accessed internally — that will make the concept 100% clear.
Do you want me to do that?
