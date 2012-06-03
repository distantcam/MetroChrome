@echo off
call "%VS100COMNTOOLS%vsvars32.bat"

msbuild.exe build.proj /p:Configuration=Release

pause