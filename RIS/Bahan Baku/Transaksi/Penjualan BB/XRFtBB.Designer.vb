<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRFtBB
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRFtBB))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBDisc = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQty = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatuan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHarSat = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHarAkhir = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.LBAlamat = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHeader = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBJualID = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBDueDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotAkhir = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDiscH = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDiscDet = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotSbDisc = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBDisc, Me.LBNo, Me.LBQty, Me.LBSatuan, Me.LBBahan, Me.LBHarSat, Me.LBHarAkhir})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 51.00002!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBDisc
        '
        Me.LBDisc.Dpi = 254.0!
        Me.LBDisc.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBDisc.LocationFloat = New DevExpress.Utils.PointFloat(1416.552!, 3.0!)
        Me.LBDisc.Name = "LBDisc"
        Me.LBDisc.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDisc.SizeF = New System.Drawing.SizeF(291.042!, 48.0!)
        Me.LBDisc.StylePriority.UseFont = False
        Me.LBDisc.StylePriority.UseTextAlignment = False
        Me.LBDisc.Text = "Disc"
        Me.LBDisc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(126.3572!, 3.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(88.99149!, 48.0!)
        Me.LBNo.StylePriority.UseFont = False
        Me.LBNo.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        Me.LBNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQty
        '
        Me.LBQty.Dpi = 254.0!
        Me.LBQty.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBQty.LocationFloat = New DevExpress.Utils.PointFloat(939.3994!, 2.999978!)
        Me.LBQty.Name = "LBQty"
        Me.LBQty.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQty.SizeF = New System.Drawing.SizeF(129.1457!, 48.0!)
        Me.LBQty.StylePriority.UseFont = False
        Me.LBQty.StylePriority.UseTextAlignment = False
        Me.LBQty.Text = "Qty"
        Me.LBQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSatuan
        '
        Me.LBSatuan.Dpi = 254.0!
        Me.LBSatuan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBSatuan.LocationFloat = New DevExpress.Utils.PointFloat(1068.545!, 3.000019!)
        Me.LBSatuan.Name = "LBSatuan"
        Me.LBSatuan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatuan.SizeF = New System.Drawing.SizeF(128.6654!, 48.0!)
        Me.LBSatuan.StylePriority.UseFont = False
        Me.LBSatuan.StylePriority.UseTextAlignment = False
        Me.LBSatuan.Text = "Satuan"
        Me.LBSatuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBahan
        '
        Me.LBBahan.Dpi = 254.0!
        Me.LBBahan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBBahan.LocationFloat = New DevExpress.Utils.PointFloat(224.7317!, 2.999978!)
        Me.LBBahan.Name = "LBBahan"
        Me.LBBahan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahan.SizeF = New System.Drawing.SizeF(713.668!, 48.0!)
        Me.LBBahan.StylePriority.UseFont = False
        Me.LBBahan.StylePriority.UseTextAlignment = False
        Me.LBBahan.Text = "Nama Bahan"
        Me.LBBahan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBHarSat
        '
        Me.LBHarSat.Dpi = 254.0!
        Me.LBHarSat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBHarSat.LocationFloat = New DevExpress.Utils.PointFloat(1199.21!, 2.999978!)
        Me.LBHarSat.Name = "LBHarSat"
        Me.LBHarSat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHarSat.SizeF = New System.Drawing.SizeF(217.342!, 48.0!)
        Me.LBHarSat.StylePriority.UseFont = False
        Me.LBHarSat.StylePriority.UseTextAlignment = False
        Me.LBHarSat.Text = "Harga Satuan"
        Me.LBHarSat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBHarAkhir
        '
        Me.LBHarAkhir.Dpi = 254.0!
        Me.LBHarAkhir.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBHarAkhir.LocationFloat = New DevExpress.Utils.PointFloat(1713.962!, 3.0!)
        Me.LBHarAkhir.Name = "LBHarAkhir"
        Me.LBHarAkhir.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHarAkhir.SizeF = New System.Drawing.SizeF(354.6637!, 48.0!)
        Me.LBHarAkhir.StylePriority.UseFont = False
        Me.LBHarAkhir.StylePriority.UseTextAlignment = False
        Me.LBHarAkhir.Text = "Jumlah"
        Me.LBHarAkhir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBAlamat, Me.LBCust, Me.LBHeader, Me.LBJualID})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 392.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'LBAlamat
        '
        Me.LBAlamat.Dpi = 254.0!
        Me.LBAlamat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBAlamat.LocationFloat = New DevExpress.Utils.PointFloat(1290.941!, 164.0082!)
        Me.LBAlamat.Name = "LBAlamat"
        Me.LBAlamat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBAlamat.SerializableRtfString = resources.GetString("LBAlamat.SerializableRtfString")
        Me.LBAlamat.SizeF = New System.Drawing.SizeF(708.177!, 119.316!)
        Me.LBAlamat.StylePriority.UseFont = False
        Me.LBAlamat.StylePriority.UsePadding = False
        '
        'LBCust
        '
        Me.LBCust.Dpi = 254.0!
        Me.LBCust.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBCust.LocationFloat = New DevExpress.Utils.PointFloat(1290.941!, 118.5046!)
        Me.LBCust.Name = "LBCust"
        Me.LBCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCust.SizeF = New System.Drawing.SizeF(708.177!, 45.19092!)
        Me.LBCust.StylePriority.UseFont = False
        Me.LBCust.Text = "Customer"
        '
        'LBHeader
        '
        Me.LBHeader.Dpi = 254.0!
        Me.LBHeader.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBHeader.LocationFloat = New DevExpress.Utils.PointFloat(1300.0!, 34.00002!)
        Me.LBHeader.Name = "LBHeader"
        Me.LBHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHeader.SizeF = New System.Drawing.SizeF(605.2819!, 58.42001!)
        Me.LBHeader.StylePriority.UseFont = False
        Me.LBHeader.StylePriority.UseTextAlignment = False
        Me.LBHeader.Text = "BAHAN"
        Me.LBHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBJualID
        '
        Me.LBJualID.Dpi = 254.0!
        Me.LBJualID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBJualID.LocationFloat = New DevExpress.Utils.PointFloat(338.2193!, 172.0!)
        Me.LBJualID.Name = "LBJualID"
        Me.LBJualID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJualID.SizeF = New System.Drawing.SizeF(711.7291!, 48.0!)
        Me.LBJualID.StylePriority.UseFont = False
        Me.LBJualID.Text = "Nomor"
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 0.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.LBDueDate, Me.LBTanggal, Me.LBTotAkhir, Me.LBTotDiscH, Me.LBTotDiscDet, Me.LBTotSbDisc, Me.LBKet})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 702.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(887.1042!, 535.9776!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(277.8125!, 47.83667!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBDueDate
        '
        Me.LBDueDate.Dpi = 254.0!
        Me.LBDueDate.LocationFloat = New DevExpress.Utils.PointFloat(1786.839!, 535.9776!)
        Me.LBDueDate.Name = "LBDueDate"
        Me.LBDueDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDueDate.SizeF = New System.Drawing.SizeF(288.7866!, 47.83661!)
        Me.LBDueDate.StylePriority.UseTextAlignment = False
        Me.LBDueDate.Text = "Due Date"
        Me.LBDueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(232.7317!, 264.7378!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(606.2866!, 47.83661!)
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBTotAkhir
        '
        Me.LBTotAkhir.Dpi = 254.0!
        Me.LBTotAkhir.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotAkhir.LocationFloat = New DevExpress.Utils.PointFloat(1713.962!, 334.1129!)
        Me.LBTotAkhir.Name = "LBTotAkhir"
        Me.LBTotAkhir.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotAkhir.SizeF = New System.Drawing.SizeF(354.6637!, 48.0!)
        Me.LBTotAkhir.StylePriority.UseFont = False
        Me.LBTotAkhir.StylePriority.UseTextAlignment = False
        Me.LBTotAkhir.Text = "Tot Akhir"
        Me.LBTotAkhir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotDiscH
        '
        Me.LBTotDiscH.Dpi = 254.0!
        Me.LBTotDiscH.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDiscH.LocationFloat = New DevExpress.Utils.PointFloat(1713.962!, 178.3629!)
        Me.LBTotDiscH.Name = "LBTotDiscH"
        Me.LBTotDiscH.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDiscH.SizeF = New System.Drawing.SizeF(354.6637!, 48.0!)
        Me.LBTotDiscH.StylePriority.UseFont = False
        Me.LBTotDiscH.StylePriority.UseTextAlignment = False
        Me.LBTotDiscH.Text = "Tot Disc Header"
        Me.LBTotDiscH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotDiscDet
        '
        Me.LBTotDiscDet.Dpi = 254.0!
        Me.LBTotDiscDet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDiscDet.LocationFloat = New DevExpress.Utils.PointFloat(1713.962!, 101.4874!)
        Me.LBTotDiscDet.Name = "LBTotDiscDet"
        Me.LBTotDiscDet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDiscDet.SizeF = New System.Drawing.SizeF(354.6637!, 48.0!)
        Me.LBTotDiscDet.StylePriority.UseFont = False
        Me.LBTotDiscDet.StylePriority.UseTextAlignment = False
        Me.LBTotDiscDet.Text = "Tot Disc Det"
        Me.LBTotDiscDet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotSbDisc
        '
        Me.LBTotSbDisc.Dpi = 254.0!
        Me.LBTotSbDisc.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotSbDisc.LocationFloat = New DevExpress.Utils.PointFloat(1713.962!, 23.0!)
        Me.LBTotSbDisc.Name = "LBTotSbDisc"
        Me.LBTotSbDisc.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotSbDisc.SizeF = New System.Drawing.SizeF(354.6637!, 48.0!)
        Me.LBTotSbDisc.StylePriority.UseFont = False
        Me.LBTotSbDisc.StylePriority.UseTextAlignment = False
        Me.LBTotSbDisc.Text = "Tot Sb Disc"
        Me.LBTotSbDisc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(94.35721!, 35.529!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(672.4347!, 151.066!)
        Me.LBKet.StylePriority.UseFont = False
        '
        'XRFtBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter, Me.PageFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        Me.PageHeight = 2030
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBHarAkhir As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBAlamat As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBHarSat As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatuan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQty As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHeader As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBJualID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDisc As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBTotAkhir As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDiscH As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDiscDet As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotSbDisc As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDueDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
End Class
