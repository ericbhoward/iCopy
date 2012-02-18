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

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration

Imports System.Windows.Forms
Imports System.Collections.Specialized
Imports Microsoft.Win32
Imports System.Xml

Public Class PortableSettingsProvider
    Inherits SettingsProvider
    Dim settingsPath As String = ""
    Const SETTINGSROOT As String = "Settings" 'XML Root Node

    Public Overrides Sub Initialize(ByVal name As String, ByVal col As NameValueCollection)
        MyBase.Initialize(Me.ApplicationName, col)
    End Sub

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "PortableSettingsProvider"
        End Get
    End Property

    Public Overrides Property ApplicationName() As String
        Get
            If Application.ProductName.Trim.Length > 0 Then
                Return Application.ProductName
            Else
                Dim fi As New IO.FileInfo(Application.ExecutablePath)
                Return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length)
            End If
        End Get
        Set(ByVal value As String)
            'Do nothing
        End Set
    End Property

    Function HasWritePermission(ByVal folder As String) As Boolean
        Try
            IO.File.Create("iCopy.settings")
        Catch ex As UnauthorizedAccessException
            Return False
        End Try
        Dim oFp As Security.Permissions.FileIOPermission = New Security.Permissions.FileIOPermission(Security.Permissions.FileIOPermissionAccess.Write, folder)
        Return Security.SecurityManager.IsGranted(oFp)
    End Function

    Public Function GetUserAppDataPath() As String
        Dim path As String = Application.LocalUserAppDataPath

        path = path.Replace(Application.CompanyName + "\", "")
        path = path.Replace(Application.ProductVersion, "")
        Try
            IO.Directory.CreateDirectory(path)
        Catch ex As Exception

        End Try
        Return IO.Path.Combine(path, GetAppSettingsFileName)
    End Function

    Overridable Function GetAppSettingsPath() As String
        If settingsPath = "" Then
            Dim fi As New System.IO.FileInfo(Application.ExecutablePath)
            Return IO.Path.Combine(fi.DirectoryName, GetAppSettingsFileName)
        Else
            Return settingsPath
        End If
    End Function

    Overridable Function GetAppSettingsFileName() As String
        'Used to determine the filename to store the settings
        Return ApplicationName & ".settings"
    End Function

    Public Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal propvals As SettingsPropertyValueCollection)
        'Iterate through the settings to be stored
        'Only dirty settings are included in propvals, and only ones relevant to this provider
        For Each propval As SettingsPropertyValue In propvals
            SetValue(propval)
        Next

        Try
            SettingsXML.Save(GetAppSettingsPath)
        Catch ex As UnauthorizedAccessException
            SettingsXML.Save(GetUserAppDataPath)
        Catch e As Exception
            'Ignore other exceptions
        End Try
    End Sub

    Public Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal props As SettingsPropertyCollection) As SettingsPropertyValueCollection
        'Create new collection of values
        Dim values As SettingsPropertyValueCollection = New SettingsPropertyValueCollection()

        'Iterate through the settings to be retrieved
        For Each setting As SettingsProperty In props
            Dim value As SettingsPropertyValue = New SettingsPropertyValue(setting)
            value.IsDirty = False
            value.SerializedValue = GetValue(setting)
            values.Add(value)
        Next
        Return values
    End Function

    Private m_SettingsXML As Xml.XmlDocument

    Private ReadOnly Property SettingsXML() As Xml.XmlDocument
        Get
            'If we dont hold an xml document, try opening one.  
            'If it doesnt exist then create a new one ready.
            If m_SettingsXML Is Nothing Then
                m_SettingsXML = New Xml.XmlDocument

                Try
                    m_SettingsXML.Load(GetAppSettingsPath)
                Catch ex As Exception
                    'Check if the file is in the alternate place
                    Try
                        m_SettingsXML.Load(GetUserAppDataPath)
                    Catch e As Exception
                        'If both files are absent, create new document
                        Dim dec As XmlDeclaration = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
                        m_SettingsXML.AppendChild(dec)

                        Dim nodeRoot As XmlNode

                        nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "")
                        m_SettingsXML.AppendChild(nodeRoot)

                    End Try
                End Try
            End If

            Return m_SettingsXML
        End Get
    End Property

    Private Function GetValue(ByVal setting As SettingsProperty) As String
        Dim ret As String = ""
        Dim node As XmlNode
        Try
            If IsRoaming(setting) Then
                node = SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & setting.Name)
            Else
                node = SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name & "/" & setting.Name)
            End If

            If setting.SerializeAs = SettingsSerializeAs.Xml Then
                ret = node.InnerXml
            Else
                ret = node.InnerText
            End If

        Catch ex As Exception
            If Not setting.DefaultValue Is Nothing Then
                ret = setting.DefaultValue.ToString
            Else
                ret = ""
            End If
        End Try

        Return ret
    End Function

    Private Sub SetValue(ByVal propVal As SettingsPropertyValue)

        Dim MachineNode As Xml.XmlElement
        Dim SettingNode As Xml.XmlElement

        'Determine if the setting is roaming.
        'If roaming then the value is stored as an element under the root
        'Otherwise it is stored under a machine name node 
        Try
            If IsRoaming(propVal.Property) Then
                SettingNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & propVal.Name), XmlElement)
            Else
                SettingNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name & "/" & propVal.Name), XmlElement)
            End If
        Catch ex As Exception
            SettingNode = Nothing
        End Try

        'Check to see if the node exists, if so then set its new value
        If Not SettingNode Is Nothing Then
            If propVal.Property.SerializeAs = SettingsSerializeAs.Xml Then
                SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=""1.0"" encoding=""utf-16""?>", "")
            Else
                SettingNode.InnerText = propVal.SerializedValue.ToString()
            End If
        Else
            If IsRoaming(propVal.Property) Then
                'Store the value as an element of the Settings Root Node
                SettingNode = SettingsXML.CreateElement(propVal.Name)
                If propVal.Property.SerializeAs = SettingsSerializeAs.Xml Then
                    SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=""1.0"" encoding=""utf-16""?>", "")
                Else
                    SettingNode.InnerText = propVal.SerializedValue.ToString()
                End If
                SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(SettingNode)
            Else
                'Its machine specific, store as an element of the machine name node,
                'creating a new machine name node if one doesnt exist.
                Try
                    MachineNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name), XmlElement)
                Catch ex As Exception
                    MachineNode = SettingsXML.CreateElement(My.Computer.Name)
                    SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode)
                End Try

                If MachineNode Is Nothing Then
                    MachineNode = SettingsXML.CreateElement(My.Computer.Name)
                    SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode)
                End If

                SettingNode = SettingsXML.CreateElement(propVal.Name)
                If propVal.Property.SerializeAs = SettingsSerializeAs.Xml Then
                    SettingNode.InnerXml = propVal.SerializedValue.ToString().Replace("<?xml version=""1.0"" encoding=""utf-16""?>", "")
                Else
                    SettingNode.InnerText = propVal.SerializedValue.ToString()
                End If
                MachineNode.AppendChild(SettingNode)
            End If
        End If
    End Sub

    Private Function IsRoaming(ByVal prop As SettingsProperty) As Boolean
        'Determine if the setting is marked as Roaming
        For Each d As DictionaryEntry In prop.Attributes
            Dim a As Attribute = DirectCast(d.Value, Attribute)
            If TypeOf a Is System.Configuration.SettingsManageabilityAttribute Then
                Return True
            End If
        Next
        Return False
    End Function

End Class

