'iCopy - Simple Photocopier
'Copyright (C) 2007-2011 Matteo Rossi

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <http://www.gnu.org/licenses/>.

Option Explicit On
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Module NativeMethods


    'API declarations
    Private Declare Function LoadLibraryA Lib "kernel32" (ByVal lLibFileName As String) As Long
    Private Declare Function CreateThread Lib "kernel32" (ByVal lThreadAttributes As Object, ByVal lStackSize As UInteger, ByVal lStartAddress As UInteger, ByVal larameter As UInteger, ByVal lCreationFlags As UInteger, ByVal lThreadID As UInteger) As UInteger
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal lMilliseconds As Integer) As UInteger
    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lProcName As String) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function GetExitCodeThread Lib "kernel32" (ByVal hThread As Integer, ByVal lExitCode As UInteger) As Long
    Private Declare Sub ExitThread Lib "kernel32" (ByVal lExitCode As UInteger)

    Private Declare Function FreeLibrary Lib "kernel32" (ByVal hLibModule As Integer) As Integer

    Function RegisterComponent(ByVal sFilePath As String, Optional ByVal bRegister As Boolean = True) As Boolean
        Dim lLibAddress As Long, lProcAddress As Long, lThreadID As Long, lSuccess As Long, lExitCode As Long, lThread As Long
        Dim sRegister As String
        Const clMaxTimeWait As Long = 20000     'Wait 20 secs for register to complete

        On Error GoTo ErrFailed
        If Len(sFilePath) > 0 And Len(Dir(sFilePath)) > 0 Then
            'File exists

            'Register/Unregister DLL
            If bRegister Then
                sRegister = "DllRegisterServer"
            Else
                sRegister = "DllUnRegisterServer"
            End If

            'Load library into current process
            lLibAddress = LoadLibraryA(sFilePath)

            If lLibAddress Then
                'Get address of the DLL function
                lProcAddress = GetProcAddress(lLibAddress, sRegister)
                If lProcAddress Then
                    lThread = CreateThread(0&, 0&, lProcAddress, 0&, 0&, lThread)
                    If lThread Then
                        'Created thread and wait for it to terminate
                        lSuccess = (WaitForSingleObject(lThread, clMaxTimeWait) = 0)
                        If Not lSuccess Then
                            'Failed to register, close thread
                            Call GetExitCodeThread(lThread, lExitCode)
                            Call ExitThread(lExitCode)
                            RegisterComponent = False
                        Else
                            'Successfully registered component
                            RegisterComponent = True
                            Call CloseHandle(lThread)
                        End If
                    End If
                    Call FreeLibrary(lLibAddress)
                Else
                    'Object doesn't expose OLE interface
                    Call FreeLibrary(lLibAddress)
                End If
            End If
        End If
        Exit Function

ErrFailed:
        Debug.Print(Err.Description)
        Debug.Assert(False)
        On Error GoTo 0
    End Function

End Module

