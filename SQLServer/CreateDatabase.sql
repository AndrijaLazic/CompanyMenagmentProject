IF EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'CompanyMenagmentProject'
)
BEGIN
	USE [master];
	ALTER DATABASE [CompanyMenagmentProject] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE [CompanyMenagmentProject];
END

CREATE DATABASE CompanyMenagmentProject;



