USE [master]
GO
/****** Object:  Database [PcClub]    Script Date: 13.06.2023 1:34:22 ******/
CREATE DATABASE [PcClub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PcClub', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PcClub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PcClub_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PcClub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PcClub] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PcClub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PcClub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PcClub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PcClub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PcClub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PcClub] SET ARITHABORT OFF 
GO
ALTER DATABASE [PcClub] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PcClub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PcClub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PcClub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PcClub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PcClub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PcClub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PcClub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PcClub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PcClub] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PcClub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PcClub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PcClub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PcClub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PcClub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PcClub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PcClub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PcClub] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PcClub] SET  MULTI_USER 
GO
ALTER DATABASE [PcClub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PcClub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PcClub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PcClub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PcClub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PcClub] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PcClub] SET QUERY_STORE = OFF
GO
USE [PcClub]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 13.06.2023 1:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPlace] [int] NULL,
	[IdUser] [int] NULL,
	[Hour] [float] NULL,
	[DateTimeStart] [datetime] NULL,
	[DateTimeEnd] [datetime] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 13.06.2023 1:34:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [date] NULL,
	[MaxUser] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventUser]    Script Date: 13.06.2023 1:34:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[IdEvent] [int] NULL,
 CONSTRAINT [PK_EventUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Place]    Script Date: 13.06.2023 1:34:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IdType] [int] NULL,
	[IsBooking] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 13.06.2023 1:34:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13.06.2023 1:34:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[PhoneNumber] [nchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[Passport] [nchar](10) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (1, 1, 1, NULL, CAST(N'2023-06-11T13:59:10.740' AS DateTime), CAST(N'2023-06-11T14:59:10.740' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (2, 9, 9, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (3, 14, 14, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (4, 19, 19, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (5, 6, 6, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (6, 2, 2, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (7, 17, 17, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (8, 11, 11, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (9, 3, 3, 1, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (10, 20, 20, 2, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (11, 7, 7, 2, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (12, 16, 16, 2, CAST(N'2023-06-11T13:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (13, 1, 1, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (14, 13, 13, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (15, 1, 1, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (16, 8, 8, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (17, 10, 10, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (18, 12, 12, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (19, 5, 5, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (20, 4, 4, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (21, 18, 18, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (22, 15, 15, 2, CAST(N'2023-06-11T15:59:00.000' AS DateTime), CAST(N'2023-06-11T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (23, 20, 20, 3, CAST(N'2023-06-11T17:59:00.000' AS DateTime), CAST(N'2023-06-11T20:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (24, 9, 9, 3, CAST(N'2023-06-11T17:59:00.000' AS DateTime), CAST(N'2023-06-11T20:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (25, 14, 14, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-12T14:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (26, 19, 19, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (27, 6, 6, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (28, 2, 2, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (29, 17, 17, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (30, 11, 11, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (31, 3, 3, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (32, 20, 20, 1, CAST(N'2023-06-11T14:59:00.000' AS DateTime), CAST(N'2023-06-11T15:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (33, 7, 7, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (34, 16, 16, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (35, 1, 1, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (36, 13, 13, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (37, 10, 10, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (38, 8, 8, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (39, 10, 10, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (40, 12, 12, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (41, 5, 5, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (42, 4, 4, 2, CAST(N'2023-06-12T15:59:00.000' AS DateTime), CAST(N'2023-06-12T17:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (43, 18, 18, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (44, 15, 15, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (45, 2, 2, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (46, 9, 9, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (47, 14, 14, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (48, 19, 19, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (49, 6, 6, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (50, 2, 3, 1, CAST(N'2023-06-12T17:59:00.000' AS DateTime), CAST(N'2023-06-12T18:59:00.000' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (51, 1, 1, NULL, CAST(N'2023-06-12T18:20:06.670' AS DateTime), CAST(N'2023-06-12T18:26:06.670' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (52, 1, 1, NULL, CAST(N'2023-06-12T18:28:41.477' AS DateTime), CAST(N'2023-06-12T18:28:47.477' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (53, 1, 1, NULL, CAST(N'2023-06-12T18:29:17.920' AS DateTime), CAST(N'2023-06-12T18:29:23.920' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (54, 1, 1, NULL, CAST(N'2023-06-12T19:33:55.103' AS DateTime), CAST(N'2023-06-12T19:34:55.103' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (55, 2, 1, NULL, CAST(N'2023-06-12T19:34:03.130' AS DateTime), CAST(N'2023-06-12T19:35:03.130' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (56, 1, 1, NULL, CAST(N'2023-06-13T00:08:34.600' AS DateTime), CAST(N'2023-06-13T00:09:34.600' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (57, 2, 2, NULL, CAST(N'2023-06-13T00:10:05.017' AS DateTime), CAST(N'2023-06-13T00:11:05.017' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (58, 1, 1, NULL, CAST(N'2023-06-13T00:15:48.777' AS DateTime), CAST(N'2023-06-13T00:16:48.777' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (59, 1, 2, NULL, CAST(N'2023-06-13T00:17:27.390' AS DateTime), CAST(N'2023-06-13T00:18:27.390' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (60, 1, 1, NULL, CAST(N'2023-06-13T00:19:17.640' AS DateTime), CAST(N'2023-06-13T00:20:17.640' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (61, 1, 1, NULL, CAST(N'2023-06-13T00:27:02.617' AS DateTime), CAST(N'2023-06-13T00:28:02.617' AS DateTime), 1)
INSERT [dbo].[Booking] ([Id], [IdPlace], [IdUser], [Hour], [DateTimeStart], [DateTimeEnd], [IsDelete]) VALUES (62, 1, 3, NULL, CAST(N'2023-06-13T00:28:22.870' AS DateTime), CAST(N'2023-06-13T00:29:22.870' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (1, N'PC Power-Up Party', N'Присоединяйтесь к нам на "PC Power-Up Party" - грандиозном событии для всех любителей компьютерных игр и технологий! Это мероприятие предназначено для объединения страстных геймеров и энтузиастов ПК-мира, чтобы показать всю мощь и возможности современных игровых компьютеров.', CAST(N'2023-06-13' AS Date), 10, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (2, N'Game On: PC Gaming Extravaganza', N'Присоединяйтесь к нам на "Game On: PC Gaming Extravaganza" - невероятном событии, посвященном играм на компьютере! В этот день мы предлагаем вам окунуться в мир игровых приключений, где вы сможете испытать новые игры, принять участие в соревнованиях, узнать о последних технологических новинках в области ПК-игр и встретиться с другими энтузиастами компьютерных игр.', CAST(N'2023-06-12' AS Date), 5, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (3, N'PC Tech Expo: Discover the Future', N'"PC Tech Expo: Discover the Future" - это уникальное событие, на котором вы сможете погрузиться в мир передовых технологий компьютеров. Посетите нашу выставку, чтобы узнать о последних трендах в области ПК-технологий, увидеть демонстрации новейших компонентов и аксессуаров, прослушать увлекательные лекции от экспертов и встретиться с представителями ведущих компаний.', CAST(N'2023-06-10' AS Date), 3, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (4, N'PC Masterclass: Unleash Your Gaming Potential', N'"PC Masterclass: Unleash Your Gaming Potential" - это уникальная возможность узнать все секреты успешной игры на компьютере. Примите участие в наших мастер-классах, где опытные игроки и специалисты поделятся своими советами и приемами, чтобы помочь вам стать настоящим мастером компьютерных игр. Обсуждайте новые стратегии, техники и настраивайте свою систему для оптимальной производительности.', CAST(N'2023-06-08' AS Date), 7, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (5, N'PC LAN Party: Game, Connect, Dominate', N'"PC LAN Party: Game, Connect, Dominate" - это вечер полного веселья и соревнований для всех фанатов многопользовательских компьютерных игр. Приведите свои компьютеры и подключитесь к нашей высокоскоростной локальной сети, чтобы сразиться с другими игроками в эпических битвах. Насладитесь атмосферой взаимодействия, соревнований и незабываемого геймерского опыта.', CAST(N'2023-06-06' AS Date), 10, NULL)
INSERT [dbo].[Event] ([Id], [Name], [Description], [Date], [MaxUser], [IsDeleted]) VALUES (6, N'PC Modding Showcase: Unleash Your Creativity', N'Описание: "PC Modding Showcase: Unleash Your Creativity" - это уникальное мероприя', CAST(N'2023-06-04' AS Date), 11, 1)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[EventUser] ON 

INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (9, 2, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (10, 4, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (12, 9, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (13, 14, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (14, 19, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (15, 6, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (16, 2, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (17, 17, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (18, 11, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (19, 3, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (20, 20, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (21, 7, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (22, 16, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (24, 13, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (25, 1, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (26, 8, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (27, 10, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (28, 12, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (29, 5, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (30, 4, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (31, 18, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (32, 15, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (33, 20, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (34, 9, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (35, 14, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (36, 19, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (37, 6, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (38, 2, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (39, 17, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (40, 11, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (41, 3, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (42, 20, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (43, 7, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (44, 16, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (45, 1, 5)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (46, 13, 6)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (47, 10, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (48, 8, 2)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (49, 10, 3)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (50, 12, 4)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (51, 8, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (52, 8, 1)
INSERT [dbo].[EventUser] ([Id], [IdUser], [IdEvent]) VALUES (75, 1, 1)
SET IDENTITY_INSERT [dbo].[EventUser] OFF
GO
SET IDENTITY_INSERT [dbo].[Place] ON 

INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (1, N'1', 1, 0, NULL)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (2, N'2', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (3, N'3', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (4, N'4', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (5, N'5', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (6, N'6', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (7, N'7', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (8, N'8', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (9, N'9', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (10, N'10', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (11, N'11', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (12, N'12', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (13, N'13', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (14, N'14', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (15, N'15', 1, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (16, N'16', 2, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (17, N'17', 2, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (18, N'18', 2, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (19, N'19', 2, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (20, N'20', 2, 0, 0)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (21, N'21', 2, 0, NULL)
INSERT [dbo].[Place] ([Id], [Name], [IdType], [IsBooking], [IsDeleted]) VALUES (22, N'23', 2, 0, NULL)
SET IDENTITY_INSERT [dbo].[Place] OFF
GO
INSERT [dbo].[Type] ([Id], [Name], [Price], [Description]) VALUES (1, N'Обычный', 300, NULL)
INSERT [dbo].[Type] ([Id], [Name], [Price], [Description]) VALUES (2, N'VIP', 500, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (1, N'Мавлин Данис Ильдарович', N'9270446062', N'mavlindanis@gmail.com', N'1234567890')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (2, N'Мамонов Иван Андреевич', N'9270568082', N'vanyamamonov03@gmail.com', N'0987654321')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (3, N'Свиридова Василиса Никитична', N'2801837174', N'slbRCX5@example.com', N'5967192868')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (4, N'Зайцева Алиса Максимовна', N'5056449289', N'xyZ9mB8@example.com', N'5978061021')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (5, N'Сорокина Василиса Аркадьевна', N'7875384196', N'jWu3lA2@example.com', N'4767779284')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (6, N'Давыдова Таисия Марковна', N'9873691167', N'pQtZvY6@example.com', N'4124410335')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (7, N'Иванов Евгений Иванович', N'2367285273', N'kSeRbX7@example.com', N'4361539743')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (8, N'Яковлев Артём Александрович', N'5844353043', N'gHxVfT9@example.com', N'1381057472')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (9, N'Антонов Григорий Михайлович', N'2652768480', N'mDwQnC4@example.com', N'7111844375')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (10, N'Калмыков Константин Никитич', N'4713341746', N'rLyKsP2@example.com', N'1135119470')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (11, N'Дорофеев Денис Алиевич', N'9004309791', N'cNuBzG8@example.com', N'9557103073')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (12, N'Афанасьева Василиса Михайловна', N'5311250245', N'oIaFpH9@example.com', N'8452990571')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (13, N'Горбачев Дмитрий Игоревич', N'5930014773', N'tJvMwD3@example.com', N'1683620780')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (14, N'Климова Фатима Ивановна', N'7828797922', N'eOuSlX6@example.com', N'4654051330')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (15, N'Шаповалова Милана Мироновна', N'3459483089', N'fPrYqZ5@example.com', N'9379337409')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (16, N'Старикова Ева Тимуровна', N'2993470858', N'bLtNaX9@example.com', N'8271349423')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (17, N'Миронов Артём Ярославович', N'7646339662', N'vGkHjW2@example.com', N'7803947026')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (18, N'Ушаков Егор Глебович', N'2250659541', N'dMiUcL4@example.com', N'6633113322')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (19, N'Александров Егор Кириллович', N'4401586354', N'uKgSvH7@example.com', N'3952137535')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (20, N'Логинова София Викторовна', N'9033990203', N'hRxWnJ6@example.com', N'4839541272')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (21, N'Данилов Дмитрий Сергеевич', N'5301266320', N'qTyFaZ8@example.com', N'4526958119')
INSERT [dbo].[User] ([Id], [FullName], [PhoneNumber], [Email], [Passport]) VALUES (22, N'Денисов Илья Тимофеевич', N'2231966367', N'nBpVeD2@example.com', N'4632127762')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Place1] FOREIGN KEY([IdPlace])
REFERENCES [dbo].[Place] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Place1]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_User]
GO
ALTER TABLE [dbo].[EventUser]  WITH CHECK ADD  CONSTRAINT [FK_EventUser_Event] FOREIGN KEY([IdEvent])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[EventUser] CHECK CONSTRAINT [FK_EventUser_Event]
GO
ALTER TABLE [dbo].[EventUser]  WITH CHECK ADD  CONSTRAINT [FK_EventUser_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[EventUser] CHECK CONSTRAINT [FK_EventUser_User]
GO
ALTER TABLE [dbo].[Place]  WITH CHECK ADD  CONSTRAINT [FK_Place_Type1] FOREIGN KEY([IdType])
REFERENCES [dbo].[Type] ([Id])
GO
ALTER TABLE [dbo].[Place] CHECK CONSTRAINT [FK_Place_Type1]
GO
USE [master]
GO
ALTER DATABASE [PcClub] SET  READ_WRITE 
GO
