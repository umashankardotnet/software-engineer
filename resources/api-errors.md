# API Errors
When calling an API, a wide range of errors can occur due to issues in **client configuration**, **server logic**, **network communication**, **authentication**, **rate limiting**, and more. Below is a comprehensive categorization of **all types of errors** you might encounter during an API call.

---

## 🧭 1. **Client-Side Errors (4xx)**

These errors indicate that the client request is invalid or unauthorized.

| Error Code                     | Meaning                                                        | Common Causes                                          |
| ------------------------------ | -------------------------------------------------------------- | ------------------------------------------------------ |
| **400 Bad Request**            | The server cannot process the request due to malformed syntax. | Invalid JSON, missing parameters, invalid query values |
| **401 Unauthorized**           | Authentication failed or not provided.                         | Missing/expired token, incorrect API key               |
| **403 Forbidden**              | The client is authenticated but does not have access.          | Insufficient permissions, IP blocked                   |
| **404 Not Found**              | The requested endpoint/resource does not exist.                | Incorrect URL, deleted resource                        |
| **405 Method Not Allowed**     | HTTP method not supported for the endpoint.                    | Using POST instead of GET, etc.                        |
| **406 Not Acceptable**         | Server cannot respond with the requested content type.         | Unsupported `Accept` header                            |
| **408 Request Timeout**        | The client took too long to send the request.                  | Slow connection, heavy payload                         |
| **409 Conflict**               | Conflict with current state of the resource.                   | Duplicate record, data versioning conflict             |
| **410 Gone**                   | The resource has been permanently removed.                     | Deprecated API                                         |
| **413 Payload Too Large**      | Request body exceeds limit.                                    | Large file uploads                                     |
| **414 URI Too Long**           | The request URL is too long.                                   | Long query strings                                     |
| **415 Unsupported Media Type** | Content type is not supported.                                 | Sending XML when only JSON is accepted                 |
| **429 Too Many Requests**      | Rate limiting exceeded.                                        | Too many API calls in a short time                     |

---

## 🏗️ 2. **Server-Side Errors (5xx)**

These indicate a problem with the server or API provider.

| Error Code                         | Meaning                                         | Common Causes                       |
| ---------------------------------- | ----------------------------------------------- | ----------------------------------- |
| **500 Internal Server Error**      | Generic server error.                           | Unhandled exceptions, logic bugs    |
| **501 Not Implemented**            | Endpoint exists but method not implemented.     | Placeholder API                     |
| **502 Bad Gateway**                | Received invalid response from upstream server. | Load balancer/Reverse proxy failure |
| **503 Service Unavailable**        | Server is down or overloaded.                   | Maintenance, scaling issues         |
| **504 Gateway Timeout**            | Upstream server failed to respond in time.      | Microservice communication timeout  |
| **505 HTTP Version Not Supported** | Unsupported HTTP version used.                  | Outdated clients                    |

---

## 🌐 3. **Network/Transport Layer Errors**

These errors occur **before** an actual HTTP response is received.

| Type                       | Examples                                 | Common Causes                       |
| -------------------------- | ---------------------------------------- | ----------------------------------- |
| **DNS Resolution Failure** | Cannot resolve domain name               | Typo in URL, DNS misconfiguration   |
| **Connection Timeout**     | Server didn’t respond in time            | Server is slow or down              |
| **Connection Refused**     | No server at the specified port          | Firewall, server offline            |
| **SSL/TLS Errors**         | Certificate mismatch or expired          | Invalid SSL cert, wrong hostname    |
| **Network Unreachable**    | Network issues between client and server | ISP problems, VPC misconfig         |
| **Proxy Errors**           | Proxy server fails or rejects request    | Misconfigured forward/reverse proxy |

---

## 🔐 4. **Authentication & Authorization Errors**

Even if the HTTP request is well-formed, failures can occur at the security layer.

| Type                          | Examples                         | Common Causes                       |
| ----------------------------- | -------------------------------- | ----------------------------------- |
| **Missing or Expired Tokens** | JWT token expired                | Clock drift, session expired        |
| **Invalid Signature**         | Signature mismatch               | Wrong secret key or hashing         |
| **OAuth Errors**              | `invalid_grant`, `invalid_scope` | Misconfigured scopes or grant types |
| **Insufficient Permissions**  | Authenticated but not authorized | Role lacks required privileges      |

---

## 🚦 5. **Rate Limiting / Throttling**

APIs often apply limits to prevent abuse.

