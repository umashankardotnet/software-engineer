# Software Development Best Practices and Principles in C#/.NET

## Code Structure and Organization Principles

### DRY (Don't Repeat Yourself)
**Explanation**: Every piece of knowledge should have a single, unambiguous representation within a system.

**Example**: Instead of duplicating validation logic across multiple forms, create a reusable validator.
```csharp
// Bad (violates DRY)
if (!Regex.IsMatch(form1Email, @"^[^@]+@[^@]+\.[^@]+$")) { /* show error */ }
if (!Regex.IsMatch(form2Email, @"^[^@]+@[^@]+\.[^@]+$")) { /* show error */ }

// Good (follows DRY)
public static class EmailValidator
{
    public static bool IsValid(string email)
    {
        return Regex.IsMatch(email, @"^[^@]+@[^@]+\.[^@]+$");
    }
}

// Use in multiple places
if (!EmailValidator.IsValid(form1Email)) { /* show error */ }
if (!EmailValidator.IsValid(form2Email)) { /* show error */ }
```

### KISS (Keep It Simple, Stupid)
**Explanation**: Simplicity should be a key goal in design, and unnecessary complexity should be avoided.

**Example**: Using straightforward LINQ instead of overly complex chains when the operation is simple.
```csharp
// Overly complex
var result = users
    .Where(u => u.IsActive)
    .Select(u => u.Name)
    .Aggregate(0, (total, name) => total + name.Length);

// Simpler and more readable
int totalLength = 0;
foreach (var user in users)
{
    if (user.IsActive)
    {
        totalLength += user.Name.Length;
    }
}
```

### SoC (Separation of Concerns)
**Explanation**: Different aspects of a program should be handled by separate, independent modules.

**Example**: In an ASP.NET Core application, separate data access, business logic, and presentation:
```csharp
// Data access layer
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}

// Business logic layer
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> GetUserDetailsAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        // Apply business rules
        return new UserDto 
        { 
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}

// Presentation layer (ASP.NET Core controller)
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserDetailsAsync(id);
        return Ok(user);
    }
}
```

## Design Principles

### SOLID Principles
A collection of five design principles for creating maintainable and extensible software:

#### 1. Single Responsibility Principle (SRP)
**Explanation**: A class should have only one reason to change.

**Example**: Split a `User` class that handles both authentication and profile management into separate classes.
```csharp
// Bad (violates SRP)
public class User
{
    public void SaveProfile() { /* save profile data */ }
    public bool Authenticate(string password) { /* handle authentication */ }
    public Report GenerateReport() { /* create user reports */ }
}

// Good (follows SRP)
public class UserProfile
{
    public void Save() { /* save profile data */ }
}

public class UserAuthenticator
{
    public bool Authenticate(string username, string password) { /* handle auth */ }
}

public class UserReportGenerator
{
    public Report Generate() { /* create user reports */ }
}
```

#### 2. Open/Closed Principle (OCP)
**Explanation**: Software entities should be open for extension but closed for modification.

**Example**: Using inheritance or interfaces to extend functionality without changing existing code.
```csharp
// Base class closed for modification
public abstract class Shape
{
    public abstract double Area();
}

// Extension without modifying Shape
public class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    public override double Area()
    {
        return Width * Height;
    }
}

// Another extension
public class Circle : Shape
{
    public double Radius { get; }
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}

// Usage without modifying original classes
public class AreaCalculator
{
    public double TotalArea(IEnumerable<Shape> shapes)
    {
        double total = 0;
        foreach (var shape in shapes)
        {
            total += shape.Area();
        }
        return total;
    }
}
```

#### 3. Liskov Substitution Principle (LSP)
**Explanation**: Objects should be replaceable with instances of their subtypes without altering program correctness.

**Example**: Ensuring that derived classes can be used in place of their base classes.
```csharp
// Base class
public abstract class Bird
{
    public abstract void Eat();
}

// LSP violation (not all birds can fly)
public class Ostrich : Bird
{
    public override void Eat() { /* implementation */ }
    
    public void Fly()
    {
        throw new NotSupportedException("Ostriches cannot fly");
    }
}

// Better design
public abstract class Bird
{
    public abstract void Eat();
}

public abstract class FlyingBird : Bird
{
    public abstract void Fly();
}

public class Sparrow : FlyingBird
{
    public override void Eat() { /* implementation */ }
    public override void Fly() { /* implementation */ }
}

public class Ostrich : Bird
{
    public override void Eat() { /* implementation */ }
    // No fly method to violate
}
```

