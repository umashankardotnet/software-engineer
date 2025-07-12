## C# coding best practices
That’s a great start—you’re already focusing on some of the most common pitfalls like duplication, method length, and naming. Here’s a **comprehensive guide** to **C# coding best practices** for writing **clean, readable, maintainable, and efficient code** that you can follow and also suggest in code reviews.


## ✅ **1. Naming Conventions (Clarity First)**

* **Use meaningful names:** Descriptive, self-explanatory names (avoid single-letter or cryptic names).
* **PascalCase** for:

  * Class names: `CustomerService`
  * Method names: `CalculateInvoice()`
  * Public properties: `OrderDate`
* **camelCase** for:

  * Local variables: `orderList`
  * Method parameters: `customerId`
* **Prefix interfaces with 'I'**: `IRepository`, `ILogger`


## ✅ **2. Keep Methods Short & Focused (Single Responsibility)**

* Follow the **Single Responsibility Principle (SRP)**: Each method should do **one thing only**.
* Ideally, methods should be **10-20 lines** or less.
* If a method does multiple things, split it into smaller private methods.


## ✅ **3. Avoid Duplicate Code (DRY Principle)**

* Don’t Repeat Yourself (**DRY**): Reuse existing code through methods, classes, or helper utilities.
* Common duplication sources:

  * Validation logic
  * Mapping logic
  * Business rules

✅ **Refactor repeated code into shared methods or extension methods.**


## ✅ **4. Consistent Code Formatting**

