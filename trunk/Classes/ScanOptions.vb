Imports WIA

Public Class ScanOptions

    Private _Brightness As Integer
    Private _Contrast As Integer

    Public Property Brightness() As Integer
        Get
            Return _Brightness
        End Get
        Set(ByVal value As Integer)
            _Brightness = value
        End Set
    End Property

    Public Property Contrast() As Integer
        Get
            Return _Contrast
        End Get
        Set(ByVal value As Integer)
            _Contrast = value
        End Set
    End Property

    Private _Resolution As Short
    Public Property Resolution() As Integer
        Get
            Return _Resolution
        End Get
        Set(ByVal value As Integer)
            _Resolution = value
        End Set
    End Property

    Private _Intent As WiaImageIntent
    Public Property Intent() As WiaImageIntent
        Get
            Return _Intent
        End Get
        Set(ByVal value As WiaImageIntent)
            _Intent = value
        End Set
    End Property

    Private _Quality As Integer
    Public Property Quality() As Integer
        Get
            Return _Quality
        End Get
        Set(ByVal value As Integer)
            _Quality = value
        End Set
    End Property

    Private _Preview As Boolean
    Public Property Preview() As Boolean
        Get
            Return _Preview
        End Get
        Set(ByVal value As Boolean)
            _Preview = value
        End Set
    End Property

End Class
