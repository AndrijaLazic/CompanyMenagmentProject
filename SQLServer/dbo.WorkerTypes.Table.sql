USE [CompanyMenagmentProject]
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkerTypes]') AND type in (N'U'))
begin
	/****** Object:  Table [dbo].[WorkerTypes]    Script Date: 12-Jul-24 17:03:54 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[WorkerTypes](
		[Id] [tinyint] NOT NULL,
		[TypeName] [nchar](20) NOT NULL,
	 CONSTRAINT [PK_WorkerTypes] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
end



