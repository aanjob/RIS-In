<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRPersenBB
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
        Me.LBSalGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSalTdkGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotSal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPersenTdkGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBJenis = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPersenGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPeriod = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine7 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine6 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GHJnsPers = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBGHJnsPers = New DevExpress.XtraReports.UI.XRLabel()
        Me.GFJnsPers = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.LBGFJnsPers = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBGFTotSal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGFTdkGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGFGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBRFGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBRFTdkGerak = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBRFTotSal = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine4 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine5 = New DevExpress.XtraReports.UI.XRLine()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSalGerak, Me.LBSalTdkGerak, Me.LBTotSal, Me.LBPersenTdkGerak, Me.LBNo, Me.LBJenis, Me.LBPersenGerak})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 45.0!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSalGerak
        '
        Me.LBSalGerak.Dpi = 254.0!
        Me.LBSalGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBSalGerak.LocationFloat = New DevExpress.Utils.PointFloat(615.9652!, 0.0!)
        Me.LBSalGerak.Name = "LBSalGerak"
        Me.LBSalGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSalGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBSalGerak.StylePriority.UseFont = False
        Me.LBSalGerak.StylePriority.UseTextAlignment = False
        Me.LBSalGerak.Text = "Saldo Bergerak"
        Me.LBSalGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBSalTdkGerak
        '
        Me.LBSalTdkGerak.Dpi = 254.0!
        Me.LBSalTdkGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBSalTdkGerak.LocationFloat = New DevExpress.Utils.PointFloat(1121.24!, 0.0!)
        Me.LBSalTdkGerak.Name = "LBSalTdkGerak"
        Me.LBSalTdkGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSalTdkGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBSalTdkGerak.StylePriority.UseFont = False
        Me.LBSalTdkGerak.StylePriority.UseTextAlignment = False
        Me.LBSalTdkGerak.Text = "Saldo Tidak Bergerak"
        Me.LBSalTdkGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotSal
        '
        Me.LBTotSal.Dpi = 254.0!
        Me.LBTotSal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotSal.ForeColor = System.Drawing.Color.Black
        Me.LBTotSal.LocationFloat = New DevExpress.Utils.PointFloat(1626.834!, 0.0!)
        Me.LBTotSal.Name = "LBTotSal"
        Me.LBTotSal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotSal.SizeF = New System.Drawing.SizeF(408.4377!, 45.0!)
        Me.LBTotSal.StylePriority.UseFont = False
        Me.LBTotSal.StylePriority.UseForeColor = False
        Me.LBTotSal.StylePriority.UseTextAlignment = False
        Me.LBTotSal.Text = "Saldo Akhir"
        Me.LBTotSal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBPersenTdkGerak
        '
        Me.LBPersenTdkGerak.Dpi = 254.0!
        Me.LBPersenTdkGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBPersenTdkGerak.LocationFloat = New DevExpress.Utils.PointFloat(1484.698!, 0.0!)
        Me.LBPersenTdkGerak.Name = "LBPersenTdkGerak"
        Me.LBPersenTdkGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPersenTdkGerak.SizeF = New System.Drawing.SizeF(141.2084!, 45.0!)
        Me.LBPersenTdkGerak.StylePriority.UseFont = False
        Me.LBPersenTdkGerak.StylePriority.UseTextAlignment = False
        Me.LBPersenTdkGerak.Text = "%"
        Me.LBPersenTdkGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(75.30779!, 45.0!)
        Me.LBNo.StylePriority.UseFont = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBNo.Summary = XrSummary1
        Me.LBNo.Text = "No."
        '
        'LBJenis
        '
        Me.LBJenis.Dpi = 254.0!
        Me.LBJenis.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBJenis.LocationFloat = New DevExpress.Utils.PointFloat(100.3077!, 0.0!)
        Me.LBJenis.Name = "LBJenis"
        Me.LBJenis.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJenis.SizeF = New System.Drawing.SizeF(514.7501!, 45.0!)
        Me.LBJenis.StylePriority.UseFont = False
        Me.LBJenis.Text = "Jenis Bahan"
        '
        'LBPersenGerak
        '
        Me.LBPersenGerak.Dpi = 254.0!
        Me.LBPersenGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBPersenGerak.LocationFloat = New DevExpress.Utils.PointFloat(979.4235!, 0.0!)
        Me.LBPersenGerak.Name = "LBPersenGerak"
        Me.LBPersenGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPersenGerak.SizeF = New System.Drawing.SizeF(141.2086!, 45.0!)
        Me.LBPersenGerak.StylePriority.UseFont = False
        Me.LBPersenGerak.StylePriority.UseTextAlignment = False
        Me.LBPersenGerak.Text = "%"
        Me.LBPersenGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
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
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 0.0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LBPerusahaan
        '
        Me.LBPerusahaan.Dpi = 254.0!
        Me.LBPerusahaan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(1394.172!, 45.0!)
        Me.LBPerusahaan.StylePriority.UseFont = False
        Me.LBPerusahaan.StylePriority.UseTextAlignment = False
        Me.LBPerusahaan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 45.93583!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2010.084!, 50.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Persentase Saldo Akhir Bahan"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBGudang
        '
        Me.LBGudang.Dpi = 254.0!
        Me.LBGudang.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 95.99987!)
        Me.LBGudang.Name = "LBGudang"
        Me.LBGudang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudang.SizeF = New System.Drawing.SizeF(2010.084!, 50.0!)
        Me.LBGudang.StylePriority.UseFont = False
        Me.LBGudang.StylePriority.UseTextAlignment = False
        Me.LBGudang.Text = "Gudang"
        Me.LBGudang.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBPeriod
        '
        Me.LBPeriod.Dpi = 254.0!
        Me.LBPeriod.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBPeriod.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 146.5625!)
        Me.LBPeriod.Name = "LBPeriod"
        Me.LBPeriod.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPeriod.SizeF = New System.Drawing.SizeF(2010.084!, 50.0!)
        Me.LBPeriod.StylePriority.UseFont = False
        Me.LBPeriod.StylePriority.UseTextAlignment = False
        Me.LBPeriod.Text = "Periode"
        Me.LBPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel16, Me.XrLabel18, Me.XrLabel21, Me.XrLabel22, Me.XrLabel23, Me.XrLine7, Me.XrLabel6, Me.XrLine6, Me.XrLabel2, Me.LBPerusahaan, Me.LBGudang, Me.XrLabel1, Me.LBPeriod})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 309.3919!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(979.4234!, 258.0103!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(141.2086!, 45.0!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "%"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(100.3077!, 258.0103!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(514.7501!, 45.0!)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.Text = "Jenis Bahan"
        '
        'XrLabel21
        '
        Me.XrLabel21.Dpi = 254.0!
        Me.XrLabel21.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(24.99993!, 258.0103!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(75.30779!, 45.0!)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.Text = "No."
        '
        'XrLabel22
        '
        Me.XrLabel22.Dpi = 254.0!
        Me.XrLabel22.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(1484.698!, 258.0103!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(141.2084!, 45.0!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "%"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel23
        '
        Me.XrLabel23.Dpi = 254.0!
        Me.XrLabel23.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel23.ForeColor = System.Drawing.Color.Black
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(1626.834!, 258.0103!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(408.4377!, 45.0!)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseForeColor = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "Saldo Akhir"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine7
        '
        Me.XrLine7.Dpi = 254.0!
        Me.XrLine7.LineWidth = 3
        Me.XrLine7.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 252.0!)
        Me.XrLine7.Name = "XrLine7"
        Me.XrLine7.SizeF = New System.Drawing.SizeF(2011.016!, 5.746177!)
        '
        'XrLabel6
        '
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(1121.24!, 258.0103!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "Saldo Tidak Bergerak"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine6
        '
        Me.XrLine6.Dpi = 254.0!
        Me.XrLine6.LineWidth = 3
        Me.XrLine6.LocationFloat = New DevExpress.Utils.PointFloat(24.99993!, 303.6457!)
        Me.XrLine6.Name = "XrLine6"
        Me.XrLine6.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(615.9651!, 258.0103!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "Saldo Bergerak"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'GHJnsPers
        '
        Me.GHJnsPers.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine2, Me.LBGHJnsPers})
        Me.GHJnsPers.Dpi = 254.0!
        Me.GHJnsPers.HeightF = 50.74618!
        Me.GHJnsPers.Name = "GHJnsPers"
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(24.99988!, 45.0!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'LBGHJnsPers
        '
        Me.LBGHJnsPers.Dpi = 254.0!
        Me.LBGHJnsPers.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBGHJnsPers.LocationFloat = New DevExpress.Utils.PointFloat(100.3077!, 0.0!)
        Me.LBGHJnsPers.Name = "LBGHJnsPers"
        Me.LBGHJnsPers.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGHJnsPers.SizeF = New System.Drawing.SizeF(1934.776!, 45.0!)
        Me.LBGHJnsPers.StylePriority.UseFont = False
        Me.LBGHJnsPers.Text = "Jenis Persediaan"
        '
        'GFJnsPers
        '
        Me.GFJnsPers.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFJnsPers, Me.XrLine3, Me.XrLine1, Me.LBGFTotSal, Me.LBGFTdkGerak, Me.LBGFGerak})
        Me.GFJnsPers.Dpi = 254.0!
        Me.GFJnsPers.HeightF = 56.49236!
        Me.GFJnsPers.Name = "GFJnsPers"
        '
        'LBGFJnsPers
        '
        Me.LBGFJnsPers.Dpi = 254.0!
        Me.LBGFJnsPers.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBGFJnsPers.LocationFloat = New DevExpress.Utils.PointFloat(100.3077!, 5.746177!)
        Me.LBGFJnsPers.Name = "LBGFJnsPers"
        Me.LBGFJnsPers.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGFJnsPers.SizeF = New System.Drawing.SizeF(490.0466!, 45.0!)
        Me.LBGFJnsPers.StylePriority.UseFont = False
        Me.LBGFJnsPers.StylePriority.UseTextAlignment = False
        Me.LBGFJnsPers.Text = "Total Jns Pers"
        Me.LBGFJnsPers.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 50.74618!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 0.0!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'LBGFTotSal
        '
        Me.LBGFTotSal.Dpi = 254.0!
        Me.LBGFTotSal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBGFTotSal.ForeColor = System.Drawing.Color.Black
        Me.LBGFTotSal.LocationFloat = New DevExpress.Utils.PointFloat(1626.834!, 5.746177!)
        Me.LBGFTotSal.Name = "LBGFTotSal"
        Me.LBGFTotSal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGFTotSal.SizeF = New System.Drawing.SizeF(408.4377!, 45.0!)
        Me.LBGFTotSal.StylePriority.UseFont = False
        Me.LBGFTotSal.StylePriority.UseForeColor = False
        Me.LBGFTotSal.StylePriority.UseTextAlignment = False
        Me.LBGFTotSal.Text = "Saldo Akhir"
        Me.LBGFTotSal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBGFTdkGerak
        '
        Me.LBGFTdkGerak.Dpi = 254.0!
        Me.LBGFTdkGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBGFTdkGerak.LocationFloat = New DevExpress.Utils.PointFloat(1121.24!, 5.746177!)
        Me.LBGFTdkGerak.Name = "LBGFTdkGerak"
        Me.LBGFTdkGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGFTdkGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBGFTdkGerak.StylePriority.UseFont = False
        Me.LBGFTdkGerak.StylePriority.UseTextAlignment = False
        Me.LBGFTdkGerak.Text = "Saldo Tidak Bergerak"
        Me.LBGFTdkGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBGFGerak
        '
        Me.LBGFGerak.Dpi = 254.0!
        Me.LBGFGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBGFGerak.LocationFloat = New DevExpress.Utils.PointFloat(615.9651!, 5.746177!)
        Me.LBGFGerak.Name = "LBGFGerak"
        Me.LBGFGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGFGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBGFGerak.StylePriority.UseFont = False
        Me.LBGFGerak.StylePriority.UseTextAlignment = False
        Me.LBGFGerak.Text = "Saldo Bergerak"
        Me.LBGFGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBUser, Me.XrPageInfo2, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 72.83669!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBUser
        '
        Me.LBUser.Dpi = 254.0!
        Me.LBUser.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 24.99993!)
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
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(212.0001!, 24.99993!)
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
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 25.00009!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(2011.008!, 47.8366!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.LBRFGerak, Me.LBRFTdkGerak, Me.LBRFTotSal, Me.XrLine4, Me.XrLine5})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 58.1211!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(100.3077!, 6.000038!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(490.0466!, 45.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Total Saldo Akhir"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBRFGerak
        '
        Me.LBRFGerak.Dpi = 254.0!
        Me.LBRFGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBRFGerak.LocationFloat = New DevExpress.Utils.PointFloat(615.9651!, 6.0!)
        Me.LBRFGerak.Name = "LBRFGerak"
        Me.LBRFGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBRFGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBRFGerak.StylePriority.UseFont = False
        Me.LBRFGerak.StylePriority.UseTextAlignment = False
        Me.LBRFGerak.Text = "Saldo Bergerak"
        Me.LBRFGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBRFTdkGerak
        '
        Me.LBRFTdkGerak.Dpi = 254.0!
        Me.LBRFTdkGerak.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBRFTdkGerak.LocationFloat = New DevExpress.Utils.PointFloat(1121.24!, 6.0!)
        Me.LBRFTdkGerak.Name = "LBRFTdkGerak"
        Me.LBRFTdkGerak.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBRFTdkGerak.SizeF = New System.Drawing.SizeF(363.4583!, 45.0!)
        Me.LBRFTdkGerak.StylePriority.UseFont = False
        Me.LBRFTdkGerak.StylePriority.UseTextAlignment = False
        Me.LBRFTdkGerak.Text = "Saldo Tidak Bergerak"
        Me.LBRFTdkGerak.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBRFTotSal
        '
        Me.LBRFTotSal.Dpi = 254.0!
        Me.LBRFTotSal.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBRFTotSal.ForeColor = System.Drawing.Color.Black
        Me.LBRFTotSal.LocationFloat = New DevExpress.Utils.PointFloat(1626.834!, 6.0!)
        Me.LBRFTotSal.Name = "LBRFTotSal"
        Me.LBRFTotSal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBRFTotSal.SizeF = New System.Drawing.SizeF(408.4377!, 45.0!)
        Me.LBRFTotSal.StylePriority.UseFont = False
        Me.LBRFTotSal.StylePriority.UseForeColor = False
        Me.LBRFTotSal.StylePriority.UseTextAlignment = False
        Me.LBRFTotSal.Text = "Saldo Akhir"
        Me.LBRFTotSal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine4
        '
        Me.XrLine4.Dpi = 254.0!
        Me.XrLine4.LineWidth = 3
        Me.XrLine4.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 0.2538605!)
        Me.XrLine4.Name = "XrLine4"
        Me.XrLine4.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'XrLine5
        '
        Me.XrLine5.Dpi = 254.0!
        Me.XrLine5.LineWidth = 3
        Me.XrLine5.LocationFloat = New DevExpress.Utils.PointFloat(25.00001!, 51.25386!)
        Me.XrLine5.Name = "XrLine5"
        Me.XrLine5.SizeF = New System.Drawing.SizeF(2011.016!, 5.746178!)
        '
        'XRPersenBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.GHJnsPers, Me.GFJnsPers, Me.PageFooter, Me.ReportFooter})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margins = New System.Drawing.Printing.Margins(25, 35, 35, 55)
        Me.PageHeight = 2794
        Me.PageWidth = 2159
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
    Friend WithEvents GHJnsPers As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GFJnsPers As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPeriod As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine7 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine6 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBGHJnsPers As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSalGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSalTdkGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotSal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPersenTdkGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBJenis As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPersenGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBGFTotSal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGFTdkGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGFGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents LBRFGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBRFTdkGerak As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBRFTotSal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine5 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGFJnsPers As DevExpress.XtraReports.UI.XRLabel
End Class
