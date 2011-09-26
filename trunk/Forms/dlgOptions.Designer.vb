<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgOptions
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
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cboLanguage = New System.Windows.Forms.ComboBox()
        Me.chkRememberWindowPos = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip()
        Me.lblBitDepth = New System.Windows.Forms.Label()
        Me.cboBitDepth = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(172, 100)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(65, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(243, 100)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(65, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(12, 15)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(55, 13)
        Me.lblLanguage.TabIndex = 1
        Me.lblLanguage.Text = "Language"
        '
        'cboLanguage
        '
        Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguage.FormattingEnabled = True
        Me.cboLanguage.Location = New System.Drawing.Point(73, 12)
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.Size = New System.Drawing.Size(235, 21)
        Me.cboLanguage.TabIndex = 2
        '
        'chkRememberWindowPos
        '
        Me.chkRememberWindowPos.Location = New System.Drawing.Point(15, 39)
        Me.chkRememberWindowPos.Name = "chkRememberWindowPos"
        Me.chkRememberWindowPos.Size = New System.Drawing.Size(293, 17)
        Me.chkRememberWindowPos.TabIndex = 3
        Me.chkRememberWindowPos.Text = "Remember window position"
        Me.chkRememberWindowPos.UseVisualStyleBackColor = True
        '
        'lblBitDepth
        '
        Me.lblBitDepth.Location = New System.Drawing.Point(12, 65)
        Me.lblBitDepth.Name = "lblBitDepth"
        Me.lblBitDepth.Size = New System.Drawing.Size(196, 13)
        Me.lblBitDepth.TabIndex = 6
        Me.lblBitDepth.Text = "Force Bit Depth for Color Mode"
        Me.lblBitDepth.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboBitDepth
        '
        Me.cboBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBitDepth.FormattingEnabled = True
        Me.cboBitDepth.Items.AddRange(New Object() {"Auto", "8", "16", "24", "32"})
        Me.cboBitDepth.Location = New System.Drawing.Point(214, 62)
        Me.cboBitDepth.Name = "cboBitDepth"
        Me.cboBitDepth.Size = New System.Drawing.Size(94, 21)
        Me.cboBitDepth.TabIndex = 7
        '
        'dlgOptions
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(320, 135)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.cboBitDepth)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblBitDepth)
        Me.Controls.Add(Me.chkRememberWindowPos)
        Me.Controls.Add(Me.cboLanguage)
        Me.Controls.Add(Me.lblLanguage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkRememberWindowPos As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblBitDepth As System.Windows.Forms.Label
    Friend WithEvents cboBitDepth As System.Windows.Forms.ComboBox

End Class
