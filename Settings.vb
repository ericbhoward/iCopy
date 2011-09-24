'iCopy - Simple Photocopier
'Copyright (C) 2007-2010 Matteo Rossi

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

Imports System.Configuration

Namespace My
    
    ' This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.

    Partial Friend NotInheritable Class MySettings

        Private Sub MySettings_SettingChanging(ByVal sender As Object, ByVal e As System.Configuration.SettingChangingEventArgs) Handles Me.SettingChanging
            If e.SettingName = "DefaultIntent" Then
                If Not (e.NewValue = 1 Or e.NewValue = 2 Or e.NewValue = 4) Then
                    e.Cancel = True
                End If
            End If

        End Sub

    End Class
End Namespace
