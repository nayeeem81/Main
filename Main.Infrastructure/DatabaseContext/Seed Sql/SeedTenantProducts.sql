USE [CloneTenantDatabase]

INSERT [dbo].[Products] 
(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
1,   -- ProductID
4,   -- PostType
N' A macaw is a type of large, long-tailed New World parrot belonging to the tribe Arini. Famous for their vibrant, colorful plumage and exceptional intelligence, these birds are native to the tropical regions of Central and South America, as well as Mexico.  ',  -- ProductName
N' 🎨 Macaws are among the most celebrated subjects in wildlife art due to their explosive, naturally vivid color palettes and expressive features. Artists utilize various mediums to capture their tropical majesty, each bringing a unique texture and mood to the canvas.

🎨 Oil & Acrylic PaintingsTraditional oil and acrylic mediums excel at capturing the rich depth, intricate feather textures, and intense lighting of a tropical environment. These works often focus on high realism or vivid impressionism, placing the macaw within its natural rainforest canopy.

💧 Watercolor Illustrations Watercolor paintings offer a lighter, more fluid approach. The natural bleeding of watercolor pigments perfectly mimics the soft gradient transition of a macaw''s feathers, often complemented by abstract background splashes for a modern aesthetic.

💧 Watercolor IllustrationsWatercolor paintings offer a lighter, more fluid approach. The natural bleeding of watercolor pigments perfectly mimics the soft gradient transition of a macaw''s feathers, often complemented by abstract background splashes for a modern aesthetic.Beautiful Colored Yellow-blue Macaw Parrot Watercolor Stock ...',  -- Description
5,  -- CategoryID
18,  --  SubCategoryID (painting)
CAST(5000.00 AS Decimal(3200, 2)), --  Price
CAST(5.00 AS Decimal(5, 2)), --  Discount
CAST(5.00 AS Decimal(5, 2)),  --  SaleCommission
N'Oil & Acrylic, Watercolor paintings, wildlife art', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO

INSERT [dbo].[ProductImageFiles] ([ProductImageFileID], [FileContent], [FiePath], [ProductID], [TenantContinent], [MyTenantId], [TenantCountry], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive]) VALUES
(
1, -- ProductImageFileID (PK)
NULL,  -- FileContent
N'/TenantProducts/IMG_20260617_203406.jpg', -- FiePath (macaow)
1,  -- ProductID (FK)
NULL, 
N'00000001-0000-0000-0000-000000000000', -- MyTenantId
1, N'00000006-0000-0000-0000-000000000000', NULL, NULL, CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2), NULL, NULL, 1
)

GO

INSERT [dbo].[Products] 
(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
2,   -- ProductID
4,   -- PostType
N'   🏔️ Fine Art Landscape Paintings Professional oil and acrylic landscape paintings capture the grand scale of nature. These works feature the stark contrast of hard, earthy, textured rock cliffs alongside the soft, vibrant plumage of wild macaws flocking together.   ',  -- ProductName

N'   Fine art landscape paintings of macaws on cliffs and rocks capture a breath-taking contrast between the harsh, textured stone and the vibrant, elegant feathers of the birds. In the wild, macaws frequently gather on clay licks and steep sandstone hillsides to feed on minerals and find safe nesting sites, making this a profoundly authentic wildlife scene.

🌄 Dramatic Cliffside FlocksThese grand oil and acrylic canvases showcase large, panoramic views of deep canyons, misty waterfalls, or coastal cliffs. The focus is on the sheer scale of nature, with flocks of Scarlet or Blue-and-Yellow Macaws soaring past or roosting along the immense rock walls. 

🪨 Intimate Rock Face PortraitsThese compositions zoom in closer, utilizing the sharp shadows, deep crevices, and rough surfaces of the hill rocks to frame a single macaw or a bonded pair. The neutral earthy tones of the stone act as a natural backdrop that makes the intense primary colors of the birds instantly pop out to the viewer.',  -- Description
5,  -- CategoryID
18,  --  SubCategoryID (painting)
CAST(5200.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO

INSERT [dbo].[ProductImageFiles] ([ProductImageFileID], [FileContent], [FiePath], [ProductID], [TenantContinent], [MyTenantId], [TenantCountry], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive]) VALUES
(
2, -- ProductImageFileID (PK)
NULL,  -- FileContent
N'/TenantProducts/IMG_20260617_203447.jpg', -- FiePath (Fine Art Landscape Paintings, hills)
2,  -- ProductID (FK)
NULL, 
N'00000001-0000-0000-0000-000000000000', -- MyTenantId
1, N'00000006-0000-0000-0000-000000000000', NULL, NULL, CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2), NULL, NULL, 1
)

