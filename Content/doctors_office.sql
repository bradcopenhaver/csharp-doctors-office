USE [doctors_office]
GO
/****** Object:  Table [dbo].[doctors]    Script Date: 12/6/2016 3:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[doctors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[specialty_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[patients]    Script Date: 12/6/2016 3:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[ailment] [varchar](255) NULL,
	[doctor_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[specialties]    Script Date: 12/6/2016 3:19:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[specialties](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