Module ScannerEvents
    Dim GUID As String = GetGUID()
    Const WIACLSID As String = "{A55803CC-4D53-404c-8557-FD63DBA95D24}"

    Function GetGUID() As String
        Dim assy As [Assembly]
        Dim Attributes As Object()

        assy = [Assembly].GetExecutingAssembly
        Attributes = assy.GetCustomAttributes(GetType(GuidAttribute), False)

        If Attributes.Length = 0 Then
            Return Nothing
        Else
            Dim tmp As String = DirectCast(Attributes(0), GuidAttribute).Value.ToUpper()
            Return "{" & tmp & "}"
        End If
    End Function

    Sub RemoveFromScannerEvents()
        'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoplayHandlers\Handlers\"
        Dim HandlersKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoplayHandlers\Handlers", True)
        Try
            HandlersKey.DeleteSubKeyTree("WIA_" & GUID)
        Catch ex As ArgumentException

        End Try
        HandlersKey.Close()

        'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\StillImage\Registered Applications
        Dim STIKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\StillImage\Registered Applications", True)
        STIKey.DeleteValue("iCopy")
        STIKey.Close()

        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\StillImage\Events\STIProxyEvent
        Dim STIProxyEventsKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\StillImage\Events\STIProxyEvent", True)
        Try
            STIProxyEventsKey.DeleteSubKeyTree(GUID)
        Catch ex As ArgumentException

        End Try
        STIProxyEventsKey.Close()

        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet002\Control\StillImage\Events\STIProxyEvent
        STIProxyEventsKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet002\Control\StillImage\Events\STIProxyEvent", True)
        Try
            STIProxyEventsKey.DeleteSubKeyTree(GUID)
        Catch ex As ArgumentException

        End Try
        STIProxyEventsKey.Close()
        'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\StillImage\Events\STIProxyEvent
        STIProxyEventsKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\StillImage\Events\STIProxyEvent", True)
        Try
            STIProxyEventsKey.DeleteSubKeyTree(GUID)
        Catch ex As ArgumentException

        End Try
        STIProxyEventsKey.Close()
    End Sub

    '''Adds the registry keys requested in order to make iCopy be listed in scanner events
    Sub RegisterToScannerEvents()
        'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoplayHandlers\Handlers\"
        Dim HandlersKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoplayHandlers\Handlers", True)
        Dim WIAHandlerKey As RegistryKey = HandlersKey.CreateSubKey("WIA_" & GUID)
        WIAHandlerKey.SetValue("Action", "Copy with iCopy")
        WIAHandlerKey.SetValue("CLSID", WIACLSID)
        WIAHandlerKey.SetValue("DefaultIcon", "sti.dll,0")
        WIAHandlerKey.SetValue("InitCmdline", "/WiaCmd;" & Application.ExecutablePath & " /StiDevice:%1 /StiEvent:%2;")
        WIAHandlerKey.SetValue("Provider", "iCopy")

        WIAHandlerKey.Close()
        HandlersKey.Close()

        'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\StillImage\Registered Applications
        Dim STIKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\StillImage\Registered Applications", True)
        STIKey.SetValue("iCopy", Application.ExecutablePath)
        STIKey.Close()

        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\StillImage\Events\STIProxyEvent
        Dim STIProxyEventsKey As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\StillImage\Events\STIProxyEvent", True)
        Dim icopySTIProxyEventsKey As RegistryKey = STIProxyEventsKey.CreateSubKey(GUID)
        icopySTIProxyEventsKey.SetValue("Cmdline", Application.ExecutablePath & " /StiDevice:%1 /StiEvent:%2")
        icopySTIProxyEventsKey.SetValue("Desc", "Directly print with iCopy")
        icopySTIProxyEventsKey.SetValue("Icon", Application.ExecutablePath & ",0")
        icopySTIProxyEventsKey.SetValue("Name", "iCopy")

        icopySTIProxyEventsKey.Close()
        STIProxyEventsKey.Close()

        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet002\Control\StillImage\Events\STIProxyEvent
        STIProxyEventsKey = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet002\Control\StillImage\Events\STIProxyEvent", True)
        icopySTIProxyEventsKey = STIProxyEventsKey.CreateSubKey(GUID)
        icopySTIProxyEventsKey.SetValue("Cmdline", Application.ExecutablePath & " /StiDevice:%1 /StiEvent:%2")
        icopySTIProxyEventsKey.SetValue("Desc", "Directly print with iCopy")
        icopySTIProxyEventsKey.SetValue("Icon", Application.ExecutablePath & ",0")
        icopySTIProxyEventsKey.SetValue("Name", "iCopy")

        icopySTIProxyEventsKey.Close()
        STIProxyEventsKey.Close()

        'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\StillImage\Events\STIProxyEvent
        STIProxyEventsKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\StillImage\Events\STIProxyEvent", True)
        icopySTIProxyEventsKey = STIProxyEventsKey.CreateSubKey(GUID)
        icopySTIProxyEventsKey.SetValue("Cmdline", Application.ExecutablePath & " /StiDevice:%1 /StiEvent:%2")
        icopySTIProxyEventsKey.SetValue("Desc", "Directly print with iCopy")
        icopySTIProxyEventsKey.SetValue("Icon", Application.ExecutablePath & ",0")
        icopySTIProxyEventsKey.SetValue("Name", "iCopy")

        icopySTIProxyEventsKey.Close()
        STIProxyEventsKey.Close()
    End Sub
End Module