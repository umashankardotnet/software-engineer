Great! Let’s turn this into an interactive **mock interview exercise**.


## **Scenario**

You’ve joined a legacy C# project where the following code is written to calculate discounts and taxes for different types of customers.

Here’s the **code sample**:

```csharp
public class Invoice
{
    public string CustomerType { get; set; }
    public decimal Amount { get; set; }

    public decimal GetDiscountedAmount()
    {
        if (CustomerType == "Regular")
        {
            return Amount * 0.9m; // 10% discount
        }
        else if (CustomerType == "Premium")
        {
            return Amount * 0.8m; // 20% discount
        }
        else if (CustomerType == "Gold")
        {
            return Amount * 0.7m; // 30% discount
        }
        else
        {
            return Amount;
        }
    }

    public decimal CalculateTax()
    {
        if (CustomerType == "Regular" || CustomerType == "Premium")
        {
            return GetDiscountedAmount() * 0.18m; // 18% GST
        }
        else if (CustomerType == "Gold")
        {
            return GetDiscountedAmount() * 0.12m; // 12% GST
        }
        else
        {
            return GetDiscountedAmount() * 0.05m; // default GST
        }
    }

    public void PrintInvoice()
    {
        Console.WriteLine($"Customer Type: {CustomerType}");
        Console.WriteLine($"Original Amount: {Amount}");
        Console.WriteLine($"Discounted Amount: {GetDiscountedAmount()}");
        Console.WriteLine($"Tax: {CalculateTax()}");
        Console.WriteLine($"Total: {GetDiscountedAmount() + CalculateTax()}");
    }
}
```


### Your Turn (Mock Interview Style)

**Question**: Based on your understanding of software principles and design patterns, what all issues or violations do you notice in the code above?
👉 *You can list what’s wrong and which principles are violated (e.g. SRP, OCP, DRY, etc.)*.

Once you answer, I’ll provide detailed feedback and cross-check your response.

Go ahead.


## Answer Discussion


#### **1. SRP Violation (Single Responsibility Principle) — You are Correct**

> **Your Answer:** Invoice class contains responsibilities like discount logic, tax logic, and printing — hence violating SRP.

**Explanation:**
`Invoice` is doing too much:

* Calculating discount
* Calculating tax
* Printing invoice

Best Practice: These should be **separated into different services**, e.g.:

* `IDiscountCalculator`
* `ITaxCalculator`
* `IInvoicePrinter`


#### **2. OCP Violation (Open/Closed Principle) — Absolutely Correct**

> **Your Answer:** Adding a new customer type means modifying both `GetDiscountedAmount` and `CalculateTax`.

**Explanation:**
Currently, business rules are based on `if-else` chains, which will **grow unmanageable** with more customer types.

**Fix:**
Use **Strategy Pattern** or a **lookup table** to delegate logic. This allows you to add new behavior **without modifying existing code**.


#### **3. Hardcoded Customer Types — Correct Again**

> **Your Answer:** We should use Enums instead of string literals.

**Explanation:**
Hardcoded `"Regular"`, `"Premium"`, `"Gold"` strings are error-prone and break encapsulation.

Replace with:

```csharp
public enum CustomerType
{
    Regular,
    Premium,
    Gold
}
```

You can then use `switch` expressions or polymorphism.


#### **4. Constants for Magic Numbers — Great Point**

> **Your Answer:** Replace magic numbers with named constants.

**Explanation:**
`0.9m`, `0.18m`, `0.12m` should be extracted as constants or configuration.

e.g.:

```csharp
private const decimal RegularDiscount = 0.10m;
private const decimal PremiumDiscount = 0.20m;
```


### Bonus Observations (From My Side)

#### **5. Violates DRY (Don’t Repeat Yourself)**

* `GetDiscountedAmount()` is called 3 times in `PrintInvoice()`.
  👉 It should be called once and stored in a variable.


#### **6. Violates YAGNI (You Ain’t Gonna Need It) — *Debatable***

If the `PrintInvoice` method is never used in production or handled elsewhere (e.g. UI), then keeping it here is unnecessary.


#### **7. Tight Coupling**

* Tax and Discount logic are **tightly coupled to customer types**.
* Should be abstracted and injected using interfaces.


### Design Pattern Suggestion

You rightly said: **Use Strategy Pattern**.
Here’s how you could organize it:

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount);
}

public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount) => amount * 0.9m;
}
```

Then inject appropriate strategy at runtime.


### Summary: Violations You Correctly Identified

| Principle / Practice | Status | Your Observation | ✅ Feedback    |
| -------------------- | ------ | ---------------- | ------------- |
| SRP                  | ✅      | Yes              | Valid         |
| OCP                  | ✅      | Yes              | Valid         |
| Enum Usage           | ✅      | Yes              | Best Practice |
| Magic Numbers        | ✅      | Yes              | Valid         |
| DRY                  | ➕      | Not Mentioned    | Add this too  |
| YAGNI                | ➕      | Not Mentioned    | Optional      |

---

Would you like me to now **refactor this code** using Strategy Pattern and SOLID principles as a reference solution?
