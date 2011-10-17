﻿'iCopy - Simple Photocopier
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

Imports WIA
Imports System.ComponentModel
Imports System.Globalization
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text.RegularExpressions

<Assembly: CLSCompliantAttribute(True)> 
Class appControl

    Shared WithEvents manager As New DeviceManager
    Shared _deviceID As String
    Shared _device As Device
    Shared _wscanner As WIA.Item
    Shared console_fs As FileStream
    Private Shared LocRM As New System.Resources.ResourceManager("iCopy.WinFormStrings", GetType(mainFrm).Assembly)
    Private Shared GetCulturesThread As New Threading.Thread(AddressOf GetAvailableLanguages)

    Private Shared _availableCultures As Globalization.CultureInfo()
    Private Shared HasThreadFinished As Boolean
    Private Shared WithEvents _scanner As Scanner
    Private Shared WithEvents _printer As New Printer
    Private Shared CommandLine As Boolean

    Public Shared MainForm As mainFrm

    Shared Sub Main(ByVal sArgs() As String)
        Application.EnableVisualStyles()

        If My.Settings.LastScanSettings Is Nothing Then
            My.Settings.LastScanSettings = New ScanSettings()
        End If

        Dim tmp As TextWriter 'Temporary output stream for the console
        Dim sw As StreamWriter
        If console_fs Is Nothing Then
            Try
                console_fs = New FileStream("iCopy.log", FileMode.Create)
            Catch ex As UnauthorizedAccessException
                Dim path As String = Application.LocalUserAppDataPath

                path = path.Replace(Application.CompanyName + "\", "")
                path = path.Replace(Application.ProductVersion, "")
                Try
                    IO.Directory.CreateDirectory(path)
                    console_fs = New FileStream(path + "iCopy.log", FileMode.Create)
                Catch e As Exception

                End Try
            End Try
            sw = New StreamWriter(console_fs)
            sw.AutoFlush = True
            tmp = Console.Out
            Console.SetOut(sw)
        End If

#If Not Debug Then
            Try
#End If
        If sArgs.Length = 0 Then 'If there are no arguments, run app normally
            CommandLine = False
            'Avoids that two processes run simultaneously
            If Process.GetProcessesByName("icopy").Length > 1 Then
                MsgBox(LocRM.GetString("Msg_AlreadyRunning"), MsgBoxStyle.Information, "iCopy")
                Throw New Exception("Exit")
            End If

            'Searches for languages installed
            Try            'Should avoid ThreadStateException
                If GetCulturesThread.ThreadState = Threading.ThreadState.Unstarted Then
                    GetCulturesThread.Start()
                End If
            Catch ex As Threading.ThreadStateException
                MsgBox(ex.ToString)
            End Try

            'Initializes new scanning interface
            appControl.CreateScanner(My.Settings.DeviceID)

            Try
                My.Settings.DeviceID = _scanner.DeviceId
            Catch ex As NullReferenceException
                Application.Exit()
            End Try

            MainForm = New mainFrm()
            Application.Run(MainForm)

            My.Settings.Save()

        Else    'Handle Command line arguments
            CommandLine = True 'To inform the program that it is running in command line mode

            'Prints the argument list for debugging purpose
            Dim argstring As String = ""
            For Each arg As String In sArgs
                argstring += arg + " "
            Next
            Console.WriteLine("Command Line parameters: {0}", argstring)

            Dim settings As ScanSettings = My.Settings.LastScanSettings

            'Command line arguments parsing
            'STEP 1 Parameters with an argument
            Dim i As Integer
            For i = 0 To sArgs.GetUpperBound(0)
                Try
                    Select Case sArgs(i)
                        Case "/brightness", "/b"
                            settings.Brightness = CType(sArgs(i + 1), Integer)
                        Case "/contrast", "/cnt"
                            settings.Contrast = CType(sArgs(i + 1), Integer)
                        Case "/resolution", "/r"
                            settings.Resolution = CType(sArgs(i + 1), Integer)
                        Case "/copies", "/nc"
                            settings.Copies = CType(sArgs(i + 1), Integer)
                        Case "/scaling", "/s"
                            settings.Scaling = CType(sArgs(i + 1), Integer)
                        Case "/printer"
                            Try
                                _printer.Name = sArgs(i + 1)
                            Catch ex As ArgumentException
                                MsgBox("The provided printer name is not valid. Using default printer.")
                            End Try
                        Case "/path"
                            settings.Path = sArgs(i + 1)
                    End Select
                Catch ex As InvalidCastException
                    MsgBox("Command line parsing failed. See README for correct sintax")
                    GoTo exit_app
                End Try

                'STEP 2 Parameters without an argument
                Select Case sArgs(i)
                    Case "/color", "/colour", "/col"
                        settings.Intent = WiaImageIntent.ColorIntent
                    Case "/grayscale", "/gray"
                        settings.Intent = WiaImageIntent.GrayscaleIntent
                    Case "/text", "/bw"
                        settings.Intent = WiaImageIntent.TextIntent
                    Case "/preview", "/p"
                        settings.Preview = True
                End Select

                If sArgs(i).StartsWith("/StiDevice:") Then
                    _deviceID = sArgs(i).Substring(sArgs(i).IndexOf(":") + 1)
                End If
                If sArgs(i).StartsWith("/StiEvent:") Then
                    'TODO: Implement
                End If
            Next

            If _deviceID = "" Then
                _deviceID = changescanner(My.Settings.DeviceID)
            Else
                _deviceID = changescanner(_deviceID)
            End If

            _device = manager.DeviceInfos.Item(_deviceID).Connect()
            Console.WriteLine("DeviceID = {0}", _deviceID)
            _wscanner = _device.Items(1)

            'STEP 3 Action parameters
            For i = 0 To sArgs.GetUpperBound(0)
                Select Case sArgs(i).ToLower()
                    Case "/?"
                        Console.SetOut(tmp)
                        Console.Write(LocRM.GetString("Console_Help"))
                    Case "/wiareg", "/wr"
                        RegisterWiaautdll(True)
                        GoTo exit_app
                    Case "/adf"
                        CopyADF(settings)
                    Case "/copy", "/c"
                        Copy(settings)
                        GoTo exit_app
                    Case "/file", "/tofile", "/Scantofile", "/f"
                        Try
                            SaveToFile(settings)
                        Catch ex As ArgumentException
                            MsgBox(ex.Message, vbExclamation, "iCopy")
                        End Try
                        GoTo exit_app
                    Case "/copymultiplepages", "/multiplepages"
                        CopyMultiplePages(settings)
                        GoTo exit_app
                    Case "/register", "/reg"
                        Try
                            manager.RegisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath + ",0", WIA.EventID.wiaEventScanImage)
                        Catch ex As UnauthorizedAccessException
                            MsgBox("iCopy must be executed with administrative privileges in order to complete the operation.", vbInformation, "iCopy")
                        End Try
                    Case "/unregister", "/unreg"
                        Try
                            manager.UnregisterPersistentEvent(Application.ExecutablePath + " /StiDevice:%1 /StiEvent:%2 /copy", "iCopy", "Directly print using iCopy", Application.ExecutablePath & ",0", WIA.EventID.wiaEventScanImage)
                        Catch ex As UnauthorizedAccessException
                            MsgBox("iCopy must be executed with administrative privileges in order to complete the operation.", vbInformation, "iCopy")
                        Catch ex As ArgumentException 'Thrown if the event is not found. Either wrong sintax or has already been removed
                            MsgBox("Couldn't find the correct entry in the registry. Maybe it has already been removed?")
                        End Try
                End Select
            Next
        End If
