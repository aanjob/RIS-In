<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRBCMutBJ
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
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtCode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtName = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSat = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSA = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBMasuk = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKeluar = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSAkh = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGdID = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.LBPeriod = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        Me.XrCrossBandLine22 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine21 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine20 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine19 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine18 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine17 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine16 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        Me.XrCrossBandLine15 = New DevExpress.XtraReports.UI.XRCrossBandLine()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel8, Me.LBArtCode, Me.LBArtName, Me.LBSat, Me.LBSA, Me.LBMasuk, Me.LBKeluar, Me.LBSAkh, Me.LBGdID})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 47.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(49.80712!, 0.0!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(81.06796!, 47.0!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel8.Summary = XrSummary1
        Me.XrLabel8.Text = "No."
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBArtCode
        '
        Me.LBArtCode.Dpi = 254.0!
        Me.LBArtCode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBArtCode.LocationFloat = New DevExpress.Utils.PointFloat(135.8071!, 0.0!)
        Me.LBArtCode.Name = "LBArtCode"
        Me.LBArtCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtCode.SizeF = New System.Drawing.SizeF(303.8921!, 47.0!)
        Me.LBArtCode.StylePriority.UseFont = False
        Me.LBArtCode.StylePriority.UseTextAlignment = False
        Me.LBArtCode.Text = "Kode Barang"
        Me.LBArtCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBArtName
        '
        Me.LBArtName.Dpi = 254.0!
        Me.LBArtName.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBArtName.LocationFloat = New DevExpress.Utils.PointFloat(443.2471!, 0.0!)
        Me.LBArtName.Name = "LBArtName"
        Me.LBArtName.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtName.SizeF = New System.Drawing.SizeF(1242.921!, 47.0!)
        Me.LBArtName.StylePriority.UseFont = False
        Me.LBArtName.StylePriority.UseTextAlignment = False
        Me.LBArtName.Text = "Nama Barang"
        Me.LBArtName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSat
        '
        Me.LBSat.Dpi = 254.0!
        Me.LBSat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBSat.LocationFloat = New DevExpress.Utils.PointFloat(1689.802!, 0.0!)
        Me.LBSat.Name = "LBSat"
        Me.LBSat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSat.SizeF = New System.Drawing.SizeF(169.2852!, 47.0!)
        Me.LBSat.StylePriority.UseFont = False
        Me.LBSat.StylePriority.UseTextAlignment = False
        Me.LBSat.Text = "Sat"
        Me.LBSat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSA
        '
        Me.LBSA.Dpi = 254.0!
        Me.LBSA.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBSA.LocationFloat = New DevExpress.Utils.PointFloat(1863.852!, 0.0!)
        Me.LBSA.Name = "LBSA"
        Me.LBSA.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSA.SizeF = New System.Drawing.SizeF(223.1898!, 47.0!)
        Me.LBSA.StylePriority.UseFont = False
        Me.LBSA.StylePriority.UseTextAlignment = False
        Me.LBSA.Text = "Saldo Awal"
        Me.LBSA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBMasuk
        '
        Me.LBMasuk.Dpi = 254.0!
        Me.LBMasuk.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBMasuk.LocationFloat = New DevExpress.Utils.PointFloat(2090.935!, 0.0!)
        Me.LBMasuk.Name = "LBMasuk"
        Me.LBMasuk.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBMasuk.SizeF = New System.Drawing.SizeF(223.1895!, 47.0!)
        Me.LBMasuk.StylePriority.UseFont = False
        Me.LBMasuk.StylePriority.UseTextAlignment = False
        Me.LBMasuk.Text = "Pemasukan"
        Me.LBMasuk.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBKeluar
        '
        Me.LBKeluar.Dpi = 254.0!
        Me.LBKeluar.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBKeluar.LocationFloat = New DevExpress.Utils.PointFloat(2318.018!, 0.0!)
        Me.LBKeluar.Name = "LBKeluar"
        Me.LBKeluar.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKeluar.SizeF = New System.Drawing.SizeF(233.7729!, 47.0!)
        Me.LBKeluar.StylePriority.UseFont = False
        Me.LBKeluar.StylePriority.UseTextAlignment = False
        Me.LBKeluar.Text = "Pengeluaran"
        Me.LBKeluar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSAkh
        '
        Me.LBSAkh.Dpi = 254.0!
        Me.LBSAkh.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBSAkh.LocationFloat = New DevExpress.Utils.PointFloat(2554.977!, 0.0!)
        Me.LBSAkh.Name = "LBSAkh"
        Me.LBSAkh.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSAkh.SizeF = New System.Drawing.SizeF(233.7729!, 47.0!)
        Me.LBSAkh.StylePriority.UseFont = False
        Me.LBSAkh.StylePriority.UseTextAlignment = False
        Me.LBSAkh.Text = "Saldo Akhir"
        Me.LBSAkh.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBGdID
        '
        Me.LBGdID.Dpi = 254.0!
        Me.LBGdID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!)
        Me.LBGdID.LocationFloat = New DevExpress.Utils.PointFloat(2795.019!, 0.0!)
        Me.LBGdID.Name = "LBGdID"
        Me.LBGdID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGdID.SizeF = New System.Drawing.SizeF(201.0349!, 47.0!)
        Me.LBGdID.StylePriority.UseFont = False
        Me.LBGdID.StylePriority.UseTextAlignment = False
        Me.LBGdID.Text = "Gudang"
        Me.LBGdID.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
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
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBPeriod, Me.XrLabel1, Me.LBPerusahaan})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 216.9583!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LBPeriod
        '
        Me.LBPeriod.Dpi = 254.0!
        Me.LBPeriod.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPeriod.LocationFloat = New DevExpress.Utils.PointFloat(45.51547!, 115.4167!)
        Me.LBPeriod.Name = "LBPeriod"
        Me.LBPeriod.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPeriod.SizeF = New System.Drawing.SizeF(969.8853!, 52.41669!)
        Me.LBPeriod.StylePriority.UseFont = False
        Me.LBPeriod.StylePriority.UseTextAlignment = False
        Me.LBPeriod.Text = "Periode"
        Me.LBPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(45.51547!, 0.0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(969.8853!, 62.99999!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "LAPORAN MUTASI HASIL PRODUKSI"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(45.51547!, 63.00003!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(969.8856!, 52.41669!)
        Me.LBPerusahaan.StylePriority.UseFont = False
        Me.LBPerusahaan.StylePriority.UseTextAlignment = False
        Me.LBPerusahaan.Text = "Perusahaan"
        Me.LBPerusahaan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrLabel2, Me.XrLabel11, Me.XrLabel13, Me.XrLabel12})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 63.63861!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(44.28654!, 58.25725!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2955.767!, 5.381355!)
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(49.80712!, 5.751445!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(81.06796!, 52.41669!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "No."
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(2795.019!, 5.751445!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(201.0352!, 52.41668!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Gudang"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(2554.977!, 5.751445!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(233.7729!, 52.41669!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "Saldo Akhir"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(2318.018!, 5.751445!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(233.7729!, 52.41668!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "Pengeluaran"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(2090.935!, 5.751445!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(223.1895!, 52.41669!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Pemasukan"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1863.852!, 5.751445!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(223.1898!, 52.41669!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "Saldo Awal"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(135.8071!, 5.751452!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(303.8922!, 52.4167!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "Kode Barang"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel13
        '
        Me.XrLabel13.Dpi = 254.0!
        Me.XrLabel13.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(1689.802!, 5.751425!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(169.2852!, 52.41668!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "Sat"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(443.2471!, 5.751452!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(1242.921!, 52.41668!)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "Nama Barang"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.LBUser, Me.XrPageInfo2})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 78.68365!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(2712.95!, 33.49279!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(283.1042!, 45.19086!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(44.28654!, 30.84695!)
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
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(229.7868!, 30.84695!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(378.3541!, 47.83661!)
        Me.XrPageInfo2.StylePriority.UseFont = False
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.BorderWidth = 2.0!
        Me.XrCrossBandBox1.Dpi = 254.0!
        Me.XrCrossBandBox1.EndBand = Me.PageFooter
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(41.38919!, 5.000039!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(41.38919!, 0.0!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.PageHeader
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(41.38919!, 0.0!)
        Me.XrCrossBandBox1.WidthF = 2960.664!
        '
        'XrCrossBandLine22
        '
        Me.XrCrossBandLine22.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine22.Dpi = 254.0!
        Me.XrCrossBandLine22.EndBand = Me.PageFooter
        Me.XrCrossBandLine22.EndPointFloat = New DevExpress.Utils.PointFloat(2790.039!, 3.960968!)
        Me.XrCrossBandLine22.LocationFloat = New DevExpress.Utils.PointFloat(2790.039!, 2.803426!)
        Me.XrCrossBandLine22.Name = "XrCrossBandLine22"
        Me.XrCrossBandLine22.StartBand = Me.PageHeader
        Me.XrCrossBandLine22.StartPointFloat = New DevExpress.Utils.PointFloat(2790.039!, 2.803426!)
        Me.XrCrossBandLine22.WidthF = 3.0!
        '
        'XrCrossBandLine21
        '
        Me.XrCrossBandLine21.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine21.Dpi = 254.0!
        Me.XrCrossBandLine21.EndBand = Me.PageFooter
        Me.XrCrossBandLine21.EndPointFloat = New DevExpress.Utils.PointFloat(2314.789!, 2.472641!)
        Me.XrCrossBandLine21.LocationFloat = New DevExpress.Utils.PointFloat(2314.789!, 1.526115!)
        Me.XrCrossBandLine21.Name = "XrCrossBandLine21"
        Me.XrCrossBandLine21.StartBand = Me.PageHeader
        Me.XrCrossBandLine21.StartPointFloat = New DevExpress.Utils.PointFloat(2314.789!, 1.526115!)
        Me.XrCrossBandLine21.WidthF = 3.0!
        '
        'XrCrossBandLine20
        '
        Me.XrCrossBandLine20.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine20.Dpi = 254.0!
        Me.XrCrossBandLine20.EndBand = Me.PageFooter
        Me.XrCrossBandLine20.EndPointFloat = New DevExpress.Utils.PointFloat(2087.058!, 2.472641!)
        Me.XrCrossBandLine20.LocationFloat = New DevExpress.Utils.PointFloat(2087.058!, 3.014396!)
        Me.XrCrossBandLine20.Name = "XrCrossBandLine20"
        Me.XrCrossBandLine20.StartBand = Me.PageHeader
        Me.XrCrossBandLine20.StartPointFloat = New DevExpress.Utils.PointFloat(2087.058!, 3.014396!)
        Me.XrCrossBandLine20.WidthF = 3.0!
        '
        'XrCrossBandLine19
        '
        Me.XrCrossBandLine19.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine19.Dpi = 254.0!
        Me.XrCrossBandLine19.EndBand = Me.PageFooter
        Me.XrCrossBandLine19.EndPointFloat = New DevExpress.Utils.PointFloat(1859.863!, 0.0!)
        Me.XrCrossBandLine19.LocationFloat = New DevExpress.Utils.PointFloat(1859.863!, 4.464844!)
        Me.XrCrossBandLine19.Name = "XrCrossBandLine19"
        Me.XrCrossBandLine19.StartBand = Me.PageHeader
        Me.XrCrossBandLine19.StartPointFloat = New DevExpress.Utils.PointFloat(1859.863!, 4.464844!)
        Me.XrCrossBandLine19.WidthF = 3.0!
        '
        'XrCrossBandLine18
        '
        Me.XrCrossBandLine18.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine18.Dpi = 254.0!
        Me.XrCrossBandLine18.EndBand = Me.PageFooter
        Me.XrCrossBandLine18.EndPointFloat = New DevExpress.Utils.PointFloat(1686.387!, 3.960923!)
        Me.XrCrossBandLine18.LocationFloat = New DevExpress.Utils.PointFloat(1686.387!, 2.894627!)
        Me.XrCrossBandLine18.Name = "XrCrossBandLine18"
        Me.XrCrossBandLine18.StartBand = Me.PageHeader
        Me.XrCrossBandLine18.StartPointFloat = New DevExpress.Utils.PointFloat(1686.387!, 2.894627!)
        Me.XrCrossBandLine18.WidthF = 3.0!
        '
        'XrCrossBandLine17
        '
        Me.XrCrossBandLine17.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine17.Dpi = 254.0!
        Me.XrCrossBandLine17.EndBand = Me.PageFooter
        Me.XrCrossBandLine17.EndPointFloat = New DevExpress.Utils.PointFloat(439.6992!, 4.449204!)
        Me.XrCrossBandLine17.LocationFloat = New DevExpress.Utils.PointFloat(439.6992!, 4.87119!)
        Me.XrCrossBandLine17.Name = "XrCrossBandLine17"
        Me.XrCrossBandLine17.StartBand = Me.PageHeader
        Me.XrCrossBandLine17.StartPointFloat = New DevExpress.Utils.PointFloat(439.6992!, 4.87119!)
        Me.XrCrossBandLine17.WidthF = 3.0!
        '
        'XrCrossBandLine16
        '
        Me.XrCrossBandLine16.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine16.Dpi = 254.0!
        Me.XrCrossBandLine16.EndBand = Me.PageFooter
        Me.XrCrossBandLine16.EndPointFloat = New DevExpress.Utils.PointFloat(131.4805!, 2.19536!)
        Me.XrCrossBandLine16.LocationFloat = New DevExpress.Utils.PointFloat(131.4805!, 5.953125!)
        Me.XrCrossBandLine16.Name = "XrCrossBandLine16"
        Me.XrCrossBandLine16.StartBand = Me.PageHeader
        Me.XrCrossBandLine16.StartPointFloat = New DevExpress.Utils.PointFloat(131.4805!, 5.953125!)
        Me.XrCrossBandLine16.WidthF = 3.0!
        '
        'XrCrossBandLine15
        '
        Me.XrCrossBandLine15.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.XrCrossBandLine15.Dpi = 254.0!
        Me.XrCrossBandLine15.EndBand = Me.PageFooter
        Me.XrCrossBandLine15.EndPointFloat = New DevExpress.Utils.PointFloat(2551.914!, 0.9843603!)
        Me.XrCrossBandLine15.LocationFloat = New DevExpress.Utils.PointFloat(2551.914!, 4.263185!)
        Me.XrCrossBandLine15.Name = "XrCrossBandLine15"
        Me.XrCrossBandLine15.StartBand = Me.PageHeader
        Me.XrCrossBandLine15.StartPointFloat = New DevExpress.Utils.PointFloat(2551.914!, 4.263185!)
        Me.XrCrossBandLine15.WidthF = 3.0!
        '
        'XRBCMutBJ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.PageFooter})
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandLine16, Me.XrCrossBandLine17, Me.XrCrossBandLine18, Me.XrCrossBandLine19, Me.XrCrossBandLine20, Me.XrCrossBandLine21, Me.XrCrossBandLine15, Me.XrCrossBandLine22, Me.XrCrossBandBox1})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 2159
        Me.PageWidth = 3100
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
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
    Friend WithEvents LBPeriod As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandLine22 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine21 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine20 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine19 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine18 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine17 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine16 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrCrossBandLine15 As DevExpress.XtraReports.UI.XRCrossBandLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtCode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSat As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSA As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBMasuk As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKeluar As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSAkh As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGdID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
End Class