| Error Code                               | Type                          | Causes                        |
| ---------------------------------------- | ----------------------------- | ----------------------------- |
| **429 Too Many Requests**                | Client sent too many requests | Exceeded quota or burst limit |
| **403 Forbidden (with rate limit info)** | Over global rate limit        | Shared API key exceeded usage |

Often includes headers like:

* `Retry-After`
* `X-RateLimit-Reset`
* `X-RateLimit-Remaining`

---

## 🧪 6. **Data Validation & Contract Errors**

These are logic-level errors returned in the **response body**, not as HTTP status codes.

| Type                      | Examples                        | Cause                    |
| ------------------------- | ------------------------------- | ------------------------ |
| **Missing Fields**        | `"email" is required`           | Schema mismatch          |
| **Invalid Format**        | `"email" must be a valid email` | Input value is malformed |
| **Constraint Violations** | `"quantity must be > 0"`        | Business rules broken    |

---

## 🔄 7. **API Versioning / Deprecation Errors**

| Type                   | Examples                   | Causes                        |
| ---------------------- | -------------------------- | ----------------------------- |
| **Invalid Version**    | API v1 no longer supported | Client uses outdated endpoint |
| **Deprecated Feature** | Field or parameter removed | Old clients not updated       |

---

## 🧩 8. **Dependency/Upstream Failures (in microservices)**

| Type                          | Examples                            | Cause                        |
| ----------------------------- | ----------------------------------- | ---------------------------- |
| **Circuit Breaker Open**      | Service unavailable due to failures | Too many recent errors       |
| **Service Discovery Failure** | Microservice can’t locate another   | DNS or service registry down |
| **Message Queue Errors**      | Can't enqueue/dequeue               | Kafka, SQS, RabbitMQ issues  |

---

## 🧭 9. **Business Logic Errors (Application-Level)**

These are returned as 2xx or 4xx codes but indicate that something went wrong logically.

| Type                 | Examples                 | Cause                            |
| -------------------- | ------------------------ | -------------------------------- |
| **Payment Failed**   | `"transaction_declined"` | Insufficient funds, card expired |
| **Inventory Error**  | `"item out of stock"`    | Real-time data changed           |
| **Duplicate Action** | `"duplicate request"`    | Idempotency token reused         |

---

## 🧯 10. **SDK or Client-Side Library Errors**

| Type                     | Examples                    | Causes                                   |
| ------------------------ | --------------------------- | ---------------------------------------- |
| **Parsing Errors**       | Cannot deserialize response | Invalid or unexpected response structure |
| **Missing Dependencies** | Missing packages/modules    | Incorrect SDK installation               |
| **Serialization Errors** | JSON encode/decode errors   | Unsupported data types                   |

---

## 🛠️ How to Handle API Errors Gracefully

1. **Retry with Backoff**: For transient errors like `500`, `502`, `503`, and `504`.
2. **Fallback Mechanism**: Show cached or default data.
3. **Logging**: Always log request and error context.
4. **User-Friendly Messages**: Hide raw error codes in UI.
5. **Monitoring and Alerts**: Track API error rates using tools like CloudWatch, Datadog, etc.
6. **Validation**: Always validate inputs before making API calls.

---

# API error handling techniques
All the code examples written in **C#**, specifically suitable for ASP.NET Core or general .NET applications.

---

## ✅ 1. **Input Validation and Sanitization**

### 🔧 Technique:

Validate inputs using model annotations and custom validation logic.

### ✅ Fixes:

* **400 Bad Request**
* **422 Unprocessable Entity**

### 📌 C# Example:

