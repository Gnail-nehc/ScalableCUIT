@echo off
set pid=1
set tcid=1
set runtimes=1
set logFolder=c:\logs

setlocal ENABLEDELAYEDEXPANSION

FOR %%A IN (%tcid%) DO (
	for /l %%i in (1,1,%runtimes%) do (
		for /f "tokens=1,2,3,4 delims=/ " %%a in ("%date%") do set month=%%b&set day=%%c&set year=%%d
		set currentDate=!year!-!month!-!day!
		if defined %time:~0,1% (set hour=%time:~1,1%) else set hour=%time:~0,2%
		set logFile=%logFolder%\PRJ%pid%_TC%tcid%_RT%%i_!currentDate!_!hour!-%time:~3,2%-%time:~6,2%.trx
		TestAgent !pid! %%A !logFile!
	)
)
endlocal