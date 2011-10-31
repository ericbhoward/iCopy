<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainFrm
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
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnSelScanner = New System.Windows.Forms.Button()
        Me.cboPrintMode = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboScanMode = New System.Windows.Forms.ComboBox()
        Me.btnPrintSetup = New System.Windows.Forms.Button()
        Me.nudNCopie = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ScannerStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrinterStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.VersionStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkPreview = New System.Windows.Forms.CheckBox()
        Me.btnScanModes = New System.Windows.Forms.Button()
        Me.llblAbout = New System.Windows.Forms.LinkLabel()
        Me.llblSettings = New System.Windows.Forms.LinkLabel()
        Me.btnImageSettings = New System.Windows.Forms.Button()
        Me.ScanMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ScanMultiplePages = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScanToFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboPaperSize = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkADF = New System.Windows.Forms.CheckBox()
        CType(Me.nudNCopie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ScanMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCopy
        '
        Me.btnCopy.Image = Global.iCopy.My.Resources.Resources.iCopyBig
        Me.btnCopy.Location = New System.Drawing.Point(12, 12)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(138, 134)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnSelScanner
        '
        Me.btnSelScanner.Location = New System.Drawing.Point(364, 13)
        Me.btnSelScanner.Name = "btnSelScanner"
        Me.btnSelScanner.Size = New System.Drawing.Size(137, 23)
        Me.btnSelScanner.TabIndex = 7
        Me.btnSelScanner.Text = "Choose &scanner..."
        Me.btnSelScanner.UseVisualStyleBackColor = True
        '
        'cboPrintMode
        '
        Me.cboPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrintMode.FormattingEnabled = True
        Me.cboPrintMode.Location = New System.Drawing.Point(251, 45)
        Me.cboPrintMode.Name = "cboPrintMode"
        Me.cboPrintMode.Size = New System.Drawing.Size(107, 21)
        Me.cboPrintMode.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(160, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 18)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Printer"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(160, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 18)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Scanner"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboScanMode
        '
        Me.cboScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboScanMode.FormattingEnabled = True
        Me.cboScanMode.Location = New System.Drawing.Point(251, 14)
        Me.cboScanMode.Name = "cboScanMode"
        Me.cboScanMode.Size = New System.Drawing.Size(107, 21)
        Me.cboScanMode.TabIndex = 2
        '
        'btnPrintSetup
        '
        Me.btnPrintSetup.Location = New System.Drawing.Point(364, 44)
        Me.btnPrintSetup.Name = "btnPrintSetup"
        Me.btnPrintSetup.Size = New System.Drawing.Size(137, 23)
        Me.btnPrintSetup.TabIndex = 6
        Me.btnPrintSetup.Text = "&Printer options..."
        Me.btnPrintSetup.UseVisualStyleBackColor = True
        '
        'nudNCopie
        '
        Me.nudNCopie.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudNCopie.Location = New System.Drawing.Point(284, 114)
        Me.nudNCopie.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNCopie.Name = "nudNCopie"
        Me.nudNCopie.Size = New System.Drawing.Size(55, 26)
        Me.nudNCopie.TabIndex = 1
        Me.nudNCopie.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(167, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 19)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "N° of copies"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScannerStatusLabel, Me.PrinterStatusLabel, Me.VersionStatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 188)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(513, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 20
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ScannerStatusLabel
        '
        Me.ScannerStatusLabel.Image = Global.iCopy.My.Resources.Resources.scanner
        Me.ScannerStatusLabel.Name = "ScannerStatusLabel"
        Me.ScannerStatusLabel.Size = New System.Drawing.Size(137, 17)
        Me.ScannerStatusLabel.Text = "ToolStripStatusLabel1"
        '
        'PrinterStatusLabel
        '
        Me.PrinterStatusLabel.Image = Global.iCopy.My.Resources.Resources.printer
        Me.PrinterStatusLabel.Name = "PrinterStatusLabel"
        Me.PrinterStatusLabel.Size = New System.Drawing.Size(137, 17)
        Me.PrinterStatusLabel.Text = "ToolStripStatusLabel2"
        '
        'VersionStatusLabel
        '
        Me.VersionStatusLabel.IsLink = True
        Me.VersionStatusLabel.Name = "VersionStatusLabel"
        Me.VersionStatusLabel.Size = New System.Drawing.Size(224, 17)
        Me.VersionStatusLabel.Spring = True
        Me.VersionStatusLabel.Text = "New Version Available!"
        Me.VersionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VersionStatusLabel.Visible = False
        '
        'chkPreview
        '
        Me.chkPreview.AutoSize = True
        Me.chkPreview.Location = New System.Drawing.Point(181, 157)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(64, 17)
        Me.chkPreview.TabIndex = 4
        Me.chkPreview.Text = "Preview"
        Me.chkPreview.UseVisualStyleBackColor = True
        '
        'btnScanModes
        '
        Me.btnScanModes.Location = New System.Drawing.Point(12, 152)
        Me.btnScanModes.Name = "btnScanModes"
        Me.btnScanModes.Size = New System.Drawing.Size(138, 25)
        Me.btnScanModes.TabIndex = 8
        Me.btnScanModes.Text = "Other scan modes"
        Me.btnScanModes.UseVisualStyleBackColor = True
        '
        'llblAbout
        '
        Me.llblAbout.Location = New System.Drawing.Point(352, 164)
        Me.llblAbout.Name = "llblAbout"
        Me.llblAbout.Size = New System.Drawing.Size(149, 13)
        Me.llblAbout.TabIndex = 10
        Me.llblAbout.TabStop = True
        Me.llblAbout.Text = "About iCopy"
        Me.llblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llblSettings
        '
        Me.llblSettings.Location = New System.Drawing.Point(357, 148)
        Me.llblSettings.Name = "llblSettings"
        Me.llblSettings.Size = New System.Drawing.Size(144, 13)
        Me.llblSettings.TabIndex = 9
        Me.llblSettings.TabStop = True
        Me.llblSettings.Text = "Settings"
        Me.llblSettings.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnImageSettings
        '
        Me.btnImageSettings.Location = New System.Drawing.Point(367, 98)
        Me.btnImageSettings.Name = "btnImageSettings"
        Me.btnImageSettings.Size = New System.Drawing.Size(134, 42)
        Me.btnImageSettings.TabIndex = 5
        Me.btnImageSettings.Text = "Image Settings >>"
        Me.btnImageSettings.UseVisualStyleBackColor = True
        '
        'ScanMenuStrip
        '
        Me.ScanMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScanMultiplePages, Me.ScanToFile})
        Me.ScanMenuStrip.Name = "ScanMenuStrip"
        Me.ScanMenuStrip.Size = New System.Drawing.Size(229, 48)
        '
        'ScanMultiplePages
        '
        Me.ScanMultiplePages.Name = "ScanMultiplePages"
        Me.ScanMultiplePages.ShortcutKeyDisplayString = "Ctrl +M"
        Me.ScanMultiplePages.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.ScanMultiplePages.Size = New System.Drawing.Size(228, 22)
        Me.ScanMultiplePages.Text = "Scan Multiple Pages"
        '
        'ScanToFile
        '
        Me.ScanToFile.Name = "ScanToFile"
        Me.ScanToFile.ShortcutKeyDisplayString = "Ctrl+F"
        Me.ScanToFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.ScanToFile.Size = New System.Drawing.Size(228, 22)
        Me.ScanToFile.Text = "Scan to &File"
        '
        'cboPaperSize
        '
        Me.cboPaperSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cboPaperSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaperSize.FormattingEnabled = True
        Me.cboPaperSize.Location = New System.Drawing.Point(251, 75)
        Me.cboPaperSize.Name = "cboPaperSize"
        Me.cboPaperSize.Size = New System.Drawing.Size(107, 21)
        Me.cboPaperSize.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(156, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 23)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Paper Size"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkADF
        '
        Me.chkADF.Location = New System.Drawing.Point(262, 150)
        Me.chkADF.Name = "chkADF"
        Me.chkADF.Size = New System.Drawing.Size(141, 31)
        Me.chkADF.TabIndex = 23
        Me.chkADF.Text = "Use Document Feeder (beta)"
        Me.chkADF.UseVisualStyleBackColor = True
        '
        'mainFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(513, 210)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.chkADF)
        Me.Controls.Add(Me.cboPrintMode)
        Me.Controls.Add(Me.llblSettings)
        Me.Controls.Add(Me.btnScanModes)
        Me.Controls.Add(Me.cboPaperSize)
        Me.Controls.Add(Me.cboScanMode)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.nudNCopie)
        Me.Controls.Add(Me.btnImageSettings)
        Me.Controls.Add(Me.btnSelScanner)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.llblAbout)
        Me.Controls.Add(Me.btnPrintSetup)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "mainFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "iCopy"
        CType(Me.nudNCopie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ScanMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnSelScanner As System.Windows.Forms.Button
    Friend WithEvents btnPrintSetup As System.Windows.Forms.Button
    Friend WithEvents nudNCopie As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboScanMode As System.Windows.Forms.ComboBox
    Friend WithEvents cboPrintMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents btnScanModes As System.Windows.Forms.Button
    Friend WithEvents llblAbout As System.Windows.Forms.LinkLabel
    Friend WithEvents llblSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents btnImageSettings As System.Windows.Forms.Button
    Friend WithEvents ScanMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ScanToFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScanMultiplePages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboPaperSize As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ScannerStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PrinterStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents VersionStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents chkADF As System.Windows.Forms.CheckBox
End Class
