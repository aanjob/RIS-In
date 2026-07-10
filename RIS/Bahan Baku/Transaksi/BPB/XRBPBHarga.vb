Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRBPBHarga
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumTotAkhir As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotAkhir As Decimal

    Public Sub InitializeData(ByVal Bind As Collection, Gol As String)
        Dim command As New SqlCommand("Select ISOID From M_DocCode Where DocID=8 and CabID='" & Gol & "'", koneksi)

        koneksi.Close()

        With koneksi
            .Open()
            Me.LBIDISO.Text = command.ExecuteScalar()
            .Close()
        End With

        cmsl = New SqlDataAdapter("Select (Select BtNum + ', '  From (Select Distinct BtNum From T_BPBDtl Where BBID=x.BBID and BPBID='" & Bind.Item("Kode").ToString & "') as q order By BtNum FOR XML PATH('')) As BtNum,*,Round(Qty*HarSat,2) As Jml From (Select DocID,B.BBID,'('+ B.BBID +') '+ B.Nama as Bahan,D.Sat,Sum(Qty) As Qty,(Select Top 1 HarSat From T_TrmBB Hx Inner Join T_TrmBBDtl Dx On Hx.TrmID=Dx.TrmID where BBID=D.BBID and Tanggal <=H.Tanggal Order By Tanggal desc) As HarSat From T_BPB H Inner Join T_BPBDtl D On H.BPBID=D.BPBID Inner Join M_BB B On D.BBID=B.BBID Where H.BPBID ='" & Bind.Item("Kode").ToString & "' Group By DocID, D.BBID, B.BBID,B.Nama,D.Sat,H.BPBID,H.Tanggal) as x", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BPBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BtNum", "BtNum"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("HarSat", "HarSat"), New System.Data.Common.DataColumnMapping("Jml", "Jml")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BPBDtl")

        Me.DataMember = "T_BPBDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")

        If CBool(Bind.Item("Manual").ToString) = False Then
            Me.LBDocID.Text = ": " & Bind.Item("DocID").ToString
        Else
            Me.LBDocID.Text = ": "
        End If

        Me.LBBagian.Text = ": " & Bind.Item("Bagian").ToString
        Me.LBUnit.Text = ": " & Bind.Item("Unit").ToString
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBtNum.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.BtNum")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Sat")})
        Me.LBKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Qty", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBHarSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.HarSat", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Jml", "{0:#,##0.00;(#,##0.00);""}")})

        Me.LBPFJml.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Jml", "{0:n2}")})

        SumTotAkhir.FormatString = "{0:n2}"
        SumTotAkhir.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBPFJml.Summary = SumTotAkhir

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
            Me.LBNB.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
            Me.ShowPreview()
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If

    End Sub

    Private Sub XRBPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class