```csharp
public class UserModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

In your controller:

```csharp
[HttpPost]
public IActionResult Register([FromBody] UserModel user)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    
    // Proceed with logic
}
```

---

## 🔐 2. **Authentication and Authorization Handling**

### 🔧 Technique:

Use JWT, OAuth2, and ASP.NET authorization policies.

### ✅ Fixes:

* **401 Unauthorized**
* **403 Forbidden**

### 📌 C# Example:

```csharp
[Authorize(Roles = "Admin")]
[HttpGet("secure-data")]
public IActionResult GetSecureData()
{
    return Ok("Authorized access.");
}
```

---

## 🕒 3. **Retry with Exponential Backoff**

### 🔧 Technique:

Retry API calls for transient faults using a library like **Polly**.

### ✅ Fixes:

* **5xx Errors**
* **429 Too Many Requests**
* **Timeouts**

### 📌 C# Example using Polly:

```csharp
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetry(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

await retryPolicy.ExecuteAsync(async () =>
{
    var response = await httpClient.GetAsync("https://api.example.com/data");
    response.EnsureSuccessStatusCode();
});
```

---

## 🚨 4. **Circuit Breaker Pattern**

### 🔧 Technique:

Stop calling a service after repeated failures.

### ✅ Fixes:

* Service unavailability, dependency failures

### 📌 C# Example using Polly:

```csharp
var circuitBreaker = Policy
    .Handle<HttpRequestException>()
    .CircuitBreaker(2, TimeSpan.FromMinutes(1));
```

---

## 📦 5. **Graceful Degradation and Fallbacks**

### 🔧 Technique:

Return cached/default data if the service fails.

### ✅ Fixes:

* **503 Service Unavailable**
* **504 Gateway Timeout**

### 📌 C# Example with Polly fallback:

```csharp
var fallback = Policy<string>
    .Handle<Exception>()
    .FallbackAsync("Default data due to service failure.");

var result = await fallback.ExecuteAsync(async () => 
{
    return await httpClient.GetStringAsync("https://api.example.com/data");
});
```

---

## 🚧 6. **Rate Limiting and Throttling Management**

### 🔧 Technique:

Respect `Retry-After` headers and implement client throttling.

### ✅ Fixes:

* **429 Too Many Requests**

### 📌 C# Concept:

```csharp
if (response.StatusCode == HttpStatusCode.TooManyRequests &&
    response.Headers.RetryAfter != null)
{
    var retryAfter = response.Headers.RetryAfter.Delta?.TotalSeconds ?? 60;
    await Task.Delay(TimeSpan.FromSeconds(retryAfter));
}
```

---

## 🗺️ 7. **Centralized Error Handling and Logging**

### 🔧 Technique:

Use middleware to handle errors globally.

### ✅ Fixes:

* All categories

### 📌 C# Example:

```csharp
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
            // Log the exception
        }
    }
}
```

Register in `Startup.cs`:

```csharp
app.UseMiddleware<ErrorHandlingMiddleware>();
```

---

## 📬 8. **Idempotency Keys for Safe Retries**

### 🔧 Technique:

Use headers or tokens to identify duplicate POST/PUT requests.

### ✅ Fixes:

* **409 Conflict**
* Duplicate processing

### 📌 C# Concept:

```csharp
[HttpPost]
public IActionResult CreateOrder([FromBody] OrderRequest request, [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
{
    if (OrderAlreadyProcessed(idempotencyKey))
        return Conflict("Duplicate request");

    SaveOrder(request);
    StoreIdempotencyKey(idempotencyKey);
    return Ok();
}
```

---

## 💬 9. **User-Friendly Error Messages**

### 🔧 Technique:

Catch and translate exceptions into user-friendly messages.

### ✅ Fixes:

* All client-side usability

### 📌 C# Example:

```csharp
try
{
    var result = await _apiService.GetUserProfile();
    return Ok(result);
}
catch (UnauthorizedAccessException)
{
    return Forbid("Your session has expired. Please login again.");
}
```

---

## 🔁 10. **Graceful Timeouts and Cancellation**

### 🔧 Technique:

Use cancellation tokens and HTTP client timeouts.

### ✅ Fixes:

* **408**, **504**

### 📌 C# Example:

```csharp
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var response = await httpClient.GetAsync("https://api.example.com", cts.Token);
```

---

## 🧪 11. **Contract Testing and Schema Validation**

### 🔧 Technique:

Use Swagger and unit tests to ensure schema consistency.

### ✅ Fixes:

* **400**, **415**, serialization errors

### 📌 C# Example:

```csharp
services.AddSwaggerGen(); // Enable OpenAPI docs
```

Add `[Produces("application/json")]` to ensure format compliance.

---

## 🔄 12. **Monitoring, Alerting & Health Checks**

### 🔧 Technique:

Implement health endpoints and integrate with CloudWatch, Grafana, etc.

### ✅ Fixes:

* Detect and resolve 5xx or dependency failures early.

### 📌 C# Example:

```csharp
services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddUrlGroup(new Uri("https://api.example.com/health"));
```

---

## ⚙️ 13. **Versioning and Compatibility Management**

### 🔧 Technique:

Use versioned routes and backward compatibility.

### ✅ Fixes:

* **410 Gone**, deprecation issues

### 📌 C# Example:

```csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders() => Ok("Version 1");
}
```

---
