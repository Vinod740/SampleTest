USE [master]
GO
/****** Object:  Database [Shopping]    Script Date: 5/15/2025 10:24:44 AM ******/
CREATE DATABASE [Shopping]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Shopping', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Shopping.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Shopping_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Shopping_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Shopping] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shopping].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shopping] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shopping] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shopping] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shopping] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shopping] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shopping] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shopping] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shopping] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shopping] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shopping] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shopping] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shopping] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shopping] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shopping] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shopping] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shopping] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shopping] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shopping] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shopping] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shopping] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shopping] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shopping] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Shopping] SET RECOVERY FULL 
GO
ALTER DATABASE [Shopping] SET  MULTI_USER 
GO
ALTER DATABASE [Shopping] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shopping] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Shopping] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Shopping] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Shopping] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Shopping] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Shopping', N'ON'
GO
ALTER DATABASE [Shopping] SET QUERY_STORE = ON
GO
ALTER DATABASE [Shopping] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Shopping]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CartDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NULL,
	[UserId] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_CartDetail] PRIMARY KEY CLUSTERED 
(
	[CartDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [varchar](50) NOT NULL,
	[Image] [varchar](2000) NULL,
	[Stock] [int] NOT NULL,
	[Price] [decimal](10, 2) NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserDetailId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Nationality] [int] NOT NULL,
	[UserType] [int] NOT NULL,
	[DOB] [datetime] NOT NULL,
	[Photo] [varchar](500) NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NULL,
	[UserName] [varchar](20) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[UserGroup] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserMaster] ADD  CONSTRAINT [DF_UserMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_ItemMaster]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_UserMaster] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMaster] ([UserId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_UserMaster]
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckStockAvailable]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC SP_CheckStockAvailable 8,2 , null
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckStockAvailable]
	@ItemId INT,
	@UserId INT,
	@AvailableUnit INT OUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @ItemInCart INT;
SELECT  @ItemInCart = count(*) from Cartdetail where itemid = @ItemId and isactive = 1 and Userid = @UserId
--SELECT  @ItemInCart 

SELECT @AvailableUnit = (stock - @ItemInCart ) from ItemMaster where ItemId = @ItemId
SELECT @AvailableUnit
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Ins_Up_Item]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Ins_Up_Item]
	@ItemId int,
	@ItemName varchar(50),
	@Image varchar(50),
	@Stock int,
	@Price decimal
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@ItemId = 0)
		BEGIN
		INSERT INTO ItemMaster (ItemName, [Image], Stock,Price)
		VALUES (@ItemName, @Image, @Stock, @Price)
 		
		END
	ELSE
		BEGIN 
		UPDATE ItemMaster 
		SET ItemName = @ItemName, 
		[Image] = @Image,
		Stock = @Stock,
		Price = @Price
		WHERE ItemId = @ItemId
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Ins_Up_User]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Ins_Up_User]
	@UserDetailId int,
	@Email nvarchar(100),
	@Nationality int,
	@UserType int,
	@DOB datetime,
	@Gender bit,
	@PhotoPath  nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@UserDetailId = 0)
		BEGIN
		INSERT INTO UserDetails(Email, Nationality, UserType, DOB, Gender, [Photo])
		VALUES (@Email, @Nationality, @UserType, @DOB, @Gender, @PhotoPath)
 		
		END
	ELSE
		BEGIN 
		UPDATE UserDetails 
		SET Email = @Email, 
		[Nationality] = @Nationality,
		[UserType] = @UserType,
		[DOB] = @DOB,
		[Gender]= @Gender,
		[Photo]= @PhotoPath
		WHERE UserDetailId = @UserDetailId
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Login]    Script Date: 5/15/2025 10:24:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Login]
	@Email varchar(50),
	@Password nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM 
	UserMaster 
	WHERE Email = @Email AND [Password] = @Password collate Latin1_General_CS_AS
END
GO
USE [master]
GO
ALTER DATABASE [Shopping] SET  READ_WRITE 
GO
