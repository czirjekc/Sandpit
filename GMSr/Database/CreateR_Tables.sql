USE [GMS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_R_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_Audit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_Audit](
	[Id] [varchar](200) NOT NULL,
	[RevisionStamp] [datetime] NULL,
	[TableName] [nvarchar](50) NULL,
	[UserLogin] [nvarchar](50) NULL,
	[Action] [nvarchar](1) NULL,
	[OldData] [xml] NULL,
	[NewData] [xml] NULL,
	[ChangedColumns] [nvarchar](1000) NULL,
 CONSTRAINT [PK_R_Audit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MediaItemType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MediaItemType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_MediaItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MediaItemLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MediaItemLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_R_MediaLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MenuElement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MenuElement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Location] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Url] [varchar](200) NULL,
 CONSTRAINT [PK_R_MenuElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareProductStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareProductStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_SoftwareProductStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareOrderType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareOrderType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_R_SoftwareOrderUnitType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicenseType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareLicenseType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_R_SoftwareLicenseType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_Group]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_R_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_HardwareItemOrderForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_HardwareItemOrderForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OlafHardId] [int] NOT NULL,
	[ContractOrderId] [int] NOT NULL,
 CONSTRAINT [PK_R_Olaf_HardContract_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_OrderFormDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_OrderFormDocument](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractOrderId] [int] NULL,
	[Path] [nvarchar](500) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicenseInUse]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareLicenseInUse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareLicenseId] [int] NULL,
	[OlafUserId] [int] NULL,
	[HardwareItemId] [int] NULL,
	[DateAdded] [datetime] NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_SoftwareLicense] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MenuElementUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MenuElementUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuElementId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_R_MenuElement_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_GroupUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_GroupUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_R_UserGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MediaItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MediaItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[MediaItemTypeId] [int] NULL,
	[MediaItemLocationId] [int] NULL,
	[SoftwareOrderId] [int] NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_R_MediaItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_MenuElementGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_MenuElementGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuElementId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_GroupMenuElement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareProduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Version] [nvarchar](max) NULL,
	[CompanyName] [nvarchar](max) NULL,
	[Other] [nvarchar](max) NULL,
	[SoftwareProductStatusId] [int] NULL,
	[SupportedUntil] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_SoftwareProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareOrder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContractOrderId] [int] NULL,
	[SoftwareProductId] [int] NULL,
	[SoftwareOrderTypeId] [int] NULL,
	[PreviousSoftwareOrderId] [int] NULL,
	[OlafRef] [nvarchar](50) NULL,
	[Vendor] [nvarchar](50) NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_R_SoftwareItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicense]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[R_SoftwareLicense](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SoftwareOrderId] [int] NULL,
	[SoftwareLicenseTypeId] [int] NULL,
	[MultiUserQuantity] [int] NULL,
	[PreviousSoftwareLicenseId] [int] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[SerialKey] [nvarchar](max) NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_R_SoftwareItem_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareLicenseInUse_R_SoftwareLicense]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicenseInUse]'))
ALTER TABLE [dbo].[R_SoftwareLicenseInUse]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareLicenseInUse_R_SoftwareLicense] FOREIGN KEY([SoftwareLicenseId])
REFERENCES [dbo].[R_SoftwareLicense] ([Id])
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MenuElementUser_R_MenuElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MenuElementUser]'))
ALTER TABLE [dbo].[R_MenuElementUser]  WITH CHECK ADD  CONSTRAINT [FK_R_MenuElementUser_R_MenuElement] FOREIGN KEY([MenuElementId])
REFERENCES [dbo].[R_MenuElement] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MenuElementUser_R_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MenuElementUser]'))
ALTER TABLE [dbo].[R_MenuElementUser]  WITH CHECK ADD  CONSTRAINT [FK_R_MenuElementUser_R_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[R_User] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_GroupUser_R_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_GroupUser]'))
ALTER TABLE [dbo].[R_GroupUser]  WITH CHECK ADD  CONSTRAINT [FK_R_GroupUser_R_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[R_Group] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_GroupUser_R_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_GroupUser]'))
ALTER TABLE [dbo].[R_GroupUser]  WITH CHECK ADD  CONSTRAINT [FK_R_GroupUser_R_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[R_User] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MediaItem_R_MediaItemLocation]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MediaItem]'))
ALTER TABLE [dbo].[R_MediaItem]  WITH CHECK ADD  CONSTRAINT [FK_R_MediaItem_R_MediaItemLocation] FOREIGN KEY([MediaItemLocationId])
REFERENCES [dbo].[R_MediaItemLocation] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MediaItem_R_MediaItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MediaItem]'))
ALTER TABLE [dbo].[R_MediaItem]  WITH CHECK ADD  CONSTRAINT [FK_R_MediaItem_R_MediaItemType] FOREIGN KEY([MediaItemTypeId])
REFERENCES [dbo].[R_MediaItemType] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MediaItem_R_SoftwareOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MediaItem]'))
ALTER TABLE [dbo].[R_MediaItem]  WITH CHECK ADD  CONSTRAINT [FK_R_MediaItem_R_SoftwareOrder] FOREIGN KEY([SoftwareOrderId])
REFERENCES [dbo].[R_SoftwareOrder] ([Id])
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MenuElementGroup_R_Group]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MenuElementGroup]'))
ALTER TABLE [dbo].[R_MenuElementGroup]  WITH CHECK ADD  CONSTRAINT [FK_R_MenuElementGroup_R_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[R_Group] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_MenuElementGroup_R_MenuElement]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_MenuElementGroup]'))
ALTER TABLE [dbo].[R_MenuElementGroup]  WITH CHECK ADD  CONSTRAINT [FK_R_MenuElementGroup_R_MenuElement] FOREIGN KEY([MenuElementId])
REFERENCES [dbo].[R_MenuElement] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareProduct_R_SoftwareProductStatus]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareProduct]'))
ALTER TABLE [dbo].[R_SoftwareProduct]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareProduct_R_SoftwareProductStatus] FOREIGN KEY([SoftwareProductStatusId])
REFERENCES [dbo].[R_SoftwareProductStatus] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareOrder_R_SoftwareOrderType]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareOrder]'))
ALTER TABLE [dbo].[R_SoftwareOrder]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareOrder_R_SoftwareOrderType] FOREIGN KEY([SoftwareOrderTypeId])
REFERENCES [dbo].[R_SoftwareOrderType] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareOrder_R_SoftwareProduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareOrder]'))
ALTER TABLE [dbo].[R_SoftwareOrder]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareOrder_R_SoftwareProduct] FOREIGN KEY([SoftwareProductId])
REFERENCES [dbo].[R_SoftwareProduct] ([Id])
ON DELETE CASCADE
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareLicense_R_SoftwareLicenseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicense]'))
ALTER TABLE [dbo].[R_SoftwareLicense]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareLicense_R_SoftwareLicenseType] FOREIGN KEY([SoftwareLicenseTypeId])
REFERENCES [dbo].[R_SoftwareLicenseType] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_R_SoftwareLicense_R_SoftwareOrder]') AND parent_object_id = OBJECT_ID(N'[dbo].[R_SoftwareLicense]'))
ALTER TABLE [dbo].[R_SoftwareLicense]  WITH CHECK ADD  CONSTRAINT [FK_R_SoftwareLicense_R_SoftwareOrder] FOREIGN KEY([SoftwareOrderId])
REFERENCES [dbo].[R_SoftwareOrder] ([Id])
ON DELETE CASCADE
