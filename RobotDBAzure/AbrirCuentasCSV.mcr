IF WINDOW EXISTS : Log Cuentas CSV : 0
SWITCH TO WINDOW : Log Cuentas CSV : 0
ELSE
OPEN FILE : LogCuentasCSV.exe :  : 1
WAIT FOR : Log Cuentas CSV : appear : 0 : 0
SWITCH TO WINDOW : Log Cuentas CSV : 0
ENDIF
