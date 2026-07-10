Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRTAGv1
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.BBID,Nama As Bahan,D.Sat,Qty,D.Ket From T_TAGDtl D Inner Join M_BB B On D.BBID=B.BBID Where TAGID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TAGDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TAGDtl")

        Me.DataMember = "T_TAGDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBHeader.Text = Me.LBHeader.Text & " " & Bind.Item("Gol").ToString
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudangAs.Text = ": " & Bind.Item("GudangAs").ToString
        Me.LBGudangTj.Text = ": " & Bind.Item("GudangTj").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TAGDtl.Bahan")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TAGDtl.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TAGDtl.Qty")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TAGDtl.Ket")})

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

    Private Sub XRTAG_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class