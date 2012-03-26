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

Public Class dlgScanMorePages
    Dim localizedRootStr As String
    Private Sub DlgScanMorePages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        localizedRootStr = Me.Name & "_"
        Me.Text = appControl.GetLocalizedString(Me.Name)
        For Each control As System.Windows.Forms.Control In Me.Controls
            control.Text = appControl.GetLocalizedString(localizedRootStr & control.Name)
            ToolTip1.SetToolTip(control, appControl.GetLocalizedString(localizedRootStr & control.Name & "ToolTip"))
        Next
    End Sub
End Class