#### 4. Interface Segregation Principle (ISP)
**Explanation**: Many client-specific interfaces are better than one general-purpose interface.

**Example**: Breaking down large interfaces into smaller, more specific ones.
```csharp
// Bad (violates ISP)
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

// Good (follows ISP)
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

// Classes can implement only what they need
public class Human : IWorkable, IEatable, ISleepable
{
    public void Work() { /* implementation */ }
    public void Eat() { /* implementation */ }
    public void Sleep() { /* implementation */ }
}

public class Robot : IWorkable
{
    public void Work() { /* implementation */ }
    // No need to implement unnecessary methods
}
```

#### 5. Dependency Inversion Principle (DIP)
**Explanation**: Depend on abstractions, not concretions.

**Example**: Using interfaces and dependency injection to decouple high-level and low-level modules.
```csharp
// Bad (high-level module depends on low-level)
public class ReportGenerator
{
    private readonly SqlServerDatabase _database; // Concrete dependency
    
    public ReportGenerator()
    {
        _database = new SqlServerDatabase();
    }
    
    public void GenerateReport()
    {
        var data = _database.Query("SELECT * FROM data");
        // Process data
    }
}

// Good (depends on abstraction)
public interface IDatabase
{
    IEnumerable<dynamic> Query(string sql);
}

public class SqlServerDatabase : IDatabase
{
    public IEnumerable<dynamic> Query(string sql)
    {
        // Implementation
        return new List<dynamic>();
    }
}

public class ReportGenerator
{
    private readonly IDatabase _database; // Abstract dependency
    
    // Constructor injection (common in .NET with DI container)
    public ReportGenerator(IDatabase database)
    {
        _database = database;
    }
    
    public void GenerateReport()
    {
        var data = _database.Query("SELECT * FROM data");
        // Process data
    }
}

// In ASP.NET Core Startup.cs or Program.cs
services.AddScoped<IDatabase, SqlServerDatabase>();
services.AddScoped<ReportGenerator>();
```

### LoD (Law of Demeter) or Principle of Least Knowledge
**Explanation**: An object should only communicate with its immediate friends and not with "strangers."

**Example**: Avoiding chained method calls that navigate through multiple objects.
```csharp
// Violates Law of Demeter
customer.GetWallet().GetCard().Charge(amount);

// Follows Law of Demeter
customer.ChargeAmount(amount);

// Inside Customer class
public void ChargeAmount(decimal amount)
{
    _wallet.ChargeAmount(amount);
}

// Inside Wallet class
public void ChargeAmount(decimal amount)
{
    _card.Charge(amount);
}
```

### Composition Over Inheritance
**Explanation**: Favor object composition over class inheritance when designing reusable functionality.

**Example**: Using composition to create flexible object relationships.
```csharp
// Inheritance approach
public abstract class Animal
{
    public virtual void Eat() { /* implementation */ }
    public virtual void Sleep() { /* implementation */ }
}

public class Bird : Animal
{
    public virtual void Fly() { /* implementation */ }
}

// Composition approach
public class EatingBehavior
{
    public void Eat() { /* implementation */ }
}

public class SleepingBehavior
{
    public void Sleep() { /* implementation */ }
}

public class FlyingBehavior
{
    public void Fly() { /* implementation */ }
}

public class Bird
{
    private readonly EatingBehavior _eatingBehavior = new();
    private readonly SleepingBehavior _sleepingBehavior = new();
    private readonly FlyingBehavior _flyingBehavior = new();
    
    public void Eat()
    {
        _eatingBehavior.Eat();
    }
    
    public void Sleep()
    {
        _sleepingBehavior.Sleep();
    }
    
    public void Fly()
    {
        _flyingBehavior.Fly();
    }
}
```

## Development Methodologies

### TDD (Test-Driven Development)
**Explanation**: Write tests before writing the actual code, following the red-green-refactor cycle.

**Example** using xUnit:
1. Write a failing test:
```csharp
// CircleTests.cs
public class CircleTests
{
    [Fact]
    public void CalculateArea_WithRadius5_Returns78Point54()
    {
        // Arrange
        var circle = new Circle(5);
        
        // Act
        var area = circle.Area();
        
        // Assert
        Assert.Equal(78.54, area, 2); // Precision to 2 decimal places
    }
}
```

