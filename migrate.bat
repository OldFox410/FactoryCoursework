@echo off
chcp 65001 > nul
"Factory.Migrator/bin/Debug/net11.0/Factory.Migrator.exe" ./Migrations ./Factory.WebClient/
pause