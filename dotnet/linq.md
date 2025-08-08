Great! Here's a complete LINQ guide covering:

---

## ✅ **LINQ (Language Integrated Query) in C# – Complete Guide**

### 🔹 **What is LINQ?**

LINQ lets you query collections like arrays, lists, XML, or databases using a SQL-like syntax directly in C#.

---

## 🧱 **LINQ Query Syntax vs Method Syntax**

* **Query Syntax** (similar to SQL):

```csharp
var result = from n in numbers where n > 5 select n;
```

* **Method Syntax** (fluent style):

```csharp
var result = numbers.Where(n => n > 5);
```

Both give the same output — method syntax is more powerful and preferred for advanced scenarios.

---

## 📘 **Commonly Used LINQ Methods – With Examples**

### 1. **Where**

Filters elements based on a predicate.

```csharp
var evenNumbers = numbers.Where(n => n % 2 == 0);
```

---

### 2. **Select**

Projects each element into a new form.

```csharp
var squares = numbers.Select(n => n * n);
```

---

### 3. **SelectMany**

Flattens collections within collections.

```csharp
var allChars = words.SelectMany(word => word.ToCharArray());
```

---

### 4. **Any / All**

Checks if **any** or **all** elements satisfy a condition.

```csharp
var hasEven = numbers.Any(n => n % 2 == 0);
var allPositive = numbers.All(n => n > 0);
```

---

### 5. **First / FirstOrDefault / Single / SingleOrDefault / Last**

Fetch elements with control over defaults and exceptions.

```csharp
var first = list.FirstOrDefault(); // safer
var single = list.Single(); // throws if more than one
```

---

### 6. **OrderBy / ThenBy / Reverse**

Sort collections.

```csharp
var sorted = students.OrderBy(s => s.Marks).ThenBy(s => s.Name);
```

---

### 7. **GroupBy**

Groups elements by a key.

```csharp
var grouped = items.GroupBy(x => x.Category);
```

---

### 8. **ToDictionary / ToList / ToArray / ToHashSet**

Materialize query results.

```csharp
var dict = students.ToDictionary(s => s.Id);
```

---

### 9. **Aggregate**

Performs custom accumulation.

```csharp
var sentence = words.Aggregate((a, b) => a + " " + b);
```

---

### 10. **Distinct / Union / Intersect / Except**

Set-based operations.

```csharp
var common = list1.Intersect(list2);
```

---

### 11. **Skip / Take / SkipWhile / TakeWhile**

Pagination or filtering based on conditions.

```csharp
var page = products.Skip(20).Take(10);
```

---

## 🧠 **Advanced LINQ Scenarios and Problems**

### 🔹 Get all substrings of a string using LINQ

```csharp
string str = "abc";
var substrings = Enumerable.Range(0, str.Length)
    .SelectMany(i => Enumerable.Range(1, str.Length - i)
        .Select(len => str.Substring(i, len)));
```

---

### 🔹 Cartesian Product of Two Lists

```csharp
var product = list1.SelectMany(a => list2, (a, b) => (a, b));
```

---

### 🔹 Flatten a 2D list

```csharp
List<List<int>> matrix = ...;
var flat = matrix.SelectMany(x => x);
```

---

### 🔹 Find Missing Numbers in Range

```csharp
var fullRange = Enumerable.Range(1, 100);
var missing = fullRange.Except(actualList);
```

---

### 🔹 Group By and Count

```csharp
var freq = items.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
```

---

## 🛠️ **LINQ Performance Tips**

| Tip                                           | Description                                                |
| --------------------------------------------- | ---------------------------------------------------------- |
| **Use Deferred Execution**                    | Chained LINQ methods are lazily evaluated.                 |
| **Avoid multiple enumerations**               | Use `.ToList()` or `.ToArray()` if you must iterate again. |
| **Indexing? Use `Select((val, idx) => ...)`** | Adds index to selection.                                   |
| **Use HashSet for `Contains()`**              | Faster lookups than list.                                  |
| **Use `AsParallel()`**                        | For large data sets (Parallel LINQ).                       |

---

## 🎯 **Deferred Execution Example**

```csharp
var query = numbers.Where(n => n > 5); // Nothing happens yet

foreach (var n in query) {
    Console.WriteLine(n); // Query is evaluated here
}
```

---

## ⚡ Useful Combinations for Real-World Problems

### 1. **Top N Frequent Elements**

```csharp
var top = list.GroupBy(x => x)
              .OrderByDescending(g => g.Count())
              .Take(3)
              .Select(g => g.Key);
```

---

### 2. **Anagram Grouping**

