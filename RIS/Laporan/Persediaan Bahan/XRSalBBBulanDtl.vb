Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRSalBBBulanDtl
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
    Dim SumQB1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB4 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB1RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB2RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB3RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumQB4RF As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary

    Public Sub InitializeData()
        cmsl = New SqlDataAdapter("SPLSalBBBulanDtl", koneksi)
        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Gd", SqlDbType.VarChar).Value = MainModule.PilihGudangID
        'cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        'cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.Parameters.Add("@Tahun", SqlDbType.Int).Value = MainModule.periodeTahun
        cmsl.SelectCommand.Parameters.Add("@B1", SqlDbType.VarChar).Value = MainModule.B1
        cmsl.SelectCommand.Parameters.Add("@B2", SqlDbType.VarChar).Value = MainModule.B2
        cmsl.SelectCommand.Parameters.Add("@B3", SqlDbType.VarChar).Value = MainModule.B3
        cmsl.SelectCommand.Parameters.Add("@B4", SqlDbType.VarChar).Value = MainModule.B4

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SalBBBulanDtl", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BBID", "BBID"), New System.Data.Common.DataColumnMapping("Jenis", "Jenis"), New System.Data.Common.DataColumnMapping("stsGerak", "stsGerak"), New System.Data.Common.DataColumnMapping(MainModule.B1, MainModule.B1), New System.Data.Common.DataColumnMapping(MainModule.B2, MainModule.B2), New System.Data.Common.DataColumnMapping(MainModule.B3, MainModule.B3), New System.Data.Common.DataColumnMapping(MainModule.B4, MainModule.B4)})})

        DsLap = New System.Data.DataSet
        cmsl.SelectCommand.CommandTimeout = 90000
        cmsl.Fill(DsLap, "SalBBBulanDtl")

        Me.DataMember = "SalBBBulanDtl"
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

        Me.LBGHstsGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.stsGerak")})
        Me.LBGFstsGerak.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.stsGerak", "Total {0}")})
        Me.LBBBID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.BBID")})
        Me.LBBahan.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Bahan")})
        Me.LBSat.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Sat")})
        Me.LBSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B1, "{0:n2}")})
        Me.LBSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B2, "{0:n2}")})
        Me.LBSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B3, "{0:n2}")})
        Me.LBSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B4, "{0:n2}")})

        Me.LBQtyB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B1, "{0:n2}")})
        Me.LBQtyB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B2, "{0:n2}")})
        Me.LBQtyB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B3, "{0:n2}")})
        Me.LBQtyB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B4, "{0:n2}")})

        Me.GFstsGerak.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFSalB1, Me.LBGFSalB2, Me.LBGFSalB3, Me.LBGFSalB4})

        Me.LBGFSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B1, "{0:n2}")})
        SumB1.FormatString = "{0:n2}"
        SumB1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB1.Summary = SumB1

        Me.LBGFSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B2, "{0:n2}")})
        SumB2.FormatString = "{0:n2}"
        SumB2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB2.Summary = SumB2

        Me.LBGFSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B3, "{0:n2}")})
        SumB3.FormatString = "{0:n2}"
        SumB3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB3.Summary = SumB3

        Me.LBGFSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B4, "{0:n2}")})
        SumB4.FormatString = "{0:n2}"
        SumB4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFSalB4.Summary = SumB4


        Me.LBRFSalB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B1, "{0:n2}")})
        SumB1RF.FormatString = "{0:n2}"
        SumB1RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB1.Summary = SumB1RF

        Me.LBRFSalB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B2, "{0:n2}")})
        SumB2RF.FormatString = "{0:n2}"
        SumB2RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB2.Summary = SumB2RF

        Me.LBRFSalB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B3, "{0:n2}")})
        SumB3RF.FormatString = "{0:n2}"
        SumB3RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB3.Summary = SumB3RF

        Me.LBRFSalB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl." & MainModule.B4, "{0:n2}")})
        SumB4RF.FormatString = "{0:n2}"
        SumB4RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFSalB4.Summary = SumB4RF


        Me.LBGFQtyB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B1, "{0:n2}")})
        SumQB1.FormatString = "{0:n2}"
        SumQB1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQtyB1.Summary = SumQB1

        Me.LBGFQtyB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B2, "{0:n2}")})
        SumQB2.FormatString = "{0:n2}"
        SumQB2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQtyB2.Summary = SumQB2

        Me.LBGFQtyB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B3, "{0:n2}")})
        SumQB3.FormatString = "{0:n2}"
        SumQB3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQtyB3.Summary = SumQB3

        Me.LBGFQtyB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B4, "{0:n2}")})
        SumQB4.FormatString = "{0:n2}"
        SumQB4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFQtyB4.Summary = SumQB4


        Me.LBRFQtyB1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B1, "{0:n2}")})
        SumQB1RF.FormatString = "{0:n2}"
        SumQB1RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFQtyB1.Summary = SumQB1RF

        Me.LBRFQtyB2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B2, "{0:n2}")})
        SumQB2RF.FormatString = "{0:n2}"
        SumQB2RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFQtyB2.Summary = SumQB2RF

        Me.LBRFQtyB3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B3, "{0:n2}")})
        SumQB3RF.FormatString = "{0:n2}"
        SumQB3RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFQtyB3.Summary = SumQB3RF

        Me.LBRFQtyB4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SalBBBulanDtl.Q" & MainModule.B4, "{0:n2}")})
        SumQB4RF.FormatString = "{0:n2}"
        SumQB4RF.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFQtyB4.Summary = SumQB4RF

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRSalBBBulanDtl_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class