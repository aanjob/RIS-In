<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRAdjBJ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRAdjBJ))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBArtCode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtName = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBIsi = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKeterangan = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBMM = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrRichText1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBKota = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTT1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBTotPsg = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBKeterangan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBArtCode, Me.LBArtName, Me.LBIsi, Me.LBPsg, Me.LBKet})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 51.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBArtCode
        '
        Me.LBArtCode.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBArtCode.Dpi = 254.0!
        Me.LBArtCode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBArtCode.LocationFloat = New DevExpress.Utils.PointFloat(33.36365!, 0.0!)
        Me.LBArtCode.Name = "LBArtCode"
        Me.LBArtCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtCode.SizeF = New System.Drawing.SizeF(330.1768!, 51.0!)
        Me.LBArtCode.StylePriority.UseBorders = False
        Me.LBArtCode.StylePriority.UseFont = False
        Me.LBArtCode.Text = "Art Code"
        '
        'LBArtName
        '
        Me.LBArtName.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBArtName.Dpi = 254.0!
        Me.LBArtName.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBArtName.LocationFloat = New DevExpress.Utils.PointFloat(363.5406!, 0.0!)
        Me.LBArtName.Name = "LBArtName"
        Me.LBArtName.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtName.SizeF = New System.Drawing.SizeF(603.9485!, 51.0!)
        Me.LBArtName.StylePriority.UseBorders = False
        Me.LBArtName.StylePriority.UseFont = False
        Me.LBArtName.Text = "Art Name"
        '
        'LBIsi
        '
        Me.LBIsi.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBIsi.Dpi = 254.0!
        Me.LBIsi.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBIsi.LocationFloat = New DevExpress.Utils.PointFloat(967.4894!, 0.0!)
        Me.LBIsi.Name = "LBIsi"
        Me.LBIsi.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBIsi.SizeF = New System.Drawing.SizeF(95.33716!, 51.0!)
        Me.LBIsi.StylePriority.UseBorders = False
        Me.LBIsi.StylePriority.UseFont = False
        Me.LBIsi.StylePriority.UseTextAlignment = False
        Me.LBIsi.Text = "Isi"
        Me.LBIsi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBPsg
        '
        Me.LBPsg.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBPsg.Dpi = 254.0!
        Me.LBPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPsg.LocationFloat = New DevExpress.Utils.PointFloat(1063.826!, 0.0!)
        Me.LBPsg.Name = "LBPsg"
        Me.LBPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPsg.SizeF = New System.Drawing.SizeF(171.792!, 51.0!)
        Me.LBPsg.StylePriority.UseBorders = False
        Me.LBPsg.StylePriority.UseFont = False
        Me.LBPsg.StylePriority.UseTextAlignment = False
        Me.LBPsg.Text = "Qty Psg"
        Me.LBPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBKet
        '
        Me.LBKet.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(1235.618!, 0.0!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SizeF = New System.Drawing.SizeF(807.26!, 51.0!)
        Me.LBKet.StylePriority.UseBorders = False
        Me.LBKet.StylePriority.UseFont = False
        Me.LBKet.Text = "Ket"
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
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.LBKeterangan, Me.LBTanggal, Me.LBKode, Me.XrLabel1, Me.LBPerusahaan, Me.XrLabel2, Me.LBGudang, Me.XrLabel4})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 333.375!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(851.4954!, 186.6459!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(211.5831!, 51.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Keterangan"
        '
        'LBKeterangan
        '
        Me.LBKeterangan.Dpi = 254.0!
        Me.LBKeterangan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKeterangan.LocationFloat = New DevExpress.Utils.PointFloat(1064.078!, 186.6459!)
        Me.LBKeterangan.Name = "LBKeterangan"
        Me.LBKeterangan.SerializableRtfString = resources.GetString("LBKeterangan.SerializableRtfString")
        Me.LBKeterangan.SizeF = New System.Drawing.SizeF(978.7996!, 101.9999!)
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(34.24505!, 103.9999!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(2008.633!, 51.0!)
        Me.LBTanggal.StylePriority.UseFont = False
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(244.4869!, 186.6459!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(510.5623!, 51.0!)
        Me.LBKode.StylePriority.UseFont = False
        Me.LBKode.Text = "Nomor"
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 52.99998!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2010.133!, 51.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Adjustment Barang Jadi"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(2010.133!, 45.0!)
        Me.LBPerusahaan.StylePriority.UseFont = False
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 186.6459!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(211.5831!, 51.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "No. Opname"
        '
        'LBGudang
        '
        Me.LBGudang.Dpi = 254.0!
        Me.LBGudang.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(244.4869!, 237.6458!)
        Me.LBGudang.Name = "LBGudang"
        Me.LBGudang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudang.SizeF = New System.Drawing.SizeF(510.5623!, 51.0!)
        Me.LBGudang.StylePriority.UseFont = False
        Me.LBGudang.Text = "Gudang"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 237.6458!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(211.5831!, 51.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Gudang"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel3, Me.XrLabel5, Me.XrLabel6, Me.XrLabel7, Me.XrLabel8})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 60.71253!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(30.50776!, 55.33117!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2017.662!, 5.381355!)
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(1235.618!, 4.541712!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(807.26!, 51.0!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Ket"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(33.16592!, 4.541668!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(330.1769!, 51.0!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "Art Code"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(363.5406!, 4.541668!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(603.9485!, 51.0!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Art Name"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(967.4894!, 4.541665!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(95.33716!, 51.0!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "Isi"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel8
        '
        Me.XrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(1063.826!, 4.541668!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(171.792!, 51.0!)
        Me.XrLabel8.StylePriority.UseBorders = False
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "Qty Psg"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBUser, Me.XrPageInfo1, Me.XrPageInfo2})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 72.83661!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 24.99987!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(32.74501!, 25.00003!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2019.209!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(222.745!, 24.99987!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandBox1.BorderWidth = 2.0!
        Me.XrCrossBandBox1.Dpi = 254.0!
        Me.XrCrossBandBox1.EndBand = Me.ReportFooter
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(27.45332!, 59.19492!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(27.45332!, 0.0!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.PageHeader
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(27.45332!, 0.0!)
        Me.XrCrossBandBox1.WidthF = 2020.716!
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel17, Me.LBMM, Me.XrRichText1, Me.LBKota, Me.LBTT1, Me.XrLabel15, Me.XrLine2, Me.LBTotPsg})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 386.4476!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(1146.235!, 149.8599!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(408.6952!, 45.1908!)
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "Manager Marketing"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrLabel17.Visible = False
        '
        'LBMM
        '
        Me.LBMM.Dpi = 254.0!
        Me.LBMM.LocationFloat = New DevExpress.Utils.PointFloat(1146.235!, 328.6877!)
        Me.LBMM.Name = "LBMM"
        Me.LBMM.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBMM.SizeF = New System.Drawing.SizeF(408.6952!, 45.1908!)
        Me.LBMM.StylePriority.UseTextAlignment = False
        Me.LBMM.Text = "Manager Marketing"
        Me.LBMM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.LBMM.Visible = False
        '
        'XrRichText1
        '
        Me.XrRichText1.Dpi = 254.0!
        Me.XrRichText1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.XrRichText1.LocationFloat = New DevExpress.Utils.PointFloat(1639.475!, 149.8599!)
        Me.XrRichText1.Name = "XrRichText1"
        Me.XrRichText1.SerializableRtfString = resources.GetString("XrRichText1.SerializableRtfString")
        Me.XrRichText1.SizeF = New System.Drawing.SizeF(408.6951!, 236.5877!)
        Me.XrRichText1.StylePriority.UseFont = False
        Me.XrRichText1.Visible = False
        '
        'LBKota
        '
        Me.LBKota.Dpi = 254.0!
        Me.LBKota.LocationFloat = New DevExpress.Utils.PointFloat(1326.108!, 94.2262!)
        Me.LBKota.Name = "LBKota"
        Me.LBKota.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKota.SizeF = New System.Drawing.SizeF(653.2986!, 45.19083!)
        Me.LBKota.Text = "Kota"
        Me.LBKota.Visible = False
        '
        'LBTT1
        '
        Me.LBTT1.Dpi = 254.0!
        Me.LBTT1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBTT1.LocationFloat = New DevExpress.Utils.PointFloat(651.3962!, 149.8599!)
        Me.LBTT1.Name = "LBTT1"
        Me.LBTT1.SerializableRtfString = resources.GetString("LBTT1.SerializableRtfString")
        Me.LBTT1.SizeF = New System.Drawing.SizeF(408.6951!, 236.5877!)
        Me.LBTT1.StylePriority.UseFont = False
        Me.LBTT1.Visible = False
        '
        'XrLabel15
        '
        Me.XrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(33.36365!, 3.999969!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(1029.463!, 51.0!)
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Total"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(29.54803!, 0.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2013.33!, 5.0!)
        '
        'LBTotPsg
        '
        Me.LBTotPsg.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBTotPsg.Dpi = 254.0!
        Me.LBTotPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotPsg.LocationFloat = New DevExpress.Utils.PointFloat(1063.826!, 4.0!)
        Me.LBTotPsg.Name = "LBTotPsg"
        Me.LBTotPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotPsg.SizeF = New System.Drawing.SizeF(171.792!, 51.0!)
        Me.LBTotPsg.StylePriority.UseBorders = False
        Me.LBTotPsg.StylePriority.UseFont = False
        Me.LBTotPsg.StylePriority.UseTextAlignment = False
        Me.LBTotPsg.Text = "Qty Psg"
        Me.LBTotPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XRAdjBJ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandBox1})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me.LBKeterangan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtCode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBIsi As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents LBTotPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBMM As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrRichText1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBKota As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTT1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKeterangan As DevExpress.XtraReports.UI.XRRichText
End Class
