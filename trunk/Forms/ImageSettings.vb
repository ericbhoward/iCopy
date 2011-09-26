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

Class frmImageSettings

    Dim locRootStr As String

    'Prevent the form from unloading
    Private Sub frmImageSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            e.Cancel = True
            appControl.MainForm.btnImageSettings.Text = appControl.GetLocalizedString("mainFrm_btnImageSettings")
            Me.Hide()
        End If
    End Sub

    Private Sub frmImageSettings_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmImageSettings_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Or (e.Control And e.KeyCode = Keys.I) Then
            Me.Close()
        End If
    End Sub

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        locRootStr = Me.Name & "_"
        'Applies localized strings to the controls
        For Each control As System.Windows.Forms.Control In Me.Controls
            control.Text = appControl.GetLocalizedString(locRootStr & control.Name)
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(locRootStr & control.Name & "ToolTip"))
        Next

        'Quality
        For Each res As Short In appControl.GetAvailableResolutions()
            cboResolution.Items.Add(res)
        Next

        If My.Settings.Resolution <> 0 Then
            cboResolution.Text = My.Settings.Resolution
        Else
            cboResolution.Text = "100"
            My.Settings.Resolution = 100
        End If

        'Loads settings
        tbBrightness.Value = My.Settings.Brightness
        tbContrast.Value = My.Settings.Contrast
        txtBrightness.Text = My.Settings.Brightness
        txtContrast.Text = My.Settings.Contrast
        tbScaling.Value = 100
        txtScaling.Text = "100"
        tbCompression.Value = My.Settings.Compression
        lblCompression.Text = tbCompression.Value
    End Sub

    Private Sub valid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBrightness.KeyPress, txtContrast.KeyPress, txtScaling.KeyPress

        'Permits only numerical input to the textbox
        'Apllied to both txtBrightness and txtContrast
        Dim txt As Object = sender
        If IsNumeric(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            e.Handled = False   'permits
        ElseIf e.KeyChar = "-" And (txt.Text.Length = 0 Or txt.SelectionStart = 0) And TypeOf sender Is TextBox Then 'Permits the pressing of "-" only if it is the first char
            e.Handled = False   'permits
        Else
            e.Handled = True    'blocks keypress
        End If
    End Sub

    Shared Sub CheckValue(ByVal textBox As TextBox, ByVal tb As TrackBar, ByVal value As String)
        'Checks that values inserted in brightness and contrast textboxes are correct
        Try
            value = CShort(value) 'Converts text into a short
            If value <= tb.Maximum And value >= tb.Minimum Then
                tb.Value = value
            Else
                MsgBox(String.Format(appControl.GetLocalizedString("Msg_InsertNumber"), tb.Minimum, tb.Maximum), MsgBoxStyle.Information, "iCopy")
                textBox.Text = tb.Value
                textBox.Focus()
            End If
        Catch ex As InvalidCastException
            MsgBox(String.Format(appControl.GetLocalizedString("Msg_InsertNumber"), tb.Minimum, tb.Maximum), MsgBoxStyle.Information, "iCopy")
            textBox.Text = tb.Value
            textBox.Focus()
        End Try

    End Sub

    Private Sub txtContrast_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtContrast.LostFocus
        CheckValue(txtContrast, tbContrast, txtContrast.Text)
        tbContrast.Value = CInt(txtContrast.Text)
    End Sub

    Private Sub tbContrast_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbContrast.ValueChanged
        txtContrast.Text = tbContrast.Value
        My.Settings.Contrast = tbContrast.Value
    End Sub

    Private Sub txtBrightness_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrightness.LostFocus
        CheckValue(txtBrightness, tbBrightness, txtBrightness.Text)
        tbBrightness.Value = CInt(txtBrightness.Text)
    End Sub

    Private Sub tbBrightness_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbBrightness.ValueChanged
        txtBrightness.Text = tbBrightness.Value
        My.Settings.Brightness = tbBrightness.Value
    End Sub

    Private Sub tbenlargement_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbScaling.Scroll
        txtScaling.Text = tbScaling.Value
    End Sub

    Private Sub txtenlargement_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtScaling.LostFocus
        CheckValue(txtScaling, tbScaling, txtScaling.Text)
        tbBrightness.Value = CInt(txtBrightness.Text)
    End Sub

    Private Sub cboResolution_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboResolution.SelectedIndexChanged
        My.Settings.Resolution = cboResolution.Text
    End Sub

    Private Sub tbCompression_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCompression.ValueChanged
        lblCompression.Text = tbCompression.Value
        My.Settings.Compression = tbCompression.Value
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        tbBrightness.Value = 0 : tbContrast.Value = 0
        tbCompression.Value = 100
        cboResolution.Text = 100
        tbScaling.Value = 100
    End Sub
End Class