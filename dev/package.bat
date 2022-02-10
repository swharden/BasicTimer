@echo off

echo.
echo ### Deleting previous builds...
rmdir /s /q -r BasicTimer
del *.zip

echo.
echo ### Building Release...
dotnet build ..\src\BasicTimer\ --configuration Release --output Release
move Release BasicTimer

echo.
echo ### Zipping...
tar.exe -a -cf BasicTimer.zip BasicTimer


echo.
echo ### DONE
pause