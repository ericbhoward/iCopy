<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId:="frm")> <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImageSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tbenlargement = New System.Windows.Forms.TrackBar()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbContrast = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbBrightness = New System.Windows.Forms.TrackBar()
        Me.txtContrast = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBrightness = New System.Windows.Forms.TextBox()
        Me.txtenlargement = New System.Windows.Forms.TextBox()
        Me.lblPerc = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.cboResolution = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCompressionLabel = New System.Windows.Forms.Label()
        Me.tbCompression = New System.Windows.Forms.TrackBar()
        Me.lblCompression = New System.Windows.Forms.Label()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tbenlargement
        '
        Me.tbenlargement.LargeChange = 50
        Me.tbenlargement.Location = New System.Drawing.Point(86, 76)
        Me.tbenlargement.Maximum = 200
        Me.tbenlargement.Minimum = 1
        Me.tbenlargement.Name = "tbenlargement"
        Me.tbenlargement.Size = New System.Drawing.Size(101, 45)
        Me.tbenlargement.SmallChange = 10
        Me.tbenlargement.TabIndex = 39
        Me.tbenlargement.TickFrequency = 50
        Me.tbenlargement.Value = 100
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(7, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "Enlargement"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(7, 119)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "Resolution"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbContrast
        '
        Me.tbContrast.LargeChange = 10
        Me.tbContrast.Location = New System.Drawing.Point(86, 43)
        Me.tbContrast.Maximum = 100
        Me.tbContrast.Minimum = -100
        Me.tbContrast.Name = "tbContrast"
        Me.tbContrast.Size = New System.Drawing.Size(101, 45)
        Me.tbContrast.TabIndex = 32
        Me.tbContrast.TickFrequency = 20
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Brightness"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tbBrightness
        '
        Me.tbBrightness.LargeChange = 10
        Me.tbBrightness.Location = New System.Drawing.Point(86, 14)
        Me.tbBrightness.Maximum = 100
        Me.tbBrightness.Minimum = -100
        Me.tbBrightness.Name = "tbBrightness"
        Me.tbBrightness.Size = New System.Drawing.Size(101, 45)
        Me.tbBrightness.TabIndex = 30
        Me.tbBrightness.TickFrequency = 20
        '
        'txtContrast
        '
        Me.txtContrast.Location = New System.Drawing.Point(193, 45)
        Me.txtContrast.MaxLength = 4
        Me.txtContrast.Name = "txtContrast"
        Me.txtContrast.Size = New System.Drawing.Size(37, 20)
        Me.txtContrast.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Contrast"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBrightness
        '
        Me.txtBrightness.Location = New System.Drawing.Point(193, 17)
        Me.txtBrightness.MaxLength = 4
        Me.txtBrightness.Name = "txtBrightness"
        Me.txtBrightness.Size = New System.Drawing.Size(37, 20)
        Me.txtBrightness.TabIndex = 31
        '
        'txtenlargement
        '
        Me.txtenlargement.Location = New System.Drawing.Point(193, 78)
        Me.txtenlargement.MaxLength = 3
        Me.txtenlargement.Name = "txtenlargement"
        Me.txtenlargement.Size = New System.Drawing.Size(28, 20)
        Me.txtenlargement.TabIndex = 41
        '
        'lblPerc
        '
        Me.lblPerc.AutoSize = True
        Me.lblPerc.Location = New System.Drawing.Point(221, 81)
        Me.lblPerc.Name = "lblPerc"
        Me.lblPerc.Size = New System.Drawing.Size(15, 13)
        Me.lblPerc.TabIndex = 44
        Me.lblPerc.Text = "%"
        '
        'cboResolution
        '
        Me.cboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResolution.FormattingEnabled = True
        Me.cboResolution.Location = New System.Drawing.Point(93, 116)
        Me.cboResolution.Name = "cboResolution"
        Me.cboResolution.Size = New System.Drawing.Size(82, 21)
        Me.cboResolution.TabIndex = 45
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(193, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "DPI"
        '
        'lblCompressionLabel
        '
        Me.lblCompressionLabel.Location = New System.Drawing.Point(7, 143)
        Me.lblCompressionLabel.Name = "lblCompressionLabel"
        Me.lblCompressionLabel.Size = New System.Drawing.Size(80, 33)
        Me.lblCompressionLabel.TabIndex = 47
        Me.lblCompressionLabel.Text = "JPEG Compression"
        Me.lblCompressionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbCompression
        '
        Me.tbCompression.Location = New System.Drawing.Point(86, 143)
        Me.tbCompression.Maximum = 100
        Me.tbCompression.Minimum = 1
        Me.tbCompression.Name = "tbCompression"
        Me.tbCompression.Size = New System.Drawing.Size(101, 45)
        Me.tbCompression.TabIndex = 48
        Me.tbCompression.TickFrequency = 10
        Me.tbCompression.Value = 100
        '
        'lblCompression
        '
        Me.lblCompression.Location = New System.Drawing.Point(193, 153)
        Me.lblCompression.Name = "lblCompression"
        Me.lblCompression.Size = New System.Drawing.Size(28, 23)
        Me.lblCompression.TabIndex = 49
        '
        'btnDefault
        '
        Me.btnDefault.Location = New System.Drawing.Point(155, 179)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(75, 23)
        Me.btnDefault.TabIndex = 50
        Me.btnDefault.Text = "Default"
        Me.btnDefault.UseVisualStyleBackColor = True
        '
        'frmImageSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 210)
        Me.Controls.Add(Me.btnDefault)
        Me.Controls.Add(Me.lblCompression)
        Me.Controls.Add(Me.cboResolution)
        Me.Controls.Add(Me.tbenlargement)
        Me.Controls.Add(Me.tbCompression)
        Me.Controls.Add(Me.lblCompressionLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtContrast)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBrightness)
        Me.Controls.Add(Me.tbContrast)
        Me.Controls.Add(Me.tbBrightness)
        Me.Controls.Add(Me.lblPerc)
        Me.Controls.Add(Me.txtenlargement)
        Me.Controls.Add(Me.Label10)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmImageSettings"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Image Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbenlargement As System.Windows.Forms.TrackBar
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbContrast As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbBrightness As System.Windows.Forms.TrackBar
    Friend WithEvents txtContrast As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBrightness As System.Windows.Forms.TextBox
    Friend WithEvents txtenlargement As System.Windows.Forms.TextBox
    Friend WithEvents lblPerc As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboResolution As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCompressionLabel As System.Windows.Forms.Label
    Friend WithEvents tbCompression As System.Windows.Forms.TrackBar
    Friend WithEvents lblCompression As System.Windows.Forms.Label
    Friend WithEvents btnDefault As System.Windows.Forms.Button
End Class
