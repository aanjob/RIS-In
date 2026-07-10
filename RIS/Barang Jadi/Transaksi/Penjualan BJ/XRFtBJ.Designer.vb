<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRFtBJ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRFtBJ))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHarAkhir = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBHarSat = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBarang = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDos = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDisc = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.LBCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBAlamat = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBJualID = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBDueDate = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotByExpd = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotSbDisc = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDiscDet = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDiscH = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotAkhir = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDos = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBPsg, Me.LBHarAkhir, Me.LBHarSat, Me.LBBarang, Me.LBDos, Me.LBNo, Me.LBDisc})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 48.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBPsg
        '
        Me.LBPsg.Dpi = 254.0!
        Me.LBPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPsg.LocationFloat = New DevExpress.Utils.PointFloat(1022.095!, 0.0!)
        Me.LBPsg.Name = "LBPsg"
        Me.LBPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPsg.SizeF = New System.Drawing.SizeF(157.1238!, 48.0!)
        Me.LBPsg.StylePriority.UseFont = False
        Me.LBPsg.StylePriority.UseTextAlignment = False
        Me.LBPsg.Text = "Psg"
        Me.LBPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBHarAkhir
        '
        Me.LBHarAkhir.Dpi = 254.0!
        Me.LBHarAkhir.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBHarAkhir.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 0.0!)
        Me.LBHarAkhir.Name = "LBHarAkhir"
        Me.LBHarAkhir.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHarAkhir.SizeF = New System.Drawing.SizeF(344.0804!, 48.0!)
        Me.LBHarAkhir.StylePriority.UseFont = False
        Me.LBHarAkhir.StylePriority.UseTextAlignment = False
        Me.LBHarAkhir.Text = "Jumlah"
        Me.LBHarAkhir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBHarSat
        '
        Me.LBHarSat.Dpi = 254.0!
        Me.LBHarSat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBHarSat.LocationFloat = New DevExpress.Utils.PointFloat(1166.219!, 0.0!)
        Me.LBHarSat.Name = "LBHarSat"
        Me.LBHarSat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBHarSat.SizeF = New System.Drawing.SizeF(269.8745!, 48.0!)
        Me.LBHarSat.StylePriority.UseFont = False
        Me.LBHarSat.StylePriority.UseTextAlignment = False
        Me.LBHarSat.Text = "Harga Satuan"
        Me.LBHarSat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBBarang
        '
        Me.LBBarang.Dpi = 254.0!
        Me.LBBarang.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBBarang.LocationFloat = New DevExpress.Utils.PointFloat(210.3989!, 0.0!)
        Me.LBBarang.Name = "LBBarang"
        Me.LBBarang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBarang.SizeF = New System.Drawing.SizeF(717.7177!, 48.0!)
        Me.LBBarang.StylePriority.UseFont = False
        Me.LBBarang.StylePriority.UseTextAlignment = False
        Me.LBBarang.Text = "Article Code"
        Me.LBBarang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBDos
        '
        Me.LBDos.Dpi = 254.0!
        Me.LBDos.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBDos.LocationFloat = New DevExpress.Utils.PointFloat(919.1163!, 0.0!)
        Me.LBDos.Name = "LBDos"
        Me.LBDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDos.SizeF = New System.Drawing.SizeF(102.9785!, 48.0!)
        Me.LBDos.StylePriority.UseFont = False
        Me.LBDos.StylePriority.UseTextAlignment = False
        Me.LBDos.Text = "Dos"
        Me.LBDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(121.3657!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(88.99149!, 48.0!)
        Me.LBNo.StylePriority.UseFont = False
        Me.LBNo.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        Me.LBNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBDisc
        '
        Me.LBDisc.Dpi = 254.0!
        Me.LBDisc.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBDisc.LocationFloat = New DevExpress.Utils.PointFloat(1345.677!, 0.0!)
        Me.LBDisc.Name = "LBDisc"
        Me.LBDisc.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDisc.SizeF = New System.Drawing.SizeF(239.2931!, 48.0!)
        Me.LBDisc.StylePriority.UseFont = False
        Me.LBDisc.StylePriority.UseTextAlignment = False
        Me.LBDisc.Text = "Disc"
        Me.LBDisc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBCust, Me.LBAlamat, Me.LBJualID, Me.LBKode})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 390.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'LBCust
        '
        Me.LBCust.Dpi = 254.0!
        Me.LBCust.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBCust.LocationFloat = New DevExpress.Utils.PointFloat(1296.724!, 99.36102!)
        Me.LBCust.Name = "LBCust"
        Me.LBCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCust.SizeF = New System.Drawing.SizeF(768.3273!, 45.19092!)
        Me.LBCust.StylePriority.UseFont = False
        Me.LBCust.Text = "Customer"
        '
        'LBAlamat
        '
        Me.LBAlamat.Dpi = 254.0!
        Me.LBAlamat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBAlamat.LocationFloat = New DevExpress.Utils.PointFloat(1296.724!, 144.8645!)
        Me.LBAlamat.Name = "LBAlamat"
        Me.LBAlamat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBAlamat.SerializableRtfString = resources.GetString("LBAlamat.SerializableRtfString")
        Me.LBAlamat.SizeF = New System.Drawing.SizeF(768.3273!, 119.316!)
        Me.LBAlamat.StylePriority.UseFont = False
        Me.LBAlamat.StylePriority.UsePadding = False
        '
        'LBJualID
        '
        Me.LBJualID.Dpi = 254.0!
        Me.LBJualID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBJualID.LocationFloat = New DevExpress.Utils.PointFloat(326.8854!, 213.0!)
        Me.LBJualID.Name = "LBJualID"
        Me.LBJualID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJualID.SizeF = New System.Drawing.SizeF(711.7291!, 48.0!)
        Me.LBJualID.StylePriority.UseFont = False
        Me.LBJualID.Text = "Nomor"
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(326.8854!, 165.0!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(711.7291!, 48.0!)
        Me.LBKode.StylePriority.UseFont = False
        Me.LBKode.Text = "Kode"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBUser, Me.XrPageInfo2, Me.LBDueDate, Me.XrPageInfo1, Me.LBKet, Me.LBTanggal, Me.LBTotByExpd, Me.LBTotSbDisc, Me.LBTotDiscDet, Me.LBTotDiscH, Me.LBTotAkhir, Me.LBTotDos, Me.LBTotPsg})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 702.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(1739.635!, 596.9566!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(153.25!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:ddMMyyhhmm}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(1893.24!, 596.9566!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(190.5!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'LBDueDate
        '
        Me.LBDueDate.Dpi = 254.0!
        Me.LBDueDate.LocationFloat = New DevExpress.Utils.PointFloat(1792.266!, 528.9358!)
        Me.LBDueDate.Name = "LBDueDate"
        Me.LBDueDate.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDueDate.SizeF = New System.Drawing.SizeF(288.7866!, 47.83661!)
        Me.LBDueDate.StylePriority.UseTextAlignment = False
        Me.LBDueDate.Text = "Due Date"
        Me.LBDueDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(892.5311!, 528.9358!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(277.8125!, 47.83667!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(101.3657!, 43.2065!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(672.4347!, 151.066!)
        Me.LBKet.StylePriority.UseFont = False
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(232.7402!, 256.4152!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(606.2866!, 47.83661!)
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBTotByExpd
        '
        Me.LBTotByExpd.Dpi = 254.0!
        Me.LBTotByExpd.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotByExpd.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 265.1458!)
        Me.LBTotByExpd.Name = "LBTotByExpd"
        Me.LBTotByExpd.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotByExpd.SizeF = New System.Drawing.SizeF(344.0804!, 47.99997!)
        Me.LBTotByExpd.StylePriority.UseFont = False
        Me.LBTotByExpd.StylePriority.UseTextAlignment = False
        Me.LBTotByExpd.Text = "Tot By Expedisi"
        Me.LBTotByExpd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.LBTotByExpd.Visible = False
        '
        'LBTotSbDisc
        '
        Me.LBTotSbDisc.Dpi = 254.0!
        Me.LBTotSbDisc.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotSbDisc.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 15.99999!)
        Me.LBTotSbDisc.Name = "LBTotSbDisc"
        Me.LBTotSbDisc.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotSbDisc.SizeF = New System.Drawing.SizeF(344.0804!, 48.00001!)
        Me.LBTotSbDisc.StylePriority.UseFont = False
        Me.LBTotSbDisc.StylePriority.UseTextAlignment = False
        Me.LBTotSbDisc.Text = "Tot Sb Disc"
        Me.LBTotSbDisc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotDiscDet
        '
        Me.LBTotDiscDet.Dpi = 254.0!
        Me.LBTotDiscDet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDiscDet.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 105.4874!)
        Me.LBTotDiscDet.Name = "LBTotDiscDet"
        Me.LBTotDiscDet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDiscDet.SizeF = New System.Drawing.SizeF(344.0804!, 48.0!)
        Me.LBTotDiscDet.StylePriority.UseFont = False
        Me.LBTotDiscDet.StylePriority.UseTextAlignment = False
        Me.LBTotDiscDet.Text = "Tot Disc Det"
        Me.LBTotDiscDet.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotDiscH
        '
        Me.LBTotDiscH.Dpi = 254.0!
        Me.LBTotDiscH.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDiscH.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 182.3629!)
        Me.LBTotDiscH.Name = "LBTotDiscH"
        Me.LBTotDiscH.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDiscH.SizeF = New System.Drawing.SizeF(344.0804!, 48.0!)
        Me.LBTotDiscH.StylePriority.UseFont = False
        Me.LBTotDiscH.StylePriority.UseTextAlignment = False
        Me.LBTotDiscH.Text = "Tot Disc Header"
        Me.LBTotDiscH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.LBTotDiscH.Visible = False
        '
        'LBTotAkhir
        '
        Me.LBTotAkhir.Dpi = 254.0!
        Me.LBTotAkhir.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotAkhir.LocationFloat = New DevExpress.Utils.PointFloat(1720.971!, 338.1129!)
        Me.LBTotAkhir.Name = "LBTotAkhir"
        Me.LBTotAkhir.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotAkhir.SizeF = New System.Drawing.SizeF(344.0804!, 47.99997!)
        Me.LBTotAkhir.StylePriority.UseFont = False
        Me.LBTotAkhir.StylePriority.UseTextAlignment = False
        Me.LBTotAkhir.Text = "Tot Akhir"
        Me.LBTotAkhir.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.LBTotAkhir.Visible = False
        '
        'LBTotDos
        '
        Me.LBTotDos.Dpi = 254.0!
        Me.LBTotDos.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDos.LocationFloat = New DevExpress.Utils.PointFloat(919.1163!, 16.0!)
        Me.LBTotDos.Name = "LBTotDos"
        Me.LBTotDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDos.SizeF = New System.Drawing.SizeF(102.9785!, 48.0!)
        Me.LBTotDos.StylePriority.UseFont = False
        Me.LBTotDos.StylePriority.UseTextAlignment = False
        Me.LBTotDos.Text = "Dos"
        Me.LBTotDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotPsg
        '
        Me.LBTotPsg.Dpi = 254.0!
        Me.LBTotPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotPsg.LocationFloat = New DevExpress.Utils.PointFloat(1022.095!, 16.0!)
        Me.LBTotPsg.Name = "LBTotPsg"
        Me.LBTotPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotPsg.SizeF = New System.Drawing.SizeF(157.1238!, 48.0!)
        Me.LBTotPsg.StylePriority.UseFont = False
        Me.LBTotPsg.StylePriority.UseTextAlignment = False
        Me.LBTotPsg.Text = "Psg"
        Me.LBTotPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 0.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XRFtBJ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter, Me.ReportFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBJualID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBAlamat As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHarAkhir As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBHarSat As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBarang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDisc As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotSbDisc As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDiscDet As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDiscH As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotAkhir As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotByExpd As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDueDate As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
End Class
