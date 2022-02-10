@echo off

echo.
echo ### Deleting previous builds...
rmdir /s /q -r Release
del *.zip

echo.
echo ### Deleting previous builds...
dotnet build ..\src\BasicTimer\ --configuration Release --output Release

echo.
echo ### Zipping...
tar.exe -a -cf BasicTimer.zip Release


echo.
echo ### DONE
pause