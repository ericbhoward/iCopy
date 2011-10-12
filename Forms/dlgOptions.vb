'iCopy - Simple Photocopier
'Copyright (C) 2007-2009 Matteo Rossi

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
            MsgBox(appControl.GetLocalizedString("Msg_Language"), MsgBoxStyle.Information, "iCopy")
        End If

        My.Settings.StoreLocation = chkRememberWindowPos.Checked
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
            'If control.HasChildren = True Then
            '    For Each subcontrol As Windows.Forms.Control In control.Controls
            '        subcontrol.Text = LocRM.GetString(subcontrol.Name)
            '        ToolTip1.SetToolTip(subcontrol, LocRM.GetString(subcontrol.Name & "ToolTip"))
            '    Next
            'End If
            control.Text = appControl.GetLocalizedString(localeRootStr & control.Name)
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

        'Loads other settings
        chkRememberWindowPos.Checked = My.Settings.StoreLocation
    End Sub

    Private Sub dlgOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadSettings()
    End Sub

    Private Sub btnResetSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        My.Settings.Reset()
    End Sub
End Class
