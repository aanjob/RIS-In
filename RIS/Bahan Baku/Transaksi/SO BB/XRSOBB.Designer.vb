<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSOBB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRSOBB))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatuan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQty = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBAlamat = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHeader = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBTT2 = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBTT1 = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBKota = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBTT2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBNo, Me.LBBahan, Me.LBSatuan, Me.LBQty})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 45.19093!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(21.5544!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(81.054!, 45.19086!)
        Me.LBNo.StylePriority.UseFont = False
        Me.LBNo.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        Me.LBNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBBahan
        '
        Me.LBBahan.Dpi = 254.0!
        Me.LBBahan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBBahan.LocationFloat = New DevExpress.Utils.PointFloat(102.6084!, 0.0!)
        Me.LBBahan.Name = "LBBahan"
        Me.LBBahan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahan.SizeF = New System.Drawing.SizeF(1519.072!, 45.19086!)
        Me.LBBahan.StylePriority.UseFont = False
        Me.LBBahan.StylePriority.UseTextAlignment = False
        Me.LBBahan.Text = "Nama Bahan"
        Me.LBBahan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSatuan
        '
        Me.LBSatuan.Dpi = 254.0!
        Me.LBSatuan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBSatuan.LocationFloat = New DevExpress.Utils.PointFloat(1621.68!, 0.0!)
        Me.LBSatuan.Name = "LBSatuan"
        Me.LBSatuan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatuan.SizeF = New System.Drawing.SizeF(184.228!, 45.19093!)
        Me.LBSatuan.StylePriority.UseFont = False
        Me.LBSatuan.StylePriority.UseTextAlignment = False
        Me.LBSatuan.Text = "Satuan"
        Me.LBSatuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQty
        '
        Me.LBQty.Dpi = 254.0!
        Me.LBQty.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBQty.LocationFloat = New DevExpress.Utils.PointFloat(1805.908!, 0.0!)
        Me.LBQty.Name = "LBQty"
        Me.LBQty.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQty.SizeF = New System.Drawing.SizeF(229.0302!, 45.19093!)
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
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.LBKet, Me.LBAlamat, Me.XrLabel15, Me.XrLabel16, Me.LBCust, Me.XrLabel12, Me.XrLabel4, Me.XrLabel5, Me.XrLabel18, Me.LBTanggal, Me.LBPerusahaan, Me.XrLine1, Me.XrLine2, Me.XrLabel7, Me.LBHeader, Me.LBKode})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 451.508!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1021.948!, 232.8143!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(214.3125!, 45.19086!)
        Me.XrLabel2.Text = "Keterangan"
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(1236.407!, 232.8143!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(798.531!, 143.1494!)
        Me.LBKet.StylePriority.UsePadding = False
        '
        'LBAlamat
        '
        Me.LBAlamat.Dpi = 254.0!
        Me.LBAlamat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBAlamat.LocationFloat = New DevExpress.Utils.PointFloat(224.503!, 232.8143!)
        Me.LBAlamat.Name = "LBAlamat"
        Me.LBAlamat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBAlamat.SerializableRtfString = resources.GetString("LBAlamat.SerializableRtfString")
        Me.LBAlamat.SizeF = New System.Drawing.SizeF(708.177!, 119.316!)
        Me.LBAlamat.StylePriority.UsePadding = False
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(102.6084!, 401.1754!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(1519.072!, 45.19092!)
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Deskripsi Bahan"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(1621.68!, 401.1754!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(184.228!, 45.19092!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Satuan"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBCust
        '
        Me.LBCust.Dpi = 254.0!
        Me.LBCust.LocationFloat = New DevExpress.Utils.PointFloat(224.503!, 187.3941!)
        Me.LBCust.Name = "LBCust"
        Me.LBCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCust.SizeF = New System.Drawing.SizeF(600.5207!, 45.19092!)
        Me.LBCust.Text = "Customer"
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(21.5544!, 401.1756!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(81.05399!, 45.19086!)
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "No."
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(1021.948!, 187.3942!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(214.3125!, 45.19083!)
        Me.XrLabel4.Text = "No. SO"
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(52.57383!, 232.8143!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(171.9292!, 45.19083!)
        Me.XrLabel5.Text = "Alamat"
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(1805.908!, 401.1754!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(229.0302!, 45.19092!)
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Qty"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(22.07294!, 101.6475!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(2012.865!, 47.83665!)
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(22.07294!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(733.8341!, 58.42!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(22.55444!, 395.3372!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(22.56554!, 446.508!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(52.42711!, 187.3942!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(172.0759!, 45.19092!)
        Me.XrLabel7.Text = "Customer"
        '
        'LBHeader
        '
        Me.LBHeader.Dpi = 254.0!
        Me.LBHeader.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBHeader.LocationFloat = New DevExpress.Utils.PointFloat(22.07294!, 42.22752!)
        Me.LBHeader.Name = "LBHeader"
        Me.LBHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHeader.SizeF = New System.Drawing.SizeF(2012.865!, 58.42001!)
        Me.LBHeader.StylePriority.UseFont = False
        Me.LBHeader.StylePriority.UseTextAlignment = False
        Me.LBHeader.Text = "Sales Order"
        Me.LBHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(1236.407!, 187.3942!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(711.7291!, 45.19081!)
        Me.LBKode.Text = "No. SO"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel11, Me.LBUser, Me.XrPageInfo2, Me.LBTT2, Me.LBTT1, Me.LBKota, Me.XrLine3, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 378.0433!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(22.07294!, 330.2066!)
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
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(210.0729!, 330.2066!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'LBTT2
        '
        Me.LBTT2.Dpi = 254.0!
        Me.LBTT2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBTT2.LocationFloat = New DevExpress.Utils.PointFloat(486.0366!, 75.63379!)
        Me.LBTT2.Name = "LBTT2"
        Me.LBTT2.SerializableRtfString = resources.GetString("LBTT2.SerializableRtfString")
        Me.LBTT2.SizeF = New System.Drawing.SizeF(322.5933!, 182.7741!)
        Me.LBTT2.StylePriority.UseFont = False
        Me.LBTT2.Visible = False
        '
        'LBTT1
        '
        Me.LBTT1.Dpi = 254.0!
        Me.LBTT1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBTT1.LocationFloat = New DevExpress.Utils.PointFloat(102.6084!, 75.63379!)
        Me.LBTT1.Name = "LBTT1"
        Me.LBTT1.SerializableRtfString = resources.GetString("LBTT1.SerializableRtfString")
        Me.LBTT1.SizeF = New System.Drawing.SizeF(322.5934!, 182.7741!)
        Me.LBTT1.StylePriority.UseFont = False
        Me.LBTT1.Visible = False
        '
        'LBKota
        '
        Me.LBKota.Dpi = 254.0!
        Me.LBKota.LocationFloat = New DevExpress.Utils.PointFloat(102.6084!, 30.0!)
        Me.LBKota.Name = "LBKota"
        Me.LBKota.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKota.SizeF = New System.Drawing.SizeF(653.2986!, 45.19083!)
        Me.LBKota.Text = "Kota"
        Me.LBKota.Visible = False
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(24.06!, 0.0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(22.07294!, 330.2067!)
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
        'XrLabel11
        '
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(102.6084!, 258.4078!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(585.0958!, 47.83667!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "* Untuk semua tanda tangan diberi nama terang"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XRSOBB
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
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBTT2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LBTT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHeader As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatuan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQty As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBAlamat As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBKota As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTT2 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBTT1 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
End Class
