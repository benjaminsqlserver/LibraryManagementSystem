USE [master]
GO
/****** Object:  Database [MyLibraryDB]    Script Date: 30/01/2023 1:20:08 am ******/
CREATE DATABASE [MyLibraryDB]
 
GO

USE [MyLibraryDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BindingDetails]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BindingDetails](
	[BindingID] [int] IDENTITY(1,1) NOT NULL,
	[BindingName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BindingDetails] PRIMARY KEY CLUSTERED 
(
	[BindingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookDetails]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookDetails](
	[BookID] [bigint] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](50) NOT NULL,
	[Language] [nvarchar](50) NOT NULL,
	[BindingID] [int] NOT NULL,
	[NoOfActualCopies] [int] NOT NULL,
	[NoOfCurrentCopies] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[YearOfPublication] [int] NOT NULL,
	[ShelfID] [int] NOT NULL,
 CONSTRAINT [PK_BookDetails] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookShelves]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookShelves](
	[ShelfID] [int] IDENTITY(1,1) NOT NULL,
	[ShelfNo] [int] NOT NULL,
	[ShelfFloor] [int] NOT NULL,
 CONSTRAINT [PK_BookShelves] PRIMARY KEY CLUSTERED 
(
	[ShelfID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BorrowerDetails]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowerDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [bigint] NOT NULL,
	[BorrowedFrom] [date] NOT NULL,
	[BorrowedTo] [date] NOT NULL,
	[ActualReturnDate] [date] NULL,
	[BorrowedBy] [bigint] NOT NULL,
	[IssuedBy] [bigint] NOT NULL,
 CONSTRAINT [PK_BorrowerDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryDetails]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryDetails](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryDetails] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryClients]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryClients](
	[LibraryClientID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ConfirmPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LibraryClients] PRIMARY KEY CLUSTERED 
(
	[LibraryClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibraryEmployees]    Script Date: 30/01/2023 1:20:11 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryEmployees](
	[LibraryEmployeeID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ConfirmPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LibraryEmployees] PRIMARY KEY CLUSTERED 
(
	[LibraryEmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'7.0.0')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'49b778d8-89a1-4a96-8e70-ffdc37c4a136', NULL, N'LibraryUser', N'LIBRARYUSER')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'952fd3f5-3b11-4603-b870-b47deba65c22', NULL, N'LibraryStaff', N'LIBRARYSTAFF')
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'ecb6775d-d63b-44f9-a5ee-91f89060291d', NULL, N'Admin', N'ADMIN')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'04b0720e-c467-4153-8a6f-e2d32e3781f2', N'49b778d8-89a1-4a96-8e70-ffdc37c4a136')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'865c33a0-5570-4051-a584-4a38850def9f', N'952fd3f5-3b11-4603-b870-b47deba65c22')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8e424dcb-8726-46f5-b37e-0f65aa31a45f', N'49b778d8-89a1-4a96-8e70-ffdc37c4a136')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'acc7c646-e03d-4657-a2d4-640a68d8fee2', N'952fd3f5-3b11-4603-b870-b47deba65c22')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f3b47541-4073-40fc-949f-3f2e9395740a', N'952fd3f5-3b11-4603-b870-b47deba65c22')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ffe62535-40fe-4e69-b182-ea90b0c5e284', N'ecb6775d-d63b-44f9-a5ee-91f89060291d')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'04b0720e-c467-4153-8a6f-e2d32e3781f2', 0, N'67abcdf7-39d4-4d1a-9106-9005d6ce5466', N'olumuyiwabenjamin1@gmail.com', 1, 1, NULL, N'OLUMUYIWABENJAMIN1@GMAIL.COM', N'OLUMUYIWABENJAMIN1@GMAIL.COM', N'AQAAAAIAAYagAAAAEHjm9cSufbevQgmw594+1Sr5eC53ihJg7rK+dTDCELswWUuA2HJWYWZgbqVgl4etEA==', NULL, 0, N'KKBFML6AXG776F3Z757HZ7KUVD6XJSUZ', 0, N'olumuyiwabenjamin1@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'865c33a0-5570-4051-a584-4a38850def9f', 0, N'89922ecd-2311-4042-a974-c7939b717089', N'benmuyiwa@yahoo.com', 1, 1, NULL, N'BENMUYIWA@YAHOO.COM', N'BENMUYIWA@YAHOO.COM', N'AQAAAAIAAYagAAAAEK4nynyL1jXjG3lgfLQF1akPbsfSbbGOZIQEF76tADPzcI0zHOKfhy6uG88c+z3Fpw==', NULL, 0, N'YU4OBHM6AGPFYSMVZGDQN5Z4G2S4WDEI', 0, N'benmuyiwa@yahoo.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'8e424dcb-8726-46f5-b37e-0f65aa31a45f', 0, N'16eb4c5d-5256-420e-961c-8eccfa41d29f', N'gabriel.john@library.com', 1, 1, NULL, N'GABRIEL.JOHN@LIBRARY.COM', N'GABRIEL.JOHN@LIBRARY.COM', N'AQAAAAIAAYagAAAAED7gnz1dPl7fB3+prxAkverHdqLGkhAevdsI/VgGG3K0RCywFL8efNuTusDjPIMHJg==', NULL, 0, N'6QQIMH22RPCCBWJY672HBUNWLIFBIINO', 0, N'gabriel.john@library.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'acc7c646-e03d-4657-a2d4-640a68d8fee2', 0, N'859c0c30-5d6a-4d21-b5c1-9cdd96ab4989', N'm.jane@library.com', 1, 1, NULL, N'M.JANE@LIBRARY.COM', N'M.JANE@LIBRARY.COM', N'AQAAAAIAAYagAAAAELBBVpQ+qRRw3MzumSeW3/zjTIVquU/K6/4rAcTJH6WjD50ZD+9GKh9G451pG+aytg==', NULL, 0, N'7FKULWNJSOG7CVG3YOK4BX5WOQ3ME6DQ', 0, N'm.jane@library.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'f3b47541-4073-40fc-949f-3f2e9395740a', 0, N'dd0aa71e-39d0-43d0-beb6-a115940256b2', N'paul.arsene@library.com', 1, 1, NULL, N'PAUL.ARSENE@LIBRARY.COM', N'PAUL.ARSENE@LIBRARY.COM', N'AQAAAAIAAYagAAAAEN/FxzDBNTN0A81KYPPJJhIjX/zJXh34dFs+wsELOviy0KWA1CDgmLxBbFu1ZCHCXw==', NULL, 0, N'X7L3MFJAVPXTGBYTG2KDZ4BMD3ZUS6JN', 0, N'paul.arsene@library.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'ffe62535-40fe-4e69-b182-ea90b0c5e284', 0, N'f1edd96c-4ddf-4129-96c1-55f2a7f602f4', N'benjaminsqlserver@gmail.com', 1, 1, NULL, N'BENJAMINSQLSERVER@GMAIL.COM', N'BENJAMINSQLSERVER@GMAIL.COM', N'AQAAAAIAAYagAAAAEB7txfMs2Mup/2GcdrEMNkRXo+WzAHK0iQXI7EdE1LwXyElGHBTpmd0eZqeQyISRmQ==', NULL, 0, N'BCNXRMIISX3A7RCECDTAOUFCCQJ62EOS', 0, N'benjaminsqlserver@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[LibraryClients] ON 
GO
INSERT [dbo].[LibraryClients] ([LibraryClientID], [FirstName], [LastName], [EmailAddress], [RegistrationDate], [Password], [ConfirmPassword]) VALUES (1, N'gabriel', N'john', N'gabriel.john@library.com', CAST(N'2023-01-20' AS Date), N'Not Displayed', N'Not Displayed')
GO
SET IDENTITY_INSERT [dbo].[LibraryClients] OFF
GO
SET IDENTITY_INSERT [dbo].[LibraryEmployees] ON 
GO
INSERT [dbo].[LibraryEmployees] ([LibraryEmployeeID], [FirstName], [LastName], [EmailAddress], [RegistrationDate], [Password], [ConfirmPassword]) VALUES (1, N'Paul', N'Arsene', N'paul.arsene@library.com', CAST(N'2023-01-04' AS Date), N'Not Displayed', N'Not Displayed')
GO
INSERT [dbo].[LibraryEmployees] ([LibraryEmployeeID], [FirstName], [LastName], [EmailAddress], [RegistrationDate], [Password], [ConfirmPassword]) VALUES (2, N'Mary', N'Jane', N'm.jane@library.com', CAST(N'2023-01-03' AS Date), N'Not Displayed', N'Not Displayed')
GO
SET IDENTITY_INSERT [dbo].[LibraryEmployees] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_UserId]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 30/01/2023 1:20:13 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_BookDetails]    Script Date: 30/01/2023 1:20:13 am ******/
ALTER TABLE [dbo].[BookDetails] ADD  CONSTRAINT [IX_BookDetails] UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LibraryClients]    Script Date: 30/01/2023 1:20:13 am ******/
ALTER TABLE [dbo].[LibraryClients] ADD  CONSTRAINT [IX_LibraryClients] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LibraryEmployees]    Script Date: 30/01/2023 1:20:13 am ******/
ALTER TABLE [dbo].[LibraryEmployees] ADD  CONSTRAINT [IX_LibraryEmployees] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BookDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookDetails_BindingDetails] FOREIGN KEY([BindingID])
REFERENCES [dbo].[BindingDetails] ([BindingID])
GO
ALTER TABLE [dbo].[BookDetails] CHECK CONSTRAINT [FK_BookDetails_BindingDetails]
GO
ALTER TABLE [dbo].[BookDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookDetails_BookShelves] FOREIGN KEY([ShelfID])
REFERENCES [dbo].[BookShelves] ([ShelfID])
GO
ALTER TABLE [dbo].[BookDetails] CHECK CONSTRAINT [FK_BookDetails_BookShelves]
GO
ALTER TABLE [dbo].[BookDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookDetails_CategoryDetails] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[CategoryDetails] ([CategoryID])
GO
ALTER TABLE [dbo].[BookDetails] CHECK CONSTRAINT [FK_BookDetails_CategoryDetails]
GO
ALTER TABLE [dbo].[BorrowerDetails]  WITH CHECK ADD  CONSTRAINT [FK_BorrowerDetails_BookDetails] FOREIGN KEY([BookID])
REFERENCES [dbo].[BookDetails] ([BookID])
GO
ALTER TABLE [dbo].[BorrowerDetails] CHECK CONSTRAINT [FK_BorrowerDetails_BookDetails]
GO
ALTER TABLE [dbo].[BorrowerDetails]  WITH CHECK ADD  CONSTRAINT [FK_BorrowerDetails_LibraryClients] FOREIGN KEY([BorrowedBy])
REFERENCES [dbo].[LibraryClients] ([LibraryClientID])
GO
ALTER TABLE [dbo].[BorrowerDetails] CHECK CONSTRAINT [FK_BorrowerDetails_LibraryClients]
GO
ALTER TABLE [dbo].[BorrowerDetails]  WITH CHECK ADD  CONSTRAINT [FK_BorrowerDetails_LibraryEmployees] FOREIGN KEY([IssuedBy])
REFERENCES [dbo].[LibraryEmployees] ([LibraryEmployeeID])
GO
ALTER TABLE [dbo].[BorrowerDetails] CHECK CONSTRAINT [FK_BorrowerDetails_LibraryEmployees]
GO
USE [master]
GO
ALTER DATABASE [MyLibraryDB] SET  READ_WRITE 
GO
