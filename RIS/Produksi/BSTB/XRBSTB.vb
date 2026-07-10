Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRBSTB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumQty As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim TotQty, Sisa As Decimal

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select *,Case when JnsDoc='Lain-Lain' Then 0 Else Qty-(Kirim+Terima) End As Sisa From (Select JnsDoc, POID,SPK,D.ArtCode+ '(' + B.ArtName +')' As ArtName,W.Nama As Warna,Qty As Kirim,(Select Isnull((Select Distinct Case When ArtCodeInd=ArtCode Then (Select Qty-BtlOrder-Upp-Hancur-Hilang-LunasMan From T_BOMPO Where BOMID=BM.BOMID and ArtCode=D.ArtCode) Else (Select (TotPsg-(Select Isnull((Select Sum(BtlOrder+Upp+Hancur+Hilang+LunasMan) From T_BOMPO where BOMID=T_BOM.BOMID),0)))/B.Isi From T_BOM Where BOMID=BM.BOMID) End As Qty From T_BOMPO Where BOMID=BM.BOMID and (ArtCodeInd=D.ArtCode OR ArtCode=D.ArtCode)),0)) as Qty, (Select Isnull(Sum(Qty),0) From T_BSTBDtl Where ArtCode=D.ArtCode and BOMID=D.BOMID and BSTBID<>D.BSTBID and BSTBIDD<D.BSTBIDD)As Terima From T_BSTB H Inner Join T_BSTBDtl D On H.BSTBID=D.BSTBID Left Outer Join T_BOM BM On D.BOMID=BM.BOMID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where D.BSTBID ='" & Bind.Item("Kode").ToString & "') As x", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_BSTBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("POID", "POID"), New System.Data.Common.DataColumnMapping("SPK", "SPK"), New System.Data.Common.DataColumnMapping("ArtCode", "ArtCode"), New System.Data.Common.DataColumnMapping("Warna", "Warna"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Terima", "Terima"), New System.Data.Common.DataColumnMapping("Kirim", "Kirim"), New System.Data.Common.DataColumnMapping("Sisa", "Sisa")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_BSTBDtl")

        Me.DataMember = "T_BSTBDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBSupp.Text = ": " & Bind.Item("Supp").ToString
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBKota.Text = MainModule.Kota & ", " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBNoPO.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.POID")})
        Me.LBSPK.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.SPK")})
        Me.LBBrg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.ArtName")})
        Me.LBWarna.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Warna")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Qty", "{0:n1}")})
        Me.LBKirim.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Kirim", "{0:n1}")})
        Me.LBDiterima.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Terima", "{0:n1}")})
        Me.LBSisa.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Sisa", "{0:n1}")})

        Me.LBTotQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_BSTBDtl.Kirim", "{0:n1}")})
        SumQty.FormatString = "{0:n1}"
        SumQty.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.LBTotQty.Summary = SumQty

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
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

    Private Sub XRBSTB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub

    Private Sub ReportFooter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles ReportFooter.BeforePrint
        If ReportFooter.Visible = True Then
            Me.LBKota.Visible = True
            Me.LBTT1.Visible = True
            Me.LBTT2.Visible = True
            Me.LBTT3.Visible = True
            Me.LBTT4.Visible = True

        Else
            Me.LBTT1.Visible = False
            Me.LBTT2.Visible = False
            Me.LBTT3.Visible = False
            Me.LBTT4.Visible = False
        End If
    End Sub

End Class