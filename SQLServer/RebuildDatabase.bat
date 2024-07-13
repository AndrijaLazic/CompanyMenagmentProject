@echo off
SET SERVER=DESKTOP-6F991P0
SET DATABASE=CompanyMenagmentProject
SET USERNAME=DESKTOP-6F991P0\kebab
SET PASSWORD=
::Set current directory to batch file directory
CD /D %~dp0
SET MAIN_SCRIPT_PATH=CreateDatabase.sql
:: -U %USERNAME% -P %PASSWORD%


ECHO Starting main script...
sqlcmd -S %SERVER% -i %MAIN_SCRIPT_PATH%


set tableNames[0]=dbo.Users.Table.sql
set tableNames[1]=dbo.WorkCalendar.Table.sql
set tableNames[2]=dbo.WorkerTypes.Table.sql
set tableNames[3]=TestDataReset.sql

FOR /L %%f IN (0,1,3) DO (
	call ECHO Running %%tableNames[%%f]%% ...
	
	call sqlcmd -S %SERVER% -d %DATABASE% -i %%tableNames[%%f]%%
	IF %ERRORLEVEL% NEQ 0 (
	call ECHO Failed to execute %%tableNames[%%f]%%.
	EXIT /B %ERRORLEVEL%
	) ELSE (
		call ECHO %%tableNames[%%f]%% executed successfully.
	)
)

sqlcmd -S %SERVER% -i "CreateCONSTRAINTS.sql"


echo;
echo;
echo;

echo "Starting to rebuild procedures"
CALL  StoredProcedures/RebuildProcedures.bat

pause