```csharp
var groups = words.GroupBy(w => new string(w.OrderBy(c => c).ToArray()));
```

---

### 3. **Sliding Window Sums**

```csharp
var k = 3;
var slidingSums = Enumerable.Range(0, list.Count - k + 1)
    .Select(i => list.Skip(i).Take(k).Sum());
```

---

## 🧪 Testing LINQ Results

Use assertions like:

```csharp
Assert.AreEqual(expectedList, result.ToList());
```

---

## ✅ **Summary – When to Use What**

| Task            | LINQ Method            |
| --------------- | ---------------------- |
| Filter          | `Where`                |
| Transform       | `Select`, `SelectMany` |
| Grouping        | `GroupBy`, `ToLookup`  |
| Sorting         | `OrderBy`, `ThenBy`    |
| Pagination      | `Skip`, `Take`         |
| Uniqueness      | `Distinct`, `GroupBy`  |
| Count/Frequency | `GroupBy().Count()`    |
| Joins           | `Join`, `GroupJoin`    |

---

```csharp
// Complete LINQ Guide in C# with Examples

using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQGuide
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public List<string> Skills { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }

    public class LINQExamples
    {
        public static void Main()
        {
            // Sample data
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", Department = "HR", Salary = 60000, Skills = new List<string>{"Excel", "Communication"} },
                new Employee { Id = 2, Name = "Bob", Department = "IT", Salary = 90000, Skills = new List<string>{"C#", "Azure"} },
                new Employee { Id = 3, Name = "Charlie", Department = "IT", Salary = 95000, Skills = new List<string>{"Java", "AWS"} },
                new Employee { Id = 4, Name = "David", Department = "Finance", Salary = 70000, Skills = new List<string>{"Excel", "Accounting"} },
            };

            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Price = 80000, Category = "Electronics" },
                new Product { ProductId = 2, Name = "Phone", Price = 50000, Category = "Electronics" },
                new Product { ProductId = 3, Name = "Shirt", Price = 2000, Category = "Clothing" },
            };

            // 1. Filtering (Where)
            var highPaid = employees.Where(e => e.Salary > 70000);

            // 2. Projection (Select)
            var names = employees.Select(e => e.Name);

            // 3. Flattening (SelectMany)
            var allSkills = employees.SelectMany(e => e.Skills).Distinct();

            // 4. Sorting (OrderBy, ThenBy)
            var sortedBySalary = employees.OrderBy(e => e.Salary).ThenBy(e => e.Name);

            // 5. Grouping (GroupBy)
            var deptGroups = employees.GroupBy(e => e.Department);

            // 6. Aggregation (Sum, Average, Min, Max)
            var avgSalary = employees.Average(e => e.Salary);
            var totalSalary = employees.Sum(e => e.Salary);

            // 7. Set operations (Distinct, Union, Intersect, Except)
            var skills1 = new List<string> { "C#", "SQL" };
            var skills2 = new List<string> { "SQL", "Azure" };
            var commonSkills = skills1.Intersect(skills2);

            // 8. Partitioning (Take, Skip)
            var top2 = employees.OrderByDescending(e => e.Salary).Take(2);

            // 9. Element Operators (First, FirstOrDefault, Single)
            var firstIT = employees.FirstOrDefault(e => e.Department == "IT");

            // 10. Quantifiers (Any, All)
            var hasHighSalary = employees.Any(e => e.Salary > 100000);

            // 11. Join
            var productWithCategory = employees.Join(products,
                emp => emp.Department,
                prod => prod.Category,
                (emp, prod) => new { emp.Name, prod.Name });

            // 12. GroupJoin
            var deptWithEmployees = departments.GroupJoin(employees,
                dept => dept,
                emp => emp.Department,
                (dept, emps) => new { Department = dept, Employees = emps });

            // Deferred Execution Example
            var deferredQuery = employees.Where(e => e.Salary > 80000);
            employees.Add(new Employee { Id = 5, Name = "Eva", Department = "IT", Salary = 85000 });
            // Eva will be included here
            foreach (var e in deferredQuery) Console.WriteLine(e.Name);

            // Advanced Problem: Find employees with unique skills
            var uniqueSkills = employees.SelectMany(e => e.Skills)
                .GroupBy(skill => skill)
                .Where(g => g.Count() == 1)
                .Select(g => g.Key);

            // All substrings using LINQ
            string str = "abc";
            var substrings = Enumerable.Range(0, str.Length)
                .SelectMany(i => Enumerable.Range(1, str.Length - i)
                .Select(len => str.Substring(i, len)));
        }

        private static List<string> departments = new List<string> { "HR", "IT", "Finance" };
    }
}

```
