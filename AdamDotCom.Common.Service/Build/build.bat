@echo off
cls
..\Dependencies\nant\bin\NAnt.exe -buildfile:AdamDotCom.Common.Service.build %*
pause