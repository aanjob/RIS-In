Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports System
Imports DevExpress.XtraEditors

Public Class XRBCPakaiBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLBCPakaiBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@GdID", SqlDbType.VarChar).Value = MainModule.PilihGudangID

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BCPakaiBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BPBID", "BPBID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "BCPakaiBB")

        Me.DataMember = "BCPakaiBB"
        Me.DataSource = DsLap

        Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBBPBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.BPBID")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.Tanggal", "{0:dd/MM/yy}")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.BBID")})
        Me.LBNama.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.Nama")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCPakaiBB.Qty", "{0:n2}")})

        Me.ShowPreview()
    End Sub

    Private Sub XRBCPakaiBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class