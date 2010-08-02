powershell -Command "& {Import-Module .\tools\psake\psake.psm1; Invoke-psake .\default.ps1 %1 -framework 4.0}"
