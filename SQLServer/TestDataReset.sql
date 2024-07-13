USE [CompanyMenagmentProject]
GO

INSERT INTO [dbo].[WorkerTypes]
           ([Id]
           ,[TypeName])
     VALUES
           (0,'Admin'),
		   (1,'Menager'),
		   (2,'Worker')
GO



INSERT INTO [dbo].[Users]
           ([Name]
           ,[Lastname]
           ,[Email]
           ,[PasswordHash]
           ,[PasswordSalt]
           ,[PhoneNumber]
           ,[WorkerType])
     VALUES
           ('User1'
           ,'LastName1'
           ,'User1@gmail.com'
           ,<PasswordHash, varbinary(max),>
           ,<PasswordSalt, varbinary(max),>
           ,'123456'
           ,0)
GO
