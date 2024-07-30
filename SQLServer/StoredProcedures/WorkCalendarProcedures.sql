USE[CompanyMenagmentProject]
Go

CREATE PROCEDURE spGetUsersShifts
	@Id [int], 
	@Date date,
	@Offset [int],
	@NumOfRows [int]
AS
BEGIN
	SELECT *
	FROM [dbo].[WorkCalendar]
	WHERE UserId=@Id AND Date>=@Date
	ORDER BY Date
	OFFSET @Offset ROWS
	FETCH NEXT @NumOfRows ROWS ONLY;
END
Go

CREATE PROCEDURE spGetShifsForAllUsers
	@Date date,
	@Offset [int],
	@NumOfRows [int]
AS
BEGIN
	SELECT WorkCalendar.Id as RowId,Date,Shift,UserId,Name,Lastname,WorkerType
	FROM WorkCalendar
	INNER JOIN Users on WorkCalendar.UserId = Users.Id
	WHERE Date=@Date 
	ORDER BY Date
	OFFSET @Offset ROWS
	FETCH NEXT @NumOfRows ROWS ONLY;
END
GO