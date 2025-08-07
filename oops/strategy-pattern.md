# Strategy Design Pattern

The **Strategy Design Pattern** is a behavioral design pattern that allows you to define a family of algorithms, put each of them in a separate class, and make their objects interchangeable. It enables the algorithm to vary independently from the clients that use it.

### 🧠 **Category:**

* **Behavioral** Design Pattern
  Because it focuses on the behavior of objects and how they interact.

---

### ✅ **When to Use Strategy Pattern**

Use it when:

* You have multiple algorithms (or behaviors) for a specific task and want to switch between them easily.
* You want to avoid using complex `if-else` or `switch-case` statements for behavior selection.
* You want to follow the **Open/Closed Principle** — classes should be open for extension but closed for modification.

---

### 🔧 **Structure**

* **Context**: Uses a strategy object.
* **Strategy (Interface)**: Declares a method the context uses.
* **Concrete Strategies**: Implement different versions of the strategy.

---

### 🏗️ **UML Diagram**

```
          +------------------+
          |   IShippingCost  |  <-- Strategy Interface
          +------------------+
          | +Calculate(Order): decimal |
                  ▲
                  |
  +---------------+-----------------+
  |                                 |
+------------------+       +--------------------+
| StandardShipping |       |  ExpressShipping   |
+------------------+       +--------------------+
| +Calculate(...)  |       | +Calculate(...)    |
+------------------+       +--------------------+

                +----------------------+
                |     ShippingService  |  <-- Context
                +----------------------+
                | -strategy: IShippingCost |
                | +CalculateShipping(...) |
                +----------------------+
```

---

### 👨‍💻 **C# Example**

```csharp
// Strategy Interface
public interface IShippingCostStrategy
{
    decimal CalculateShipping(Order order);
}

// Concrete Strategies
public class StandardShipping : IShippingCostStrategy
{
    public decimal CalculateShipping(Order order) => 5.00m;
}

public class ExpressShipping : IShippingCostStrategy
{
    public decimal CalculateShipping(Order order) => 15.00m;
}

// Context
public class ShippingService
{
    private IShippingCostStrategy _strategy;

    public ShippingService(IShippingCostStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal Calculate(Order order)
    {
        return _strategy.CalculateShipping(order);
    }

    public void SetStrategy(IShippingCostStrategy strategy)
    {
        _strategy = strategy;
    }
}

// Domain Class
public class Order
{
    public string OrderName { get; set; }
    public string OrderType { get; set; }
}
```

### 🧪 **Usage**

```csharp
var order = new Order { OrderType = "Express" };

IShippingCostStrategy strategy = order.OrderType == "Standard"
    ? new StandardShipping()
    : new ExpressShipping();

var shippingService = new ShippingService(strategy);
Console.WriteLine($"Shipping Cost: {shippingService.Calculate(order)}");
```

---

### 💼 **Real-World Use Cases**

* Different payment methods: Credit card, PayPal, UPI, etc.
* Sorting algorithms: QuickSort, MergeSort, BubbleSort, etc.
* Data compression strategies: ZIP, RAR, TAR
* Image filters: Sepia, Grayscale, Contrast, etc.
* Logging strategies: FileLogger, DatabaseLogger, EventViewerLogger

---

### 🧹 **Benefits**

* Avoids large conditionals or `switch` statements.
* Complies with **Open/Closed Principle** and **Single Responsibility Principle**.
* Algorithms can be unit tested independently.
* Makes it easy to add new behaviors without modifying existing code.

---

### ⚠️ **Drawbacks**

* Increases the number of classes.
* Client code must understand the different strategies to select appropriately.
* Might be overkill if you have only one behavior that rarely changes.

---

### 🔁 **Related Patterns**

* **State Pattern**: Similar in structure but used for state transitions.
* **Decorator Pattern**: Wraps behavior dynamically instead of switching.
* **Command Pattern**: Encapsulates a request as an object, similar to strategies encapsulating behaviors.

---

### ✅ Summary Table

| Aspect            | Strategy Pattern                          |
| ----------------- | ----------------------------------------- |
| Pattern Category  | Behavioral                                |
| Purpose           | Select algorithm at runtime               |
| Follows Principle | Open/Closed, Single Responsibility        |
| Replaces          | Complex conditionals or switch statements |
| Example Use Case  | Shipping, Payment, Sorting, Compression   |
| Related Patterns  | State, Decorator, Command                 |


# ✅ Strategy Pattern vs Factory Pattern

## 1. 🎯 **What is Strategy Design Pattern?**

### ➤ **Definition:**

The **Strategy Pattern** defines a family of algorithms, encapsulates each one, and makes them interchangeable. It lets the algorithm vary independently from clients that use it.

### ➤ **Category:**

**Behavioral Design Pattern** – because it changes **behavior** of a class at runtime by injecting different algorithms.

### ➤ **Key Concepts:**

