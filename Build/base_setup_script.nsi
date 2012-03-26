Name iCopy

RequestExecutionLevel admin
# Included files
!include Sections.nsh
!include "MUI2.nsh"
#!include MUI.nsh
!include LogicLib.nsh

# Defines
VIProductVersion     	;Completed by PowershellScript
!define REGKEY "SOFTWARE\$(^Name)"
!define VERSION  		;Completed by PowershellScript
!define COMPANY "Matteo Rossi"
!define URL http://icopy.sourceforge.net

# MUI defines
;!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_ICON "..\Resources\icopy2.ico"
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_LICENSEPAGE_RADIOBUTTONS
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER iCopy
;!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"
!define MUI_UNICON "..\Resources\icopy2.ico"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_NOSTRETCH
!define MUI_HEADERIMAGE_BITMAP "..\Resources\InstBann.bmp" ; optional
!define MUI_HEADERIMAGE_UNBITMAP "..\Resources\InstBann.bmp"

!define MUI_WELCOMEFINISHPAGE_BITMAP "..\Resources\welcome.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "..\Resources\welcome.bmp"

# Reserved Files
ReserveFile "${NSISDIR}\Plugins\System.dll"

# Variables
Var StartMenuGroup

Function .onInit
 
  ReadRegStr $R0 HKLM \
  "Software\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" \
  "UninstallString"
  StrCmp $R0 "" done
 
  MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
  "$(^Name) is already installed. $\n$\nClick `OK` to remove the \
  previous version or `Cancel` to cancel this upgrade." \
  IDOK uninst
  Abort
 
;Run the uninstaller
uninst:
  ClearErrors
  ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file
 
  IfErrors no_remove_uninstaller done
    ;You can either use Delete /REBOOTOK in the uninstaller or add some code
    ;here to remove the uninstaller. Use a registry key to check
    ;whether the user has chosen to uninstall. If you are using an uninstaller
    ;components page, make sure all sections are uninstalled.
  no_remove_uninstaller:
 
done:
 
FunctionEnd
# Register to scanner events page variables
Var Dialog
Var Label
Var Label2
Var Chk
Var Checkbox_State

Function nsDialogsPage
   !insertmacro MUI_HEADER_TEXT "Scanner Buttons" "Register iCopy to your scanner buttons"  
	nsDialogs::Create 1018
	Pop $Dialog

	${If} $Dialog == error
		Abort
	${EndIf}

	${NSD_CreateLabel} 0 0 100% 25u "iCopy can be registered to the scanner buttons. This means that when you press one of the buttons of your scanner you can have iCopy to automatically start a copy process."
	Pop $Label
	${NSD_CreateLabel} 0 25u 100% 30u "In Control Panel, under Scanners and Digital Cameras, you can set iCopy to be the default program when you press your scanner button."
	Pop $Label2
	
	${NSD_CreateCheckBox} 0 60u 100% 13u "Register iCopy to scanner buttons"
	Pop $Chk
	
	${If} $Checkbox_State == ${BST_CHECKED}
		${NSD_Check} $Chk
	${EndIf}

	nsDialogs::Show
FunctionEnd

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "..\Resources\license.rtf"
Page custom nsDialogsPage nsDialogsPageLeave
#!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English
!insertmacro MUI_LANGUAGE Italian
!insertmacro MUI_LANGUAGE German
!insertmacro MUI_LANGUAGE French
!insertmacro MUI_LANGUAGE Spanish

# Installer attributes
OutFile "..\bin\iCopy${VERSION}setup.exe"
InstallDir $PROGRAMFILES\iCopy
CRCCheck on
XPStyle on
ShowInstDetails show
;TargetMinimalOS 5.1    ; target Windows XP or more recent    / make a Unicode installer

VIAddVersionKey ProductName iCopy
VIAddVersionKey ProductVersion "${VERSION}"
VIAddVersionKey CompanyName "${COMPANY}"
VIAddVersionKey CompanyWebsite "${URL}"
VIAddVersionKey FileVersion "${VERSION}"
VIAddVersionKey FileDescription ""
VIAddVersionKey LegalCopyright "2007-2012 Matteo Rossi"
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails show

