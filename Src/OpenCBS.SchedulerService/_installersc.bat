@echo OFF
echo Stopping old service version...
net stop "OpenCBS Scheduler Service"
echo Uninstalling old service version...
sc delete "OpenCBS Scheduler Service"

echo Installing service...
rem DO NOT remove the space after "binpath="!
sc create "OpenCBS Scheduler Service" binpath= "bin\Debug\OpenCBS.SchedulerService.exe" start= auto
echo Starting server complete
pause


