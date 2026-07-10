Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraEditors

Public Class XRRekPiutAcc
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim cmsl As SqlDataAdapter
    Dim SumGFPiut As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumGFPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPiut As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim SumRFPsg As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
    Dim Saldo As Integer = 0
    Dim Hal As Integer = 0
    Dim Start As Integer = 0

    Public Sub InitializeData(ByVal Gol As String)
        If Gol <> "Lain-Lain" Then
            cmsl = New SqlDataAdapter("SPLRekPiutAcc", koneksi)
        Else
            cmsl = New SqlDataAdapter("SPLRekPiutAccL2", koneksi)
        End If

        cmsl.SelectCommand.CommandType = CommandType.StoredProcedure
        cmsl.SelectCommand.Parameters.Add("@Awal", SqlDbType.Date).Value = CDate(MainModule.PilihAwal)
        cmsl.SelectCommand.Parameters.Add("@Akhir", SqlDbType.Date).Value = CDate(MainModule.PilihAkhir)
        cmsl.SelectCommand.Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol

        cmsl.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "OmsetAcc", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CustID", "CustID"), New System.Data.Common.DataColumnMapping("Cust", "Cust"), New System.Data.Common.DataColumnMapping("DocID", "DocID"), New System.Data.Common.DataColumnMapping("Tanggal", "Tanggal"), New System.Data.Common.DataColumnMapping("TotAkhir", "TotAkhir"), New System.Data.Common.DataColumnMapping("TotPsg", "TotPsg")})})

        DsLap = New System.Data.DataSet
        cmsl.Fill(DsLap, "OmsetAcc")

        Me.DataMember = "OmsetAcc"
        Me.DataSource = DsLap

        'Me.LBPerusahaan.Text = MainModule.NmPerusahaan
        Me.LBPeriod.Text = "Periode : " & Format(CDate(MainModule.PilihAwal), "dd MMMM yyyy") & " s/d " & Format(CDate(MainModule.PilihAkhir), "dd MMMM yyyy")
       Me.LBUser.Text = MainModule.LoginAktif

        Me.GHCustID.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {})
        Me.GHCustID.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("CustID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})

        Me.LBCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.Cust")})
        Me.LBDocID.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.DocID")})
        Me.LBTanggal.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.Tanggal", "{0:dd}")})
        Me.LBTotPiut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotAkhir", "{0:n2}")})
        Me.LBTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotPsg", "{0:n0}")})
        Me.LBGFCust.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.Cust", "Total Piutang {0}")})

        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LBGFCust, Me.LBGFTotPiut, Me.LBGFTotPsg})

        Me.LBGFTotPiut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotAkhir", "{0:n2}")})
        SumGFPiut.FormatString = "{0:n2}"
        SumGFPiut.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotPiut.Summary = SumGFPiut

        Me.LBGFTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotPsg", "{0:n0}")})
        SumGFPsg.FormatString = "{0:n0}"
        SumGFPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.LBGFTotPsg.Summary = SumGFPsg

        Me.LBRFTotPiut.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotAkhir", "{0:n2}")})
        SumRFPiut.FormatString = "{0:n2}"
        SumRFPiut.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotPiut.Summary = SumRFPiut

        Me.LBRFTotPsg.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "OmsetAcc.TotPsg", "{0:n0}")})
        SumRFPsg.FormatString = "{0:n0}"
        SumRFPsg.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.LBRFTotPsg.Summary = SumRFPsg

        If MainModule.PrintDt = "False" Then
            Me.LBUser.Visible = False
            Me.XrPageInfo2.Visible = False
        End If

        Me.ShowPreview()
    End Sub

    Private Sub XRKrtOmsetAcc_PrintProgress(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles Me.PrintProgress
        Me.ClosePreview()
    End Sub
End Class