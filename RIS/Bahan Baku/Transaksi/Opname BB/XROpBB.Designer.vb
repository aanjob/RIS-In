<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XROpBB
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
        Me.LBData = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatuan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBBID = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.LBTotData = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBJenis = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrCrossBandLine1 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine2 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine3 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine4 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBtNum = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBData, Me.LBSatuan, Me.LBBahan, Me.LBBBID, Me.LBBtNum})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 51.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBData
        '
        Me.LBData.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBData.Dpi = 254.0!
        Me.LBData.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBData.LocationFloat = New DevExpress.Utils.PointFloat(1489.922!, 0.0!)
        Me.LBData.Name = "LBData"
        Me.LBData.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBData.SizeF = New System.Drawing.SizeF(306.5198!, 51.0!)
        Me.LBData.StylePriority.UseBorders = False
        Me.LBData.StylePriority.UseFont = False
        Me.LBData.StylePriority.UseTextAlignment = False
        Me.LBData.Text = "Data"
        Me.LBData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSatuan
        '
        Me.LBSatuan.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBSatuan.Dpi = 254.0!
        Me.LBSatuan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBSatuan.LocationFloat = New DevExpress.Utils.PointFloat(1347.394!, 0.0!)
        Me.LBSatuan.Name = "LBSatuan"
        Me.LBSatuan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatuan.SizeF = New System.Drawing.SizeF(137.2654!, 51.0!)
        Me.LBSatuan.StylePriority.UseBorders = False
        Me.LBSatuan.StylePriority.UseFont = False
        Me.LBSatuan.StylePriority.UseTextAlignment = False
        Me.LBSatuan.Text = "Satuan"
        Me.LBSatuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBahan
        '
        Me.LBBahan.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBahan.Dpi = 254.0!
        Me.LBBahan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBBahan.LocationFloat = New DevExpress.Utils.PointFloat(548.7697!, 0.0!)
        Me.LBBahan.Name = "LBBahan"
        Me.LBBahan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahan.SizeF = New System.Drawing.SizeF(795.3624!, 51.0!)
        Me.LBBahan.StylePriority.UseBorders = False
        Me.LBBahan.StylePriority.UseFont = False
        Me.LBBahan.Text = "Bahan"
        '
        'LBBBID
        '
        Me.LBBBID.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBBID.Dpi = 254.0!
        Me.LBBBID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBBBID.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 0.0!)
        Me.LBBBID.Name = "LBBBID"
        Me.LBBBID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBBID.SizeF = New System.Drawing.SizeF(253.6!, 51.0!)
        Me.LBBBID.StylePriority.UseBorders = False
        Me.LBBBID.StylePriority.UseFont = False
        Me.LBBBID.Text = "Bahan ID"
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
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTanggal, Me.LBKode, Me.XrLabel1, Me.LBPerusahaan, Me.XrLabel2, Me.LBGudang, Me.XrLabel4})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 336.0208!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(25.29159!, 104.0!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(2025.089!, 51.0!)
        Me.LBTanggal.StylePriority.UseFont = False
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(242.854!, 186.6459!)
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
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(25.29159!, 53.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2025.089!, 51.00001!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Stok Opname Bahan Baku"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(25.29159!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(2025.088!, 45.0!)
        Me.LBPerusahaan.StylePriority.UseFont = False
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 186.6459!)
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
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(242.854!, 237.6458!)
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
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 237.6458!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(211.5831!, 51.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Gudang"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLine1, Me.XrLabel5, Me.XrLabel6, Me.XrLabel7, Me.XrLabel8, Me.XrLabel9})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 58.15003!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 53.15003!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2006.732!, 5.0!)
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(30.58325!, 4.00006!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(256.2488!, 51.0!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "ID Bahan"
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(548.7697!, 3.999998!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(798.3424!, 50.99999!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = "Deskripsi Bahan"
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(1347.112!, 4.00006!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(141.681!, 51.0!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "Satuan"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(1488.793!, 4.00006!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(309.8945!, 51.0!)
        Me.XrLabel8.StylePriority.UseBorders = False
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "Data"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel9
        '
        Me.XrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(1798.688!, 4.00006!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(231.1803!, 51.0!)
        Me.XrLabel9.StylePriority.UseBorders = False
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "Fisik"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBUser, Me.XrPageInfo2, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 84.40298!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 36.56623!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(220.5833!, 36.56623!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(30.58326!, 36.5664!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2019.209!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.BorderWidth = 2.0!
        Me.XrCrossBandBox1.Dpi = 254.0!
        Me.XrCrossBandBox1.EndBand = Me.GroupFooter1
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(25.29159!, 62.63911!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(25.29159!, 0.0!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.PageHeader
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(25.29159!, 0.0!)
        Me.XrCrossBandBox1.WidthF = 2012.023!
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTotData, Me.XrLine4, Me.XrLabel16})
        Me.GroupFooter1.Dpi = 254.0!
        Me.GroupFooter1.HeightF = 64.98174!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'LBTotData
        '
        Me.LBTotData.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBTotData.Dpi = 254.0!
        Me.LBTotData.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotData.LocationFloat = New DevExpress.Utils.PointFloat(1489.922!, 5.000043!)
        Me.LBTotData.Name = "LBTotData"
        Me.LBTotData.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotData.SizeF = New System.Drawing.SizeF(306.5199!, 50.99999!)
        Me.LBTotData.StylePriority.UseBorders = False
        Me.LBTotData.StylePriority.UseFont = False
        Me.LBTotData.StylePriority.UseTextAlignment = False
        Me.LBTotData.Text = "Data"
        Me.LBTotData.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine4
        '
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LineWidth = 3
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(29.13409!, 0.0!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(2006.732!, 5.0!)
        '
        'XrLabel16
        '
        Me.XrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(30.58327!, 5.000043!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(1454.076!, 50.99999!)
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Total"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine3, Me.XrLine2, Me.LBJenis})
        Me.GroupHeader1.Dpi = 254.0!
        Me.GroupHeader1.HeightF = 62.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(24.99999!, 55.99995!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2012.023!, 5.0!)
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(25.2916!, 0.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2012.023!, 5.0!)
        '
        'LBJenis
        '
        Me.LBJenis.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBJenis.Dpi = 254.0!
        Me.LBJenis.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBJenis.LocationFloat = New DevExpress.Utils.PointFloat(30.58325!, 4.999986!)
        Me.LBJenis.Name = "LBJenis"
        Me.LBJenis.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJenis.SizeF = New System.Drawing.SizeF(1999.285!, 51.0!)
        Me.LBJenis.StylePriority.UseBorders = False
        Me.LBJenis.StylePriority.UseFont = False
        Me.LBJenis.Text = "Jenis"
        '
        'XrCrossBandLine1
        '
        Me.XrCrossBandLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine1.Dpi = 254.0!
        Me.XrCrossBandLine1.EndBand = Me.GroupFooter1
        Me.XrCrossBandLine1.EndPointFloat = New DevExpress.Utils.PointFloat(284.1832!, 0.0!)
        Me.XrCrossBandLine1.LocationFloat = New DevExpress.Utils.PointFloat(284.1832!, 57.12099!)
        Me.XrCrossBandLine1.Name = "XrCrossBandLine1"
        Me.XrCrossBandLine1.StartBand = Me.GroupHeader1
        Me.XrCrossBandLine1.StartPointFloat = New DevExpress.Utils.PointFloat(284.1832!, 57.12099!)
        Me.XrCrossBandLine1.WidthF = 3.261993!
        '
        'XrCrossBandLine2
        '
        Me.XrCrossBandLine2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine2.Dpi = 254.0!
        Me.XrCrossBandLine2.EndBand = Me.GroupFooter1
        Me.XrCrossBandLine2.EndPointFloat = New DevExpress.Utils.PointFloat(1344.132!, 2.392137!)
        Me.XrCrossBandLine2.LocationFloat = New DevExpress.Utils.PointFloat(1344.132!, 57.12099!)
        Me.XrCrossBandLine2.Name = "XrCrossBandLine2"
        Me.XrCrossBandLine2.StartBand = Me.GroupHeader1
        Me.XrCrossBandLine2.StartPointFloat = New DevExpress.Utils.PointFloat(1344.132!, 57.12099!)
        Me.XrCrossBandLine2.WidthF = 3.262085!
        '
        'XrCrossBandLine3
        '
        Me.XrCrossBandLine3.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine3.Dpi = 254.0!
        Me.XrCrossBandLine3.EndBand = Me.GroupFooter1
        Me.XrCrossBandLine3.EndPointFloat = New DevExpress.Utils.PointFloat(1486.66!, 57.8459!)
        Me.XrCrossBandLine3.LocationFloat = New DevExpress.Utils.PointFloat(1486.66!, 58.71575!)
        Me.XrCrossBandLine3.Name = "XrCrossBandLine3"
        Me.XrCrossBandLine3.StartBand = Me.GroupHeader1
        Me.XrCrossBandLine3.StartPointFloat = New DevExpress.Utils.PointFloat(1486.66!, 58.71575!)
        Me.XrCrossBandLine3.WidthF = 3.261963!
        '
        'XrCrossBandLine4
        '
        Me.XrCrossBandLine4.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine4.Dpi = 254.0!
        Me.XrCrossBandLine4.EndBand = Me.GroupFooter1
        Me.XrCrossBandLine4.EndPointFloat = New DevExpress.Utils.PointFloat(1796.441!, 62.73888!)
        Me.XrCrossBandLine4.LocationFloat = New DevExpress.Utils.PointFloat(1796.441!, 57.12099!)
        Me.XrCrossBandLine4.Name = "XrCrossBandLine4"
        Me.XrCrossBandLine4.StartBand = Me.GroupHeader1
        Me.XrCrossBandLine4.StartPointFloat = New DevExpress.Utils.PointFloat(1796.441!, 57.12099!)
        Me.XrCrossBandLine4.WidthF = 3.261963!
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(286.8321!, 4.000079!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(261.9376!, 51.0!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Batch Number"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBtNum
        '
        Me.LBBtNum.Borders = DevExpress.XtraPrinting.BorderSide.Right
        Me.LBBtNum.Dpi = 254.0!
        Me.LBBtNum.LocationFloat = New DevExpress.Utils.PointFloat(286.8321!, 0.0!)
        Me.LBBtNum.Name = "LBBtNum"
        Me.LBBtNum.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBtNum.SizeF = New System.Drawing.SizeF(261.9376!, 51.0!)
        Me.LBBtNum.StylePriority.UseBorders = False
        Me.LBBtNum.StylePriority.UseTextAlignment = False
        Me.LBBtNum.Text = "Batch Number"
        Me.LBBtNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XROpBBv2
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.PageFooter, Me.GroupHeader1, Me.GroupFooter1})
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandLine4, Me.XrCrossBandLine3, Me.XrCrossBandLine2, Me.XrCrossBandLine1, Me.XrCrossBandBox1})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(24, 35, 35, 55)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
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
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBData As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatuan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBBID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBJenis As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBTotData As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandLine1 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine2 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine3 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine4 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBtNum As DevExpress.XtraReports.UI.XRLabel
End Class
