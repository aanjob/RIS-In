Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRPersenBB
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGerak As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTdkGerak As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTot As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGerakRF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTdkGerakRF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumTotRF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLPersenBB", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = MainModule.PilihGudangID
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "PersenBB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsID", "JnsID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("JnsPers", "JnsPers"), New System.Data.Common.DataColumnMapping("Begerak", "Begerak"), New System.Data.Common.DataColumnMapping("TidakBergerak", "TidakBergerak"), New System.Data.Common.DataColumnMapping("PersenGerak", "PersenGerak"), New System.Data.Common.DataColumnMapping("PersenTdkGerak", "PersenTdkGerak"), New System.Data.Common.DataColumnMapping("Tot", "Tot")})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "PersenBB")

        Me.DataMember = "PersenBB"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
        Me.LBGudang.Text = "Gudang : " & MainModule.PilihGudang
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHJnsPers.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHJnsPers})
        Me.GHJnsPers.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("JnsPers", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHJnsPers.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.JnsPers")})
        Me.LBGFJnsPers.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.JnsPers", "Total {0}")})
        Me.LBJenis.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Jenis")})
        Me.LBSalGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Bergerak", "{0:n2}")})
        Me.LBSalTdkGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.TidakBergerak", "{0:n2}")})
        Me.LBPersenGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.PersenGerak", "{0:n2}")})
        Me.LBPersenTdkGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.PersenTdkGerak", "{0:n2}")})
        Me.LBTotSal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Tot", "{0:n2}")})

        Me.GFJnsPers.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFJnsPers, Me.LBGFGerak, Me.LBGFTdkGerak, Me.LBGFTotSal})

        Me.LBGFGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Bergerak", "{0:n2}")})
        SumGerak.FormatString = "{0:n2}"
        SumGerak.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFGerak.Summary = SumGerak

        Me.LBGFTdkGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.TidakBergerak", "{0:n2}")})
        SumTdkGerak.FormatString = "{0:n2}"
        SumTdkGerak.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTdkGerak.Summary = SumTdkGerak

        Me.LBGFTotSal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Tot", "{0:n2}")})
        SumTot.FormatString = "{0:n2}"
        SumTot.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotSal.Summary = SumTot

        Me.LBRFGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Bergerak", "{0:n2}")})
        SumGerakRF.FormatString = "{0:n2}"
        SumGerakRF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFGerak.Summary = SumGerakRF

        Me.LBRFTdkGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.TidakBergerak", "{0:n2}")})
        SumTdkGerakRF.FormatString = "{0:n2}"
        SumTdkGerakRF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTdkGerak.Summary = SumTdkGerakRF

        Me.LBRFTotSal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PersenBB.Tot", "{0:n2}")})
        SumTotRF.FormatString = "{0:n2}"
        SumTotRF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotSal.Summary = SumTotRF

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRPersenBB_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class