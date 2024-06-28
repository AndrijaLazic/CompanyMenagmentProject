USE[CompanyMenagmentProject]
Go

CREATE PROCEDURE [dbo].[spGetUsersShifts]
	@Id [int], 
	@Date date 
AS
BEGIN
	SELECT *
	FROM [dbo].[WorkCalendar]
	WHERE UserId=@Id AND Date>@Date 
END
Go