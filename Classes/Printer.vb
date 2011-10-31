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

Imports System.Drawing.Printing

Friend Class Printer

    Private WithEvents pd As PrintDocument

    Dim _scaleperc As Short

    Dim _images As New ImageBuffer 'The buffer of images to be printed

    Public Event BeginPrint As PrintEventHandler
    Public Event EndPrint As PrintEventHandler

    Public Sub New()
        'Initializes PrintDocument
        pd = New PrintDocument
        pd.DocumentName = "iCopy"
    End Sub

    Public Property Name() As String
        Get
            Name = pd.PrinterSettings.PrinterName
        End Get

        Set(ByVal value As String)
            If value = "" Then
                Dim tmpPd As New PrintDocument
                value = tmpPd.PrinterSettings.PrinterName
            End If
            pd.PrinterSettings.PrinterName = value
            If Not pd.PrinterSettings.IsValid Then Throw New ArgumentException("Printer name is not valid")
        End Set
    End Property

    ReadOnly Property PrinterSettings() As PrinterSettings
        Get
            PrinterSettings = pd.PrinterSettings
        End Get
    End Property

    ReadOnly Property PageSettings() As PageSettings
        Get
            PageSettings = pd.DefaultPageSettings
        End Get
    End Property

    Sub showPreferences()
        Dim dlg As New PrintDialog
        dlg.Document = pd
        dlg.UseEXDialog = True
        dlg.AllowSelection = False
        dlg.AllowSomePages = False
        dlg.ShowDialog()
        pd.DefaultPageSettings = dlg.Document.DefaultPageSettings
    End Sub

    Sub AddImage(ByVal image As Image, Optional ByVal scaleperc As Short = 100)
        'Adds an image to the image buffer
        Dim stream As IO.MemoryStream
        stream = New IO.MemoryStream()
        image.Save(stream, image.RawFormat)
        _images.Add(image.FromStream(stream))

        _scaleperc = scaleperc
    End Sub

    Sub AddImage(ByVal stream As IO.MemoryStream, Optional ByVal scaleperc As Short = 100)
        _images.Add(Image.FromStream(stream))
        stream.Close()
        stream.Dispose()
        _scaleperc = scaleperc
    End Sub

    Sub AddImages(ByVal images As List(Of Image), Optional ByVal scaleperc As Short = 100)
        If images IsNot Nothing Then
            For Each img As Image In images
                _images.Add(img)
            Next
            _scaleperc = scaleperc
        End If
    End Sub

    Public Sub Print(Optional ByVal copies As Short = 1)
        'Check if Image Buffer is empty
        If _images.Count = 0 Then Exit Sub

        pd.PrinterSettings.Copies = copies
        'Starts printing process
        pd.Print()
        'TODO:Check Win32Exception

    End Sub

    Private Sub pd_Print(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles pd.PrintPage
        'Print the current image in the image buffer

        'Resize the images, then draw it 
        e.Graphics.ScaleTransform(_scaleperc / 100, _scaleperc / 100)
        e.Graphics.DrawImage(_images.Item(_images.Counter), 0, 0)

        'Check if other pages have to be printed
        If _images.Counter < _images.Count - 1 Then
            e.HasMorePages = True
            _images.Counter += 1
        End If
    End Sub

    Sub pd_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles pd.BeginPrint
        RaiseEvent BeginPrint(sender, e)
    End Sub

    Sub pd_EndPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles pd.EndPrint
        RaiseEvent EndPrint(sender, e)

        'Empty image buffer
        _images.Clear()
        _images = New ImageBuffer
    End Sub

    Sub ClearBuffer()
        _images.Clear()
    End Sub

End Class
