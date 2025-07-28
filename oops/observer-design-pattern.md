## ✅ **Observer Design Pattern in .NET – Full Guide**

---

### 🔷 What is the Observer Design Pattern?

The **Observer Pattern** is a **behavioral design pattern** where an object, known as the **Subject**, maintains a list of dependents, called **Observers**, and notifies them automatically of any state changes.

> 💡 **Key Idea**: "When one object changes state, all its dependents are notified and updated automatically."

---

### 🔷 Real-World Analogy

* A **YouTube Channel** is a `Subject`.
* Subscribers are `Observers`.
* When the channel uploads a new video (state change), all subscribers (observers) are notified.

---

### 🔷 Components of Observer Pattern

| Component            | Description                                                           |
| -------------------- | --------------------------------------------------------------------- |
| **Subject**          | Maintains a list of observers and notifies them of any state changes. |
| **Observer**         | Defines an interface for receiving updates from the Subject.          |
| **ConcreteSubject**  | The object being observed.                                            |
| **ConcreteObserver** | Objects that get notified when the subject changes.                   |

---

### 🔷 Use Cases in .NET Projects

| Use Case                      | Explanation                                                             |
| ----------------------------- | ----------------------------------------------------------------------- |
| **Event notification system** | Notify multiple components about a change (e.g., order status updates). |
| **UI frameworks (MVVM)**      | Notify views when the model changes.                                    |
| **Logging or monitoring**     | Notify multiple loggers of changes/events.                              |
| **Stock ticker app**          | Update clients in real time when stock prices change.                   |
| **File watchers**             | Notify systems when files are modified.                                 |

---

### 🔧 **Implementation in C# (.NET)**

#### Step 1: Define the `IObserver` interface

```csharp
public interface IObserver
{
    void Update(string message);
}
```

#### Step 2: Define the `ISubject` interface

```csharp
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(string message);
}
```

#### Step 3: Implement the `ConcreteSubject`

```csharp
public class NewsAgency : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver observer) => observers.Add(observer);
    public void Detach(IObserver observer) => observers.Remove(observer);

    public void Notify(string news)
    {
        foreach (var observer in observers)
        {
            observer.Update(news);
        }
    }

    public void PublishNews(string news)
    {
        Console.WriteLine($"NewsAgency: Publishing news -> {news}");
        Notify(news);
    }
}
```

#### Step 4: Implement the `ConcreteObserver`

```csharp
public class NewsSubscriber : IObserver
{
    private string _name;

    public NewsSubscriber(string name)
    {
        _name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{_name} received news: {message}");
    }
}
```

#### Step 5: Client Code – Usage Example

```csharp
class Program
{
    static void Main(string[] args)
    {
        NewsAgency agency = new NewsAgency();

        var subscriber1 = new NewsSubscriber("Alice");
        var subscriber2 = new NewsSubscriber("Bob");

        agency.Attach(subscriber1);
        agency.Attach(subscriber2);

        agency.PublishNews("Breaking: Market hits all-time high!");

        agency.Detach(subscriber1);

        agency.PublishNews("Update: Market closed with gains.");
    }
}
```

---

### 🔷 Output:

```
NewsAgency: Publishing news -> Breaking: Market hits all-time high!
Alice received news: Breaking: Market hits all-time high!
Bob received news: Breaking: Market hits all-time high!
NewsAgency: Publishing news -> Update: Market closed with gains.
Bob received news: Update: Market closed with gains.
```

---

### 🔷 Alternative: Using .NET Events/Delegates (Built-in Observer Pattern)

.NET has **events and delegates** which follow the Observer pattern internally.

```csharp
public class TemperatureSensor
{
    public delegate void TemperatureChangedHandler(double newTemp);
    public event TemperatureChangedHandler OnTemperatureChanged;

    public void SetTemperature(double temp)
    {
        Console.WriteLine($"Sensor: Temperature is now {temp}");
        OnTemperatureChanged?.Invoke(temp);
    }
}

public class Display
{
    public void Subscribe(TemperatureSensor sensor)
    {
        sensor.OnTemperatureChanged += ShowTemperature;
    }

    public void ShowTemperature(double temp)
    {
        Console.WriteLine($"Display: Current Temperature is {temp}");
    }
}
```

---

### 🔷 When to Use the Observer Pattern?

| When                                                                      | Why                                                       |
| ------------------------------------------------------------------------- | --------------------------------------------------------- |
| When multiple objects need to respond to a state change in another object | Reduces tight coupling between them.                      |
| When implementing event-based or publish-subscribe systems                | Clean, extensible notification mechanism.                 |
| When you want to promote loose coupling                                   | Observers and subjects know very little about each other. |

---

### ✅ Summary

| Pattern Aspect          | Description                                                              |
| ----------------------- | ------------------------------------------------------------------------ |
| **Type**                | Behavioral                                                               |
| **Benefit**             | Decouples subject from observers, supports dynamic subscriptions         |
| **Implemented in .NET** | Via interfaces, or built-in events/delegates                             |
| **Real Examples**       | `INotifyPropertyChanged` in WPF, Pub/Sub systems, logging, notifications |

Would you like a version using `IObservable<T>` and `IObserver<T>` interfaces in .NET reactive programming (Rx.NET)?
