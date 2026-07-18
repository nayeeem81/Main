# Global Exception Handling & Logging System

## 📋 Overview

A production-ready exception handling and logging system for multi-tenant .NET 8.0 MVC Core applications. Built with **Serilog** for structured logging and **Entity Framework Core** for persistent storage.

### Key Features

- 🎯 **Global Exception Middleware** - Catches all unhandled exceptions automatically
- 📝 **Structured Logging** - Serilog with file and database persistence
- 🔐 **Secure** - Detailed logs for developers, generic messages for users
- 👥 **Multi-Tenant** - Automatic tenant isolation in all logs
- 📊 **Admin Dashboard API** - Full REST API for exception management
- 📈 **Analytics** - Exception trends, statistics, and summaries
- 🗑️ **Auto-Cleanup** - Configurable log retention policies
- 🔍 **Searchable** - Filter and export exception logs
- ⚡ **Performance** - Async logging, database indexes, deduplication

---

## 📦 Installation

### 1. Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB, Express, or Full)
- Entity Framework Core migrations

### 2. Install NuGet Packages

```bash
cd Main.WebAppCore
dotnet add package Serilog
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Formatting.Compact
```

### 3. Create and Apply Migration

```bash
cd Main.Migrator
dotnet ef migrations add AddExceptionLogsTable --project ..\Main.Infrastructure\
dotnet ef database update
```

### 4. Verify Files Created

All files should exist in your project:

```
✅ Main.Model/DomainModel/ExceptionLog.cs
✅ Main.Model/Enums/ExceptionEnums.cs
✅ Main.Common/Constants/ExceptionErrorCodes.cs
✅ Main.Common/Models/ErrorResponse.cs
✅ Main.Infrastructure/RegisterSerilogConfiguration.cs
✅ Main.Infrastructure/Services/ExceptionLoggingService.cs
✅ Main.WebAppCore/Middleware/GlobalExceptionHandlingMiddleware.cs
✅ Main.WebAppCore/Controllers/Admin/ExceptionLogsController.cs
✅ Main.WebAppCore/Controllers/Admin/ExceptionDashboardController.cs
```

### 5. Start Application

```bash
dotnet run
```

---

## 🏗️ Architecture

### Exception Flow

```
HTTP Request
    ↓
Global Exception Middleware
    ↓
Application Logic
    ↓ [Exception Thrown]
    ↓
Exception Caught by Middleware
    ↓
1. Map to Error Code & Status Code
2. Log to Serilog (files)
3. Log to Database
4. Return Safe Response
    ↓
HTTP Response (400/500/503...)
```

### Component Diagram

```
┌─────────────────────────────────────────────────────────┐
│  GlobalExceptionHandlingMiddleware                      │
│  - Catches all unhandled exceptions                     │
│  - Maps to error codes                                  │
└────────┬────────────────────────────────────────────────┘
         │
         ├─→ Serilog (File Logging)
         │   ├─ application-log-YYYY-MM-DD.txt
         │   ├─ errors-log-YYYY-MM-DD.txt
         │   └─ exceptions-log-YYYY-MM-DD.json
         │
         └─→ ExceptionLoggingService (Database)
             ├─ ApplicationDbContext
             ├─ ExceptionLogs Table
             └─ Multi-tenant Isolation
```

---

## 🔌 API Endpoints

Base URL: `https://api.yourdomain.com/api/exceptionlogs`

Authorization: `Admin` role required

### 1. Search Exception Logs

```http
POST /search
Content-Type: application/json

{
  "errorCode": "ERR_DB_001",
  "statusCode": 500,
  "startDate": "2024-01-01T00:00:00Z",
  "endDate": "2024-12-31T23:59:59Z",
  "isResolved": false,
  "pageNumber": 1,
  "pageSize": 20
}
```

### 2. Get Exception Details

```http
GET /{id}
```

### 3. Dashboard Statistics

```http
GET /summary/stats
```

Returns:
```json
{
  "totalExceptions": 150,
  "unresolvedCount": 45,
  "todayCount": 12,
  "exceptionsByType": {
    "SqlException": 65,
    "ArgumentNullException": 30
  },
  "exceptionsByStatus": {
    "500": 80,
    "400": 40
  },
  "trends": [
    {"date": "2024-01-10T00:00:00Z", "count": 5}
  ]
}
```

### 4. Mark as Resolved

```http
PUT /{id}/resolve
Content-Type: application/json

{
  "notes": "Fixed in v1.2.3"
}
```

### 5. Export to CSV

```http
POST /export/csv
```

### 6. Cleanup Old Logs

```http
DELETE /cleanup?days=90
X-Role: SuperAdmin
```

---

## 📊 Log File Structure

### File Locations

