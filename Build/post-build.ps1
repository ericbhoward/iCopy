# Automated script for creating setup and zip files
# Project folder
$ProjDir = "D:\Matteo\Documenti\Visual Studio 2010\Projects\iCopy\iCopy"

# NEEDED:
#	* NSIS Installer
$NSISPath = "C:\Program Files (x86)\UTILITIES\NSIS Installer"
#	* 7za.exe in $PWD
#	* base_setup_script.nsi in $PWD
$NSISBaseScript = "$ProjDir\Build\base_setup_script.nsi"
$NSISOutScript = "$ProjDir\Build\setup.nsi"

del "$ProjDir\bin\*.exe"
del "$ProjDir\bin\*.zip"

#Gets verison
$FullVersion = (dir "$ProjDir\bin\Release\iCopy.exe").VersionInfo.FileVersion
$Version = $FullVersion.Substring(0,5)

#Creates the installer
#Set-Location "$NSISPath"
."$NSISPath\makensis.exe" "/DVERSION=$Version" /X"VIProductVersion $FullVersion" "$NSISOutScript" 

# del $NSISOutScript #Delete temporary script
#Set-Location $ProjDir\bin\Release
#Now creates the zip archive
."$ProjDir\Build\7za.exe" a -tzip -mx9 -r "$ProjDir\bin\iCopy$Version.zip" -x!*log -x!*settings -x!*vshost* "$ProjDir\bin\Release\*"