2. Write code to make the test pass:
```csharp
// Circle.cs
public class Circle
{
    private readonly double _radius;
    
    public Circle(double radius)
    {
        _radius = radius;
    }
    
    public double Area()
    {
        return Math.PI * _radius * _radius;
    }
}
```

3. Refactor while ensuring tests still pass:
```csharp
// Circle.cs after refactoring
public class Circle : IShape
{
    private readonly double _radius;
    
    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Radius must be positive", nameof(radius));
            
        _radius = radius;
    }
    
    public double Area() => Math.PI * Math.Pow(_radius, 2);
}
```

### BDD (Behavior-Driven Development)
**Explanation**: An extension of TDD that emphasizes collaboration and focuses on the behavior of the system.

**Example**: Using SpecFlow (BDD framework for .NET):
```gherkin
# Login.feature
Feature: User authentication
  
  Scenario: Successful login
    Given a user exists with username "john" and password "secret"
    When the user attempts to login with username "john" and password "secret"
    Then the user should be authenticated successfully
    And they should be redirected to the dashboard
```

```csharp
// LoginSteps.cs
[Binding]
public class LoginSteps
{
    private readonly User _user;
    private readonly AuthenticationService _authService;
    private bool _authResult;
    
    public LoginSteps()
    {
        _authService = new AuthenticationService();
    }
    
    [Given(@"a user exists with username ""(.*)"" and password ""(.*)""")]
    public void GivenAUserExistsWithUsernameAndPassword(string username, string password)
    {
        _user = new User(username, password);
        _authService.Register(_user);
    }
    
    [When(@"the user attempts to login with username ""(.*)"" and password ""(.*)""")]
    public void WhenTheUserAttemptsToLoginWithUsernameAndPassword(string username, string password)
    {
        _authResult = _authService.Authenticate(username, password);
    }
    
    [Then(@"the user should be authenticated successfully")]
    public void ThenTheUserShouldBeAuthenticatedSuccessfully()
    {
        Assert.True(_authResult);
    }
    
    [Then(@"they should be redirected to the dashboard")]
    public void ThenTheyShouldBeRedirectedToTheDashboard()
    {
        Assert.Equal("/dashboard", _authService.GetRedirectUrl());
    }
}
```

### DBC (Design By Contract)
**Explanation**: Software components should have clear specifications about preconditions, postconditions, and invariants.

**Example** using Code Contracts or manual checks:
```csharp
/// <summary>
/// Divides two numbers.
/// </summary>
/// <param name="dividend">The number to be divided</param>
/// <param name="divisor">The number to divide by</param>
/// <returns>The quotient</returns>
/// <exception cref="ArgumentException">Thrown when divisor is zero</exception>
/// 
/// Precondition: divisor != 0
/// Postcondition: result * divisor = dividend (approximately)
public double Divide(double dividend, double divisor)
{
    // Check precondition
    if (divisor == 0)
    {
        throw new ArgumentException("Divisor cannot be zero", nameof(divisor));
    }
    
    // Perform operation
    double result = dividend / divisor;
    
    // Could check postcondition in debug mode
    Debug.Assert(Math.Abs(result * divisor - dividend) < 0.0001);
    
    return result;
}
```

## Practical Coding Guidelines

### YAGNI (You Aren't Gonna Need It)
**Explanation**: Don't add functionality until it's necessary.

**Example**: Avoiding premature optimization or features that aren't immediately needed.
```csharp
// Violating YAGNI
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Dictionary<string, string> Preferences { get; } = new(); // Not needed yet
    public List<SocialAccount> SocialAccounts { get; } = new(); // Not needed yet
    public List<PaymentMethod> PaymentMethods { get; } = new(); // Not needed yet
    
    // Methods for features we don't need yet
    public void LinkSocialAccount(SocialAccount account) { /* ... */ }
    public void AddPaymentMethod(PaymentMethod method) { /* ... */ }
}

// Following YAGNI
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    // Only implement what's currently needed
}
```

### Fail Fast
**Explanation**: Systems should report failures as soon as they're detected rather than proceeding with uncertain data.

**Example**: Validating inputs early and throwing exceptions immediately.
```csharp
public void ProcessOrder(Order order)
{
    // Fail fast with clear error messages
    if (order == null)
    {
        throw new ArgumentNullException(nameof(order));
    }
    
    if (order.Items == null || !order.Items.Any())
    {
        throw new ArgumentException("Order must contain at least one item", nameof(order));
    }
    
    if (order.Customer == null)
    {
        throw new ArgumentException("Order must have a customer", nameof(order));
    }
    
    // Process the order knowing inputs are valid
    // ...
}
```

