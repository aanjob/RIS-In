<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRAdjBB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRAdjBB))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBBtNum = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBBID = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatuan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQty = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.LBKeterangan = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.LBTT1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBKota = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrRichText1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBTotQty = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        Me.XrCrossBandLine1 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine2 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine3 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine4 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine5 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine6 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSaldo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotSaldo = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBKeterangan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSaldo, Me.LBBtNum, Me.LBKet, Me.LBBBID, Me.LBBahan, Me.LBSatuan, Me.LBQty})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 47.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBtNum
        '
        Me.LBBtNum.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBtNum.Dpi = 254.0!
        Me.LBBtNum.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBBtNum.LocationFloat = New DevExpress.Utils.PointFloat(278.3348!, 0.0!)
        Me.LBBtNum.Name = "LBBtNum"
        Me.LBBtNum.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBtNum.SizeF = New System.Drawing.SizeF(258.1331!, 47.0!)
        Me.LBBtNum.StylePriority.UseBorders = False
        Me.LBBtNum.StylePriority.UseFont = False
        Me.LBBtNum.StylePriority.UseTextAlignment = False
        Me.LBBtNum.Text = "Batch Number"
        Me.LBBtNum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBKet
        '
        Me.LBKet.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(1754.753!, 0.0!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SizeF = New System.Drawing.SizeF(284.087!, 47.0!)
        Me.LBKet.StylePriority.UseBorders = False
        Me.LBKet.StylePriority.UseFont = False
        Me.LBKet.StylePriority.UseTextAlignment = False
        Me.LBKet.Text = "Keterangan"
        Me.LBKet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBBID
        '
        Me.LBBBID.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBBID.Dpi = 254.0!
        Me.LBBBID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBBBID.LocationFloat = New DevExpress.Utils.PointFloat(41.49999!, 0.0!)
        Me.LBBBID.Name = "LBBBID"
        Me.LBBBID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBBID.SizeF = New System.Drawing.SizeF(232.7467!, 47.0!)
        Me.LBBBID.StylePriority.UseBorders = False
        Me.LBBBID.StylePriority.UseFont = False
        Me.LBBBID.Text = "Bahan ID"
        '
        'LBBahan
        '
        Me.LBBahan.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBBahan.Dpi = 254.0!
        Me.LBBahan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBBahan.LocationFloat = New DevExpress.Utils.PointFloat(539.4678!, 0.0!)
        Me.LBBahan.Name = "LBBahan"
        Me.LBBahan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahan.SizeF = New System.Drawing.SizeF(692.6422!, 47.0!)
        Me.LBBahan.StylePriority.UseBorders = False
        Me.LBBahan.StylePriority.UseFont = False
        Me.LBBahan.Text = "Bahan"
        '
        'LBSatuan
        '
        Me.LBSatuan.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBSatuan.Dpi = 254.0!
        Me.LBSatuan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBSatuan.LocationFloat = New DevExpress.Utils.PointFloat(1236.029!, 0.0!)
        Me.LBSatuan.Name = "LBSatuan"
        Me.LBSatuan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatuan.SizeF = New System.Drawing.SizeF(141.1176!, 47.0!)
        Me.LBSatuan.StylePriority.UseBorders = False
        Me.LBSatuan.StylePriority.UseFont = False
        Me.LBSatuan.StylePriority.UseTextAlignment = False
        Me.LBSatuan.Text = "Satuan"
        Me.LBSatuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQty
        '
        Me.LBQty.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBQty.Dpi = 254.0!
        Me.LBQty.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBQty.LocationFloat = New DevExpress.Utils.PointFloat(1381.161!, 0.0!)
        Me.LBQty.Name = "LBQty"
        Me.LBQty.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQty.SizeF = New System.Drawing.SizeF(147.6011!, 47.0!)
        Me.LBQty.StylePriority.UseBorders = False
        Me.LBQty.StylePriority.UseFont = False
        Me.LBQty.StylePriority.UseTextAlignment = False
        Me.LBQty.Text = "Qty"
        Me.LBQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBKeterangan, Me.XrLabel10, Me.XrLabel4, Me.LBGudang, Me.XrLabel2, Me.LBPerusahaan, Me.XrLabel1, Me.LBKode, Me.LBTanggal})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 325.4375!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LBKeterangan
        '
        Me.LBKeterangan.Dpi = 254.0!
        Me.LBKeterangan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKeterangan.LocationFloat = New DevExpress.Utils.PointFloat(1089.145!, 186.6459!)
        Me.LBKeterangan.Name = "LBKeterangan"
        Me.LBKeterangan.SerializableRtfString = resources.GetString("LBKeterangan.SerializableRtfString")
        Me.LBKeterangan.SizeF = New System.Drawing.SizeF(954.9869!, 101.9999!)
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(876.7502!, 186.6459!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(211.5831!, 51.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "Keterangan"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(34.00002!, 237.6458!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(264.5544!, 51.00003!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Gudang"
        '
        'LBGudang
        '
        Me.LBGudang.Dpi = 254.0!
        Me.LBGudang.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(298.7419!, 237.6458!)
        Me.LBGudang.Name = "LBGudang"
        Me.LBGudang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudang.SizeF = New System.Drawing.SizeF(510.5623!, 51.0!)
        Me.LBGudang.StylePriority.UseFont = False
        Me.LBGudang.Text = "Gudang"
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(34.00002!, 186.6459!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(264.0592!, 51.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "No. Adjustment"
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(34.00001!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(2010.133!, 45.0!)
        Me.LBPerusahaan.StylePriority.UseFont = False
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(34.00001!, 45.0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2010.133!, 51.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Adjustment Bahan Baku"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(298.7419!, 186.6459!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(510.5623!, 51.0!)
        Me.LBKode.StylePriority.UseFont = False
        Me.LBKode.Text = "Nomor"
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(35.5!, 96.0!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(2008.633!, 51.0!)
        Me.LBTanggal.StylePriority.UseFont = False
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.XrPageInfo1, Me.LBUser})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 72.83656!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(231.5!, 24.99985!)
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
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(41.49999!, 24.99998!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2019.209!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(41.49999!, 24.99985!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTotSaldo, Me.LBTT1, Me.LBKota, Me.XrRichText1, Me.LBTotQty, Me.XrLine2, Me.XrLabel9})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 383.5961!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'LBTT1
        '
        Me.LBTT1.Dpi = 254.0!
        Me.LBTT1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBTT1.LocationFloat = New DevExpress.Utils.PointFloat(1120.067!, 147.0084!)
        Me.LBTT1.Name = "LBTT1"
        Me.LBTT1.SerializableRtfString = resources.GetString("LBTT1.SerializableRtfString")
        Me.LBTT1.SizeF = New System.Drawing.SizeF(408.6951!, 236.5877!)
        Me.LBTT1.StylePriority.UseFont = False
        Me.LBTT1.Visible = False
        '
        'LBKota
        '
        Me.LBKota.Dpi = 254.0!
        Me.LBKota.LocationFloat = New DevExpress.Utils.PointFloat(1316.779!, 91.3747!)
        Me.LBKota.Name = "LBKota"
        Me.LBKota.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKota.SizeF = New System.Drawing.SizeF(653.2986!, 45.19083!)
        Me.LBKota.Text = "Kota"
        Me.LBKota.Visible = False
        '
        'XrRichText1
        '
        Me.XrRichText1.Dpi = 254.0!
        Me.XrRichText1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.XrRichText1.LocationFloat = New DevExpress.Utils.PointFloat(1630.146!, 147.0084!)
        Me.XrRichText1.Name = "XrRichText1"
        Me.XrRichText1.SerializableRtfString = resources.GetString("XrRichText1.SerializableRtfString")
        Me.XrRichText1.SizeF = New System.Drawing.SizeF(408.6951!, 236.5877!)
        Me.XrRichText1.StylePriority.UseFont = False
        Me.XrRichText1.Visible = False
        '
        'LBTotQty
        '
        Me.LBTotQty.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBTotQty.Dpi = 254.0!
        Me.LBTotQty.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotQty.LocationFloat = New DevExpress.Utils.PointFloat(1381.161!, 5.557396!)
        Me.LBTotQty.Name = "LBTotQty"
        Me.LBTotQty.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotQty.SizeF = New System.Drawing.SizeF(147.601!, 47.0!)
        Me.LBTotQty.StylePriority.UseBorders = False
        Me.LBTotQty.StylePriority.UseFont = False
        Me.LBTotQty.StylePriority.UseTextAlignment = False
        Me.LBTotQty.Text = "Qty"
        Me.LBTotQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(40.49999!, 0.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2001.732!, 5.557416!)
        '
        'XrLabel9
        '
        Me.XrLabel9.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(278.3938!, 5.999999!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(1098.753!, 47.0!)
        Me.XrLabel9.StylePriority.UseBorders = False
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "Total"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel12, Me.XrLabel3, Me.XrLabel5, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLine1, Me.XrLabel11})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 62.14995!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(1754.753!, 5.999957!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(284.087!, 51.00001!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Keterangan"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(41.49999!, 5.999984!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(232.7467!, 51.0!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "ID Bahan"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel8
        '
        Me.XrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(1381.161!, 5.999957!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(147.6011!, 51.0!)
        Me.XrLabel8.StylePriority.UseBorders = False
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "Qty"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel7
        '
        Me.XrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(1236.029!, 5.999957!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(141.1176!, 51.0!)
        Me.XrLabel7.StylePriority.UseBorders = False
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "Satuan"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(539.4676!, 5.999954!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(692.6424!, 51.00001!)
        Me.XrLabel6.StylePriority.UseBorders = False
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Deskripsi Bahan"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(35.50002!, 57.14995!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2006.732!, 5.0!)
        '
        'XrLabel11
        '
        Me.XrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(278.3348!, 6.000002!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(258.1331!, 51.00001!)
        Me.XrLabel11.StylePriority.UseBorders = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "Batch Number"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandBox1.BorderWidth = 2.0!
        Me.XrCrossBandBox1.Dpi = 254.0!
        Me.XrCrossBandBox1.EndBand = Me.ReportFooter
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(35.50002!, 58.69126!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(35.50002!, 0.0!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.PageHeader
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(35.50002!, 0.0!)
        Me.XrCrossBandBox1.WidthF = 2008.633!
        '
        'XrCrossBandLine1
        '
        Me.XrCrossBandLine1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine1.Dpi = 254.0!
        Me.XrCrossBandLine1.EndBand = Me.ReportFooter
        Me.XrCrossBandLine1.EndPointFloat = New DevExpress.Utils.PointFloat(275.1313!, 5.0!)
        Me.XrCrossBandLine1.LocationFloat = New DevExpress.Utils.PointFloat(275.1313!, 4.274038!)
        Me.XrCrossBandLine1.Name = "XrCrossBandLine1"
        Me.XrCrossBandLine1.StartBand = Me.PageHeader
        Me.XrCrossBandLine1.StartPointFloat = New DevExpress.Utils.PointFloat(275.1313!, 4.274038!)
        Me.XrCrossBandLine1.WidthF = 3.203491!
        '
        'XrCrossBandLine2
        '
        Me.XrCrossBandLine2.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine2.Dpi = 254.0!
        Me.XrCrossBandLine2.EndBand = Me.ReportFooter
        Me.XrCrossBandLine2.EndPointFloat = New DevExpress.Utils.PointFloat(1232.11!, 5.0!)
        Me.XrCrossBandLine2.LocationFloat = New DevExpress.Utils.PointFloat(1232.11!, 4.419316!)
        Me.XrCrossBandLine2.Name = "XrCrossBandLine2"
        Me.XrCrossBandLine2.StartBand = Me.PageHeader
        Me.XrCrossBandLine2.StartPointFloat = New DevExpress.Utils.PointFloat(1232.11!, 4.419316!)
        Me.XrCrossBandLine2.WidthF = 3.014282!
        '
        'XrCrossBandLine3
        '
        Me.XrCrossBandLine3.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine3.Dpi = 254.0!
        Me.XrCrossBandLine3.EndBand = Me.ReportFooter
        Me.XrCrossBandLine3.EndPointFloat = New DevExpress.Utils.PointFloat(1378.146!, 54.54195!)
        Me.XrCrossBandLine3.LocationFloat = New DevExpress.Utils.PointFloat(1378.146!, 3.414562!)
        Me.XrCrossBandLine3.Name = "XrCrossBandLine3"
        Me.XrCrossBandLine3.StartBand = Me.PageHeader
        Me.XrCrossBandLine3.StartPointFloat = New DevExpress.Utils.PointFloat(1378.146!, 3.414562!)
        Me.XrCrossBandLine3.WidthF = 3.000122!
        '
        'XrCrossBandLine4
        '
        Me.XrCrossBandLine4.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine4.Dpi = 254.0!
        Me.XrCrossBandLine4.EndBand = Me.ReportFooter
        Me.XrCrossBandLine4.EndPointFloat = New DevExpress.Utils.PointFloat(1751.295!, 0.0!)
        Me.XrCrossBandLine4.LocationFloat = New DevExpress.Utils.PointFloat(1751.295!, 2.431295!)
        Me.XrCrossBandLine4.Name = "XrCrossBandLine4"
        Me.XrCrossBandLine4.StartBand = Me.PageHeader
        Me.XrCrossBandLine4.StartPointFloat = New DevExpress.Utils.PointFloat(1751.295!, 2.431295!)
        Me.XrCrossBandLine4.WidthF = 3.0!
        '
        'XrCrossBandLine5
        '
        Me.XrCrossBandLine5.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine5.Dpi = 254.0!
        Me.XrCrossBandLine5.EndBand = Me.ReportFooter
        Me.XrCrossBandLine5.EndPointFloat = New DevExpress.Utils.PointFloat(536.4679!, 4.023129!)
        Me.XrCrossBandLine5.LocationFloat = New DevExpress.Utils.PointFloat(536.4679!, 2.643047!)
        Me.XrCrossBandLine5.Name = "XrCrossBandLine5"
        Me.XrCrossBandLine5.StartBand = Me.PageHeader
        Me.XrCrossBandLine5.StartPointFloat = New DevExpress.Utils.PointFloat(536.4679!, 2.643047!)
        Me.XrCrossBandLine5.WidthF = 3.0!
        '
        'XrCrossBandLine6
        '
        Me.XrCrossBandLine6.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine6.Dpi = 254.0!
        Me.XrCrossBandLine6.EndBand = Me.ReportFooter
        Me.XrCrossBandLine6.EndPointFloat = New DevExpress.Utils.PointFloat(1529.427!, 54.68478!)
        Me.XrCrossBandLine6.LocationFloat = New DevExpress.Utils.PointFloat(1529.427!, 0.0!)
        Me.XrCrossBandLine6.Name = "XrCrossBandLine6"
        Me.XrCrossBandLine6.StartBand = Me.PageHeader
        Me.XrCrossBandLine6.StartPointFloat = New DevExpress.Utils.PointFloat(1529.427!, 0.0!)
        Me.XrCrossBandLine6.WidthF = 3.0!
        '
        'XrLabel12
        '
        Me.XrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(1532.427!, 5.999954!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(218.868!, 51.0!)
        Me.XrLabel12.StylePriority.UseBorders = False
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "Saldo"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBSaldo
        '
        Me.LBSaldo.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBSaldo.Dpi = 254.0!
        Me.LBSaldo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBSaldo.LocationFloat = New DevExpress.Utils.PointFloat(1532.427!, 0.0!)
        Me.LBSaldo.Name = "LBSaldo"
        Me.LBSaldo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSaldo.SizeF = New System.Drawing.SizeF(218.868!, 47.0!)
        Me.LBSaldo.StylePriority.UseBorders = False
        Me.LBSaldo.StylePriority.UseFont = False
        Me.LBSaldo.StylePriority.UseTextAlignment = False
        Me.LBSaldo.Text = "Saldo"
        Me.LBSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotSaldo
        '
        Me.LBTotSaldo.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.LBTotSaldo.Dpi = 254.0!
        Me.LBTotSaldo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotSaldo.LocationFloat = New DevExpress.Utils.PointFloat(1532.427!, 5.557396!)
        Me.LBTotSaldo.Name = "LBTotSaldo"
        Me.LBTotSaldo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotSaldo.SizeF = New System.Drawing.SizeF(260.351!, 47.0!)
        Me.LBTotSaldo.StylePriority.UseBorders = False
        Me.LBTotSaldo.StylePriority.UseFont = False
        Me.LBTotSaldo.StylePriority.UseTextAlignment = False
        Me.LBTotSaldo.Text = "Saldo"
        Me.LBTotSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XRAdjBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageFooter, Me.ReportFooter, Me.PageHeader})
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandLine6, Me.XrCrossBandLine5, Me.XrCrossBandLine4, Me.XrCrossBandLine3, Me.XrCrossBandLine2, Me.XrCrossBandLine1, Me.XrCrossBandBox1})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me.LBKeterangan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBBID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatuan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQty As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotQty As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandLine1 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine2 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine3 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine4 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents LBTT1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBKota As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrRichText1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKeterangan As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandLine5 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBtNum As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSaldo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotSaldo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandLine6 As DevExpress.XtraReports.UI.XRCrossBandLine
End Class
