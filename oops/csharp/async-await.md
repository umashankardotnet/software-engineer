Alright — let’s make a **complete, interview-level guide** for **`async` / `await` in C#**
We’ll cover **how it works**, **relation to threads**, **`Task` & `Task.WhenAll`**, **CancellationTokens**, **`ConfigureAwait`**, and **common pitfalls** — plus **real examples**.

---

## **1. Why Async/Await Exists**

* To write **non-blocking code** in a synchronous style.
* Lets you **free threads** while waiting for I/O or other delays.
* Improves responsiveness in **UI apps** and **throughput** in **server apps**.

---

## **2. Core Concepts**

### **`async` keyword**

* Marks a method as asynchronous.
* Allows the use of `await` inside.
* An `async` method returns **`Task`**, **`Task<T>`**, or **`ValueTask`** (or `void` in event handlers).

```csharp
public async Task<int> GetDataAsync()
{
    await Task.Delay(1000); // simulates I/O
    return 42;
}
```

---

### **`await` keyword**

* Tells the compiler to **pause execution** until the awaited task finishes.
* The method returns control to the **caller** while awaiting.
* After the task completes, execution resumes **after** the `await`.

---

### How it relates to **Threads**

* `await` **does not** create new threads by itself.
* While waiting (e.g., network, file, DB), the thread is **released back** to the thread pool.
* When the operation finishes, **a thread** (could be the same or different) continues execution.
* For **CPU-bound** work → use `Task.Run()` to run on a thread pool thread.

---

## **3. Example: Basic Async/Await**

```csharp
public async Task ExampleAsync()
{
    Console.WriteLine("Before await: " + Thread.CurrentThread.ManagedThreadId);

    await Task.Delay(1000); // I/O wait simulation

    Console.WriteLine("After await: " + Thread.CurrentThread.ManagedThreadId);
}
```

Output in a console app might show different thread IDs (but not guaranteed).

---

## **4. Parallel Execution with `Task.WhenAll`**

```csharp
public async Task DemoWhenAllAsync()
{
    var task1 = GetDataAsync(1);
    var task2 = GetDataAsync(2);
    var task3 = GetDataAsync(3);

    int[] results = await Task.WhenAll(task1, task2, task3);

    Console.WriteLine(string.Join(", ", results));
}

private async Task<int> GetDataAsync(int id)
{
    await Task.Delay(1000);
    return id * 10;
}
```

* All tasks start **before** the `await Task.WhenAll`.
* **Total time** ≈ longest task, not sum of all.

---

## **5. Cancellation with `CancellationToken`**

```csharp
public async Task FetchDataAsync(CancellationToken token)
{
    for (int i = 0; i < 5; i++)
    {
        token.ThrowIfCancellationRequested();
        await Task.Delay(1000, token);
        Console.WriteLine($"Step {i+1} done");
    }
}
```

**Usage:**

```csharp
var cts = new CancellationTokenSource();
var task = FetchDataAsync(cts.Token);
cts.CancelAfter(2500); // Cancel after 2.5 sec
await task;
```

* **CancellationToken** propagates stop requests to awaited operations.
* Always **pass tokens** into async methods when you can.

---

## **6. Async vs Thread.Sleep / Task.Delay**

| **Method**         | Blocks Thread? | Use Case                                  |
| ------------------ | -------------- | ----------------------------------------- |
| `Thread.Sleep(ms)` | ✅ Yes          | Rare, testing, force pause                |
| `Task.Delay(ms)`   | ❌ No           | Non-blocking delays, timers in async code |

Example:

```csharp
Thread.Sleep(5000);  // Wastes thread for 5 sec
await Task.Delay(5000); // Frees thread, resumes later
```

---

## **7. `Task.Run` vs Async I/O**

* `Task.Run` → Run CPU-bound work on a **thread pool thread**.
* `await` on async I/O method → No thread is blocked.

```csharp
await Task.Run(() => DoHeavyCalculation());
```

---

## **8. `ConfigureAwait`**

```csharp
await Task.Delay(1000).ConfigureAwait(false);
```

* **true** (default) → Capture and resume on original context (UI, ASP.NET sync context).
* **false** → Resume on any thread → avoids deadlocks & improves performance in non-UI code.

**Library code best practice:** Use `.ConfigureAwait(false)`.

---

## **9. Exception Handling in Async Methods**

```csharp
try
{
    await SomeAsync();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

* Exceptions are captured and re-thrown when awaited.
* Un-awaited async methods → Exceptions go unobserved.

---

## **10. Advanced Example — Async, Await, Task.WhenAll, Cancellation**

```csharp
public async Task ProcessMultipleAsync(CancellationToken token)
{
    var tasks = new[]
    {
        Task.Delay(2000, token),
        Task.Delay(3000, token),
        Task.Delay(1000, token)
    };

    await Task.WhenAll(tasks);
    Console.WriteLine("All tasks done");
}
```

* Cancelling token will cancel all tasks if passed in.

---

## **11. Common Pitfalls**

* **Mixing sync + async** → Deadlocks in UI/ASP.NET if `.Result` or `.Wait()` used.
* **Forgetting to await** → Task runs but exceptions get lost.
* **CPU-bound work in async method** → Won’t be parallel unless `Task.Run` used.
* **Not using cancellation tokens** → Can't stop long operations cleanly.

---

## **12. Decision Flowchart**

```
Is the work I/O bound? → Use async/await
Is it CPU bound and long? → Use Task.Run inside async
Do I need parallelism? → Use Task.WhenAll / Parallel.ForEachAsync
Do I need UI thread after await? → Keep ConfigureAwait(true)
Otherwise → ConfigureAwait(false)
```

---

If you want, I can now make **one “Master PDF-style” guide** combining this Async/Await section with **`ConfigureAwait` diagrams**, **thread comparison tables**, and **practical interview questions** so you’ll have the full story in one place.

Do you want me to prepare that combined version?
