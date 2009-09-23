@echo off
cls
..\Dependencies\nant\bin\NAnt.exe -buildfile:AdamDotCom.Resume.Service.build %*
pause