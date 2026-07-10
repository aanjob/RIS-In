<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRTransBBv1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRTransBBv1))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBKetDtl = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQtyAs = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatAs = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahanAs = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQtyTj = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatTj = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahanTj = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBKetDtl, Me.LBQtyAs, Me.LBSatAs, Me.LBBahanAs, Me.LBNo, Me.LBQtyTj, Me.LBSatTj, Me.LBBahanTj})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 45.19093!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBKetDtl
        '
        Me.LBKetDtl.Dpi = 254.0!
        Me.LBKetDtl.LocationFloat = New DevExpress.Utils.PointFloat(1711.404!, 0.0!)
        Me.LBKetDtl.Name = "LBKetDtl"
        Me.LBKetDtl.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKetDtl.SizeF = New System.Drawing.SizeF(327.5339!, 45.19092!)
        Me.LBKetDtl.StylePriority.UseTextAlignment = False
        Me.LBKetDtl.Text = "Keterangan"
        Me.LBKetDtl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQtyAs
        '
        Me.LBQtyAs.Dpi = 254.0!
        Me.LBQtyAs.LocationFloat = New DevExpress.Utils.PointFloat(756.7626!, 0.0!)
        Me.LBQtyAs.Name = "LBQtyAs"
        Me.LBQtyAs.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQtyAs.SizeF = New System.Drawing.SizeF(149.347!, 45.19093!)
        Me.LBQtyAs.StylePriority.UseTextAlignment = False
        Me.LBQtyAs.Text = "Qty"
        Me.LBQtyAs.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSatAs
        '
        Me.LBSatAs.Dpi = 254.0!
        Me.LBSatAs.LocationFloat = New DevExpress.Utils.PointFloat(587.4717!, 0.0!)
        Me.LBSatAs.Name = "LBSatAs"
        Me.LBSatAs.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatAs.SizeF = New System.Drawing.SizeF(168.3531!, 45.19093!)
        Me.LBSatAs.StylePriority.UseTextAlignment = False
        Me.LBSatAs.Text = "Satuan"
        Me.LBSatAs.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBahanAs
        '
        Me.LBBahanAs.Dpi = 254.0!
        Me.LBBahanAs.LocationFloat = New DevExpress.Utils.PointFloat(101.526!, 0.0!)
        Me.LBBahanAs.Name = "LBBahanAs"
        Me.LBBahanAs.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahanAs.SizeF = New System.Drawing.SizeF(485.5501!, 45.1909!)
        Me.LBBahanAs.StylePriority.UseTextAlignment = False
        Me.LBBahanAs.Text = "Nama Bahan Asal"
        Me.LBBahanAs.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(29.55448!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(71.30391!, 45.19086!)
        Me.LBNo.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        Me.LBNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQtyTj
        '
        Me.LBQtyTj.Dpi = 254.0!
        Me.LBQtyTj.LocationFloat = New DevExpress.Utils.PointFloat(1562.057!, 0.0!)
        Me.LBQtyTj.Name = "LBQtyTj"
        Me.LBQtyTj.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQtyTj.SizeF = New System.Drawing.SizeF(149.347!, 45.19093!)
        Me.LBQtyTj.StylePriority.UseTextAlignment = False
        Me.LBQtyTj.Text = "Qty"
        Me.LBQtyTj.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSatTj
        '
        Me.LBSatTj.Dpi = 254.0!
        Me.LBSatTj.LocationFloat = New DevExpress.Utils.PointFloat(1392.766!, 0.0!)
        Me.LBSatTj.Name = "LBSatTj"
        Me.LBSatTj.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatTj.SizeF = New System.Drawing.SizeF(168.3533!, 45.19093!)
        Me.LBSatTj.StylePriority.UseTextAlignment = False
        Me.LBSatTj.Text = "Satuan"
        Me.LBSatTj.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBahanTj
        '
        Me.LBBahanTj.Dpi = 254.0!
        Me.LBBahanTj.LocationFloat = New DevExpress.Utils.PointFloat(906.903!, 0.0!)
        Me.LBBahanTj.Name = "LBBahanTj"
        Me.LBBahanTj.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahanTj.SizeF = New System.Drawing.SizeF(485.5501!, 45.1909!)
        Me.LBBahanTj.StylePriority.UseTextAlignment = False
        Me.LBBahanTj.Text = "Nama Bahan Tujuan"
        Me.LBBahanTj.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 35.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 55.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel7, Me.XrLabel3, Me.XrLabel5, Me.XrLabel6, Me.XrLabel2, Me.XrLine1, Me.XrLabel12, Me.XrLabel15, Me.XrLabel16, Me.XrLabel18, Me.XrLabel1, Me.LBPerusahaan, Me.LBTanggal, Me.XrLabel4, Me.LBKode, Me.LBGudang, Me.XrLine2})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 339.7644!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(1711.404!, 289.4319!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(327.5339!, 45.19092!)
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "Keterangan"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(906.903!, 289.4319!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(485.5501!, 45.19089!)
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Deskripsi Bahan Tujuan"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(1392.766!, 289.4319!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(168.3533!, 45.19092!)
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "Satuan"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(1562.057!, 289.4319!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(149.347!, 45.19092!)
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Qty"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1052.406!, 189.6506!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(187.8541!, 45.1908!)
        Me.XrLabel2.Text = "Gudang"
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(26.55444!, 283.5936!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(29.5544!, 289.432!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(71.3039!, 45.19086!)
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "No."
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(101.526!, 289.4319!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(485.5501!, 45.19089!)
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Deskripsi Bahan Asal"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(587.4719!, 289.4319!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(168.3532!, 45.19092!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Satuan"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(756.7626!, 289.4319!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(149.347!, 45.19092!)
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Qty"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(26.07294!, 44.48393!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2012.865!, 58.42001!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Transfer Bahan"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(26.07294!, 2.256409!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(733.8341!, 58.42!)
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(26.07294!, 102.9039!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(2012.865!, 47.83665!)
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(26.07294!, 189.8799!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(214.3125!, 45.19083!)
        Me.XrLabel4.Text = "No. Transfer"
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(240.407!, 189.6506!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(711.7291!, 45.19081!)
        Me.LBKode.Text = "Nomor"
        '
        'LBGudang
        '
        Me.LBGudang.Dpi = 254.0!
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(1240.407!, 189.6506!)
        Me.LBGudang.Name = "LBGudang"
        Me.LBGudang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudang.SizeF = New System.Drawing.SizeF(711.7291!, 45.19083!)
        Me.LBGudang.Text = "Gudang"
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(28.05796!, 334.7644!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.LBUser, Me.LBKet, Me.XrLabel17, Me.XrLine3, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 216.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(207.8254!, 168.1633!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(23.72901!, 168.1633!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(246.6175!, 5.164916!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(1669.071!, 127.2535!)
        Me.LBKet.StylePriority.UsePadding = False
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(29.55448!, 5.164916!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(214.3125!, 45.19086!)
        Me.XrLabel17.Text = "Keterangan"
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(28.05796!, 0.0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(23.72901!, 168.1634!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2019.209!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 0.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XRTransBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 1391
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQtyAs As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatAs As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahanAs As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQtyTj As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatTj As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahanTj As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKetDtl As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
End Class
