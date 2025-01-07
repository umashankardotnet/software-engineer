## Low-Level Design Principles in .NET

### **SOLID Principles**

#### **1. Single Responsibility Principle (SRP)**
- **Definition**: A class should have only one reason to change.
- **Explanation**: Each class should focus on a single responsibility or task. If a class handles multiple concerns, changes to one concern might inadvertently affect the others.
- **Example in .NET**:
```csharp
public class InvoiceService
{
    public void GenerateInvoice() { /* Logic for generating an invoice */ }
}

public class EmailService
{
    public void SendEmail(string recipient, string message) { /* Logic for sending email */ }
}
// Separate responsibilities for invoice generation and email sending.
```

---

#### **2. Open/Closed Principle (OCP)**
- **Definition**: Classes should be open for extension but closed for modification.
- **Explanation**: Extend functionality by adding new code rather than altering existing code, preserving the existing functionality.
- **Example in .NET**:
```csharp
public interface IDiscount
{
    decimal ApplyDiscount(decimal totalAmount);
}

public class NoDiscount : IDiscount
{
    public decimal ApplyDiscount(decimal totalAmount) => totalAmount;
}

public class PercentageDiscount : IDiscount
{
    private readonly decimal _percentage;
    public PercentageDiscount(decimal percentage) { _percentage = percentage; }
    public decimal ApplyDiscount(decimal totalAmount) => totalAmount * (1 - _percentage);
}
// New discount strategies can be added without modifying existing classes.
```

---

#### **3. Liskov Substitution Principle (LSP)**
- **Definition**: Subtypes must be substitutable for their base types.
- **Explanation**: Derived classes should not violate the expectations set by the base class.
- **Example in .NET**:
```csharp
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
    public int Area => Width * Height;
}

public class Square : Rectangle
{
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
}
// Squares correctly extend the behavior of rectangles.
```

---

#### **4. Interface Segregation Principle (ISP)**
- **Definition**: Clients should not be forced to depend on interfaces they don’t use.
- **Explanation**: Split large interfaces into smaller, more specific ones tailored to client needs.
- **Example in .NET**:
```csharp
public interface IPrinter
{
    void PrintDocument();
}

public interface IScanner
{
    void ScanDocument();
}

public class MultifunctionPrinter : IPrinter, IScanner
{
    public void PrintDocument() { /* Print logic */ }
    public void ScanDocument() { /* Scan logic */ }
}

public class SimplePrinter : IPrinter
{
    public void PrintDocument() { /* Print logic */ }
}
// Clients can depend only on the interfaces they require.
```

---

#### **5. Dependency Inversion Principle (DIP)**
- **Definition**: Depend on abstractions, not on concrete implementations.
- **Explanation**: High-level modules should not depend on low-level modules; both should depend on abstractions.
- **Example in .NET**:
```csharp
public interface ILogger
{
    void Log(string message);
}

public class FileLogger : ILogger
{
    public void Log(string message) { /* Write to file */ }
}

public class OrderService
{
    private readonly ILogger _logger;
    public OrderService(ILogger logger) { _logger = logger; }
    public void ProcessOrder() { _logger.Log("Order processed."); }
}
// Dependency injection can be used to pass the logger implementation.
```

---

### **Other Key Principles**

#### **1. DRY (Don't Repeat Yourself)**
- **Definition**: Avoid code duplication by abstracting reusable logic.
- **Example in .NET**:
```csharp
public static class TaxCalculator
{
    public static decimal CalculateTax(decimal amount) => amount * 0.1m;
}

public class Invoice
{
    public decimal Amount { get; set; }
    public decimal Tax => TaxCalculator.CalculateTax(Amount);
}

public class Order
{
    public decimal Amount { get; set; }
    public decimal Tax => TaxCalculator.CalculateTax(Amount);
}
// Reuse tax calculation logic.
```

---

#### **2. KISS (Keep It Simple, Stupid)**
- **Definition**: Design should be simple and easy to understand.
- **Example in .NET**:
```csharp
// Complex
public int Calculate(int a, int b, bool multiply)
{
    return multiply ? a * b : a + b;
}

// Simple
public int Add(int a, int b) => a + b;
public int Multiply(int a, int b) => a * b;
```

---

#### **3. YAGNI (You Aren't Gonna Need It)**
- **Definition**: Don’t implement features until they are necessary.
- **Example in .NET**:
```csharp
// Avoid preemptively adding unused features.
public class UserService
{
    public void RegisterUser(string name, string email) { /* Registration logic */ }
}
```

---

#### **4. Separation of Concerns (SoC)**
- **Definition**: Divide the application into distinct features that overlap minimally.
- **Example in .NET**:
```csharp
// Presentation Layer
public class HomeController : Controller
{
    private readonly IProductService _service;
    public HomeController(IProductService service) { _service = service; }
    public IActionResult Index() => View(_service.GetProducts());
}

// Business Logic Layer
public interface IProductService
{
    IEnumerable<Product> GetProducts();
}

public class ProductService : IProductService
{
    public IEnumerable<Product> GetProducts() { /* Fetch products */ }
}

// Data Access Layer
public class ProductRepository
{
    public IEnumerable<Product> GetAll() { /* Database logic */ }
}
```

---

#### **5. Encapsulation**
- **Definition**: Limit the exposure of internal state and behavior.
- **Example in .NET**:
```csharp
public class BankAccount
{
    private decimal _balance;
    public decimal GetBalance() => _balance;
    public void Deposit(decimal amount) { _balance += amount; }
    public void Withdraw(decimal amount) { if (amount <= _balance) _balance -= amount; }
}
// Internal balance is hidden, and controlled methods are exposed.
```

---

#### **6. Law of Demeter (LoD)**
- **Definition**: A module should only interact with its immediate dependencies.
- **Example in .NET**:
```csharp
// Violation
public string GetCustomerZip(Order order) => order.Customer.Address.ZipCode;

// Following LoD
public string GetCustomerZip(Order order) => order.GetCustomerZipCode();

public class Order
{
    public Customer Customer { get; set; }
    public string GetCustomerZipCode() => Customer?.Address?.ZipCode;
}
```

---

By following these principles, you can create maintainable, scalable, and robust .NET applications.

