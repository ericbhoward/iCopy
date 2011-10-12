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

Imports WIA
Imports System.Drawing.Printing

Class mainFrm
    Public Shared frmImageSettings As frmImageSettings
    Public Shared AboutBox As AboutBox1
    Public Shared frmOptions As dlgOptions
    Dim splash As SplashScreen

    Dim intent As WiaImageIntent = My.Settings.LastScanSettings.Intent

    Private VersionCheckThread As New Threading.Thread(AddressOf VersionCheck)
    Dim weburl As String
    Dim LocalizedRootStr As String

    Sub VersionCheck()
        Dim diff As Long = DateDiff(DateInterval.WeekOfYear, My.Settings.LastVersionCheck, Today)
        If True Then '(My.Settings.LastVersionCheck = Nothing) Or diff > 2 Then
            Dim reader As Xml.XmlTextReader
            Dim newVersion As Version
            Try
                reader = New Xml.XmlTextReader(My.Resources.VersionCheckURL)
            Catch ex As Exception 'File is not available, or internet access missing. Just die without any output
                Exit Sub
            End Try
            reader.MoveToContent()
            If reader.NodeType = Xml.XmlNodeType.Element And reader.Name = Application.ProductName Then
                Dim elementName As String
                While reader.Read()
                    If reader.NodeType = Xml.XmlNodeType.Element Then
                        elementName = reader.Name
                    ElseIf reader.NodeType = Xml.XmlNodeType.Text And reader.HasValue Then
                        Select Case elementName
                            Case "version"
                                newVersion = New Version(reader.Value)
                            Case "url"
                                webURL = reader.Value
                        End Select
                    End If
                End While
            End If
            Dim curVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            If curVersion.CompareTo(newVersion) < 0 Then
                VersionStatusLabel.Visible = True
            End If
            My.Settings.LastVersionCheck = Today
        End If
    End Sub

    Sub LoadSettings()
        If My.Settings.CheckForUpdates Then
            VersionCheckThread.Start() 'Version check
        End If
        'Sets culture depending on Culture setting
        AboutBox = New AboutBox1()
        frmImageSettings = New frmImageSettings()
        frmOptions = New dlgOptions()

        'Loads form location if storelocation is true
        If My.Settings.StoreLocation Then
            Me.Location = My.Settings.Location
        Else
            Me.Location = New Point((Screen.GetBounds(Me).Width - Me.Width) / 2, (Screen.GetBounds(Me).Height - Me.Height) / 2)
        End If

        btnCopy.Image = My.Resources.iCopyBig
        Me.Icon = My.Resources.iCopyIco

        'Set frmImageSettings as child
        Me.AddOwnedForm(frmImageSettings)

        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls
            control.Text = appControl.GetLocalizedString(LocalizedRootStr & control.Name)
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(LocalizedRootStr & control.Name & "ToolTip"))
        Next

        'Applies localized strings to the menustrip
        For Each strip As ToolStripItem In ScanMenuStrip.Items
            strip.Text = appControl.GetLocalizedString(LocalizedRootStr & strip.Name)
        Next

        'Populates comboboxes
        For i As Integer = 0 To 2
            cboScanMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboScanModeItem" & i))
        Next

        For i As Integer = 0 To 1
            cboPrintMode.Items.Add(appControl.GetLocalizedString(LocalizedRootStr & "cboPrintModeItem" & i))
        Next

        'Sets default copies number
        nudNCopie.Controls(1).Text = "1"

        'Loads default printer
        appControl.Printer.Name = My.Settings.DefaultPrinter

        'Statusbar labels
        ScannerStatusLabel.Image = My.Resources.scanner
        ScannerStatusLabel.Text = appControl.ScannerDescription
        PrinterStatusLabel.Image = My.Resources.printer
        PrinterStatusLabel.Text = appControl.Printer.Name

        'Loads saved intent setting
        If My.Settings.LastScanSettings.Intent = 4 Or My.Settings.LastScanSettings.Intent = 0 Then
            cboScanMode.SelectedIndex = 2
        Else
            cboScanMode.SelectedIndex = My.Settings.LastScanSettings.Intent - 1
        End If

        'Populates paper sizes combo box
        cboPaperSize.DisplayMember = "PaperName" 'Links 
        For Each pkSize As PaperSize In appControl.Printer.PrinterSettings.PaperSizes
            cboPaperSize.Items.Add(pkSize)
        Next

        cboPaperSize.Text = My.Settings.PrinterSize 'Sets default paper size as stored in settings

    End Sub

    Private Sub mainFrm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        'Shortcuts
        If e.Control Then 'If CTRL is pressed
            Dim ea As New EventArgs()
            Select Case e.KeyCode
                Case Keys.C 'Copy
                    btnCopy_Click(btnCopy, ea)
                Case Keys.M 'Copy Multiple Pages
                    ScanMultiplePages_Click(ScanMultiplePages, ea)
                Case Keys.F 'Scan to File
                    ScanToFile_Click(ScanToFile, ea)
                Case Keys.I 'Image settings
                    btnImageSettings_Click(btnImageSettings, ea)
            End Select
        End If
    End Sub

    Private Sub StartSplash()
        splash = New SplashScreen()
        Application.Run(splash)
    End Sub

    Private Sub mainFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Settings.CustomCulture Then Threading.Thread.CurrentThread.CurrentUICulture = My.Settings.Culture

        LocalizedRootStr = Me.Name & "_"
        Dim SplashThread As New Threading.Thread(AddressOf StartSplash)
        SplashThread.Start()
        LoadSettings() 'Loads stored settings

        splash.Invoke(New EventHandler(AddressOf splash.KillMe))
        splash.Dispose()
        splash = Nothing

        Me.BringToFront()
        Me.Focus()
    End Sub

    Private Sub mainFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Stores form location
        If My.Settings.StoreLocation Then My.Settings.Location = Me.Location
    End Sub

    Private Sub mainFrm_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If frmImageSettings.Visible Then 'Moves the image settings form with main form
            Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
            If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
            Else
                frmImageSettings.Location = tempLocation
            End If
        End If
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Me.Enabled = False
        'Starts copy process
        Dim settings As ScanSettings = getScanSettings()
        appControl.Copy(settings)
        Me.Enabled = True
    End Sub

    Private Sub SelScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScannerStatusLabel.Click, btnSelScanner.Click
        Try 'Tries changing the scanner
            Dim newscannerID As String = appControl.changescanner()
            If newscannerID Is Nothing Then Exit Sub

            If My.Settings.DeviceID <> newscannerID Then
                My.Settings.DeviceID = newscannerID 'if a deviceId is returned, store it
            End If

            ScannerStatusLabel.Text = appControl.ScannerDescription
            If frmImageSettings.Visible Then btnImageSettings.PerformClick()
            frmImageSettings.Dispose()
            frmImageSettings = New frmImageSettings()
        Catch ex As NullReferenceException
            If ex.Message = "Exit" Then
                'Don't change the scanner
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub PrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSetup.Click, PrinterStatusLabel.Click
        appControl.Printer.showPreferences()
        My.Settings.DefaultPrinter = appControl.Printer.Name
        PrinterStatusLabel.Text = appControl.Printer.Name
    End Sub

    Private Sub cboScanMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboScanMode.SelectedIndexChanged
        'Changes scanning intent and print mode
        Select Case cboScanMode.SelectedIndex
            Case 0
                intent = WiaImageIntent.ColorIntent
                cboPrintMode.SelectedIndex = 0
            Case 1
                intent = WiaImageIntent.GrayscaleIntent
                cboPrintMode.SelectedIndex = 1
            Case 2
                intent = WiaImageIntent.TextIntent
                cboPrintMode.SelectedIndex = 1
        End Select
    End Sub

    Private Sub cboPrintMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPrintMode.SelectedIndexChanged
        'Changes print mode
        appControl.Printer.PageSettings.Color = Not CBool(cboPrintMode.SelectedIndex) 'If index is 0, returns true
        My.Settings.PrintColor = appControl.Printer.PageSettings.Color
    End Sub

    Private Sub btnScanModes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanModes.Click
        Dim pos As Point = btnScanModes.Location
        pos.Y += btnScanModes.Height
        ScanMenuStrip.Show(Me, pos)
    End Sub

    Private Sub btnImageSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageSettings.Click

        'Shows / hides image settings form in the correct position
        If frmImageSettings.Visible = False Then
            Dim tempLocation As New Point(Me.Location.X + Me.Size.Width, Me.Location.Y)
            If tempLocation.X + frmImageSettings.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
                frmImageSettings.Location = New Point(Me.Location.X - frmImageSettings.Width, Me.Location.Y)
            Else
                frmImageSettings.Location = tempLocation
            End If
            frmImageSettings.Show()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettingsHide")
        Else
            frmImageSettings.Hide()
            btnImageSettings.Text = appControl.GetLocalizedString(LocalizedRootStr & "btnImageSettings")
        End If
    End Sub

    Private Sub llblSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llblSettings.LinkClicked
        frmOptions.ShowDialog()
    End Sub

    Private Sub llblAbout_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llblAbout.LinkClicked
        AboutBox.Show()
    End Sub

    Private Function getScanSettings() As ScanSettings
        Dim opts As New ScanSettings
        Try
            opts.Resolution = Convert.ToInt32(frmImageSettings.cboResolution.Text, Globalization.CultureInfo.InvariantCulture)
        Catch ex As FormatException 'Fixes a bug
            If My.Settings.LastScanSettings.Resolution <> 0 Or (Not Nothing) Then opts.Resolution = My.Settings.LastScanSettings.Resolution
        End Try

        opts.Brightness = frmImageSettings.tbBrightness.Value
        opts.Contrast = frmImageSettings.tbContrast.Value
        opts.Intent = intent
        opts.Preview = chkPreview.Checked
        opts.Quality = frmImageSettings.tbCompression.Value
        opts.Copies = nudNCopie.Value
        opts.Scaling = frmImageSettings.tbScaling.Value
        opts.BitDepth = My.Settings.LastScanSettings.BitDepth

        My.Settings.LastScanSettings = opts
        Return opts
    End Function

    Private Sub ScanToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanToFile.Click
        Dim res As Short

        appControl.SaveToFile(getScanSettings())
    End Sub

    Private Sub ScanMultiplePages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanMultiplePages.Click
        Me.Enabled = False
        'Starts copy process
        Dim res As Short
        Try
            res = Convert.ToInt16(frmImageSettings.cboResolution.Text, Globalization.CultureInfo.InvariantCulture)
        Catch ex As FormatException
            If My.Settings.LastScanSettings.Resolution <> 0 Or (Not Nothing) Then res = My.Settings.LastScanSettings.Resolution
        End Try
        appControl.CopyMultiplePages(getScanSettings())
        Me.Enabled = True
    End Sub

    Private Sub cboPaperSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPaperSize.SelectedIndexChanged
        appControl.Printer.PageSettings.PaperSize = appControl.Printer.PrinterSettings.PaperSizes.Item(cboPaperSize.SelectedIndex)
        My.Settings.PrinterSize = cboPaperSize.Text 'Stores value in settings
    End Sub

    Private Sub VersionStatusLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionStatusLabel.Click
        Process.Start(weburl)
    End Sub
End Class
