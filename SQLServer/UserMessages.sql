USE [CompanyMenagmentProject]
GO
/****** Object:  Table [dbo].[CommunicationMessages]    Script Date: 06-Aug-24 23:56:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommunicationMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommunicationId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[Message] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CommunicationMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


