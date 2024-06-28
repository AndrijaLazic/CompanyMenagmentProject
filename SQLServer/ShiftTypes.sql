USE [CompanyMenagmentProject]
GO

/****** Object:  Table [dbo].[ShiftTypes]    Script Date: 26-Jul-24 23:35:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShiftTypes](
	[ShiftNumber] [tinyint] NOT NULL,
	[StartTime] [char](20) NOT NULL,
	[EndTime] [char](20) NOT NULL,
 CONSTRAINT [PK_ShiftTypes] PRIMARY KEY CLUSTERED 
(
	[ShiftNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