```
{Application Root}/
└─ Logs/
   ├─ application-log-2024-01-15.txt      (All logs)
   ├─ application-log-2024-01-14.txt      (Archived)
   ├─ errors-log-2024-01-15.txt           (Errors only)
   ├─ errors-log-2024-01-14.txt           (Archived)
   ├─ exceptions-log-2024-01-15.json      (JSON format)
   └─ exceptions-log-2024-01-14.json      (Archived)
```

### Retention Policy

- Daily rolling files
- 30-day retention by default
- Automatic cleanup of files older than 30 days
- Manual cleanup available via API (database records)

### Log Example

```
2024-01-15 10:30:45.123 +00:00 [ERR] [Main.Infrastructure.Services.ProductService]
Database connection timeout. The operation timed out.
{"Application":"Main.WebAppCore","Environment":"Production","ThreadId":5}
System.Data.SqlClient.SqlException: Timeout expired. The timeout period elapsed...
    at System.Data.SqlClient.SqlCommand.ExecuteReader()
    at Main.Infrastructure.Repository.ProductRepository.GetProductAsync()
    at Main.Services.ProductService.GetProductAsync(Int32 id)
```

---

## 🛡️ Security

### What Gets Logged

✅ Exception details (message, stack trace, inner exceptions)
✅ Request information (URL, method, status code)
✅ User context (User ID, IP address)
✅ Tenant ID (for multi-tenant isolation)
✅ Request headers (safe ones only - excludes auth, cookies, api keys)
✅ Request body (first 2000 characters only)

### What NEVER Gets Logged

❌ Authorization tokens (Bearer tokens)
❌ Cookie headers
❌ API keys and secrets
❌ X-Access-Token headers
❌ Password fields
❌ Sensitive data

### Client Response Security

**What Users See** (No sensitive details):

```json
{
  "errorCode": "ERR_DB_001",
  "message": "A system error occurred. Our team has been notified.",
  "statusCode": 500,
  "timestamp": "2024-01-15T10:30:45Z",
  "traceId": "0HN1GD7VGV5T0..." // For support ticket reference
}
```

**What Developers See** (Full details in logs):

```
ERROR: Database connection timeout
Stack Trace: at System.Data.SqlClient.SqlCommand.ExecuteReader()
Request: POST /api/products/bulk-import
User: admin@example.com
IP: 192.168.1.100
Tenant: acme-corp
```

### Multi-Tenant Isolation

- Automatic tenant ID injection in all logs
- Global query filter prevents cross-tenant queries
- Tenant context resolved before logging
- No manual tenant handling required

---

## 🎯 Exception Mapping

### Standard Error Codes

| Code | Status | Exception Type | User Message |
|------|--------|---|---|
| `ERR_VALIDATION_001` | 400 | ArgumentException | "Your request contains invalid data" |
| `ERR_BAD_REQUEST_001` | 400 | Invalid input | "Could not process your request" |
| `ERR_AUTH_001` | 401 | Unauthorized | "Invalid credentials" |
| `ERR_AUTH_002` | 401 | Token expired | "Your session has expired" |
| `ERR_FORBIDDEN_001` | 403 | Forbidden | "Insufficient permissions" |
| `ERR_NOT_FOUND_001` | 404 | KeyNotFoundException | "Resource not found" |
| `ERR_CONFLICT_001` | 409 | Duplicate | "Conflict occurred" |
| `ERR_INVALID_OP_001` | 422 | InvalidOperationException | "Operation cannot be completed" |
| `ERR_DB_001` | 500 | Database | "System error occurred" |
| `ERR_INTERNAL_001` | 500 | General | "Unexpected error occurred" |
| `ERR_TIMEOUT_001` | 504 | Timeout | "Request timeout" |

Complete list: See `Main.Common/Constants/ExceptionErrorCodes.cs`

---

## 🚀 Usage Examples

### Example 1: Automatic Exception Handling

```csharp
[HttpPost("products")]
public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
{
    // Middleware automatically catches exceptions
    // No try-catch needed for basic scenarios
    
    if (request.Price <= 0)
    {
        throw new ArgumentException("Price must be positive");
        // → Automatically mapped to ERR_INVALID_ARGUMENT_ERROR (400)
    }

    var product = await _service.CreateProductAsync(request);
    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
}
```

