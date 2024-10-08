USE [CompanyMenagmentProject]
IF NOT EXISTS (SELECT 1 as usersTable FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
begin
	/****** Object:  Table [dbo].[Users]    Script Date: 12-Jul-24 17:03:54 ******/
	SET ANSI_NULLS ON
	SET QUOTED_IDENTIFIER ON
	CREATE TABLE [dbo].[Users](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](30) NOT NULL,
		[Lastname] [nvarchar](30) NOT NULL,
		[Email] [nvarchar](100) NOT NULL,
		[PasswordHash] [varbinary](max) NOT NULL,
		[PasswordSalt] [varbinary](max) NOT NULL,
		[PhoneNumber] [nvarchar](30) NOT NULL,
		[WorkerType] [tinyint] NOT NULL,
	 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	 
	 CONSTRAINT [UQ_PhoneNumber] UNIQUE NONCLUSTERED 
	(
		[PhoneNumber] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	 
	 CONSTRAINT [UQ_Email] UNIQUE NONCLUSTERED 
	(
		[Email] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
end