-- 1. Create a server login for the Windows SYSTEM account if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'NT AUTHORITY\SYSTEM')
BEGIN
    CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master];
END
GO

-- 2. Switch to your target background worker database
USE [CloneLogDatabase];
GO

-- 3. Create a database user mapping for the login
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'NT AUTHORITY\SYSTEM')
BEGIN
    CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM];
END
GO

-- 4. Grant read and write permissions to the service account
ALTER ROLE [db_datareader] ADD MEMBER [NT AUTHORITY\SYSTEM];
ALTER ROLE [db_datawriter] ADD MEMBER [NT AUTHORITY\SYSTEM];
GO
