# Global Exception Handling & Logging System - Implementation Guide

## Overview

A comprehensive exception handling and logging system for multi-tenant .NET 8.0 MVC Core applications using **Serilog** and **Entity Framework Core**.

### Features

✅ Global exception middleware capturing all unhandled exceptions
✅ Automatic exception logging to database and files
✅ Detailed stack traces and request information for developers
✅ Generic user-friendly error messages (no sensitive data exposure)
✅ Multi-tenant isolation of exception logs
✅ Status codes and error codes for easy categorization
✅ Admin dashboard API for viewing exception logs
✅ Exception filtering, searching, and export functionality
✅ Exception trend analysis and statistics
✅ Automatic exception deduplication (logs repeat exceptions as occurrence count)
✅ File-based logging with rolling files and retention policies
✅ Support for development and production environments

---

## Architecture

### Components

1. **ExceptionLog Entity** - Database model for persisting exception details
2. **GlobalExceptionHandlingMiddleware** - Catches all unhandled exceptions
3. **ExceptionLoggingService** - Business logic for logging and retrieving exceptions
4. **ExceptionLogsController** - Admin API endpoints for managing exception logs
5. **Serilog Configuration** - Structured logging to files and console
6. **Error Constants** - Standardized error codes and status codes
7. **Exception Enums** - Types of exceptions, sources, and environments

### Flow Diagram

```
Request
   ↓
Global Exception Middleware
   ↓
[Exception Caught]
   ↓
Map to Error Code & Status Code
   ↓
Log to Serilog (File) ← Detailed Information
Log to Database      ← Long-term Storage
   ↓
Return User-Friendly Response to Client ← Generic Message (No Details)
```

---

## Installation & Setup

### Step 1: Install NuGet Packages

Add Serilog to your `Main.WebAppCore.csproj`:

```bash
dotnet add package Serilog
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
dotnet add package Serilog.Formatting.Compact
```

Or manually add to `.csproj`:

```xml
<ItemGroup>
    <PackageReference Include="Serilog" Version="3.0.1" />
    <PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="4.1.0" />
    <PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
    <PackageReference Include="Serilog.Formatting.Compact" Version="1.1.1" />
</ItemGroup>
```

### Step 2: Create Database Migration

```bash
cd Main.Migrator

# Add migration for ExceptionLogs table
dotnet ef migrations add AddExceptionLogsTable --project ..\Main.Infrastructure\

# Apply migration
dotnet ef database update
```

### Step 3: Verify Setup

Check that the following files exist:
- ✅ `Main.Model/DomainModel/ExceptionLog.cs`
- ✅ `Main.Model/Enums/ExceptionEnums.cs`
- ✅ `Main.Common/Constants/ExceptionErrorCodes.cs`
- ✅ `Main.Common/Models/ErrorResponse.cs`
- ✅ `Main.Infrastructure/RegisterSerilogConfiguration.cs`
- ✅ `Main.Infrastructure/Services/ExceptionLoggingService.cs`
- ✅ `Main.WebAppCore/Middleware/GlobalExceptionHandlingMiddleware.cs`
- ✅ `Main.WebAppCore/Controllers/Admin/ExceptionLogsController.cs`
- ✅ `Main.WebAppCore/Program.cs` (updated)
- ✅ `Main.Infrastructure/Data/ApplicationDbContext.cs` (updated)

### Step 4: Verify Program.cs Configuration

The following should be in Program.cs:

```csharp
using Main.WebAppCore.Middleware;
using Serilog;

public class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        _ = builder.AddSerilogConfiguration();
        
        // ... other configurations ...

        // Add exception logging service
        _ = builder.Services.AddExceptionLogging();
        
        var app = builder.Build();

        // Add global exception handling middleware - early in pipeline
        _ = app.UseGlobalExceptionHandling();
        
        // ... rest of middleware ...
    }
}
```

---

## Configuration

### Serilog Configuration (RegisterSerilogConfiguration.cs)

Default behavior:
- **Log Level**: Information and above
- **Log Directory**: `{Application Root}/Logs/`
- **Rolling**: Daily files with 30-day retention
- **File Names**:
  - `application-log-YYYY-MM-DD.txt` - All logs
  - `errors-log-YYYY-MM-DD.txt` - Errors only
  - `exceptions-log-YYYY-MM-DD.json` - Exceptions in JSON format

### Customization

To customize logging, modify `RegisterSerilogConfiguration.cs`:

