Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPersBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumSA As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumMasuk As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumKeluar As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumSAkh As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData(ByVal Bind As Collection)
        cmsl = New SqlDataAdapter("SPLPersBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = MainModule.PilihGudangID
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "PersBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsID", "JnsID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Bahan", "Bahan"), New System.Data.Common.DataColumnMapping("Sat", "Sat"), New System.Data.Common.DataColumnMapping("SA", "SA"), New System.Data.Common.DataColumnMapping("Masuk", "Masuk"), New System.Data.Common.DataColumnMapping("Keluar", "Keluar"), New System.Data.Common.DataColumnMapping("SAkh", "SAkh")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "PersBB")

        Me.DataMember = "PersBB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBGudang.Text = "Gudang : " & MainModule.PilihGudang
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHJnsID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHJnsID})
        Me.GHJnsID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("Jenis", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHJnsID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Jenis")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Bahan")})
        Me.LBSatuan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Sat")})
        Me.LBSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SA", "{0:n2}")})
        Me.LBMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Masuk", "{0:n2}")})
        Me.LBKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Keluar", "{0:n2}")})
        Me.LBSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SAkh", "{0:n2}")})

        Me.LBGFJenisID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Jenis", "Total {0}")})

        Me.LBGFSA.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SA", "{0:n2}")})
        SumSA.FormatString = "{0:n2}"
        SumSA.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSA.Summary = SumSA

        Me.LBGFMasuk.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Masuk", "{0:n2}")})
        SumMasuk.FormatString = "{0:n2}"
        SumMasuk.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFMasuk.Summary = SumMasuk

        Me.LBGFKeluar.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.Keluar", "{0:n2}")})
        SumKeluar.FormatString = "{0:n2}"
        SumKeluar.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFKeluar.Summary = SumKeluar

        Me.LBGFSAkh.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersBB.SAkh", "{0:n2}")})
        SumSAkh.FormatString = "{0:n2}"
        SumSAkh.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSAkh.Summary = SumSAkh

        Me.FilterString = "[JnsID] In (" & Bind.Item("JnsID").ToString & ")"

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPersBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class