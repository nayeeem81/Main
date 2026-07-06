# Exception Handling System - Deployment Guide

## Pre-Deployment Checklist

### 1. Configuration Review

#### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",  // Production: Warning or Error
      "Microsoft": "Error",  // Reduce Microsoft logs
      "Microsoft.EntityFrameworkCore": "Error"
    }
  },
  "ExceptionHandling": {
    "LogToDatabase": true,
    "LogToFile": true,
    "RetentionDays": 90,    // Adjust based on storage
    "IncludeSensitiveData": false
  }
}
```

#### Environment Setup
- [ ] `ASPNETCORE_ENVIRONMENT=Production` set
- [ ] Application root folder has write permissions (for Logs directory)
- [ ] SQL Server connection string verified
- [ ] Database backups configured

### 2. Security Checklist

- [ ] Authorization header filtering verified
- [ ] Cookie filtering verified
- [ ] API key filtering verified
- [ ] No sensitive data in custom fields
- [ ] Request body truncation set to 2000 chars
- [ ] Admin role authorization enforced
- [ ] SSL/TLS enabled for API endpoints
- [ ] CORS properly configured

### 3. Performance Tuning

#### Database Indexes
Verify all 6 indexes created:
```sql
SELECT * FROM sys.indexes WHERE object_name(object_id) = 'ExceptionLogs'
```

Should show:
- PK_ExceptionLogs (TenantId + Id)
- IX_ExceptionLogs_TenantId
- IX_ExceptionLogs_CreatedAt
- IX_ExceptionLogs_ErrorCode
- IX_ExceptionLogs_StatusCode
- IX_ExceptionLogs_IsResolved
- IX_ExceptionLogs_UserId

#### Log File Configuration
```csharp
// Production recommendation:
.WriteTo.File(
    path: Path.Combine(loggingPath, "application-log-.txt"),
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 30,  // Keep 30 days
    fileSizeLimitBytes: 1073741824,  // 1 GB per file
    rollOnFileSizeLimit: true)
```

#### Database Maintenance
- [ ] Database backups scheduled (daily minimum)
- [ ] Cleanup task scheduled (monthly, deletes resolved exceptions > 90 days)
- [ ] Index maintenance scheduled (weekly)
- [ ] Query performance monitored

### 4. Monitoring Setup

#### Log File Monitoring
- [ ] Monitor `/Logs` directory size
- [ ] Set up alerts if size exceeds threshold
- [ ] Archive old logs to cold storage (optional)

#### Database Monitoring
```sql
-- Monitor table size
SELECT OBJECT_NAME(p.object_id) AS TableName,
       SUM(p.rows) AS RowCount,
       CAST(SUM(au.total_pages) * 8 / 1024.0 AS DECIMAL(18, 2)) AS SizeMB
FROM sys.partitions p
JOIN sys.allocation_units au ON p.partition_id = au.container_id
WHERE OBJECT_NAME(p.object_id) = 'ExceptionLogs'
GROUP BY OBJECT_NAME(p.object_id)
```

#### Exception Monitoring
- [ ] Set up alert for total exceptions > threshold
- [ ] Set up alert for critical errors (500 status)
- [ ] Review unresolved count daily
- [ ] Monitor trends weekly

### 5. Documentation Deployment

- [ ] README_EXCEPTION_HANDLING.md deployed to project
- [ ] QUICK_REFERENCE.md shared with dev team
- [ ] API endpoints documented in API portal
- [ ] Error codes listed in internal wiki
- [ ] Troubleshooting guide accessible to support team

---

## Deployment Steps

### Step 1: Database Migration

```bash
# Connect to production server
# On deployment machine or server with SQL access:

# Create backup of existing database
BACKUP DATABASE [MainDb] TO DISK = N'C:\Backups\MainDb_PreException_Backup.bak'

# Apply migration
dotnet ef database update --configuration Release

# Verify
SELECT COUNT(*) FROM ExceptionLogs  -- Should return 0
```

### Step 2: Code Deployment

```bash
# Build release version
dotnet build --configuration Release

# Publish
dotnet publish --configuration Release --output ./publish

# Deploy to server
# Copy files to: C:\inetpub\wwwroot\MainApp (or your deployment path)
```

### Step 3: File System Setup

```powershell
# Create Logs directory
New-Item -ItemType Directory -Path "C:\inetpub\wwwroot\MainApp\Logs" -Force

# Set permissions (IIS App Pool identity needs write access)
$acl = Get-Acl "C:\inetpub\wwwroot\MainApp\Logs"
$ar = New-Object System.Security.AccessControl.FileSystemAccessRule("IIS_IUSRS","FullControl","Allow")
$acl.SetAccessRule($ar)
Set-Acl "C:\inetpub\wwwroot\MainApp\Logs" $acl