GO

INSERT [dbo].[Products] 
(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
3,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
5,  -- CategoryID
18,  --  SubCategoryID (painting)
CAST(7000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO

INSERT [dbo].[ProductImageFiles] ([ProductImageFileID], [FileContent], [FiePath], [ProductID], [TenantContinent], [MyTenantId], [TenantCountry], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive]) VALUES
(
2, -- ProductImageFileID (PK)
NULL,  -- FileContent
N'/TenantProducts/IMG_20260617_203517.jpg', -- FiePath 
2,  -- ProductID (FK)
NULL, 
N'00000001-0000-0000-0000-000000000000', -- MyTenantId
1, N'00000006-0000-0000-0000-000000000000', NULL, NULL, CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2), NULL, NULL, 1
)

GO

INSERT [dbo].[Products] 
(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
4,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
5,  -- CategoryID
18,  --  SubCategoryID (painting)
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO

INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
5,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
6,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
7,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
8,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
9,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
10,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
11,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
12,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
13,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
14,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
15,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
16,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
17,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
18,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
19,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
20,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
21,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
22,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
23,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO


INSERT [dbo].[Products] 

(
[ProductID], [PostType], 
[ProductName], [Description],
[CategoryID], [SubCategoryID], 
[Price], [Discount], [SaleCommission], 
[SearchTag], 
[TenantContinent], 
[MyTenantId], 
[TenantCountry],
[CreatedBy], [ModifiedBy], 
[DeletedBy], [CreatedDate], [ModifiedDate], 
[DeletedDate], [IsActive]
)
VALUES (
24,   -- ProductID
4,   -- PostType
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- ProductName
N'   Wall painting frame 🐦 a Scarlet Macaw   ',  -- Description
18,  -- CategoryID
NULL,  --  SubCategoryID 
CAST(5000.00 AS Decimal(18, 2)), --  Price
CAST(5.00 AS Decimal(18, 2)), --  Discount
CAST(5.00 AS Decimal(18, 2)),  --  SaleCommission
N'   search tags   ', --name tags 
NULL,
N'00000001-0000-0000-0000-000000000000', --  MyTenantId 
1, -- TenantCountry
N'00000006-0000-0000-0000-000000000000', 
NULL,
NULL, 
CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2),
NULL,
NULL,
1
)

GO



INSERT [dbo].[ProductImageFiles] ([ProductImageFileID], [FileContent], [FiePath], [ProductID], [TenantContinent], [MyTenantId], [TenantCountry], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive]) VALUES
(1, NULL, N'/TenantProducts/00000001-0000-0000-0000-000000000000-Product-00000006-0000-0000-0000-000000000000-b8e51a24-aa58-428d-a22b-9a602734598a-IMG_20260617_205537.jpg', 1, NULL, N'00000001-0000-0000-0000-000000000000', 1, N'00000006-0000-0000-0000-000000000000', NULL, NULL, CAST(N'2026-08-28T16:10:21.0520589' AS DateTime2), NULL, NULL, 1)

GO
INSERT [dbo].[ProductImageFiles] ([ProductImageFileID], [FileContent], [FiePath], [ProductID], [TenantContinent], [MyTenantId], [TenantCountry], [CreatedBy], [ModifiedBy], [DeletedBy], [CreatedDate], [ModifiedDate], [DeletedDate], [IsActive]) VALUES
(2, NULL, N'/TenantProducts/00000001-0000-0000-0000-000000000000-Product-00000006-0000-0000-0000-000000000000-8e67aec8-5356-417e-b308-33dd478a4ff9-IMG_20260617_205605.jpg', 2, NULL, N'00000001-0000-0000-0000-000000000000', 1, N'00000006-0000-0000-0000-000000000000', NULL, NULL, CAST(N'2026-08-28T16:10:53.7232304' AS DateTime2), NULL, NULL, 1)
GO