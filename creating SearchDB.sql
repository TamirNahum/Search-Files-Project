USE [master] 
GO


CREATE DATABASE [SearchDB]  
GO


USE [SearchDB]
GO

/****** Object:  Table [dbo].[SearchResults]    Script Date: 24/07/2018 17:23:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SearchResults](
	[ResultId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](128) NOT NULL,
	[FilePath] [nvarchar](256) NOT NULL,
	[SearchId] [int] NOT NULL,
 CONSTRAINT [PK_SearchResults] PRIMARY KEY CLUSTERED 
(
	[ResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SearchResults]  WITH CHECK ADD  CONSTRAINT [FK_SearchResults_SearchDetails] FOREIGN KEY([SearchId])
REFERENCES [dbo].[SearchDetails] ([SearchId])
GO

ALTER TABLE [dbo].[SearchResults] CHECK CONSTRAINT [FK_SearchResults_SearchDetails]
GO


USE [SearchDB]
GO

/****** Object:  Table [dbo].[SearchDetails]    Script Date: 24/07/2018 17:24:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SearchDetails](
	[SearchId] [int] IDENTITY(1,1) NOT NULL,
	[SearchString] [nvarchar](256) NOT NULL,
	[SearchFolder] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_SearchDetails] PRIMARY KEY CLUSTERED 
(
	[SearchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
