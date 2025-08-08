Great! Let's take **another real-world code example** that **intentionally violates multiple software engineering principles** — including **SOLID, DRY, KISS, YAGNI**, and general **C# best practices**.

I'll give you a flawed class first, and then you tell me which principles are violated. After that, I’ll cross-verify your answers like a **mock interviewer**.

---

### **Bad Code Example** – Inventory Management System

```csharp
public class InventoryManager
{
    private List<string> items = new List<string>();

    public void AddItem(string item, int quantity, string category)
    {
        for (int i = 0; i < quantity; i++)
        {
            items.Add(item + "-" + category);
        }

        // send notification
        Console.WriteLine("Added " + quantity + " " + item + "(s) to inventory in category: " + category);

        // audit logging
        Console.WriteLine("Audit: Item added at " + DateTime.Now);
    }

    public void RemoveItem(string item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Console.WriteLine("Removed item: " + item);
        }
        else
        {
            Console.WriteLine("Item not found: " + item);
        }
    }

    public void PrintInventory()
    {
        Console.WriteLine("Current Inventory:");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public void BackupInventory()
    {
        Console.WriteLine("Backing up inventory to disk...");
        // placeholder logic
    }

    public void LoadInventory()
    {
        Console.WriteLine("Loading inventory from disk...");
        // placeholder logic
    }
}
```

---

### Your Task:

Act like you're in a **mock interview**. Look at the above code and tell me:

* Which **SOLID** principles are violated?
* Which **C# best practices** are broken?
* Is **DRY**, **KISS**, or **YAGNI** violated?
* How would you **refactor it**?

Once you're done, I’ll evaluate your response and add insights or corrections. Ready?



### ✅ Your Observations (and validation):

#### ✅ **1. SRP (Single Responsibility Principle)** — ✔️ Correct

You said:

> *“Breaking SRP as we can use separate classes for Auditing and Notification.”*

**Mock Interviewer's Take:**
Exactly! `InventoryManager` is doing **too much**:

* Managing inventory
* Handling notifications (`Console.WriteLine`)
* Performing audit logging
* Handling persistence (backup/load)

➡️ **Fix:** Introduce interfaces like `INotificationService`, `IAuditLogger`, and `IInventoryStorage`, and inject them via constructor (Dependency Injection).

---

#### ✅ **2. YAGNI (You Aren’t Gonna Need It)** — ✔️ Correct

You said:

> *“BackupInventory and LoadInventory are not being used so as per YAGNI remove that code.”*

**Mock Interviewer's Take:**
Correct. Adding functionality “just in case” violates YAGNI. Those methods are placeholders, and they pollute the class unnecessarily unless needed.

---

#### 🔄 **3. Dependency Injection** — ✔️ Suggested

> *“Inject those 2 classes as a dependency in constructor.”*

**Mock Interviewer's Take:**
Spot on. Right now, the class is tightly coupled with its implementations (`Console.WriteLine`). Using DI allows for better testing, decoupling, and extensibility.

---

### ❌ Missed or Partially Covered Issues

Let’s walk through what you **could also mention**:

#### ❌ **OCP (Open-Closed Principle)**

The class is not **open for extension and closed for modification**. For example:

* If tomorrow we want to store items by **category**, or send **email instead of console log**, we need to modify the existing code.
* Adding a new audit type or notification logic would mean changing the `InventoryManager`.

➡️ Using interfaces like `INotificationService` and strategy for `ItemStorageStrategy` would help.

---

#### ❌ **DRY (Don't Repeat Yourself)**

```csharp
Console.WriteLine("...");
Console.WriteLine("Audit: Item added at " + DateTime.Now);
Console.WriteLine("Removed item: " + item);
```

**Lots of repeated string concatenation logic.**

➡️ Could extract common logging patterns or message formatting logic into a utility or logger.

---

#### ❌ **KISS (Keep It Simple, Stupid)**

The class **tries to do too much** and mixes responsibilities, which **adds cognitive load**.

➡️ Break responsibilities into smaller focused classes.

---

#### ❌ **Encapsulation & Collection Usage**

```csharp
private List<string> items = new List<string>();
```

* You’re storing `string` to represent complex items (`item + "-" + category`) — this breaks **OOP principles**.

➡️ Create a proper class:

```csharp
public class InventoryItem
{
    public string Name { get; set; }
    public string Category { get; set; }
}
```

---

### 🔁 Suggested Refactored Class Design (High-Level)

```csharp
public class InventoryManager
{
    private readonly INotifier _notifier;
    private readonly IAuditLogger _auditLogger;
    private readonly IInventoryStorage _storage;
    private readonly List<InventoryItem> _items = new();

    public InventoryManager(INotifier notifier, IAuditLogger logger, IInventoryStorage storage)
    {
        _notifier = notifier;
        _auditLogger = logger;
        _storage = storage;
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
            _items.Add(item);

        _notifier.Notify($"Added {quantity} {item.Name}(s) to inventory");
        _auditLogger.Log("Item added at " + DateTime.UtcNow);
    }

    // ... other SRP-aligned methods
}
```

---

### ⭐ Summary Feedback (as Interviewer):

✅ You've nailed the **major issues** — SRP and YAGNI — and identified DI.

🔄 If this was a real interview, I’d ask you to dig deeper into:

* OCP/DRY/KISS
* Modeling data (e.g., avoid string concatenation in inventory)
* Using proper abstractions/interfaces

---

Would you like **another example** with more advanced principles like **LSP or ISP violations**, or a **code review style test case** next?
