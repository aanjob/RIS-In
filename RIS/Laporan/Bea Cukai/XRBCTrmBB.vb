Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports System
Imports DevExpress.XtraEditors

Public Class XRBCTrmBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim cmSP As SqlCommand

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLBCTrmBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = MainModule.PilihAwal
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = MainModule.PilihAkhir
        cmsl.SelectCommand.Parameters.Add("@Kat", SqlDbType.VarChar).Value = MainModule.PilihKat

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BCTrmBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsDoc", "JnsDoc"), New System.Data.Common.DataColumnMapping("PEN", "PEN"), New System.Data.Common.DataColumnMapping("TglPEN", "TglPEN"), New System.Data.Common.DataColumnMapping("Urut", "Urut"), New System.Data.Common.DataColumnMapping("TrmID", "TrmID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Nama", "Nama"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("Qty", "Qty"), New System.Data.Common.DataColumnMapping("MtUang", "MtUang"), New System.Data.Common.DataColumnMapping("CIF", "CIF"), New System.Data.Common.DataColumnMapping("GdID", "GdID"), New System.Data.Common.DataColumnMapping("Negara", "Negara")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "BCTrmBB")

        Me.DataMember = "BCTrmBB"
        Me.DataSource = DsLap

        Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBJnsDoc.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.JnsDoc")})
        Me.LBPENID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.PEN")})
        Me.LBTglPEN.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.TglPEN", "{0:dd/MM/yy}")})
        Me.LBSeri.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Urut", "{0:#,##0;(#,##0);""}")})
        Me.LBLPBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.TrmID")})
        Me.LBTglLPB.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Tanggal", "{0:dd/MM/yy}")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.BBID")})
        Me.LBNama.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Nama")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Sat")})
        Me.LBQty.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Qty", "{0:n2}")})
        Me.LBMtUang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.MtUang")})
        Me.LBCIF.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.CIF", "{0:n2}")})
        Me.LBGudang.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.GdID")})
        Me.LBNegara.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "BCTrmBB.Negara")})

        Me.ShowPreview()
    End Sub

    Private Sub XRBCTrmBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class