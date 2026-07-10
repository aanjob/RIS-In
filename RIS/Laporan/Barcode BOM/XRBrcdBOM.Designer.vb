<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRBrcdBOM
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
        Dim QrCodeGenerator1 As DevExpress.XtraPrinting.BarCode.QRCodeGenerator = New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBNama = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBarcode = New DevExpress.XtraReports.UI.XRLabel()
        Me.BCQrCode = New DevExpress.XtraReports.UI.XRBarCode()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.BBID = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBNama, Me.LBBarcode, Me.BCQrCode})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 205.5001!
        Me.Detail.KeepTogether = True
        Me.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBNama
        '
        Me.LBNama.Dpi = 254.0!
        Me.LBNama.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LBNama.LocationFloat = New DevExpress.Utils.PointFloat(26.00002!, 141.6284!)
        Me.LBNama.Name = "LBNama"
        Me.LBNama.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNama.SizeF = New System.Drawing.SizeF(162.7979!, 60.39574!)
        Me.LBNama.StylePriority.UseFont = False
        Me.LBNama.StylePriority.UseTextAlignment = False
        Me.LBNama.Text = "LBNama"
        Me.LBNama.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBBarcode
        '
        Me.LBBarcode.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBarcode.Dpi = 254.0!
        Me.LBBarcode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBBarcode.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.LBBarcode.Name = "LBBarcode"
        Me.LBBarcode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBarcode.SizeF = New System.Drawing.SizeF(26.16666!, 45.19093!)
        Me.LBBarcode.StylePriority.UseBorders = False
        Me.LBBarcode.StylePriority.UseFont = False
        Me.LBBarcode.StylePriority.UseTextAlignment = False
        Me.LBBarcode.Text = "NIK"
        Me.LBBarcode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.LBBarcode.Visible = False
        '
        'BCQrCode
        '
        Me.BCQrCode.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.BCQrCode.AutoModule = True
        Me.BCQrCode.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.BCQrCode.Dpi = 254.0!
        Me.BCQrCode.LocationFloat = New DevExpress.Utils.PointFloat(29.00002!, 0.0!)
        Me.BCQrCode.Module = 5.08!
        Me.BCQrCode.Name = "BCQrCode"
        Me.BCQrCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BCQrCode.SizeF = New System.Drawing.SizeF(162.7979!, 141.6284!)
        Me.BCQrCode.StylePriority.UseBorders = False
        Me.BCQrCode.StylePriority.UsePadding = False
        Me.BCQrCode.StylePriority.UseTextAlignment = False
        QrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.[Byte]
        QrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H
        QrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version1
        Me.BCQrCode.Symbology = QrCodeGenerator1
        Me.BCQrCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
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
        'BBID
        '
        Me.BBID.Name = "BBID"
        Me.BBID.Visible = False
        '
        'XRBrcdBOM
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        Me.PageHeight = 200
        Me.PageWidth = 200
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.BBID})
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
    Friend WithEvents BCQrCode As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents LBBarcode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNama As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents BBID As DevExpress.XtraReports.Parameters.Parameter
End Class
