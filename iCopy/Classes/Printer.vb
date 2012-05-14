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

Imports System.Drawing.Printing

Friend Class Printer

    Private WithEvents pd As PrintDocument

    Dim _scaleperc As Short
    Dim _center As Boolean

    Dim _images As New Queue(Of String) 'The buffer of images to be printed

    Public Event BeginPrint As PrintEventHandler
    Public Event EndPrint As PrintEventHandler

    Public Sub New()
        'Initializes PrintDocument
        pd = New PrintDocument
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

    Sub AddImages(ByVal images As List(Of String), Optional ByVal scaleperc As Short = 100, Optional center As Boolean = False)
        If images IsNot Nothing Then
            For Each img As String In images
                _images.Enqueue(img)
            Next
            _scaleperc = scaleperc
            _center = center
        End If
    End Sub

    Public Sub Print(Optional ByVal copies As Short = 1)
        'Check if Image Buffer is empty
        If _images.Count = 0 Then Exit Sub

        pd.PrinterSettings.Copies = copies
        pd.DocumentName = "iCopy " + Date.Now.ToString("yyyy-MM-dd hh-mm")
        'Starts printing process
        pd.Print()

    End Sub

    Private Sub pd_Print(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles pd.PrintPage
        'Print the current image in the image buffer

        'Loads the image from the temporary file
        Dim imgPath As String = _images.Dequeue()
        Dim img As Image = Image.FromFile(imgPath)

        e.Graphics.ScaleTransform(_scaleperc / 100, _scaleperc / 100)
        'Resize the image, then draw it 
        If _center Then
            Dim picture_bounds As RectangleF = img.GetBounds(e.Graphics.PageUnit)

            Dim margin_bounds As RectangleF = e.Graphics.VisibleClipBounds

            ' Apply the transformation.
            Dim dx As Single = _
                margin_bounds.Left - _scaleperc / 100 * picture_bounds.Left + _
                (margin_bounds.Width - _scaleperc / 100 * picture_bounds.Width) / 4
            Dim dy As Single = _
                margin_bounds.Top - _scaleperc / 100 * picture_bounds.Top + _
                (margin_bounds.Height - _scaleperc / 100 * picture_bounds.Height) / 4
            e.Graphics.TranslateTransform(dx, dy)
        End If

        e.Graphics.DrawImage(img, 0, 0)

        img.Dispose()

        Try
            IO.File.Delete(imgPath)
        Catch ex As Exception
            Throw
        End Try
        'Check if other pages have to be printed
        If _images.Count > 0 Then
            e.HasMorePages = True
        End If
    End Sub

    Sub pd_BeginPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles pd.BeginPrint
        RaiseEvent BeginPrint(sender, e)
    End Sub

    Sub pd_EndPrint(ByVal sender As Object, ByVal e As PrintEventArgs) Handles pd.EndPrint
        RaiseEvent EndPrint(sender, e)

        'Empty image buffer
        _images.Clear()
    End Sub

    Sub ClearBuffer()
        _images.Clear()
    End Sub

End Class
