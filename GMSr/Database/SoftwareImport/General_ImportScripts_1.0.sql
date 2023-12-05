USE [xxx]
GO
/****** Object:  Table [dbo].[R_UnknownHardware]    Script Date: 03/21/2011 14:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_UnknownHardware](
	[MachineName] [nvarchar](max) NOT NULL,
	[FoundOn] [datetime] NOT NULL,
	[SourceName] [nvarchar](max) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[R_UnknownUser]    Script Date: 03/21/2011 14:20:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_UnknownUser](
	[UserName] [nvarchar](max) NOT NULL,
	[FoundOn] [datetime] NOT NULL,
	[SourceName] [nvarchar](max) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[R_ImportHistory]    Script Date: 03/21/2011 14:21:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[R_ImportHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
	[ChangeType] [nvarchar](450) NOT NULL,
	[NewValue] [nvarchar](max) NOT NULL,
	[SourceName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_R_ImportHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
