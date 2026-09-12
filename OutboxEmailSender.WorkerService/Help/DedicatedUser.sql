-- 1. Create a secure login on the SQL Server instance
USE [master];
GO
CREATE LOGIN [EmailWorkerUser] WITH PASSWORD = 'YourStrongPassword123!', DEFAULT_DATABASE=[CloneLogDatabase];
GO

-- 2. Switch to your target background service database
USE [CloneLogDatabase];
GO

-- 3. Create a database user linked to that login
CREATE USER [EmailWorkerUser] FOR LOGIN [EmailWorkerUser];
GO

-- 4. Grant read and write permissions to this user
ALTER ROLE [db_datareader] ADD MEMBER [EmailWorkerUser];
ALTER ROLE [db_datawriter] ADD MEMBER [EmailWorkerUser];
GO
