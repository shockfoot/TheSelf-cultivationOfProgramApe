chcp 65001

@echo off
setlocal enabledelayedexpansion

set count=1
for %%f in (*.jpg *.jpeg *.png) do (
    set newname=20160811学校!count!
    ren "%%f" "!newname!%%~xf"
    set /a count+=1
)

endlocal