# Complete Guide to Logging in .NET using Serilog

## Introduction to Logging

### What is Logging?
Logging is the practice of recording application events, errors, and other relevant information during software execution. It's like keeping a detailed diary of what happens in your application, helping developers understand system behavior, troubleshoot issues, and monitor application health.

### Why is Logging Important?
1. **Troubleshooting & Debugging**
   - Helps identify the root cause of problems
   - Provides context about errors and exceptions
   - Enables reproduction of issues

2. **Monitoring & Analytics**
   - Track application performance
   - Monitor system health
   - Analyze user behavior
   - Detect patterns and trends

3. **Security & Compliance**
   - Track security-related events
   - Maintain audit trails
   - Meet regulatory requirements
   - Detect suspicious activities

4. **Business Intelligence**
   - Understanding user patterns
   - Tracking feature usage
   - Measuring business metrics

### Types of Logging

1. **Development Logging**
   - Detailed debugging information
   - Stack traces
   - Variable values
   - Used during development

2. **Production Logging**
   - Performance metrics
   - Error tracking
   - User actions
   - System events

3. **Security Logging**
   - Authentication attempts
   - Authorization failures
   - Data access logs
   - System changes

### Logging Levels

1. **Verbose** - Detailed debugging information
2. **Debug** - Internal system events
3. **Information** - Normal system operation
4. **Warning** - Potential issues
5. **Error** - Problems that need attention
6. **Fatal** - Critical issues that need immediate attention

# Serilog Logging in .NET: Traditional vs Structured Logging

## 1. Basic Setup
```csharp
// Install Serilog NuGet packages:
// Serilog
// Serilog.Sinks.Console
// Serilog.Sinks.File

using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

## 2. Traditional Logging

### 2.1 Code Examples
```csharp
// Basic logging at different levels
Log.Verbose("Starting application initialization");
Log.Debug("Loading configuration from file");
Log.Information("User logged in");
Log.Warning("Database connection slow");
Log.Error("Failed to save file");
Log.Fatal("System crash");

// Logging with Exception
try
{
    throw new Exception("File not found");
}
catch (Exception ex)
{
    Log.Error(ex, "Error occurred while processing file");
}
```

### 2.2 Traditional Output
```
[2023-07-21 10:15:30.123 VRB] Starting application initialization
[2023-07-21 10:15:30.124 DBG] Loading configuration from file
[2023-07-21 10:15:30.125 INF] User logged in
[2023-07-21 10:15:30.126 WRN] Database connection slow
[2023-07-21 10:15:30.127 ERR] Failed to save file
[2023-07-21 10:15:30.128 FTL] System crash

[2023-07-21 10:15:30.129 ERR] Error occurred while processing file
System.Exception: File not found
   at Program.<Main>$ in C:\Project\Program.cs:line 15
   at Program.Main() in C:\Project\Program.cs:line 8
```

## 3. Structured Logging

### 3.1 Code Examples
```csharp
// Basic structured logging
Log.Information("User {UserId} logged in from {IPAddress}", "john123", "192.168.1.1");
Log.Warning("Database response time {ResponseTime}ms exceeded threshold {Threshold}ms", 
    1500, 1000);
Log.Error("Order {OrderId} failed processing for customer {CustomerId}", 
    "ORD-123", "CUST-456");

// Structured logging with exception
try
{
    throw new Exception("Payment processing failed");
}
catch (Exception ex)
{
    Log.Error(ex, "Failed to process payment for order {OrderId}", "ORD-123");
}
```

### 3.2 Structured Output

#### Console Output
```
[2023-07-21 10:15:30 INF] User "john123" logged in from "192.168.1.1"
[2023-07-21 10:15:31 WRN] Database response time 1500ms exceeded threshold 1000ms
[2023-07-21 10:15:32 ERR] Order "ORD-123" failed processing for customer "CUST-456"
```

#### JSON Output (when configured)
```json
{
  "Timestamp": "2023-07-21T10:15:30",
  "Level": "Information",
  "Message": "User \"john123\" logged in from \"192.168.1.1\"",
  "Properties": {
    "UserId": "john123",
    "IPAddress": "192.168.1.1"
  }
}
{
  "Timestamp": "2023-07-21T10:15:31",
  "Level": "Warning",
  "Message": "Database response time 1500ms exceeded threshold 1000ms",
  "Properties": {
    "ResponseTime": 1500,
    "Threshold": 1000
  }
}
```

## 4. Common Use Cases

### 4.1 Application Monitoring
```csharp
Log.Information("Application {AppName} started. Version: {Version}", 
    "MyApp", "1.0.0");
Log.Information("Server listening on port {Port}", 5000);
```

### 4.2 Performance Tracking
```csharp
var sw = Stopwatch.StartNew();
// ... some operation
sw.Stop();
Log.Information("Operation completed in {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
```

### 4.3 Error Handling
```csharp
try
{
    // ... business logic
}
catch (Exception ex)
{
    Log.Error(ex, 
        "Transaction {TransactionId} failed for user {UserId}", 
        "TXN-123", 
        "USER-456");
}
```

### 4.4 Security Events
```csharp
Log.Warning("Failed login attempt for user {Username} from {IPAddress}", 
    username, 
    ipAddress);
```

## 5. Benefits Comparison

### Traditional Logging
- Simple and straightforward
- Easy to read in plain text
- Familiar format
- Lower overhead
- Suitable for simple applications

### Structured Logging
- Better searchability
- Enhanced filtering capabilities
- Machine-readable format
- Better integration with log analysis tools
- Easier to aggregate and analyze
- Consistent property naming
- Better context preservation
- Improved debugging capabilities

## 6. ASP.NET Core Integration

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp-.txt", rollingInterval: RollingInterval.Day))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

## 7. Common Sinks (Output Destinations)
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()                    // Console output
    .WriteTo.File()                       // File output
    .WriteTo.Seq("http://seq:5341")      // Seq server
    .WriteTo.SQLite("logs.db")           // SQLite database
    .WriteTo.MSSqlServer()               // SQL Server
    .WriteTo.Elasticsearch()             // Elasticsearch
    .CreateLogger();
```

This comprehensive guide shows the key differences between traditional and structured logging, their implementations, outputs, and benefits in a .NET environment using Serilog.
