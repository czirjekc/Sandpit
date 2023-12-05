
/***************************************DROPS*********************************************/
GO
/****** Object:  Table [dbo].[SWD_Import]    Script Date: 03/10/2011 17:57:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SWD_Import]') AND type in (N'U'))
DROP TABLE [dbo].[SWD_Import]
GO
/******************************************************************************************************/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_Installation_Olaf_Hard]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_Installation]'))
ALTER TABLE [dbo].[SWD_Installation] DROP CONSTRAINT [FK_SWD_Installation_Olaf_Hard]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_Installation_SWD_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_Installation]'))
ALTER TABLE [dbo].[SWD_Installation] DROP CONSTRAINT [FK_SWD_Installation_SWD_Product]
GO
GO
/****** Object:  Table [dbo].[SWD_Installation]    Script Date: 03/10/2011 17:57:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SWD_Installation]') AND type in (N'U'))
DROP TABLE [dbo].[SWD_Installation]
/******************************************************************************************************/
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_ProductMatch_R_SoftwareProduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_ProductMatch]'))
ALTER TABLE [dbo].[SWD_ProductMatch] DROP CONSTRAINT [FK_SWD_ProductMatch_R_SoftwareProduct]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_ProductMatch_SWD_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_ProductMatch]'))
ALTER TABLE [dbo].[SWD_ProductMatch] DROP CONSTRAINT [FK_SWD_ProductMatch_SWD_Product]
GO
USE [GMS]
GO
/****** Object:  Table [dbo].[SWD_ProductMatch]    Script Date: 03/10/2011 18:01:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SWD_ProductMatch]') AND type in (N'U'))
DROP TABLE [dbo].[SWD_ProductMatch]
/******************************************************************************************************/
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_Uninstallation_Olaf_Hard]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_Uninstallation]'))
ALTER TABLE [dbo].[SWD_Uninstallation] DROP CONSTRAINT [FK_SWD_Uninstallation_Olaf_Hard]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SWD_Uninstallation_SWD_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SWD_Uninstallation]'))
ALTER TABLE [dbo].[SWD_Uninstallation] DROP CONSTRAINT [FK_SWD_Uninstallation_SWD_Product]
GO
USE [GMS]
GO
/****** Object:  Table [dbo].[SWD_Uninstallation]    Script Date: 03/10/2011 18:01:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SWD_Uninstallation]') AND type in (N'U'))
DROP TABLE [dbo].[SWD_Uninstallation]
/******************************************************************************************************/
GO
/****** Object:  Table [dbo].[SWD_Product]    Script Date: 03/10/2011 18:01:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SWD_Product]') AND type in (N'U'))
DROP TABLE [dbo].[SWD_Product]
/******************************************************************************************************/
GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ClearData]    Script Date: 03/10/2011 18:03:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SWD_ClearData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SWD_ClearData]
/******************************************************************************************************/
GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ImportInstallations]    Script Date: 03/10/2011 18:03:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SWD_ImportInstallations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SWD_ImportInstallations]
/******************************************************************************************************/
GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ProcessImports]    Script Date: 03/10/2011 18:03:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SWD_ProcessImports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_SWD_ProcessImports]
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/
/******************************************************************************************************/
/*************************************CREATES***************************************************/
GO
/****** Object:  Table [dbo].[SWD_Import]    Script Date: 03/10/2011 17:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SWD_Import](
	[MachineName] [nvarchar](max) NOT NULL,
	[SoftwareName] [nvarchar](max) NOT NULL,
	[Version] [nvarchar](max) NULL,
	[AdditionalInfo] [nvarchar](max) NULL,
	[DetectionDate] [datetime] NOT NULL,
	[SourceName] [nvarchar](max) NOT NULL,
	[Hash] [binary](20) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/******************************************************************************************************/

GO
/****** Object:  Index [IX_SWD_Import]    Script Date: 03/10/2011 17:46:20 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_SWD_Import] ON [dbo].[SWD_Import] 
(
	[Hash] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

/******************************************************************************************************/

GO
/****** Object:  Table [dbo].[SWD_Product]    Script Date: 03/10/2011 17:19:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SWD_Product](
	[SoftwareName] [nvarchar](max) NOT NULL,
	[Version] [nvarchar](max) NULL,
	[SourceName] [nvarchar](max) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DetectionDate] [datetime] NOT NULL CONSTRAINT [DF_SWD_Product_DetectionDate]  DEFAULT (((2000)-(1))-(1)),
	[Hash] [binary](20) NOT NULL,
 CONSTRAINT [PK_SWD_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

/******************************************************************************************************/

GO
/****** Object:  Index [IX_SWD_Product]    Script Date: 03/10/2011 17:51:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_SWD_Product] ON [dbo].[SWD_Product] 
(
	[Hash] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

/******************************************************************************************************/

