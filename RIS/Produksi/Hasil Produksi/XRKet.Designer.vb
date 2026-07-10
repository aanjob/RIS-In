<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRKet
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
        Me.LBLine = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBStyle = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHasil = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTKL = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBJam = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrCrossBandLine1 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine4 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine5 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine6 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.Tanggal = New DevExpress.XtraReports.Parameters.Parameter()
        Me.Unit = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBLine, Me.LBStyle, Me.LBHasil, Me.LBTKL, Me.LBJam, Me.XrLine2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 51.75587!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBLine
        '
        Me.LBLine.Dpi = 254.0!
        Me.LBLine.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!)
        Me.LBLine.LocationFloat = New DevExpress.Utils.PointFloat(851.5028!, 11.40634!)
        Me.LBLine.Name = "LBLine"
        Me.LBLine.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBLine.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.LBLine.StylePriority.UseFont = False
        Me.LBLine.StylePriority.UseTextAlignment = False
        Me.LBLine.Text = "Line"
        Me.LBLine.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBStyle
        '
        Me.LBStyle.Dpi = 254.0!
        Me.LBStyle.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!)
        Me.LBStyle.LocationFloat = New DevExpress.Utils.PointFloat(1109.5!, 11.40634!)
        Me.LBStyle.Name = "LBStyle"
        Me.LBStyle.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBStyle.SizeF = New System.Drawing.SizeF(456.2665!, 40.34953!)
        Me.LBStyle.StylePriority.UseFont = False
        Me.LBStyle.StylePriority.UseTextAlignment = False
        Me.LBStyle.Text = "Style"
        Me.LBStyle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBHasil
        '
        Me.LBHasil.Dpi = 254.0!
        Me.LBHasil.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!)
        Me.LBHasil.LocationFloat = New DevExpress.Utils.PointFloat(1570.375!, 11.40634!)
        Me.LBHasil.Name = "LBHasil"
        Me.LBHasil.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHasil.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.LBHasil.StylePriority.UseFont = False
        Me.LBHasil.StylePriority.UseTextAlignment = False
        Me.LBHasil.Text = "Hasil"
        Me.LBHasil.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBTKL
        '
        Me.LBTKL.Dpi = 254.0!
        Me.LBTKL.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!)
        Me.LBTKL.LocationFloat = New DevExpress.Utils.PointFloat(1830.708!, 11.40634!)
        Me.LBTKL.Name = "LBTKL"
        Me.LBTKL.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTKL.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.LBTKL.StylePriority.UseFont = False
        Me.LBTKL.StylePriority.UseTextAlignment = False
        Me.LBTKL.Text = "TKL"
        Me.LBTKL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBJam
        '
        Me.LBJam.Dpi = 254.0!
        Me.LBJam.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!)
        Me.LBJam.LocationFloat = New DevExpress.Utils.PointFloat(2089.708!, 11.40634!)
        Me.LBJam.Name = "LBJam"
        Me.LBJam.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJam.SizeF = New System.Drawing.SizeF(253.9999!, 40.34953!)
        Me.LBJam.StylePriority.UseFont = False
        Me.LBJam.StylePriority.UseTextAlignment = False
        Me.LBJam.Text = "Jam"
        Me.LBJam.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(851.5028!, 0.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(1498.669!, 5.406322!)
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
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel1})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 52.0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(846.2111!, 46.59368!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(1498.669!, 5.406322!)
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(2089.708!, 6.244168!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(253.9999!, 40.34953!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "Jam"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(1830.708!, 6.244168!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "TKL"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(1570.375!, 6.244168!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Hasil"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1109.5!, 6.244169!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(456.2665!, 40.34953!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "Style"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(851.5028!, 6.244178!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(254.0!, 40.34953!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Line"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.None
        Me.XrCrossBandBox1.BorderWidth = 2.0!
        Me.XrCrossBandBox1.Dpi = 254.0!
        Me.XrCrossBandBox1.EndBand = Me.ReportFooter
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(846.2111!, 3.056237!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(846.2111!, 0.08510114!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.ReportHeader
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(846.2111!, 0.08510114!)
        Me.XrCrossBandBox1.WidthF = 1503.961!
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 4.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrCrossBandLine1
        '
        Me.XrCrossBandLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.None
        Me.XrCrossBandLine1.Dpi = 254.0!
        Me.XrCrossBandLine1.EndBand = Me.ReportFooter
        Me.XrCrossBandLine1.EndPointFloat = New DevExpress.Utils.PointFloat(1106.5!, 3.771727!)
        Me.XrCrossBandLine1.LocationFloat = New DevExpress.Utils.PointFloat(1106.5!, 4.335329!)
        Me.XrCrossBandLine1.Name = "XrCrossBandLine1"
        Me.XrCrossBandLine1.StartBand = Me.ReportHeader
        Me.XrCrossBandLine1.StartPointFloat = New DevExpress.Utils.PointFloat(1106.5!, 4.335329!)
        Me.XrCrossBandLine1.WidthF = 3.0!
        '
        'XrCrossBandLine4
        '
        Me.XrCrossBandLine4.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.None
        Me.XrCrossBandLine4.Dpi = 254.0!
        Me.XrCrossBandLine4.EndBand = Me.ReportFooter
        Me.XrCrossBandLine4.EndPointFloat = New DevExpress.Utils.PointFloat(2085.082!, 0.4678324!)
        Me.XrCrossBandLine4.LocationFloat = New DevExpress.Utils.PointFloat(2085.082!, 4.799876!)
        Me.XrCrossBandLine4.Name = "XrCrossBandLine4"
        Me.XrCrossBandLine4.StartBand = Me.ReportHeader
        Me.XrCrossBandLine4.StartPointFloat = New DevExpress.Utils.PointFloat(2085.082!, 4.799876!)
        Me.XrCrossBandLine4.WidthF = 3.0!
        '
        'XrCrossBandLine5
        '
        Me.XrCrossBandLine5.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.None
        Me.XrCrossBandLine5.Dpi = 254.0!
        Me.XrCrossBandLine5.EndBand = Me.ReportFooter
        Me.XrCrossBandLine5.EndPointFloat = New DevExpress.Utils.PointFloat(1826.158!, 0.7344694!)
        Me.XrCrossBandLine5.LocationFloat = New DevExpress.Utils.PointFloat(1826.158!, 5.066494!)
        Me.XrCrossBandLine5.Name = "XrCrossBandLine5"
        Me.XrCrossBandLine5.StartBand = Me.ReportHeader
        Me.XrCrossBandLine5.StartPointFloat = New DevExpress.Utils.PointFloat(1826.158!, 5.066494!)
        Me.XrCrossBandLine5.WidthF = 3.0!
        '
        'XrCrossBandLine6
        '
        Me.XrCrossBandLine6.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.None
        Me.XrCrossBandLine6.Dpi = 254.0!
        Me.XrCrossBandLine6.EndBand = Me.ReportFooter
        Me.XrCrossBandLine6.EndPointFloat = New DevExpress.Utils.PointFloat(1565.767!, 0.7344694!)
        Me.XrCrossBandLine6.LocationFloat = New DevExpress.Utils.PointFloat(1565.767!, 6.333116!)
        Me.XrCrossBandLine6.Name = "XrCrossBandLine6"
        Me.XrCrossBandLine6.StartBand = Me.ReportHeader
        Me.XrCrossBandLine6.StartPointFloat = New DevExpress.Utils.PointFloat(1565.767!, 6.333116!)
        Me.XrCrossBandLine6.WidthF = 3.0!
        '
        'Tanggal
        '
        Me.Tanggal.Name = "Tanggal"
        Me.Tanggal.Type = GetType(Date)
        Me.Tanggal.Visible = False
        '
        'Unit
        '
        Me.Unit.Name = "Unit"
        Me.Unit.Visible = False
        '
        'XRKet
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.ReportFooter})
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandLine6, Me.XrCrossBandLine5, Me.XrCrossBandLine4, Me.XrCrossBandLine1, Me.XrCrossBandBox1})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        Me.PageHeight = 2159
        Me.PageWidth = 3300
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.Tanggal, Me.Unit})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents LBLine As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBStyle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHasil As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTKL As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBJam As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrCrossBandLine1 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine4 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine5 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine6 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents Tanggal As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents Unit As DevExpress.XtraReports.Parameters.Parameter
End Class
