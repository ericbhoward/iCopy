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

Imports Microsoft.WindowsAPICodePack.Dialogs

Class WIARegisterDialog
    Dim td As TaskDialog
    Dim OKClicked As Boolean

    Public Function Show(ByVal text As String, ByVal instructions As String, ByVal title As String, ByVal cancel As String) As TaskDialogResult

        td = New TaskDialog()
        td.Caption = title
        td.InstructionText = instructions
        td.Icon = TaskDialogStandardIcon.Shield
        td.Cancelable = True
        td.Text = text

        Dim CancelButton As TaskDialogButton = New TaskDialogButton("Cancel", cancel)
        Dim OKButton As TaskDialogButton = New TaskDialogButton("OK", "OK")
        OKButton.ShowElevationIcon = True
        OKButton.Default = True

        td.Controls.Add(OKButton)
        td.Controls.Add(CancelButton)

        AddHandler CancelButton.Click, AddressOf Cancel_Click
        AddHandler OKButton.Click, AddressOf OK_Click
        Try
            td.Show()
        Catch ex As NotSupportedException
            Dim msg As MsgBoxResult = MsgBoxWrap(text, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, title)
            If msg = MsgBoxResult.Ok Then Return TaskDialogResult.Ok Else Return TaskDialogResult.Cancel
        End Try

        If OKClicked Then Return TaskDialogResult.Ok Else Return TaskDialogResult.Cancel
    End Function

    Sub OK_Click()
        td.Close(Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Ok)
        OKClicked = True
    End Sub
    Sub Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        td.Close(Microsoft.WindowsAPICodePack.Dialogs.TaskDialogResult.Cancel)
        OKClicked = False
    End Sub

End Class