```csharp
.MinimumLevel.Information()
.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)

.WriteTo.File(
    path: Path.Combine(loggingPath, "custom-log-.txt"),
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 30,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}")
```

---

## Exception Mapping

### Error Codes & Status Codes

| Error Type | Error Code | Status Code | User Message |
|---|---|---|---|
| Validation Error | `ERR_VALIDATION_001` | 400 | "Your request contains invalid data" |
| Bad Request | `ERR_BAD_REQUEST_001` | 400 | "Could not process request" |
| Authentication Error | `ERR_AUTH_001` | 401 | "Invalid credentials" |
| Token Expired | `ERR_AUTH_002` | 401 | "Session expired" |
| Forbidden | `ERR_FORBIDDEN_001` | 403 | "No permission" |
| Not Found | `ERR_NOT_FOUND_001` | 404 | "Resource not found" |
| Conflict | `ERR_CONFLICT_001` | 409 | "Conflict occurred" |
| Database Error | `ERR_DB_001` | 500 | "System error occurred" |
| Internal Server Error | `ERR_INTERNAL_001` | 500 | "Unexpected error" |
| Service Unavailable | `ERR_SERVICE_001` | 503 | "Service unavailable" |
| Network Error | `ERR_NETWORK_001` | 503 | "Network error" |
| Timeout | `ERR_TIMEOUT_001` | 504 | "Request timeout" |

See `Main.Common/Constants/ExceptionErrorCodes.cs` for complete list.

---

## API Endpoints

### Exception Logs Controller

**Base URL**: `/api/exceptionlogs`

**Authorization**: Admin role required (except where noted)

#### 1. Search Exception Logs

```http
POST /api/exceptionlogs/search
Authorization: Bearer {token}
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

**Response**:
```json
{
    "items": [
        {
            "id": 123,
            "exceptionType": "SqlException",
            "statusCode": 500,
            "errorCode": "ERR_DB_001",
            "detailedMessage": "Timeout expired. The timeout period elapsed...",
            "stackTrace": "at ...",
            "requestUrl": "https://api.example.com/api/products",
            "httpMethod": "GET",
            "userId": "user123",
            "clientIpAddress": "192.168.1.1",
            "createdAt": "2024-01-15T10:30:00Z",
            "source": "API",
            "environment": "Production",
            "isResolved": false,
            "occurrenceCount": 5
        }
    ],
    "totalCount": 1,
    "pageNumber": 1,
    "pageSize": 20,
    "totalPages": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
}
```

#### 2. Get Single Exception Log

```http
GET /api/exceptionlogs/{id}
Authorization: Bearer {token}
```

#### 3. Get Exception Summary/Dashboard

```http
GET /api/exceptionlogs/summary/stats
Authorization: Bearer {token}
```

**Response**:
```json
{
    "totalExceptions": 150,
    "unresolvedCount": 45,
    "todayCount": 12,
    "exceptionsByType": {
        "SqlException": 65,
        "ArgumentNullException": 30,
        "InvalidOperationException": 25
    },
    "exceptionsByStatus": {
        "500": 80,
        "400": 40,
        "403": 30
    },
    "trends": [
        {"date": "2024-01-10T00:00:00Z", "count": 5},
        {"date": "2024-01-11T00:00:00Z", "count": 8},
        {"date": "2024-01-12T00:00:00Z", "count": 12}
    ]
}
```

#### 4. Mark Exception as Resolved

```http
PUT /api/exceptionlogs/{id}/resolve
Authorization: Bearer {token}
Content-Type: application/json

{
    "notes": "Fixed in commit abc123. Updated connection string."
}
```

#### 5. Export Exceptions to CSV

```http
POST /api/exceptionlogs/export/csv
Authorization: Bearer {token}
Content-Type: application/json

