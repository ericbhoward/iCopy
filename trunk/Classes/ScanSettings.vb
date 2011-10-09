Imports WIA

Public Class ScanSettings

    Private _Brightness As Integer
    Private _Contrast As Integer

    Public Sub New()
        _Brightness = 0
        _Contrast = 0
        _Quality = 100
        _Preview = False
        _Copies = 1
        _Intent = WIA.WiaImageIntent.ColorIntent
        _Resolution = 100
        _Scaling = 100
    End Sub

    Public Sub New(ByVal Res As Integer)
        _Brightness = 0
        _Contrast = 0
        _Quality = 100
        _Preview = False
        _Copies = 1
        _Intent = WIA.WiaImageIntent.ColorIntent
        _Resolution = Res
        _Scaling = 100
        _BitDepth = 0
    End Sub


    Private _BitDepth As Integer
    Public Property BitDepth() As Integer
        Get
            Return _BitDepth
        End Get
        Set(ByVal value As Integer)
            _BitDepth = value
        End Set
    End Property


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

    Private _Scaling As Integer
    Public Property Scaling() As Integer
        Get
            Return _Scaling
        End Get
        Set(ByVal value As Integer)
            _Scaling = value
        End Set
    End Property

    Private _Copies As Integer
    Public Property Copies() As Integer
        Get
            Return _Copies
        End Get
        Set(ByVal value As Integer)
            _Copies = value
        End Set
    End Property

End Class