GO
/****** Object:  Table [dbo].[SWD_Installation]    Script Date: 03/10/2011 17:19:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SWD_Installation](
	[Olaf_HardId] [int] NOT NULL,
	[DetectionDate] [datetime] NOT NULL,
	[SWD_ProductId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceName] [nvarchar](max) NOT NULL,
	[AdditionalInfo] [nvarchar](max) NULL,
	[Hash] [binary](20) NOT NULL,
 CONSTRAINT [PK_SWD_Installation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[SWD_Installation]  WITH CHECK ADD  CONSTRAINT [FK_SWD_Installation_Olaf_Hard] FOREIGN KEY([Olaf_HardId])
REFERENCES [dbo].[Olaf_Hard] ([OlafHardId])
GO
ALTER TABLE [dbo].[SWD_Installation] CHECK CONSTRAINT [FK_SWD_Installation_Olaf_Hard]
GO
ALTER TABLE [dbo].[SWD_Installation]  WITH CHECK ADD  CONSTRAINT [FK_SWD_Installation_SWD_Product] FOREIGN KEY([SWD_ProductId])
REFERENCES [dbo].[SWD_Product] ([Id])
GO
ALTER TABLE [dbo].[SWD_Installation] CHECK CONSTRAINT [FK_SWD_Installation_SWD_Product]

/******************************************************************************************************/

GO
/****** Object:  Index [IX_SWD_Installation]    Script Date: 03/10/2011 17:49:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_SWD_Installation] ON [dbo].[SWD_Installation] 
(
	[Hash] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

/******************************************************************************************************/

GO
/****** Object:  Table [dbo].[SWD_Uninstallation]    Script Date: 03/10/2011 17:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SWD_Uninstallation](
	[Olaf_HardId] [int] NOT NULL,
	[DetectionDate] [datetime] NOT NULL,
	[SWD_ProductId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceName] [nvarchar](max) NOT NULL,
	[InstallationDate] [datetime] NOT NULL,
	[AdditonalInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_SWD_Uninstallation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SWD_Uninstallation]  WITH CHECK ADD  CONSTRAINT [FK_SWD_Uninstallation_Olaf_Hard] FOREIGN KEY([Olaf_HardId])
REFERENCES [dbo].[Olaf_Hard] ([OlafHardId])
GO
ALTER TABLE [dbo].[SWD_Uninstallation] CHECK CONSTRAINT [FK_SWD_Uninstallation_Olaf_Hard]
GO
ALTER TABLE [dbo].[SWD_Uninstallation]  WITH CHECK ADD  CONSTRAINT [FK_SWD_Uninstallation_SWD_Product] FOREIGN KEY([SWD_ProductId])
REFERENCES [dbo].[SWD_Product] ([Id])
GO
ALTER TABLE [dbo].[SWD_Uninstallation] CHECK CONSTRAINT [FK_SWD_Uninstallation_SWD_Product]

/******************************************************************************************************/

GO
/****** Object:  Table [dbo].[SWD_ProductMatch]    Script Date: 03/10/2011 17:20:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SWD_ProductMatch](
	[SWD_ProductId] [int] NOT NULL,
	[R_SoftwareProductId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_SWD_ProductMatch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SWD_ProductMatch]  WITH CHECK ADD  CONSTRAINT [FK_SWD_ProductMatch_R_SoftwareProduct] FOREIGN KEY([R_SoftwareProductId])
REFERENCES [dbo].[R_SoftwareProduct] ([Id])
GO
ALTER TABLE [dbo].[SWD_ProductMatch] CHECK CONSTRAINT [FK_SWD_ProductMatch_R_SoftwareProduct]
GO
ALTER TABLE [dbo].[SWD_ProductMatch]  WITH CHECK ADD  CONSTRAINT [FK_SWD_ProductMatch_SWD_Product] FOREIGN KEY([SWD_ProductId])
REFERENCES [dbo].[SWD_Product] ([Id])
GO
ALTER TABLE [dbo].[SWD_ProductMatch] CHECK CONSTRAINT [FK_SWD_ProductMatch_SWD_Product]

/******************************************************************************************************/

GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ClearData]    Script Date: 03/10/2011 17:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Jeanquart
-- Create date: 2011-03-04
-- Description:	Deletes all data from the software detection tables
-- =============================================
CREATE PROCEDURE [dbo].[sp_SWD_ClearData] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    DELETE FROM SWD_Import
	DELETE FROM SWD_Installation
	DELETE FROM SWD_Uninstallation
	DELETE FROM SWD_ProductMatch
	DELETE FROM SWD_Product
	
END

/******************************************************************************************************/

GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ImportInstallations]    Script Date: 03/10/2011 17:21:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Jeanquart
-- Create date: 2011-03-03
-- Description:	Fills the SWD_Import table with the values from LANDesk 9 (and other sources)
-- =============================================
CREATE PROCEDURE [dbo].[sp_SWD_ImportInstallations]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DELETE FROM SWD_Import
    INSERT INTO SWD_Import
	SELECT Comps.DeviceName, Installations.SuiteName, NULLIF(Installations.[Version], 'Unknown'),NULL, 'InstallDate'= CASE WHEN ISDATE(Installations.InstallDate) = 0 THEN GETDATE() ELSE CONVERT(Datetime, Installations.InstallDate) END, 'LANDesk9', 
	HASHBYTES('SHA1', LOWER(Comps.DeviceName + '.'+ Installations.SuiteName + '.' + ISNULL(Installations.[Version], 'Unknown') + '.' + 'LANDesk9'))  
	FROM [s-olaf-vprod223].LDMS90.dbo.AppSoftwareSuites Installations 
	INNER JOIN [s-olaf-vprod223].LDMS90.dbo.Computer Comps ON Comps.Computer_Idn = Installations.Computer_Idn

