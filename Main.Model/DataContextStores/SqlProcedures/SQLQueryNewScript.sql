USE [DemoFineArtsWebApp]
GO
/****** Object:  StoredProcedure [dbo].[GetAllGroupPosts]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Return All Posts
-- =============================================
CREATE PROCEDURE [dbo].[GetAllGroupPosts]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	p.Title,
	p.PostID,
	p.IsStudentDeal,
	p.IsBrandNew,
	p.IsUsed,
	p.IsUrgent,
	p.CategoryID,
	p.SubCategoryID,
	p.IsForRent,
	p.IsForSell,
	p.Description,
	u.Email,
	u.ClientName,
	u.UserID,
	a.AreaDescription,
	a.StateID,
	av.Text as DisplayState,
	p.CreatedDate
	FROM Posts p
	LEFT JOIN Users u
	ON p.UserID = u.UserID
	LEFT JOIN Addresses a
	ON p.AddressID = a.AddressID
	LEFT JOIN AValues av
	ON a.StateID = av.ValueID
	WHERE p.SubCategoryID IN (101,114,115,119,127,123,159,109)
	ORDER BY P.CreatedDate DESC
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllImageByPostId]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Getting single image for a post
-- =============================================
CREATE PROCEDURE [dbo].[GetAllImageByPostId] 
	-- Add the parameters for the stored procedure here
	@PostID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT f.Image
	FROM Posts p
	LEFT JOIN Files f
	ON p.PostID = f.PostID
	WHERE p.PostID=@PostID AND f.Image is not null
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllPosts]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Return All Posts
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPosts]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	p.Title,
	p.PostID,
	p.IsStudentDeal,
	p.IsBrandNew,
	p.IsUsed,
	p.IsUrgent,
	p.CategoryID,
	p.SubCategoryID,
	p.IsForRent,
	p.IsForSell,
	p.Description,
	u.Email,
	u.ClientName,
	u.UserID,
	a.AreaDescription,
	a.StateID,
	av.Text as DisplayState,
	p.CreatedDate
	FROM Posts p
	LEFT JOIN Users u
	ON p.UserID = u.UserID
	LEFT JOIN Addresses a
	ON p.AddressID = a.AddressID
	LEFT JOIN AValues av
	ON a.StateID = av.ValueID
	ORDER BY P.CreatedDate DESC
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllPostsByUserID]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Return All Posts
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPostsByUserID]
@UserID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
	p.Title,
	p.PostID,
	p.IsStudentDeal,
	p.IsBrandNew,
	p.IsUsed,
	p.IsUrgent,
	p.CategoryID,
	p.SubCategoryID,
	p.IsForRent,
	p.IsForSell,
	p.Description,
	u.Email,
	u.ClientName,
	u.UserID,
	a.AreaDescription,
	a.StateID,
	av.Text as DisplayState,
	p.CreatedDate
	FROM Posts p
	LEFT JOIN Users u
	ON p.UserID = u.UserID
	LEFT JOIN Addresses a
	ON p.AddressID = a.AddressID
	LEFT JOIN AValues av
	ON a.StateID = av.ValueID
	WHERE u.UserID = @UserID
	ORDER BY P.CreatedDate DESC
END

GO
/****** Object:  StoredProcedure [dbo].[GetPostByPostID]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Return All Posts
-- =============================================
CREATE PROCEDURE [dbo].[GetPostByPostID]
@PostID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	p.Title,
	p.PostID,
	p.IsStudentDeal,
	p.IsBrandNew,
	p.IsUsed,
	p.IsUrgent,
	p.CategoryID,
	p.SubCategoryID,
	p.IsForRent,
	p.IsForSell,
	p.Description,
	u.Email,
	u.Phone,
	u.ClientName,
	u.UserID,
	a.AreaDescription,
	a.StateID,
	av.Text as DisplayState,
	p.CreatedDate
	FROM Posts p
	LEFT JOIN Users u
	ON p.UserID = u.UserID
	LEFT JOIN Addresses a
	ON p.AddressID = a.AddressID
	LEFT JOIN AValues av
	ON a.StateID = av.ValueID
	WHERE P.PostID = @PostID
END

GO
/****** Object:  StoredProcedure [dbo].[GetSingleImageByPostId]    Script Date: 17-Jun-17 4:36:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Naim Ul Islam Prodhan
-- Create date: 23-Jan-2016
-- Description:	Getting single image for a post
-- =============================================
CREATE PROCEDURE [dbo].[GetSingleImageByPostId] 
	-- Add the parameters for the stored procedure here
	@PostID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT top 1 f.Image
	FROM Posts p
	LEFT JOIN Files f
	ON p.PostID = f.PostID
	WHERE p.PostID=@PostID AND f.Image is not null
END

GO
