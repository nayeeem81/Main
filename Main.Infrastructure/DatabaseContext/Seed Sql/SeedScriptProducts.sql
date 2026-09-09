USE [IdentityDatabase]

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000001-0000-0000-0000-000000000000', N'Tenant 1', 0, N'tenant1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000002-0000-0000-0000-000000000000', N'Tenant 2', 0, N'tenant2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000003-0000-0000-0000-000000000000', N'finearts', 0, N'finearts', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

INSERT [dbo].[Tenants] ([TenantId], [TenantName], [HostType], [Host], [SecretKey], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) 
VALUES (N'00000004-0000-0000-0000-000000000000', N'lifestyles', 0, N'lifestyles', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL)

GO

USE [CloneTenantDatabase]
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (19, 1, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (20, 3, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (21, 8, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (22, 4, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent])VALUES (23, 5, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (24, 6, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (25, 2, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (26, 7, N'00000003-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO

USE [CloneTenantDatabase]
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (28, 1, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (29, 3, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (30, 8, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (31, 4, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent])VALUES (32, 5, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (33, 6, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (34, 2, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
INSERT [dbo].[Pages] ([PageID], [EnumPublicPage], [MyTenantId], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive], [TenantCountry], [TenantContinent]) VALUES (35, 7, N'00000011-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, NULL)
GO