### Example 2: Database Error Handling

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    try
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (product == null)
        {
            throw new KeyNotFoundException($"Product {id} not found");
            // → Automatically mapped to ERR_NOT_FOUND_001 (404)
        }

        return Ok(product);
    }
    catch (SqlException ex)
    {
        // Automatically caught and logged
        // → Automatically mapped to ERR_DB_001 (500)
        throw;
    }
}
```

### Example 3: Explicit Exception Logging

```csharp
public class ReportService
{
    private readonly IExceptionLoggingService _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public async Task<Report> GenerateReportAsync(ReportRequest request)
    {
        try
        {
            // Business logic
        }
        catch (TimeoutException ex)
        {
            // Explicitly log with custom data
            await _logger.LogExceptionAsync(
                exception: ex,
                errorCode: ExceptionErrorCodes.TIMEOUT_ERROR,
                statusCode: ExceptionErrorCodes.TIMEOUT_ERROR_CODE,
                userMessage: UserFriendlyMessages.TIMEOUT_ERROR,
                customData: JsonSerializer.Serialize(new { 
                    reportId = request.ReportId,
                    generationTime = "5m 30s"
                }),
                source: "Background Job"
            );
            throw;
        }
    }
}
```

---

## 📈 Monitoring & Analytics

### Dashboard Stats API

```bash
curl -X GET "https://api.yourdomain.com/api/exceptionlogs/summary/stats" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

**Metrics Provided**:
- Total exceptions
- Unresolved count
- Today's count
- Exceptions by type (chart data)
- Exceptions by status code (chart data)
- Last 7 days trend (chart data)

### Performance Considerations

- Database indexes on: TenantId, CreatedAt, ErrorCode, StatusCode, IsResolved
- Automatic deduplication within 1-hour window
- Async logging (non-blocking)
- Efficient Serilog batching
- Log file rotation and retention

---

## ⚙️ Configuration

### Logging Settings

Default configuration in `RegisterSerilogConfiguration.cs`:

```csharp
.MinimumLevel.Information()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
.WriteTo.File(
    path: Path.Combine(loggingPath, "application-log-.txt"),
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 30)
```

### Customizing Log Level

```csharp
// More verbose logging
.MinimumLevel.Debug()

// Less verbose (production)
.MinimumLevel.Warning()

// Override specific namespace
.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
```

### Custom Log Sink

Add additional destinations (e.g., Seq, Splunk):

```csharp
.WriteTo.Seq("http://seq.yourdomain.com:5341")
```

---

## 🧹 Maintenance

### Regular Tasks

**Daily**:
- Monitor unresolved exception count
- Review critical errors (500 status)

**Weekly**:
- Check exception trends
- Resolve/investigate patterns

**Monthly**:
- Clean up old resolved exceptions
- Review error code patterns

**Quarterly**:
- Archive old log files
- Analyze exception root causes

### Cleanup Script

```bash
# Delete resolved exceptions older than 90 days
curl -X DELETE "https://api.yourdomain.com/api/exceptionlogs/cleanup?days=90" \
  -H "Authorization: Bearer ADMIN_TOKEN"

# Response
{
  "deletedCount": 250
}
```

### View Recent Errors

```bash
# Last 20 lines
tail -n 20 Logs/errors-log-*.txt

# Follow in real-time
tail -f Logs/application-log-*.txt

# Search for specific error
grep "ERR_DB_001" Logs/errors-log-*.txt
```

---

## 🐛 Troubleshooting

### Issue: Logs Not Appearing

**Check 1**: Verify log directory exists
```bash
ls -la Logs/
# Should show files like errors-log-2024-01-15.txt
```

**Check 2**: Verify Serilog is configured
```csharp
// In Program.cs
_ = builder.AddSerilogConfiguration();
```

**Check 3**: Check log level
```csharp
// Ensure level is not Warning or higher
.MinimumLevel.Information()
```

### Issue: Database Logging Fails

**Check 1**: Verify migration applied
```bash
dotnet ef database update
# Should show: "Done" with no errors
```

**Check 2**: Verify service registered
```csharp
_ = builder.Services.AddExceptionLogging();
```

**Check 3**: Check connection string
```csharp
var connectionString = configuration.GetConnectionString("DefaultConnection");
// Ensure it's valid and database is accessible
```

### Issue: API Returns 403 Forbidden

**Solution**: Verify admin authorization
```csharp
[Authorize(Roles = "Admin")]
public class ExceptionLogsController : ControllerBase { }

// User must have Admin role assigned
```

### Issue: No Tenant Isolation

**Check**: Verify tenant middleware order
```csharp
// Correct order:
_ = app.UseGlobalExceptionHandling();  // Early
_ = app.UseMiddleware<TenantResolverMiddleware>();  // After exception handling
```

---

## 📚 Documentation

- **Full Guide**: [EXCEPTION_HANDLING_GUIDE.md](./EXCEPTION_HANDLING_GUIDE.md)
- **Quick Reference**: [QUICK_REFERENCE.md](./QUICK_REFERENCE.md)
- **Code References**:
  - [ExceptionErrorCodes.cs](../Main.Common/Constants/ExceptionErrorCodes.cs)
  - [ExceptionLog.cs](../Main.Model/DomainModel/ExceptionLog.cs)
  - [ExceptionLoggingService.cs](../Main.Infrastructure/Services/ExceptionLoggingService.cs)
  - [GlobalExceptionHandlingMiddleware.cs](../Main.WebAppCore/Middleware/GlobalExceptionHandlingMiddleware.cs)