* Use consistent **indentation (4 spaces in C#)**.
* Braces `{}` on **new lines** (C# standard) or follow your team’s style.
* Maintain consistent **white space** for readability.


## ✅ **5. Comment Wisely**

* **Code should explain itself**—use comments **only** when the reasoning is not obvious.
* Avoid **obvious comments**:

  ```csharp
  // Increment i by 1
  i++;
  ```
* Use comments for:

  * Explaining **why** something is done (not **what**).
  * Documenting complex algorithms or business rules.


## ✅ **6. Avoid Large Classes (Separation of Concerns)**

* Classes should be **cohesive**—stick to **one purpose or entity**.
* Break down **God classes** into:

  * Service classes
  * Helper classes
  * Domain models

## ✅ **7. Use Meaningful Constants and Enums**

* Don’t use **magic numbers** or hardcoded strings.

  ```csharp
  const int MaxAttempts = 3;
  enum OrderStatus { Pending, Shipped, Delivered }
  ```


## ✅ **8. Exception Handling Best Practices**

* Catch only **expected exceptions**—don’t use `catch (Exception ex)` for everything.
* Avoid swallowing exceptions silently.
* Use **custom exceptions** when necessary.
* Example:

  ```csharp
  try
  {
      // risky code
  }
  catch (FileNotFoundException ex)
  {
      // handle specific case
  }
  ```


## ✅ **9. Avoid Premature Optimization**

* Write **clean and readable code first**, then optimize only when there’s a clear performance issue.
* Measure before you optimize.


## ✅ **10. Minimize Public Surface Area (Encapsulation)**

* Keep methods and fields **private** unless they need to be public.
* Use **interfaces** for abstraction and expose only what's necessary.


## ✅ **11. Use LINQ & Functional Style Carefully**

* Prefer **LINQ** over manual loops for concise, readable code:

  ```csharp
  var activeUsers = users.Where(u => u.IsActive).ToList();
  ```
* But don’t overdo it—sometimes a simple `foreach` loop is clearer.


## ✅ **12. Null Safety & Defensive Programming**

* Use **nullable reference types (C# 8+)**: `string? name`
* Check for **null** before accessing:

  ```csharp
  if (user != null) { ... }
  ```
* Use `??` (null-coalescing) or `?.` (null-conditional).


## ✅ **13. Follow SOLID Principles**

1. **S**ingle Responsibility
2. **O**pen/Closed
3. **L**iskov Substitution
4. **I**nterface Segregation
5. **D**ependency Inversion

These ensure scalability and maintainability.


## ✅ **14. Unit Tests & Testability**

* Write **unit-testable code**—favor **interfaces** and **dependency injection**.
* Use **xUnit** or **NUnit** for writing unit tests.
* Keep business logic **outside of controllers**.


## ✅ **15. Version Control Best Practices**

* Write **meaningful commit messages**.
* Keep commits **small and atomic**.
* Follow **branching strategies** (Git Flow, Trunk-Based).


## ✅ **16. Code Reviews Best Practices**

* Be constructive and specific in feedback.
* Suggest improvements rather than just pointing out issues.
* Discuss **why** something should be changed.


## ✍️ Example Before & After Refactoring:

### ❌ Bad:

```csharp
public void ProcessOrder(int id, string name, int status)
{
    if (status == 1)
    {
        // do something
    }
}
```

### ✅ Good:

```csharp
public void ProcessOrder(int orderId, string customerName, OrderStatus status)
{
    if (status == OrderStatus.Pending)
    {
        // process order
    }
}

public enum OrderStatus
{
    Pending,
    Shipped,
    Delivered
}
```

## Summary Cheat Sheet:

| Practice               | Key Point                                 |
| ---------------------- | ----------------------------------------- |
| **Naming**             | Clear, descriptive, consistent            |
| **Method Length**      | Short, single responsibility              |
| **DRY**                | Avoid duplication                         |
| **Formatting**         | Consistent indent, braces, spacing        |
| **Commenting**         | Explain **why**, not **what**             |
| **Exception Handling** | Catch specific exceptions only            |
| **Encapsulation**      | Minimize public members                   |
| **SOLID Principles**   | Apply design principles for scalability   |
| **Testing**            | Write testable code with clear boundaries |


# ✅ **C# Code Review Checklist**


## 🔹 **1. Code Readability & Clarity**

* [ ] Is the code **self-explanatory** and easy to read without needing excessive comments?
* [ ] Are **method and variable names meaningful** and follow naming conventions? (`PascalCase` for methods/classes, `camelCase` for variables/parameters)
* [ ] Is the code **well-formatted** (consistent indentation, spacing, and braces)?
* [ ] Are **comments used sparingly** and only to explain *why*, not *what*?


## 🔹 **2. Duplication & Reusability**

* [ ] Is there any **duplicate code** that can be extracted into shared methods or helpers?
* [ ] Are **common patterns, logic, or constants** reused appropriately?


## 🔹 **3. Method & Class Design**

* [ ] Do methods follow the **Single Responsibility Principle**?
* [ ] Are **methods short (ideally under 20 lines)** and do they perform **one task**?
* [ ] Are large classes broken down into **smaller, focused classes**?


## 🔹 **4. Naming Conventions**

* [ ] Are all names (classes, methods, variables, parameters) **consistent, descriptive, and follow C# naming standards**?
* [ ] Are **enums** used instead of numeric or string literals where appropriate?
* [ ] Are constants and readonly fields named using **PascalCase**?


## 🔹 **5. Error Handling**

* [ ] Are **exceptions properly handled** (specific exception types, not general `catch (Exception)` unless justified)?
* [ ] Are **no silent catch blocks** used without logging or meaningful handling?
* [ ] Are **custom exceptions** used where needed for clarity?


## 🔹 **6. Null Handling & Defensive Coding**

* [ ] Is the code **safe against null references** (`?.`, `??`, or explicit null checks)?
* [ ] Are **nullable reference types** used where applicable (`string?` vs `string`)?


## 🔹 **7. Code Efficiency & Performance**

* [ ] Is the code written with **performance in mind** (avoiding unnecessary loops, redundant calls)?
* [ ] Are expensive operations **avoided inside loops**?
* [ ] Are **collections properly used** (e.g., `Dictionary` vs `List` for lookups)?


## 🔹 **8. SOLID Principles & Design**

* [ ] Does the code follow **SOLID principles** (especially **Single Responsibility**, **Open/Closed**, **Dependency Inversion**)?
* [ ] Are **interfaces and abstractions** used where applicable for flexibility and testability?
* [ ] Are **Dependency Injection (DI)** and **IoC containers** used instead of hard-coded dependencies?


## 🔹 **9. Security & Sensitive Data**

* [ ] Is **sensitive information** (like connection strings, API keys) **not hardcoded**?
* [ ] Are **input validations** in place to avoid injection attacks or other security issues?
* [ ] Are error messages not revealing **internal system details**?


## 🔹 **10. Unit Testing & Testability**

* [ ] Is the code **unit-testable** (no tightly coupled dependencies, no static calls)?
* [ ] Are there **sufficient unit tests** covering all logical paths?
* [ ] Are **test names** clear and descriptive of the scenario?


## 🔹 **11. Logging & Monitoring**

* [ ] Are **proper logs added** for key business operations and exceptions?
* [ ] Is **log level** (Info, Debug, Warning, Error) used appropriately?
* [ ] Are logs not exposing **sensitive data**?


## 🔹 **12. Code Style & Tools**

* [ ] Are **static analysis tools** (e.g., SonarQube, ReSharper, StyleCop) used and followed?
* [ ] Does the code pass **build warnings and analysis checks**?


## 🔹 **13. Version Control & Commit Quality**

* [ ] Are **commit messages clear, concise, and meaningful**?
* [ ] Are **commits small and focused** on a single change or fix?


## ✅ Quick Code Smell Detection (Red Flags 🚩):

| 🚩 Code Smell                        | Example                                                  |
| ------------------------------------ | -------------------------------------------------------- |
| **Long Methods**                     | Methods > 30 lines                                       |
| **Too Many Parameters**              | More than 4–5 parameters—consider DTOs                   |
| **Hardcoded values**                 | Magic numbers or strings                                 |
| **Boolean Flags**                    | Method behavior changes based on too many flags          |
| **Tightly Coupled Code**             | Direct dependency on concrete classes                    |
| **Nested if/else or switch ladders** | Can it be replaced by polymorphism or dictionary lookup? |


👉 **Suggested Workflow:**

1. **Automated Checks First:** StyleCop, Code Analyzers, Static Tools.
2. **Manual Review:** Focus on readability, architecture, and business logic.
3. **Collaborative Discussion:** Share reasoning, not just problems.