# Verify
Get-Acl "C:\inetpub\wwwroot\MainApp\Logs" | Format-List
```

### Step 4: Application Pool Setup (IIS)

1. Create/update App Pool:
   - Name: `MainApp`
   - .NET CLR Version: No Managed Code (for ASP.NET Core)
   - Managed Pipeline Mode: Integrated

2. Configure Recycling:
   - Recycle periodically: 4 hours
   - Recycle on demand: Enabled
   - Disable private memory: 500 MB

3. Configure Identity:
   - Identity: ApplicationPoolIdentity or specific user
   - Verify it has write access to Logs directory

### Step 5: Website Binding (IIS)

1. Add application:
   - Path: `/`
   - Physical Path: `C:\inetpub\wwwroot\MainApp`
   - App Pool: MainApp
   - Enable URL Rewrite

2. Configure SSL/TLS:
   - Bind HTTPS with valid certificate
   - Enforce HTTPS redirect

### Step 6: Environment Variables

Set on server:
```
ASPNETCORE_ENVIRONMENT=Production
DOTNET_ENVIRONMENT=Production
```

IIS: Add to Application Pool advanced settings or web.config:
```xml
<system.webServer>
  <aspNetCore processPath="dotnet" arguments=".\Main.WebAppCore.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout">
    <environmentVariables>
      <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
      <environmentVariable name="DOTNET_ENVIRONMENT" value="Production" />
    </environmentVariables>
  </aspNetCore>
</system.webServer>
```

### Step 7: Verify Deployment

```bash
# Check application is running
curl https://yourdomain.com/

# Verify API accessible
curl https://yourdomain.com/api/exceptionlogs/summary/stats \
  -H "Authorization: Bearer ADMIN_TOKEN"

# Check logs created
# Should see files in Logs directory within 5 minutes
ls -la C:\inetpub\wwwroot\MainApp\Logs\
```

### Step 8: Health Check

```powershell
# Test exception logging
$response = Invoke-WebRequest -Uri "https://yourdomain.com/api/invalid" -ErrorAction SilentlyContinue
Write-Host "Status: $($response.StatusCode)"

# Wait 1 minute for logs to be written
Start-Sleep -Seconds 60

# Verify logs created
Get-ChildItem C:\inetpub\wwwroot\MainApp\Logs\

# Verify database entry
# Execute: SELECT TOP 1 * FROM ExceptionLogs ORDER BY CreatedAt DESC
```

---

## Post-Deployment Validation

### 1. Application Functionality

- [ ] Application loads without errors
- [ ] No critical errors in Event Log
- [ ] Response times acceptable
- [ ] Memory usage stable
- [ ] CPU usage normal

### 2. Exception Logging

- [ ] Logs directory created
- [ ] Log files being written:
  - [ ] application-log-YYYY-MM-DD.txt
  - [ ] errors-log-YYYY-MM-DD.txt
  - [ ] exceptions-log-YYYY-MM-DD.json

- [ ] Database entries created
  - [ ] ExceptionLogs table has records
  - [ ] TenantId populated correctly
  - [ ] Timestamps accurate

### 3. API Functionality

All admin endpoints should work:

```bash
# 1. Search
curl -X POST https://yourdomain.com/api/exceptionlogs/search \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer ADMIN_TOKEN" \
  -d '{}'

# 2. Stats
curl -X GET https://yourdomain.com/api/exceptionlogs/summary/stats \
  -H "Authorization: Bearer ADMIN_TOKEN"

# 3. Export
curl -X POST https://yourdomain.com/api/exceptionlogs/export/csv \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer ADMIN_TOKEN" \
  -d '{}'
```

### 4. Security Verification

- [ ] No authorization tokens in logs
- [ ] No API keys in logs
- [ ] No passwords in logs
- [ ] User-friendly error messages shown
- [ ] Admin API requires authentication
- [ ] HTTPS enforced

### 5. Performance Baseline

Establish baseline metrics:
```sql
-- Record baseline
SELECT COUNT(*) as TotalExceptions FROM ExceptionLogs
SELECT AVG(DATEDIFF(ms, CreatedAt, GETDATE())) as AvgAge FROM ExceptionLogs
SELECT MAX(OccurrenceCount) as MaxOccurrences FROM ExceptionLogs
```

---

## Ongoing Maintenance

### Daily Tasks
- [ ] Review critical exceptions (500 status)
- [ ] Check unresolved exception count
- [ ] Monitor disk space for logs

### Weekly Tasks
- [ ] Review exception trends
- [ ] Check API response times
- [ ] Verify backups completed
- [ ] Analyze patterns for fixes

### Monthly Tasks
- [ ] Run cleanup (delete resolved exceptions > 90 days):
  ```bash
  curl -X DELETE "https://yourdomain.com/api/exceptionlogs/cleanup?days=90" \
    -H "Authorization: Bearer ADMIN_TOKEN"
  ```

- [ ] Archive old log files
- [ ] Update error codes if needed
- [ ] Review retention policies

### Quarterly Tasks
- [ ] Performance analysis
- [ ] Capacity planning
- [ ] Security audit
- [ ] Update documentation

---

## Rollback Plan

If issues arise post-deployment:

### Option 1: Disable Exception Middleware (Quick)
```csharp
// In Program.cs, comment out:
// _ = app.UseGlobalExceptionHandling();

