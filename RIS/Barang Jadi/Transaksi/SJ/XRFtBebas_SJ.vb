Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRFtBebas_SJ
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQtyP As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotDisc, TotPPn, TotAkhir, TotP, TotD As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        'cmsl = New SqlDataAdapter("Select Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_JualBebas_SJDtl Where JualID='" & Bind.Item("Kode").ToString & "'", koneksi)
        cmsl = New SqlDataAdapter("select x.Nama,x.QtyP as Qty,x.QtyD,x.Size,x.Sat,x.HarSat,x.HarSbDisc,x.DiscRp,x.DiscP,x.RpDiscP,x.HarAkhir, x.TotD, x.TotP as TotPP from ( Select Nama,Qty as QtyP,0 as QtyD,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir, dbo.fcTotSat('" & Bind.Item("Kode").ToString & "','P') as TotP, 0 as TotD,Size  From T_JualBebas_SJDtl Where JualID='" & Bind.Item("Kode").ToString & "' Group by JualID,Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir,size ) x", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_JualBebasDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Size", "Size"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("QtyD", "QtyD"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc"), New System.Data.Common.DataColumnMapping("TotPP", "TotPP"), New System.Data.Common.DataColumnMapping("TotD", "TotD")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_JualBebasDtl")

        Me.DataMember = "T_JualBebasDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan & vbCrLf & MainModule.Alamat & vbCrLf & MainModule.Kota
        Me.LBKode.Text = Bind.Item("Kode").ToString
        'Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = Bind.Item("Cust").ToString
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString & vbCrLf & "  " & Bind.Item("Kota").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBJenis.Text = Bind.Item("Jenis").ToString

        'If Bind.Item("TipePPn").ToString <> "Non PPn" Then
        '    'Me.XLBPPn.Text = "PPn (" & String.Format("{0:#,##0.##}", CDec(Bind.Item("PersenPPn").ToString)) & " %)"
        'End If

        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif
        'Me.XLBHarga.Text = "Harga (" & Bind.Item("MtUang").ToString & ")"
        'Me.XLBJml.Text = "Jumlah (" & Bind.Item("MtUang").ToString & ")"
        'Me.XLBGrandTot.Text = "Grand Total (" & Bind.Item("MtUang").ToString & ")"
        'Me.XLBSumQty.Text = "Total Qty (" & Bind.Item("MtUang").ToString & ")"

        TotDisc = CDec(Bind.Item("TotDisc").ToString)
        TotPPn = CDec(Bind.Item("TotPPn").ToString)
        TotAkhir = CDec(Bind.Item("TotAkhir").ToString)
        'TotP = CDec(Bind.Item("TotP").ToString)
        'TotD = CDec(Bind.Item("TotD").ToString)

        'Me.LBTotDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotDisc)
        'Me.LBPPn.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotPPn)
        'Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotAkhir)
        'Me.LBSumQty.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotQty)
        'Me.LBTotP.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotP)
        'Me.LBTotD.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotD)

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.Nama")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.Sat")})
        Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.Size")})
        Me.LBQtyP.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.Qty")})
        'Me.LBSize.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.QtyD")})
        Me.LBTotP3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.TotPP")})
        'Me.LBTotD.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.TotD")})
        'Me.LBTotP2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.TotPP")})
        'Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.HarSat", "{0:n2}")})

        'Me.LBJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.HarSbDisc", "{0:n2}")})

        'Me.LBSubTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.HarSbDisc", "{0:n2}")})
        'SumSbDisc.FormatString = "{0:n2}"
        'SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        ''Me.LBSubTot.Summary = SumSbDisc

        'tambahan 11/04/2023 sumqty
        Me.LBTotP3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_JualBebasDtl.TotPP", "{0:n2}")})
        SumQtyP.FormatString = "{0:n2}"
        SumQtyP.Running = DevExpress.XtraReports.UI.SummaryRunning.None
        Me.LBTotP3.Summary = SumQtyP

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
        End If

        'If MainModule.PrintDt = "False" Then
        Me.LBUser.Visible = False
        Me.XrPageInfo2.Visible = False
        'End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            'Me.LBTotDisc.Visible = True
            'Me.LBPPn.Visible = True
            'Me.LBGrandTot.Visible = True
            'Me.LBSumQty.Visible = True

            'Me.XLBTotDisc.Visible = True
            'Me.XLBPPn.Visible = True
            'Me.XLBGrandTot.Visible = True
            'Me.XLBSumQty.Visible = True

            Me.LBKota.Visible = True
            Me.LBTT1.Visible = True
            Me.LBTT2.Visible = True

        Else
            'Me.LBTotDisc.Visible = False
            'Me.LBPPn.Visible = False
            'Me.LBGrandTot.Visible = False
            Me.LBTotP3.Visible = False
            'Me.LBTotD.Visible = False

            'Me.XLBTotDisc.Visible = False
            'Me.XLBPPn.Visible = False
            'Me.XLBGrandTot.Visible = False
            'Me.XLBSumQty.Visible = False

            Me.LBTT1.Visible = False
            Me.LBTT2.Visible = False
        End If
    End Sub
End Class