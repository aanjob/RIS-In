<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSJMBB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRSJMBB))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.LBNo = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBBahan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBSatuan = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBQty = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.LBSJ = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDocID = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBDari = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBGudang = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBTanggal = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBPerusahaan = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.LBKode = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.LBKet = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrRichText4 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrRichText2 = New DevExpress.XtraReports.UI.XRRichText()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.LBUser = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.LBKet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRichText2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LBNo.LocationFloat = New DevExpress.Utils.PointFloat(22.55442!, 0.0!)
        Me.LBNo.Name = "LBNo"
        Me.LBNo.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBNo.SizeF = New System.Drawing.SizeF(103.0539!, 45.19086!)
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
        Me.LBBahan.LocationFloat = New DevExpress.Utils.PointFloat(125.6084!, 0.0!)
        Me.LBBahan.Name = "LBBahan"
        Me.LBBahan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBBahan.SizeF = New System.Drawing.SizeF(1393.071!, 45.19086!)
        Me.LBBahan.StylePriority.UseFont = False
        Me.LBBahan.StylePriority.UseTextAlignment = False
        Me.LBBahan.Text = "Nama Bahan"
        Me.LBBahan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBSatuan
        '
        Me.LBSatuan.Dpi = 254.0!
        Me.LBSatuan.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBSatuan.LocationFloat = New DevExpress.Utils.PointFloat(1518.68!, 0.0!)
        Me.LBSatuan.Name = "LBSatuan"
        Me.LBSatuan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSatuan.SizeF = New System.Drawing.SizeF(245.0825!, 45.19093!)
        Me.LBSatuan.StylePriority.UseFont = False
        Me.LBSatuan.StylePriority.UseTextAlignment = False
        Me.LBSatuan.Text = "Satuan"
        Me.LBSatuan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBQty
        '
        Me.LBQty.Dpi = 254.0!
        Me.LBQty.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LBQty.LocationFloat = New DevExpress.Utils.PointFloat(1763.867!, 0.0!)
        Me.LBQty.Name = "LBQty"
        Me.LBQty.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBQty.SizeF = New System.Drawing.SizeF(269.8751!, 45.19093!)
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
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBSJ, Me.XrLabel5, Me.LBDocID, Me.XrLabel16, Me.LBDari, Me.LBGudang, Me.XrLabel12, Me.XrLabel4, Me.XrLabel6, Me.XrLabel18, Me.XrLabel2, Me.LBTanggal, Me.LBPerusahaan, Me.XrLine1, Me.XrLine2, Me.XrLabel7, Me.XrLabel1, Me.LBKode, Me.XrLabel15})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 427.508!
        Me.PageHeader.Name = "PageHeader"
        '
        'LBSJ
        '
        Me.LBSJ.Dpi = 254.0!
        Me.LBSJ.LocationFloat = New DevExpress.Utils.PointFloat(267.8863!, 277.7761!)
        Me.LBSJ.Name = "LBSJ"
        Me.LBSJ.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBSJ.SizeF = New System.Drawing.SizeF(581.8534!, 45.19083!)
        Me.LBSJ.Text = "Nomor"
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(53.4273!, 277.7761!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(214.3125!, 45.19083!)
        Me.XrLabel5.Text = "No. SJ"
        '
        'LBDocID
        '
        Me.LBDocID.Dpi = 254.0!
        Me.LBDocID.LocationFloat = New DevExpress.Utils.PointFloat(1237.407!, 232.8143!)
        Me.LBDocID.Name = "LBDocID"
        Me.LBDocID.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDocID.SizeF = New System.Drawing.SizeF(711.7291!, 45.19084!)
        Me.LBDocID.Text = "Doc"
        '
        'XrLabel16
        '
        Me.XrLabel16.Dpi = 254.0!
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(1518.68!, 377.1755!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(245.0824!, 45.19092!)
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "Satuan"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'LBDari
        '
        Me.LBDari.Dpi = 254.0!
        Me.LBDari.LocationFloat = New DevExpress.Utils.PointFloat(53.42728!, 232.5852!)
        Me.LBDari.Name = "LBDari"
        Me.LBDari.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBDari.SizeF = New System.Drawing.SizeF(796.3123!, 45.19092!)
        Me.LBDari.Text = "Dari"
        '
        'LBGudang
        '
        Me.LBGudang.Dpi = 254.0!
        Me.LBGudang.LocationFloat = New DevExpress.Utils.PointFloat(1237.407!, 278.7761!)
        Me.LBGudang.Name = "LBGudang"
        Me.LBGudang.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBGudang.SizeF = New System.Drawing.SizeF(711.7291!, 45.19083!)
        Me.LBGudang.Text = "Gudang"
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.LocationFloat = New DevExpress.Utils.PointFloat(22.55444!, 377.1756!)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.SizeF = New System.Drawing.SizeF(103.0539!, 45.19086!)
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "No."
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(1022.948!, 187.3942!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(214.3125!, 45.19083!)
        Me.XrLabel4.Text = "No. SJM"
        '
        'XrLabel6
        '
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(1022.948!, 232.8143!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(214.3125!, 45.19083!)
        Me.XrLabel6.Text = "No. BOM"
        '
        'XrLabel18
        '
        Me.XrLabel18.Dpi = 254.0!
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(1763.867!, 377.1755!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(269.8751!, 45.19092!)
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "Qty"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(1022.948!, 278.7761!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(214.3125!, 45.19081!)
        Me.XrLabel2.Text = "Gudang"
        '
        'LBTanggal
        '
        Me.LBTanggal.Dpi = 254.0!
        Me.LBTanggal.LocationFloat = New DevExpress.Utils.PointFloat(23.07294!, 101.6475!)
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
        Me.LBPerusahaan.LocationFloat = New DevExpress.Utils.PointFloat(23.07294!, 0.0!)
        Me.LBPerusahaan.Name = "LBPerusahaan"
        Me.LBPerusahaan.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBPerusahaan.SizeF = New System.Drawing.SizeF(733.8341!, 58.42!)
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(23.55444!, 371.3372!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(23.56554!, 422.508!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(53.42713!, 187.3942!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(219.7009!, 45.19092!)
        Me.XrLabel7.Text = "Diterima Dari :"
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Abadi MT Condensed Light", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(23.07294!, 42.22752!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(2012.865!, 58.42001!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "Surat Jalan Masuk Bahan"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'LBKode
        '
        Me.LBKode.Dpi = 254.0!
        Me.LBKode.LocationFloat = New DevExpress.Utils.PointFloat(1237.407!, 187.3942!)
        Me.LBKode.Name = "LBKode"
        Me.LBKode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKode.SizeF = New System.Drawing.SizeF(711.7291!, 45.19081!)
        Me.LBKode.Text = "Nomor"
        '
        'XrLabel15
        '
        Me.XrLabel15.Dpi = 254.0!
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(125.6084!, 377.1755!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(1393.071!, 45.19089!)
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "Deskripsi Bahan"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine3, Me.LBKet, Me.XrLabel9, Me.XrRichText4, Me.XrRichText2, Me.XrPageInfo2, Me.XrPageInfo1, Me.LBUser})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 443.3127!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrLine3
        '
        Me.XrLine3.Dpi = 254.0!
        Me.XrLine3.LineWidth = 3
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(18.86206!, 0.0!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(2014.88!, 5.0!)
        '
        'LBKet
        '
        Me.LBKet.Dpi = 254.0!
        Me.LBKet.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBKet.LocationFloat = New DevExpress.Utils.PointFloat(240.6286!, 7.0!)
        Me.LBKet.Name = "LBKet"
        Me.LBKet.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBKet.SerializableRtfString = resources.GetString("LBKet.SerializableRtfString")
        Me.LBKet.SizeF = New System.Drawing.SizeF(1669.071!, 127.2535!)
        Me.LBKet.StylePriority.UsePadding = False
        '
        'XrLabel9
        '
        Me.XrLabel9.Dpi = 254.0!
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(23.5655!, 7.0!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(214.3125!, 45.19086!)
        Me.XrLabel9.Text = "Keterangan"
        '
        'XrRichText4
        '
        Me.XrRichText4.Dpi = 254.0!
        Me.XrRichText4.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.XrRichText4.LocationFloat = New DevExpress.Utils.PointFloat(1712.159!, 146.7381!)
        Me.XrRichText4.Name = "XrRichText4"
        Me.XrRichText4.SerializableRtfString = resources.GetString("XrRichText4.SerializableRtfString")
        Me.XrRichText4.SizeF = New System.Drawing.SizeF(322.5934!, 227.7533!)
        Me.XrRichText4.StylePriority.UseFont = False
        '
        'XrRichText2
        '
        Me.XrRichText2.Dpi = 254.0!
        Me.XrRichText2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 11.25!, System.Drawing.FontStyle.Bold)
        Me.XrRichText2.LocationFloat = New DevExpress.Utils.PointFloat(1349.326!, 146.7381!)
        Me.XrRichText2.Name = "XrRichText2"
        Me.XrRichText2.SerializableRtfString = resources.GetString("XrRichText2.SerializableRtfString")
        Me.XrRichText2.SizeF = New System.Drawing.SizeF(322.5934!, 227.7533!)
        Me.XrRichText2.StylePriority.UseFont = False
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Font = New System.Drawing.Font("Abadi MT Condensed Light", 10.0!)
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy/ hh:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(208.0657!, 395.476!)
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
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(23.79482!, 395.4761!)
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
        Me.LBUser.LocationFloat = New DevExpress.Utils.PointFloat(23.5655!, 395.476!)
        Me.LBUser.Name = "LBUser"
        Me.LBUser.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LBUser.SizeF = New System.Drawing.SizeF(185.0!, 47.83667!)
        Me.LBUser.StylePriority.UseFont = False
        Me.LBUser.StylePriority.UseTextAlignment = False
        Me.LBUser.Text = "User"
        Me.LBUser.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XRSJMBB
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.PageFooter})
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
        CType(Me.XrRichText4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRichText2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents LBDocID As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBDari As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBGudang As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBTanggal As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBPerusahaan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBNo As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBBahan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSatuan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBQty As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBKet As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrRichText4 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrRichText2 As DevExpress.XtraReports.UI.XRRichText
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents LBUser As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LBSJ As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
End Class
