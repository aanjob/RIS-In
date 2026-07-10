Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRBPBBtNum
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection, Gol As String)
        Dim command As New SqlCommand("Select ISOID From M_DocCode Where DocID=8 and CabID='" & Gol & "'", koneksi)

        koneksi.Close()

        With koneksi
            .Open()
            Me.LBIDISO.Text = command.ExecuteScalar()
            .Close()
        End With

        cmsl = New SqlDataAdapter("Select (Select BtNum + ', '  From (Select Distinct BtNum From T_BPBDtl Where BBID=x.BBID and BPBID='" & Bind.Item("Kode").ToString & "') as q order By BtNum FOR XML PATH('')) As BtNum,Bahan,Sat,QtyBOM,Kirim,CASE WHEN DocID = '' Or DocID IS NULL THEN 0 ELSE SaatIni End As SaatIni, CASE WHEN DocID = '' Or DocID IS NULL THEN 0 ELSE QtyBOM - (SaatIni + Kirim) END AS Sisa From(Select DocID, B.BBID,'('+ B.BBID +') ' + B.Nama as Bahan,D.Sat,Sum(Qty) As Kirim,(Select Isnull ((Select Round(Sum(Keb), 0) From T_BOMDtl Where (BBID = D.BBID) And (BOMID = H.DocID)), 0) AS Expr1) AS QtyBOM,(Select Isnull ((Select Sum(T_BPBDtl.Qty) -(Select Isnull ((Select Sum(T_RPBDtl.Qty) From T_RPB INNER JOIN T_RPBDtl On T_RPB.RPBID = T_RPBDtl.RPBID Where (T_RPB.DocID = H.DocID) And (T_RPBDtl.BBID = D.BBID) And (T_RPB.Tanggal <= H.Tanggal)), 0) AS Expr1) From T_BPB INNER JOIN T_BPBDtl On T_BPB.BPBID =T_BPBDtl.BPBID Where (T_BPBDtl.BPBID <> H.BPBID) And (T_BPBDtl.BBID = D.BBID) And (T_BPB.DocID = H.DocID) And (T_BPB.Tanggal <= H.Tanggal)), 0)) AS SaatIni From T_BPB H Inner Join T_BPBDtl D On H.BPBID=D.BPBID Inner Join M_BB B On D.BBID=B.BBID Where H.BPBID ='" & Bind.Item("Kode").ToString & "' Group By DocID, D.BBID,B.BBID,B.Nama,D.Sat,H.BPBID,H.Tanggal) as x Order By Bahan", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BPBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BtNum", "BtNum"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("QtyBOM", "QtyBOM"), New System.Data.Common.DataColumnMapping("SaatIni", "SaatIni"), New System.Data.Common.DataColumnMapping("Kirim", "Kirim"), New System.Data.Common.DataColumnMapping("Sisa", "Sisa")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BPBDtl")

        Me.DataMember = "T_BPBDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBHeader.Text = Me.LBHeader.Text & " " & Bind.Item("Gol").ToString
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
        Me.LBQtyBOM.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.QtyBOM", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBSaatIni.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.SaatIni", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Kirim", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBSisa.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BPBDtl.Sisa", "{0:#,##0.00;(#,##0.00);""}")})

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