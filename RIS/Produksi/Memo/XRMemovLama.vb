Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRMemovLama
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select MD.BOMID,BM.ArtName,K.Nama As Komp,B.Nama As BahanAs,UkBBAs,SatAs,KebAs,Case When stsTidakPki='False' Then (Select Nama From M_BB Where BBID=MD.BBIDTj) Else 'Tidak Dipakai' End As BahanTj,UkBBTj,SatTj,KebTj From T_MemoDtl MD Inner Join T_BOM BM On MD.BOMID=BM.BOMID Inner Join M_BB B On MD.BBIDAs=B.BBID Inner Join M_Komp K On MD.KompID=K.KompID Where MemoID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_MemoDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BOMID", "BOMID"), New System.Data.Common.DataColumnMapping("Komp", "Komp"), New System.Data.Common.DataColumnMapping("BahanAs", "BahanAs"), New System.Data.Common.DataColumnMapping("SatAs", "SatAs"), New System.Data.Common.DataColumnMapping("KebAs", "KebAs"), New System.Data.Common.DataColumnMapping("BahanTj", "BahanTj"), New System.Data.Common.DataColumnMapping("SatTj", "SatTj"), New System.Data.Common.DataColumnMapping("KebTj", "KebTj")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_MemoDtl")

        Me.DataMember = "T_MemoDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = ": " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBOMID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.BOMID")})
        Me.LBArtName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.ArtName")})
        Me.LBKomp.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.Komp")})
        Me.LBBahanAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.BahanAs")})
        Me.LBSatAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.SatAs")})
        Me.LBKebAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.KebAs", "{0:#,##0.00;(#,##0.00);""}")})
        Me.LBBahanTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.BahanTj")})
        Me.LBSatTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.SatTj")})
        Me.LBKebTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_MemoDtl.KebTj", "{0:#,##0.00;(#,##0.00);""}")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1600
            Me.PageWidth = 2159
            Me.ShowPreview()
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 3300
            Me.PageWidth = 2159
            Me.ShowPreview()
        End If

    End Sub

    Private Sub XRMemo_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class