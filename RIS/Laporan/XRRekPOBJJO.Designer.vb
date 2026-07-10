<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRRekPOBJJO
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBSubTot = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBMtUang = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPOCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTglKirim = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHeader = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBTotAkhir = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKota = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBManager = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSubTot, Me.LBMtUang, Me.LBPsg, Me.LBNo, Me.LBKode, Me.LBPOCust, Me.LBTglKirim, Me.LBCust})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 48.00008!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSubTot
        '
        Me.LBSubTot.Dpi = 254.0!
        Me.LBSubTot.LocationFloat = New DevExpress.Utils.PointFloat(1749.792!, 0.00008074442!)
        Me.LBSubTot.Name = "LBSubTot"
        Me.LBSubTot.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSubTot.SizeF = New System.Drawing.SizeF(282.6456!, 45.19092!)
        Me.LBSubTot.StylePriority.UseTextAlignment = False
        Me.LBSubTot.Text = "Tot Akhir"
        Me.LBSubTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBMtUang
        '
        Me.LBMtUang.Dpi = 254.0!
        Me.LBMtUang.LocationFloat = New DevExpress.Utils.PointFloat(1588.396!, 0.00008074442!)
        Me.LBMtUang.Name = "LBMtUang"
        Me.LBMtUang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBMtUang.SizeF = New System.Drawing.SizeF(161.3959!, 45.19092!)
        Me.LBMtUang.StylePriority.UseTextAlignment = False
        Me.LBMtUang.Text = "Mata Uang"
        Me.LBMtUang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBPsg
        '
        Me.LBPsg.Dpi = 254.0!
        Me.LBPsg.LocationFloat = New DevExpress.Utils.PointFloat(1406.812!, 0.00008074442!)
        Me.LBPsg.Name = "LBPsg"
        Me.LBPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPsg.SizeF = New System.Drawing.SizeF(181.5831!, 45.19092!)
        Me.LBPsg.StylePriority.UseTextAlignment = False
        Me.LBPsg.Text = "Tot Psg"
        Me.LBPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(19.57275!, 0.0!)
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
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(557.7192!, 0.00008074442!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(291.6663!, 45.19081!)
        Me.LBKode.Text = "No. PO"
        '
        'LBPOCust
        '
        Me.LBPOCust.Dpi = 254.0!
        Me.LBPOCust.LocationFloat = New DevExpress.Utils.PointFloat(849.3856!, 0.00008074442!)
        Me.LBPOCust.Name = "LBPOCust"
        Me.LBPOCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPOCust.SizeF = New System.Drawing.SizeF(328.356!, 45.19086!)
        Me.LBPOCust.Text = "No. PO Customer"
        '
        'LBTglKirim
        '
        Me.LBTglKirim.Dpi = 254.0!
        Me.LBTglKirim.LocationFloat = New DevExpress.Utils.PointFloat(1178.41!, 0.00008074442!)
        Me.LBTglKirim.Name = "LBTglKirim"
        Me.LBTglKirim.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTglKirim.SizeF = New System.Drawing.SizeF(225.101!, 48.0!)
        Me.LBTglKirim.Text = "Tanggal Kirim"
        '
        'LBCust
        '
        Me.LBCust.Dpi = 254.0!
        Me.LBCust.LocationFloat = New DevExpress.Utils.PointFloat(107.9478!, 0.0!)
        Me.LBCust.Name = "LBCust"
        Me.LBCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCust.SizeF = New System.Drawing.SizeF(449.7714!, 45.19081!)
        Me.LBCust.Text = "Customer"
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel4, Me.XrLabel10, Me.XrLabel14, Me.XrLine2, Me.XrLine1, Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel12, Me.LBPerusahaan, Me.LBHeader})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 260.8583!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(105.9374!, 210.5257!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(449.7714!, 45.19092!)
        Me.XrLabel1.Text = "Customer"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(557.7192!, 210.5257!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(291.6663!, 45.19092!)
        Me.XrLabel4.Text = "No. PO"
        '
        'XrLabel10
        '
        Me.XrLabel10.Dpi = 254.0!
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(849.3856!, 210.5257!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(328.356!, 45.19092!)
        Me.XrLabel10.Text = "No. PO Customer"
        '
        'XrLabel14
        '
        Me.XrLabel14.Dpi = 254.0!
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(1178.41!, 210.5257!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(227.5216!, 45.19092!)
        Me.XrLabel14.Text = "Tanggal Kirim"
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(18.5735!, 255.8583!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(17.56242!, 204.6875!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(1749.792!, 210.5257!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(282.6456!, 45.19092!)
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Tot Akhir"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel17
        '
        Me.XrLabel17.Dpi = 254.0!
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(1588.396!, 210.5258!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(161.3959!, 45.19092!)
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "Mata Uang"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(1406.812!, 210.5258!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(181.5831!, 45.19092!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Tot Psg"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(17.56242!, 210.5258!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(88.375!, 45.19092!)
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "No."
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(19.57273!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(733.8341!, 58.42!)
        '
        'LBHeader
        '
        Me.LBHeader.Dpi = 254.0!
        Me.LBHeader.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBHeader.LocationFloat = New DevExpress.Utils.PointFloat(19.57273!, 42.22752!)
        Me.LBHeader.Name = "LBHeader"
        Me.LBHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHeader.SizeF = New System.Drawing.SizeF(2012.865!, 58.42001!)
        Me.LBHeader.StylePriority.UseFont = False
        Me.LBHeader.StylePriority.UseTextAlignment = False
        Me.LBHeader.Text = "Rekap Purchase Order"
        Me.LBHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2, Me.LBUser, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 68.83667!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBTotAkhir
        '
        Me.LBTotAkhir.Dpi = 254.0!
        Me.LBTotAkhir.LocationFloat = New DevExpress.Utils.PointFloat(1750.808!, 5.000008!)
        Me.LBTotAkhir.Name = "LBTotAkhir"
        Me.LBTotAkhir.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotAkhir.SizeF = New System.Drawing.SizeF(282.6456!, 45.19092!)
        Me.LBTotAkhir.StylePriority.UseTextAlignment = False
        Me.LBTotAkhir.Text = "Tot Akhir"
        Me.LBTotAkhir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotPsg
        '
        Me.LBTotPsg.Dpi = 254.0!
        Me.LBTotPsg.LocationFloat = New DevExpress.Utils.PointFloat(1406.812!, 5.000008!)
        Me.LBTotPsg.Name = "LBTotPsg"
        Me.LBTotPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotPsg.SizeF = New System.Drawing.SizeF(181.5831!, 45.19092!)
        Me.LBTotPsg.StylePriority.UseTextAlignment = False
        Me.LBTotPsg.Text = "Tot Psg"
        Me.LBTotPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBKota
        '
        Me.LBKota.Dpi = 254.0!
        Me.LBKota.LocationFloat = New DevExpress.Utils.PointFloat(1119.267!, 111.535!)
        Me.LBKota.Name = "LBKota"
        Me.LBKota.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKota.SizeF = New System.Drawing.SizeF(884.4652!, 48.00001!)
        Me.LBKota.StylePriority.UseTextAlignment = False
        Me.LBKota.Text = "Kota"
        Me.LBKota.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.LocationFloat = New DevExpress.Utils.PointFloat(1119.267!, 159.5352!)
        Me.XrLabel20.Multiline = True
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel20.SizeF = New System.Drawing.SizeF(390.6398!, 252.8674!)
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "Dibuat Oleh" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(                               )"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel21
        '
        Me.XrLabel21.Dpi = 254.0!
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(1613.093!, 159.5352!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(390.6398!, 48.0!)
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "Mengetahui"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBManager
        '
        Me.LBManager.Dpi = 254.0!
        Me.LBManager.LocationFloat = New DevExpress.Utils.PointFloat(1613.093!, 353.7599!)
        Me.LBManager.Name = "LBManager"
        Me.LBManager.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBManager.SizeF = New System.Drawing.SizeF(390.6398!, 48.0!)
        Me.LBManager.StylePriority.UseTextAlignment = False
        Me.LBManager.Text = "(...................................)"
        Me.LBManager.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(1119.267!, 413.4025!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(585.0958!, 47.83667!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "* Untuk semua tanda tangan diberi nama terang"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 0
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(17.5624!, 0.0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2014.876!, 5.0!)
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(198.5001!, 21.0!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(13.0!, 21.0!)
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
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(13.22917!, 21.00009!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2019.209!, 47.83658!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBTotPsg, Me.XrLabel11, Me.LBManager, Me.XrLabel21, Me.XrLabel20, Me.LBKota, Me.XrLine3, Me.LBTotAkhir})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 461.2391!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XRRekPOBJJO
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 3300
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
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHeader As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBSubTot As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBMtUang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPOCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTglKirim As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotAkhir As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKota As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBManager As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
End Class