### Boy Scout Rule
**Explanation**: "Leave the code cleaner than you found it." Make small improvements whenever you work with existing code.

**Example**: Refactoring while fixing a bug or adding a feature.
```csharp
// Before: Found while fixing a bug
public void SaveUser(User user)
{
    // Check if user exists
    bool exists = false;
    for (int i = 0; i < _users.Count; i++)
    {
        if (_users[i].Id == user.Id)
        {
            exists = true;
            break;
        }
    }
    
    if (!exists)
    {
        _users.Add(user);
        Console.WriteLine("User added");
    }
    else
    {
        // Update user
        for (int i = 0; i < _users.Count; i++)
        {
            if (_users[i].Id == user.Id)
            {
                _users[i] = user;
                Console.WriteLine("User updated");
            }
        }
    }
}

// After: Fixed bug and improved code (Boy Scout Rule)
public void SaveUser(User user)
{
    if (user == null)
    {
        throw new ArgumentNullException(nameof(user));
    }
    
    int index = FindUserIndex(user.Id);
    if (index == -1)
    {
        _users.Add(user);
        _logger.LogInformation($"User added: {user.Id}");
    }
    else
    {
        _users[index] = user;
        _logger.LogInformation($"User updated: {user.Id}");
    }
}

private int FindUserIndex(Guid userId)
{
    for (int i = 0; i < _users.Count; i++)
    {
        if (_users[i].Id == userId)
        {
            return i;
        }
    }
    return -1;
}
```

### Clean Code
**Explanation**: Code should be readable, meaningful, and maintainable with proper naming, small functions, and minimal comments.

**Example**: Improving code readability.
```csharp
// Before: Not clean
public List<int[]> GetThem()
{
    List<int[]> list1 = new List<int[]>();
    for (int i = 0; i < theList.Count; i++)
    {
        if (theList[i][0] == 4)
        {
            list1.Add(theList[i]);
        }
    }
    return list1;
}

// After: Clean code with meaningful names
public List<Cell> GetFlaggedCells()
{
    var flaggedCells = new List<Cell>();
    foreach (var cell in _gameBoard)
    {
        if (cell.IsFlagged)
        {
            flaggedCells.Add(cell);
        }
    }
    return flaggedCells;
}
```

## Architectural Patterns

### DDD (Domain-Driven Design)
**Explanation**: Focus on the core domain and domain logic, with complex designs implemented based on a model of the domain.

**Example**: Organizing code around business concepts.
```csharp
// Domain model with rich behavior
public class Order
{
    private readonly List<OrderLine> _orderLines = new();
    
    public OrderId Id { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();
    
    public Order(CustomerId customerId)
    {
        Id = new OrderId(Guid.NewGuid());
        CustomerId = customerId;
        Status = OrderStatus.Draft;
    }
    
    public void AddProduct(Product product, int quantity)
    {
        if (Status != OrderStatus.Draft)
        {
            throw new OrderAlreadyConfirmedException();
        }
        
        var line = FindOrderLineForProduct(product);
        if (line != null)
        {
            line.IncreaseQuantity(quantity);
        }
        else
        {
            _orderLines.Add(new OrderLine(product, quantity));
        }
    }
    
    public Money CalculateTotal()
    {
        return _orderLines.Aggregate(Money.Zero, (total, line) => total + line.LineTotal);
    }
    
    public void Confirm()
    {
        if (!_orderLines.Any())
        {
            throw new EmptyOrderException();
        }
        
        Status = OrderStatus.Confirmed;
        // Raise domain event
        DomainEvents.Raise(new OrderConfirmedEvent(Id));
    }
    
    private OrderLine FindOrderLineForProduct(Product product)
    {
        return _orderLines.FirstOrDefault(ol => ol.ProductId == product.Id);
    }
}
```

### CQRS (Command Query Responsibility Segregation)
**Explanation**: Separates read and update operations for a data store.

