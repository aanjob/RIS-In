<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRAssOrder
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
        Me.LBQtyAs1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBUk1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBAs1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.POIDDtl = New DevExpress.XtraReports.Parameters.Parameter()
        Me.POID = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBQtyAs1, Me.LBUk1, Me.LBAs1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 137.437!
        Me.Detail.MultiColumn.ColumnCount = 8
        Me.Detail.MultiColumn.ColumnWidth = 107.0!
        Me.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQtyAs1
        '
        Me.LBQtyAs1.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.LBQtyAs1.Dpi = 254.0!
        Me.LBQtyAs1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.5!, System.Drawing.FontStyle.Bold)
        Me.LBQtyAs1.LocationFloat = New DevExpress.Utils.PointFloat(2.0!, 91.2461!)
        Me.LBQtyAs1.Name = "LBQtyAs1"
        Me.LBQtyAs1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQtyAs1.SizeF = New System.Drawing.SizeF(103.3374!, 45.19093!)
        Me.LBQtyAs1.StylePriority.UseBorders = False
        Me.LBQtyAs1.StylePriority.UseFont = False
        Me.LBQtyAs1.StylePriority.UseTextAlignment = False
        Me.LBQtyAs1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBUk1
        '
        Me.LBUk1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.LBUk1.Dpi = 254.0!
        Me.LBUk1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.5!, System.Drawing.FontStyle.Bold)
        Me.LBUk1.LocationFloat = New DevExpress.Utils.PointFloat(2.0!, 0.0!)
        Me.LBUk1.Name = "LBUk1"
        Me.LBUk1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUk1.SizeF = New System.Drawing.SizeF(103.3374!, 45.19093!)
        Me.LBUk1.StylePriority.UseBorders = False
        Me.LBUk1.StylePriority.UseFont = False
        Me.LBUk1.StylePriority.UseTextAlignment = False
        Me.LBUk1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LBAs1
        '
        Me.LBAs1.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.LBAs1.Dpi = 254.0!
        Me.LBAs1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.5!, System.Drawing.FontStyle.Bold)
        Me.LBAs1.LocationFloat = New DevExpress.Utils.PointFloat(2.0!, 46.05518!)
        Me.LBAs1.Name = "LBAs1"
        Me.LBAs1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBAs1.SizeF = New System.Drawing.SizeF(103.3374!, 45.19093!)
        Me.LBAs1.StylePriority.UseBorders = False
        Me.LBAs1.StylePriority.UseFont = False
        Me.LBAs1.StylePriority.UseTextAlignment = False
        Me.LBAs1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
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
        'POIDDtl
        '
        Me.POIDDtl.Name = "POIDDtl"
        Me.POIDDtl.Type = GetType(Integer)
        Me.POIDDtl.ValueInfo = "0"
        Me.POIDDtl.Visible = False
        '
        'POID
        '
        Me.POID.Name = "POID"
        Me.POID.Visible = False
        '
        'XRAssOrder
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        Me.PageHeight = 2794
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.POIDDtl, Me.POID})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ShowPrintMarginsWarning = False
        Me.SnapToGrid = False
        Me.Version = "14.1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents LBQtyAs1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBUk1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBAs1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents POIDDtl As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents POID As DevExpress.XtraReports.Parameters.Parameter
End Class
