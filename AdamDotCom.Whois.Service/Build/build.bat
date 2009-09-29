@echo off
cls
..\Dependencies\nant\bin\NAnt.exe -buildfile:AdamDotCom.Whois.Service.build %*
pause