**Example**: Separate models for reading and writing.
```csharp
// Command side (write model)
public class OrderCommandService
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    
    public OrderCommandService(IOrderRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }
    
    public async Task<Guid> CreateOrderAsync(CreateOrderCommand cmd)
    {
        var order = new Order(new CustomerId(cmd.CustomerId));
        await _repository.SaveAsync(order);
        return order.Id.Value;
    }
    
    public async Task AddProductToOrderAsync(AddProductCommand cmd)
    {
        var order = await _repository.FindByIdAsync(new OrderId(cmd.OrderId));
        var product = await _productRepository.FindByIdAsync(new ProductId(cmd.ProductId));
        
        order.AddProduct(product, cmd.Quantity);
        await _repository.SaveAsync(order);
    }
}

// Query side (read model)
public class OrderQueryService
{
    private readonly IDbConnection _connection;
    
    public OrderQueryService(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<OrderSummaryDto> GetOrderSummaryAsync(Guid orderId)
    {
        const string sql = @"
            SELECT o.Id, o.Status, c.Name AS CustomerName,
                   COUNT(ol.Id) AS ItemCount, SUM(ol.Price * ol.Quantity) AS Total
            FROM Orders o
            JOIN Customers c ON o.CustomerId = c.Id
            JOIN OrderLines ol ON o.Id = ol.OrderId
            WHERE o.Id = @OrderId
            GROUP BY o.Id, o.Status, c.Name";
            
        return await _connection.QuerySingleOrDefaultAsync<OrderSummaryDto>(
            sql, new { OrderId = orderId });
    }
}
```

## Process and Workflow Principles

### CI/CD (Continuous Integration/Continuous Deployment)
**Explanation**: Regularly integrate code changes into a shared repository with automated testing and deployment.

**Example**: A CI/CD pipeline configuration using Azure DevOps:
```yaml
# azure-pipelines.yml
trigger:
- main

pool:
  vmImage: 'windows-latest'

variables:
  buildConfiguration: 'Release'
  solution: '**/*.sln'
  
steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: DotNetCoreCLI@2
  displayName: 'Build'
  inputs:
    command: 'build'
    projects: '$(solution)'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Run Tests'
  inputs:
    command: 'test'
    projects: '**/*Tests/*.csproj'
    arguments: '--configuration $(buildConfiguration) --collect "Code coverage"'

- task: DotNetCoreCLI@2
  displayName: 'Publish'
  inputs:
    command: 'publish'
    publishWebProjects: true
    arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: true

- task: PublishBuildArtifacts@1
  displayName: 'Publish Artifacts'
  inputs:
    pathtoPublish: '$(Build.ArtifactStagingDirectory)'
    artifactName: 'drop'
```

### GRASP (General Responsibility Assignment Software Patterns)
**Explanation**: Guidelines for assigning responsibilities to classes and objects in object-oriented design.

**Example**: Applying the Controller pattern in ASP.NET Core:
```csharp
// Following GRASP's Controller pattern
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    
    public OrdersController(
        IOrderService orderService,
        ICustomerService customerService,
        IProductService productService)
    {
        _orderService = orderService;
        _customerService = customerService;
        _productService = productService;
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder(CreateOrderRequest request)
    {
        // Controller coordinates the use case but delegates actual work
        var customer = await _customerService.FindByIdAsync(request.CustomerId);
        if (customer == null)
        {
            return NotFound("Customer not found");
        }
        
        var order = await _orderService.CreateOrderAsync(customer);
        
        foreach (var itemRequest in request.Items)
        {
            var product = await _productService.FindByIdAsync(itemRequest.ProductId);
            if (product == null)
            {
                return NotFound($"Product {itemRequest.ProductId} not found");
            }
            
            await _orderService.AddProductToOrderAsync(order, product, itemRequest.Quantity);
        }
        
        await _orderService.ConfirmOrderAsync(order.Id);
        
        // Return response
        return new CreateOrderResponse { OrderId = order.Id };
    }
}
```

## .NET Specific Best Practices

### Use C# Features Effectively
**Explanation**: Leverage modern C# features to write cleaner, more concise code.

## Applying These Principles Together

The most effective software development comes from understanding when and how to apply these principles in combination. For example:

1. Use **DDD** to model your domain with clear boundaries
2. Apply **SOLID** principles when designing classes within those boundaries
3. Follow **TDD** to ensure your implementation meets requirements
4. Use **Clean Code** practices to make your code readable and maintainable
5. Apply the **Boy Scout Rule** to gradually improve legacy code
6. Remember **YAGNI** to avoid over-engineering
7. Implement **CI/CD** to ensure quality and rapid delivery

These principles are not rigid rules but guidelines that should be applied thoughtfully based on the specific context of your project.