END
 
/******************************************************************************************************/

GO
/****** Object:  StoredProcedure [dbo].[sp_SWD_ProcessImports]    Script Date: 03/10/2011 17:21:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Jeanquart
-- Create date: 2011-03-03
-- Description:	Processes the information contained in SWD_Import
-- =============================================
CREATE PROCEDURE [dbo].[sp_SWD_ProcessImports] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    --Add new products to the SWD_Product table
	INSERT INTO SWD_Product (SoftwareName, [Version], SourceName, DetectionDate, [Hash]) 
	SELECT Imports.SoftwareName, Imports.[Version], Imports.SourceName, GETDATE(), 
	HASHBYTES('SHA1', LOWER(Imports.SoftwareName + '.' + ISNULL(Imports.Version, 'Unknown') + '.' + Imports.SourceName)) as ImportHash
	FROM SWD_Import Imports
	WHERE HASHBYTES('SHA1', LOWER(Imports.SoftwareName + '.' + ISNULL(Imports.Version, 'Unknown') + '.' + Imports.SourceName)) NOT IN (SELECT [Hash] FROM SWD_Product)
	GROUP BY Imports.SoftwareName, Imports.[Version], Imports.SourceName, HASHBYTES('SHA1', LOWER(Imports.SoftwareName + '.' + ISNULL(Imports.Version, 'Unknown') + '.' + Imports.SourceName))
	
	
	-- Add new unknown hardware to the R_UnknownHardware table
	INSERT INTO R_UnknownHardware
	SELECT ImportHardware.MachineName, GETDATE(),ImportHardware.SourceName FROM (
	SELECT MachineName, SourceName FROM SWD_Import 
	GROUP BY MachineName, SourceName) As ImportHardware
	WHERE 
	(LOWER(ImportHardware.MachineName) NOT IN (SELECT LOWER(OlafHardName) FROM Olaf_Hard WHERE OlafHardName IS NOT NULL)) 
	AND
	ImportHardware.MachineName + '.' + ImportHardware.SourceName NOT IN (SELECT MachineName + '.' + SourceName FROM R_UnknownHardware)
	
	-- Add new uninstallations to the SWD_Uninstallation table and deletes corresponding installations from the SWD_Installations table.
	INSERT INTO SWD_Uninstallation (Olaf_HardId, DetectionDate, SWD_ProductId, SourceName, InstallationDate, AdditonalInfo)
	SELECT Hard.OlafHardId, GETDATE(), Products.Id, Installations.SourceName, Installations.DetectionDate, Installations.AdditionalInfo FROM SWD_Installation Installations
	INNER JOIN Olaf_Hard Hard ON Hard.OlafHardId = Installations.Olaf_HardId
	INNER JOIN SWD_Product Products ON Products.Id = Installations.SWD_ProductId
	WHERE (Installations.[Hash]) NOT IN
	(SELECT [Hash] FROM SWD_Import)
	AND (SELECT COUNT(*) FROM SWD_Import Imports WHERE Hard.OlafHardName = Imports.MachineName AND Installations.SourceName = Imports.SourceName) > 0
	
	DELETE FROM SWD_Installation WHERE Id IN
	(
	SELECT Installations.Id FROM SWD_Installation Installations
	INNER JOIN Olaf_Hard Hard ON Hard.OlafHardId = Installations.Olaf_HardId
	WHERE (Installations.[Hash]) NOT IN
	(SELECT [Hash] FROM SWD_Import)
	AND (SELECT COUNT(*) FROM SWD_Import Imports WHERE Hard.OlafHardName = Imports.MachineName AND Installations.SourceName = Imports.SourceName) > 0
	)
	
	-- Add new installations to the SWD_Installation table
	INSERT INTO SWD_Installation (Olaf_HardId, DetectionDate, SWD_ProductId, SourceName, AdditionalInfo, [Hash]) 
	SELECT Hard.OlafHardId, GETDATE(), Products.Id, Imports.SourceName, Imports.AdditionalInfo 
	,
	HASHBYTES('SHA1', LOWER(Hard.OlafHardName + '.' + Products.SoftwareName + '.' + ISNULL(Products.[Version], 'Unknown') + '.' + Products.Sourcename))
	FROM SWD_Import Imports	
	INNER JOIN Olaf_Hard Hard ON LOWER(Hard.OlafHardName) = LOWER(Imports.MachineName)
	INNER JOIN SWD_Product Products ON 
	(Products.SoftwareName) 
	= 
	(Imports.SoftwareName)
	WHERE Products.[Version] = Imports.[Version]
	AND Products.SourceName = Imports.SourceName
	AND
	Imports.Hash 
	NOT IN 
	(SELECT [Hash] FROM SWD_Installation Installations)
	
END
