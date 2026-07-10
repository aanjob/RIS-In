Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI

Public Class XRQCBahan
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("Select D.BBID,B.Nama As Bahan,CaraUji,D.Ket,Kesimpulan,KetKesimpulan From T_QCBahanDtl D Inner Join M_BB B On D.BBID=B.BBID Where QCID='" & Bind.Item("Kode").ToString & "'", koneksi)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "T_QCBahanDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("CaraUji", "CaraUji"), New System.Data.Common.DataColumnMapping("Ket", "Ket"), New System.Data.Common.DataColumnMapping("Kesimpulan", "Kesimpulan"), New System.Data.Common.DataColumnMapping("KetKesimpulan", "KetKesimpulan")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "T_QCBahanDtl")

        Me.DataMember = "T_QCBahanDtl"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBKode.Text = ": " & Bind.Item("Kode").ToString
        Me.LBTanggal.Text = "Tanggal : " & Format(CDate(Bind.Item("Tanggal")), "dd MMMM yyyy")
        Me.LBCust.Text = ": " & Bind.Item("Cust").ToString
        Me.LBSupp.Text = ": " & Bind.Item("Supp").ToString
        Me.LBSJ.Text = ": " & Bind.Item("SJ").ToString
        Me.LBTglDtg.Text = ": " & Format(CDate(Bind.Item("TglLPB")), "dd MMMM yyyy")
        Me.LBKet.Text = ": " & Bind.Item("Ket").ToString
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_QCBahanDtl.Bahan")})
        Me.LBUji.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_QCBahanDtl.CaraUji")})
        Me.LBKetDtl.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_QCBahanDtl.Ket")})
        Me.LBKesimpulan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_QCBahanDtl.Kesimpulan")})
        Me.LBKetKesimpulan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "T_QCBahanDtl.KetKesimpulan")})

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRRPB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class