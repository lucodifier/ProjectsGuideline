Create DataBase Guideline
GO
Use Guideline
GO
/****** Object:  Table [dbo].[User]    Script Date: 13/01/2021 00:00:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Login] [nvarchar](30) NULL,
	[Email] [nvarchar](255) NULL,
	[Document] [varchar](15) NULL,
	[Pass] [nvarchar](20) NULL,
	[Created] [datetimeoffset](7) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Id]  DEFAULT (newid()) FOR [Id]
GO
