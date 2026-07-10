Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRTransBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select BtNumAs,D.BBIDAs,Nama As BahanAs,D.SatAs,QtyAs,BtNumTj,D.BBIDTj,(Select Nama From M_BB Where BBID=D.BBIDTj) As BahanTj,D.SatTj,QtyTj,D.Ket From T_TrBBDtl D Inner Join M_BB B On D.BBIDAs=B.BBID Where TrID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_TrBBDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BtNumAs", "BtNumAs"), New System.Data.Common.DataColumnMapping("BahanAs", "BahanAs"), New System.Data.Common.DataColumnMapping("SatAs", "SatAs"), New System.Data.Common.DataColumnMapping("QtyAs", "QtyAs"), New System.Data.Common.DataColumnMapping("BtNumTj", "BtNumTj"), New System.Data.Common.DataColumnMapping("BahanTj", "BahanTj"), New System.Data.Common.DataColumnMapping("SatTj", "SatTj"), New System.Data.Common.DataColumnMapping("QtyTj", "QtyTj"), New System.Data.Common.DataColumnMapping("Ket", "Ket")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_TrBBDtl")

        Me.DataMember = "T_TrBBDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBGudang.Text = ": " & Bind.Item("Gudang").ToString
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
        Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBtNumAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.BtNumAs")})
        Me.LBBahanAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.BahanAs")})
        Me.LBSatAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.SatAs")})
        Me.LBQtyAs.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.QtyAs")})
        Me.LBBtNumTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.BtNumTj")})
        Me.LBBahanTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.BahanTj")})
        Me.LBSatTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.SatTj")})
        Me.LBQtyTj.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.QtyTj")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_TrBBDtl.Ket")})

        If Bind.Item("Ukuran").ToString = "1/2 Halaman" Then
            Me.PaperKind = Printing.PaperKind.Custom
            Me.PageHeight = 1396
            Me.PageWidth = 2159
        Else
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

    Private Sub XRTransBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class