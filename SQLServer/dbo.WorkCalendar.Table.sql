USE [CompanyMenagmentProject]
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkCalendar]') AND type in (N'U'))
begin
	/****** Object:  Table [dbo].[WorkCalendar]    Script Date: 12-Jul-24 17:03:54 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[WorkCalendar](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Date] [date] NOT NULL,
		[Shift] [tinyint] NOT NULL,
		[UserId] [int] NOT NULL,
	 CONSTRAINT [PK_WorkCalendar] PRIMARY KEY CLUSTERED 
	(
		[Date] ASC,
		[Shift] ASC,
		[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
end