---

## 🎓 Best Practices

1. **Log at Right Level**
   - DEBUG: Detailed flow information
   - INFO: Significant application events
   - WARN: Potentially harmful situations
   - ERROR: Serious failures

2. **Include Context**
   - User ID for user-specific issues
   - Tenant ID automatically added
   - Request URL and method
   - Relevant business IDs

3. **Create Meaningful Messages**
   - "Timeout connecting to database 'ProductDb'"
   - Not: "Error occurred"

4. **Use Error Codes**
   - Standard codes for consistency
   - Makes search and filtering easier
   - API consumers rely on codes

5. **Secure Sensitive Data**
   - Middleware automatically excludes auth headers
   - Don't log passwords or tokens
   - Truncate large request bodies

6. **Monitor Regular**
   - Check dashboard daily
   - Set up alerts for critical errors
   - Review trends weekly

---

## 📝 License

This implementation is part of the Main Application project.

---

## 🤝 Support

For questions or issues:
1. Check [QUICK_REFERENCE.md](./QUICK_REFERENCE.md)
2. Review [EXCEPTION_HANDLING_GUIDE.md](./EXCEPTION_HANDLING_GUIDE.md)
3. Check log files in `/Logs` folder
4. Review source code comments

---

**Last Updated**: January 15, 2024
**Version**: 1.0.0
**Status**: Production Ready ✅


# Google AI:

No, a standard catch block inside an ASP.NET Core global exception handler middleware will not automatically stream or bubble the exception up to the browser unless you explicitly write it to the response or rethrow it.
Here is exactly how the behavior works in this scenario:
## 1. The Request Pipeline Interruption
When an exception occurs downstream, the execution stops and execution immediately jumps to your catch block. [1, 2] 

* The original response (if any was being built) is aborted.
* The browser is still waiting for an HTTP response; it has not received the exception yet. [3] 

## 2. What HandleExceptionAsync Dictates
What the browser actually sees depends entirely on what happens inside your HandleExceptionAsync method:

* If it writes to context.Response: It will send a clean, formatted HTTP response (like a 500 Internal Server Error with a JSON payload) back to the browser. The browser sees the JSON, not the raw C# exception code. [4, 5] 
* If it rethrows the exception (throw;): The exception will bubble up to the web server (like Kestrel or IIS). If no other middleware catches it, the server will terminate the request and send a generic, unhandled 500 error page or an empty response to the browser. [6] 
* If it does nothing: The pipeline considers the request "handled" successfully. If your code forgets to set an HTTP status code, the browser might unexpectedly receive a 200 OK with an empty body. [7] 

## Best Practice for HandleExceptionAsync
To ensure the browser receives a secure, readable error without exposing sensitive system details, structure your handler like this: [8] 

private static async Task HandleExceptionAsync(
    HttpContext context, 
    Exception exception, 
    IExceptionLoggingService logger, 
    ITenantSetter tenantSetter)
{
    // 1. Log the full, sensitive error internally
    logger.LogError(exception, "An unhandled exception occurred.");

    // 2. Set secure headers and status code
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    // 3. Send a safe, sanitized message to the browser
    var responsePayload = new { error = "An unexpected error occurred on the server." };
    await context.Response.WriteAsJsonAsync(responsePayload);
}

Would you like to see how to differentiate error responses based on environment (e.g., showing full details in Development but hiding them in Production), or do you need help extracting the tenant context during an error?

[1] [https://medium.com](https://medium.com/@adityaharendramall/core-java-exceptional-handling-04-control-flow-in-try-catch-methods-to-print-exception-5a7d58b9eacf)
[2] [https://medium.com](https://medium.com/@himanshupapola/deep-dive-exception-handling-565a9cab06b4)
[3] [https://www.udacity.com](https://www.udacity.com/blog/getting-started-with-try-except-in-python/)
[4] [https://medium.com](https://medium.com/@startfromlocalhost/middlewares-in-net-core-a-complete-beginner-s-guide-ba5d70a5ee8a)
[5] [https://medium.com](https://medium.com/@muratcinek/global-exception-handler-and-logging-to-elastic-stack-in-net-7-a62c43040568)
[6] [https://dev.to](https://dev.to/andytechdev/10-bad-practices-to-avoid-in-aspnet-core-api-controllers-2o9l)
[7] [https://code-maze.com](https://code-maze.com/global-error-handling-aspnetcore/)
[8] [https://betterstack.com](https://betterstack.com/community/guides/scaling-python/error-handling-fastapi/)
