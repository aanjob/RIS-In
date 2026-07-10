<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRKatalogBBDtl
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLine6 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine5 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBStok = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNama = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.JnsID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.CustID = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine6, Me.XrLine5, Me.LBStok, Me.LBNama, Me.XrLine4, Me.XrLine3, Me.XrLine2, Me.XrLine1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 472.7031!
        Me.Detail.MultiColumn.ColumnWidth = 600.0!
        Me.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLine6
        '
        Me.XrLine6.Dpi = 254.0!
        Me.XrLine6.LineWidth = 3
        Me.XrLine6.LocationFloat = New DevExpress.Utils.PointFloat(6.562519!, 467.7031!)
        Me.XrLine6.Name = "XrLine6"
        Me.XrLine6.SizeF = New System.Drawing.SizeF(600.0!, 5.0!)
        '
        'XrLine5
        '
        Me.XrLine5.Dpi = 254.0!
        Me.XrLine5.LineWidth = 3
        Me.XrLine5.LocationFloat = New DevExpress.Utils.PointFloat(6.854172!, 418.6354!)
        Me.XrLine5.Name = "XrLine5"
        Me.XrLine5.SizeF = New System.Drawing.SizeF(600.0!, 5.0!)
        '
        'LBStok
        '
        Me.LBStok.Dpi = 254.0!
        Me.LBStok.Font = New System.Drawing.Font("Abadi MT Condensed Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LBStok.LocationFloat = New DevExpress.Utils.PointFloat(7.562519!, 424.0678!)
        Me.LBStok.Multiline = True
        Me.LBStok.Name = "LBStok"
        Me.LBStok.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBStok.SizeF = New System.Drawing.SizeF(594.0!, 43.63531!)
        Me.LBStok.StylePriority.UseFont = False
        Me.LBStok.StylePriority.UseTextAlignment = False
        Me.LBStok.Text = "Stok"
        Me.LBStok.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBNama
        '
        Me.LBNama.Dpi = 254.0!
        Me.LBNama.Font = New System.Drawing.Font("Abadi MT Condensed Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.LBNama.LocationFloat = New DevExpress.Utils.PointFloat(7.562519!, 350.0!)
        Me.LBNama.Multiline = True
        Me.LBNama.Name = "LBNama"
        Me.LBNama.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNama.SizeF = New System.Drawing.SizeF(594.0!, 68.63535!)
        Me.LBNama.StylePriority.UseFont = False
        Me.LBNama.StylePriority.UseTextAlignment = False
        Me.LBNama.Text = "Jenis Bahan"
        Me.LBNama.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLine4
        '
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical
        Me.XrLine4.LineWidth = 3
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(601.5626!, 0.0!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(5.291626!, 472.7031!)
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(7.562519!, 345.0001!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(600.0!, 5.0!)
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(6.854172!, 0.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(600.0!, 5.0!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(2.5625!, 1.00002!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(5.291668!, 471.7031!)
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 0.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 0.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'JnsID
        '
        Me.JnsID.Name = "JnsID"
        Me.JnsID.Visible = False
        '
        'CustID
        '
        Me.CustID.Name = "CustID"
        Me.CustID.Visible = False
        '
        'XRKatalogBBDtl
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 0, 0)
        Me.PageHeight = 3300
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.JnsID, Me.CustID})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrLine6 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine5 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBStok As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNama As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents JnsID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents CustID As DevExpress.XtraReports.Parameters.Parameter
End Class