# Installer sections
Section -Main SEC0000
    #Main DIR
    SetOutPath $INSTDIR
    SetOverwrite on
	
	File /r ..\bin\Release\*-*
	File /r ..\bin\Release\*.exe
	File /r ..\bin\Release\*.dll
	File /r ..\bin\Release\*.txt
    File ..\bin\Release\iCopy.exe.config
	
    WriteRegStr HKLM "${REGKEY}\Components" Main 1
SectionEnd

Section -Reg SEC0001
	Exec '"$INSTDIR\iCopy.exe" /reg'
	
    WriteRegStr HKLM "${REGKEY}\Components" Reg 1
SectionEnd

Function nsDialogsPageLeave
	${NSD_GetState} $Chk $Checkbox_State
	${If} $Checkbox_State == ${BST_CHECKED}
		!insertmacro SelectSection ${SEC0001}
	${Else}
		!insertmacro UnselectSection ${SEC0001}
	${EndIf}
FunctionEnd

Section -post SEC0002
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall.exe
    !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortCut "$SMPROGRAMS\$StartMenuGroup\iCopy.lnk" "$INSTDIR\iCopy.exe"
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\Uninstall $(^Name).lnk" $INSTDIR\uninstall.exe
    !insertmacro MUI_STARTMENU_WRITE_END
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" URLInfoAbout "${URL}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

Section -un.Reg UNSEC0001
	Exec '"$INSTDIR\iCopy.exe" /unreg'
	Sleep 1000
SectionEnd

# Uninstaller sections
Section /o -un.Main UNSEC0002
/*     Delete /REBOOTOK $INSTDIR\wiaaut.dll
    Delete /REBOOTOK $INSTDIR\README.txt
    Delete /REBOOTOK $INSTDIR\License.txt
    Delete /REBOOTOK $INSTDIR\Interop.WIA.dll
    Delete /REBOOTOK $INSTDIR\iCopy.exe.config
    Delete /REBOOTOK $INSTDIR\iCopy.exe.manifest
    Delete /REBOOTOK $INSTDIR\iCopy.exe
    Delete /REBOOTOK $INSTDIR\iCopy.application
    Delete /REBOOTOK $INSTDIR\iCopy.settings
    Delete /REBOOTOK $INSTDIR\Changelog.txt
    Delete /REBOOTOK $INSTDIR\Microsoft.WindowsAPICodePack.dll 
	Delete /REBOOTOK $INSTDIR\*
	
    RmDir /r $INSTDIR\bg-BG 
    RmDir /r $INSTDIR\de-DE 
    RmDir /r $INSTDIR\it-IT 
    RmDir /r $INSTDIR\ja-JP
    RmDir /r $INSTDIR\nl-NL
    RmDir /r $INSTDIR\ru-RU
    RmDir /r $INSTDIR\sk-SK
    RmDir /r $INSTDIR\es-ES
    RmDir /r $INSTDIR\fr-FR*/

    RmDir /r $INSTDIR

    DeleteRegValue HKLM "${REGKEY}\Components" Main
SectionEnd


Section -un.post UNSEC0003
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\Uninstall $(^Name).lnk"
    Delete /REBOOTOK "$SMPROGRAMS\$StartMenuGroup\iCopy.lnk"
    Delete /REBOOTOK $INSTDIR\uninstall.exe
    DeleteRegValue HKLM "${REGKEY}" StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"
    RmDir /REBOOTOK $SMPROGRAMS\$StartMenuGroup
    RmDir /REBOOTOK $INSTDIR
    Push $R0
    StrCpy $R0 $StartMenuGroup 1
    StrCmp $R0 ">" no_smgroup
no_smgroup:
    Pop $R0
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir
FunctionEnd

# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro SELECT_UNSECTION Reg ${UNSEC0001}
    !insertmacro SELECT_UNSECTION Main ${UNSEC0002}
    !insertmacro SELECT_UNSECTION Main ${UNSEC0003}
FunctionEnd

; # Section Descriptions
; !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
; !insertmacro MUI_DESCRIPTION_TEXT ${SEC0001} "If you choose to install this option, iCopy will be added to the list of available programs when clicking on a scanner button. It is useful if you wish that iCopy is launched automatically when you press a button."
; !insertmacro MUI_FUNCTION_DESCRIPTION_END
