# What is Clean Architecture?

**Clean Architecture** is a software design philosophy introduced by **Robert C. Martin (Uncle Bob)**. It organizes code in a way that separates concerns, promotes maintainability, and ensures that the core business logic is independent of external factors like frameworks, databases, or UI. It emphasizes creating a system that is easy to test, extend, and adapt to future changes.

---

### Key Principles of Clean Architecture

1. **Independence**:
   - **Framework Independence**: The architecture does not depend on any specific framework. Frameworks can be easily replaced.
   - **UI Independence**: The business logic does not depend on UI components, so you can change the UI without affecting the core logic.
   - **Database Independence**: The system's logic does not depend on the database, so you can switch databases or data storage mechanisms easily.
   - **Testability**: The system is designed to facilitate unit testing of individual components.

2. **Separation of Concerns**:
   - Different layers have distinct responsibilities and communicate through well-defined boundaries.

3. **Dependency Rule**:
   - Dependencies point inward, toward the core of the application. The inner layers do not know anything about the outer layers.

---

### Layers of Clean Architecture

Clean Architecture is typically represented as concentric circles, where the **core business logic** is at the center, and the outer layers handle implementation details.

#### 1. **Entities (Core Business Logic)**:
   - **Purpose**: Represents the core business rules and domain logic.
   - **Characteristics**:
     - Independent of frameworks, databases, and UI.
     - Contains enterprise-wide business rules.
   - **Example**: Classes representing core concepts like `Order`, `User`, or `Invoice`.

#### 2. **Use Cases (Application Logic)**:
   - **Purpose**: Contains specific application logic to coordinate user interactions with the core domain (entities).
   - **Characteristics**:
     - Orchestrates the flow of data between entities and outer layers.
     - Ensures the system behaves correctly for a given scenario.
   - **Example**: Use case classes like `PlaceOrder`, `GenerateInvoice`, or `AuthenticateUser`.

#### 3. **Interface Adapters (Presentation/Adapters)**:
   - **Purpose**: Translates data between the core application and external systems (e.g., UI, database).
   - **Characteristics**:
     - Adapts data formats to match what the use cases or entities require.
     - Can include controllers, view models, or serializers.
   - **Example**: REST controllers, mappers, or data transfer objects (DTOs).

#### 4. **Frameworks and Drivers (Infrastructure)**:
   - **Purpose**: Implements external tools and technologies such as web frameworks, databases, or APIs.
   - **Characteristics**:
     - Handles infrastructure-level details.
     - Should be easily replaceable without affecting core layers.
   - **Example**: Flask/Django, PostgreSQL, AWS SDKs.

---

### The Dependency Rule

The **dependency rule** states that:
- All dependencies should point inward, toward the higher-level policy (core business logic).
- Outer layers can depend on inner layers, but inner layers must never depend on outer layers.

This ensures that the **core logic** is isolated and not affected by changes in external systems.

---

### Benefits of Clean Architecture

1. **Testability**:
   - Core business rules can be tested independently of UI, databases, or frameworks.

2. **Maintainability**:
   - Clear separation of concerns makes it easier to modify and extend the system.

3. **Scalability**:
   - New features can be added without breaking existing functionality.

4. **Framework Independence**:
   - You can switch frameworks or tools with minimal effort.

5. **Portability**:
   - Core logic can be reused across different platforms (e.g., web, mobile, desktop).

---

### Example of Clean Architecture (E-commerce Application)

Here's an example of implementing **Clean Architecture** in a .NET application. We'll structure it for an e-commerce application where users can place orders.

---

### Project Structure for Clean Architecture in .NET

The project is divided into separate layers, each represented as a separate project in the solution:

1. **Core**:
   - Contains the **Entities** and **Use Cases**.
   - Example: `ECommerce.Core`.

2. **Application**:
   - Contains **Interfaces** and **DTOs** to interact with the use cases.
   - Example: `ECommerce.Application`.

3. **Infrastructure**:
   - Implements external dependencies (e.g., database, third-party services).
   - Example: `ECommerce.Infrastructure`.

4. **Presentation**:
   - Handles the **Web API**, **Controllers**, or **UI**.
   - Example: `ECommerce.Web`.

---

### Example Code: Clean Architecture in .NET

#### **1. Core (Entities and Use Cases)**

**Entities (Domain Layer)**:
```csharp
namespace ECommerce.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; private set; }

        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(item => item.Price * item.Quantity);
        }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
```

**Use Case (Application Logic)**:
```csharp
namespace ECommerce.Core.UseCases
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public class PlaceOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Execute(Order order)
        {
            order.CalculateTotal();
            _orderRepository.Save(order);
        }
    }
}
```

---

#### **2. Application (Interface Adapters)**

**DTOs**:
```csharp
namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
```

**Adapters**:
```csharp
namespace ECommerce.Application.Adapters
{
    public static class OrderAdapter
    {
        public static Order ToEntity(OrderDto dto)
        {
            return new Order
            {
                Items = dto.Items.Select(item => new OrderItem
                {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}
```

---

#### **3. Infrastructure (Database Implementation)**

**Order Repository**:
```csharp
using ECommerce.Core.Entities;
using ECommerce.Core.UseCases;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            // Simulate saving to database
            Console.WriteLine($"Order saved with total amount: {order.TotalAmount}");
        }
    }
}
```

---

#### **4. Presentation (Web Layer)**

**Controller**:
```csharp
using ECommerce.Application.Adapters;
using ECommerce.Application.DTOs;
using ECommerce.Core.Entities;
using ECommerce.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly PlaceOrderUseCase _placeOrderUseCase;

        public OrderController(PlaceOrderUseCase placeOrderUseCase)
        {
            _placeOrderUseCase = placeOrderUseCase;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderDto orderDto)
        {
            var order = OrderAdapter.ToEntity(orderDto);
            _placeOrderUseCase.Execute(order);

            return Ok(new { message = "Order placed successfully" });
        }
    }
}
```

---

### Setting Up the Application

1. **Register Services in Dependency Injection (DI)**:
   Configure DI in `Program.cs` or `Startup.cs`.

```csharp
using ECommerce.Core.UseCases;
using ECommerce.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<PlaceOrderUseCase>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

2. **Test the API**:
   Run the application and send a POST request to `/api/orders` with a JSON payload:
```json
{
  "items": [
    { "productName": "Book", "price": 10.5, "quantity": 2 },
    { "productName": "Pen", "price": 1.2, "quantity": 5 }
  ]
}
```

---

### Benefits of This Implementation
1. **Separation of Concerns**:
   - The core logic is independent of frameworks, ensuring high maintainability.
   
2. **Testability**:
   - Use cases and entities can be unit tested without dependencies on infrastructure.

3. **Scalability**:
   - Adding new features or changing the database requires minimal changes to the codebase.

This example demonstrates how Clean Architecture principles can be applied in a .NET application. By following these practices, the system remains modular, testable, and adaptable to future changes.

### When to Use Clean Architecture

- **Complex Applications**:
  - Applications with multiple layers or a high level of business logic.
- **Scalable Systems**:
  - Systems that require frequent updates or additions.
- **Cross-Platform Development**:
  - Applications that need to share the same core logic across different platforms.
- **Long-Term Projects**:
  - Projects expected to evolve significantly over time.

By following the principles of Clean Architecture, you can build software systems that are flexible, maintainable, and robust.
