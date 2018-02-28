@echo off
set CFG=%1
chcp 437

echo Cnfiguration: "%CFG%"

xcopy AMDOpenCLDeviceDetection\%CFG% %CFG% /Y /EXCLUDE:ignorefiles.txt
copy setcpuaff\%CFG%\setcpuaff.vcxproj.exe setcpuaff\%CFG%\setcpuaff.exe
xcopy setcpuaff\%CFG% %CFG% /Y /EXCLUDE:ignorefiles.txt