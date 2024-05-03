@echo off
setlocal

set "params=%*"
set "%params: =" & set "%"

if [%help%] == [true] (
	echo Counts the number of subdirectories.
	echo count_subdirectories.bat directory=directory [hidden=true/false]
	echo directory	Specifies the directory to count the number of subdirectories
	echo hidden		Flag to specify the need to count hidden subfoders. If blank - all subfoders are counted, true - only hidden subfoders, false - only visible subfoders.
) else if [%directory%] == [] (
	exit /b
) else if [%hidden%] == [] (
	dir /a:d /s /b %directory% | find /c ":\"
) else if [%hidden%] == [true] (
	dir /a:dh /s /b %directory% | find /c ":\"
) else (dir /a:d-h /s /b %directory% | find /c ":\")

exit /b 0