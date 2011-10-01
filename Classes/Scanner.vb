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
Imports System.Xml

Public Class Scanner

    Dim WithEvents manager As New DeviceManager
    Dim _AvailableResolutions As List(Of Integer)
    Dim _description As String = ""
    Dim _deviceID As String = ""
    Public Event ScannerDisconnected As EventHandler

    Sub New(ByVal deviceID As String)
        If deviceID Is Nothing Then Throw New ArgumentNullException("deviceID", "No deviceID specified")
        If manager.DeviceInfos.Count = 0 Then Throw New ArgumentException("No WIA device connected")

        Dim _device As Device
        Dim _scanner As WIA.Item

        Try
            _device = manager.DeviceInfos.Item(deviceID).Connect
            _deviceID = deviceID
            _scanner = _device.Items(1)
            Console.WriteLine("Connection established. Available resolutions")
            _description = _device.Properties.Item("Description").Value
            _AvailableResolutions = GetAvailableResolutions(_scanner)
            For Each res As Integer In _AvailableResolutions
                Console.WriteLine(res)
            Next
        Catch ex As Exception
            Throw
        End Try

    End Sub

#Region "Properties"

    Private Function GetBrightness(ByRef _scanner As WIA.Item) As Integer
        Dim prop_name As String = "Brightness"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center
            Return CInt(Math.Round((temp.Value - center) / delta * 100, 0))
        Else
            Return temp.Value
        End If
    End Function

    Private Sub SetBrightess(ByVal value As Integer, ByRef _scanner As WIA.Item)
        Dim prop_name As String = "Brightness"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center

            If value <= 100 And value >= -100 Then
                Dim tmpVal As Integer = CInt(Math.Round(value / 100 * delta + center, 0))
                Try
                    temp.Value = tmpVal
                Catch ex As Exception
                    Throw New ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString())
                End Try
            Else
                Throw New ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.")
            End If
        Else
            Try
                temp.Value = value
            Catch ex As Exception
                MsgBox("There was an exception while setting the property " + prop_name + " to " + value.ToString() + ". Please report this information to iCopy bug tracker on Sourceforge:" + vbCrLf + _
                       "property type: " + temp.Type.ToString() + vbCrLf + _
                       "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "iCopy")

            End Try
        End If
    End Sub

    Public Function GetContrast(ByRef _scanner As WIA.Item) As Integer
        Dim prop_name As String = "Contrast"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center
            Return CInt(Math.Round((temp.Value - center) / delta * 100, 0))
        Else
            Return temp.Value
        End If
    End Function

    Public Sub SetContrast(ByVal value As Integer, ByRef _scanner As WIA.Item)
        Dim prop_name As String = "Contrast"
        Dim temp As WIA.Property = _scanner.Properties(prop_name)

        If temp.SubType = WiaSubType.RangeSubType Then
            Dim min As Integer = temp.SubTypeMin
            Dim max As Integer = temp.SubTypeMax
            Dim stp As Integer = temp.SubTypeStep
            Dim center As Integer = (max + min) / 2
            Dim delta As Integer = max - center

            If value <= 100 And value >= -100 Then
                Dim tmpVal As Integer = CInt(Math.Round(value / 100 * delta + center, 0))
                Try
                    temp.Value = tmpVal
                Catch ex As Exception
                    Throw New ArgumentException(prop_name + " value not accepted by the scanner: " + value.ToString() + " -> " + tmpVal.ToString())
                End Try
            Else
                Throw New ArgumentOutOfRangeException("value", prop_name + " setting must be between -100 and 100," + value.ToString() + " entered.")
            End If
        Else
            Try
                temp.Value = value
            Catch ex As Exception
                MsgBox("There was an exception while setting the property " + prop_name + " to " + value.ToString() + ". Please report this information to iCopy bug tracker on Sourceforge:" + vbCrLf + _
                       "property type: " + temp.Type.ToString() + vbCrLf + _
                       "property subtype: " + temp.SubType.ToString(), MsgBoxStyle.Critical, "iCopy")

            End Try
        End If
    End Sub

    Public ReadOnly Property DeviceId() As String
        Get
            Return _deviceID
        End Get
    End Property

    Public Sub SetBitDepth(ByVal value As Short, ByRef _scanner As WIA.Item)
        If value <= 32 And value Mod 8 = 0 Then 'La profondità è multipla di 8 e minore o uguale a 32 bit
            Try
                _scanner.Properties("Bits Per Pixel").Value = value
            Catch ex As Exception
                'Could throw an ACCESS_DENIED exception
            End Try
        Else
            Throw New ArgumentException("Bit depth must be multiple of 8 and less or equal to 32")
        End If

    End Sub

    Public Sub SetIntent(ByVal value As WiaImageIntent, ByRef _scanner As WIA.Item)
        If value = 1 Then
            _scanner.Properties("Current Intent").Value = value
            If My.Settings.BitsPerPixel <> 0 Then
                SetBitDepth(My.Settings.BitsPerPixel, _scanner)
            End If
        ElseIf value = 2 Or value = 4 Then
            _scanner.Properties("Current Intent").Value = value
        Else
            _scanner.Properties("Current Intent").Value = WiaImageIntent.UnspecifiedIntent
        End If
    End Sub

    Public Sub SetResolution(ByVal value As Integer, ByRef _scanner As WIA.Item)
        If value = 0 Then value = _scanner.Properties("Horizontal Resolution").SubTypeDefault 'In case resolution value hasn't been set
        Try
            _scanner.Properties("Horizontal Resolution").Value = value
            _scanner.Properties("Vertical Resolution").Value = value
        Catch ex As ArgumentException
            If AvailableResolutions.Count > 0 Then
                For i As Integer = 0 To AvailableResolutions.Count - 1
                    If AvailableResolutions(i) = value Then
                        AvailableResolutions.RemoveAt(i)
                        SetResolution(AvailableResolutions(i - 1), _scanner)
                        Return
                    End If
                Next
            Else
                Throw ex
            End If
        End Try
    End Sub

    Public Sub SetMaxExtent(ByRef _scanner As WIA.Item)
        With _scanner.Properties("Horizontal Extent")
            If .SubType = WiaSubType.RangeSubType Then
                .Value = .SubTypeMax
            Else                'TODO: Remove those things
                Dim max As Boolean = False
                Dim hext As Integer = 1000
                Dim n As Integer = 1000
                Do Until max
                    Try
                        .Value = hext
                    Catch ex As ArgumentException
                        If n <> 1 Then
                            hext -= n
                            n /= 10
                        Else
                            max = True
                        End If
                    End Try
                    hext += n
                Loop
            End If
        End With

        With _scanner.Properties("Vertical Extent")
            If .SubType = WiaSubType.RangeSubType Then
                .Value = .SubTypeMax
            Else
                Dim max As Boolean = False
                Dim vext As Integer = 1000
                Dim n As Integer = 1000
                Do Until max
                    Try
                        .Value = vext
                    Catch ex As ArgumentException
                        If n <> 1 Then
                            vext -= n
                            n /= 10
                        Else
                            max = True
                        End If
                    End Try
                    vext += n
                Loop
            End If
        End With
    End Sub

    ReadOnly Property Description() As String
        Get
            Return _description
        End Get
    End Property

    Public Function GetAvailableResolutions(ByRef _scanner As WIA.Item) As List(Of Integer)
        Dim _AvailableResolutions As New List(Of Integer)

        Dim res As WIA.Property = _scanner.Properties("Horizontal Resolution")
        If res.SubType = WiaSubType.RangeSubType Then
            Dim stp As Integer = 100
            Dim min As Integer = 100
            Dim max As Integer = 2000
            If res.SubTypeMin <= 75 Then _AvailableResolutions.Add(75) 'Add ultra low resolution
            If res.SubTypeMin > min Then min = res.SubTypeMin
            If res.SubTypeStep > stp Then stp = res.SubTypeStep
            If res.SubTypeMax < max Then max = res.SubTypeMax
            For i As Integer = min To max Step stp
                _AvailableResolutions.Add(i)
            Next

        ElseIf res.SubType = WiaSubType.ListSubType Then
            For i As Integer = 1 To _scanner.Properties("Horizontal Resolution").SubTypeValues.Count
                _AvailableResolutions.Add(CInt(_scanner.Properties("Horizontal Resolution").SubTypeValues(i)))
            Next i
        End If
        Return _AvailableResolutions
    End Function

    ReadOnly Property AvailableResolutions() As List(Of Integer)
        Get
            Return _AvailableResolutions
        End Get
    End Property

#End Region

    Private Shared Function Compress(ByVal quality As Integer, ByVal tmpImg As ImageFile) As ImageFile
        Dim ip As New ImageProcess()
        ip.Filters.Add(ip.FilterInfos("Convert").FilterID)
        ip.Filters(1).Properties("FormatID").Value = WIA.FormatID.wiaFormatJPEG
        ip.Filters(1).Properties("Quality").Value = quality

        Dim newimg As ImageFile = ip.Apply(tmpImg)
        tmpImg = Nothing
        Return newimg

    End Function

    Function Scan(ByVal options As ScanSettings) As IO.MemoryStream
        Dim dialog As New WIA.CommonDialog
        Dim tmpImg As ImageFile
        If options.Preview Then
            'With preview
            Try
                tmpImg = dialog.ShowAcquireImage(WiaDeviceType.ScannerDeviceType, options.Intent, WiaImageBias.MaximizeQuality, , False, True, True)
            Catch ex As ArgumentException
                MessageBox.Show(ex.Message)
                ' Show the stack trace, which is a list of methods that are currently executing.
                MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
            End Try
        Else
            'Without preview
            Dim _device As Device
            Dim _scanner As WIA.Item

            Try
                _device = manager.DeviceInfos.Item(DeviceId).Connect
                _deviceID = DeviceId
                _scanner = _device.Items(1)
                'TODO: Set all the scanner properties

            Catch ex As Exception
                Throw
            End Try

            'Set all properties
            Try
                SetBrightess(options.Brightness, _scanner)
                SetContrast(options.Contrast, _scanner)
                SetIntent(options.Intent, _scanner)
                'TODO: SetBitDepth
                SetResolution(options.Resolution, _scanner)
                SetMaxExtent(_scanner)
            Catch ex As Exception

            End Try
            'Begin the transfer. The file is saved to a WIA image file that is then put on a memory stream.
            Try
                tmpImg = dialog.ShowTransfer(_device.Items(1), , True)

            Catch ex As Runtime.InteropServices.COMException
                If ex.ErrorCode <> WIA_ERRORS.WIA_ERROR_NO_SCANNER_CONNECTED Then Throw ex

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
            End Try
        End If

        tmpImg = Compress(options.Quality, tmpImg)
        'Converts WIA.ImageFile into Image
        'Don't dispose this stream!
        Dim stream As IO.MemoryStream
        stream = New IO.MemoryStream(CType(tmpImg.FileData.BinaryData, Byte()))
        Return stream
    End Function

    Function ScanImg(ByVal options As ScanSettings) As Image
        Dim Img As Image = Image.FromStream(Scan(options))
        Return Img
    End Function

    <CLSCompliant(False)> _
    Function PropToXML(ByVal prop As WIA.IProperty, ByVal doc As XmlDocument) As XmlNode
        PropToXML = doc.CreateNode(XmlNodeType.Element, "Property", "")
        PropToXML.Attributes.Append(doc.CreateAttribute("Name", "")).Value = prop.Name
        PropToXML.Attributes.Append(doc.CreateAttribute("ID", "")).Value = prop.PropertyID
        PropToXML.Attributes.Append(doc.CreateAttribute("Type", "")).Value = prop.Type
        If prop.IsVector Then
            PropToXML.AppendChild(doc.CreateNode(XmlNodeType.Element, "Vector", "")).InnerText = prop.IsVector
        Else
            If prop.SubType <> WIA.WiaSubType.UnspecifiedSubType Then
                PropToXML.AppendChild(doc.CreateNode(XmlNodeType.Element, "DefaultValue", "")).InnerText = prop.SubTypeDefault
            End If
        End If

        PropToXML.Attributes.Append(doc.CreateAttribute("isReadOnly", "")).Value = prop.IsReadOnly
        PropToXML.AppendChild(doc.CreateNode(XmlNodeType.Element, "Value", "")).InnerText = prop.Value
        Dim subType As XmlNode = PropToXML.AppendChild(doc.CreateNode(XmlNodeType.Element, "SubType", ""))

        Select Case prop.SubType
            Case WiaSubType.FlagSubType
                subType.Attributes.Append(doc.CreateAttribute("Type")).Value = "Flag"
                Dim flags As XmlNode = doc.CreateNode(XmlNodeType.Element, "PossibleValues", "")
                For i = 1 To prop.SubTypeValues.Count
                    flags.AppendChild(doc.CreateNode(XmlNodeType.Element, "Value", "")).InnerText = prop.SubTypeValues(i)
                Next
                subType.AppendChild(flags)
            Case WiaSubType.ListSubType
                subType.Attributes.Append(doc.CreateAttribute("Type")).Value = "List"
                Dim flags As XmlNode = doc.CreateNode(XmlNodeType.Element, "PossibleValues", "")
                For i = 1 To prop.SubTypeValues.Count
                    flags.AppendChild(doc.CreateNode(XmlNodeType.Element, "Value", "")).InnerText = prop.SubTypeValues(i)
                Next
                subType.AppendChild(flags)
            Case WiaSubType.RangeSubType
                subType.Attributes.Append(doc.CreateAttribute("Type")).Value = "Range"
                subType.Attributes.Append(doc.CreateAttribute("Min")).Value = prop.SubTypeMin
                subType.Attributes.Append(doc.CreateAttribute("Max")).Value = prop.SubTypeMax
                subType.Attributes.Append(doc.CreateAttribute("Step")).Value = prop.SubTypeStep

            Case Else 'UnspecifiedSubType
                subType.Attributes.Append(doc.CreateAttribute("Type")).Value = "Unspecified"

        End Select

    End Function

    Sub WritePropertiesLogXML(ByVal doc As XmlDocument)

        Dim _device As Device
        Dim _scanner As WIA.Item

        _device = manager.DeviceInfos.Item(DeviceId).Connect
        _deviceID = DeviceId
        _scanner = _device.Items(1)

        Dim root As XmlElement = doc.DocumentElement

        Dim nodeDevProp As XmlNode
        nodeDevProp = doc.CreateNode(XmlNodeType.Element, "DeviceProperties", "")
        root.AppendChild(nodeDevProp)

        For Each p As WIA.Property In _device.Properties
            nodeDevProp.AppendChild(PropToXML(p, doc))
        Next

        Dim nodeScaProp As XmlNode
        nodeScaProp = doc.CreateNode(XmlNodeType.Element, "ScannerProperties", "")
        root.AppendChild(nodeScaProp)

        For Each p In _scanner.Properties
            nodeScaProp.AppendChild(PropToXML(p, doc))
        Next

    End Sub

End Class
