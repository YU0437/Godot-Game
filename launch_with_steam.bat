@echo off
:: 记录启动时间
echo %date% %time% >> steam_time.log

:: 启动Godot并保持进程
start "" /wait /b "C:\Users\xinyu\Work\Godot_v4.4-stable_mono_win64\Godot_v4.4-stable_mono_win64.exe" --path %cd%

:: 记录结束时间
echo %date% %time% >> steam_time.log