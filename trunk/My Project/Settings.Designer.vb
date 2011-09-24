﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.21006.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region

        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DeviceID() As String
            Get
                Return CType(Me("DeviceID"),String)
            End Get
            Set
                Me("DeviceID") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("ColorIntent"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property DefaultIntent() As Global.WIA.WiaImageIntent
            Get
                Return CType(Me("DefaultIntent"),Global.WIA.WiaImageIntent)
            End Get
            Set
                Me("DefaultIntent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property Brightness() As Short
            Get
                Return CType(Me("Brightness"),Short)
            End Get
            Set
                Me("Brightness") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property Contrast() As Short
            Get
                Return CType(Me("Contrast"),Short)
            End Get
            Set
                Me("Contrast") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("100")>  _
        Public Property Resolution() As Short
            Get
                Return CType(Me("Resolution"),Short)
            End Get
            Set
                Me("Resolution") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property PrintColor() As Boolean
            Get
                Return CType(Me("PrintColor"),Boolean)
            End Get
            Set
                Me("PrintColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("100, 100")>  _
        Public Property Location() As Global.System.Drawing.Point
            Get
                Return CType(Me("Location"),Global.System.Drawing.Point)
            End Get
            Set
                Me("Location") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property StoreLocation() As Boolean
            Get
                Return CType(Me("StoreLocation"),Boolean)
            End Get
            Set
                Me("StoreLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("(Default)"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return CType(Me("Culture"),Global.System.Globalization.CultureInfo)
            End Get
            Set
                Me("Culture") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property RememberSettings() As Boolean
            Get
                Return CType(Me("RememberSettings"),Boolean)
            End Get
            Set
                Me("RememberSettings") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DefaultPrinter() As String
            Get
                Return CType(Me("DefaultPrinter"),String)
            End Get
            Set
                Me("DefaultPrinter") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property DefaultScanner() As String
            Get
                Return CType(Me("DefaultScanner"),String)
            End Get
            Set
                Me("DefaultScanner") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("A4"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property PrinterSize() As String
            Get
                Return CType(Me("PrinterSize"),String)
            End Get
            Set
                Me("PrinterSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property ID() As Integer
            Get
                Return CType(Me("ID"),Integer)
            End Get
            Set
                Me("ID") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property SendInfo() As Boolean
            Get
                Return CType(Me("SendInfo"),Boolean)
            End Get
            Set
                Me("SendInfo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property ncopies() As Integer
            Get
                Return CType(Me("ncopies"),Integer)
            End Get
            Set
                Me("ncopies") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property npreview() As Integer
            Get
                Return CType(Me("npreview"),Integer)
            End Get
            Set
                Me("npreview") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property nmultiple() As Integer
            Get
                Return CType(Me("nmultiple"),Integer)
            End Get
            Set
                Me("nmultiple") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property nfile() As Integer
            Get
                Return CType(Me("nfile"),Integer)
            End Get
            Set
                Me("nfile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property CustomCulture() As Boolean
            Get
                Return CType(Me("CustomCulture"),Boolean)
            End Get
            Set
                Me("CustomCulture") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("10"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property Compression() As Integer
            Get
                Return CType(Me("Compression"),Integer)
            End Get
            Set
                Me("Compression") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Configuration.SettingsProviderAttribute(GetType(iCopy.PortableSettingsProvider)),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property BitsPerPixel() As Integer
            Get
                Return CType(Me("BitsPerPixel"),Integer)
            End Get
            Set
                Me("BitsPerPixel") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.iCopy.My.MySettings
            Get
                Return Global.iCopy.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
