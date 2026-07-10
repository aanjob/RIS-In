Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRTrArt
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select Concat (D.ArtCodeAs,' ( ',ArtName,' )') As BrgAs,D.SatIDAs,QtyAs,Concat (D.ArtCodeTj,' ( ',(Select ArtName From M_Brg Where ArtCode=D.ArtCodeTj),' )') As BrgTj,D.SatIDTj,QtyTj,Ket From T_TrBJDtl D Inner Join M_Brg B On D.ArtCodeAs=B.ArtCode Where TrID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TrBJDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BrgAs", "BrgAs"), New System.Data.Common.DataColumnMapping("SatIDAs", "SatIDAs"), New System.Data.Common.DataColumnMapping("QtyAs", "QtyAs"), New System.Data.Common.DataColumnMapping("BrgTj", "BrgTj"), New System.Data.Common.DataColumnMapping("SatIDTj", "SatIDTj"), New System.Data.Common.DataColumnMapping("QtyTj", "QtyTj"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TrBJDtl")

        Me.DataMember = "T_TrBJDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBrgAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.BrgAs")})
        Me.LBSatAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.SatIDAs")})
        Me.LBQtyAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.QtyAs")})
        Me.LBBrgTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.BrgTj")})
        Me.LBSatTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.SatIDTj")})
        Me.LBQtyTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.QtyTj")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBJDtl.Ket")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        ElseIf Bind.Item("Ukuran").ToString = "1 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 2780
            Me.PageWidth = 2159
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRTrArt_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class