<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRReqCab
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRReqCab))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDos = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtCode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSat = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtName = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBCab = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDocID = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBPFDos = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPFPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBPsg, Me.LBDos, Me.LBArtCode, Me.LBSat, Me.LBArtName, Me.LBNo})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 46.02921!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBPsg
        '
        Me.LBPsg.Dpi = 254.0!
        Me.LBPsg.LocationFloat = New DevExpress.Utils.PointFloat(1807.214!, 0.0!)
        Me.LBPsg.Name = "LBPsg"
        Me.LBPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPsg.SizeF = New System.Drawing.SizeF(253.3644!, 45.19093!)
        Me.LBPsg.StylePriority.UseTextAlignment = False
        Me.LBPsg.Text = "Psg"
        Me.LBPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBDos
        '
        Me.LBDos.Dpi = 254.0!
        Me.LBDos.LocationFloat = New DevExpress.Utils.PointFloat(1555.318!, 0.0!)
        Me.LBDos.Name = "LBDos"
        Me.LBDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDos.SizeF = New System.Drawing.SizeF(251.3541!, 45.19093!)
        Me.LBDos.StylePriority.UseTextAlignment = False
        Me.LBDos.Text = "Dos"
        Me.LBDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBArtCode
        '
        Me.LBArtCode.Dpi = 254.0!
        Me.LBArtCode.LocationFloat = New DevExpress.Utils.PointFloat(132.0884!, 0.0!)
        Me.LBArtCode.Name = "LBArtCode"
        Me.LBArtCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtCode.SizeF = New System.Drawing.SizeF(389.3751!, 45.19093!)
        Me.LBArtCode.StylePriority.UseTextAlignment = False
        Me.LBArtCode.Text = "Art. Code"
        Me.LBArtCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSat
        '
        Me.LBSat.Dpi = 254.0!
        Me.LBSat.LocationFloat = New DevExpress.Utils.PointFloat(1361.838!, 0.0!)
        Me.LBSat.Name = "LBSat"
        Me.LBSat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSat.SizeF = New System.Drawing.SizeF(193.4796!, 45.19093!)
        Me.LBSat.StylePriority.UseTextAlignment = False
        Me.LBSat.Text = "Satuan"
        Me.LBSat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBArtName
        '
        Me.LBArtName.Dpi = 254.0!
        Me.LBArtName.LocationFloat = New DevExpress.Utils.PointFloat(521.4635!, 0.0!)
        Me.LBArtName.Name = "LBArtName"
        Me.LBArtName.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtName.SizeF = New System.Drawing.SizeF(839.4583!, 46.02921!)
        Me.LBArtName.StylePriority.UseTextAlignment = False
        Me.LBArtName.Text = "Art Name"
        Me.LBArtName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(43.71342!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(88.375!, 45.19092!)
        Me.LBNo.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        Me.LBNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.XrLine1, Me.XrLabel20, Me.XrLabel19, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.LBKet, Me.LBCab, Me.LBDocID, Me.XrLabel9, Me.XrLabel7, Me.XrLabel4, Me.LBTanggal, Me.LBPerusahaan, Me.XrLabel1, Me.XrLabel12})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 415.397!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(44.7245!, 410.397!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(43.71342!, 359.2261!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(1807.214!, 365.0644!)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(253.3644!, 45.19092!)
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "Psg"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel19
        '
        Me.XrLabel19.Dpi = 254.0!
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(1555.318!, 365.0644!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(251.3541!, 45.19092!)
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "Dos"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(132.0884!, 365.0644!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(389.3751!, 45.19092!)
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "Art. Code"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(1361.838!, 365.0644!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(193.4796!, 45.19092!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Satuan"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(521.4635!, 364.2262!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(839.4583!, 46.02921!)
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Art Name"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(1269.839!, 185.9329!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(790.7389!, 135.1909!)
        Me.LBKet.StylePriority.UsePadding = False
        '
        'LBCab
        '
        Me.LBCab.Dpi = 254.0!
        Me.LBCab.LocationFloat = New DevExpress.Utils.PointFloat(225.2343!, 231.3529!)
        Me.LBCab.Name = "LBCab"
        Me.LBCab.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCab.SizeF = New System.Drawing.SizeF(735.5419!, 45.19092!)
        Me.LBCab.Text = "Cabang"
        '
        'LBDocID
        '
        Me.LBDocID.Dpi = 254.0!
        Me.LBDocID.LocationFloat = New DevExpress.Utils.PointFloat(225.2343!, 185.9329!)
        Me.LBDocID.Name = "LBDocID"
        Me.LBDocID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDocID.SizeF = New System.Drawing.SizeF(444.5!, 45.19083!)
        Me.LBDocID.Text = "Nomor"
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(1055.38!, 185.9329!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(214.3125!, 45.19086!)
        Me.XrLabel9.Text = "Keterangan"
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(78.72379!, 231.3529!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(145.5208!, 45.19092!)
        Me.XrLabel7.Text = "Cabang"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(78.72379!, 185.9329!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(145.5208!, 45.19083!)
        Me.XrLabel4.Text = "No. Doc"
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(45.72371!, 100.1861!)
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
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(45.72371!, 0.3947999!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(733.8341!, 58.42!)
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(45.72371!, 40.76612!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2012.865!, 58.42001!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Request Cabang"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(43.71342!, 365.0644!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(88.375!, 45.19092!)
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "No."
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.LBUser, Me.XrPageInfo2, Me.XrLine4, Me.LBPFDos, Me.LBPFPsg, Me.XrLabel10})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 128.1779!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(43.71431!, 80.34133!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2016.864!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(43.48499!, 79.34125!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(182.6558!, 47.83667!)
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
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(228.9852!, 80.34125!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(376.0099!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'XrLine4
        '
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LineWidth = 3
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(44.72514!, 0.0!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'LBPFDos
        '
        Me.LBPFDos.Dpi = 254.0!
        Me.LBPFDos.LocationFloat = New DevExpress.Utils.PointFloat(1555.319!, 5.822138!)
        Me.LBPFDos.Name = "LBPFDos"
        Me.LBPFDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPFDos.SizeF = New System.Drawing.SizeF(251.3541!, 45.19092!)
        Me.LBPFDos.StylePriority.UseTextAlignment = False
        Me.LBPFDos.Text = "Dos"
        Me.LBPFDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBPFPsg
        '
        Me.LBPFPsg.Dpi = 254.0!
        Me.LBPFPsg.LocationFloat = New DevExpress.Utils.PointFloat(1807.215!, 5.822138!)
        Me.LBPFPsg.Name = "LBPFPsg"
        Me.LBPFPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPFPsg.SizeF = New System.Drawing.SizeF(253.3644!, 45.19092!)
        Me.LBPFPsg.StylePriority.UseTextAlignment = False
        Me.LBPFPsg.Text = "Psg"
        Me.LBPFPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(1361.839!, 5.822138!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(193.4796!, 45.19092!)
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "Total"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XRReqCab
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
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
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBCab As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDocID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtCode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSat As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBPFDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPFPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
End Class
