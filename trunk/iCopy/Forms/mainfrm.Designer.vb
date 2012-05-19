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
        Me.lblPrinter = New System.Windows.Forms.Label()
        Me.lblScanner = New System.Windows.Forms.Label()
        Me.cboScanMode = New System.Windows.Forms.ComboBox()
        Me.btnPrintSetup = New System.Windows.Forms.Button()
        Me.nudNCopie = New System.Windows.Forms.NumericUpDown()
        Me.lblCopies = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ScannerStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrinterStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.VersionStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.llblAbout = New System.Windows.Forms.LinkLabel()
        Me.llblSettings = New System.Windows.Forms.LinkLabel()
        Me.btnImageSettings = New System.Windows.Forms.Button()
        Me.ScanMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ScanMultiplePages = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScanToFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboPaperSize = New System.Windows.Forms.ComboBox()
        Me.lblPaperSize = New System.Windows.Forms.Label()
        Me.chkADF = New System.Windows.Forms.CheckBox()
        Me.chkDuplex = New System.Windows.Forms.CheckBox()
        Me.chkMultipage = New System.Windows.Forms.CheckBox()
        Me.chkSaveToFile = New System.Windows.Forms.CheckBox()
        Me.chkPDF = New System.Windows.Forms.CheckBox()
        Me.chkPreview = New System.Windows.Forms.CheckBox()
        CType(Me.nudNCopie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ScanMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCopy
        '
        Me.btnCopy.Image = Global.iCopy.My.Resources.Resources.iCopyBig
        Me.btnCopy.Location = New System.Drawing.Point(12, 11)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(138, 134)
        Me.btnCopy.TabIndex = 0
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnSelScanner
        '
        Me.btnSelScanner.Location = New System.Drawing.Point(383, 11)
        Me.btnSelScanner.Name = "btnSelScanner"
        Me.btnSelScanner.Size = New System.Drawing.Size(137, 21)
        Me.btnSelScanner.TabIndex = 7
        Me.btnSelScanner.Text = "btnSelScanner"
        Me.btnSelScanner.UseVisualStyleBackColor = True
        '
        'cboPrintMode
        '
        Me.cboPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrintMode.FormattingEnabled = True
        Me.cboPrintMode.Location = New System.Drawing.Point(270, 39)
        Me.cboPrintMode.Name = "cboPrintMode"
        Me.cboPrintMode.Size = New System.Drawing.Size(107, 21)
        Me.cboPrintMode.TabIndex = 3
        '
        'lblPrinter
        '
        Me.lblPrinter.Location = New System.Drawing.Point(163, 42)
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(101, 18)
        Me.lblPrinter.TabIndex = 2
        Me.lblPrinter.Text = "lblPrinter"
        Me.lblPrinter.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblScanner
        '
        Me.lblScanner.Location = New System.Drawing.Point(163, 15)
        Me.lblScanner.Name = "lblScanner"
        Me.lblScanner.Size = New System.Drawing.Size(101, 18)
        Me.lblScanner.TabIndex = 1
        Me.lblScanner.Text = "lblScanner"
        Me.lblScanner.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboScanMode
        '
        Me.cboScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboScanMode.FormattingEnabled = True
        Me.cboScanMode.Location = New System.Drawing.Point(270, 12)
        Me.cboScanMode.Name = "cboScanMode"
        Me.cboScanMode.Size = New System.Drawing.Size(107, 21)
        Me.cboScanMode.TabIndex = 2
        '
        'btnPrintSetup
        '
        Me.btnPrintSetup.Location = New System.Drawing.Point(383, 38)
        Me.btnPrintSetup.Name = "btnPrintSetup"
        Me.btnPrintSetup.Size = New System.Drawing.Size(137, 21)
        Me.btnPrintSetup.TabIndex = 6
        Me.btnPrintSetup.Text = "btnPrintSetup"
        Me.btnPrintSetup.UseVisualStyleBackColor = True
        '
        'nudNCopie
        '
        Me.nudNCopie.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudNCopie.Location = New System.Drawing.Point(322, 96)
        Me.nudNCopie.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudNCopie.Name = "nudNCopie"
        Me.nudNCopie.Size = New System.Drawing.Size(55, 26)
        Me.nudNCopie.TabIndex = 1
        Me.nudNCopie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudNCopie.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCopies
        '
        Me.lblCopies.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCopies.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopies.Location = New System.Drawing.Point(166, 100)
        Me.lblCopies.Name = "lblCopies"
        Me.lblCopies.Size = New System.Drawing.Size(150, 19)
        Me.lblCopies.TabIndex = 12
        Me.lblCopies.Text = "lblCopies"
        Me.lblCopies.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScannerStatusLabel, Me.PrinterStatusLabel, Me.VersionStatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 200)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(532, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 20
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ScannerStatusLabel
        '
        Me.ScannerStatusLabel.Image = Global.iCopy.My.Resources.Resources.scanner
        Me.ScannerStatusLabel.Margin = New System.Windows.Forms.Padding(0, 3, 6, 2)
        Me.ScannerStatusLabel.Name = "ScannerStatusLabel"
        Me.ScannerStatusLabel.Size = New System.Drawing.Size(125, 17)
        Me.ScannerStatusLabel.Text = "ScannerStatusLabel"
        '
        'PrinterStatusLabel
        '
        Me.PrinterStatusLabel.Image = Global.iCopy.My.Resources.Resources.printer
        Me.PrinterStatusLabel.Name = "PrinterStatusLabel"
        Me.PrinterStatusLabel.Size = New System.Drawing.Size(118, 17)
        Me.PrinterStatusLabel.Text = "PrinterStatusLabel"
        '
        'VersionStatusLabel
        '
        Me.VersionStatusLabel.IsLink = True
        Me.VersionStatusLabel.Name = "VersionStatusLabel"
        Me.VersionStatusLabel.Size = New System.Drawing.Size(268, 17)
        Me.VersionStatusLabel.Spring = True
        Me.VersionStatusLabel.Text = "New Version Available!"
        Me.VersionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VersionStatusLabel.Visible = False
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 300
        '
        'llblAbout
        '
        Me.llblAbout.Location = New System.Drawing.Point(371, 177)
        Me.llblAbout.Name = "llblAbout"
        Me.llblAbout.Size = New System.Drawing.Size(149, 13)
        Me.llblAbout.TabIndex = 10
        Me.llblAbout.TabStop = True
        Me.llblAbout.Text = "llblAbout"
        Me.llblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llblSettings
        '
        Me.llblSettings.Location = New System.Drawing.Point(376, 157)
        Me.llblSettings.Name = "llblSettings"
        Me.llblSettings.Size = New System.Drawing.Size(144, 13)
        Me.llblSettings.TabIndex = 9
        Me.llblSettings.TabStop = True
        Me.llblSettings.Text = "llblSettings"
        Me.llblSettings.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnImageSettings
        '
        Me.btnImageSettings.Location = New System.Drawing.Point(383, 96)
        Me.btnImageSettings.Name = "btnImageSettings"
        Me.btnImageSettings.Size = New System.Drawing.Size(137, 42)
        Me.btnImageSettings.TabIndex = 5
        Me.btnImageSettings.Text = "btnImageSettings"
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
        Me.cboPaperSize.Location = New System.Drawing.Point(270, 66)
        Me.cboPaperSize.Name = "cboPaperSize"
        Me.cboPaperSize.Size = New System.Drawing.Size(107, 21)
        Me.cboPaperSize.TabIndex = 21
        '
        'lblPaperSize
        '
        Me.lblPaperSize.Location = New System.Drawing.Point(163, 69)
        Me.lblPaperSize.Name = "lblPaperSize"
        Me.lblPaperSize.Size = New System.Drawing.Size(101, 23)
        Me.lblPaperSize.TabIndex = 22
        Me.lblPaperSize.Text = "lblPaperSize"
        Me.lblPaperSize.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkADF
        '
        Me.chkADF.Location = New System.Drawing.Point(166, 154)
        Me.chkADF.Name = "chkADF"
        Me.chkADF.Size = New System.Drawing.Size(207, 21)
        Me.chkADF.TabIndex = 23
        Me.chkADF.Text = "chkADF"
        Me.chkADF.UseVisualStyleBackColor = True
        '
        'chkDuplex
        '
        Me.chkDuplex.AutoSize = True
        Me.chkDuplex.Location = New System.Drawing.Point(166, 177)
        Me.chkDuplex.Name = "chkDuplex"
        Me.chkDuplex.Size = New System.Drawing.Size(77, 17)
        Me.chkDuplex.TabIndex = 24
        Me.chkDuplex.Text = "chkDuplex"
        Me.chkDuplex.UseVisualStyleBackColor = True
        '
        'chkMultipage
        '
        Me.chkMultipage.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkMultipage.Image = Global.iCopy.My.Resources.Resources.multipage
        Me.chkMultipage.Location = New System.Drawing.Point(12, 151)
        Me.chkMultipage.Name = "chkMultipage"
        Me.chkMultipage.Size = New System.Drawing.Size(42, 42)
        Me.chkMultipage.TabIndex = 26
        Me.chkMultipage.UseVisualStyleBackColor = True
        '
        'chkSaveToFile
        '
        Me.chkSaveToFile.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkSaveToFile.Image = Global.iCopy.My.Resources.Resources.saveToFile
        Me.chkSaveToFile.Location = New System.Drawing.Point(60, 151)
        Me.chkSaveToFile.Name = "chkSaveToFile"
        Me.chkSaveToFile.Size = New System.Drawing.Size(42, 42)
        Me.chkSaveToFile.TabIndex = 26
        Me.chkSaveToFile.UseVisualStyleBackColor = True
        '
        'chkPDF
        '
        Me.chkPDF.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPDF.Image = Global.iCopy.My.Resources.Resources.pdficon_large
        Me.chkPDF.Location = New System.Drawing.Point(108, 151)
        Me.chkPDF.Name = "chkPDF"
        Me.chkPDF.Size = New System.Drawing.Size(42, 42)
        Me.chkPDF.TabIndex = 26
        Me.chkPDF.UseVisualStyleBackColor = True
        '
        'chkPreview
        '
        Me.chkPreview.AutoSize = True
        Me.chkPreview.Location = New System.Drawing.Point(166, 135)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(82, 17)
        Me.chkPreview.TabIndex = 4
        Me.chkPreview.Text = "chkPreview"
        Me.chkPreview.UseVisualStyleBackColor = True
        '
        'mainFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 222)
        Me.Controls.Add(Me.chkPDF)
        Me.Controls.Add(Me.chkSaveToFile)
        Me.Controls.Add(Me.chkMultipage)
        Me.Controls.Add(Me.lblPrinter)
        Me.Controls.Add(Me.lblPaperSize)
        Me.Controls.Add(Me.cboPrintMode)
        Me.Controls.Add(Me.cboPaperSize)
        Me.Controls.Add(Me.cboScanMode)
        Me.Controls.Add(Me.lblScanner)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnImageSettings)
        Me.Controls.Add(Me.chkDuplex)
        Me.Controls.Add(Me.btnSelScanner)
        Me.Controls.Add(Me.llblSettings)
        Me.Controls.Add(Me.lblCopies)
        Me.Controls.Add(Me.btnPrintSetup)
        Me.Controls.Add(Me.llblAbout)
        Me.Controls.Add(Me.nudNCopie)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.chkADF)
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
    Friend WithEvents lblCopies As System.Windows.Forms.Label
    Friend WithEvents cboScanMode As System.Windows.Forms.ComboBox
    Friend WithEvents cboPrintMode As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrinter As System.Windows.Forms.Label
    Friend WithEvents lblScanner As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents llblAbout As System.Windows.Forms.LinkLabel
    Friend WithEvents llblSettings As System.Windows.Forms.LinkLabel
    Friend WithEvents btnImageSettings As System.Windows.Forms.Button
    Friend WithEvents ScanMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ScanToFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScanMultiplePages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboPaperSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblPaperSize As System.Windows.Forms.Label
    Friend WithEvents ScannerStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PrinterStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents VersionStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents chkADF As System.Windows.Forms.CheckBox
    Friend WithEvents chkDuplex As System.Windows.Forms.CheckBox
    Friend WithEvents chkMultipage As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveToFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkPDF As System.Windows.Forms.CheckBox
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
End Class