// Application will still work, exceptions won't be logged
```

### Option 2: Restore Database (Safe)
```sql
-- Restore from backup if database changes caused issues
RESTORE DATABASE [MainDb] FROM DISK = N'C:\Backups\MainDb_PreException_Backup.bak'
```

### Option 3: Full Rollback
```bash
# Stop application
# Deploy previous version
# Clear Logs directory (optional)
# Restart application
```

---

## Troubleshooting Production Issues

### Issue: Application crashes on startup

**Check 1**: Verify Serilog configuration
```xml
<!-- Check appsettings.json for valid Serilog section -->
```

**Check 2**: Verify database connection
```sql
-- Test connection from server
sqlcmd -S SERVER\INSTANCE -U sa -P PASSWORD -Q "SELECT 1"
```

**Check 3**: Verify Logs directory permissions
```powershell
# Should have write permissions for App Pool identity
icacls "C:\inetpub\wwwroot\MainApp\Logs"
```

### Issue: API endpoints return 403

**Solution**: Verify user has Admin role
```sql
-- Check user roles in database
SELECT u.Id, u.UserName, ur.RoleId, r.Name 
FROM AspNetUsers u
JOIN AspNetUserRoles ur ON u.Id = ur.UserId
JOIN AspNetRoles r ON ur.RoleId = r.Id
WHERE r.Name = 'Admin'
```

### Issue: Logs not being written

**Check 1**: Verify Logs directory exists
```powershell
Test-Path "C:\inetpub\wwwroot\MainApp\Logs"
```

**Check 2**: Verify file permissions
```powershell
icacls "C:\inetpub\wwwroot\MainApp\Logs" /grant "IIS_IUSRS:F"
```

**Check 3**: Trigger an exception and wait 1 minute

### Issue: Database storage growing too fast

**Solution**: Run cleanup task more frequently or with longer retention
```bash
# Delete exceptions > 30 days (instead of 90)
curl -X DELETE "https://yourdomain.com/api/exceptionlogs/cleanup?days=30" \
  -H "Authorization: Bearer ADMIN_TOKEN"
```

### Issue: Slow database queries

**Solution**: Verify indexes exist and update statistics
```sql
-- Update statistics
EXEC sp_updatestats

-- Rebuild fragmented indexes
ALTER INDEX IX_ExceptionLogs_CreatedAt ON ExceptionLogs REBUILD
```

---

## Monitoring Scripts

### PowerShell Monitoring
```powershell
# Daily health check script
$logDir = "C:\inetpub\wwwroot\MainApp\Logs"
$logFiles = Get-ChildItem $logDir -Filter "errors-log-*.txt"

foreach ($file in $logFiles) {
    $errors = @(Get-Content $file | Measure-Object -Line).Lines
    Write-Host "File: $($file.Name) - Errors: $errors"
}

# Check database
$query = "SELECT COUNT(*) as UnresolvedCount FROM ExceptionLogs WHERE IsResolved = 0"
# Execute query and alert if > threshold
```

### SQL Monitoring
```sql
-- Create job to cleanup old exceptions
USE [msdb]
EXEC sp_add_job @job_name = 'Cleanup_ExceptionLogs'
-- Configure to run monthly

-- Create alert for high exception count
-- Alert if COUNT(*) > 1000
```

---

## Support & Documentation

### For Developers
- Share: QUICK_REFERENCE.md
- Share: ExceptionErrorCodes.cs constants
- Share: API endpoint documentation

### For Operations
- Share: Deployment guide (this document)
- Share: Monitoring checklist
- Share: Troubleshooting guide

### For Support Team
- Share: Common error codes
- Share: How to search logs
- Share: When to escalate

---

## Sign-Off

- [ ] Development team approved
- [ ] QA testing passed
- [ ] Operations team trained
- [ ] Database backed up
- [ ] Monitoring configured
- [ ] Documentation complete
- [ ] Rollback plan documented

**Deployment Date**: _______________
**Deployed By**: _______________
**Approved By**: _______________

---

**Document Version**: 1.0
**Last Updated**: January 15, 2024
**Status**: Ready for Deployment ✅
