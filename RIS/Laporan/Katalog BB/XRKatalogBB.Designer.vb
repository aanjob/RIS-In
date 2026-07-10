<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRKatalogBB
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
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTgl = New DevExpress.XtraReports.UI.XRLabel()
        Me.GHJnsBB = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.LBJnsBB = New DevExpress.XtraReports.UI.XRLabel()
        Me.GHCust = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.LBCust = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 91.70306!
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.LBTgl})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 150.8125!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(48.06263!, 0.0!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(1997.583!, 51.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Katalog Sisa Bahan"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBTgl
        '
        Me.LBTgl.Dpi = 254.0!
        Me.LBTgl.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LBTgl.LocationFloat = New DevExpress.Utils.PointFloat(48.06263!, 51.00004!)
        Me.LBTgl.Name = "LBTgl"
        Me.LBTgl.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBTgl.SizeF = New System.Drawing.SizeF(1997.583!, 51.0!)
        Me.LBTgl.StylePriority.UseFont = False
        Me.LBTgl.StylePriority.UseTextAlignment = False
        Me.LBTgl.Text = "Per Tanggal"
        Me.LBTgl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GHJnsBB
        '
        Me.GHJnsBB.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBJnsBB})
        Me.GHJnsBB.Dpi = 254.0!
        Me.GHJnsBB.HeightF = 43.63529!
        Me.GHJnsBB.Name = "GHJnsBB"
        '
        'LBJnsBB
        '
        Me.LBJnsBB.Dpi = 254.0!
        Me.LBJnsBB.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBJnsBB.LocationFloat = New DevExpress.Utils.PointFloat(48.06312!, 0.0!)
        Me.LBJnsBB.Multiline = True
        Me.LBJnsBB.Name = "LBJnsBB"
        Me.LBJnsBB.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBJnsBB.SizeF = New System.Drawing.SizeF(1997.583!, 43.63529!)
        Me.LBJnsBB.StylePriority.UseFont = False
        Me.LBJnsBB.Text = "Jenis Bahan"
        '
        'GHCust
        '
        Me.GHCust.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBCust})
        Me.GHCust.Dpi = 254.0!
        Me.GHCust.HeightF = 44.97917!
        Me.GHCust.Level = 1
        Me.GHCust.Name = "GHCust"
        '
        'LBCust
        '
        Me.LBCust.Dpi = 254.0!
        Me.LBCust.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.0!, System.Drawing.FontStyle.Bold)
        Me.LBCust.LocationFloat = New DevExpress.Utils.PointFloat(48.06263!, 0.0!)
        Me.LBCust.Multiline = True
        Me.LBCust.Name = "LBCust"
        Me.LBCust.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBCust.SizeF = New System.Drawing.SizeF(1997.583!, 43.63529!)
        Me.LBCust.StylePriority.UseFont = False
        Me.LBCust.Text = "Customer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrPageInfo2, Me.LBUser})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 73.64764!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo1.Format = "Page {0} Of {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(48.06312!, 25.81105!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(1997.583!, 47.83659!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(235.0633!, 25.81073!)
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
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(48.06312!, 25.81073!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Dpi = 254.0!
        Me.GroupFooter1.HeightF = 0.0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Dpi = 254.0!
        Me.GroupFooter2.HeightF = 0.0!
        Me.GroupFooter2.Level = 1
        Me.GroupFooter2.Name = "GroupFooter2"
        Me.GroupFooter2.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Dpi = 254.0!
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(48.06312!, 0.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ReportSource = New RIS.XRKatalogBBDtl()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(1997.583!, 91.70306!)
        '
        'XRKatalogBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.GHJnsBB, Me.GHCust, Me.PageFooter, Me.GroupFooter1, Me.GroupFooter2})
        Me.Dpi = 254.0!
        Me.Font = New System.Drawing.Font("Abadi MT Condensed Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
    Friend WithEvents GHJnsBB As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GHCust As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTgl As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBJnsBB As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBCust As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
End Class