{
    "statusCode": 500,
    "startDate": "2024-01-01T00:00:00Z",
    "endDate": "2024-12-31T23:59:59Z",
    "isResolved": false
}
```

**Response**: CSV file download

#### 6. Cleanup Old Exceptions

```http
DELETE /api/exceptionlogs/cleanup?days=90
Authorization: Bearer {token}
X-Role: SuperAdmin
```

**Response**:
```json
{
    "deletedCount": 250
}
```

---

## Usage Examples

### Example 1: Handling Validation Exception

```csharp
[HttpPost("users")]
public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
{
    // Validation happens in middleware automatically
    // If request is null or invalid, GlobalExceptionHandlingMiddleware catches it
    
    if (string.IsNullOrWhiteSpace(request.Email))
    {
        throw new ArgumentException("Email is required");
        // Automatically mapped to: ERR_INVALID_ARGUMENT_ERROR, 400
    }

    // ... rest of logic ...
    return Ok(user);
}
```

### Example 2: Database Error Handling

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetProduct(int id)
{
    try
    {
        var product = await _dbContext.Products.FindAsync(id);
        
        if (product == null)
        {
            throw new KeyNotFoundException($"Product {id} not found");
            // Automatically mapped to: ERR_NOT_FOUND_001, 404
        }
        
        return Ok(product);
    }
    catch (SqlException ex)
    {
        // Automatically caught by middleware
        // Automatically mapped to: ERR_DB_001, 500
        // Logged with full stack trace and details
        throw;
    }
}
```

### Example 3: Explicit Exception Logging

```csharp
public class OrderService
{
    private readonly IExceptionLoggingService _exceptionLogger;
    
    public OrderService(IExceptionLoggingService exceptionLogger)
    {
        _exceptionLogger = exceptionLogger;
    }
    
    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        try
        {
            // Business logic
        }
        catch (Exception ex)
        {
            // Log specific exception if needed
            await _exceptionLogger.LogExceptionAsync(
                exception: ex,
                errorCode: ExceptionErrorCodes.INVALID_OPERATION,
                statusCode: ExceptionErrorCodes.INVALID_OPERATION_CODE,
                userMessage: UserFriendlyMessages.INVALID_OPERATION,
                customData: JsonSerializer.Serialize(new { OrderId = request.OrderId })
            );
            throw;
        }
    }
}
```

---

## Log File Structure

### Example: application-log-2024-01-15.txt

```
2024-01-15 10:30:45.123 +00:00 [ERR] [Main.WebAppCore.Controllers.ProductController] 
Timeout expired. The timeout period elapsed before completion of operation.
{"Application":"Main.WebAppCore","Environment":"Production","ThreadId":5,"MachineName":"SERVER01"}
System.Data.SqlClient.SqlException: Timeout expired...
    at System.Data.SqlClient.SqlCommand.ExecuteReader()
    at Main.Infrastructure.Repository.ProductRepository.GetProductAsync()
```

### Example: exceptions-log-2024-01-15.json

```json
{
  "@t":"2024-01-15T10:30:45.1234567Z",
  "@mt":"Exception occurred - ErrorCode: {ErrorCode}, StatusCode: {StatusCode}",
  "@l":"Error",
  "ErrorCode":"ERR_DB_001",
  "StatusCode":500,
  "@x":"System.Data.SqlClient.SqlException: ...",
  "Application":"Main.WebAppCore",
  "Environment":"Production"
}
```

---

## Client Response Examples

### Success Response (200 OK)

```json
{
  "id": 123,
  "name": "Product A",
  "price": 99.99
}
```

### Error Response (4xx/5xx)

**Development**:
```json
{
  "errorCode": "ERR_DB_001",
  "message": "A system error occurred while processing your request. Our team has been notified.",
  "statusCode": 500,
  "timestamp": "2024-01-15T10:30:45Z",
  "path": "/api/products/123",
  "traceId": "0HN1GD... (for correlation)"
}
```

**Production** (Same - no details exposed):
```json
{
  "errorCode": "ERR_DB_001",
  "message": "A system error occurred while processing your request. Our team has been notified.",
  "statusCode": 500,
  "timestamp": "2024-01-15T10:30:45Z",
  "path": "/api/products/123",
  "traceId": "0HN1GD..."
}
```

---

## Security Considerations

### What Gets Logged (Safe for Developers)
✅ Full exception message and stack trace
✅ Request URL and HTTP method
✅ Request headers (excluding sensitive ones)
✅ Request body (limited to 2000 chars)
✅ User ID and IP address
✅ Tenant ID (multi-tenant isolation)

### What Doesn't Get Logged (Security)
❌ Authorization header (Bearer token)
❌ Cookie header
❌ X-Api-Key
❌ X-Access-Token
❌ X-Secret-Token
❌ Password fields

### Client Response (Safe for Users)
❌ Actual exception details
❌ Stack trace information
❌ Database query details
❌ Internal file paths
❌ System configuration

### Multi-Tenant Isolation
- Exception logs are automatically scoped to the current tenant
- Query filters ensure tenants only see their own logs
- Global query filter prevents cross-tenant data leaks

---

## Performance & Optimization