#If Not Debug Then
        Catch ex As Exception
            If ex.Message <> "Exit" Then HandleException(ex) 'Overrides .NET message box to include error reporting
        End Try
#End If

exit_app:
        If Not tmp Is Nothing Then
            Console.SetOut(tmp)
            sw.Close()
        End If
        Application.Exit()
    End Sub

    Private Shared Sub HandleException(ByVal ex As Exception)
        If ex.Message = "Exit" Then
            Exit Sub
        End If
        If TypeOf ex Is IO.FileNotFoundException Then
            RegisterWiaautdll(False)
        ElseIf TypeOf ex Is Runtime.InteropServices.COMException Then
            Dim cex As Runtime.InteropServices.COMException = TryCast(ex, Runtime.InteropServices.COMException)
            If cex.ErrorCode = WIA_ERRORS.WIA_ERROR_NOT_REGISTERED Then 'WIA COM error
                RegisterWiaautdll(False)
            ElseIf cex.ErrorCode = WIA_ERRORS.WIA_ERROR_UNKNOWN_ERROR Or cex.ErrorCode = WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED Then
                MsgBox("There is a problem with your scanner connection. Please try to reconnect your scanner and restart iCopy. If this doesn't solve the problem, please report it on iCopy website", MsgBoxStyle.Critical, "iCopy")
            End If
        Else
            'If the exception is unhandled, prepare error report and send info
            'If Not System.Diagnostics.Debugger.IsAttached Then SendInfo() 'Send info on iCopy
            ErrorReport(ex) 'Prepare error report
        End If
    End Sub

    Private Shared Sub ErrorReport(ByVal ex As Exception)

        Dim sendReport As MsgBoxResult = MsgBox(String.Format(appControl.GetLocalizedString("Msg_SendErrorReport"), ex.ToString()), MsgBoxStyle.Critical + MsgBoxStyle.OkCancel, "iCopy")
        If sendReport = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        'Prepares error report XML
        Dim doc As New Xml.XmlDocument()
        doc.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?> <errorReport> </errorReport>")
        Dim root As Xml.XmlNode = doc.DocumentElement
        'Date & Time
        root.Attributes.Append(doc.CreateAttribute("Date")).Value = Date.Now

        'System information
        Dim sysInfo As Xml.XmlNode = root.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "SystemInfo", ""))

        'iCopy version
        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "iCopy", "")).InnerText = Application.ProductVersion & vbCrLf

        'Windows version
        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Windows", "")).InnerText = System.Environment.OSVersion.VersionString

        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "NET", "")).InnerText = System.Environment.Version.ToString()

        Dim exception As Xml.XmlNode = root.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Exception", ""))
        exception.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Type", "")).InnerText = ex.GetType().ToString()
        exception.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Message", "")).InnerText = ex.Message
        exception.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Source", "")).InnerText = ex.Source
        exception.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "StackTrace", "")).InnerText = _
                ex.StackTrace.Replace("D:\Matteo\Documenti\Visual Studio Codename Orcas\Projects\iCopy\", "*\")
        exception.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Method", "")).InnerText = ex.TargetSite.Name

        Try
            appControl.Scanner.WritePropertiesLogXML(doc)
        Catch e As Exception
            'Do nothing, the scanner wasn't initialized
        End Try

        Dim sw As IO.StringWriter = New IO.StringWriter()
        Dim tx As Xml.XmlTextWriter = New Xml.XmlTextWriter(sw)
        tx.Formatting = Xml.Formatting.Indented
        doc.WriteTo(tx)
        Clipboard.SetText(sw.ToString())
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim sVersion As String = String.Format("{0}.{1}{2}", version.Major, version.Minor, version.Build)
        Process.Start(String.Format("", ex.GetType().ToString(), ex.TargetSite.Name, sVersion, "pincopallino", "Please paste here")) 'TODO: Change with Sourceforge BugTracker
    End Sub

    Shared Function GetLocalizedString(ByVal Label As String) As String
        Dim LocalizedString As String
        LocalizedString = LocRM.GetString(Label)
        Return LocalizedString
    End Function

    Private Shared Sub Diagnosis()
        'Prepares error report XML
        Dim doc As New Xml.XmlDocument()
        doc.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?> <errorReport> </errorReport>")
        Dim root As Xml.XmlNode = doc.DocumentElement
        'Date & Time
        root.Attributes.Append(doc.CreateAttribute("Date")).Value = Date.Now

        'System information
        Dim sysInfo As Xml.XmlNode = root.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "SystemInfo", ""))

        'iCopy version
        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "iCopy", "")).InnerText = Application.ProductVersion & vbCrLf

        'Windows version
        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "Windows", "")).InnerText = System.Environment.OSVersion.VersionString

        sysInfo.AppendChild(doc.CreateNode(Xml.XmlNodeType.Element, "NET", "")).InnerText = System.Environment.Version.ToString()

        Try
            appControl.Scanner.WritePropertiesLogXML(doc)
        Catch e As Exception
            Dim nodeDevProp As Xml.XmlNode
            nodeDevProp = doc.CreateNode(Xml.XmlNodeType.Element, "DeviceProperties", "")
            root.AppendChild(nodeDevProp)
            nodeDevProp.InnerText = "Impossible to read scanner properties: " & e.Message
        End Try
        doc.Save(Console.Out)

        'Dim fileDial As New SaveFileDialog()
        'fileDial.AddExtension = True
        'fileDial.Filter = "File XML (*.xml)|*.xml"
        'fileDial.FileName = "iCopyDiagnosis.xml"
        'If fileDial.ShowDialog() = DialogResult.OK Then
        '    doc.Save(fileDial.FileName)
        'End If
    End Sub

    Private Shared Sub RegisterWiaautdll(ByVal suppressMessage As Boolean)

        MsgBox("iCopy will now try to register WIA Automation layer.")

        'Check if iCopy is run as administrator
        Dim isElevated As Boolean
        Dim identity As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent()
        Dim principal As New System.Security.Principal.WindowsPrincipal(identity)
        isElevated = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)

        If isElevated Then
            'Copy wiaaut.dll to system32
            Dim proceed As Boolean = True
            If Not suppressMessage Then
                If MsgBox(LocRM.GetString("Msg_WIAUnregistered"), MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "iCopy") = MsgBoxResult.Cancel Then proceed = False
            End If
            If proceed Then
                Try
                    System.IO.File.Copy("wiaaut.dll", "C:\WINDOWS\system32\wiaaut.dll", False)
                Catch authEx As UnauthorizedAccessException 'iCopy has not administrator privileges
                    MsgBox(LocRM.GetString("Msg_UnauthorizedAccess"), MsgBoxStyle.Exclamation, "iCopy")
                    Exit Sub
                Catch fileEx As IO.FileNotFoundException 'File is missing from iCopy directory
                    MsgBox(LocRM.GetString("Msg_WiaautMissing"), MsgBoxStyle.Exclamation, "iCopy")
                    Exit Sub
                Catch ex As IO.IOException
                    'The file is already in system32
                End Try

                'Start regsvr32 to register the dll
                Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                psi.FileName = "regsvr32"
                psi.Arguments = "C:\WINDOWS\system32\wiaaut.dll /s"
                Process.Start(psi)

                MsgBox(LocRM.GetString("Msg_WIARegistrationSuccessful"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "iCopy")
            End If
        Else
            If Environment.OSVersion.Version.Major >= 6 Then
                Dim WIADialog As New WIARegisterDialog()
                Dim msg As Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult = WIADialog.Show(LocRM.GetString("Msg_WIAUnregistered"), LocRM.GetString("Msg_WIAUnregisteredInstruction"), "iCopy", LocRM.GetString("Msg_WIAUnregisteredCancel"))
                If msg = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Ok Then
                    Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.Arguments = "-r"
                    psi.Verb = "runas"
                    Process.Start(psi)
                    Exit Sub
                ElseIf msg = Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_WIAUnregistered"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "iCopy")
                If msg = MsgBoxResult.Yes Then
                    Dim psi As System.Diagnostics.ProcessStartInfo = New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.Arguments = "-r"
                    psi.Verb = "runas"
                    Process.Start(psi)
                    Exit Sub
                ElseIf msg = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Shared Sub GetAvailableLanguages()

        HasThreadFinished = False
        ReDim _availableCultures(-1)
        'Need to find a faster way
        Dim resSet As Resources.ResourceSet
        For Each cult As CultureInfo In CultureInfo.GetCultures(CultureTypes.FrameworkCultures)
            If Not cult.IsNeutralCulture Then 'Excludes neutral languages
                resSet = LocRM.GetResourceSet(cult, True, False) 'Verify if resources for that culture are present
                If Not resSet Is Nothing Then
                    If Not cult.LCID = 127 Then 'Excludes standard language
                        ReDim Preserve _availableCultures(_availableCultures.GetUpperBound(0) + 1)
                        _availableCultures(_availableCultures.GetUpperBound(0)) = cult
                    End If
                End If
            End If
        Next
        HasThreadFinished = True
    End Sub

    '''Cambia lo scanner con un altro
    Shared Function changescanner(Optional ByVal deviceID As String = "") As String
        Try
            If deviceID = "" Then
                'Shows WIA scanner selection dialog
                Dim dialog As New CommonDialog
                _scanner = New Scanner(dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, True, True).DeviceID)
                Diagnosis()
                Return _scanner.DeviceId
            Else
                _scanner = New Scanner(deviceID)
                Diagnosis()
                Return _scanner.DeviceId
            End If

            '**************Exception handling*************
        Catch ex As ArgumentException
            Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_NoScannerConnected"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "iCopy")
            If msg = MsgBoxResult.Retry Then
                Return changescanner(deviceID)
            Else
                Throw New ArgumentException("Exit")
            End If

        Catch ex As Runtime.InteropServices.COMException

            Select Case ex.ErrorCode
                Case WIA_ERRORS.WIA_ERROR_NO_SCANNER_SELECTED 'No scanner is selected
                    Return Nothing

                Case WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED 'No scanner is connected
                    Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_NoScannerConnected"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "iCopy")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ArgumentException("Exit")
                    End If

                Case WIA_ERRORS.WIA_ERROR_CONNECTION_ERROR  'Can't establish connection with the scanner
                    Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_ConnectionError"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Exclamation, "iCopy")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ArgumentException("Exit")
                    End If

                Case WIA_ERRORS.WIA_ERROR_OFFLINE
                    Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_ScannerOffline"), MsgBoxStyle.RetryCancel + MsgBoxStyle.Information, "iCopy")
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ArgumentException("Exit")
                    End If

                Case WIA_ERRORS.WIA_ERROR_EXCEPTION_IN_DRIVER
                    MsgBox(My.Resources.WinFormStrings.Msg_DriverException, MsgBoxStyle.Critical, "iCopy")
                    Throw New ArgumentException("Exit")

                Case WIA_ERRORS.WIA_ERROR_BUSY 'Scanner in use
                    Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_ScannerInUse"), MsgBoxStyle.Exclamation + MsgBoxStyle.RetryCancel)
                    If msg = MsgBoxResult.Retry Then
                        Return changescanner(deviceID)
                    Else
                        Throw New ArgumentException("Exit")
                    End If

                Case Else
                    Throw
            End Select
        End Try

        Return Nothing
    End Function

    Shared Sub CreateScanner(Optional ByVal deviceID As String = Nothing)
retry:
        If deviceID = Nothing Or deviceID = "" Then
            Dim newscannerID As String = changescanner()
            If newscannerID Is Nothing Then
                Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_ChooseAScanner"), MsgBoxStyle.YesNo + MsgBoxStyle.Information, "iCopy")
                If msg = MsgBoxResult.Yes Then
                    GoTo retry
                Else
                    Throw New ArgumentException("Exit")
                End If
            End If

        Else
            changescanner(deviceID)
        End If
    End Sub

    Shared Sub SaveToFile(ByVal options As ScanSettings)
        If options.Path = "" Then
            Dim dialog As New SaveFileDialog()

            dialog.AddExtension = True
            dialog.DefaultExt = "jpg"
            dialog.Filter = "JPEG image|*.jpg|Windows Bitmap|*.bmp|Compuserve GIF|*.gif|Portable Network Graphics (PNG)|*.png"

            If Not dialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                options.Path = dialog.FileName
            Else : Exit Sub
            End If
        End If

        'Check if the provided path is valid (AUTHORIZATION, SYNTAX, ecc)
        Try
            Dim a As FileStream = IO.File.Create(options.Path)
            a.Close()
        Catch ex As Exception
            Throw New ArgumentException("Invalid file path")
        End Try
        IO.File.Delete(options.Path)

        Dim format As Imaging.ImageFormat
        'Determines the extension of the file
        Dim ext As String = Right(options.Path, 3)

        Select Case ext
            Case "jpg"
                format = ImageFormat.Jpeg
            Case "bmp"
                format = ImageFormat.Bmp
            Case "gif"
                format = ImageFormat.Gif
            Case "png"
                format = ImageFormat.Png
            Case Else
                Throw New ArgumentException("File extension isn't a valid extension")
        End Select

        changescanner(_scanner.DeviceId)
        Dim img As Image
        'Calls scan routine
        Try
            img = _scanner.ScanImg(options)

            img.Save(options.Path, format)

            img.Dispose()

        Catch ex As System.Runtime.InteropServices.COMException
            If ex.ErrorCode = -2145320860 Then       'If acquisition is cancelled

            ElseIf ex.ErrorCode = Convert.ToInt32("0x80004005", 16) Then
                MsgBox("An error occured while processing the acquired image. Please try again with a lower resolution." & vbCrLf & "If the problem persists please report it (http://icopy.sourceforge.net/reportabug.html).", MsgBoxStyle.Critical, "iCopy")
            Else
                Throw
            End If
        End Try

    End Sub

    Private Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo

        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()

        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing

    End Function

    Shared Function GetAvailableResolutions() As List(Of Integer)
        Return _scanner.AvailableResolutions
    End Function

    Shared Sub CopyMultiplePages(ByVal options As ScanSettings)

        'Sets acquisition properties
        Dim morePages As DialogResult = DialogResult.Yes

        Dim dlg As New dlgScanMorePages
        If CommandLine Then
            dlg.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - dlg.Width, Screen.PrimaryScreen.WorkingArea.Height / 2 - dlg.Height)
        Else
            dlg.Location = New Point(MainForm.Left + (MainForm.Width - dlg.Width) / 2, MainForm.Top + (MainForm.Height - dlg.Height) / 2)

        End If
retry:
        Do Until morePages = DialogResult.No Or morePages = DialogResult.Cancel
            changescanner(_scanner.DeviceId)
            'Calls scan routine
            Try
                'Add the image to the print buffer
                _printer.AddImage(_scanner.Scan(options), options.Scaling)

                morePages = dlg.ShowDialog()

            Catch ex As System.Runtime.InteropServices.COMException
                If ex.ErrorCode = -2145320860 Then       'If acquisition is cancelled
                    Exit Do
                ElseIf ex.ErrorCode = Convert.ToInt32("0x80004005", 16) Then
                    MsgBox("An error occured while processing the acquired image. Please try again with a lower resolution." & vbCrLf & "If the problem persists please report it (http://icopy.sourceforge.net/reportabug.html).", MsgBoxStyle.Critical, "iCopy")
                Else
                    Throw
                End If
            End Try

        Loop

        If Not morePages = DialogResult.Cancel Then
            'Prints images
            Try
                _printer.Print(options.Copies)
            Catch ex As ArgumentException 'If no images were sent to the printer

            End Try

        Else 'If the process is canceled by closing the dialog
            Dim res As MsgBoxResult = MsgBox("Are you sure you want to cancel the operation? Click Yes if you are sure to proceed.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "iCopy")
            If res = MsgBoxResult.Yes Then
                _printer.ClearBuffer()
            Else
                GoTo retry
            End If
        End If
    End Sub

    Shared Sub Copy(ByVal options As ScanSettings)
        'Sets acquisition properties

        changescanner(_scanner.DeviceId)
        'Calls scan routine
        Try
            'Add the image to the printer buffer
            _printer.AddImage(_scanner.Scan(options), options.Scaling)

        Catch ex As System.Runtime.InteropServices.COMException
            If ex.ErrorCode = -2145320860 Then       'If acquisition is cancelled
                Exit Sub
            ElseIf ex.ErrorCode = Convert.ToInt32("0x80004005", 16) Then
                MsgBox("An error occured while processing the acquired image. Please try again with a lower resolution." & vbCrLf & "If the problem persists please report it (http://icopy.sourceforge.net/reportabug.html).", MsgBoxStyle.Critical, "iCopy")
                Exit Sub
            Else
                Throw
            End If
        End Try

        'Prints images
        _printer.Print(options.Copies)

    End Sub

    Shared Sub CopyADF(ByVal options As ScanSettings)
        'Sets acquisition properties

        changescanner(_scanner.DeviceId)
        'Calls scan routine
        Try
            'Add the image to the printer buffer
            Dim imageList As List(Of Image) = _scanner.ScanADF(options)
            If imageList.Count > 0 Then _printer.AddImages(imageList, options.Scaling)

        Catch ex As System.Runtime.InteropServices.COMException
            If ex.ErrorCode = -2145320860 Then       'If acquisition is cancelled
                Exit Sub
            ElseIf ex.ErrorCode = Convert.ToInt32("0x80004005", 16) Then
                MsgBox("An error occured while processing the acquired image. Please try again with a lower resolution." & vbCrLf & "If the problem persists please report it (http://icopy.sourceforge.net/reportabug.html).", MsgBoxStyle.Critical, "iCopy")
                Exit Sub
            Else
                Throw
            End If
        End Try

        'Prints images
        _printer.Print(options.Copies)

    End Sub

    Shared Sub scannerDisconnected(ByVal sender As Object, ByVal e As EventArgs) Handles _scanner.ScannerDisconnected

        MainForm.Enabled = False
        Dim msg As MsgBoxResult = MsgBox(LocRM.GetString("Msg_ConnectionLost"), MsgBoxStyle.Critical + MsgBoxStyle.RetryCancel, "iCopy")
        If msg = MsgBoxResult.Retry Then
            Dim newscanner As String = changescanner(My.Settings.DeviceID)
            If Not newscanner = Nothing Then
                MainForm.Enabled = True
                MainForm.Show()
            End If
        Else
            Application.Exit()
        End If
    End Sub

    Shared ReadOnly Property ScannerDescription()
        Get
            Return _scanner.Description
        End Get
    End Property

    Shared ReadOnly Property Scanner() As Scanner
        Get
            Return _scanner
        End Get
    End Property

    Shared ReadOnly Property Printer() As Printer
        Get
            Return _printer
        End Get
    End Property

    Shared ReadOnly Property AvailableLanguages() As Globalization.CultureInfo()
        Get
            If HasThreadFinished = False Then
                Throw New Threading.ThreadStateException("Thread has not yet finished")
            End If
            Return _availableCultures
        End Get
    End Property

End Class

Public Class ImageBuffer
    Inherits CollectionBase
    Dim _counter As Short

    Property Counter() As Short 'Used to check the position in the buffer
        Get
            Counter = _counter
        End Get
        Set(ByVal value As Short)
            If value >= 0 And value < List.Count Then
                _counter = value
            Else
                Throw New ArgumentOutOfRangeException("value", "Value must be positive and fall within the upper index")
            End If
        End Set
    End Property

    Overloads Sub Clear()
        _counter = 0
        List.Clear()
    End Sub

    Default Public Property Item(ByVal index As Integer) As Image
        Get
            Return CType(List(index), Image)
        End Get
        Set(ByVal value As Image)
            List(index) = value
        End Set
    End Property

    Public Function Add(ByVal value As Image) As Integer
        Return List.Add(value)
    End Function 'Add

    Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
        ' Insert additional code to be run only when inserting values.
    End Sub 'OnInsert

    Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
        ' Insert additional code to be run only when removing values.
    End Sub 'OnRemove

    Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
        ' Insert additional code to be run only when setting values.
    End Sub 'OnSet

    Protected Overrides Sub OnValidate(ByVal value As Object)
        If Not GetType(Image).IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type image.", "value")
        End If
    End Sub

End Class
