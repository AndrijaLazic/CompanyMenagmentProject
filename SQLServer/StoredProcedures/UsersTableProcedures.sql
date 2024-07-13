USE[CompanyMenagmentProject]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Andrija Lazic
-- Create date: 13.07.2024
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spInsertNewUser]
	@Name [nvarchar](30), 
	@Lastname [nvarchar](30), 
	@Email [nvarchar](100), 
	@PasswordHash [varbinary](max), 
	@PasswordSalt [varbinary](max), 
	@PhoneNumber [nvarchar](30), 
	@WorkerType tinyint
AS
BEGIN
	INSERT INTO [dbo].[Users]
           ([Name]
           ,[Lastname]
           ,[Email]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PhoneNumber]
           ,[WorkerType])
     VALUES
           (@Name
           ,@Lastname
           ,@Email
           ,@PasswordHash
           ,@PasswordSalt
           ,@PhoneNumber
           ,@WorkerType)
END
Go


CREATE PROCEDURE spSelectAllUsers
AS
BEGIN
	SELECT *
    FROM [dbo].[Users]
END
GO
