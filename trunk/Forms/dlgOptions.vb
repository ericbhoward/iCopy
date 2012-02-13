'iCopy - Simple Photocopier
'Copyright (C) 2007-2012 Matteo Rossi

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

Imports System.Windows.Forms
Imports System.Globalization
Imports WIA

Public Class dlgOptions
    Dim localeRootStr As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        SaveSettings()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Sub SaveSettings()
        If Not (My.Settings.Culture.LCID = appControl.AvailableLanguages(cboLanguage.SelectedIndex).LCID) Then
            My.Settings.Culture = appControl.AvailableLanguages(cboLanguage.SelectedIndex)
            My.Settings.CustomCulture = True
            MsgBoxWrap(appControl.GetLocalizedString("Msg_Language"), MsgBoxStyle.Information, "iCopy")
        End If

        If cboBitDepth.Text = "Auto" Then
            My.Settings.LastScanSettings.BitDepth = 0
        Else
            My.Settings.LastScanSettings.BitDepth = cboBitDepth.Text
        End If
    End Sub

    Sub LoadSettings()
        localeRootStr = Me.Name & "_"
        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls
            Dim text As String = appControl.GetLocalizedString(localeRootStr & control.Name)
            If text <> "" Then control.Text = text
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localeRootStr & control.Name & "ToolTip"))
        Next

        'Populates language combobox
        cboLanguage.Items.Clear()
        Dim i As Integer = 0
        Dim LCID As Integer = Threading.Thread.CurrentThread.CurrentUICulture.LCID

        Try
retry:
            If appControl.AvailableLanguages.GetUpperBound(0) = 0 Then appControl.GetAvailableLanguages()
            Dim availableCultures As CultureInfo() = appControl.AvailableLanguages
            For Each cult As CultureInfo In availableCultures
                cboLanguage.Items.Add(cult.EnglishName)
                If cult.LCID = LCID Then cboLanguage.SelectedIndex = i
                i += 1
            Next
        Catch ex As Threading.ThreadStateException
            Threading.Thread.Sleep(1000)
            GoTo retry
        End Try

        'Load bit depth settings
        If My.Settings.LastScanSettings.BitDepth = 0 Then
            cboBitDepth.SelectedIndex = 0
        Else
            cboBitDepth.Text = My.Settings.LastScanSettings.BitDepth
        End If

        'Scan button association
        lblScanner.Text = "Scanner: " + appControl.ScannerDescription
        Dim deviceID As String = appControl.Scanner.DeviceId

        Dim events As WIA.DeviceEvents = appControl.GetScannerEvents()
        Dim evWrappers As New List(Of WIAEventWrapper)
        Dim availActions As New List(Of Action)
        availActions.Add(New Action("Show Interface", ""))
        availActions.Add(New Action("Copy", "/c"))
        availActions.Add(New Action("Scan to File", "/f"))

        ComboBox1.DataSource = availActions
        ComboBox1.ValueMember = "Arguments"
        ComboBox1.DisplayMember = "Description"

        For Each ev As DeviceEvent In events
            If Not ev.Name.Contains("Device") Then
                Dim evWr As New WIAEventWrapper(ev)
                evWrappers.Add(evWr)
                ListBox1.Items.Add(evWr)
            End If
        Next

    End Sub

    Private Sub dlgOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub btnResetSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultSettings.Click
        My.Settings.Reset()
    End Sub

    Private Sub btnRegister_Click(sender As System.Object, e As System.EventArgs) Handles btnRegister.Click

        Dim manager As New DeviceManager()
        Dim ev = DirectCast(ListBox1.SelectedItem, WIAEventWrapper)
        Dim command As String = String.Format("{0} {1} {2}", Application.ExecutablePath, ComboBox1.SelectedValue.ToString(), "/StiDevice:%1")

        'Affects registry keys
        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\StillImage\Events\EVENT_ID
        'HKEY_LOCAL_MACHINE\SYSTEM\ControlSet002\Control\StillImage\Events\EVENT_ID
        'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\StillImage\Events\EVENT_ID
        manager.RegisterPersistentEvent(command, "iCopy", "Whatever", "", ev.EventID) ', appControl.Scanner.DeviceId)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim manager As New DeviceManager()
        Dim ev = DirectCast(ListBox1.SelectedItem, WIAEventWrapper)
        Dim command As String = String.Format("{0} {1}", Application.ExecutablePath, ComboBox1.SelectedValue.ToString())
        manager.UnregisterPersistentEvent(Command, "iCopy", "Whatever", "", ev.EventID) ', appControl.Scanner.DeviceId)
    End Sub
End Class

Class WIAEventWrapper
    Implements DeviceEvent

    Dim _ev As WIA.DeviceEvent
    Dim _action As Action

    Sub New(ev As WIA.DeviceEvent)
        _ev = ev
    End Sub

    Public Property currentAction() As Action
        Get
            Return _action
        End Get
        Set(ByVal value As Action)
            _action = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _ev.Name
    End Function

    Public ReadOnly Property Description As String Implements WIA.IDeviceEvent.Description
        Get
            Return _ev.Description
        End Get
    End Property

    Public ReadOnly Property EventID As String Implements WIA.IDeviceEvent.EventID
        Get
            Return _ev.EventID
        End Get
    End Property

    Public ReadOnly Property Name As String Implements WIA.IDeviceEvent.Name
        Get
            Return _ev.Name
        End Get
    End Property

    Public ReadOnly Property Type As WIA.WiaEventFlag Implements WIA.IDeviceEvent.Type
        Get
            Return _ev.Type
        End Get
    End Property
End Class

Class Action
    Public Sub New(Description As String, Arguments As String)
        _description = Description
        _arguments = Arguments
    End Sub

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _arguments As String
    Public Property Arguments() As String
        Get
            Return _arguments
        End Get
        Set(ByVal value As String)
            _arguments = value
        End Set
    End Property

End Class