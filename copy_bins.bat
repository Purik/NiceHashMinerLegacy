@echo off
set CFG=%1
chcp 437

echo Cnfiguration: "%CFG%"
echo CurDir: "%cd%"

xcopy %cd%\AMDOpenCLDeviceDetection\%CFG% %cd%\%CFG% /Y /EXCLUDE:ignorefiles.txt
copy %cd%\setcpuaff\%CFG%\setcpuaff.vcxproj.exe %cd%\setcpuaff\%CFG%\setcpuaff.exe
xcopy %cd%\setcpuaff\%CFG% %cd%\%CFG% /Y /EXCLUDE:ignorefiles.txt