* Encapsulate algorithms/behaviors.
* Delegate responsibility to an interface.
* Make behavior swappable at runtime.

---

### ✅ **Use Case:**

Let’s say we want to **calculate shipping cost** differently based on shipping method: `Standard`, `Express`, or `Overnight`.

### Example (in C#):

```csharp
// Step 1: Define the strategy interface
public interface IShippingStrategy
{
    decimal CalculateShippingCost(Order order);
}

// Step 2: Implement concrete strategies
public class StandardShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(Order order) => 5.00m;
}

public class ExpressShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(Order order) => 15.00m;
}

public class OvernightShipping : IShippingStrategy
{
    public decimal CalculateShippingCost(Order order) => 25.00m;
}

// Step 3: Context class
public class ShippingContext
{
    private IShippingStrategy _strategy;

    public ShippingContext(IShippingStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IShippingStrategy strategy) => _strategy = strategy;

    public decimal GetShippingCost(Order order) => _strategy.CalculateShippingCost(order);
}
```

### 🔍 Example Usage:

```csharp
var order = new Order(); // fill order info
ShippingContext context = new ShippingContext(new ExpressShipping());
decimal cost = context.GetShippingCost(order);
```

---

## 2. 🏭 **What is Factory Design Pattern?**

### ➤ **Definition:**

The **Factory Pattern** defines an interface for creating an object but lets subclasses alter the type of objects that will be created.

### ➤ **Category:**

**Creational Design Pattern** – because it focuses on **object creation logic**.

---

### ✅ **Use Case:**

When you need to **create objects based on input/config without exposing creation logic** to the client.

Let’s say you want to create `ShippingStrategy` objects based on the order type.

### Example (in C#):

```csharp
// Factory class
public class ShippingStrategyFactory
{
    public static IShippingStrategy GetStrategy(string type)
    {
        return type switch
        {
            "Standard" => new StandardShipping(),
            "Express" => new ExpressShipping(),
            "Overnight" => new OvernightShipping(),
            _ => throw new ArgumentException("Invalid shipping type")
        };
    }
}
```

### 🔍 Example Usage:

```csharp
var strategy = ShippingStrategyFactory.GetStrategy(order.OrderType);
ShippingContext context = new ShippingContext(strategy);
decimal cost = context.GetShippingCost(order);
```

---

## 🔍 Side-by-Side Comparison

| Feature                   | **Strategy Pattern**                                  | **Factory Pattern**                                |
| ------------------------- | ----------------------------------------------------- | -------------------------------------------------- |
| **Category**              | Behavioral                                            | Creational                                         |
| **Purpose**               | Encapsulate interchangeable behavior/algorithms       | Encapsulate object creation logic                  |
| **Focus On**              | How to perform an operation                           | What object to create                              |
| **Client Responsibility** | Client sets/injects strategy                          | Client calls factory to get the object             |
| **Extensibility**         | Easy to add new algorithms without changing context   | Easy to add new types without changing client code |
| **Design Principle**      | Open/Closed, Dependency Inversion                     | Open/Closed, Single Responsibility                 |
| **Example Real-World**    | Different sorting algorithms, different payment modes | Database connector factory, Logger factory         |

---

## ✅ When to Use Which?

### Use **Strategy Pattern**:

* When you want to **change behavior at runtime**.
* When different classes implement **the same operation differently** (e.g., sorting, calculation, compression).
* When using **polymorphism to reduce conditionals** in logic.

### Use **Factory Pattern**:

* When you have a **complex object creation logic**.
* When object construction should be **abstracted from the client**.
* When you want to **hide object creation dependencies**.
* When **object type is decided at runtime** based on input/config.

---

## 🧠 Real-World Analogy:

| Concept  | Analogy Example                                     |
| -------- | --------------------------------------------------- |
| Strategy | Google Maps route strategies – shortest, fastest    |
| Factory  | Car factory builds different car types (SUV, Sedan) |

---

## ✅ Bonus: Using **Strategy + Factory Together**

In large-scale systems, it’s common to use both patterns **together**:

```csharp
// Get the right strategy (Factory)
IShippingStrategy strategy = ShippingStrategyFactory.GetStrategy(order.OrderType);

// Use the behavior (Strategy)
ShippingContext context = new ShippingContext(strategy);
var cost = context.GetShippingCost(order);
```

This gives you:

* ✅ Decoupled object creation (Factory)
* ✅ Interchangeable runtime behavior (Strategy)

---

## 🔚 Summary

| Pattern  | Category   | Use For                            |
| -------- | ---------- | ---------------------------------- |
| Strategy | Behavioral | Vary behavior/algorithm at runtime |
| Factory  | Creational | Abstract object creation           |

Both follow **SOLID principles**, especially:

* **Open/Closed Principle** (new types/algorithms without changing existing code)
* **Single Responsibility Principle** (each class does only one thing)
