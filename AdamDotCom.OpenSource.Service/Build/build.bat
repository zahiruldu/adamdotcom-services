@echo off
cls
..\Dependencies\nant\bin\NAnt.exe -buildfile:AdamDotCom.OpenSource.Service.build %*
pause