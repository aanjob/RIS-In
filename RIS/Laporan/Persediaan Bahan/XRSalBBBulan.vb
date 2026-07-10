Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRSalBBBulan
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumB1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB4 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB1RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB2RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB3RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumB4RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLSalBBBulan", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = MainModule.PilihGudangID
        'cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        'cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = MainModule.periodeTahun
        cmsl.SelectCommand.Parameters.Add("@B1", SqlDbType.VarChar).Value = MainModule.B1
        cmsl.SelectCommand.Parameters.Add("@B2", SqlDbType.VarChar).Value = MainModule.B2
        cmsl.SelectCommand.Parameters.Add("@B3", SqlDbType.VarChar).Value = MainModule.B3
        cmsl.SelectCommand.Parameters.Add("@B4", SqlDbType.VarChar).Value = MainModule.B4

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SalBBBulan", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("JnsID", "JnsID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("stsGerak", "stsGerak"), New System.Data.Common.DataColumnMapping(MainModule.B1, MainModule.B1), New System.Data.Common.DataColumnMapping(MainModule.B2, MainModule.B2), New System.Data.Common.DataColumnMapping(MainModule.B3, MainModule.B3), New System.Data.Common.DataColumnMapping(MainModule.B4, MainModule.B4)})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "SalBBBulan")

        Me.DataMember = "SalBBBulan"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBTahun.Text = "Tahun " & MainModule.periodeTahun
        Me.LBGudang.Text = "Gudang : " & MainModule.PilihGudang
       Me.LBUser.Text = MainModule.LoginAktif

        Me.LBB1.Text = MainModule.NmB1
        Me.LBB2.Text = MainModule.NmB2
        Me.LBB3.Text = MainModule.NmB3
        Me.LBB4.Text = MainModule.NmB4

        Me.GHstsGerak.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGHstsGerak})
        Me.GHstsGerak.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("stsGerak", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBGHstsGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan.stsGerak")})
        Me.LBGFstsGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan.stsGerak", "Total {0}")})
        Me.LBJenis.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan.Jenis")})
        Me.LBSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B1, "{0:n2}")})
        Me.LBSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B2, "{0:n2}")})
        Me.LBSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B3, "{0:n2}")})
        Me.LBSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B4, "{0:n2}")})

        Me.GFstsGerak.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFSalB1, Me.LBGFSalB2, Me.LBGFSalB3, Me.LBGFSalB4})

        Me.LBGFSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B1, "{0:n2}")})
        SumB1.FormatString = "{0:n2}"
        SumB1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB1.Summary = SumB1

        Me.LBGFSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B2, "{0:n2}")})
        SumB2.FormatString = "{0:n2}"
        SumB2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB2.Summary = SumB2

        Me.LBGFSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B3, "{0:n2}")})
        SumB3.FormatString = "{0:n2}"
        SumB3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB3.Summary = SumB3

        Me.LBGFSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B4, "{0:n2}")})
        SumB4.FormatString = "{0:n2}"
        SumB4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB4.Summary = SumB4


        Me.LBRFSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B1, "{0:n2}")})
        SumB1RF.FormatString = "{0:n2}"
        SumB1RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB1.Summary = SumB1RF

        Me.LBRFSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B2, "{0:n2}")})
        SumB2RF.FormatString = "{0:n2}"
        SumB2RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB2.Summary = SumB2RF

        Me.LBRFSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B3, "{0:n2}")})
        SumB3RF.FormatString = "{0:n2}"
        SumB3RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB3.Summary = SumB3RF

        Me.LBRFSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulan." & MainModule.B4, "{0:n2}")})
        SumB4RF.FormatString = "{0:n2}"
        SumB4RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB4.Summary = SumB4RF

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSalBBBulan_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class