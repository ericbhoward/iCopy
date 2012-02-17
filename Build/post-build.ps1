# Automated script for creating setup and zip files
# Project folder
$ProjDir = "D:\Matteo\Documenti\Visual Studio 2010\Projects\iCopy\iCopy"

Set-Location "$ProjDir"

# NEEDED:
#	* NSIS Installer
$NSISPath = "C:\Program Files (x86)\UTILITIES\NSIS Installer"
#	* 7za.exe in $PWD
#	* base_setup_script.nsi in $PWD
$NSISBaseScript = "Build\base_setup_script.nsi"
$NSISOutScript = "Build\setup.nsi"

del "bin\*.exe"
del "bin\*.zip"

#Gets verison
$FullVersion = (dir bin\Release\iCopy.exe).VersionInfo.FileVersion
$Version = $FullVersion.Substring(0,5)

#Inserts the version in the NSIS File and creates a temporary file
(gc $NSISBaseScript) -replace "VIProductVersion", "$& $FullVersion" | sc $NSISOutScript
(gc $NSISOutScript) -replace "!define VERSION", "$& $Version" | sc $NSISOutScript

#Creates the installer
Set-Location "$NSISPath"
.\makensis.exe "$ProjDir\$NSISOutScript" 

Set-Location "$ProjDir"

del $NSISOutScript #Delete temporary script
Set-Location .\bin\Release
#Now creates the zip archive
..\..\Build\7za.exe a -tzip -mx9 -r "..\iCopy$Version.zip" -x!*log -x!*settings -x!*vshost* *