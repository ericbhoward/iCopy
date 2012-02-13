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
        Me.components = New System.ComponentModel.Container()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cboLanguage = New System.Windows.Forms.ComboBox()
        Me.chkRememberWindowPos = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblBitDepth = New System.Windows.Forms.Label()
        Me.cboBitDepth = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkRememberScanSettings = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.lblScanner = New System.Windows.Forms.Label()
        Me.lblAvailableEvents = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.btnDefaultSettings = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(206, 259)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(65, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(277, 259)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(65, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'lblLanguage
        '
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(9, 20)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(55, 13)
        Me.lblLanguage.TabIndex = 1
        Me.lblLanguage.Text = "Language"
        '
        'cboLanguage
        '
        Me.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguage.FormattingEnabled = True
        Me.cboLanguage.Location = New System.Drawing.Point(144, 17)
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.Size = New System.Drawing.Size(161, 21)
        Me.cboLanguage.TabIndex = 2
        '
        'chkRememberWindowPos
        '
        Me.chkRememberWindowPos.Checked = Global.iCopy.My.MySettings.Default.StoreLocation
        Me.chkRememberWindowPos.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.iCopy.My.MySettings.Default, "StoreLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkRememberWindowPos.Location = New System.Drawing.Point(12, 87)
        Me.chkRememberWindowPos.Name = "chkRememberWindowPos"
        Me.chkRememberWindowPos.Size = New System.Drawing.Size(293, 17)
        Me.chkRememberWindowPos.TabIndex = 3
        Me.chkRememberWindowPos.Text = "Remember window position"
        Me.chkRememberWindowPos.UseVisualStyleBackColor = True
        '
        'lblBitDepth
        '
        Me.lblBitDepth.Location = New System.Drawing.Point(9, 185)
        Me.lblBitDepth.Name = "lblBitDepth"
        Me.lblBitDepth.Size = New System.Drawing.Size(196, 13)
        Me.lblBitDepth.TabIndex = 6
        Me.lblBitDepth.Text = "Force Bit Depth for Color Mode"
        '
        'cboBitDepth
        '
        Me.cboBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBitDepth.FormattingEnabled = True
        Me.cboBitDepth.Items.AddRange(New Object() {"Auto", "8", "16", "24", "32"})
        Me.cboBitDepth.Location = New System.Drawing.Point(211, 182)
        Me.cboBitDepth.Name = "cboBitDepth"
        Me.cboBitDepth.Size = New System.Drawing.Size(94, 21)
        Me.cboBitDepth.TabIndex = 7
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(330, 241)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkRememberScanSettings)
        Me.TabPage1.Controls.Add(Me.cboLanguage)
        Me.TabPage1.Controls.Add(Me.lblLanguage)
        Me.TabPage1.Controls.Add(Me.cboBitDepth)
        Me.TabPage1.Controls.Add(Me.chkRememberWindowPos)
        Me.TabPage1.Controls.Add(Me.lblBitDepth)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(322, 215)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkRememberScanSettings
        '
        Me.chkRememberScanSettings.AutoSize = True
        Me.chkRememberScanSettings.Checked = Global.iCopy.My.MySettings.Default.RememberSettings
        Me.chkRememberScanSettings.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRememberScanSettings.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.iCopy.My.MySettings.Default, "RememberSettings", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkRememberScanSettings.Location = New System.Drawing.Point(12, 64)
        Me.chkRememberScanSettings.Name = "chkRememberScanSettings"
        Me.chkRememberScanSettings.Size = New System.Drawing.Size(138, 17)
        Me.chkRememberScanSettings.TabIndex = 8
        Me.chkRememberScanSettings.Text = "Remeber Scan Settings"
        Me.chkRememberScanSettings.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.ComboBox1)
        Me.TabPage2.Controls.Add(Me.lblScanner)
        Me.TabPage2.Controls.Add(Me.lblAvailableEvents)
        Me.TabPage2.Controls.Add(Me.ListBox1)
        Me.TabPage2.Controls.Add(Me.btnRegister)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(322, 215)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Scanner Buttons"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(213, 169)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(66, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Unregister"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(9, 171)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(126, 21)
        Me.ComboBox1.TabIndex = 7
        '
        'lblScanner
        '
        Me.lblScanner.AutoSize = True
        Me.lblScanner.Enabled = False
        Me.lblScanner.Location = New System.Drawing.Point(6, 16)
        Me.lblScanner.Name = "lblScanner"
        Me.lblScanner.Size = New System.Drawing.Size(53, 13)
        Me.lblScanner.TabIndex = 6
        Me.lblScanner.Text = "Scanner: "
        '
        'lblAvailableEvents
        '
        Me.lblAvailableEvents.AutoSize = True
        Me.lblAvailableEvents.Enabled = False
        Me.lblAvailableEvents.Location = New System.Drawing.Point(6, 38)
        Me.lblAvailableEvents.Name = "lblAvailableEvents"
        Me.lblAvailableEvents.Size = New System.Drawing.Size(86, 13)
        Me.lblAvailableEvents.TabIndex = 5
        Me.lblAvailableEvents.Text = "Available Events"
        '
        'ListBox1
        '
        Me.ListBox1.Enabled = False
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(9, 60)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(270, 95)
        Me.ListBox1.TabIndex = 1
        '
        'btnRegister
        '
        Me.btnRegister.Enabled = False
        Me.btnRegister.Location = New System.Drawing.Point(141, 169)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(66, 23)
        Me.btnRegister.TabIndex = 0
        Me.btnRegister.Text = "Register"
        Me.btnRegister.UseVisualStyleBackColor = True
        '
        'btnDefaultSettings
        '
        Me.btnDefaultSettings.Location = New System.Drawing.Point(16, 259)
        Me.btnDefaultSettings.Name = "btnDefaultSettings"
        Me.btnDefaultSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnDefaultSettings.TabIndex = 9
        Me.btnDefaultSettings.Text = "&Default"
        Me.btnDefaultSettings.UseVisualStyleBackColor = True
        '
        'dlgOptions
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(354, 294)
        Me.Controls.Add(Me.btnDefaultSettings)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cboLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkRememberWindowPos As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblBitDepth As System.Windows.Forms.Label
    Friend WithEvents cboBitDepth As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents chkRememberScanSettings As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnDefaultSettings As System.Windows.Forms.Button
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents lblScanner As System.Windows.Forms.Label
    Friend WithEvents lblAvailableEvents As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
