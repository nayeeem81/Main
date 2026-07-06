# Exception Handling - Quick Reference

## Installation

```bash
# 1. Install Serilog packages
dotnet add package Serilog Serilog.AspNetCore Serilog.Sinks.File Serilog.Formatting.Compact

# 2. Create migration
dotnet ef migrations add AddExceptionLogsTable --project ..\Main.Infrastructure\

# 3. Apply migration
dotnet ef database update

# 4. Restart application
```

## What's New

| Component | Location | Purpose |
|-----------|----------|---------|
| **ExceptionLog** | `Main.Model/DomainModel/` | Database entity for exception storage |
| **Error Codes** | `Main.Common/Constants/` | Standardized error codes and messages |
| **Middleware** | `Main.WebAppCore/Middleware/` | Global exception handler |
| **Service** | `Main.Infrastructure/Services/` | Exception logging business logic |
| **API** | `Main.WebAppCore/Controllers/Admin/` | Admin endpoints for exception logs |
| **Config** | `Main.Infrastructure/` | Serilog configuration |
| **Logs** | `{App Root}/Logs/` | Log files (auto-created) |

## Exception Mapping

```
ArgumentException → ERR_INVALID_ARGUMENT_ERROR (400)
ArgumentNullException → ERR_NULL_REFERENCE_ERROR (500)
KeyNotFoundException → ERR_NOT_FOUND_001 (404)
TimeoutException → ERR_TIMEOUT_ERROR (504)
HttpRequestException → ERR_NETWORK_ERROR (503)
IOException → ERR_IO_ERROR (500)
FileNotFoundException → ERR_FILE_NOT_FOUND (500)
DbUpdateException → ERR_DATA_INTEGRITY_ERROR (500)
DbUpdateConcurrencyException → ERR_CONFLICT_001 (409)
OperationCanceledException → ERR_TIMEOUT_ERROR (504)
InvalidOperationException → ERR_INVALID_OPERATION (422)
```

See `Main.Common/Constants/ExceptionErrorCodes.cs` for all mappings.

## API Endpoints

### Search Exceptions
```http
POST /api/exceptionlogs/search
{
    "errorCode": "ERR_DB_001",
    "statusCode": 500,
    "isResolved": false,
    "pageNumber": 1,
    "pageSize": 20
}
```

### Get Single Exception
```http
GET /api/exceptionlogs/{id}
```

### Get Dashboard Stats
```http
GET /api/exceptionlogs/summary/stats
```

### Mark Resolved
```http
PUT /api/exceptionlogs/{id}/resolve
{
    "notes": "Fixed in v1.2.3"
}
```

### Export CSV
```http
POST /api/exceptionlogs/export/csv
```

### Cleanup Old Logs
```http
DELETE /api/exceptionlogs/cleanup?days=90
```

## Log Files

**Location**: `{Application Root}/Logs/`

- `application-log-YYYY-MM-DD.txt` - All logs (30-day rolling)
- `errors-log-YYYY-MM-DD.txt` - Errors only (30-day rolling)
- `exceptions-log-YYYY-MM-DD.json` - Exceptions JSON (30-day rolling)

## Developer Tips

### View Recent Errors
```bash
# Last 50 lines of today's error log
tail -n 50 Logs/errors-log-*.txt

# Follow log in real-time
tail -f Logs/application-log-*.txt
```

### Search Logs
```bash
# Find specific error code
grep "ERR_DB_001" Logs/application-log-*.txt

# Find specific user's errors
grep "user123" Logs/application-log-*.txt

# Find network errors
grep "ERR_NETWORK" Logs/errors-log-*.txt
```

### Error Codes Cheat Sheet

| Code | Status | Meaning |
|------|--------|---------|
| ERR_VALIDATION_001 | 400 | Invalid input |
| ERR_AUTH_001 | 401 | Invalid credentials |
| ERR_FORBIDDEN_001 | 403 | Insufficient permissions |
| ERR_NOT_FOUND_001 | 404 | Resource not found |
| ERR_CONFLICT_001 | 409 | Conflict/duplicate |
| ERR_DB_001 | 500 | Database error |
| ERR_INTERNAL_001 | 500 | Unexpected error |
| ERR_SERVICE_001 | 503 | Service unavailable |
| ERR_NETWORK_001 | 503 | Network error |
| ERR_TIMEOUT_001 | 504 | Request timeout |

## Auto-Logged Information

✅ **What's Logged**:
- Exception type, message, stack trace
- HTTP method, URL, status code
- User ID and IP address
- Tenant ID (isolated per tenant)
- Request headers (safe ones only)
- Request body (first 2000 chars)
- Timestamp and environment

❌ **What's NOT Logged**:
- Authorization tokens/bearers
- Cookies
- API keys and secrets
- Sensitive headers
- User passwords

## What Clients See

**Error Response** (Never shows details):
```json
{
  "errorCode": "ERR_DB_001",
  "message": "A system error occurred. Our team has been notified.",
  "statusCode": 500,
  "timestamp": "2024-01-15T10:30:45Z",
  "traceId": "0HN1GD..." // For support
}
```

## Tenant Isolation

Exception logs are automatically scoped to current tenant:
- Tenant filter applied in middleware
- Global query filter in DbContext
- No cross-tenant data leaks possible

## Performance

- **Async logging** - Non-blocking
- **Database indexes** - Fast queries
- **Deduplication** - Repeating exceptions increment counter
- **Auto-cleanup** - Old logs retained 90 days default
- **Batching** - Efficient Serilog writes

## Common Issues

| Issue | Solution |
|-------|----------|
| Logs not appearing | Check `{App Root}/Logs/` folder exists and is writable |
| API returns 403 | Verify user has `Admin` role |
| No database logging | Verify migration applied: `dotnet ef database update` |
| No tenant isolation | Check `TenantResolverMiddleware` runs before exception middleware |

## Next Steps

1. Install packages: `dotnet add package Serilog...`
2. Run migration: `dotnet ef database update`
3. Restart app
4. Test with invalid request → Check `/Logs` folder
5. Query `/api/exceptionlogs/summary/stats` → Should see data
6. Build admin dashboard UI (optional)

## References

- Full guide: [EXCEPTION_HANDLING_GUIDE.md](./EXCEPTION_HANDLING_GUIDE.md)
- Error codes: [ExceptionErrorCodes.cs](../Main.Common/Constants/ExceptionErrorCodes.cs)
- Logging service: [ExceptionLoggingService.cs](../Main.Infrastructure/Services/ExceptionLoggingService.cs)
- Middleware: [GlobalExceptionHandlingMiddleware.cs](../Main.WebAppCore/Middleware/GlobalExceptionHandlingMiddleware.cs)
