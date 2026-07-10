Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRtPenjBebas
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSbDisc As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotDisc, TotPPn, TotAkhir As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Nama,Qty,Sat,HarSat,HarSbDisc,DiscRp,DiscP,RpDiscP,HarAkhir From T_RtrPenjBebasDtl Where RtrID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_RtrPenjBebasDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("HarSbDisc", "HarSbDisc")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_RtrPenjBebasDtl")

        Me.DataMember = "T_RtrPenjBebasDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan & vbCrLf & MainModule.Alamat & vbCrLf & MainModule.Kota
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = Bind.Item("Cust").ToString
        Me.LBAlamat.Text = Bind.Item("Alamat").ToString & vbCrLf & "  " & Bind.Item("Kota").ToString
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBTipePPn.Text = ": " & Bind.Item("TipePPn").ToString

        If Bind.Item("TipePPn").ToString <> "Non PPn" Then
            Me.XLBPPn.Text = "PPn (" & String.Format("{0:#,##0.##}", CDec(Bind.Item("PersenPPn").ToString)) & " %)"
        End If

        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif
        Me.XLBHarga.Text = "Harga (" & Bind.Item("MtUang").ToString & ")"
        Me.XLBJml.Text = "Jumlah (" & Bind.Item("MtUang").ToString & ")"
        Me.XLBGrandTot.Text = "Grand Total (" & Bind.Item("MtUang").ToString & ")"

        TotDisc = CDec(Bind.Item("TotDisc").ToString)
        TotPPn = CDec(Bind.Item("TotPPn").ToString)
        TotAkhir = CDec(Bind.Item("TotAkhir").ToString)

        Me.LBTotDisc.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotDisc)
        Me.LBPPn.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotPPn)
        Me.LBGrandTot.Text = String.Format("{0:#,##0.00;(#,##0.00);""}", TotAkhir)

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.Nama")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.Qty")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.HarSat", "{0:n2}")})

        Me.LBJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.HarSbDisc", "{0:n2}")})

        Me.LBSubTot.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_RtrPenjBebasDtl.HarSbDisc", "{0:n2}")})
        SumSbDisc.FormatString = "{0:n2}"
        SumSbDisc.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBSubTot.Summary = SumSbDisc

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
        End If

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPOBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            Me.LBTotDisc.Visible = True
            Me.LBPPn.Visible = True
            Me.LBGrandTot.Visible = True

            Me.XLBTotDisc.Visible = True
            Me.XLBPPn.Visible = True
            Me.XLBGrandTot.Visible = True

            Me.LBKota.Visible = True
            Me.LBTT1.Visible = True
            Me.LBTT2.Visible = True

        Else
            Me.LBTotDisc.Visible = False
            Me.LBPPn.Visible = False
            Me.LBGrandTot.Visible = False

            Me.XLBTotDisc.Visible = False
            Me.XLBPPn.Visible = False
            Me.XLBGrandTot.Visible = False

            Me.LBTT1.Visible = False
            Me.LBTT2.Visible = False
        End If
    End Sub
End Class