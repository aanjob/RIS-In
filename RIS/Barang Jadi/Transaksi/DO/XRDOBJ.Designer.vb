<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRDOBJ
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
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRDOBJ))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBRow = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDos = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBArtCode = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.LBAlamat = New DevExpress.XtraReports.UI.XRRichText()
        Me.LBGudangTj = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDOID = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.LBSJ = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotPsg = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTotDos = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBAlamat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBRow, Me.LBPsg, Me.LBDos, Me.LBArtCode, Me.LBNo})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 52.91667!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBRow
        '
        Me.LBRow.Dpi = 254.0!
        Me.LBRow.Font = New System.Drawing.Font("Abadi MT Condensed Light", 9.0!, System.Drawing.FontStyle.Bold)
        Me.LBRow.ForeColor = System.Drawing.Color.Blue
        Me.LBRow.LocationFloat = New DevExpress.Utils.PointFloat(5.0!, 2.809098!)
        Me.LBRow.Name = "LBRow"
        Me.LBRow.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBRow.SizeF = New System.Drawing.SizeF(65.17899!, 45.19086!)
        Me.LBRow.StylePriority.UseFont = False
        Me.LBRow.StylePriority.UseForeColor = False
        Me.LBRow.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom
        Me.LBRow.Summary = XrSummary1
        Me.LBRow.Text = "Row"
        Me.LBRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        Me.LBRow.Visible = False
        '
        'LBPsg
        '
        Me.LBPsg.Dpi = 254.0!
        Me.LBPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBPsg.LocationFloat = New DevExpress.Utils.PointFloat(1842.021!, 0.0!)
        Me.LBPsg.Name = "LBPsg"
        Me.LBPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPsg.SizeF = New System.Drawing.SizeF(198.0!, 48.0!)
        Me.LBPsg.StylePriority.UseFont = False
        Me.LBPsg.StylePriority.UseTextAlignment = False
        Me.LBPsg.Text = "Psg"
        Me.LBPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBDos
        '
        Me.LBDos.Dpi = 254.0!
        Me.LBDos.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBDos.LocationFloat = New DevExpress.Utils.PointFloat(1643.958!, 0.0!)
        Me.LBDos.Name = "LBDos"
        Me.LBDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDos.SizeF = New System.Drawing.SizeF(198.0!, 48.0!)
        Me.LBDos.StylePriority.UseFont = False
        Me.LBDos.StylePriority.UseTextAlignment = False
        Me.LBDos.Text = "Dos"
        Me.LBDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBArtCode
        '
        Me.LBArtCode.Dpi = 254.0!
        Me.LBArtCode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBArtCode.LocationFloat = New DevExpress.Utils.PointFloat(204.3542!, 0.0!)
        Me.LBArtCode.Name = "LBArtCode"
        Me.LBArtCode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBArtCode.SizeF = New System.Drawing.SizeF(1439.604!, 48.0!)
        Me.LBArtCode.StylePriority.UseFont = False
        Me.LBArtCode.Text = "Art Code"
        '
        'LBNo
        '
        Me.LBNo.Dpi = 254.0!
        Me.LBNo.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(99.0!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(80.00001!, 48.0!)
        Me.LBNo.StylePriority.UseFont = False
        XrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBNo.Summary = XrSummary2
        Me.LBNo.Text = "No."
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBAlamat, Me.LBGudangTj, Me.LBDOID, Me.LBKode})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 390.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'LBAlamat
        '
        Me.LBAlamat.Dpi = 254.0!
        Me.LBAlamat.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBAlamat.LocationFloat = New DevExpress.Utils.PointFloat(1284.0!, 149.1909!)
        Me.LBAlamat.Name = "LBAlamat"
        Me.LBAlamat.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBAlamat.SerializableRtfString = resources.GetString("LBAlamat.SerializableRtfString")
        Me.LBAlamat.SizeF = New System.Drawing.SizeF(853.6979!, 100.9859!)
        Me.LBAlamat.StylePriority.UseFont = False
        Me.LBAlamat.StylePriority.UsePadding = False
        '
        'LBGudangTj
        '
        Me.LBGudangTj.Dpi = 254.0!
        Me.LBGudangTj.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBGudangTj.LocationFloat = New DevExpress.Utils.PointFloat(1284.0!, 101.0!)
        Me.LBGudangTj.Name = "LBGudangTj"
        Me.LBGudangTj.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudangTj.SizeF = New System.Drawing.SizeF(510.5623!, 48.0!)
        Me.LBGudangTj.StylePriority.UseFont = False
        Me.LBGudangTj.Text = "Gudang Tujuan"
        '
        'LBDOID
        '
        Me.LBDOID.Dpi = 254.0!
        Me.LBDOID.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBDOID.LocationFloat = New DevExpress.Utils.PointFloat(424.0!, 181.8091!)
        Me.LBDOID.Name = "LBDOID"
        Me.LBDOID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDOID.SizeF = New System.Drawing.SizeF(510.5623!, 48.0!)
        Me.LBDOID.StylePriority.UseFont = False
        Me.LBDOID.Text = "Nomor"
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(424.0!, 133.5087!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(510.5623!, 48.0!)
        Me.LBKode.StylePriority.UseFont = False
        Me.LBKode.Text = "Nomor"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Format = "Page {0} of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(953.8542!, 636.58!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(254.0!, 58.42!)
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSJ, Me.LBTanggal, Me.LBKet, Me.XrLabel8, Me.LBTotPsg, Me.LBTotDos, Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 720.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'LBSJ
        '
        Me.LBSJ.Dpi = 254.0!
        Me.LBSJ.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBSJ.LocationFloat = New DevExpress.Utils.PointFloat(79.99996!, 22.00001!)
        Me.LBSJ.Name = "LBSJ"
        Me.LBSJ.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSJ.SizeF = New System.Drawing.SizeF(672.4348!, 49.99999!)
        Me.LBSJ.StylePriority.UseFont = False
        Me.LBSJ.StylePriority.UseTextAlignment = False
        Me.LBSJ.Text = "SJ"
        Me.LBSJ.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(244.0415!, 402.2275!)
        Me.LBTanggal.Name = "LBTanggal"
        Me.LBTanggal.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTanggal.SizeF = New System.Drawing.SizeF(606.2866!, 47.83661!)
        Me.LBTanggal.StylePriority.UseTextAlignment = False
        Me.LBTanggal.Text = "Tanggal"
        Me.LBTanggal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(265.2082!, 72.00001!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(1187.415!, 136.6144!)
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(79.99996!, 72.00001!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(185.2082!, 45.19086!)
        Me.XrLabel8.Text = "Keterangan"
        '
        'LBTotPsg
        '
        Me.LBTotPsg.Dpi = 254.0!
        Me.LBTotPsg.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotPsg.LocationFloat = New DevExpress.Utils.PointFloat(1842.021!, 22.00001!)
        Me.LBTotPsg.Name = "LBTotPsg"
        Me.LBTotPsg.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotPsg.SizeF = New System.Drawing.SizeF(198.0!, 48.0!)
        Me.LBTotPsg.StylePriority.UseFont = False
        Me.LBTotPsg.StylePriority.UseTextAlignment = False
        Me.LBTotPsg.Text = "Psg"
        Me.LBTotPsg.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'LBTotDos
        '
        Me.LBTotDos.Dpi = 254.0!
        Me.LBTotDos.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTotDos.LocationFloat = New DevExpress.Utils.PointFloat(1643.958!, 22.00001!)
        Me.LBTotDos.Name = "LBTotDos"
        Me.LBTotDos.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTotDos.SizeF = New System.Drawing.SizeF(198.0!, 48.0!)
        Me.LBTotDos.StylePriority.UseFont = False
        Me.LBTotDos.StylePriority.UseTextAlignment = False
        Me.LBTotDos.Text = "Dos"
        Me.LBTotDos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XRDOBJ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter})
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
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudangTj As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDOID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBAlamat As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents LBPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBArtCode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotPsg As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTotDos As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBRow As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBSJ As DevExpress.XtraReports.UI.XRLabel
End Class