### Database Indexes
Indexes are created for:
- `TenantId` - Fast filtering by tenant
- `CreatedAt` - Fast sorting and date filtering
- `ErrorCode` - Quick error type lookup
- `StatusCode` - Status code filtering
- `IsResolved` - Resolved status filtering
- `UserId` - User-specific error tracking

### Logging Performance
- Asynchronous database writes
- File logging doesn't block requests
- Serilog uses efficient batching
- Old logs automatically cleaned up (90-day retention default)

### Deduplication
Exceptions are deduplicated if:
- Same exception type, error code, and status code
- Occurred within 1 hour
- Not yet resolved

---

## Troubleshooting

### Logs Not Appearing

1. **Check Log Directory**:
   ```bash
   # Logs should be in: {Application Root}/Logs/
   ls Logs/
   ```

2. **Verify Serilog Configuration**:
   ```csharp
   // In Program.cs, ensure this runs:
   _ = builder.AddSerilogConfiguration();
   ```

3. **Check Environment Variable**:
   ```bash
   echo $ASPNETCORE_ENVIRONMENT  # Should be Development, Staging, or Production
   ```

### Database Logging Fails

1. **Verify DbContext Registration**:
   ```csharp
   _ = builder.Services.AddDatabase(builder.Configuration);
   ```

2. **Check Migration Applied**:
   ```bash
   dotnet ef database update
   ```

3. **Verify IExceptionLoggingService Registered**:
   ```csharp
   _ = builder.Services.AddExceptionLogging();
   ```

### API Endpoint Returns 403 Forbidden

- Verify user has `Admin` role
- Check authorization policies configured in `AddAuthorization()`

### No Tenant Isolation

- Verify `ITenantContext` is registered
- Check `TenantResolverMiddleware` runs before `GlobalExceptionHandlingMiddleware`

---

## Database Schema

### ExceptionLogs Table

| Column | Type | Length | Nullable | Description |
|---|---|---|---|---|
| Id | bigint | - | No | Primary Key |
| TenantId | nvarchar | 450 | No | Multi-tenant FK |
| ExceptionType | nvarchar | 255 | No | Exception class name |
| StatusCode | int | - | No | HTTP status code |
| ErrorCode | nvarchar | 50 | No | Internal error code |
| DetailedMessage | nvarchar | MAX | No | Exception message |
| StackTrace | nvarchar | MAX | Yes | Stack trace |
| InnerException | nvarchar | MAX | Yes | Inner exception details |
| UserMessage | nvarchar | 500 | No | User-friendly message |
| RequestUrl | nvarchar | 500 | Yes | Request URL |
| HttpMethod | nvarchar | 10 | Yes | GET, POST, etc. |
| RequestHeaders | nvarchar | MAX | Yes | Serialized headers |
| RequestBody | nvarchar | MAX | Yes | Serialized body (truncated) |
| UserId | nvarchar | MAX | Yes | User ID |
| ClientIpAddress | nvarchar | 45 | Yes | Client IP |
| CreatedAt | datetime2 | - | No | Exception time |
| Source | nvarchar | 50 | Yes | API, WebUI, Job, etc. |
| Environment | nvarchar | 50 | Yes | Dev, Staging, Prod |
| CustomData | nvarchar | MAX | Yes | Custom JSON data |
| IsResolved | bit | - | No | Resolution status |
| ResolutionNotes | nvarchar | MAX | Yes | Developer notes |
| ResolvedAt | datetime2 | - | Yes | Resolution timestamp |
| OccurrenceCount | int | - | No | Repeat occurrence count |

---

## Next Steps

1. ✅ Apply database migration
2. ✅ Install NuGet packages
3. ✅ Restart application
4. ✅ Test by triggering exception
5. ✅ Check logs in `/Logs` folder
6. ✅ Query API endpoints
7. ✅ Create admin dashboard UI (optional)

---

## Support & Maintenance

### Monitoring Checklist
- [ ] Check unresolved exception count daily
- [ ] Review critical errors (status 500) immediately
- [ ] Run cleanup task monthly
- [ ] Archive old logs quarterly
- [ ] Update error codes as new patterns emerge

### Maintenance Tasks
```bash
# Clean up old logs manually
dotnet ef database update AddExceptionLogsTable

# View log files
tail -f Logs/application-log-*.txt

# Archive logs
tar -czf logs-2024-01.tar.gz Logs/
```

---

## Additional Resources

- [Serilog Documentation](https://serilog.net/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/)
- [HTTP Status Codes](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)
