USE [CompanyMenagmentProject]
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkCalendar]') AND type in (N'U'))
begin
	/****** Object:  Table [dbo].[WorkCalendar]    Script Date: 12-Jul-24 17:03:54 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[WorkCalendar](
		[Date] [date] NOT NULL,
		[Shift] [tinyint] NOT NULL,
		[UserId] [int] NOT NULL
	) ON [PRIMARY]
end


